using AutoMapper;
using EWallet.BusinessLogic.Implementation.Users.ViewModel;
using EWallet.DataAccess;
using EWallet.Entities.Entities;
using EWallet.Common.Extensions;
using EWallet.BusinessLogic.Implementation;
using EWallet.DataAccess.EntityFramework;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Filters;
using FluentValidation.Results;
using EWallet.BusinessLogic.Implementation.Spendings.ViewModel;
using AutoMapper.QueryableExtensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EWallet.BusinessLogic.Implementation.Users
{
    public class UserService : BaseService
    {

        public UserService(ServiceDependencies dependencies)
            : base(dependencies)
        {
        }

        public async Task<CurrentUserViewModel> GetUserViewModelByEmailAndPassword(string email, string password)
        {
            var passwordHash =  new PasswordHash(password).ToArray();
            var user = await UnitOfWork.Users.Get()
                .Include(u => u.UserRole)
                .FirstOrDefaultAsync(u => u.Email == email  && u.UserPassword== passwordHash       
                );

            if (user == null)
            {
                return new CurrentUserViewModel { IsAuthenticated = false };
            }


            var currentUserVM = Mapper.Map<User, CurrentUserViewModel>(user);

            return currentUserVM;
        }


        public async Task<List<SelectListItem>> InitializeRoles()
        {
            var allRoles =  await UnitOfWork.Roles.Get()
                .Select(r => new SelectListItem()
                {
                    Text=r.RoleName,
                    Value=r.Id.ToString()
                }).ToListAsync();
            return allRoles;

        }

        public async Task<RegistrationViewModel> CreateRegistrationVMAndDropdownValues()
        {

            var model = new RegistrationViewModel();
            //model.Roles= await InitializeRoles();
           
            return model;
        }

        public async Task RegisterNewUser(RegistrationViewModel model)
        {

            var user = Mapper.Map<RegistrationViewModel, User>(model);

            user.UserPassword = new PasswordHash(model.Password).ToArray();
            var username  = model.FirstName.Substring(0, 1) + model.LastName;
            var usernamesNumber = await UnitOfWork.Users.Get()
                .Where(u => u.Username == username)
                .CountAsync();

            if (usernamesNumber == 0)
            {
                user.Username = username;
            }
            else
            {
                usernamesNumber++;
                user.Username = username + usernamesNumber;
            }

            UnitOfWork.Users.Insert(user);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<List<SelectListItem>> GetUsers()
        {
            var users =  UnitOfWork.Users.Get();
            return await users
                .Select(u => new SelectListItem
                {
                    Text = $"{u.FirstName} {u.LastName}",
                    Value = u.Id.ToString()
                })
                .ToListAsync();
        }


        public async Task UpdateCurrentBalanceWithoutSave(decimal sum, int userId)
        {
            var user = await UnitOfWork.Users.Get()
                       .Where(u => u.Id == userId)
                      
                       .FirstOrDefaultAsync();
            user.CurrentBalance = user.CurrentBalance + sum;
            UnitOfWork.Users.Update(user);  
           
        }

        public async Task<decimal> GetCurrentBalance()
        {
            return await UnitOfWork.Users.Get()
                .Where(u => u.Id == CurrentUserViewModel.Id)
                .Select(u => u.CurrentBalance)
                .FirstOrDefaultAsync();

        }

        public async Task<ValidationResult> AddFriend(string username)
        {
            ValidationResult validationResults = new ValidationResult();
            var user1 = await UnitOfWork.Users.Get()
                .Where(u => u.Id == CurrentUserViewModel.Id)
                .FirstOrDefaultAsync();

            var user2= await UnitOfWork.Users.Get()
                .Where(u => u.Username== username)
                .FirstOrDefaultAsync();

            if (user2 != null)
            {

                bool isAlreadyFriend = false;      

                var friendship = await UnitOfWork.Friendships.Get()
                    .Include(f => f.User2)
                    .Include ( f=> f.User1)
                    .Where(f => (f.User1Id == CurrentUserViewModel.Id && f.User2Id == user2.Id) ||
                                (f.User2Id == CurrentUserViewModel.Id && f.User1Id == user1.Id)
                           )
                    .FirstOrDefaultAsync();


                if (friendship != null)
                {

                    validationResults.Errors.Add(new ValidationFailure
                    {
                        ErrorMessage = "You are already friends"
                    });
                    isAlreadyFriend = true;
                }


                else
                {
                    var newFriendship = new Friendship()
                    {
                        User1 = user1,
                        User2 = user2
                    };

                    UnitOfWork.Friendships.Insert(newFriendship);
                    await UnitOfWork.SaveChangesAsync();
                }
            }

            else
            {
                validationResults.Errors.Add(new ValidationFailure
                {
                    ErrorMessage = "This username doesnt exist"
                });
            }

            return validationResults;
        }

        public async Task<List<FriendViewModel>> GetFriends()
        {

            var friends = await UnitOfWork.Friendships.Get()
                .Include(f => f.User1)
                .Include(f => f.User2)
                .Where(f => f.User2Id == CurrentUserViewModel.Id || f.User1Id == CurrentUserViewModel.Id)
                .Select(f => new FriendViewModel()
                {
                    UserId = (f.User1Id == CurrentUserViewModel.Id ? f.User2.Id : f.User1.Id),
                    Username = (f.User1Id == CurrentUserViewModel.Id ? f.User2.Username : f.User1.Username)

                })
                .ToListAsync();

            return friends;
        }

     

        public async Task<List<SelectListItem>> GetFriendsAsSelectListItems()
        {
            var friends =  await UnitOfWork.Friendships.Get()
               .Include(f => f.User1)
               .Include(f => f.User2)
               .Where(f => f.User2Id == CurrentUserViewModel.Id || f.User1Id == CurrentUserViewModel.Id)
               .Select(f => new SelectListItem()
               {
                   Text = f.User1Id == CurrentUserViewModel.Id ? f.User2.Username : f.User1.Username,
                   Value = f.User1Id == CurrentUserViewModel.Id ? f.User2.Id.ToString() : f.User1.Id.ToString()
               })
               .ToListAsync();

            return friends;

        }


        public async Task<List<UserWithBirthdayViewModel>> GetUpcomingBirthdays()
        {
            var friends = UnitOfWork.Friendships.Get()
              .Include(f => f.User1)
              .Include(f => f.User2)
              .Where(f => f.User2Id == CurrentUserViewModel.Id || f.User1Id == CurrentUserViewModel.Id)
              .Select(f => f.User1Id == CurrentUserViewModel.Id ? f.User2.Id: f.User1.Id);
              
            var users = await UnitOfWork.VwUpcomingBirthdays.Get()
                .Where(b => friends.Contains(b.Id))
                .ProjectTo<UserWithBirthdayViewModel>(Mapper.ConfigurationProvider)
                .ToListAsync();
            return users;
        }


    }
}
