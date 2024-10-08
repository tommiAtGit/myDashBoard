

namespace myNotesService.Services
{
    
    public class HealthService : IHealthService
    {
        public string getMessage()
        {
            return "I'm alive in the service of myNotesService";
        }
    }
}