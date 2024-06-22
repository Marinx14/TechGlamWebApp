using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    /// <summary>
    /// Represents a user in the application.
    /// </summary>
    public class User : IdentityUser
    {
        public bool isAdmin = VALUE;


        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        [PersonalData]
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "You must insert your Name!")]
        [StringLength(50, ErrorMessage = "The name can not be longer than 50 characters!")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the surname of the user.
        /// </summary>
        [PersonalData]
        [Column(TypeName = "nvarchar(60)")]
        [Required(ErrorMessage = "You must insert your Surname!")]
        [StringLength(60, ErrorMessage = "The surname can not be longer than 60 characters!")]
        public string Surname { get; set; }

         /// <summary>
        /// Gets the phone number of the User.
        /// </summary>
        [PersonalData]
        [Column(TypeName = "nvarchar(60)")]
        [Required(ErrorMessage = "You must insert your phone number!")]
        [StringLength(60, ErrorMessage = "The pohone number can not be longer than 13 characters!")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the birth date of the user.
        /// </summary>
        [PersonalData]
        [Column(TypeName = "date")]
        [Required(ErrorMessage = "You must insert your Birth Date!")]
        public DateTime BirthDate { get;  set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is an admin.
        /// </summary>
       

        public bool IsAdmin { get; set; }

        


        //public ICollection<Ordine> OrdiniUtente { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        
        public User(string name, string surname,string phoneNumber, DateTime birthDate, bool isadmin = false)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name), "Name cannot be null.");
            Surname = surname ?? throw new ArgumentNullException(nameof(surname), "Surname cannot be null.");
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;
            isAdmin = isadmin;
        }
    }
}
