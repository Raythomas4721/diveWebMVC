﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace diveWebMVC.Models;

public partial class TNproduct
{
    public int ProductId { get; set; }

    public string ProductName { get; set; }

    public decimal? UnitCost { get; set; }

    public string Description { get; set; }

    public byte[] Picture { get; set; }

    public virtual ICollection<TNpicture> TNpictures { get; set; } = new List<TNpicture>();

    public virtual ICollection<TNproductvariant> TNproductvariants { get; set; } = new List<TNproductvariant>();
}