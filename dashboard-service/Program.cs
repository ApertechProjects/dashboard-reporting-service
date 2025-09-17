using AspNetCoreDashboardBackend.Code;
using AspNetCoreDashboardBackend.Middlewares;
using DevExpress.AspNetCore;
using DevExpress.DashboardAspNetCore;
using DevExpress.DashboardWeb;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

IFileProvider? fileProvider = builder.Environment.ContentRootFileProvider;
IConfiguration? configuration = builder.Configuration;

builder.Services.AddCors(options => {
    options.AddPolicy("CorsPolicy", builder => {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});
builder.Services.AddDevExpressControls();
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(CustomExceptionFilter));
});

builder.Services.AddScoped<DashboardConfigurator>((IServiceProvider serviceProvider) => {
    return DashboardUtils.CreateDashboardConfigurator(configuration, fileProvider);
});


var app = builder.Build();

app.UseMiddleware<UserIdSetMiddleware>();
app.UseDevExpressControls();
app.UseDeveloperExceptionPage();
app.UseRouting();
app.UseCors("CorsPolicy");
app.MapDashboardRoute("api/dashboard", "DefaultDashboard");
app.MapControllers().RequireCors("CorsPolicy");
Console.WriteLine(configuration.GetConnectionString("Procurement"));
app.Run();
