using System;
using System.Collections.Generic;

namespace Simple_API_user.Models;

public partial class Applicant
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
}
