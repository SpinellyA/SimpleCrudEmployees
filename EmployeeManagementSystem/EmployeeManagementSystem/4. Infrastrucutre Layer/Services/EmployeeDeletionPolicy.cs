
public class EmployeeDeletionPolicy : IEmployeeDeletionPolicy
{
    private readonly IEmployeeStatusRepository employeeStatusRepository;

    public EmployeeDeletionPolicy(IEmployeeStatusRepository employeeStatusRepository)
    {
        this.employeeStatusRepository = employeeStatusRepository;
    }

    public async Task<bool> CanDelete(Employee employee)
    {
        var status = await employeeStatusRepository.GetCurrentStatusAsync(employee.Id);
        return status?.EmployeeStatusType.Name == "Draft";
    }

}