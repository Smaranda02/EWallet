using System;
using System.Collections.Generic;

namespace EWallet.Entities.Entities;

public partial class Income
{
    public int Id { get; set; }

    public decimal IncomeSum { get; set; }

    public string IncomeDescription { get; set; } = null!;

    public int? RecurringNumber { get; set; }

    public int? RecurrenceTypeId { get; set; }

    public int UserId { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<ImmediateTransaction> ImmediateTransactions { get; set; } = new List<ImmediateTransaction>();

    public virtual ICollection<PiggyBanksIncome> PiggyBanksIncomes { get; set; } = new List<PiggyBanksIncome>();

    public virtual RecurrenceType? RecurrenceType { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual User User { get; set; } = null!;
}
