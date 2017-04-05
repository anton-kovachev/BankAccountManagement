using BankUserAccountManagmentApplicationDAL.EntityModels;
using BankUserAccountManagmentDAL.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;

namespace BankUserAccountManagmentApplicationDAL.Data
{
    public class BankUserAccountManagementContext : DbContext
    {
        public BankUserAccountManagementContext() : base()
        {
        }
        public BankUserAccountManagementContext(DbContextOptions<BankUserAccountManagementContext> options) : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<BankAccountAudit> BankAccountAudits { get; set; }
        public DbSet<BankAccountOperation> BankAccountOperations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().ToTable("Role");

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<User>().HasAlternateKey(u => u.PassportNumber).HasName("AlternateKey_PassportNumber");
            modelBuilder.Entity<User>().HasIndex(u => u.Email).HasName("Unique_Email");
            modelBuilder.Entity<User>().HasOne(u => u.BankAccount).WithOne( ba => ba.User).HasForeignKey<BankAccount>( bu => bu.UserID );
         

            modelBuilder.Entity<UserRole>().ToTable("UserRole");
            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserID, ur.RoleID });

            modelBuilder.Entity<BankAccount>().ToTable("BankAccount");
            modelBuilder.Entity<BankAccount>().HasAlternateKey(ba => ba.UserID).HasName("AlternateKey_UserID");

            modelBuilder.Entity<BankAccountAudit>().ToTable("BankAccountAudit");
            modelBuilder.Entity<BankAccountOperation>().ToTable("BankAccountOperation");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=BankAccountManagementDB;Trusted_Connection=True;");
        }
    }
}
