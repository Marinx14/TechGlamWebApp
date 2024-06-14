
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Service
{
    /// <summary>
    /// Service class for managing user operations.
    /// </summary>
    public class UserService
    {
        private readonly AppDbContext appDbContext;

        public UserService(AppDbContext dbContext)
        {
            appDbContext = dbContext;
        }

        /// <summary>
        /// Retrieves a user by ID asynchronously.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>The user with the specified ID.</returns>
        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await appDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        /// <summary>
        /// Updates user information asynchronously.
        /// </summary>
        /// <param name="updatedUser">The updated user information.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UpdateUserAsync(User updatedUser)
        {
            var existingUser = await GetUserByIdAsync(updatedUser.Id);

            if (existingUser != null)
            {
                existingUser.Name = updatedUser.Name;
                existingUser.Surname = updatedUser.Surname;
                existingUser.Email = updatedUser.Email;
                existingUser.PhoneNumber = updatedUser.PhoneNumber;

                await appDbContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("User to update not found in database.");
            }
        }

        /// <summary>
        /// Checks if an email is available (not already taken).
        /// </summary>
        /// <param name="email">The email to check.</param>
        /// <returns>True if the email is available, otherwise false.</returns>
        public bool IsEmailAvailable(string email)
        {
            return !appDbContext.Users.Any(u => u.Email == email);
        }

        /// <summary>
        /// Deletes a user account asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DeleteUserAsync(string userId)
        {
            var userToDelete = await appDbContext.Users.FindAsync(userId);
            if (userToDelete != null)
            {
                appDbContext.Users.Remove(userToDelete);
                await appDbContext.SaveChangesAsync();
            }
        }
    }
}
