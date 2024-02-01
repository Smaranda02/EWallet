
using EWallet.BusinessLogic;
using EWallet.BusinessLogic.Implementation.Incomes;
using EWallet.BusinessLogic.Implementation.PiggyBanks;
using EWallet.BusinessLogic.Implementation.Spendings;
using EWallet.BusinessLogic.Implementation.Transactions;
using EWallet.BusinessLogic.Implementation.Users;
using EWallet.DataAccess.EntityFramework;
using EWallet.DataAccess;
using EWallet.WebJobs.Code;
using EWallet.WebJobs.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EWallet.BusinessLogic.Implementation.Users.ViewModel;

var builder = Host.CreateDefaultBuilder(args);


builder.ConfigureServices(services =>
{
    services.AddAutoMapper(options =>
    {
        options.AddMaps(typeof(Program), typeof(BaseService));
    });

    services.AddDbContext<EWalletContext>();

    services.AddScoped<UnitOfWork>();

    services.AddSingleton<CurrentUserViewModel>();

    services.AddScoped<ServiceDependencies>();
    services.AddScoped<UserService>();
    services.AddScoped<IncomeService>();
    services.AddScoped<SpendingService>();
    services.AddScoped<PiggyBankService>();
    services.AddScoped<TransactionService>();



    //services.AddCronJob<IncomesJob>("* * * * *"); //every minute
    //services.AddCronJob<SpendingsJob>("* * * * *"); 
    //services.AddCronJob<SpendingsJob>("0 8 * * *"); //every day at 08:00
    //services.AddCronJob<SpendingsJob>("* /10 * * * *"); //every 10 minutes

});


builder.Build().Run();