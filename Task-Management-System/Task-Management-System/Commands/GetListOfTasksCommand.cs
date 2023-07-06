using Microsoft.EntityFrameworkCore;
using Task_Management_System.DataContexts;
using Task_Management_System.Strategies.Abstract;
using TaskManagementSystem;

namespace Task_Management_System.Commands
{
    public class GetListOfTasksCommand : ITaskCommand
    {
        private readonly string CommandName = "Show list of tasks";
        private readonly TaskManagementInMemoryDatabase _taskManagementInMemoryDatabase;

        public GetListOfTasksCommand(TaskManagementInMemoryDatabase taskManagementInMemoryDatabase)
        {
            _taskManagementInMemoryDatabase = taskManagementInMemoryDatabase;
        }

        public bool IsApplicable(string commandName)
        {
            return commandName == CommandName;
        }

        public async Task ExecuteAsync()
        {
            var tasks = await _taskManagementInMemoryDatabase.Tasks.ToListAsync();
            ConsoleManager consoleManager = ConsoleManager.Instance;

            consoleManager.DisplayListOfTasks(tasks);
        }
    }
}