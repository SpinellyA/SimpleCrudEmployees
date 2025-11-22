public class Query
{
    private readonly EmployeeService _employeeService;

    public Query(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public async Task<IEnumerable<EmployeeResponse>> GetEmployees()
    {
        try
        {
            return await _employeeService.GetEmployeesAsync();
        }
        catch (Exception ex)
        {
            // Log the error
            Console.WriteLine($"Error fetching employees: {ex}");

            // Option 1: Return empty list
            return new List<EmployeeResponse>();

            // Option 2: Throw a GraphQL-friendly error
            // throw new GraphQLException(new Error("Failed to fetch employees", "EMPLOYEES_FETCH_ERROR"));
        }
    }

    public async Task<EmployeeResponse> GetEmployee(int id)
    {
        try
        {
            return await _employeeService.GetEmployeeByIdAsync(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching employee {id}: {ex}");
            return null;

            // Or throw a GraphQL-friendly error
            // throw new GraphQLException(new Error($"Failed to fetch employee {id}", "EMPLOYEE_FETCH_ERROR"));
        }
    }
}
