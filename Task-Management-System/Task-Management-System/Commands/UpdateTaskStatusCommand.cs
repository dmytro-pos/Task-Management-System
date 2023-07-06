using Task_Management_System.Handlers.Interfaces;
using Task_Management_System.Strategies.Abstract;
using TaskManagementSystem;

namespace Task_Management_System.Commands
{
    public class UpdateTaskStatusCommand : ITaskCommand
    {
        private readonly string CommandName = "Update task status";
        private readonly IServiceBusHandler _serviceBusHandler;

        public UpdateTaskStatusCommand(IServiceBusHandler serviceBusHandler)
        {
            _serviceBusHandler = serviceBusHandler;
        }

        public bool IsApplicable(string commandName)
        {
            return commandName == CommandName;
        }

        public Task ExecuteAsync()
        {
            ConsoleManager consoleManager = ConsoleManager.Instance;

            var taskUpdateDetails = consoleManager.AskForTaskUpdateDetails();

            _serviceBusHandler.PublishTaskUpdateIntoTheQueue(taskUpdateDetails);

            return Task.CompletedTask;
        }
    }
}
