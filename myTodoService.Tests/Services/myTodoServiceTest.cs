using myTodoService.Services;
using Xunit;


namespace myTodoService.Tests.Services
{
    public class myTodoServiceTest{
        private readonly MyHealthService myHealth;

        public myTodoServiceTest(){
            myHealth = new MyHealthService();
        }

        [Fact]
        public void GetMessageTest(){
            //Act
            var result = myHealth.getMessage();

            //Assert
            Assert.Equal( "I'm alive in the service of MyHealthService", result);

        }

        [Fact]
        public void GetMessage_WithNullInput_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => myHealth.getMessage());
            Assert.Equal("message", exception.ParamName);
        }
    }
}
