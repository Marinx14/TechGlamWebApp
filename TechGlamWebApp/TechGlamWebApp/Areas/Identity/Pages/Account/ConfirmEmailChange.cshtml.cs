// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using TechGlamWebApp.Models;
using WebApp.Models;

namespace TechGlamWebApp.Areas.Identity.Pages.Account
{
    public class ConfirmEmailChangeModel : PageModel
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _signInManager;

        public ConfirmEmailChangeModel(UserManager<User> UserManager, SignInManager<User> signInManager)
        {
            _UserManager = UserManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string UserId, string email, string code)
        {
            if (UserId == null || email == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var User = await _UserManager.FindByIdAsync(UserId);
            if (User == null)
            {
                return NotFound($"Unable to load User with ID '{UserId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _UserManager.ChangeEmailAsync(User, email, code);
            if (!result.Succeeded)
            {
                StatusMessage = "Error changing email.";
                return Page();
            }

            // In our UI email and User name are one and the same, so when we update the email
            // we need to update the User name.
            var setUserNameResult = await _UserManager.SetUserNameAsync(User, email);
            if (!setUserNameResult.Succeeded)
            {
                StatusMessage = "Error changing User name.";
                return Page();
            }

            await _signInManager.RefreshSignInAsync(User);
            StatusMessage = "Thank you for confirming your email change.";
            return Page();
        }
    }
}
