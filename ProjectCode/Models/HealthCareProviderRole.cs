using System;
using System.Collections.Generic;

namespace BuildProjectSummer2024.Models;

public partial class HealthCareProviderRole
{
    public int Id { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<HealthCareProvider> HealthCareProviders { get; set; } = new List<HealthCareProvider>();
}
