﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace diveWebMVC.Models;

public partial class TMdivingLevelName
{
    public int LevelNameId { get; set; }

    public string LevelTypeName { get; set; }

    public virtual ICollection<TMdivingLevel> TMdivingLevels { get; set; } = new List<TMdivingLevel>();
}