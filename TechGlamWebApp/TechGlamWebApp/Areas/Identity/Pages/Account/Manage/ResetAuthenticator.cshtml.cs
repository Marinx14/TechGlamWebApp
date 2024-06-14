// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechGlamWebApp.Models;
using WebApp.Models;

namespace TechGlamWebApp.Areas.Identity.Pages.Account.Manage
{
    public class ResetAuthenticatorModel : PageModel
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<ResetAuthenticatorModel> _logger;

        public ResetAuthenticatorModel(
            UserManager<User> UserManager,
            SignInManager<User> signInManager,
            ILogger<ResetAuthenticatorModel> logger)
        {
            _UserManager = UserManager;
            _signInManager = signInManager;
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
            var User = await _UserManager.GetUserAsync(User);
            if (User == null)
            {
                return NotFound($"Unable to load User with ID '{_UserManager.GetUserId(User)}'.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var User = await _UserManager.GetUserAsync(User);
            if (User == null)
            {
                return NotFound($"Unable to load User with ID '{_UserManager.GetUserId(User)}'.");
            }

            await _UserManager.SetTwoFactorEnabledAsync(User, false);
            await _UserManager.ResetAuthenticatorKeyAsync(User);
            var UserId = await _UserManager.GetUserIdAsync(User);
            _logger.LogInformation("User with ID '{UserId}' has reset their authentication app key.", User.Id);

            await _signInManager.RefreshSignInAsync(User);
            StatusMessage = "Your authenticator app key has been reset, you will need to configure your authenticator app using the new key.";

            return RedirectToPage("./EnableAuthenticator");
        }
    }
}
