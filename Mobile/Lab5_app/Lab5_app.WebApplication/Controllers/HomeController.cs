using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Lab5_app.WebApplication.Models;
using Microsoft.AspNetCore.Http;

namespace Lab5_app.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new SignInViewModel());
        }

        [HttpPost]
        public IActionResult Index(SignInViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            HttpContext.Session.SetString("Username", vm.Username);

            return RedirectToAction("Chat");
        }

        [HttpGet("chat")]
        public IActionResult Chat()
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index");
            }
            return View("Chat", username);
        }
        
        
        public IActionResult Privacy()
        {
            return View();
        }
        
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}