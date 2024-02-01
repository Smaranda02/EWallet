using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NCrontab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.WebJobs.Code
{
    public static class Extensions
    {
        public static IServiceCollection AddCronJob<T>(this IServiceCollection services, string cronExpression)
            where T : class, ICronJob
        {
            var cron = CrontabSchedule.TryParse(cronExpression)
                       ?? throw new ArgumentException("Invalid cron expression", nameof(cronExpression));

            var entry = new CronRegistryEntry(typeof(T), cron);

            services.AddHostedService<CronScheduler>();
            services.TryAddScoped<T>();
            services.AddSingleton(entry);

            return services;
        }
    }
}
