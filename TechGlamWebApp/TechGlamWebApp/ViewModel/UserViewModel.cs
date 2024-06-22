using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class UserViewModel
    {
        [Required]
        public Guid? Id { get; set; }
        
        [Required]
        public string? UserName { get; set; }
        
        [Required]
        public string? Email { get; set; }
    }
}
