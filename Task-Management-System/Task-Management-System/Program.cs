using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Task_Management_System.Commands;
using Task_Management_System.DataContexts;
using Task_Management_System.DataContexts.Models;
using Task_Management_System.Handlers;
using Task_Management_System.Handlers.Interfaces;
using Task_Management_System.Strategies.Abstract;

namespace TaskManagementSystem
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Create the service collection
            var services = new ServiceCollection();

            // Register your DbContext and interface
            services.AddDbContext<DbContext, TaskManagementInMemoryDatabase>();

            //Register commands
            services.AddTransient<ITaskCommand, CreateNewTaskCommand>();
            services.AddTransient<ITaskCommand, UpdateTaskStatusCommand>();
            services.AddTransient<ITaskCommand, GetListOfTasksCommand>();

            // Register Handlers
            services.AddTransient<IServiceBusHandler, ServiceBusHandler>();

            // Build the service provider
            var serviceProvider = services.BuildServiceProvider();

            // Resolve the DbContext from the service provider
            var dbContext = serviceProvider.GetRequiredService<TaskManagementInMemoryDatabase>();
            var commands = serviceProvider.GetServices<ITaskCommand>().ToArray();

            bool exit = false;
            var menuOptions = await LoadMenuOptionsFromDatabase(dbContext);
            while (!exit)
            {
                ConsoleManager consoleManager = ConsoleManager.Instance;

                var userChoice = consoleManager.DisplayMenu(menuOptions);
                await ExecuteCommandIfApplicable(commands, userChoice);
            }
        }

        private static async Task<bool> ExecuteCommandIfApplicable(ITaskCommand[] taskCommands, string userChoice) 
        {
            for (int i = 0; i < taskCommands.Length; i++)
            {
                var taskCommand = taskCommands[i];
                if (taskCommand.IsApplicable(userChoice))
                {
                    await taskCommand.ExecuteAsync();
                    return true;
                }
            }

            return false;
        }

        private static async Task<List<MenuOption>> LoadMenuOptionsFromDatabase(TaskManagementInMemoryDatabase dbContext) 
        {
            return await dbContext.MenuOptions.Where(o => o.Enabled).ToListAsync();
        }
    }
}
