﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace JOB_PORTAL.Models;

public partial class Skill
{
    public int SkillId { get; set; }

    public int? JobSeekerId { get; set; }

    public string SkillName { get; set; }

    public string ExpertLevel { get; set; }

    public int? JobId { get; set; }

    public virtual Job Job { get; set; }

    public virtual JobSeeker JobSeeker { get; set; }
}