﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechGlamWebApp.Models;

namespace TechGlamWebApp.Areas.Identity.Pages.Account.Manage
{
    public class Disable2faModel : PageModel
    {
        private readonly UserManager<User> _UserManager;
        private readonly ILogger<Disable2faModel> _logger;

        public Disable2faModel(
            UserManager<User> UserManager,
            ILogger<Disable2faModel> logger)
        {
            _UserManager = UserManager;
            _logger = logger;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var currentUser = await _UserManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound($"Unable to load User with ID '{_UserManager.GetUserId(User)}'.");
            }

            if (!await _UserManager.GetTwoFactorEnabledAsync(currentUser))
            {
                throw new InvalidOperationException($"Cannot disable 2FA for User as it's not currently enabled.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var currentUser = await _UserManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound($"Unable to load User with ID '{_UserManager.GetUserId(User)}'.");
            }

            var disable2faResult = await _UserManager.SetTwoFactorEnabledAsync(currentUser, false);
            if (!disable2faResult.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred disabling 2FA.");
            }

            _logger.LogInformation("User with ID '{UserId}' has disabled 2fa.", _UserManager.GetUserId(User));
            StatusMessage = "2fa has been disabled. You can reenable 2fa when you setup an authenticator app";
            return RedirectToPage("./TwoFactorAuthentication");
        }
    }
}
