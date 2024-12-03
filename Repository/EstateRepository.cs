using Yungching.Common.Dto;
using Yungching.Repository.Interface;
using Yungching.Repository.Models;

namespace Yungching.Repository
{
    public class EstateRepository
    {
        private readonly IRepository<Estate> _estateRepository;

        public EstateRepository(IRepository<Estate> estateRepository)
        {
            _estateRepository = estateRepository;
        }

        //新增房地產
        public async Task<CreateEstateDto> CreateEstates(CreateEstateDto _estate)
        {
            Estate estate=new Estate();
            estate.Name = _estate.Name;
            estate.Price=_estate.Price;
            estate.Address=_estate.Address;
            estate.MemberShipId = 1;
            estate.CreateAt=DateTime.UtcNow;
            estate.Status = true;
            estate.Type= _estate.Type;
            estate.Range = _estate.Range;
            await _estateRepository.AddAsync(estate);

            return _estate;
        }


        //修改房地產
        public async Task<CreateEstateDto> UpdateEstates(CreateEstateDto _estate)
        {
            Estate res = await _estateRepository.FirstOrDefaultAsync(x => x.Id==_estate.Id);
            if (res == null) { throw new Exception($"Estate with id {_estate.Id} not found"); }

            res.UpdateAt = DateTime.UtcNow;
            res.Price = _estate.Price;
            res.Address = _estate.Address;
            res.Type = _estate.Type;
            res.Range = _estate.Range;
            res.Name = _estate.Name;

            await _estateRepository.UpdateAsync(res);
            return _estate;
        }

        //刪除房地產
        public async Task ChangeDataStatus(int id)
        {
            Estate res = await _estateRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (res == null) { throw new Exception($"Estate with id {id} not found"); }

            res.Status = !res.Status; // 如果res.Status為true=>false 反之
            res.UpdateAt = DateTime.UtcNow;
            await _estateRepository.UpdateAsync(res);
        }

        //查看房地產(自己)
        public async Task<List<GetEstate>> GetUserEstates(int userId)
        {
            List<Estate> res = await _estateRepository.ListAsync(x => x.MemberShipId == userId);
            return res.Select(estate => new GetEstate
            {
                Id=estate.Id,
                Name = estate.Name,
                Address = estate.Address,
                Price = estate.Price,
                Type = ConvertType(estate.Type),
                Range=estate.Range,
                Status=estate.Status
            }).ToList();
        }

        private string ConvertType(int type)
        {
            switch (type)
            {
                case 1:
                    return "公寓";
                case 2:
                    return "透天";
                default:
                    return "unkown";
            }
        }


        //查看房地產(所有)
    }
}
