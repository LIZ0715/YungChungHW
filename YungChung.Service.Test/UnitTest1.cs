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
            _repository = new TestEstateRepository(); //������Ʈw
            _service = new EstateService(_repository);
        }

        //AAA�Ҧ� Arrange Act Assert
        [TestMethod]
        public async Task CreateEstates_ShouldReturnCreatedEstate()
        {
            // Arrange �ǳƶ��q
            var estate = new CreateEstateDto
            {
                Name = "Test Estate"
            };

            // Act ���涥�q
            var result = await _service.CreateEstates(estate);

            // Assert ���Ҷ��q-���ҵ��G�O�_�ŦX�w��
            Assert.IsNotNull(result); //�T�{���G����null
            Assert.AreEqual("Test Estate", result.Name); //�T�{���G��Name�O�_��"Test Estate"
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

    //������Ʈw
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