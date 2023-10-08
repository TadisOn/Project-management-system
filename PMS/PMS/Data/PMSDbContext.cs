using Microsoft.EntityFrameworkCore;
using PMS.Data.Entities;
using Task = PMS.Data.Entities.Task;

namespace PMS.Data
{
    public class PMSDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Worker> Workers { get; set; }

        public PMSDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgreSQL"));
        }

    }
}
