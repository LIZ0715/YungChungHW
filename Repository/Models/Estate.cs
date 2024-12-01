using System;
using System.Collections.Generic;

namespace Yungching.Repository.Models;

public partial class Estate
{
    public int Id { get; set; }

    public int? MemberShipId { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int Price { get; set; }

    /// <summary>
    /// 1=公寓 2=透天
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// 坪數
    /// </summary>
    public int Range { get; set; }

    /// <summary>
    /// 狀態 刪除false 存在true
    /// </summary>
    public bool Status { get; set; }

    public DateTime CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual MemberShip? MemberShip { get; set; }
}
