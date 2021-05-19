using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly ContextData _context;
        public UserRepo(ContextData context)
        {
            _context = context;
        }
        public async Task AddUserAsync(AppUser user)
        {
            await _context.Users.AddAsync(user);
        }

        public void DeleteUser(AppUser user)
        {
            _context.Users.Remove(user);
        }

        public async Task<IEnumerable<AppUser>> GetAllUsersAsync(int myID)
        {
            return await _context.Users
                .Where(u => u.Id != myID).Include(u => u.UserRoles).ThenInclude(r => r.Role).ToListAsync();
        }

        public async Task<AppUser> GetSingleUserAsync(int userID)
        {
            return await _context.Users
                .Include(r => r.UserRoles).ThenInclude(r => r.Role).FirstOrDefaultAsync(x => x.Id == userID);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<AppUser> GetSingleUserByUsernameAsync(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }
    }
}