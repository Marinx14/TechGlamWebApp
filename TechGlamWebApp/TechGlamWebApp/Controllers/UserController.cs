using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApp.Data;
using WebApp.Models;
using WebApp.Services;

namespace TechGlamWebApp.Controllers
{
    public class UserController : Controller
    {
        public readonly UserServices _userServices;

        public UserController(ApplicationDbContext dbContext)
        {
            _userServices = new UserServices(dbContext);
        }

        [HttpGet]
        [Route("/PersonalArea")]
        [Authorize]
        public async Task<IActionResult> PersonalArea()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var personalArea = await _userServices.GetUserByIdAsync(userId);
            return View(personalArea);
        }

        [HttpPost]
        [Route("/PersonalArea")]
        [Authorize]
        public async Task<IActionResult> PersonalArea(string id, string firstName, string lastName, string phoneNumber, string email)
        {
            var user = await _userServices.GetUserByIdAsync(id);
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phoneNumber))
            {
                return RedirectToAction("PersonalArea");
            }

            if (await _userServices.CheckEmailExists(email))
            {
                ModelState.AddModelError("Email", "The entered email is already associated with another user.");
                return RedirectToAction("PersonalArea");
            }

            user.FirstName = firstName;
            user.LastName = lastName;
            user.Email = email;
            user.PhoneNumber = phoneNumber;

            await _userServices.UpdateUser(user);
            return RedirectToAction("PersonalArea");
        }
    }
}
