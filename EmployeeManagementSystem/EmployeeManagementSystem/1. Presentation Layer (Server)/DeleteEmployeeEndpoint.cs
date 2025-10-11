using FastEndpoints;

public class DeleteEmployeeEndpoint : Endpoint<DeleteEmployeeRequest>
{
    private readonly EmployeeService employeeService;
    private readonly EmployeeStatusService employeeStatusService;

    public DeleteEmployeeEndpoint(EmployeeService employeeService, EmployeeStatusService employeeStatusService)
    {
        this.employeeService = employeeService;
        this.employeeStatusService = employeeStatusService;
    }

    public override void Configure()
    {
        Delete("/api/v1/employees");
        AllowAnonymous();
    }
    public override async Task HandleAsync(DeleteEmployeeRequest req, CancellationToken ct)
    {

        foreach (var id in req.Ids)
        {
            await employeeService.DeleteEmployeeAsync(id); // no i cannot linq this. it throws a threading error.
        }

        await Send.OkAsync();
    }
}