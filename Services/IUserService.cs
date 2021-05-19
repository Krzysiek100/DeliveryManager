using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;

namespace API.Services
{
    public interface IUserService
    {
         Task<IAsyncResult> DeleteUserAsync(int userID);
         Task<IEnumerable<UserToReturnDTO>> GetAllUserAsync(int myID);
         Task<UserToReturnDTO> GetSingleUserByID(int userID);
    }
}