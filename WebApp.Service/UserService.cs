using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.UServices
{
    public class UserServices
    {
        private readonly AppDbContext _dbContext;

        public UserServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets a user by their ID.
        /// </summary>
        /// <param name="id">The user's ID.</param>
        /// <returns>The user if found, otherwise null.</returns>
        public async Task<User?> GetUtenteByIdAsync(string id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
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
        
        
        public async Task<bool> CheckEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return true;
            return false;
        }

       
    }
}
