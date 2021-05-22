using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dinolab.Models;

namespace Dinolab.Controllers
{
    public class BookController : Controller
    {
        private readonly ILogger<BookController> _logger;

        public BookController(ILogger<BookController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            DateTime startDate = DateTime.UtcNow.AddHours(7);
            DateTime hr9 = new DateTime(2021, 1, 1, 9, 00, 00);
            DateTime hr16 = new DateTime(2021, 1, 1, 16, 00, 00);
            int chkmaxhr = TimeSpan.Compare(startDate.TimeOfDay, hr16.TimeOfDay); // (date < hr16)[-1]
            int chkminhr = TimeSpan.Compare(startDate.TimeOfDay, hr9.TimeOfDay); // (date > hr9)[1]
            string curTime = "09:00";
            string maxHr = "7";
            if (chkmaxhr == -1)
            {
                if(chkminhr == 1){
                    curTime = startDate.ToString("HH") + ":00";
                    int intTime = Convert.ToInt32(startDate.ToString("HH"));
                    maxHr = Convert.ToString(16 - intTime);
                }
            }
            else {
                startDate = startDate.AddDays(1);
            }

            ViewBag.startDate = startDate;
            ViewBag.curTime = curTime;
            ViewBag.maxHr = maxHr;
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
