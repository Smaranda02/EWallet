using AutoMapper.QueryableExtensions;
using EWallet.BusinessLogic.Implementation.Incomes.Validations;
using EWallet.BusinessLogic.Implementation.Incomes.ViewModel;
using EWallet.BusinessLogic.Implementation.PiggyBanks;
using EWallet.BusinessLogic.Implementation.Spendings.Validations;
using EWallet.BusinessLogic.Implementation.Spendings.ViewModel;
using EWallet.BusinessLogic.Implementation.Transactions;
using EWallet.BusinessLogic.Implementation.Users;
using EWallet.BusinessLogic.Implementation.Users.ViewModel;
using EWallet.Common.Enums;
using EWallet.DataAccess.Enums;
using EWallet.Entities.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.BusinessLogic.Implementation.Incomes
{
    public class IncomeService : BaseService
    {
        private readonly TransactionService _transactionService;
        private readonly UserService _userService;
        private readonly PiggyBankService _piggyBankService;



        public IncomeService(ServiceDependencies dependencies, TransactionService transactionService, UserService userService,
           PiggyBankService piggyBankService)
            : base(dependencies)
        {
            _transactionService = transactionService;
            _userService = userService;
            _piggyBankService = piggyBankService;
        }

          public async Task<List<SelectListItem>> GetRecurrenceTypesAsSelectListItems()
        {
            var allRecurrenceTypes = await UnitOfWork.RecurrenceTypes.Get()
                .Select(r => new SelectListItem()
                {
                    Text = r.RecurrenceTypeName,
                    Value = r.Id.ToString()
                }).ToListAsync();
            return allRecurrenceTypes;

        }
        public async Task CreateIncome(CreateIncomeViewModel model)
        {
            var newIncome = Mapper.Map<CreateIncomeViewModel, Income>(model);
            newIncome.UserId = CurrentUserViewModel.Id;
            UnitOfWork.Incomes.Insert(newIncome);

            var recurrenceType = newIncome.RecurrenceTypeId;
            if (!recurrenceType.HasValue)
            {
                var transaction = Mapper.Map<Income, Transaction>(newIncome);
                transaction.IncomeId = null;
                transaction.Income = newIncome;
                _transactionService.CreateTransactionWithoutSave(transaction);

                decimal amount = newIncome.IncomeSum;
                var userId = newIncome.UserId;
                await _userService.UpdateCurrentBalanceWithoutSave(amount, userId);

            }
            await UnitOfWork.SaveChangesAsync();
        }



        //for the week 
        public async Task<List<IncomeViewModel>> GetUpcomingIncomes()
        {
            DateTime currentDate = DateTime.Now;
            var startOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek + (int)DayOfWeek.Sunday);
            var endOfWeek = startOfWeek.AddDays(6);
            

            var startOfWeekInCurrentYear = new DateTime(currentDate.Year, 1, 1)
                .AddDays((int)DayOfWeek.Sunday - (int)new DateTime(currentDate.Year, 1, 1).DayOfWeek)
                .AddDays((currentDate.DayOfYear - 1) / 7 * 7);
            var endOfWeekInCurrentYear = startOfWeekInCurrentYear.AddDays(6);


            var incomesOfTheWeek = await UnitOfWork.Incomes.Get()
                .Where(i => i.UserId == CurrentUserViewModel.Id && i.IsDeleted == false &&
                (
                    (i.RecurrenceTypeId == (int?)RecurrenceTypes.Weekly) ||
                    (i.RecurrenceTypeId == (int?)RecurrenceTypes.Monthly && 
                    i.RecurringNumber >= startOfWeek.Day &&
                    i.RecurringNumber <= endOfWeek.Day) ||
                    (i.RecurrenceTypeId == (int?)RecurrenceTypes.Yearly && 
                    i.RecurringNumber >= startOfWeekInCurrentYear.DayOfYear
                    && i.RecurringNumber <= endOfWeekInCurrentYear.DayOfYear)
                    )
                )
              .Take(18)
              .ProjectTo<IncomeViewModel>(Mapper.ConfigurationProvider)
              .ToListAsync();


            return incomesOfTheWeek;
        }


        public async Task<List<IncomeViewModel>> GetIncomes()
        {
            var incomes = await UnitOfWork.Incomes.Get()
              .Where(i => i.UserId == CurrentUserViewModel.Id && i.IsDeleted == false)
              .ProjectTo<IncomeViewModel>(Mapper.ConfigurationProvider)
              .ToListAsync();
            return incomes;
        }




        public async Task<List<IncomeViewModel>> GetLatestIncomes()
        {
            var incomes = await UnitOfWork.Incomes.Get()
              .Where(i => i.UserId == CurrentUserViewModel.Id && i.IsDeleted == false)
              .OrderByDescending(i => i.Id)
              .Take(5)
              .ProjectTo<IncomeViewModel>(Mapper.ConfigurationProvider)
              .ToListAsync();
            return incomes;
        }

        public async Task DeleteIncome(int id)
        {
            var income = await UnitOfWork.Incomes.Get()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
            income.IsDeleted = true;
            income.IncomeSum = 1;

            UnitOfWork.Incomes.Update(income);
            await UnitOfWork.SaveChangesAsync();
        }


        public async Task<IncomeViewModel> GetIncomeToEdit(int id)
        {
            var income = await UnitOfWork.Incomes.Get()
                .Where(i => i.UserId == CurrentUserViewModel.Id && i.Id == id)
                .ProjectTo<IncomeViewModel>(Mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return income;

        }


        public async Task EditIncome(EditIncomeViewModel model)
        {
            var income = Mapper.Map<EditIncomeViewModel, Income>(model);
            income.UserId = CurrentUserViewModel.Id;
            UnitOfWork.Incomes.Update(income);
            await UnitOfWork.SaveChangesAsync();
        }


        public async Task<List<SelectListItem>> GetIncomesAsSelectListItems()
        {
            var allIncomes = await UnitOfWork.Incomes.Get()
                .Where(i => i.UserId == CurrentUserViewModel.Id && i.IsDeleted==false && i.RecurrenceTypeId.HasValue)
                .Select(r => new SelectListItem()
                {
                    Text = r.IncomeDescription,
                    Value = r.Id.ToString()
                }).ToListAsync();
            return allIncomes;

        }


        public async Task<List<SelectListItem>> GetPiggyBanksAsSelectListItems()
        {
            var piggyBanks = await UnitOfWork.PiggyBanks.Get()
                .Select(r => new SelectListItem()
                {
                    Text = r.PiggyBankDescription,
                    Value = r.Id.ToString()
                }).ToListAsync();
            return piggyBanks;

        }



        public async Task AddRecurringIncomes()
        {
            var dayOfMonth = DateTime.Now.Day;
            var dayOfWeek = (int)DateTime.Now.DayOfWeek;
            var dayOfYear = DateTime.Now.DayOfYear;
            var incomesOfTheDay = await UnitOfWork.Incomes.GetDetached()
                .Where(i =>
                    (i.RecurrenceTypeId == (int?)RecurrenceTypes.Weekly && i.RecurringNumber == dayOfWeek) ||
                    (i.RecurrenceTypeId == (int?)RecurrenceTypes.Monthly && i.RecurringNumber == dayOfMonth) ||
                    (i.RecurrenceTypeId == (int?)RecurrenceTypes.Yearly && i.RecurringNumber == dayOfYear)
                )
                .ToListAsync();

            var incomeUserIds = incomesOfTheDay.Select(i => i.UserId).Distinct().ToList();

            var usersToUpdate = await UnitOfWork.Users.Get().Where(u => incomeUserIds.Contains(u.Id)).ToListAsync();

            usersToUpdate.ForEach(user =>
            {
                var diff = incomesOfTheDay.Where(i => i.UserId == user.Id).Sum(i => i.IncomeSum);
                user.CurrentBalance = user.CurrentBalance + diff;
            });

            var transactions = incomesOfTheDay.AsQueryable().ProjectTo<Transaction>(Mapper.ConfigurationProvider).ToList();
            transactions.ForEach(t => UnitOfWork.Transactions.Insert(t));

            var incomeIds = incomesOfTheDay.Select(i => i.Id).ToList();

            var piggyBanks = await UnitOfWork.PiggyBanks.Get()
               .Include(pb => pb.PiggyBanksIncomes)
               .ThenInclude(pbi => pbi.Income)
               .Where(pb => incomeUserIds.Contains(pb.CreatorId) && pb.IsDeleted == false && pb.PiggyBankStatus == (int)PiggyBankStatusTypes.Active)
               .Where(pb => pb.PiggyBanksIncomes.Any(i => incomeIds.Contains(i.IncomeId)))
               .OrderBy(pb => pb.SavingPriority).ToListAsync()
               ;


            piggyBanks.ForEach(pb =>
            {
                var diff = pb.PiggyBanksIncomes.Where(pbi => incomeIds.Contains(pbi.IncomeId))
                                             .Sum(pbi => pbi.AllocatedIncomeAmount <= pbi.Income.IncomeSum ? pbi.AllocatedIncomeAmount : pbi.Income.IncomeSum);
              

                if (diff > 0)
                {
                    var pbIncomesWithSumLeft = pb.PiggyBanksIncomes.Where(pbi => incomeIds.Contains(pbi.IncomeId) && pbi.AllocatedIncomeAmount <= pbi.Income.IncomeSum).ToList();
                    var pbIncomesWithNoSumLeft = pb.PiggyBanksIncomes.Where(pbi => incomeIds.Contains(pbi.IncomeId) && pbi.AllocatedIncomeAmount > pbi.Income.IncomeSum).ToList(); ;
                    pbIncomesWithSumLeft.ForEach(pbi => pbi.Income.IncomeSum = pbi.Income.IncomeSum - pbi.AllocatedIncomeAmount);
                    pbIncomesWithNoSumLeft.ForEach(pbi => pbi.Income.IncomeSum = 0);

                    pb.CurrentBalance = pb.CurrentBalance + diff;

                    if (pb.TargetSum <= pb.CurrentBalance)
                    {
                        pb.PiggyBankStatus = (int)PiggyBankStatusTypes.Full;
                    }

                    var piggybankTransactions = pb.PiggyBanksIncomes
                                           .Where(pbi => incomeIds.Contains(pbi.IncomeId) && (pbi.AllocatedIncomeAmount <= pbi.Income.IncomeSum ? pbi.AllocatedIncomeAmount : pbi.Income.IncomeSum) > 0)
                                           .Select(pbi => new Transaction
                                           {
                                               PiggyBankId = pbi.PiggyBankId,
                                               TrasactionDateTime = DateTime.Now,
                                               TrasactionSum = pbi.AllocatedIncomeAmount <= pbi.Income.IncomeSum ? pbi.AllocatedIncomeAmount : pbi.Income.IncomeSum,
                                               UserId = pbi.Income.UserId
                                           }).ToList();

                    piggybankTransactions.ForEach(t => _transactionService.CreateTransactionWithoutSave(t));
                }
                          

            });

            await UnitOfWork.SaveChangesAsync();
        }
    }
}
