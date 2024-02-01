using System;
using System.Collections.Generic;

namespace EWallet.Entities.Entities;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime Birthdate { get; set; }

    public string Username { get; set; } = null!;

    public byte[] UserPassword { get; set; } = null!;

    public string Email { get; set; } = null!;

    public decimal CurrentBalance { get; set; }

    public decimal? PreviousBalance { get; set; }

    public int UserRoleId { get; set; }

    public virtual ICollection<Friendship> FriendshipUser1s { get; set; } = new List<Friendship>();

    public virtual ICollection<Friendship> FriendshipUser2s { get; set; } = new List<Friendship>();

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<ImmediateTransaction> ImmediateTransactions { get; set; } = new List<ImmediateTransaction>();

    public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();

    public virtual ICollection<PiggyBank> PiggyBanks { get; set; } = new List<PiggyBank>();

    public virtual ICollection<PiggyBanksFriend> PiggyBanksFriends { get; set; } = new List<PiggyBanksFriend>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<Spending> Spendings { get; set; } = new List<Spending>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual Role UserRole { get; set; } = null!;
}
