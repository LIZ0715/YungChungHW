using Yungching.Common.Dto;

namespace Yungching.Repository.Interface
{
    public interface IEstateRepository
    {
        Task ChangeDataStatus(int id);
        Task<CreateEstateDto> CreateEstates(CreateEstateDto _estate);
        Task DeleteData(int id);
        Task<EstateDto> GetUserEstates(int page, int userId, string? searchName = null);
        Task<CreateEstateDto> UpdateEstates(CreateEstateDto _estate);
    }
}