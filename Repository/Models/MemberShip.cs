using System;
using System.Collections.Generic;

namespace Yungching.Repository.Models;

public partial class MemberShip
{
    public int Id { get; set; }

    public string Account { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Estate> Estates { get; set; } = new List<Estate>();
}
