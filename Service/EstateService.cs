using Yungching.Common.Dto;
using Yungching.Repository;
using Yungching.Repository.Interface;
using Yungching.Service.Interface;

namespace Yungching.Service
{
    public class EstateService : IEstateService
    {
        private readonly IEstateRepository _estateRepository;

        public EstateService(IEstateRepository estateRepository)
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

        //update estate status
        public async Task ChangeDataStatus(int id)
        {
            await _estateRepository.ChangeDataStatus(id);
        }


        //get my estate
        public async Task<EstateDto> GetUserEstates(int page, int userId, string? searchName = null)
        {
            EstateDto estates = await _estateRepository.GetUserEstates(page, userId, searchName);
            return estates;
        }


        //delete estate data
        public async Task DeleteData(int id)
        {
            await _estateRepository.DeleteData(id);
        }




    }
}
