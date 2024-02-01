using EWallet.BusinessLogic.Implementation.Incomes;
using EWallet.WebJobs.Code;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.WebJobs.Jobs
{
    public class IncomesJob : ICronJob
    {
        private readonly ILogger<IncomesJob> _logger;
        private readonly IncomeService _incomeService;

        public IncomesJob(ILogger<IncomesJob> logger, IncomeService incomeService)
        {
            _logger = logger;
            _incomeService = incomeService;
        }
        public async Task Run(CancellationToken token = default)
        {
            _logger.LogInformation("Hello from {name} at: {time}", nameof(IncomesJob), DateTime.UtcNow.ToShortTimeString());
            await _incomeService.AddRecurringIncomes();
            //return Task.CompletedTask;
        }
    }
}
