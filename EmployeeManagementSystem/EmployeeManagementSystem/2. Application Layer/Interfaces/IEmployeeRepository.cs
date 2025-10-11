
using Microsoft.EntityFrameworkCore;

public interface IEmployeeRepository
{
    Task AddEmployeeAsync(Employee employee);
    Task<IEnumerable<Employee>> GetEmployeesAsync();

    Task<Employee> GetEmployeeByIdAsync(int id); 
    Task RemoveEmployee(Employee employee);
}