using Dima.Web.Security;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Ucode.Core.Handlers;
using Ucode.Web;
using Ucode.Web.Handlers;
using Ucode.Web.Security;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<CookieHandler>(); // Adicionando o serviço

builder.Services.AddAuthorizationCore(); // recuso de autorização

builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>();
builder.Services.AddScoped(x =>
    (ICookieAuthenticationStateProvider)x.GetRequiredService<AuthenticationStateProvider>());


builder.Services.AddMudServices();
builder.Services.AddHttpClient(Configuration.HttpClientName, opt =>
{
    opt.BaseAddress = new Uri(Configuration.BackendUrl); //arquivo  Ucode.Api  "applicationUrl": "http://localhost:5270"
})
    .AddHttpMessageHandler<CookieHandler>();   // gerencia a request customizado e também os cookies

builder.Services.AddTransient<IAccountHandler, AccountHandler>();
builder.Services.AddTransient<IAlunoHandler, AlunoHandler>();
builder.Services.AddTransient<ICursoHandler, CursoHandler>();
builder.Services.AddTransient<IModuloHandler, ModuloHandler>();
builder.Services.AddTransient<IControleAlunoHandler, ControleAlunoHandler>();



await builder.Build().RunAsync();
