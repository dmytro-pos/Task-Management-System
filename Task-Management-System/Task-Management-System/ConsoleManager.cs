using Spectre.Console;
using Task_Management_System.DataContexts.Models;

namespace TaskManagementSystem
{
    sealed class ConsoleManager
    {
        private static readonly ConsoleManager instance = new ConsoleManager();

        private ConsoleManager() { }

        public static ConsoleManager Instance
        {
            get
            {
                return instance;
            }
        }

        public string DisplayMenu(List<MenuOption> menuOptions)
        {
            Console.WriteLine("Task Management System");
            Console.WriteLine("----------------------");

            var option = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title("What action you would like to perform?")
                        .PageSize(10)
                        .AddChoices(menuOptions.Select(o => o.Name)));

            return option;
        }

        public TaskItem AskUserForTaskCreationDetails() 
        {
            var taskName = AnsiConsole.Ask<string>("How do you want to name the [green]task[/]?");
            var taskDescription = AnsiConsole.Ask<string>("Please add description to this [green]task[/]:");
            var statusOfTask = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title("What status this [green]task[/] should have?")
                        .PageSize(10)
                        .AddChoices(Enum.GetNames(typeof(TaskStatusEnum))));

            var addAssignee = AnsiConsole.Confirm("Do you want to add asignee to this [green]task[/]?");
            string assignedTo = null;
            
            if (addAssignee)
            {
                assignedTo = AnsiConsole.Ask<string>("Type the name of assignee:");
            }

            var taskItem = new TaskItem() 
            {
                Name = taskName,
                Description = taskDescription,
                Status = (TaskStatusEnum)Enum.Parse(typeof(TaskStatusEnum), statusOfTask),
                AssignedTo = assignedTo
            };

            return taskItem;
        }

        public TaskUpdateRequest AskForTaskUpdateDetails() 
        {
            var taskId = AnsiConsole.Ask<int>("Which [green]task[/] do you want to change? (specify id of the task)");
            var newStatusOfTask = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title("To what status you want to update existing [green]task[/]?")
                        .PageSize(10)
                        .AddChoices(Enum.GetNames(typeof(TaskStatusEnum))));
            var updatedBy = AnsiConsole.Ask<string>("Please type in your name: ");

            AnsiConsole.MarkupLine("Update request will be processed. In case of any invalid data - update will not be performed");

            return new TaskUpdateRequest() 
            {
                TaskId = taskId,
                NewStatus = (TaskStatusEnum)Enum.Parse(typeof(TaskStatusEnum), newStatusOfTask),
                UpdatedBy = updatedBy
            };
        }

        public void DisplayListOfTasks(List<TaskItem> taskItems) 
        {
            foreach (var item in taskItems)
            {
                Console.WriteLine("----------------------");
                AnsiConsole.MarkupLine($"[green]Task id:[/] \"{item.Id}\"");
                AnsiConsole.MarkupLine($"[green]Task name:[/] \"{item.Name}\"");
                AnsiConsole.MarkupLine($"[green]Task description:[/] \"{item.Description}\"");
                AnsiConsole.MarkupLine($"[green]Task status:[/] \"{item.Status.ToString()}\"");
                if (string.IsNullOrEmpty(item.AssignedTo))
                {
                    AnsiConsole.MarkupLine($"[green]Task has no assignee[/]");
                }
                else 
                {
                    AnsiConsole.MarkupLine($"[green]Task assignee:[/] \"{item.AssignedTo}\"");
                }
            }
        }

        public void ShowTheResultOfCommand(string commandName) 
        {
            AnsiConsole.MarkupLine($"Command \"{commandName}\" is succesfully performed");
        }
    }
}
