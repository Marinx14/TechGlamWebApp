/*
namespace TechGlamWebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using WebApp.Services;
using WebApp.ViewModels;
using System.Threading.Tasks;
using System;

namespace WebApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _UserService;

        public UsersController(IUserService UserService)
        {
            _UserService = UserService;
        }

        public async Task<IActionResult> Index()
        {
            var Users = await _UserService.GetAllUsersAsync();
            return View(Users);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var User = await _UserService.GetUserByIdAsync(id);
            if (User == null)
            {
                return NotFound();
            }
            return View(User);
        }
    }
}
*/
