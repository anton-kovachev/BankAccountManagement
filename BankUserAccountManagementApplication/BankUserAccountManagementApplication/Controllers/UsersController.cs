using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankUserAccountManagmentApplicationDAL.Repositories;
using BankUserAccountManagmentApplicationDAL.DTOModels.User;
using BankUserAccountManagementApplication.Models.UserProfileModels;
using Microsoft.AspNetCore.Authorization;
using BankUserAccountManagmentApplicationDAL.ViewModels;
using BankUserAccountManagmentDAL.Enums;
using BankUserAccountManagementApplication.Helpers;
using BankUserAccountManagmentDAL.DTOModels.User;
using BankUserAccountManagementApplication.Models;
using BankUserAccountManagementApplication.Extensions;

namespace BankUserAccountManagementApplication.Controllers
{
    [Authorize]
    public class UsersController : BaseController 
    {
        private UserRepository userRepository;
        public UsersController(IBaseRepository baseRepository, IHttpContextAccessor httpContextAccessor) : base(baseRepository, httpContextAccessor)
        {
            userRepository = new UserRepository(baseRepository);
        }

        // GET: Users
        [Authorize(Policy = "Admin")]
        public ActionResult Index()
        {
            IEnumerable<UserProfileShortAmountDTO> allActiveUsersInSystem = this.userRepository.GetAllUsersInSystem();

           IEnumerable<UserProfileShortAmountViewModel> allActiveUsersInSystemView = allActiveUsersInSystem.Select(user => new UserProfileShortAmountViewModel
           {
                ID = user.ID,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsAdmin = user.IsAdmin,
                CreatedByUser = user.CreatedByUser,
                BankAccountAmount = user.BankAccountAmount,
                BankAccountID = user.BankAccountID
            }).ToList();

            BankClientsOverviewViewModel bankClientsOverview = new BankClientsOverviewViewModel();
            bankClientsOverview.BankClients = allActiveUsersInSystemView;
            bankClientsOverview.NumberOfClients = allActiveUsersInSystem.Count();
            bankClientsOverview.TotalAmountOfMoneyDeposit = allActiveUsersInSystem.Sum(user => user.BankAccountAmount);

            return View(bankClientsOverview);
        }

        // GET: Users/Details/5
        [Authorize(Policy = "ViewEditResourceAsSelfOrAdmin")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Users/Create
        [Authorize(Policy ="Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [Authorize(Policy = "Admin")]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(UserDetailedViewModel userForRegistrationViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserProfileDTO userProfile = new UserProfileDTO
                    {
                        FirstName = userForRegistrationViewModel.FirstName,
                        LastName = userForRegistrationViewModel.LastName,
                        Email = userForRegistrationViewModel.Email,
                        Password = userForRegistrationViewModel.Password,
                        Address = userForRegistrationViewModel.Address,
                        PassportNumber = userForRegistrationViewModel.PassportNumber,
                        PhoneNumber = userForRegistrationViewModel.PhoneNumber,
                        IsActive = true,
                        IsAdmin = userForRegistrationViewModel.IsAdmin 
                    };

                    userRepository.CreateUser(userProfile);
                    return RedirectToAction("Index", "Users");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error!Sorry, something went wrong! Please retry the operation!");
                return View(userForRegistrationViewModel);
            }
            finally
            {
            }

            return View(userForRegistrationViewModel);
        }

        // GET: Users/Edit/5
        [Authorize(Policy = "ViewEditResourceAsSelforAdmin")]
        public ActionResult Edit(int id)
        {
            UserProfileDTO userProfileDTO = this.userRepository.GetUserByID(id);
            UserEditDetailedViewModel userDetailedViewModel = UserMapHelpers.MapUseProfileEditDetailedDTOToView(userProfileDTO); 

            return View(userDetailedViewModel);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [Authorize(Policy = "ViewEditResourceAsSelfOrAdmin")]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserEditDetailedViewModel userForEditViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserProfileDTO userProfile = UserMapHelpers.MapUseProfileDetailedViewToDTO(userForEditViewModel);
                    UserProfileDTO editedUserProfileDTO = userRepository.EditUser(userProfile);
                    UserEditDetailedViewModel editedUserDetailedViewModel = UserMapHelpers.MapUseProfileEditDetailedDTOToView(editedUserProfileDTO);

                    if (User.IsUserAdmin())
                    {
                        return RedirectToAction("Index", "Users");
                    }

                    //return RedirectToAction("Edit", "Users", new {  @id = id });
                    return RedirectToAction("Index", "Home");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error!Sorry, something went wrong! Please retry the operation!");
                return View(userForEditViewModel);
            }
            finally
            {
            }

            return View(userForEditViewModel);
        } 
    }
}