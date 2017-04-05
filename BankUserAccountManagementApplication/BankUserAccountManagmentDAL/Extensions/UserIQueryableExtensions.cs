using BankUserAccountManagmentApplicationDAL.DTOModels.Role;
using BankUserAccountManagmentApplicationDAL.EntityModels;
using BankUserAccountManagmentApplicationDAL.ViewModels;
using BankUserAccountManagmentDAL.DTOModels.User;
using BankUserAccountManagmentDAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankUserAccountManagmentDAL.Extensions
{
    public static class UserIQueryableExtensions
    {
        public static IQueryable<UserProfileDTO> SelectUserProfileDTO(this IQueryable<User> Users)
        {
            IQueryable<UserProfileDTO> usersSelect = Users.Select(u => new UserProfileDTO
            {
                ID = u.ID,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                UserName = u.Email,
                Address = u.Address,
                IsActive = u.IsActive,
                PassportNumber = u.PassportNumber,
                PhoneNumber = u.PhoneNumber,
                UserRoles = u.UserRoles.Select(ur => new UserRoleDTO { RoleID = ur.RoleID, RoleName = ur.Role.Name, UserID = ur.UserID }).ToList(),
                BankAccountID = u.BankAccount.ID,
                CreatedBy = u.CreatedByUser.Email,
                CreatedDate = u.CreatedDate,
                ModifiedBy  = u.ModifiedByUser.Email,
                ModifiedDate = u.ModifiedDate
            });

            return usersSelect;
        }

        public static IQueryable<UserProfileShortAmountDTO> SelectUserProfileShortAmountDTO(this IQueryable<User> Users)
        {
            IQueryable<UserProfileShortAmountDTO> usersSelect = Users.Select(u => new UserProfileShortAmountDTO
            {
                ID = u.ID,
                Email = u.Email,
                UserName = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                IsAdmin = u.UserRoles.Any(ur => ur.Role.Name == RolesEnum.Admin.ToString()),
                CreatedByUser = u.CreatedByUser.Email,
                BankAccountAmount = u.BankAccount.Amount,
                BankAccountID = u.BankAccount.ID
            });

            return usersSelect;
        }
    }
}
