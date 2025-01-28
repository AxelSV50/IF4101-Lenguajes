using System;
using System.Collections.Generic;

namespace APISandbox.Models;

public partial class GetAllStudentsView
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? NationalityId { get; set; }

    public string? NationalityName { get; set; }
}
