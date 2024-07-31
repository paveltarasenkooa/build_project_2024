using System;
using System.Collections.Generic;

namespace BuildProjectSummer2024.Models;

public partial class Patient
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Gender { get; set; }

    public DateOnly? Dob { get; set; }

    public string? Adress { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Zip { get; set; }

    public string? Phone { get; set; }

    public string? Mrn { get; set; }

    public string? Ssn { get; set; }

    public int HospitalId { get; set; }

    public string? InsuranceMemberId { get; set; }

    public virtual Hospital Hospital { get; set; } = null!;

    public virtual ICollection<MedicalClaim> MedicalClaims { get; set; } = new List<MedicalClaim>();
}
