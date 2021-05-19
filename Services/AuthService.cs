using System;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Others;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AuthService(IMapper mapper, UserManager<AppUser> manager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _mapper = mapper;
            _userManager = manager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        public async Task<UserToReturnDTO> LoginUserAsync(UserToLoginDTO userToLoginDTO)
        {
            userToLoginDTO.UserName = userToLoginDTO.UserName.ToLower();
            
            var userFromRepo = await _userManager.FindByNameAsync(userToLoginDTO.UserName);

            if(userFromRepo == null)
                throw new Exception("Wrong credentials.");

            var result = await _signInManager
                .CheckPasswordSignInAsync(userFromRepo, userToLoginDTO.Password, false);
            
            if(!result.Succeeded) throw new Exception("Error occurred");

            var userToReturn = _mapper.Map<UserToReturnDTO>(userFromRepo);

            userToReturn.Token = await _tokenService.CreateTokenAsync(userFromRepo);

            return userToReturn;
        }
        public async Task<IAsyncResult> RegisterUserAsync(UserToRegisterDTO userToRegisterDTO, RoleName roleName)
        {
            userToRegisterDTO.UserName = userToRegisterDTO.UserName.ToLower();
            userToRegisterDTO.FirstName = userToRegisterDTO.FirstName.ToLower();
            userToRegisterDTO.LastName = userToRegisterDTO.LastName.ToLower();
            userToRegisterDTO.Email = userToRegisterDTO.Email.ToLower();

            if(await _userManager.FindByNameAsync(userToRegisterDTO.UserName)!=null)
                throw new Exception("Username is already taken.");

            var userToCreate = _mapper.Map<AppUser>(userToRegisterDTO);

            var result = await _userManager.CreateAsync(userToCreate, userToRegisterDTO.Password);

            switch(roleName)
            {
                case(RoleName.Admin):
                    await _userManager.AddToRoleAsync(userToCreate, "Admin");
                    break;
                case(RoleName.DeliveryMan):
                    await _userManager.AddToRoleAsync(userToCreate, "DeliveryMan");
                    break;
                case(RoleName.Member):
                    await _userManager.AddToRoleAsync(userToCreate, "Member");
                    break;
            }

            if(!result.Succeeded) throw new Exception(result.Errors.ToString());

            return Task.CompletedTask;

        }

    }
}