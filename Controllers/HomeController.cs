using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SmartInventory3.Models;
using System.Net;
using Microsoft.Extensions.Logging;

namespace SmartInventory3.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    // Global error action - used for 500 errors
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        // Retrieve the exception details from the HTTP context.
        var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
        if (exceptionDetails != null)
        {
            _logger.LogError(exceptionDetails.Error, "An error occurred at {Path}", exceptionDetails.Path);
        }

        var errorViewModel = new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            Message = "An unexpected error occurred. Please try again later.",
            StatusCode = (int)HttpStatusCode.InternalServerError
        };
        
        Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        return View(errorViewModel);
    }

    // Handler for 404 - Not Found errors.
    [Route("Home/NotFound404")]
    public IActionResult NotFound404()
    {
        var errorViewModel = new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            Message = "The resource you are looking for could not be found.",
            StatusCode = (int)HttpStatusCode.NotFound
        };
        
        _logger.LogWarning("404 error encountered. Path: {Path}", HttpContext.Request.Path);
        Response.StatusCode = (int)HttpStatusCode.NotFound;
        return View("Error404", errorViewModel);
    }
    
    // Handler for 403 - Forbidden errors
    [Route("Home/Forbidden403")]
    public IActionResult Forbidden403()
    {
        var errorViewModel = new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            Message = "You do not have permission to access this resource.",
            StatusCode = (int)HttpStatusCode.Forbidden
        };
        
        _logger.LogWarning("403 error encountered. Path: {Path}, User: {User}", 
            HttpContext.Request.Path, 
            User.Identity?.Name ?? "Unknown");
            
        Response.StatusCode = (int)HttpStatusCode.Forbidden;
        return View("Error403", errorViewModel);
    }
    
    // Generic status code handler
    [Route("Home/StatusCodePage")]
    public IActionResult StatusCodePage(int code)
    {
        var errorViewModel = new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            Message = $"An error occurred with status code: {code}",
            StatusCode = code
        };
        
        _logger.LogWarning("Status code {Code} encountered. Path: {Path}", 
            code, 
            HttpContext.Request.Path);
            
        Response.StatusCode = code;
        return View("Error", errorViewModel);
    }
}