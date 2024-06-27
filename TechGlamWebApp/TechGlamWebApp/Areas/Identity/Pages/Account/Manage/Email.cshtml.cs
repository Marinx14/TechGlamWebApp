// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using TechGlamWebApp.Models;

namespace TechGlamWebApp.Areas.Identity.Pages.Account.Manage
{
    public class EmailModel : PageModel
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;

        public EmailModel(
            UserManager<User> UserManager,
            SignInManager<User> signInManager,
            IEmailSender emailSender)
        {
            _UserManager = UserManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "New email")]
            public string NewEmail { get; set; }
        }

        private async Task LoadAsync(User currentUser)
        {
            var email = await _UserManager.GetEmailAsync(currentUser);
            Email = email;

            Input = new InputModel
            {
                NewEmail = email,
            };

            IsEmailConfirmed = await _UserManager.IsEmailConfirmedAsync(currentUser);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var currentUser = await _UserManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound($"Unable to load User with ID '{_UserManager.GetUserId(User)}'.");
            }

            await LoadAsync(currentUser);
            return Page();
        }

        public async Task<IActionResult> OnPostChangeEmailAsync()
        {
            var currentUser = await _UserManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound($"Unable to load User with ID '{_UserManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(currentUser);
                return Page();
            }

            var email = await _UserManager.GetEmailAsync(currentUser);
            if (Input.NewEmail != email)
            {
                var UserId = await _UserManager.GetUserIdAsync(currentUser);
                var code = await _UserManager.GenerateChangeEmailTokenAsync(currentUser, Input.NewEmail);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmailChange",
                    pageHandler: null,
                    values: new { area = "Identity", UserId = UserId, email = Input.NewEmail, code = code },
                    protocol: Request.Scheme);
                await _emailSender.SendEmailAsync(
                    Input.NewEmail,
                    "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                StatusMessage = "Confirmation link to change email sent. Please check your email.";
                return RedirectToPage();
            }

            StatusMessage = "Your email is unchanged.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            var currentUser = await _UserManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound($"Unable to load User with ID '{_UserManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(currentUser);
                return Page();
            }

            var UserId = await _UserManager.GetUserIdAsync(currentUser);
            var email = await _UserManager.GetEmailAsync(currentUser);
            var code = await _UserManager.GenerateEmailConfirmationTokenAsync(currentUser);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", UserId = UserId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage();
        }
    }
}
