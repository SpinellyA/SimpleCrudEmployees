
using Microsoft.EntityFrameworkCore;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly MyDbContext _context;

    public EmployeeRepository(MyDbContext context)
    {
        _context = context;
    }
    public async Task<Employee?> GetEmployeeByIdAsync(int id) =>
        await _context.Employees.FindAsync(id);
    public async Task<IEnumerable<Employee>> GetEmployeesAsync() => await _context.Employees.AsNoTracking().ToListAsync();
    public async Task AddEmployeeAsync(Employee employee) => await _context.Employees.AddAsync(employee);
    public async Task RemoveEmployee(Employee employee) => _context.Employees.Remove(employee);
}