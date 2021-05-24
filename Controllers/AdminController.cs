using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dinolab.Models;
using Dinolab.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Dinolab.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private ApiDbContext _db;

        public AdminController(ILogger<AdminController> logger, ApiDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("Edit")]
        [HttpPost]
        public IActionResult Edit([FromBody] ItemList ItemUpdating)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(ItemUpdating).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [Route("viewItem")]
        [HttpGet]
        public async Task<IActionResult> viewItemInfo(int? itemId)
        {
            Console.WriteLine(itemId);
            if (itemId == null)
            {
                return RedirectToAction("Index"); 
            }
            ItemList itemInfo = await _db.ItemList.FindAsync(itemId);
            if (itemInfo == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.itemInfo = itemInfo;
                return new JsonResult(itemInfo);
            }
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
