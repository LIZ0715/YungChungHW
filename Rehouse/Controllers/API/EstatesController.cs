using Microsoft.AspNetCore.Mvc;
using Yungching.Common.Dto;
using Yungching.Rehouse.Web.Abstract;
using Yungching.Service.Interface;

namespace Yungching.Rehouse.Web.Controllers.API
{
    public class EstatesController : BaseApiController
    {
        private readonly IEstateService _estateService;
        private readonly ILogger<EstatesController> _logger;

        public EstatesController(ILogger<EstatesController> logger,IEstateService estateService)
        {
            _logger = logger;
            _estateService = estateService;
        }

        //獲取自己的房產資料
        // GET /api/estates/my
        [HttpGet("my")]
        public async Task<IActionResult> GetMyEstates([FromQuery]int page, [FromQuery] string? searchName=null)
        {
            try
            {
                int userId = 1;
                EstateDto estates = await _estateService.GetUserEstates(page,userId,searchName);
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

        //修改房產status
        // update /api/estates/{id}
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ChangeDataStatus(int id)
        {
            try
            {
                await _estateService.ChangeDataStatus(id);
                return HandleResponse(true, "Successfully updated estate data");
            }
            catch (Exception)
            {
                throw;
            }
        }


        //刪除房產資料
        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> DeleteData(int id)
        {
            try
            {
                await _estateService.DeleteData(id);
                return HandleResponse(true,"Successfully deleted estate data");
            }
            catch (Exception)
            {
                throw;
            }
        }
      

    }
}