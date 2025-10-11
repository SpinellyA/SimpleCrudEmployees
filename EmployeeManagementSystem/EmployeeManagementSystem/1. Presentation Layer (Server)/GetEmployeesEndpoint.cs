using FastEndpoints;

public class GetEmployeesEndpoint : EndpointWithoutRequest<IEnumerable<EmployeeResponse>>
{
    private readonly EmployeeService employeeService;

    public GetEmployeesEndpoint(EmployeeService employeeService)
    {
        this.employeeService = employeeService;
    }

    public override void Configure()
    {
        Get("/api/v1/employees");
        AllowAnonymous();
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
        // validation is in the domain layer where business rules exist.
        var response = await employeeService.GetEmployeesAsync();
        await Send.OkAsync(response, ct);
    }
}