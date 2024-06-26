﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApp.Data;
using WebApp.Models;
using WebApp.UServices;

namespace WebApp.Controllers
{

    public class UtenteController : Controller
    {
        public readonly UserServices _userService;

        public UtenteController(AppDbContext _dbContext)
        {
            _userService = new UserServices(_dbContext);
        }
        [HttpGet]
        [Route("/PersonalArea")]
        [Authorize]
        public async Task<IActionResult> PersonalArea(string idUser)
        {
            string? idUtente = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //bool ruolo = User.IsInRole("Admin");
            //var user = new User
            //{
            //    Id = idUser,
            //    isAdmin = ruolo,
            //};
            var personalArea = await _userService.GetUtenteByIdAsync(idUser);
            return View(personalArea);
        }

        [HttpPost]
        [Route("/PersonalArea")]
        [Authorize]
        public async Task<IActionResult> AreaPersonale(string id, string name, string surname, string phoneNumber, string email)
        {
            var user = await _userService.GetUtenteByIdAsync(id);
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phoneNumber)) { RedirectToAction("PersonalArea"); }

            if (await _userService.CheckEmail(email))
            {
                ModelState.AddModelError("Email", "The entered email is already associated with another user.");
                return RedirectToAction("PersonalArea");
            }
            if (user != null)
            {
                user.Name = name;
                user.Surname = surname;
                user.Email = email;
                user.PhoneNumber = phoneNumber;
            }

            if (user != null)
            {
                await _userService.UpdateUser(user);
            }
            return RedirectToAction("PersonalArea");
        }
    }
}

