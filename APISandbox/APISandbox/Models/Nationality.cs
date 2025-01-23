using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace APISandbox.Models;

public partial class Nationality
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
