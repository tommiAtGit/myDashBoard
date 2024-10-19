namespace myTodoService.Domain
{

    public class TaskDTO
    {

        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";

        public string Reporter { get; set; } = "";
        public string Owner { get; set; } = "";

        public TodoStatus Status { get; set; }
        public DateTime DateReported { get; set; }
        public DateTime DateOpened { get; set; }
        public DateTime DataCompleted{get;set;}
        public DateTime DateClosed { get; set; }

    }

}