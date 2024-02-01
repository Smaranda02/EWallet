using AutoMapper.QueryableExtensions;
using EWallet.BusinessLogic.Implementation.Spendings.ViewModel;
using EWallet.BusinessLogic.Implementation.Transactions.ViewModel;
using EWallet.Common.Enums;
using EWallet.Entities.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.BusinessLogic.Implementation.Transactions
{
    public class TransactionService : BaseService
    {
        public TransactionService(ServiceDependencies dependencies)
            : base(dependencies)
        {

        }

        public async Task CreateTransactionAndSave(Transaction newTransaction)
        {
            newTransaction.UserId = CurrentUserViewModel.Id;
            newTransaction.TrasactionDateTime = DateTime.Now;
            UnitOfWork.Transactions.Insert(newTransaction);
            await UnitOfWork.SaveChangesAsync();
        }

        public void CreateTransactionWithoutSave(Transaction newTransaction)
        {
            newTransaction.TrasactionDateTime = DateTime.Now;
            UnitOfWork.Transactions.Insert(newTransaction);
        }

        public async Task<int> GetFilteredTransactionsCount(int? categoryFilter)
        {
            var query = UnitOfWork.Transactions.Get()
            .Where(u => u.UserId == CurrentUserViewModel.Id);


            if (categoryFilter == (int)(TransactionTypes.Income))
            {
                query.Where(t => t.IncomeId != null);
            }

            else if (categoryFilter == (int)(TransactionTypes.Spending))
            {
                query = query.Where(t => t.SpendingId != null);
            }

            return await query.CountAsync();
        }

        public async Task<List<TransactionViewModel>> GetTransactions(int pageIndex, int pageSize, int? sortOrder, int? sortFilter, int? categoryFilter)
        {

            var query = UnitOfWork.Transactions.Get()
                   .Where(u => u.UserId == CurrentUserViewModel.Id);

            if (categoryFilter == (int)(TransactionTypes.Income))
            {
                query = query.Where(t => t.IncomeId.HasValue);
            }

            else if (categoryFilter == (int)(TransactionTypes.Spending))
            {
                query = query.Where(t => t.SpendingId.HasValue);
            }


            if (sortOrder == null)
            {
                query = query.OrderByDescending(u => u.TrasactionDateTime);
            }


            if (sortFilter == (int)(SortFilters.Sum))
            {
                if (sortOrder == (int)(SortTypes.Ascending))
                {
                    query = query.OrderBy(t => t.TrasactionSum);
                }
                else
                {
                    query = query.OrderByDescending(t => t.TrasactionSum);
                }
            }

            else if (sortFilter == (int)(SortFilters.Date))
            {
                if (sortOrder == (int)(SortTypes.Ascending))
                {
                    query = query.OrderBy(t => t.TrasactionDateTime);
                }
                else
                {
                    query = query.OrderByDescending(t => t.TrasactionDateTime);
                }
            }

            return await query.ProjectTo<TransactionViewModel>(Mapper.ConfigurationProvider)
                   .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                   .ToListAsync();
        }

        public List<SelectListItem> InitializeTransactionCategories()
        {
            var allCategories = Enum.GetValues(typeof(TransactionTypes)).Cast<TransactionTypes>()
               .Select(category => new SelectListItem()
               {
                   Text = category.ToString(),
                   Value = ((int)category).ToString()
               }).ToList();

            return allCategories;

        }


        public async Task<int> GetNumberOfIncomes()
        {
            var incomesNumber = await UnitOfWork.Transactions.Get()
              .CountAsync(i => i.UserId == CurrentUserViewModel.Id && i.IncomeId!=null);
            return incomesNumber;
        }


        public async Task<int> GetNumberOfSpendings()
        {
            var spendingsNumber = await UnitOfWork.Transactions.Get()
              .CountAsync(i => i.UserId == CurrentUserViewModel.Id && i.SpendingId != null);
            return spendingsNumber; 
        }


        public async Task<int> GetNumberOfSavings()
        {
            var spendingsNumber = await UnitOfWork.Transactions.Get()
              .CountAsync(i => i.UserId == CurrentUserViewModel.Id && i.PiggyBankId != null);
            return spendingsNumber;
        }

    }
}
