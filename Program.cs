using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Serilog;

using SmartInventory3.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using SmartInventory3.Services;

var builder = WebApplication.CreateBuilder(args);

// Register services BEFORE building the app.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services
    .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>() // Add roles support
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Configure Serilog with more robust settings
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithProcessId()
    .Enrich.WithThreadId()
    .WriteTo.Console()
    .WriteTo.File("logs/smartinventory-.log", 
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 31)
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

// Replace .NET Core's built-in logging with Serilog
builder.Host.UseSerilog();

builder.Services.AddSingleton<IEmailSender, EmailSender>();

var app = builder.Build();

// Apply pending migrations on startup.
try
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        Log.Information("Applying database migrations if needed...");
        db.Database.Migrate();
        Log.Information("Database migrations applied successfully");
    }
}
catch (Exception ex)
{
    Log.Error(ex, "An error occurred while applying database migrations");
}

if (!app.Environment.IsDevelopment())
{
    // Global exception handler with logging
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "text/html";

            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionHandlerPathFeature != null)
            {
                var exception = exceptionHandlerPathFeature.Error;
                var path = exceptionHandlerPathFeature.Path;

                Log.Error(exception, "Unhandled exception occurred at {Path}", path);

                if (exception is System.Data.Common.DbException)
                {
                    Log.Error("Database error occurred when accessing {Path}", path);
                }
            }

            // Redirect users to a friendly error page
            context.Response.Redirect("/Home/Error");
            await System.Threading.Tasks.Task.CompletedTask;
        });
    });

    // Improved status code pages handling
    app.UseStatusCodePagesWithReExecute("/Home/StatusCodePage", "?code={0}");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // Make sure Authentication is before Authorization
app.UseAuthorization();
app.UseSerilogRequestLogging(); // Add request logging

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}");

try
{
    Log.Information("Starting SmartInventory3 web application");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}