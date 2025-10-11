using FastEndpoints;

public class UpdateEmployeeEndpoint : Endpoint<UpdateEmployeeRequest>
{
    private readonly EmployeeService employeeService;

    public UpdateEmployeeEndpoint(EmployeeService employeeService)
    {
        this.employeeService = employeeService;
    }

    public override void Configure()
    {
        Put("/api/v1/employees");
        AllowAnonymous();
    }
    public override async Task HandleAsync(UpdateEmployeeRequest req, CancellationToken ct)
    {
        await employeeService.UpdateEmployee(req.Id, req.Name, req.Description, req.Address, req.Salary, req.Status);
        await Send.OkAsync();
    }
}