using EtheriusWebAPI.Models.Entity;
using Microsoft.EntityFrameworkCore;

#pragma warning disable

namespace EtheriusWebAPI.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        // Add DbSet here
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Master_Roles> Master_Roles { get; set; }
        public DbSet<Master_Menu_Items> Master_Menu_Items { get; set; }
        public DbSet<User_Roles> User_Roles { get; set; }
        public DbSet<User_Permissions> User_Permissions { get; set; }
        public DbSet<Customer_Categories> Customer_Categories { get; set; }
        public DbSet<Tax_Conditions> Tax_Conditions { get; set; }
        public DbSet<States> States { get; set; }
        public DbSet<Cities> Cities { get; set; }
    }
}
