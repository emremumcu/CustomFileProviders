using CustomFileProviders.AppData;
using CustomFileProviders.AppLib.FileProviders;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;

var builder = WebApplication.CreateBuilder(args);

//
// Services
// 

builder.Services.AddDbContext<AppDbContext>();
IMvcBuilder mvcBuilder = builder.Services.AddControllersWithViews();

{   // Register DatabaseFileProvider
    //builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation(options => {
    //    options.FileProviders.Append(new DatabaseFileProvider());
    //});

    builder.Services
        .AddOptions<MvcRazorRuntimeCompilationOptions>()
        .Configure<IServiceProvider>((options, serviceProvider) =>
        {
            var provider = ActivatorUtilities.CreateInstance<DatabaseFileProvider>(serviceProvider);
            options.FileProviders.Add(provider);
        });

    // Install-Package Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation   
    mvcBuilder.AddRazorRuntimeCompilation();
}

// 
// Configuration
// 

var app = builder.Build();

app.UseDeveloperExceptionPage();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();

/*
 
 services.AddHttpContextAccessor();
services.AddMemoryCache();

service.AddTransient<IFileProvider, DatabaseFileProvider>(sp => {
    var cs = Configuration["AppSettings:SQLConnectionString"]);
    var provider = ActivatorUtilities.CreateInstance<DatabaseFileProvider>(sp, cs);
    return provider;
});

//... register other providers if any

services
    .AddOptions<MvcRazorRuntimeCompilationOptions>() 
    .Configure<IEnumerable<IFileProvider>>((options, providers) => {
        //add all registered providers
        foreach(IFileProvider provider in providers) {
            options.FileProviders.Add(provider);
        }
    });

services.AddRazorPages()
    .AddRazorRuntimeCompilation();
 */