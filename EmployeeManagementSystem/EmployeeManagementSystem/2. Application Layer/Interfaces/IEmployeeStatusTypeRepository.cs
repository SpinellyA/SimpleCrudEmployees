
public interface IEmployeeStatusTypeRepository
{
    Task<EmployeeStatusType> GetEmployeeStatusTypeByNameAsync(string name);
    Task<IEnumerable<EmployeeStatusType>> GetEmployeeStatusTypesAsync();
}