using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Repositories;
using AutoMapper;

namespace API.Services
{
    public class PackageStatusService : IPackageStatusService
    {
        private readonly IPackageStatusRepo _packageStatusRepo;
        private readonly IPackageRepo _packageRepo;
        private readonly IMapper _mapper;

        public PackageStatusService(IPackageStatusRepo packageStatusRepo, IPackageRepo packageRepo, IMapper mapper)
        {
            _packageStatusRepo = packageStatusRepo;
            _packageRepo = packageRepo;
            _mapper = mapper;
        }

        public async Task<IAsyncResult> AddStatusAsync(int packageID, StatusName status)
        {
            var packageStatus = new PackageStatusToAddDTO(status);
            packageStatus.PackageId = packageID;

            var packageToAdd = _mapper.Map<PackageStatus>(packageStatus);

            await _packageStatusRepo.AddStatusAsync(packageToAdd);

            if(status == StatusName.Delivered)
            {
                var packageFromRepo = await _packageRepo.GetSinglePackageAsync(packageID);
                packageFromRepo.DateDelivered = DateTime.Now;
                //await _packageRepo.AddPackageAsync(packageFromRepo);
            }
            
            if(await _packageStatusRepo.SaveAllAsync())
                return Task.CompletedTask;

            throw new Exception("Error occurred");
        }

        public async Task<IAsyncResult> DeleteStatusAsync(int statusID)
        {
            var packageStatusFromRepo = await _packageStatusRepo.GetPackageStatusAsync(statusID);

            if(packageStatusFromRepo==null)
                throw new Exception("Status not found");

            _packageStatusRepo.DeleteStatus(packageStatusFromRepo);

            if(await _packageStatusRepo.SaveAllAsync())
                return Task.CompletedTask;
            
            throw new Exception("Error occurred");
        }

        public async Task<IEnumerable<PackageStatusToReturnDTO>> GetAllPackageStatusesAsync(int packageID)
        {
            var packageStatusesFromRepo = await _packageStatusRepo.GetAllPackageStatusesAsync(packageID);

            if(packageStatusesFromRepo == null)
                throw new Exception("Statuses not found");

            var packageStatusesToReturn = _mapper.Map<IEnumerable<PackageStatusToReturnDTO>>(packageStatusesFromRepo);

            return packageStatusesToReturn;
        }
    }
}