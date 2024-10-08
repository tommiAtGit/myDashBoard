using myTodoService.Services;
using Xunit;

namespace myTodoService.Tests.Services
{
    public class MyHealthServicesTest
    {
        private MyHealthService _myHealthService;

        public  MyHealthServicesTest()
        {
            _myHealthService = new MyHealthService();
        }

        [Fact]
        public void GetMessage_ShouldReturnCorrectMessage()
        {
            // Act
            var result = _myHealthService.getMessage();

            // Assert
            Assert.Equal("I'm alive in the service of MyHealthService", result);
        }
    }
}