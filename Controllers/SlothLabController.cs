using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dinolab.Models;
using System.Net.Http;
using System.Text.Json;

namespace Dinolab.Controllers
{
    public class SlothLabController : Controller
    {
        private readonly ILogger<SlothLabController> _logger;

        public SlothLabController(ILogger<SlothLabController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string rootapi = "https://slothflying.azurewebsites.net";
            string[] lab =  new string[5];
            for (int i = 0; i < 5; i++)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(rootapi);
                var rtask = client.GetAsync("/Api/GetBooking/"+Convert.ToString(i+1));
                rtask.Wait();

                var result = rtask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var rcontent = result.Content.ReadAsStringAsync();
                    rcontent.Wait();

                    lab[i] = rcontent.Result;
                }
                else
                { //web api sent error response 
                  //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            DateTime startDate = DateTime.UtcNow.AddHours(7);

            ViewBag.startDate = startDate;
            ViewBag.rootapi = rootapi;
            ViewBag.lab1 = JsonDocument.Parse(lab[0]).RootElement;
            ViewBag.lab2 = JsonDocument.Parse(lab[1]).RootElement;
            ViewBag.lab3 = JsonDocument.Parse(lab[2]).RootElement;
            ViewBag.lab4 = JsonDocument.Parse(lab[3]).RootElement;
            ViewBag.lab5 = JsonDocument.Parse(lab[4]).RootElement;
            
            return View();
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