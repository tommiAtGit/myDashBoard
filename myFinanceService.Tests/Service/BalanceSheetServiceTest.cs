using AutoMapper;
using Moq;
using myFinanceService.Domain;
using myFinanceService.Model;
using myFinanceService.Repository;
using myFinanceService.Services;
using Xunit;

namespace myFinanceService.Tests.Service
{
    public class BalanceSheetServiceTest
    {
        private readonly Mock<IMockBalanceSheetRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BalanceSheetService _service;

        public BalanceSheetServiceTest()
        {
            _mockRepo = new Mock<IMockBalanceSheetRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new BalanceSheetService(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public void GetAllBalanceSheets_ReturnsList()
        {
            // Arrange
            var dtos = new List<BalanceSheetDTO> { new BalanceSheetDTO { Id = Guid.NewGuid() } };
            var models = new List<BalanceSheet> { new BalanceSheet { Id = dtos[0].Id } };
            _mockRepo.Setup(repo => repo.GetAllBalanceSheets()).Returns(dtos);
            _mockMapper.Setup(m => m.Map<IEnumerable<BalanceSheet>>(dtos)).Returns(models);

            // Act
            var result = _service.GetAllBalanceSheets();

            // Assert
            Assert.Single(result);
            Assert.Equal(models[0].Id, result.First().Id);
        }

        [Fact]
        public void GetBalanceSheetById_ExistingId_ReturnsSheet()
        {
            // Arrange
            var id = Guid.NewGuid();
            var dto = new BalanceSheetDTO { Id = id };
            var model = new BalanceSheet { Id = id };
            _mockRepo.Setup(repo => repo.GetBalanceSheetById(id)).Returns(dto);
            _mockMapper.Setup(m => m.Map<BalanceSheet>(dto)).Returns(model);

            // Act
            var result = _service.GetBalanceSheetById(id);

            // Assert
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public void AddBalanceSheet_ValidSheet_ReturnsAddedSheet()
        {
            // Arrange
            var model = new BalanceSheet { BalanceSheetItemValue = 100 };
            var dto = new BalanceSheetDTO { BalanceSheetItemValue = 100 };
            var resultDto = new BalanceSheetDTO { Id = Guid.NewGuid(), BalanceSheetItemValue = 100 };
            var resultModel = new BalanceSheet { Id = resultDto.Id, BalanceSheetItemValue = 100 };

            _mockMapper.Setup(m => m.Map<BalanceSheetDTO>(model)).Returns(dto);
            _mockRepo.Setup(repo => repo.AddBalanceSheet(dto)).Returns(resultDto);
            _mockMapper.Setup(m => m.Map<BalanceSheet>(resultDto)).Returns(resultModel);

            // Act
            var result = _service.AddBalanceSheet(model);

            // Assert
            Assert.Equal(resultModel.Id, result.Id);
        }
    }
}
