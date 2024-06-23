using DevExpress.AspNetCore;
using DevExpress.AspNetCore.Reporting;
using DevExpress.XtraReports.Web.Extensions;
using ReportingService.Services;

var builder = WebApplication.CreateBuilder(args);

// Register reporting services in an application's dependency injection container.
builder.Services.AddDevExpressControls();
builder.Services.AddScoped<ReportStorageWebExtension, CustomReportStorageWebExtension>(); 

// Use the AddMvcCore (or AddMvc) method to add MVC services.
builder.Services.AddControllersWithViews(); 


builder.Services.ConfigureReportingServices(configurator => {
    if(builder.Environment.IsDevelopment()) {
        configurator.UseDevelopmentMode();
    }
    configurator.ConfigureReportDesigner(designerConfigurator => {
        designerConfigurator.RegisterDataSourceWizardConfigFileConnectionStringsProvider();
    });
    configurator.ConfigureWebDocumentViewer(viewerConfigurator => {
        // Use cache for document generation and export.
        // This setting is necessary in asynchronous mode and when a report has interactive or drill down features.
        viewerConfigurator.UseCachedReportSourceBuilder();
    });
});

builder.Services.AddCors(options => {
    options.AddPolicy("AllowCorsPolicy", builder => {
        // Allow all ports on local host.
        builder.AllowAnyOrigin();
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("AllowCorsPolicy");

app.UseDevExpressControls();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseCors("AllowCorsPolicy");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();