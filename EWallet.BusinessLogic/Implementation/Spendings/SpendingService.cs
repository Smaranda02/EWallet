using AutoMapper.QueryableExtensions;
using EWallet.BusinessLogic.Implementation.Spendings.Validations;
using EWallet.BusinessLogic.Implementation.Spendings.ViewModel;
using EWallet.BusinessLogic.Implementation.Transactions;
using EWallet.BusinessLogic.Implementation.Users;
using EWallet.Common.Extensions;
using EWallet.Entities.Entities;
using EWallet.Common.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EWallet.BusinessLogic.Implementation.VwSpendingCategoriesCount.ViewModel;

namespace EWallet.BusinessLogic.Implementation.Spendings
{
    public class SpendingService : BaseService
    {

        private readonly TransactionService _transactionService;
        private readonly UserService _userService;


        public SpendingService(ServiceDependencies dependencies, TransactionService transactionService, UserService userService)
          : base(dependencies)
        {
            _transactionService = transactionService;
            _userService = userService;
        }

        public async Task<List<SpendingViewModel>> GetSpendings(int pageNumber, int pageSize)
        {
            var spendings = await UnitOfWork.Spendings.Get()
                .Where(i => i.UserId == CurrentUserViewModel.Id && i.IsDeleted == false)
                .ProjectTo<SpendingViewModel>(Mapper.ConfigurationProvider)
                .Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .ToListAsync();

            return spendings;
        }

        public async Task CreateSpending(CreateSpendingViewModel model)
        {
            var newSpending = Mapper.Map<CreateSpendingViewModel, Spending>(model);
            newSpending.UserId = CurrentUserViewModel.Id;
            if (model.Image != null)
            {
                var image = new Image();
                image.Id = newSpending.Id;
                using (var ms = new MemoryStream())
                {
                    model.Image.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    image.Photo = fileBytes;
                }

                UnitOfWork.Images.Insert(image);
                newSpending.Image = image;
            }
            UnitOfWork.Spendings.Insert(newSpending);


            var recurrenceType = newSpending.RecurrenceTypeId;
            if (!recurrenceType.HasValue)
            {
                var transaction = Mapper.Map<Spending, Transaction>(newSpending);
                _transactionService.CreateTransactionWithoutSave(transaction);
                transaction.SpendingId = null;
                transaction.Spending = newSpending;


                decimal amount = (-1) * newSpending.Amount;
                var userId = newSpending.UserId;
                await _userService.UpdateCurrentBalanceWithoutSave(amount, userId);
            }
            await UnitOfWork.SaveChangesAsync();

        }


        public async Task DeleteSpending(int id)
        {
            var spending = await UnitOfWork.Spendings.Get()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();

            spending.IsDeleted = true;
            UnitOfWork.Spendings.Update(spending);
            await UnitOfWork.SaveChangesAsync();
        }


        public async Task EditSpending(EditSpendingViewModel model)
        {
            var spending = Mapper.Map<EditSpendingViewModel, Spending>(model);
            spending.UserId = CurrentUserViewModel.Id;
            UnitOfWork.Spendings.Update(spending);
            await UnitOfWork.SaveChangesAsync();
        }


        public async Task CreateSpendingCategory(CreateSpendingCategoryViewModel model)
        {
            var spendingCategory = Mapper.Map<CreateSpendingCategoryViewModel, SpendingCategory>(model);
            spendingCategory.UserId = CurrentUserViewModel.Id;
            UnitOfWork.SpendingCategories.Insert(spendingCategory);
            await UnitOfWork.SaveChangesAsync();
        }


        public async Task<List<SelectListItem>> InitializeSpendingCategories()
        {
            var allSpendingCategories = await UnitOfWork.SpendingCategories.Get()
                .Where(s => s.UserId == CurrentUserViewModel.Id || !s.UserId.HasValue)
                .Select(r => new SelectListItem()
                {
                    Text = r.CategoryName,
                    Value = r.Id.ToString()
                }).ToListAsync();
            return allSpendingCategories;

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

        public async Task<SpendingViewModel> GetCurrentSpendingToEdit(int id)
        {
            var spending = await UnitOfWork.Spendings.Get()
                .Where(i => i.Id == id)
                .ProjectTo<SpendingViewModel>(Mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return spending;

        }

        public async Task AddRecurringSpendings()
        {

            var dayOfMonth = DateTime.Now.Day;
            var dayOfWeek = (int)DateTime.Now.DayOfWeek;
            var dayOfYear = DateTime.Now.DayOfYear;
            var spendingsOfTheDay = await UnitOfWork.Spendings.Get()
                .Where(i =>
                    (i.RecurrenceTypeId == (int?)RecurrenceTypes.Weekly && i.RecurringNumber == dayOfWeek) ||
                    (i.RecurrenceTypeId == (int?)RecurrenceTypes.Monthly && i.RecurringNumber == dayOfMonth) ||
                    (i.RecurrenceTypeId == (int?)RecurrenceTypes.Yearly && i.RecurringNumber == dayOfYear)
                )
                .ToListAsync();


            var spendingsUserIds = spendingsOfTheDay.Select(u => u.Id).Distinct().ToList();
            var usersToUpdate = await UnitOfWork.Users.Get().Where(u => spendingsUserIds.Contains(u.Id)).ToListAsync();
            usersToUpdate.ForEach(u =>
            {
                var diff = spendingsOfTheDay.Where(s => s.UserId == u.Id).Sum(s => s.Amount);
                u.CurrentBalance = u.CurrentBalance - diff;
            });

            var transactions = spendingsOfTheDay.AsQueryable().ProjectTo<Transaction>(Mapper.ConfigurationProvider).ToList();
            transactions.ForEach(t => UnitOfWork.Transactions.Insert(t));

            await UnitOfWork.SaveChangesAsync();
        }


        public async Task<List<VwSpendingCategoriesCountViewModel>> GetSpendingCategoriesCount()
        {
            return await UnitOfWork.SpendingCategoriesCount.Get()
                 .Where(sc => sc.UserId==CurrentUserViewModel.Id)
                 .ProjectTo<VwSpendingCategoriesCountViewModel>(Mapper.ConfigurationProvider)
                 .ToListAsync();

        }

        public async Task<List<SpendingViewModel>> GetUpcomingSpendings()
        {
            DateTime currentDate = DateTime.Now;
            var startOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek + (int)DayOfWeek.Sunday);
            var endOfWeek = startOfWeek.AddDays(6);


            var startOfWeekInCurrentYear = new DateTime(currentDate.Year, 1, 1)
                .AddDays((int)DayOfWeek.Sunday - (int)new DateTime(currentDate.Year, 1, 1).DayOfWeek)
                .AddDays((currentDate.DayOfYear - 1) / 7 * 7);
            var endOfWeekInCurrentYear = startOfWeekInCurrentYear.AddDays(6);


            var spendingsOfTheWeek = await UnitOfWork.Spendings.Get()
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
              .Take(10)
              .ProjectTo<SpendingViewModel>(Mapper.ConfigurationProvider)
              .ToListAsync();


            return spendingsOfTheWeek;
        }

    }
}
