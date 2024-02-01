using EWallet.Common;
using EWallet.Entities.Entities;
using EWallet.DataAccess.EntityFramework;


namespace EWallet.DataAccess
{
    public class UnitOfWork
    {
        private readonly EWalletContext Context;

        public UnitOfWork(EWalletContext context)
        {
            this.Context = context;
        }

        private IRepository<User> users;
        public IRepository<User> Users => users ?? (users = new BaseRepository<User>(Context));

        private IRepository<Income> incomes;
        public IRepository<Income> Incomes => incomes ?? (incomes = new BaseRepository<Income>(Context));

        private IRepository<Spending> spendings;
        public IRepository<Spending> Spendings => spendings ?? (spendings = new BaseRepository<Spending>(Context));

        private IRepository<PiggyBank> piggyBanks;
        public IRepository<PiggyBank> PiggyBanks => piggyBanks ?? (piggyBanks = new BaseRepository<PiggyBank>(Context));

        private IRepository<Transaction> transactions;
        public IRepository<Transaction> Transactions => transactions ?? (transactions = new BaseRepository<Transaction>(Context));


        private IRepository<SpendingCategory> spendingCategories;
        public IRepository<SpendingCategory> SpendingCategories => spendingCategories ?? (spendingCategories = new BaseRepository<SpendingCategory>(Context));


        private IRepository<StoredDescription> storedDescription;
        public IRepository<StoredDescription> StoredDescriptions => storedDescription ?? (storedDescription = new BaseRepository<StoredDescription>(Context));


        private IRepository<Role> role;
        public IRepository<Role> Roles => role ?? (role = new BaseRepository<Role>(Context));

        private IRepository<Image> image;
        public IRepository<Image> Images => image ?? (image = new BaseRepository<Image>(Context));

        private IRepository<RecurrenceType> recurrenceType;
        public IRepository<RecurrenceType> RecurrenceTypes => recurrenceType ?? (recurrenceType = new BaseRepository<RecurrenceType>(Context));


        private IRepository<PiggyBanksIncome> piggyBanksIncome;
        public IRepository<PiggyBanksIncome> PiggyBanksIncomes => piggyBanksIncome ?? (piggyBanksIncome = new BaseRepository<PiggyBanksIncome>(Context));


        private IRepository<VwSpendingCategoriesCount> spendingCategoriesCount;
        public IRepository<VwSpendingCategoriesCount> SpendingCategoriesCount => spendingCategoriesCount ?? (spendingCategoriesCount = new BaseRepository<VwSpendingCategoriesCount>(Context));

        private IRepository<VwUpcomingBirthday> vwUpcomingBirthdays;
        public IRepository<VwUpcomingBirthday> VwUpcomingBirthdays => vwUpcomingBirthdays ?? (vwUpcomingBirthdays = new BaseRepository<VwUpcomingBirthday>(Context));


        private IRepository<Friendship> friendships;
        public IRepository<Friendship> Friendships => friendships ?? (friendships = new BaseRepository<Friendship>(Context));



        private IRepository<PiggyBanksFriend> piggyBanksFriends;
        public IRepository<PiggyBanksFriend> PiggyBanksFriends => piggyBanksFriends ?? (piggyBanksFriends = new BaseRepository<PiggyBanksFriend>(Context));


        public void SaveChanges()
        {
            Context.SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
