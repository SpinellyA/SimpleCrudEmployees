
using Microsoft.EntityFrameworkCore;

public class EmployeeStatusRepository : IEmployeeStatusRepository
{
    private readonly MyDbContext _context;

    public EmployeeStatusRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<EmployeeStatus?> GetCurrentStatusAsync(int employeeId)
    => await _context.EmployeeStatuses
                     .Include(s => s.EmployeeStatusType)
                     .Where(s => s.EmployeeId == employeeId && s.EndTime == null)
                     .FirstOrDefaultAsync(); // tracking is enabled, we'll need it.

    public async Task AddStatusAsync(EmployeeStatus status)
        => await _context.EmployeeStatuses.AddAsync(status);

    public async Task RemoveStatusAsync(EmployeeStatus status)
        => _context.EmployeeStatuses.Remove(status);

}