﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace diveWebMVC.Models;

public partial class TUorder
{
    public int OrderId { get; set; }

    public int? MemberId { get; set; }

    public int? OrderLogId { get; set; }

    public int? OrderStatusId { get; set; }

    public DateTime? OrderDate { get; set; }

    public virtual TMmemberList Member { get; set; }

    public virtual ICollection<TUorderDetail> TUorderDetails { get; set; } = new List<TUorderDetail>();

    public virtual ICollection<TUorderLog> TUorderLogs { get; set; } = new List<TUorderLog>();
}