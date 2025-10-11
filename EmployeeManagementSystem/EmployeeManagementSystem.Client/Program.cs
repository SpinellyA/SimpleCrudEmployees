using EmployeeManagementSystem.Client.ViewModels.ComponentVM;
using EmployeeManagementSystem.Client.ViewModels.EmployeeViewmodels;
using EmployeeManagementSystem.Client.ViewModels.State;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Components.Highlighter;
using MudBlazor.Services;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();
builder.Services.AddScoped<SplitterState>();
builder.Services.AddScoped<NavbarViewmodel>();
builder.Services.AddScoped<EmployeesListViewmodel>();
builder.Services.AddScoped<AddEmployeeFormViewmodel>();
builder.Services.AddScoped<EditEmployeeFormViewmodel>();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

await builder.Build().RunAsync();
