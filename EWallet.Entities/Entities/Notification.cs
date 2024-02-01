using System;
using System.Collections.Generic;

namespace EWallet.Entities.Entities;

public partial class Notification
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public int UserId { get; set; }

    public bool Seen { get; set; }

    public virtual User User { get; set; } = null!;
}
