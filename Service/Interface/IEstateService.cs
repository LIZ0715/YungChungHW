using Yungching.Common.Dto;

namespace Yungching.Service.Interface
{
    public interface IEstateService
    {
        Task ChangeDataStatus(int id);
        Task<CreateEstateDto> CreateEstates(CreateEstateDto _estate);
        Task DeleteData(int id);
        Task<EstateDto> GetUserEstates(int page, int userId, string? searchName = null);
        Task<CreateEstateDto> UpdateEstates(CreateEstateDto _estate);
    }
}