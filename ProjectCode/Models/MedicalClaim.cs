using System;
using System.Collections.Generic;

namespace BuildProjectSummer2024.Models;

public partial class MedicalClaim
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public int HealthCareProviderId { get; set; }

    public decimal Quantity { get; set; }

    public string? Ndc { get; set; }

    public decimal? TotalAmount { get; set; }

    public int? DaysSupply { get; set; }

    public string? RxNumber { get; set; }

    public DateOnly DateFilled { get; set; }

    public virtual HealthCareProvider HealthCareProvider { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
}
