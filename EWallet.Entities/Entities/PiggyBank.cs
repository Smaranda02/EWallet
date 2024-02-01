using System;
using System.Collections.Generic;

namespace EWallet.Entities.Entities;

public partial class PiggyBank
{
    public int Id { get; set; }

    public decimal TargetSum { get; set; }

    public DateTime? DueDate { get; set; }

    public string PiggyBankDescription { get; set; } = null!;

    public int SavingPriority { get; set; }

    public decimal CurrentBalance { get; set; }

    public int? PiggyBankStatus { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatorId { get; set; }

    public virtual User Creator { get; set; } = null!;

    public virtual PiggyBankStatusType? PiggyBankStatusNavigation { get; set; }

    public virtual ICollection<PiggyBanksFriend> PiggyBanksFriends { get; set; } = new List<PiggyBanksFriend>();

    public virtual ICollection<PiggyBanksIncome> PiggyBanksIncomes { get; set; } = new List<PiggyBanksIncome>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
