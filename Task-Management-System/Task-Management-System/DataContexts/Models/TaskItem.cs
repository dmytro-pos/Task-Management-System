
namespace Task_Management_System.DataContexts.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TaskStatusEnum Status { get; set; }
        public string? AssignedTo { get; set; }
    }
}
