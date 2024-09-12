using GoPlayAsiaWebApp;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazorServices();

var host = builder.Build();


// Attach global error handlers
//AppDomain.CurrentDomain.UnhandledException += async (sender, eventArgs) =>
//{
//    await ReloadPage(host.Services);
//};

//TaskScheduler.UnobservedTaskException += async (sender, eventArgs) =>
//{
//    await ReloadPage(host.Services);
//};

await host.RunAsync();

//async Task ReloadPage(IServiceProvider services)
//{
//    var jsRuntime = services.GetRequiredService<IJSRuntime>();
//    await jsRuntime.InvokeVoidAsync("reloadPage");
//}