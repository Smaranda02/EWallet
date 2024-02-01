using System;
using System.Collections.Generic;

namespace EWallet.Entities.Entities;

public partial class Post
{
    public int Id { get; set; }

    public string PostMessage { get; set; } = null!;

    public DateTime PostDateTime { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User User { get; set; } = null!;
}
