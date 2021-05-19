using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Repositories;
using AutoMapper;

namespace API.Services
{
    public class PackageService : IPackageService
    {
        private readonly IPackageStatusRepo _packageStatusRepo;
        private readonly IPackageRepo _packageRepo;
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public PackageService(IPackageStatusRepo packageStatusRepo ,IUserRepo userRepo, IPackageRepo packageRepo, IMapper mapper)
        {
            _packageStatusRepo = packageStatusRepo;
            _packageRepo = packageRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<IAsyncResult> AddPackageAsync(PackageToAddDTO packageToAddDTO)
        {
            var user = await _userRepo.GetSingleUserAsync(packageToAddDTO.DeliveryManId);
            
            if (user.UserRoles.FirstOrDefault(x => x.UserId == user.Id).Role.Name == "Admin") 
                throw new Exception("Wybrałeś administratora. Proszę wybierz kuriera.");

            var packageToAdd = _mapper.Map<Package>(packageToAddDTO);

            await _packageRepo.AddPackageAsync(packageToAdd);

            if(await _packageRepo.SaveAllAsync())
                return Task.CompletedTask;

            throw new Exception("Error occurred");
        }

        public async Task<IAsyncResult> DeletePackageAsync(int packageID)
        {
            var packageFromRepo = await _packageRepo.GetSinglePackageAsync(packageID);

            if(packageFromRepo == null)
                throw new Exception("Not found");

            _packageRepo.DeletePackage(packageFromRepo);

            if(await _packageRepo.SaveAllAsync())
                return Task.CompletedTask;

            throw new Exception("Error occurred");
        }

        public async Task<PackageToReturnDTO> GetSinglePackageAsync(int packageID)
        {
            var packageFromRepo = await _packageRepo.GetSinglePackageAsync(packageID);

            if(packageFromRepo == null)
                throw new Exception("Not found");

            var packageToReturn = _mapper.Map<PackageToReturnDTO>(packageFromRepo);

            return packageToReturn;
        }

        public async Task<IEnumerable<PackageToReturnDTO>> GetPackagesForDeliveryManAsync(int userID)
        {
            var packagesFromRepo = await _packageRepo.GetPackagesForDeliveryManAsync(userID);

            if(packagesFromRepo == null)
                throw new Exception("Not found");

            var packagesToReturn = _mapper.Map<IEnumerable<PackageToReturnDTO>>(packagesFromRepo);

            return packagesToReturn;
        }

        public async Task<IEnumerable<PackageToReturnDTO>> GetAllPackagesAsync()
        {
            var packagesFromRepo = await _packageRepo.GetAllPackagesAsync();

            if(packagesFromRepo == null)
                throw new Exception("Not found");

            var packagesToReturn = _mapper.Map<IEnumerable<PackageToReturnDTO>>(packagesFromRepo);

            return packagesToReturn;
        }
    }
}