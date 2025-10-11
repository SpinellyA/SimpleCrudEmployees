
public interface IEmployeeStatusRepository
{
    Task AddStatusAsync(EmployeeStatus status);
    Task<EmployeeStatus?> GetCurrentStatusAsync(int employeeId);
    Task RemoveStatusAsync(EmployeeStatus status);
}