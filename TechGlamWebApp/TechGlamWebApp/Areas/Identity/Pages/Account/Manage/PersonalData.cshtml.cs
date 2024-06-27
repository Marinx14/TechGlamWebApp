using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechGlamWebApp.Models;

namespace TechGlamWebApp.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        private readonly UserManager<User> _UserManager;
        private readonly ILogger<PersonalDataModel> _logger;

        public PersonalDataModel(
            UserManager<User> userManager,
            ILogger<PersonalDataModel> logger)
        {
            _UserManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            var currentUser = await _UserManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound($"Unable to load User with ID '{_UserManager.GetUserId(User)}'.");
            }

            return Page();
        }
    }
}