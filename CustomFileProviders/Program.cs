using CustomFileProviders.AppData;
using CustomFileProviders.AppLib.FileProviders;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;

var builder = WebApplication.CreateBuilder(args);

//
// Services
// 

// https://stackoverflow.com/questions/48767910/entity-framework-core-a-second-operation-started-on-this-context-before-a-previ
// https://mehdi.me/ambient-dbcontext-in-ef6/#:%7E:text=DbContext%20is%20not%20thread%2Dsafe,over%20the%20same%20database%20connection.&text=Any%20instance%20members%20are%20not%20guaranteed%20to%20be%20thread%20safe.
// builder.Services.AddTransient<AppDbContext>();

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

await DataGenerator.Generate(app.Services);

app.UseDeveloperExceptionPage();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();

/*
 
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);
startup.Configure(app, app.Environment);


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