using Microsoft.AspNetCore.Mvc;
using Yungching.Common.Dto;
using Yungching.Rehouse.Web.Abstract;
using Yungching.Service;

namespace Yungching.Rehouse.Web.Controllers.API
{
    public class EstatesController : BaseApiController
    {
        private readonly EstateService _estateService;
        private readonly ILogger<EstatesController> _logger;

        public EstatesController(    
            ILogger<EstatesController> logger,
            EstateService estateService)
        {
            _logger = logger;
            _estateService = estateService;
        }

        //獲取所有房產資料
        // GET /api/estates
        //[HttpGet]
        //public async Task<IActionResult> GetEstates()
        //{
        //    try
        //    {
        //        return HandleResponse(estates);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //獲取自己的房產資料
        // GET /api/estates/my
        [HttpGet("my")]
        public async Task<IActionResult> GetMyEstates()
        {
            try
            {
                int userId = 1;
                List<GetEstate> estates = await _estateService.GetUserEstates(userId);
                return HandleResponse(estates, "Successfully retrieved personal estate list");
            }
            catch (Exception)
            {
                throw;
            }
        }

        //新增房產資料
        // POST /api/estates
        [HttpPost]
        public async Task<IActionResult> CreateEstate([FromBody] CreateEstateDto estate)
        {
            try
            {
                if (!ModelState.IsValid)
                    return HandleError("Data validation failed");

                CreateEstateDto res=await _estateService.CreateEstates(estate);
                return HandleResponse(estate, "Successfully created estate data");
            }
            catch (Exception)
            {
                throw;
            }
        }

        //修改房產資料
        // PUT /api/estates/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEstate(int id, [FromBody] CreateEstateDto estate)
        {
            try
            {
                if (!ModelState.IsValid)
                    return HandleError("Data validation failed");

                CreateEstateDto res = await _estateService.UpdateEstates(estate);
                return HandleResponse(estate, "Successfully updated estate data");
            }
            catch (Exception)
            {
                throw;
            }
        }

        //刪除某筆房產資料
        // DELETE /api/estates/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstate(int id)
        {
            try
            {
                await _estateService.DeleteEstate(id);
                return HandleResponse(true, "Successfully deleted estate data");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}