﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace diveWebMVC.Models;

public partial class TSphoto
{
    public int PhotoId { get; set; }

    public int? SiteId { get; set; }

    public byte[] Photo { get; set; }

    public virtual TSsiteDetail Site { get; set; }
}