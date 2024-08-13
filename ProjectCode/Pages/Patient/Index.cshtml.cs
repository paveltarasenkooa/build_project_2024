using BuildProjectSummer2024.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BuildProjectSummer2024.Pages.Patient
{
    public class IndexModel : PageModel
    {
        private readonly BuildProject2024Context _context;
        public List<Models.Patient> Patients;
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public IndexModel(BuildProject2024Context context)
        {
            _context = context;
        }

        public void OnGet(int? hospitalId, int? pageIndex)
        {
            const int pageSize = 5; // Adjust the page size as needed
            PageIndex = pageIndex ?? 1;

            IQueryable<Models.Patient> patientsQuery = _context.Patients;

            if (hospitalId.HasValue)
            {
                patientsQuery = patientsQuery.Where(p => p.HospitalId == hospitalId);
            }

            int totalPatients = patientsQuery.Count();
            TotalPages = (int)Math.Ceiling(totalPatients / (double)pageSize);

            Patients = patientsQuery.Where(x=> !hospitalId.HasValue || x.HospitalId == hospitalId).Select(x => new Models.Patient
            {
                Id = x.Id,
                Adress = x.Adress,
                City = x.City,
                ClaimsCount = x.MedicalClaims.Count,
                Gender = x.Gender,
                LastName = x.LastName,
                Mrn = x.Mrn,
                Ssn = x.Ssn,
                HospitalId = x.HospitalId,
                Hospital = new Models.Hospital
                {
                    Id = x.HospitalId,
                    HospitalName = x.Hospital.HospitalName
                },
                Dob = x.Dob,
                FirstName = x.FirstName,
                InsuranceMemberId = x.InsuranceMemberId,
                Phone = x.Phone,
                State = x.State,
                Zip = x.Zip
            })
            .Skip((PageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        }
    }
}
