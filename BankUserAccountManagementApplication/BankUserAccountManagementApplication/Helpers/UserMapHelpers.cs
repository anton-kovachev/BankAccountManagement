using BankUserAccountManagementApplication.Models.UserProfileModels;
using BankUserAccountManagmentApplicationDAL.ViewModels;
using BankUserAccountManagmentDAL.DTOModels.User;
using BankUserAccountManagmentDAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankUserAccountManagementApplication.Helpers
{
    public class UserMapHelpers
    {
        public static UserDetailedViewModel MapUseProfileDetailedDTOToView(UserProfileDTO userProfileDTO)
        {
            UserDetailedViewModel userDetailedViewModel = new UserDetailedViewModel
            {
                ID = userProfileDTO.ID,
                FirstName = userProfileDTO.FirstName,
                LastName = userProfileDTO.LastName,
                Email = userProfileDTO.Email,
                Address = userProfileDTO.Address,
                PassportNumber = userProfileDTO.PassportNumber,
                PhoneNumber = userProfileDTO.PhoneNumber,
                Password = string.Empty,
                ConfirmPassword = string.Empty,
                IsAdmin = userProfileDTO.UserRoles.Any(ur => ur.RoleID == (int)RolesEnum.Admin),
                BankAccountID = userProfileDTO.BankAccountID
            };

            return userDetailedViewModel;
        }

        public  static UserEditDetailedViewModel MapUseProfileEditDetailedDTOToView(UserProfileDTO userProfileDTO)
        {
            UserEditDetailedViewModel userDetailedViewModel = new UserEditDetailedViewModel
            {
                ID = userProfileDTO.ID,
                FirstName = userProfileDTO.FirstName,
                LastName = userProfileDTO.LastName,
                Email = userProfileDTO.Email,
                Address = userProfileDTO.Address,
                PassportNumber = userProfileDTO.PassportNumber,
                PhoneNumber = userProfileDTO.PhoneNumber,
                Password = string.Empty,
                ConfirmPassword = string.Empty,
                IsAdmin = userProfileDTO.UserRoles.Any(ur => ur.RoleID == (int)RolesEnum.Admin),
                BankAccountID = userProfileDTO.BankAccountID,
                CreatedByUser = userProfileDTO.CreatedBy,
                ModifiedBy = userProfileDTO.ModifiedBy,
                CreatedDate = userProfileDTO.CreatedDate,
                ModifiedDate = userProfileDTO.ModifiedDate
            };

            return userDetailedViewModel;
        } 
        

        public static UserProfileDTO MapUseProfileDetailedViewToDTO(UserDetailedViewModel userProfileDetailViewModel)
        {
            UserProfileDTO userProfileDTO = new UserProfileDTO
            {
                FirstName = userProfileDetailViewModel.FirstName,
                LastName = userProfileDetailViewModel.LastName,
                Email = userProfileDetailViewModel.Email,
                Password = userProfileDetailViewModel.Password,
                Address = userProfileDetailViewModel.Address,
                PassportNumber = userProfileDetailViewModel.PassportNumber,
                PhoneNumber = userProfileDetailViewModel.PhoneNumber,
                IsActive = true,
                IsAdmin = userProfileDetailViewModel.IsAdmin,
                BankAccountID = userProfileDetailViewModel.BankAccountID
            };

            return userProfileDTO;
        }

        public static UserProfileDTO MapUseProfileDetailedViewToDTO(UserEditDetailedViewModel userProfileDetailViewModel)
        {
            UserProfileDTO userProfileDTO = new UserProfileDTO
            {
                ID = userProfileDetailViewModel.ID,
                FirstName = userProfileDetailViewModel.FirstName,
                LastName = userProfileDetailViewModel.LastName,
                Email = userProfileDetailViewModel.Email,
                Password = userProfileDetailViewModel.Password,
                Address = userProfileDetailViewModel.Address,
                PassportNumber = userProfileDetailViewModel.PassportNumber,
                PhoneNumber = userProfileDetailViewModel.PhoneNumber,
                IsActive = true,
                IsAdmin = userProfileDetailViewModel.IsAdmin,
                BankAccountID = userProfileDetailViewModel.BankAccountID
            };

            return userProfileDTO;
        }

        public static UserProfileShortAmountViewModel MapUserPorilfeShortAccountAmountDTOToView(UserProfileShortAmountDTO userProfileShorttAmountDTO)
        {
            UserProfileShortAmountViewModel userProfileShortAmountViewModel = new UserProfileShortAmountViewModel
            {
                ID = userProfileShorttAmountDTO.ID,
                Email = userProfileShorttAmountDTO.Email,
                FirstName = userProfileShorttAmountDTO.FirstName,
                LastName = userProfileShorttAmountDTO.LastName,
                IsAdmin = userProfileShorttAmountDTO.IsAdmin,
                CreatedByUser = userProfileShorttAmountDTO.CreatedByUser,
                BankAccountAmount = userProfileShorttAmountDTO.BankAccountAmount,
                BankAccountID = userProfileShorttAmountDTO.BankAccountID
            };

            return userProfileShortAmountViewModel;
        }
    }
}
