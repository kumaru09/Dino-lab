using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dinolab.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;

namespace Dinolab.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly ApiDbContext _db;

        public ProfileController(ILogger<ProfileController> logger, ApiDbContext db)
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
                ViewBag.name = tokens.Claims.First(claim => claim.Type == "sub").Value;
                ViewBag.email = tokens.Claims.First(claim => claim.Type == "email").Value;

                DateTime stateDate = DateTime.UtcNow.AddHours(7).Date;
                DateTime endDate = stateDate.AddDays(7).Date;
                TimeSpan diff = endDate.Subtract(stateDate);
                DateTime pastDate = stateDate - diff;
                
                IEnumerable<BookingList> history = _db.BookingList.Where(BookingList => BookingList.Date.Date < stateDate 
                && BookingList.Date.Date >= pastDate);

                string[,] res = new string[history.Count(),3];
                Console.WriteLine(history.Count());
                int i = 0;
                foreach (BookingList book in history) {
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
