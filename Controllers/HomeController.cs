using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using RandomPasscode.Models;

namespace RandomPasscode.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("Count") == null)
            {
                HttpContext.Session.SetInt32("Count", 1);
            }
            ViewBag.Count = HttpContext.Session.GetInt32("Count");
            ViewBag.Password = HttpContext.Session.GetString("Password");
            return View();
        }

        [HttpGet]
        [Route("/generate")]
        public IActionResult Generate()
        {
            int CountVar = (int)HttpContext.Session.GetInt32("Count");
            HttpContext.Session.SetInt32("Count", CountVar + 1);
            ViewBag.Count = HttpContext.Session.GetInt32("Count");

            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvqxyz0123456789";
            string randPassword = "";
            Random rand = new Random();
            for (int i = 0; i <= 14; i++)
            {
                randPassword += chars[rand.Next(0, chars.Length)];
            }
            HttpContext.Session.SetString("Password", randPassword);
            ViewBag.Password = HttpContext.Session.GetString("Password");

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
