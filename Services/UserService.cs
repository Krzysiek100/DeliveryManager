using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(IUserRepo userRepo, IMapper mapper, UserManager<AppUser> userManager)
        {
            _userRepo = userRepo;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IAsyncResult> DeleteUserAsync(int userID)
        {
            var userToDelete = await _userRepo.GetSingleUserAsync(userID);
            
            if(userToDelete==null)
                throw new Exception("User does not exist.");

            var result = await _userManager.DeleteAsync(userToDelete);

            if(!result.Succeeded) throw new Exception("Error occured");
            
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<UserToReturnDTO>> GetAllUserAsync(int myID)
        {
            var usersFromRepo = await _userRepo.GetAllUsersAsync(myID);
            
            if(usersFromRepo == null)
                throw new Exception("No users");

            var usersToReturn = _mapper.Map<IEnumerable<UserToReturnDTO>>(usersFromRepo);

            return usersToReturn;
        }

        public async Task<UserToReturnDTO> GetSingleUserByID(int userID)
        {
            var usersFromRepo = await _userRepo.GetSingleUserAsync(userID);

            if(usersFromRepo == null)
                throw new Exception("No such user");

            var userToReturn = _mapper.Map<UserToReturnDTO>(usersFromRepo);

            return userToReturn;
        }
    }
}