using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;

namespace API.Repositories
{
    public interface IUserRepo
    {
        Task AddUserAsync(AppUser user);
        void DeleteUser(AppUser user);
        Task<bool> SaveAllAsync();
        Task<AppUser> GetSingleUserAsync(int userID);
        Task<AppUser> GetSingleUserByUsernameAsync(string userName);
        Task<IEnumerable<AppUser>> GetAllUsersAsync(int myID);
    }
}