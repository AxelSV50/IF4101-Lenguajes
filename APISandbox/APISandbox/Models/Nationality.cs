using System;
using System.Collections.Generic;

namespace APISandbox.Models;

public partial class Nationality
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
