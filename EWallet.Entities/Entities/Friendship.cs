using System;
using System.Collections.Generic;

namespace EWallet.Entities.Entities;

public partial class Friendship
{
    public int Id { get; set; }

    public int User1Id { get; set; }

    public int User2Id { get; set; }

    public virtual User User1 { get; set; } = null!;

    public virtual User User2 { get; set; } = null!;
}
