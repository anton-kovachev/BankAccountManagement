using BankUserAccountManagmentApplicationDAL.EntityModels;
using BankUserAccountManagmentApplicationDAL.Helpers;
using BankUserAccountManagmentDAL.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankUserAccountManagmentApplicationDAL.Data
{
    public static class DbInitializer
    {
        public static void Initialize(BankUserAccountManagementContext context)
        {
            context.Database.EnsureCreated();

            if (context.Roles.Any())
            {
                return;   // DB has been seeded
            }

            var roles = new Role[]
            {
                new Role { Name = "Admin", CreatedDate= DateTime.UtcNow},
                new Role { Name = "User", CreatedDate= DateTime.UtcNow}
            };

            foreach (var role in roles)
            {
                context.Roles.Add(role);
            }

            var bankAccountOperations = new BankAccountOperation[]
            {
                new BankAccountOperation { Name = AccountOperation.Deposit.ToString() },
                new BankAccountOperation { Name = AccountOperation.Withdraw.ToString() },
                new BankAccountOperation { Name = AccountOperation.Transfer.ToString() }
            };

            foreach(var operation in bankAccountOperations)
            {
                context.BankAccountOperations.Add(operation);
            }

            context.SaveChanges();

            var users = new User[]
            {
                 new User { FirstName ="System", LastName="System", Email="System@bank.org", PasswordHash= HashHelper.GetHashedString("222222"), Address="Alexandria 17, Washington D.C.", PhoneNumber="+700 222 33 11", PassportNumber="13243423423", IsActive = true, CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow,
                       UserRoles = new List<UserRole>() { new UserRole { RoleID = 1 }, new UserRole {  RoleID = 2 } }
                 }
                 //,
                //new User { ID = 1, FirstName ="Carson", LastName="Alexander", Email="calexander", PasswordHash= HashHelper.GetHashedString("222222"), Address="Alexandria 17, Washington D.C.", PhoneNumber="+700 222 33 11", PassportNumber="13243423423", IsActive = true, CreatedDate = DateTime.UtcNow,
                //              UserRoles = new List<UserRole>() { new UserRole { RoleID = 1 }, new UserRole {  RoleID = 2 } } },
                //new User { ID = 2, FirstName ="Meredith", LastName="Alonso", Email="malonso", PasswordHash= HashHelper.GetHashedString("333333"), Address="Alexandria 27, Washington D.C.",PhoneNumber="+700 313 22 11" , PassportNumber="15555423423", IsActive = true, CreatedDate = DateTime.UtcNow, 
                //          UserRoles = new List<UserRole>() { new UserRole { RoleID = 2 } } },
                //new User { ID = 3, FirstName ="Laura", LastName="Norman", Email="malonso", PasswordHash= HashHelper.GetHashedString("444444"), Address="Bethesda 27, Washington D.C.",PhoneNumber="+700 311 22 11" , PassportNumber="15555424423", IsActive = true, CreatedDate = DateTime.UtcNow,
                //            UserRoles = new List<UserRole>() { new UserRole { RoleID = 2 } } }
            };

            foreach (User s in users)
            {
                context.Users.Add(s);
            }

            context.SaveChanges();

            //var newUser = new User
            //{
            //    FirstName = "Carson",
            //    LastName = "Alexander",
            //    Email = "calexander",
            //    PasswordHash = HashHelper.GetHashedString("222222"),
            //    Address = "Alexandria 17, Washington D.C.",
            //    PhoneNumber = "+700 222 33 11",
            //    PassportNumber = "13243423423",
            //    IsActive = true,
            //    CreatedDate = DateTime.UtcNow,
            //    UserRoles = new List<UserRole>() { new UserRole { RoleID = 1 }, new UserRole { RoleID = 2 } },
            //    //CreatedByUser = users.First(),
            //   // ModifiedByUser = users.First()
            //};

            //context.Users.Add(newUser);
            context.SaveChanges();
            var bankAccounts = new BankAccount[]
            {
                new BankAccount { Amount = 0, UserID = 1, CreatedByUserID = 1, ModifiedByUserID = 1, IsActive = true, CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow },
                //new BankAccount { Amount = 5000, UserID = 2, CreatedByUserID = 1, ModifiedByUserID = 1, IsActive = true, CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow,
                // BankAccountAudits = new List<BankAccountAudit> { new BankAccountAudit { PreviousAmount = 0, CurrentAmount = 5000, CreatedByUserID = 2, CreatedDate = DateTime.UtcNow } } },
            };

            foreach (var bankAccount in bankAccounts)
            {
                context.BankAccounts.Add(bankAccount);
            }

            context.SaveChanges();
        }
    }
}
