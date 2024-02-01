using System;
using System.Collections.Generic;

namespace EWallet.Entities.Entities;

public partial class PiggyBankStatusType
{
    public int Id { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<PiggyBank> PiggyBanks { get; set; } = new List<PiggyBank>();
}
