using Yungching.Common.Dto;
using Yungching.Repository.Interface;
using Yungching.Service;

namespace YungChung.Service.Test
{
    //MSTest
    [TestClass]
    public class EstateServiceTests
    {
        private IEstateRepository _repository;
        private EstateService _service;

        [TestInitialize]
        public void Setup()
        {
            _repository = new TestEstateRepository(); //假的資料庫
            _service = new EstateService(_repository);
        }

        //AAA模式 Arrange Act Assert
        [TestMethod]
        public async Task CreateEstates_ShouldReturnCreatedEstate()
        {
            // Arrange 準備階段
            var estate = new CreateEstateDto
            {
                Name = "Test Estate"
            };

            // Act 執行階段
            var result = await _service.CreateEstates(estate);

            // Assert 驗證階段-驗證結果是否符合預期
            Assert.IsNotNull(result); //確認結果不為null
            Assert.AreEqual("Test Estate", result.Name); //確認結果的Name是否為"Test Estate"
        }

        [TestMethod]
        public async Task GetUserEstates_ShouldReturnEstateDto()
        {
            // Arrange
            int page = 1;
            int userId = 1;

            // Act
            var result = await _service.GetUserEstates(page, userId);

            // Assert
            Assert.IsNotNull(result);
        }
    }

    //假的資料庫
    public class TestEstateRepository : IEstateRepository
    {
        public async Task<CreateEstateDto> CreateEstates(CreateEstateDto estate)
        {
            return estate;
        }

        public async Task<CreateEstateDto> UpdateEstates(CreateEstateDto estate)
        {
            return estate;
        }

        public async Task ChangeDataStatus(int id)
        {
            await Task.CompletedTask;
        }

        public async Task<EstateDto> GetUserEstates(int page, int userId, string searchName = null)
        {
            return new EstateDto();
        }

        public async Task DeleteData(int id)
        {
            await Task.CompletedTask;
        }
    }
}