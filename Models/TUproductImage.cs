﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace diveWebMVC.Models;

public partial class TUproductImage
{
    public int ProductImages { get; set; }

    public int? ProductId { get; set; }

    public byte[] Image { get; set; }

    public virtual TUproduct Product { get; set; }
}