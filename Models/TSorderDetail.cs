﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace diveWebMVC.Models;

public partial class TSorderDetail
{
    public int OrderDetailId { get; set; }

    public int? OrderId { get; set; }

    public DateOnly? Date { get; set; }

    public string ScheduleId { get; set; }

    public decimal? UnitPrice { get; set; }

    public virtual TSorder Order { get; set; }
}