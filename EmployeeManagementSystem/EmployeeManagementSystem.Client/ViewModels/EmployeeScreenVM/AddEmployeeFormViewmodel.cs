using EmployeeManagementSystem.Client.ViewModels.ComponentVM;
using Shared.Enums;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;

public class AddEmployeeFormViewmodel : IDisposable
{

    private readonly HttpClient _httpClient;
    private readonly NavbarViewmodel _navbarViewmodel;

    public AddEmployeeFormViewmodel(HttpClient httpClient, NavbarViewmodel navbarViewmodel)
    {
        _httpClient = httpClient;
        _navbarViewmodel = navbarViewmodel;
    }

    public readonly string[] _statuses = [
        "Draft",
        "Active",
        "Inactive",
        "Deprecated"
        ];
    public string NameField { get; set; } = string.Empty;
    public string DescriptionField { get; set; } = string.Empty;
    public string AddressField { get; set; } = string.Empty;
    public double SalaryField { get; set; }
    public string StatusField { get; set; } = "Draft";


    private void NotifyStateHasChanged() => OnChange?.Invoke();

    public Action? OnChange { get; set; }
    public Action<string>? SnackbarLink { get; set; }

    public void Dispose()
    {
        NameField = string.Empty;
        DescriptionField = string.Empty;
        AddressField = string.Empty;
        SalaryField = 0;
        StatusField = _statuses[0];
    }

    public async Task HandleAdd()
    {
        var payload = new AddEmployeeRequest
        {
            Name = NameField,
            Address = AddressField,
            Salary = SalaryField,
            Status = StatusField,
            Description = DescriptionField
        };


        try
        {
            var response = await _httpClient.PostAsJsonAsync("/api/v1/employees", payload);

            if (response.IsSuccessStatusCode)
            {
                SnackbarLink?.Invoke("Success!");
                _navbarViewmodel.Selected = NavMenuItem.Employees_List;
            }
            else {
                SnackbarLink?.Invoke("Something went wrong.");
            }
        }
        catch
        {
            throw;
        }
        
    }

}