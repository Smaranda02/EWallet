using System;
using System.Collections.Generic;

namespace EWallet.Entities.Entities;

public partial class PiggyBankStatus
{
    public int Id { get; set; }

    public string StatusName { get; set; } = null!;
}
