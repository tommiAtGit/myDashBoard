namespace myTodoService.Services
{
    public class MyHealthService : IMyHealthService
    {
        public string getMessage()
        {
           return "I'm alive in the service of MyHealthService";
        }
    }
}