using System.Diagnostics;
using AspNetCore.Hashids.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using owasp1.Models;

namespace owasp1.Controllers;

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

    [HttpGet("GetCustomers")]
    public IActionResult GetCustomers()
    {
        return Json(new List<Customer>()
        {
            new Customer()
            {
                Id = 1,
                FirstName = "firstname",
                LastName  = "lastName"
            },
            new Customer()
            {
                Id = 1,
                FirstName = "firstname",
                LastName  = "lastName"
            }
        });
    }

    [Authorize(Policy = "just-owner")]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public class Customer
    {
        [JsonConverter(typeof(HashidsJsonConverter))]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
