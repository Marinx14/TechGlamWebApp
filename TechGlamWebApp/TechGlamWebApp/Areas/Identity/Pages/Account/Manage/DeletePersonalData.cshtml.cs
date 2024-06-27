// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TechGlamWebApp.Models;

namespace TechGlamWebApp.Areas.Identity.Pages.Account.Manage
{
    public class DeletePersonalDataModel : PageModel
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<DeletePersonalDataModel> _logger;

        public DeletePersonalDataModel(
            UserManager<User> UserManager,
            SignInManager<User> signInManager,
            ILogger<DeletePersonalDataModel> logger)
        {
            _UserManager = UserManager;
            _signInManager = signInManager;
            _logger = logger;
        }

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
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public bool RequirePassword { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var currentUser = await _UserManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound($"Unable to load User with ID '{_UserManager.GetUserId(User)}'.");
            }

            RequirePassword = await _UserManager.HasPasswordAsync(currentUser);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var currentUser = await _UserManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound($"Unable to load User with ID '{_UserManager.GetUserId(User)}'.");
            }

            RequirePassword = await _UserManager.HasPasswordAsync(currentUser);
            if (RequirePassword)
            {
                if (!await _UserManager.CheckPasswordAsync(currentUser, Input.Password))
                {
                    ModelState.AddModelError(string.Empty, "Incorrect password.");
                    return Page();
                }
            }

            var result = await _UserManager.DeleteAsync(currentUser);
            var currentUserId = await _UserManager.GetUserIdAsync(currentUser);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleting User.");
            }

            await _signInManager.SignOutAsync();

            _logger.LogInformation("User with ID '{currentUserId}' deleted themselves.", currentUserId);

            return Redirect("~/");
        }
    }
}
