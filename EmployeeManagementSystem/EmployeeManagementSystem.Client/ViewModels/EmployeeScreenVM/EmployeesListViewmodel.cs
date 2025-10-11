using EmployeeManagementSystem.Client.ViewModels.ComponentVM;
using MudBlazor;
using Shared.Enums;
using System.Collections;
using System.Net.Http.Json;
using System.Threading.Tasks;
namespace EmployeeManagementSystem.Client.ViewModels.EmployeeViewmodels
{
    public class EmployeesListViewmodel
    {
        private readonly HttpClient _httpClient;
        private readonly NavbarViewmodel _navbarViewmodel;
        private readonly EditEmployeeFormViewmodel _editEmployeeFormViewmodel;
        public EmployeesListViewmodel(HttpClient httpClient, NavbarViewmodel navbarViewmodel, EditEmployeeFormViewmodel editEmployeeFormViewmodel)
        {
            _httpClient = httpClient;
            _navbarViewmodel = navbarViewmodel;
            _editEmployeeFormViewmodel = editEmployeeFormViewmodel;
        }
        public MudTable<EmployeeResponse> _table { get; set; }
        public IEnumerable<EmployeeResponse> Employees { get; set; }
        public HashSet<EmployeeResponse> selectedItems { get; set; }
        public int _pageIndex { get; set; }
        public int _totalPages { get; set; }

        public Action<string>? SnackbarLink { get; set; }
        public Action? NotifyStateChanged { get; set; }

        public async Task LoadData()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<EmployeeResponse>>("/api/v1/employees");
            Employees = response;
        }

        public void HandleAdd()
        {
            _navbarViewmodel.Selected = NavMenuItem.Employees_Add;
        }

        public async Task HandleDelete()
        {
            var payload = new DeleteEmployeeRequest
            {
                Ids = selectedItems.Select(x => x.Id).ToList(),
            };
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/v1/employees")
            {
                Content = JsonContent.Create(payload)
            };

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                SnackbarLink?.Invoke("Deleted employees successfully!");
            }
            else
            {
                SnackbarLink?.Invoke("There was an error deleting employees.");
            }
            await LoadData();
            NotifyStateChanged?.Invoke();
        }

        public async Task HandleView()
        {
            var id = selectedItems.Select(x => x.Id).FirstOrDefault();
            await _editEmployeeFormViewmodel.LoadData(id);
            _navbarViewmodel.Selected = NavMenuItem.Employees_View;
        }

        public async Task OnRowClick()
        {

        }
        public async Task NextPage()
        {

        }
        public async Task PrevPage()
        {

        }
        public async Task FirstPage()
        {

        }
        public async Task LastPage()
        {

        }
    }
}
