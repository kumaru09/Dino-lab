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
    public class BookController : Controller
    {
        private readonly ILogger<BookController> _logger;
        private readonly ApiDbContext _db;

        public BookController(ILogger<BookController> logger, ApiDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<IActionResult> index(int id)
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

            LabList Lab = await _db.LabList.FindAsync(id);
            
            if (Lab == null) {
                return NotFound();
            }

            ItemList item = await _db.ItemList.FindAsync(id);

            DateTime _startDate = DateTime.UtcNow.AddHours(7).Date;
            if (chkmaxhr == 1) _startDate = _startDate.AddDays(1);
            DateTime endDate = _startDate.AddDays(14).Date;
            
            IEnumerable<BookingList> booked = _db.BookingList.Where(BookingList => (Lab.LabId == BookingList.EqId) && (BookingList.Date.Date < endDate)
            && (_startDate <= BookingList.Date.Date));

            int[,] BookTable = new int[14,7];
            for (int i = 0; i < 14; i++) {
                for (int j = 0; j < 7; j++) {
                    IEnumerable<BookingList> countBook = booked.Where(BookingList => _startDate.AddDays(i).Date == BookingList.Date.Date && BookingList.Date.Hour == j + 9);
                    if (BookTable[i,j] == 0 && countBook.Count() == 0) {
                        BookTable[i,j] = item.Amount;
                    }
                    else if (countBook.Count() == 0 && BookTable[i,j] != 0) {
                        BookTable[i,j] = item.Amount - BookTable[i,j];
                    }
                    else if (countBook.Count() != 0) {
                        foreach (BookingList b in countBook) {
                            int r = j;
                            for (int c = b.Time; c > 0; c--) {
                                BookTable[i,r]++;
                                r++;
                            }
                        }
                        BookTable[i,j] = item.Amount - BookTable[i,j];
                    }
                }
            }
            ViewBag.Booktable = BookTable;

            string[] itemName = {"Arduino","Hantek","3D printer","FPGA","Raspberry pi"};
            ViewBag.itemName = itemName[id-1]; 
            ViewBag.id = id;

            return View();
        }

         [HttpPost]
        public IActionResult Booking(int id, DateTime date, DateTime time, int hour)
        {
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Token"))) {
                var steam = HttpContext.Session.GetString("Token");
                var tokens = new JwtSecurityTokenHandler().ReadJwtToken(steam);

                var userId = tokens.Claims.First(claim => claim.Type == "Id").Value;
                var userRole = tokens.Claims.First(claim => claim.Type == "role").Value;

                //check blacklist user
                if(userRole == "Blacklist") return RedirectToAction("Index", "Home");

                //insert record to database
                BookingList booking = new BookingList();
                booking.UserId = userId;
                booking.EqId = id;
                booking.Date = date;
                booking.Time = time.Hour;

                _db.BookingList.Add(booking);
                _db.SaveChanges();

                int lastBookId = booking.BookId;
                return RedirectToAction("index", "Book", new {id = 1});
                }
            return RedirectToAction("Index", "Home");
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
