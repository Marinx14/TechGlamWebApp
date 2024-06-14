// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechGlamWebApp.Models;
using WebApp.Models;

namespace TechGlamWebApp.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        private readonly UserManager<User> _UserManager;
        private readonly ILogger<PersonalDataModel> _logger;

        public PersonalDataModel(
            UserManager<User> UserManager,
            ILogger<PersonalDataModel> logger)
        {
            _UserManager = UserManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            var User = await _UserManager.GetUserAsync(User);
            if (User == null)
            {
                return NotFound($"Unable to load User with ID '{_UserManager.GetUserId(User)}'.");
            }

            return Page();
        }
    }
}
