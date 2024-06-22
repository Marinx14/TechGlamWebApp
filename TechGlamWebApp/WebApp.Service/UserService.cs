using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Service
{
    public class UserService
    {
        private readonly AppDbContext _dbContext;

        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets a user by their ID.
        /// </summary>
        /// <param name="id">The user's ID.</param>
        /// <returns>The user if found, otherwise null.</returns>
        public async Task<User> GetUtenteByIdAsync(string id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        /// <summary>
        /// Checks if an email is already associated with an existing user.
        /// </summary>
        /// <param name="email">The email to check.</param>
        /// <returns>True if the email is already in use, otherwise false.</returns>
        public async Task<bool> ControlloMail(string email)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email == email);
        }

        /// <summary>
        /// Updates a user.
        /// </summary>
        /// <param name="utente">The user to update.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        
        public async Task UpdateUser(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }
        
        
        public async Task<bool> checkEmail(string email)
        {
            throw new NotImplementedException();
        }

       
    }
}