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
    [Authorize(Roles="Admin")]
    public class BlacklistController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<BlacklistController> _logger;

        public BlacklistController(ILogger<BlacklistController> logger, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // Add and Remove BlackList using ID of user
        [Route("AddBlackList")]
        [HttpPost]
        public async Task<IActionResult> AddBlackList([FromBody] IdentityUser user)
        {
            if (ModelState.IsValid)
            {
                var oldRole = await _userManager.GetRolesAsync(user);
                var blackListUser = await _userManager.FindByIdAsync(user.Id);
                if (oldRole != null)
                {
                    foreach (var role in oldRole)
                    {
                        if (role != "Blacklist")
                        {
                            var remove = await _userManager.RemoveFromRoleAsync(blackListUser, role);
                        }
                        else
                        {
                            return new JsonResult("This User is already in BlackList");
                        }

                    }
                    var presentRole = await _userManager.AddToRoleAsync(blackListUser, "Blacklist");
                    var newRole = _userManager.GetRolesAsync(user);
                    return RedirectToAction("Index");
                }
                return BadRequest(new JsonResult("This user doesn't has a role!"));

            }
            return RedirectToAction("Index");
        }

        [Route("RemoveBlackList")]
        [HttpPost]
        public async Task<IActionResult> RemoveBlackList([FromBody] IdentityUser user)
        {
            if (ModelState.IsValid)
            {
                var oldRole = await _userManager.GetRolesAsync(user);
                var blackListUser = await _userManager.FindByIdAsync(user.Id);
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
                    var newRole = _userManager.GetRolesAsync(user);
                    return RedirectToAction("Index");
                }
                return BadRequest(new JsonResult("This user doesn't has a role!"));

            }
            return RedirectToAction("Index");
        }

        [Route("ViewBlackList")]
        [HttpGet]
        public async Task<IActionResult> getBlackList()
        {
            var blackListUser = await _userManager.GetUsersInRoleAsync("Blacklist");
            string[,] userData = new string[blackListUser.Count,blackListUser.Count];
            var i = 0;
            foreach (var user in blackListUser)
            {
                userData[i,0] = user.UserName;
                userData[0,i] = user.Id;
                i++;
            } 
            ViewBag.blackListData = userData;
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
