using AutoMapper.QueryableExtensions;
using EWallet.BusinessLogic.Implementation.PiggyBanks.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using EWallet.BusinessLogic.Implementation.PiggyBanks.Validations;
using EWallet.Entities.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using EWallet.BusinessLogic.Implementation.PiggyBanks.Mappings;
using EWallet.BusinessLogic.Implementation.Spendings;
using EWallet.DataAccess.Enums;
using EWallet.BusinessLogic.Implementation.Transactions;
using EWallet.Common.Enums;
using EWallet.BusinessLogic.Implementation.Users.ViewModel;
using EWallet.BusinessLogic.Implementation.Users;
using AutoMapper.Configuration.Annotations;

namespace EWallet.BusinessLogic.Implementation.PiggyBanks
{
    public class PiggyBankService : BaseService
    {
        private readonly SpendingService _spendingService;
        private readonly TransactionService _transactionService;
        private readonly UserService _userService;

        public PiggyBankService(ServiceDependencies dependencies, SpendingService spendingService, TransactionService transactionService, UserService userService)
            : base(dependencies)
        {
            _spendingService = spendingService;
            _transactionService = transactionService;
            _userService = userService;
        }



        public async Task CreatePiggyBank(CreatePiggyBankViewModel model)
        {
            var newPiggyBank = Mapper.Map<CreatePiggyBankViewModel, PiggyBank>(model);
            newPiggyBank.CreatorId = CurrentUserViewModel.Id;
            var piggyBanksIncomes = model.PiggyBanksIncomes.AsQueryable().ProjectTo<PiggyBanksIncome>(Mapper.ConfigurationProvider).ToList();
            newPiggyBank.PiggyBanksIncomes = piggyBanksIncomes;         

            var newPiggyBankFriends = new PiggyBanksFriend();
            newPiggyBankFriends.UserId = CurrentUserViewModel.Id;
            newPiggyBankFriends.PiggyBank = newPiggyBank;


            UnitOfWork.PiggyBanks.Insert(newPiggyBank);
            UnitOfWork.PiggyBanksFriends.Insert(newPiggyBankFriends);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<List<PiggyBankViewModel>> GetPersonalPiggyBanks()
        {

            var piggyBanks = await UnitOfWork.PiggyBanks.Get()
                .Where(pb =>  pb.CreatorId == CurrentUserViewModel.Id && pb.IsDeleted == false)
                .ProjectTo<PiggyBankViewModel>(Mapper.ConfigurationProvider)
                .ToListAsync();

            return piggyBanks;
        }


        public async Task<List<PiggyBankViewModel>> GetCollaborativePiggyBanks()
        {
            var piggyBanks =  await UnitOfWork.PiggyBanksFriends.Get()
                                   .Include(pbf => pbf.PiggyBank)
                                   .Where(pbf => pbf.UserId == CurrentUserViewModel.Id && pbf.PiggyBank.CreatorId!=CurrentUserViewModel.Id &&
                                                 pbf.PiggyBank.IsDeleted==false)
                                   .Select(pbf => pbf.PiggyBank)
                                   .ProjectTo<PiggyBankViewModel>(Mapper.ConfigurationProvider)
                                   .ToListAsync()
                                   ;

            return piggyBanks;
        }


        public async Task DeleteCollaboration(int piggyBankId)
        {
            var collaboration = await UnitOfWork.PiggyBanksFriends.Get()
                .Where(pbf => pbf.PiggyBankId == piggyBankId && pbf.UserId==CurrentUserViewModel.Id)
                .FirstOrDefaultAsync();

            UnitOfWork.PiggyBanksFriends.Delete(collaboration);
            await UnitOfWork.SaveChangesAsync();
        }



        public async Task DeleteCollaborationPost(PiggyBankCollaborationViewModel model)
        {
            var collaboration = Mapper.Map<PiggyBankCollaborationViewModel, PiggyBanksFriend>(model);

            var collabToDelete = await UnitOfWork.PiggyBanksFriends.Get()
                .Where(p => p.UserId == collaboration.UserId && p.PiggyBankId == collaboration.PiggyBankId)
                .FirstOrDefaultAsync();

            UnitOfWork.PiggyBanksFriends.Delete(collabToDelete);
            await UnitOfWork.SaveChangesAsync();
        }


        public async Task DeletePiggyBank(int id)
        {
            var piggyBank = await UnitOfWork.PiggyBanks.Get()
               .Where(i => i.Id == id)
               .FirstOrDefaultAsync();

            var piggyBanksIncomes = await UnitOfWork.PiggyBanksIncomes.Get()
               .Where(i => i.PiggyBankId == id)
               .ToListAsync();

            var piggyBanksFriends = await UnitOfWork.PiggyBanksFriends.Get()
               .Where(i => i.PiggyBankId == id)
               .ToListAsync();

            foreach (var pbi in piggyBanksIncomes)
            {
                UnitOfWork.PiggyBanksIncomes.Delete(pbi);
            }

            foreach (var pbf in piggyBanksFriends)
            {
                UnitOfWork.PiggyBanksFriends.Delete(pbf);
            }

            piggyBank.IsDeleted = true;

            UnitOfWork.PiggyBanks.Update(piggyBank);
            await UnitOfWork.SaveChangesAsync();
        }



        public async Task BreakPiggyBank(int id)
        {
            var piggyBank = await UnitOfWork.PiggyBanks.Get()
               .Where(i => i.Id == id)
               .FirstOrDefaultAsync();

            piggyBank.IsDeleted = true;

            UnitOfWork.PiggyBanks.Update(piggyBank);

            var newSpending = Mapper.Map<PiggyBank, Spending>(piggyBank);
            newSpending.SpendingCategoryId = (int)SpendingCategories.PiggyBank;
            newSpending.UserId = CurrentUserViewModel.Id;
            UnitOfWork.Spendings.Insert(newSpending);

            var transaction = Mapper.Map<Spending, Transaction>(newSpending);
            transaction.PiggyBankId = piggyBank.Id;
            transaction.UserId = CurrentUserViewModel.Id;
            transaction.Spending = newSpending;
            _transactionService.CreateTransactionWithoutSave(transaction);


            await UnitOfWork.SaveChangesAsync();
        }




        public async Task<PiggyBankViewModel> GetPiggyBankToEdit(int id)
        {
            var piggyBank = await UnitOfWork.PiggyBanks.Get()
                .Where(i => i.Id == id)
                .ProjectTo<PiggyBankViewModel>(Mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            var piggyBankIncomes = await UnitOfWork.PiggyBanksIncomes.Get()
                .Where(pbi => pbi.PiggyBankId == piggyBank.Id && pbi.Income.UserId == CurrentUserViewModel.Id)
                .ProjectTo<PiggyBanksIncomeViewModel>(Mapper.ConfigurationProvider)
                .ToListAsync();

            piggyBank.PiggyBanksIncomes = piggyBankIncomes;
            return piggyBank;

        }



        public async Task<EditCollaborativePiggyBankViewModel> GetCollaborativePiggyBankToEdit(int id)
        {
            var piggyBank = await UnitOfWork.PiggyBanks.Get()
                .Where(i => i.Id == id)
                .ProjectTo<EditCollaborativePiggyBankViewModel>(Mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            var piggyBankIncomes = await UnitOfWork.PiggyBanksIncomes.Get()
                .Where(pbi => pbi.PiggyBankId == piggyBank.Id && pbi.Income.UserId==CurrentUserViewModel.Id)
                .ProjectTo<PiggyBanksIncomeViewModel>(Mapper.ConfigurationProvider)
                .ToListAsync();

            piggyBank.CollaborativePiggyBankIncomes = piggyBankIncomes;
            return piggyBank;

        }


        public async Task<PiggyBankCollaborationViewModel> GetPiggyBankCollaboration(int piggyBankId)
        {
            var collaboration = await UnitOfWork.PiggyBanksFriends.Get()
                .Where(pbf => pbf.PiggyBankId == piggyBankId && pbf.UserId == CurrentUserViewModel.Id)
                .FirstOrDefaultAsync();

            var mappedCollab = Mapper.Map<PiggyBanksFriend, PiggyBankCollaborationViewModel>(collaboration);
            return mappedCollab;
        }



        public async Task EditPiggyBank(EditPiggyBankViewModel model)
        {
            var piggyBank = Mapper.Map<EditPiggyBankViewModel, PiggyBank>(model);
            piggyBank.CreatorId = CurrentUserViewModel.Id;
            var newPiggyBanksIncomes = model.PiggyBanksIncomes.AsQueryable().ProjectTo<PiggyBanksIncome>(Mapper.ConfigurationProvider).ToList();

                var oldPiggyBankIncomes = await UnitOfWork.PiggyBanksIncomes.Get()
               .Where(pb => pb.PiggyBankId == piggyBank.Id && pb.Income.UserId == CurrentUserViewModel.Id)
               .ToListAsync();

                var piggyBankIncomesToDelete = oldPiggyBankIncomes.Except(newPiggyBanksIncomes);
                var piggyBankIncomesToInsert = newPiggyBanksIncomes.Except(oldPiggyBankIncomes);

                foreach (var pbi in piggyBankIncomesToDelete)
                {
                    UnitOfWork.PiggyBanksIncomes.Delete(pbi);

                }


                foreach (var pbi in piggyBankIncomesToInsert)
                {
                    UnitOfWork.PiggyBanksIncomes.Insert(pbi);
                }

                piggyBank.PiggyBanksIncomes = newPiggyBanksIncomes;


            if (piggyBank.TargetSum > piggyBank.CurrentBalance)
            {
                piggyBank.PiggyBankStatus = (int)PiggyBankStatusTypes.Active;
            }

            else
            {
                piggyBank.PiggyBankStatus = (int)PiggyBankStatusTypes.Full;
            }

            UnitOfWork.PiggyBanks.Update(piggyBank);
            await UnitOfWork.SaveChangesAsync();
        }




        public async Task EditCollaborativePiggyBank(EditCollaborativePiggyBankViewModel model)
        {
            var newPiggyBanksIncomes = model.CollaborativePiggyBankIncomes.AsQueryable().ProjectTo<PiggyBanksIncome>(Mapper.ConfigurationProvider).ToList();

            var oldPiggyBankIncomes = await UnitOfWork.PiggyBanksIncomes.Get()
                .Where(pb => pb.PiggyBankId == model.Id && pb.Income.UserId == CurrentUserViewModel.Id)
                .ToListAsync();

            var piggyBankIncomesToDelete = oldPiggyBankIncomes.Except(newPiggyBanksIncomes);
            var piggyBankIncomesToInsert = newPiggyBanksIncomes.Except(oldPiggyBankIncomes);

            foreach (var pbi in piggyBankIncomesToDelete)
            {
                UnitOfWork.PiggyBanksIncomes.Delete(pbi);

            }


            foreach (var pbi in piggyBankIncomesToInsert)
            {
                UnitOfWork.PiggyBanksIncomes.Insert(pbi);
            }

            await UnitOfWork.SaveChangesAsync();
        }




        public async Task UpdateCurrentBalanceWithoutSave(decimal sum, int id)
        {
            var piggyBank = await UnitOfWork.PiggyBanks.Get()
                       .Where(p => p.Id == id)
                       .FirstOrDefaultAsync();
            piggyBank.CurrentBalance = piggyBank.CurrentBalance + sum;
            UnitOfWork.PiggyBanks.Update(piggyBank);
        }


        public async Task<decimal> GetTotalSavings(int userId)
        {
            return await UnitOfWork.PiggyBanksFriends.Get()
                               .Include(pbf => pbf.PiggyBank)
                               .Where(pbf => pbf.UserId == userId && pbf.PiggyBank.IsDeleted == false)
                               .Select(pbf => pbf.PiggyBank.CurrentBalance)
                               .SumAsync()
                                ;
        }

        public async Task<List<PiggyBankViewModel>> GetAlmostCompletedPiggyBanks()
        {

            return await UnitOfWork.PiggyBanks.Get()
                .Where(pb => pb.TargetSum - pb.CurrentBalance <= 100 &&
                    pb.PiggyBankStatus == (int)PiggyBankStatusTypes.Active && pb.IsDeleted == false)
                .ProjectTo<PiggyBankViewModel>(Mapper.ConfigurationProvider)
                .ToListAsync()
                ;
        }


        public async Task<List<PiggyBankViewModel>> GetNearDueDatePiggyBanks()
        {

            return await UnitOfWork.PiggyBanks.Get()
                .Where(pb => pb.DueDate.HasValue && (pb.DueDate.Value.Day - DateTime.Now.Day <= 7)
                 && pb.PiggyBankStatus == (int)PiggyBankStatusTypes.Active && pb.IsDeleted == false)
                .ProjectTo<PiggyBankViewModel>(Mapper.ConfigurationProvider)
                .ToListAsync()
                ;
        }


        public async Task AddCollaborator(PiggyBankCollaborationViewModel model)
        {
            var newPiggyBanksFriends = new PiggyBanksFriend()
            {
                UserId = model.UserId,
                PiggyBankId = model.PiggyBankId,
            };

            UnitOfWork.PiggyBanksFriends.Insert(newPiggyBanksFriends);
            await UnitOfWork.SaveChangesAsync();
        }


        public async Task<List<FriendViewModel>> GetCollaborators(int piggyBankId)
        {
            var collaborators = await UnitOfWork.PiggyBanksFriends.Get()
                                .Where(pbf => pbf.PiggyBankId == piggyBankId && pbf.UserId != CurrentUserViewModel.Id)
                                .Select(pbf => pbf.User)
                                .ProjectTo<FriendViewModel>(Mapper.ConfigurationProvider)
                                .ToListAsync();


            return collaborators;
        }


        public async Task<List<SelectListItem>> GetNonCollaborators(int piggyBankId, string name)
        {

            var friends = UnitOfWork.Friendships.Get()
                                    .Where(f => f.User2Id == CurrentUserViewModel.Id || f.User1Id == CurrentUserViewModel.Id)
                                    .Select(f => f.User1Id == CurrentUserViewModel.Id ? f.User2Id : f.User1Id);

            var piggyBankFriends = UnitOfWork.PiggyBanksFriends.Get()
                                    .Where(pbf => pbf.PiggyBankId == piggyBankId && pbf.UserId!=pbf.PiggyBank.CreatorId)
                                    .Select(pbf => pbf.UserId);

            var nonCollaboratorsIds = await friends.Except(piggyBankFriends).ToListAsync();

            name = name.ToLower();
            var nonCollaboratorsNames = await UnitOfWork.Users.Get()
                                        .Where(u => nonCollaboratorsIds.Contains(u.Id) && (u.Username.ToLower().Contains(name) ||
                                        u.Email.ToLower().Contains(name) || u.FirstName.ToLower().Contains(name) || u.LastName.ToLower().Contains(name)))
                                        .Select(u => u.Username)
                                        .Take(5)
                                        .ToListAsync();

            var suggestions = new List<SelectListItem>();
            
            
            for (var index=0;index< nonCollaboratorsNames.Count;index++)
            {
                var suggestion = new SelectListItem()
                {
                    Value = nonCollaboratorsIds[index].ToString(),
                    Text = nonCollaboratorsNames[index]
                };

                suggestions.Add(suggestion);    
            }
            

            return suggestions;
        }

        
    }
}
