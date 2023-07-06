using Microsoft.EntityFrameworkCore;
using Task_Management_System.DataContexts.Models;

namespace Task_Management_System.DataContexts
{
    public class TaskManagementInMemoryDatabase : DbContext
    {
        private List<MenuOption> DefaultMenuOptions = new List<MenuOption>()
        {
            new MenuOption() { Id = 1, Name = "Add new task", Enabled = true },
            new MenuOption() { Id = 2, Name = "Update task status", Enabled = true },
            new MenuOption() { Id = 3, Name = "Show list of tasks", Enabled = true }
        };

        public DbSet<MenuOption> MenuOptions { get; private set; }
        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("TaskManagementInMemoryDatabase");

            base.OnConfiguring(optionsBuilder);
        }

        public TaskManagementInMemoryDatabase(DbContextOptions<TaskManagementInMemoryDatabase> options) : base(options)
        {
            MenuOptions?.AddRange(DefaultMenuOptions);
            SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>(c => 
            {
                c.HasKey(item => item.Id);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
