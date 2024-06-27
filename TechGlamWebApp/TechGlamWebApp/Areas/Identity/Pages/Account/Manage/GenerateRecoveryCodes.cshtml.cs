// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechGlamWebApp.Models;

namespace TechGlamWebApp.Areas.Identity.Pages.Account.Manage
{
    public class GenerateRecoveryCodesModel : PageModel
    {
        private readonly UserManager<User> _UserManager;
        private readonly ILogger<GenerateRecoveryCodesModel> _logger;

        public GenerateRecoveryCodesModel(
            UserManager<User> UserManager,
            ILogger<GenerateRecoveryCodesModel> logger)
        {
            _UserManager = UserManager;
            _logger = logger;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string[] RecoveryCodes { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var currentUser = await _UserManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound($"Unable to load User with ID '{_UserManager.GetUserId(User)}'.");
            }

            var isTwoFactorEnabled = await _UserManager.GetTwoFactorEnabledAsync(currentUser);
            if (!isTwoFactorEnabled)
            {
                throw new InvalidOperationException($"Cannot generate recovery codes for User because they do not have 2FA enabled.");
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

            var isTwoFactorEnabled = await _UserManager.GetTwoFactorEnabledAsync(currentUser);
            var UserId = await _UserManager.GetUserIdAsync(currentUser);
            if (!isTwoFactorEnabled)
            {
                throw new InvalidOperationException($"Cannot generate recovery codes for User as they do not have 2FA enabled.");
            }

            var recoveryCodes = await _UserManager.GenerateNewTwoFactorRecoveryCodesAsync(currentUser, 10);
            RecoveryCodes = recoveryCodes.ToArray();

            _logger.LogInformation("User with ID '{UserId}' has generated new 2FA recovery codes.", UserId);
            StatusMessage = "You have generated new recovery codes.";
            return RedirectToPage("./ShowRecoveryCodes");
        }
    }
}
