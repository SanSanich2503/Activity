using System.Reflection;
using Core;
using Core.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Services.Services;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);
var services  = builder.Services;

services.AddHttpContextAccessor();
services.AddDbContext<DataContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
services.AddAdvancedDependencyInjection();
services.Scan(scan => scan
    .FromAssemblyOf<BaseRepository<Entity>>()
    .AddClasses(classes => classes
        .InNamespaces("Core.Entities")
        .AssignableTo<BaseRepository<Entity>>())
    .AsImplementedInterfaces()
    .WithTransientLifetime());
services.Scan(scan => scan
    .FromAssemblyOf<BaseService>()
    .AddClasses(classes => classes
        .InNamespaces("Data.ViewModels")
        .AssignableTo<BaseService>())
    .AsImplementedInterfaces()
    .WithTransientLifetime());

services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new PathString("/Auth/Login");
        options.AccessDeniedPath = new PathString("/Auth/AccessDenied");
    });
services.AddControllersWithViews();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Web API",
        Description = "API веб-версии приложения"
    });
    
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.RoutePrefix = "swagger");

app.UseAdvancedDependencyInjection();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    context.Database.Migrate();
    
    DbInitializer.Initialize(context);
}

app.Run();