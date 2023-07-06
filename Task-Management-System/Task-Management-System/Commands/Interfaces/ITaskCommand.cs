
namespace Task_Management_System.Strategies.Abstract
{
    public interface ITaskCommand
    {
        public bool IsApplicable(string commandName);
        public Task ExecuteAsync();
    }
}
