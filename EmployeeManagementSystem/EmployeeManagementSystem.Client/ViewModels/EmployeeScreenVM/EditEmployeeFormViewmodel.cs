
using EmployeeManagementSystem.Client.ViewModels.ComponentVM;
using Shared.Enums;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography.X509Certificates;

public class EditEmployeeFormViewmodel : IDisposable
{

    private readonly HttpClient _httpClient;
    private readonly NavbarViewmodel _navbarViewmodel;

    public EditEmployeeFormViewmodel(HttpClient httpClient, NavbarViewmodel navbarViewmodel)
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

    public int Id { get; set; }
    public string NameField { get; set; } = string.Empty;
    public string DescriptionField { get; set; } = string.Empty;
    public string AddressField { get; set; } = string.Empty;
    public double SalaryField { get; set; }
    public bool IsLocked { get; private set; } = false;
    public string StatusField { get; set; } = "Draft";

    public void Dispose()
    {
        NameField = string.Empty;
        DescriptionField = string.Empty;
        AddressField = string.Empty;
        SalaryField = 0;
        StatusField = _statuses[0];
    }

    public void ToggleLock()
    {
        IsLocked = !IsLocked;
        NotifyStateHasChanged();
    }

    public async Task LoadData(int id)
    {
        var response = await _httpClient.GetFromJsonAsync<EmployeeResponse>($"/api/v1/employees/{id}");
        Id = response.Id;
        NameField = response.Name;
        DescriptionField = response.Description;
        AddressField = response.Address;
        SalaryField = response.Salary;
        StatusField = response.Status;
        IsLocked = false; // just to make sure i dont wanna deal with this
    }

    private void NotifyStateHasChanged() => OnChange?.Invoke();

    public Action? OnChange { get; set; }
    public Action<string>? SnackbarLink { get; set; }

    public async Task HandleSave()
    {
        var payload = new UpdateEmployeeRequest
        {
            Id = Id,
            Name = NameField,
            Address = AddressField,
            Salary = SalaryField,
            Status = StatusField,
            Description = DescriptionField
        };


        try
        {
            var response = await _httpClient.PutAsJsonAsync("/api/v1/employees", payload);

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