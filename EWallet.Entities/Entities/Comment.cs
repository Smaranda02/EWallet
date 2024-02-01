using System;
using System.Collections.Generic;

namespace EWallet.Entities.Entities;

public partial class Comment
{
    public int Id { get; set; }

    public int ParentCommentId { get; set; }

    public int PostId { get; set; }

    public string CommentMessage { get; set; } = null!;

    public DateTime CommentDateTime { get; set; }

    public virtual ICollection<Comment> InverseParentComment { get; set; } = new List<Comment>();

    public virtual Comment ParentComment { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;
}
