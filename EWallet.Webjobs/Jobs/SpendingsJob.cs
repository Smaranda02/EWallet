using EWallet.BusinessLogic.Implementation.Spendings;
using EWallet.WebJobs.Code;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.WebJobs.Jobs
{
    public class SpendingsJob : ICronJob
    {
        private readonly ILogger<IncomesJob> _logger;
        private readonly SpendingService _spendingService;

        public SpendingsJob(ILogger<IncomesJob> logger, SpendingService spendingService)
        {
            _logger = logger;
            _spendingService = spendingService;
        }
        public async Task Run(CancellationToken token = default)
        {
            _logger.LogInformation("Hello from {name} at: {time}", nameof(SpendingsJob), DateTime.UtcNow.ToShortTimeString());
            await _spendingService.AddRecurringSpendings();
            //return Task.CompletedTask;
        }
    }
}
