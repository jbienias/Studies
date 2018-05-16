using CSGO.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace CSGO.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Blad()
        {
            throw new Exception("Eldo");
        }

        public IActionResult Error(int? statusCode)
        {
            var vm = new ErrorViewModel
            {
                Response = statusCode?.ToString() ?? "-",
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Ip = HttpContext.Connection.LocalIpAddress
            };

            return View(vm);
        }
    }
}
