using System;
using System.Collections.Generic;

namespace EWallet.Entities.Entities;

public partial class PiggyBanksFriend
{
    public int Id { get; set; }

    public int PiggyBankId { get; set; }

    public int UserId { get; set; }

    public virtual PiggyBank PiggyBank { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
