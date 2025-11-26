using EmployeeManagementSystem.Client.Pages;
using EmployeeManagementSystem.Components;
using MudBlazor.Services;
using MudExtensions.Services;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Client.ViewModels.EmployeeViewmodels;
using EmployeeManagementSystem.Client.ViewModels.ComponentVM;
using EmployeeManagementSystem.Client.ViewModels.State;

var builder = WebApplication.CreateBuilder(args);

// Viewmodels here part
builder.Services.AddScoped<SplitterState>();
builder.Services.AddScoped<NavbarViewmodel>();
builder.Services.AddScoped<EmployeesListViewmodel>();
builder.Services.AddScoped<AddEmployeeFormViewmodel>();
builder.Services.AddScoped<EditEmployeeFormViewmodel>();


// Those stuff with Interfaces here (usually Infra)
builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();
builder.Services.AddScoped<IEmployeeStatusRepository,EmployeeStatusRepository>();
builder.Services.AddScoped<IEmployeeStatusTypeRepository, EmployeeStatusTypeRepository>();
builder.Services.AddScoped<IEmployeeDeletionPolicy, EmployeeDeletionPolicy>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<EmployeeDeletionPolicy>();

builder.Services.AddScoped<Query>();
builder.Services.AddScoped<Mutation>();

// Application Layer Services here
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<EmployeeStatusService>();

// DbContext
builder.Services.AddDbContext<MyDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("EmployeeDb")));

// Add MudBlazor services
builder.Services.AddMudServices();
builder.Services.AddMudExtensions();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Fast Endpoints
builder.Services.AddFastEndpoints();

var serviceAddress = builder.Configuration["ServiceAddress"] ?? "";
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(serviceAddress) // or your server URL
});

var reactFrontEndUrl = builder.Configuration["ReactFrontEndUrl"] ?? "";
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins(reactFrontEndUrl) // React app origin
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// HotChocolateStuff
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

var app = builder.Build();

app.UseCors();
app.MapGraphQL();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseFastEndpoints();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(EmployeeManagementSystem.Client._Imports).Assembly);

app.Run();
