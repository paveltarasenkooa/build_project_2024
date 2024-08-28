namespace BuildProjectSummer2024.Models.PageModels
{
    public class HospitalModel
    {
        public int PatientsCount { get; set; }
        public int HealthcareProvidersCount { get; set; }
        public int Id { get; set; }

        public string HospitalName { get; set; } = null!;

        public string? Adress { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Zip { get; set; }

        public string? Phone { get; set; }

    }
}
