public class Mutation
{
    private readonly EmployeeService _employeeService;

    public Mutation(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public async Task<bool> DeleteEmployees(int[] ids)
    {
        try
        {
            foreach (int id in ids)
            {
                await _employeeService.DeleteEmployeeAsync(id);
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return false;
    }

    public async Task<bool> AddEmployee(string name, string description, string address, double salary, string stat)
    {
        try
        {
            await _employeeService.AddEmployeeAsync(name, description, address, salary, stat);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return false;
    }

    public async Task<bool> UpdateEmployee(int id, string name, string description, string address, double salary, string stat)
    {
        try
        {
            await _employeeService.UpdateEmployee(id, name, description, address, salary, stat);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return false;
    }


}
