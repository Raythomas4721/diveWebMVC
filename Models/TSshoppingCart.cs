﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace diveWebMVC.Models;

public partial class TSshoppingCart
{
    public int CartId { get; set; }

    public int? MemberId { get; set; }

    public DateOnly? Date { get; set; }

    public string ScheduleId { get; set; }

    public virtual TMmemberList Member { get; set; }
}