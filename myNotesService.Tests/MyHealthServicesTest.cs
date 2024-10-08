using myNotesService.Services;

using Xunit;

namespace myNotesService.Tests.Services
{
    public class MyHealthServicesTest
    {
        private HealthService _myHealthService;

        public  MyHealthServicesTest()
        {
            _myHealthService = new HealthService();
        }

        [Fact]
        public void GetMessage_ShouldReturnCorrectMessage()
        {
            // Act
            var result = _myHealthService.getMessage();
            // Assert
            Assert.Equal("I'm alive in the service of myNotesService", result);
        }
    }
}