using BankUserAccountManagmentApplicationDAL.DTOModels.Role;
using BankUserAccountManagmentApplicationDAL.DTOModels.User;
using BankUserAccountManagmentApplicationDAL.EntityModels;
using BankUserAccountManagmentApplicationDAL.Helpers;
using BankUserAccountManagmentApplicationDAL.ViewModels;
using BankUserAccountManagmentDAL.DTOModels.User;
using BankUserAccountManagmentDAL.Enums;
using BankUserAccountManagmentDAL.Extensions;
using BankUserAccountManagmentDAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BankUserAccountManagmentApplicationDAL.Repositories
{
    public class UserRepository : BaseRepository
    {
        public UserRepository(int userID) : base(userID)
        {
        }

        public UserRepository(BaseRepository baseRepository) : base(baseRepository)
        {
        }

        public UserRepository(IBaseRepository baseRepository) : base(baseRepository.Context, baseRepository.UserID)
        {
        }

        public int CreateUser(UserProfileDTO userProfile)
        {
            User user =  new User();
            user.Email = userProfile.Email;
            user.FirstName = userProfile.FirstName;
            user.LastName = userProfile.LastName;
            user.PasswordHash = HashHelper.GetHashedString(userProfile.Password);
            user.Address = userProfile.Address;
            user.PassportNumber = userProfile.PassportNumber;
            user.PhoneNumber = userProfile.PhoneNumber;
            user.UserRoles = new List<UserRole>();

            if(userProfile.IsAdmin)
            {
                user.UserRoles.Add(new UserRole { RoleID = (int)RolesEnum.Admin, User = user });
            }

            user.UserRoles.Add(new UserRole { RoleID = (int)RolesEnum.User, User = user });
            user.IsActive = true;
            user.CreatedDate = DateTime.UtcNow;
            user.CreatedByUserID = UserID;
            user.ModifiedDate = DateTime.UtcNow;
            user.ModifiedByUserID = UserID;

            BankAccountRepository bankAccountRepository = new BankAccountRepository(this);
            BankAccount initialBankAccount = bankAccountRepository.CreateInitialBankAccountForUser();

            user.BankAccount = initialBankAccount;
            
            this.Context.Users.Add(user);
            this.Context.SaveChanges();

            return user.ID;
        }

        public UserProfileDTO EditUser(UserProfileDTO userProfile)
        {
            User user = this.Context.Users.Where(u => u.ID == userProfile.ID).Include(u => u.UserRoles).SingleOrDefault();

            if(user == null)
            {
                throw new Exception("User not found!");
            }

            MapUserProfileDToToEntity(userProfile, user);

            user.ModifiedDate = DateTime.UtcNow;
            user.ModifiedByUserID = UserID;

            
            this.Context.SaveChanges();
          

            return GetUserByID(user.ID);
        }

        public IEnumerable<UserProfileShortAmountDTO> GetAllUsersInSystem()
        {
           IEnumerable<UserProfileShortAmountDTO> allActiveUsers  =  this.Context.Users.Where(u => u.IsActive)
                                                                                       .SelectUserProfileShortAmountDTO().ToList();

            return allActiveUsers;
        }

        private void MapUserProfileDToToEntity(UserProfileDTO userProfile, User user)
        {
            user.Email = userProfile.Email;
            user.FirstName = userProfile.FirstName;
            user.LastName = userProfile.LastName;

            if (!string.IsNullOrEmpty(userProfile.Password))
            {
                user.PasswordHash = HashHelper.GetHashedString(userProfile.Password);
            }

            user.Address = userProfile.Address;
            user.PassportNumber = userProfile.PassportNumber;
            user.PhoneNumber = userProfile.PhoneNumber;
            var userRoles = user.UserRoles.ToList();
                
            if (userProfile.IsAdmin &&  !userRoles.Any( ur => ur.RoleID == (int)RolesEnum.Admin))
            {
                user.UserRoles.Add(new UserRole { RoleID = (int)RolesEnum.Admin, User = user });
            }
            else if(!userProfile.IsAdmin && userRoles.Any(ur => ur.RoleID == (int)RolesEnum.Admin)) {
                UserRole adminRole = userRoles.Where(ur => ur.RoleID == (int)RolesEnum.Admin).Single();

                this.Context.UserRoles.Remove(adminRole);
            }
        }

        public UserProfileDTO GetUserByNameAndPassword(string email, string password)
        {
            string passwordHash = HashHelper.GetHashedString(password);

            UserProfileDTO user = this.Context.Users.Where(u => u.Email == email && u.PasswordHash == passwordHash && u.IsActive)
                                                    .SelectUserProfileDTO().SingleOrDefault();

            return user;
        }

        public UserProfileDTO GetUserByID(int userId)
        {
            UserProfileDTO user = this.Context.Users.Where(u => u.ID == userId).SelectUserProfileDTO().SingleOrDefault();

            return user;
        }
    }
}
