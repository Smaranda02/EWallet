using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.WebJobs.Code
{
    public interface ICronJob
    {
        Task Run(CancellationToken token = default);
    }
}
