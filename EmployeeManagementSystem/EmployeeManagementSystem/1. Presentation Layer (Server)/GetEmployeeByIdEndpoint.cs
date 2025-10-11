using FastEndpoints;

public class GetEmployeeByIdEndpoint : Endpoint<EmployeeRequest, EmployeeResponse>
{
    private readonly EmployeeService employeeService;

    public GetEmployeeByIdEndpoint(EmployeeService employeeService)
    {
        this.employeeService = employeeService;
    }

    public override void Configure()
    {
        Get("/api/v1/employees/{id:int}");
        AllowAnonymous();
    }
    public override async Task HandleAsync(EmployeeRequest req, CancellationToken ct)
    {
        var response = await employeeService.GetEmployeeByIdAsync(req.Id);

        if (response == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }
        await Send.OkAsync(response, ct);
    }
}