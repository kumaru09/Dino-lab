using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dinolab.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Dinolab.Controllers
{
    //[Authorize(Roles="Admin")]
    public class BlacklistController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<BlacklistController> _logger;

        public BlacklistController(ILogger<BlacklistController> logger, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var blackListUser = await _userManager.GetUsersInRoleAsync("Blacklist");
            string[,] userData = new string[2, blackListUser.Count];
            var i = 0;
            var j = 0;
            foreach (var user in blackListUser)
            {
                userData[0, i] = user.UserName;
                userData[1, i] = user.Id;
                i++;
            }
            ViewBag.blackListData = userData;
            ViewBag.dataCount = blackListUser.Count;
            Console.WriteLine(ViewBag.dataCount);
            foreach (var user in blackListUser)
            {
                Console.WriteLine(ViewBag.blackListData[0, j]);
                Console.WriteLine(ViewBag.blackListData[1, j]);
                j++;
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // Add and Remove BlackList using ID of user
        [Route("AddBlackList")]
        [HttpPost]
        public async Task<IActionResult> AddBlackList(string username)
        {
            var blackListUser = await _userManager.FindByNameAsync(username);
            var userId = await _userManager.GetUserIdAsync(blackListUser);
            var idObj = await _userManager.FindByIdAsync(userId);
            var oldRole = await _userManager.GetRolesAsync(idObj);
            if (oldRole != null)
            {
                foreach (var role in oldRole)
                {
                    if (role != "Blacklist")
                    {
                        var remove = await _userManager.RemoveFromRoleAsync(idObj, role);
                    }
                    else
                    {
                        return new JsonResult("This User is already in BlackList");
                    }

                }
                var presentRole = await _userManager.AddToRoleAsync(idObj, "Blacklist");
                var newRole = _userManager.GetRolesAsync(idObj);
                return RedirectToAction("Index");
            }
            return BadRequest(new JsonResult("This user doesn't has a role!"));
        }

        [Route("RemoveBlackList")]
        [HttpPost]
        public async Task<IActionResult> RemoveBlackList(string id)
        {
            var blackListUser = await _userManager.FindByIdAsync(id);
            var oldRole = await _userManager.GetRolesAsync(blackListUser);
            if (oldRole != null)
            {
                foreach (var role in oldRole)
                {
                    if (role != "User")
                    {
                        var remove = await _userManager.RemoveFromRoleAsync(blackListUser, role);
                    }
                    else
                    {
                        return new JsonResult("This User is already in a User role");
                    }

                }
                var presentRole = await _userManager.AddToRoleAsync(blackListUser, "User");
                var newRole = await _userManager.GetRolesAsync(blackListUser);
                return RedirectToAction("Index");
            }
            return BadRequest(new JsonResult("This user doesn't has a role!"));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
