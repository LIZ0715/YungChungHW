using System;
using System.Collections.Generic;

namespace Yungching.Repository.Models;

public partial class Estate
{
    /// <summary>
    /// 流水號
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 會員Id
    /// </summary>
    public int? MemberShipId { get; set; }

    /// <summary>
    /// 物件名稱
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// 地址
    /// </summary>
    public string Address { get; set; } = null!;

    /// <summary>
    /// 價格
    /// </summary>
    public int Price { get; set; }

    /// <summary>
    /// 1=公寓 2=透天
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// 坪數
    /// </summary>
    public double Range { get; set; }

    /// <summary>
    /// false=停售  true=銷售中
    /// </summary>
    public bool Status { get; set; }

    /// <summary>
    /// 新增時間
    /// </summary>
    public DateTime CreateAt { get; set; }

    /// <summary>
    /// 修改時間
    /// </summary>
    public DateTime? UpdateAt { get; set; }
}
