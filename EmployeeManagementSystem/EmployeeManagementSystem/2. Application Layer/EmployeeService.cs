
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using Shared.Enums;
using System.Data;
using System.Security.Cryptography.X509Certificates;
public class EmployeeService
{
    private readonly EmployeeStatusService _statusService;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmployeeStatusTypeRepository _employeeStatusTypeRepository;
    private readonly IEmployeeDeletionPolicy _employeeDeletionPolicy;
    private readonly IEmployeeStatusRepository _employeeStatusRepository;
    private readonly IUnitOfWork _uow;

    public EmployeeService(IEmployeeRepository employeeRepository, IEmployeeStatusRepository employeeStatusRepository, IUnitOfWork uow, IEmployeeDeletionPolicy employeeDeletionPolicy, IEmployeeStatusTypeRepository employeeStatusTypeRepository, EmployeeStatusService statusService)
    {
        _employeeRepository = employeeRepository;
        _employeeStatusRepository = employeeStatusRepository;
        _uow = uow;
        _employeeDeletionPolicy = employeeDeletionPolicy;
        _employeeStatusTypeRepository = employeeStatusTypeRepository;
        _statusService = statusService;
    }

    /*
     * return (await _employeeRepository.GetEmployeesAsync())
     * .Select(async e => new EmployeeResponse
     *{ Id = e.Id, Address = e.Address, Description = e.Description, Name = e.Name, Salary = e.Salary, 
     * Status = (await _employeeStatusRepository.GetCurrentStatusAsync(e.Id)).EmployeeStatusType.Name)
     */
    public async Task<IEnumerable<EmployeeResponse>> GetEmployeesAsync() // I honestly tried to LINQ this. Commented code above showcases attempt.
    {
        var employees = await _employeeRepository.GetEmployeesAsync();
        var responses = new List<EmployeeResponse>();

        foreach (var e in employees)
        {
            var status = await _employeeStatusRepository.GetCurrentStatusAsync(e.Id);

            responses.Add(new EmployeeResponse
            {
                Id = e.Id,
                Address = e.Address,
                Description = e.Description,
                Name = e.Name,
                Salary = e.Salary,
                Status = status?.EmployeeStatusType?.Name
            });
        }

        return responses;
    }

    public async Task UpdateEmployee(int id, string name, string description, string address, double salary, string status)
    {
        var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

        await _uow.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        try
        {
            employee.Name = name;
            employee.Description = description;
            employee.Address = address;
            employee.Salary = salary;

            await _statusService.UpdateEmployeeStatus(employee, status);

            await _uow.CommitAsync();

        } catch
        {
            await _uow.RollBackAsync();
            throw;
        }

    } 

    public async Task AddEmployeeAsync(string name, string description, string address, double salary, string stat)
    {

        var st = await _employeeStatusTypeRepository.GetEmployeeStatusTypeByNameAsync(stat);

        await _uow.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        try
        {
            var employee = new Employee(name, address, salary, description);

            await _employeeRepository.AddEmployeeAsync(employee);

            var status = new EmployeeStatus(employee, st, DateTime.UtcNow);

            await _employeeStatusRepository.AddStatusAsync(status);
            await _uow.CommitAsync();
        }
        catch
        {
            await _uow.RollBackAsync();
            throw;
        }
    }

    public async Task DeleteEmployeeAsync(int id)
    {

        var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

        if (employee == null)
        {
            throw new Exception("Employee not found!");
        }

        if(!(await _employeeDeletionPolicy.CanDelete(employee))) // it honestly feels more right to do this than domain business rules.
        {
            throw new Exception("Employee cannot be deleted!");
        }

        
        await _uow.BeginTransactionAsync(IsolationLevel.ReadCommitted); // do i need this?
                                                                        // yes, i may add logging support later. 
        try
        {
            await _employeeRepository.RemoveEmployee(employee); 
            await _uow.CommitAsync();
        } catch
        {
            await _uow.RollBackAsync();
            throw;
        }
    }

    public async Task<EmployeeResponse> GetEmployeeByIdAsync(int id)
    {
        var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
        var status = await _employeeStatusRepository.GetCurrentStatusAsync(id);

        if (status == null)
            throw new Exception("Status not found!");
        if (employee == null)
            throw new Exception("Employee not found!");

        return new EmployeeResponse
        {
            Id = employee.Id,
            Address = employee.Address,
            Description = employee.Description,
            Name = employee.Name,
            Salary = employee.Salary,
            Status = status.EmployeeStatusType.Name
        };
    }
}