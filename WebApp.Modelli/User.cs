using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Models
{
        public class User : IdentityUser
        {
            /// <summary>
            /// Gets the first name of the user.
            /// </summary>
            [PersonalData]
            [Column(TypeName = "nvarchar(50)")]
            [Required(ErrorMessage = "You must insert your Name!")]
            [StringLength(50, ErrorMessage = "The name can not be longer than 50 characters!")]
            public string Name { get; private set; }

            /// <summary>
            /// Gets the last name of the user.
            /// </summary>
            [PersonalData]
            [Column(TypeName = "nvarchar(60)")]
            [Required(ErrorMessage = "You must insert your Surname!")]
            [StringLength(60, ErrorMessage = "The surname can not be longer than 60 characters!")]
            public string Surname { get; private set; }

            /// <summary>
            /// Gets the birth date of the user.
            /// </summary>
            [PersonalData]
            [Column(TypeName = "date")]
            [Required(ErrorMessage = "You must insert your Birth Date!")]
            public DateTime BirthDate { get; private set; }

            /// <summary>
            /// Gets a value indicating whether the user has administrative privileges.
            /// </summary>
            public bool IsAdmin { get; private set; }
        
            // Uncomment the following line if you need to establish a relationship with the Ordine entity.
            // public ICollection<Ordine> OrdiniUtente { get; set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="User"/> class.
            /// </summary>
            public User() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class with specified details.
        /// </summary>
        /// <param name="name">The first name of the user.</param>
        /// <param name="surname">The last name of the user.</param>
        /// <param name="birthDate">The birth date of the user.</param>
        /// <param name="isAdmin">Indicates whether the user has administrative privileges. Default is false.</param>
        public User(string name, string surname, DateTime birthDate, bool isAdmin = false)
            {
                Name = name ?? throw new ArgumentNullException(nameof(name), "Name cannot be null.");
                Surname = surname ?? throw new ArgumentNullException(nameof(surname), "Surname cannot be null.");
                BirthDate = birthDate;
                IsAdmin = isAdmin;
            }

            /// <summary>
            /// Sets the administrative status of the user.
            /// </summary>
            /// <param name="isAdmin">True to grant administrative privileges, false otherwise.</param>
            public void SetAdminStatus(bool isAdmin)
            {
                IsAdmin = isAdmin;
            }

            /// <summary>
            /// Updates the details of the user.
            /// </summary>
            /// <param name="nome">The new first name of the user.</param>
            /// <param name="cognome">The new last name of the user.</param>
            /// <param name="dataNascita">The new birth date of the user.</param>
            public void UpdateDetails(string name, string surname, DateTime birthDate)
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentException("Name cannot be just spaces or null", nameof(name));
                }

                if (string.IsNullOrWhiteSpace(surname))
                {
                    throw new ArgumentException("Surname cannot be just spaces or null", nameof(surname));
                }

                Name = name;
                Surname = surname;
                BirthDate = birthDate;
            }