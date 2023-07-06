using Task_Management_System.DataContexts;
using Task_Management_System.Strategies.Abstract;
using TaskManagementSystem;

namespace Task_Management_System.Commands
{
    public class CreateNewTaskCommand : ITaskCommand
    {
        private readonly string CommandName = "Add new task";
        private readonly TaskManagementInMemoryDatabase _taskManagementInMemoryDatabase;

        public CreateNewTaskCommand(TaskManagementInMemoryDatabase taskManagementInMemoryDatabase)
        {
            _taskManagementInMemoryDatabase = taskManagementInMemoryDatabase;
        }

        public bool IsApplicable(string commandName)
        {
            return commandName == CommandName;
        }

        public async Task ExecuteAsync()
        {
            ConsoleManager consoleManager = ConsoleManager.Instance;
            var taskItem = consoleManager.AskUserForTaskCreationDetails();

            await _taskManagementInMemoryDatabase.Tasks.AddAsync(taskItem);
            await _taskManagementInMemoryDatabase.SaveChangesAsync();

            consoleManager.ShowTheResultOfCommand(CommandName);
        }
    }
}
