﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace JOB_PORTAL.Models;

public partial class Application
{
    public int ApplicationId { get; set; }

    public int? JobId { get; set; }

    public int? JobSeekerId { get; set; }

    public DateOnly? ApplyDate { get; set; }

    public string Status { get; set; }

    public int? EmployerId { get; set; }

    public virtual EmployerDetail Employer { get; set; }

    public virtual Job Job { get; set; }

    public virtual JobSeeker JobSeeker { get; set; }
}