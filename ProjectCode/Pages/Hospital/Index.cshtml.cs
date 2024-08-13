using BuildProjectSummer2024.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BuildProjectSummer2024.Pages.Hospital
{
    public class IndexModel : PageModel
    {
        // we need to add context to have access to our database
        private readonly BuildProject2024Context _context;
        // in Hospitals variable we will store our hospitals from database
        public List<Models.Hospital> Hospitals;

        //let's initialise our context
        public IndexModel(BuildProject2024Context context)
        {
            _context = context;
        }

        // OnGet method is called when page is loaded. Lets pull all hospitals from our database
        public void OnGet()
        {
            Hospitals = _context.Hospitals.Select(x=> new Models.Hospital
            {
                Id = x.Id,
                Adress = x.Adress,
                City = x.City,
                HospitalName = x.HospitalName,               
                HealthcareProvidersCount = x.HealthCareProviders.Count,                
                PatientsCount = x.Patients.Count,
                Phone = x.Phone,
                State = x.State,
                Zip = x.Zip
            }).ToList();
        }
    }
}
