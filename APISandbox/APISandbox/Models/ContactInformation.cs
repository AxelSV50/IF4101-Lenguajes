using System;
using System.Collections.Generic;

namespace APISandbox.Models;

public partial class ContactInformation
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Message { get; set; }
}
