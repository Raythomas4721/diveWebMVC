﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace diveWebMVC.Models;

public partial class TSorder
{
    public int? MemberId { get; set; }

    public int OrderId { get; set; }

    public int? SiteId { get; set; }

    public virtual TMmemberList Member { get; set; }

    public virtual TSsiteDetail Site { get; set; }

    public virtual ICollection<TSorderDetail> TSorderDetails { get; set; } = new List<TSorderDetail>();
}