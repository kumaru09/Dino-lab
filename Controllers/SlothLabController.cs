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

        public IActionResult Index(string labNum)
        {
            string rootapi = "https://slothflying.azurewebsites.net";
            string lab = "";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(rootapi);
            var rtask = client.GetAsync("/Api/GetBooking/" + labNum);
            rtask.Wait();

            var result = rtask.Result;
            if (result.IsSuccessStatusCode)
            {
                var rcontent = result.Content.ReadAsStringAsync();
                rcontent.Wait();

                lab = rcontent.Result;
            }
            DateTime startDate = DateTime.UtcNow.AddHours(7);

            ViewBag.startDate = startDate;
            ViewBag.rootapi = rootapi;
            ViewBag.labNum = labNum;
            ViewBag.lab = JsonDocument.Parse(lab).RootElement;

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