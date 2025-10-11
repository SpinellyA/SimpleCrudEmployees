
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using Shared.Enums;
using System.Data;
public class EmployeeStatusService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmployeeStatusRepository _employeeStatusRepository;
    private readonly IEmployeeStatusTypeRepository _employeeStatusTypeRepository;
    private readonly IUnitOfWork _uow; // will be useful later

    public EmployeeStatusService(IEmployeeRepository employeeRepository, IEmployeeStatusRepository employeeStatusRepository, IUnitOfWork uow, IEmployeeStatusTypeRepository employeeStatusTypeRepository)
    {
        _employeeRepository = employeeRepository;
        _employeeStatusRepository = employeeStatusRepository;
        _uow = uow;
        _employeeStatusTypeRepository = employeeStatusTypeRepository;
    }

    public async Task UpdateEmployeeStatus(Employee employee, string employeeStatusString)
    {
        var employeestatus = await _employeeStatusRepository.GetCurrentStatusAsync(employee.Id);
        var newstatustype = await _employeeStatusTypeRepository.GetEmployeeStatusTypeByNameAsync(employeeStatusString);

        if (employeestatus == null)
        {
            throw new Exception("Employee status not found!");
        }

        if(employeestatus.EmployeeStatusType.Name == employeeStatusString)
        {
            return; // no error.
        }

            employeestatus.EndStatus(); // this should end it
            var newStatus = new EmployeeStatus(employee, newstatustype, DateTime.UtcNow);
            await _employeeStatusRepository.AddStatusAsync(newStatus);


    }

}

