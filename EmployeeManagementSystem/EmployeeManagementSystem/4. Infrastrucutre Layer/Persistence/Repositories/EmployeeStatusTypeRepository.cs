
using Microsoft.EntityFrameworkCore;

public class EmployeeStatusTypeRepository : IEmployeeStatusTypeRepository
{
    private readonly MyDbContext _context;

    public EmployeeStatusTypeRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<EmployeeStatusType> GetEmployeeStatusTypeByNameAsync(string name)
        => await _context.EmployeeStatusTypes
                .AsNoTracking()
                .FirstAsync(e => e.Name == name);

    public async Task<IEnumerable<EmployeeStatusType>> GetEmployeeStatusTypesAsync() => await _context.EmployeeStatusTypes.AsNoTracking().ToListAsync();


}