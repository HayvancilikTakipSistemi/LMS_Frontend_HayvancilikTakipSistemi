using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using LMS.Client;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using LMS.Client.Auth;
using LMS.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiUrl = builder.Configuration["ApiUrl"] ?? "http://localhost:5171";

// Register Services
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

// Auth State Provider
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiUrl) });

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAnimalService, AnimalService>();
builder.Services.AddScoped<IVaccinationRecordService, VaccinationRecordService>();

await builder.Build().RunAsync();
