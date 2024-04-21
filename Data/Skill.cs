using System;
using System.Collections.Generic;

namespace Simple_API_user.Models;

public partial class Skill
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Applicantid { get; set; }

    public virtual Applicant Applicant { get; set; } = null!;
}
