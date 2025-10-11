using FastEndpoints;

public class AddEmployeeEndpoint : Endpoint<AddEmployeeRequest>
{
    private readonly EmployeeService employeeService;
    public AddEmployeeEndpoint(EmployeeService employeeService)
    {
        this.employeeService = employeeService;
    }

    public override void Configure()
    {
        Post("/api/v1/employees");
        AllowAnonymous();
    }
    public override async Task HandleAsync(AddEmployeeRequest req, CancellationToken ct)
    {
        await employeeService.AddEmployeeAsync(req.Name, req.Description, req.Address, req.Salary, req.Status);
        await Send.OkAsync();
    }
}