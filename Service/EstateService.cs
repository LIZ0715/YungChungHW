using Yungching.Common.Dto;
using Yungching.Repository;
using Yungching.Repository.Models;

namespace Yungching.Service
{
    public class EstateService
    {
        private readonly EstateRepository _estateRepository;

        public EstateService(EstateRepository estateRepository)
        {
            _estateRepository = estateRepository;
        }


        //create estate
        public async Task<CreateEstateDto> CreateEstates(CreateEstateDto _estate)
        {
            CreateEstateDto estate = await _estateRepository.CreateEstates(_estate);
            return estate;
        }

        //update estate
        public async Task<CreateEstateDto> UpdateEstates(CreateEstateDto _estate)
        {
            CreateEstateDto estate = await _estateRepository.UpdateEstates(_estate);
            return estate;
        }

        //delete estate
        public async Task DeleteEstate(int id)
        {
            await _estateRepository.DeleteEstates(id);
        }




        //get my estate
        public async Task<List<GetEstate>> GetUserEstates(int UserId)
        {
            List<GetEstate> estates = await _estateRepository.GetUserEstates(UserId);
            return estates;
        }


        //get all estate




    }
}
