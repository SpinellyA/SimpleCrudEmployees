
public interface IEmployeeDeletionPolicy
{
    Task<bool> CanDelete(Employee employee);
}