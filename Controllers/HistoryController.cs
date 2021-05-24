using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dinolab.Models;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;

namespace Dinolab.Controllers
{
    public class HistoryController : Controller
    {
        private readonly ILogger<HistoryController> _logger;
        private readonly ApiDbContext _db;

        public HistoryController(ILogger<HistoryController> logger, ApiDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Token"))) {
                var steam = HttpContext.Session.GetString("Token");
                var tokens = new JwtSecurityTokenHandler().ReadJwtToken(steam);
                
                var userId = tokens.Claims.First(claim => claim.Type == "Id").Value;

                Console.WriteLine(userId);

                DateTime startDate = DateTime.UtcNow.AddHours(7);
                DateTime hr16 = new DateTime(2021, 1, 1, 16, 00, 00);
                int chkmaxhr = TimeSpan.Compare(startDate.TimeOfDay, hr16.TimeOfDay);
                if (chkmaxhr == 1) startDate = startDate.AddDays(1);
                DateTime endDate = startDate.AddDays(14);

                IEnumerable<BookingList> booked = _db.BookingList.Where(BookingList => (BookingList.UserId == userId) && (BookingList.Date < endDate)
                && (startDate <= BookingList.Date));
                
                string[,] res = new string[booked.Count(),3];
                Console.WriteLine(booked.Count());
                int i = 0;
                foreach (BookingList book in booked) {
                    res[i,0] = book.EqId.ToString();
                    res[i,1] = book.Date.ToString("MM/dd/yyyy hh:mm tt");
                    res[i,2] = book.Date.AddHours(book.Time).ToString("hh:mm tt");
                    Console.WriteLine(res[i,0]+" "+res[i,1]+" "+res[i,2]);
                    i++; 
                }
                ViewBag.res = res;
                
                return View();
            }
            return Redirect("Home");
            
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
