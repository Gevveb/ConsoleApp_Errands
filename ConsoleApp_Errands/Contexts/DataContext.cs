
using ConsoleApp_Errands.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp_Errands.Contexts
{
    internal class DataContext : DbContext
    {
        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\georg\OneDrive\Skrivbord\datalagring\ConsoleApp_Errands\ConsoleApp_Errands\Contexts\sql_db.mdf;Integrated Security=True;Connect Timeout=30";
        public DataContext()
        {

        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AddressEntity> Addresses { get; set; } = null!;
        public DbSet<CustomerEntity> Customers { get; set; } = null!;
        public DbSet<AdminsEntity> Admins { get; set; } = null!;
        public DbSet<ErrandsEntity> Errands { get; set; } = null!;
    }
}
