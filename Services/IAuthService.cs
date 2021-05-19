using System;
using System.Threading.Tasks;
using API.DTOs;

namespace API.Services
{
    public interface IAuthService
    {
         Task<IAsyncResult> RegisterUserAsync(UserToRegisterDTO userToRegisterDTO, RoleName roleName);
         Task<UserToReturnDTO> LoginUserAsync(UserToLoginDTO userToLoginDTO);
    }
}