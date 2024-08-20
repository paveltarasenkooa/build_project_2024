# Adding an Add/Edit Patient Page

## Create PatientModel

### Let's change our approach of using extension modelt to separate Class for our models.

Let's create PatientModel.cs file:

1) Create new folder "PageModels" in "Models" folder;
   
   ![image](https://github.com/user-attachments/assets/3d50d8cf-303b-40e5-b9b9-393646b5465b)

2) Add new .cs file with name "PatientModel".

   Here is the code for "PatientModel.cs"
```c#
namespace BuildProjectSummer2024.Models.PageModels
{
    public class PatientModel
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
        public string? HospitalName { get; set; }

        public string? InsuranceMemberId { get; set; }
        public int? ClaimsCount { get; set; }

    }
}
 ```

As you can see out model have all the fields from Patient class except complex properties and collections. We can add any properties that we need here without having problems with the database connections.
We will use this class to store data about our patient.

let's chage our Patient/Index page to be using "PatientModel" class:

"Index.cshtml.cs":

  ```c#
using BuildProjectSummer2024.Models;
using BuildProjectSummer2024.Models.PageModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BuildProjectSummer2024.Pages.Patient
{
    public class IndexModel : PageModel
    {
        private readonly BuildProject2024Context _context;
        public List<PatientModel> Patients;
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

            Patients = patientsQuery.Where(x=> !hospitalId.HasValue || x.HospitalId == hospitalId).Select(x => new PatientModel 
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
                HospitalName =  x.Hospital.HospitalName,
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
```

Basically what we did is just changed usage of Models.Patient to PatientModel class.

### We are ready to add new page for Patient "Add/Edit:"

1) Add new Razor Page to Pages/Patient folder:
   
     Right click on "Patient" folder -> Add -> Razor Page.

     Add empty page and call it "Edit"

![image](https://github.com/user-attachments/assets/0ea70e8e-dbec-4cd6-bd4c-77cfb7564029)

2) let's add code to out c# file for our new page:

```c#
using BuildProjectSummer2024.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BuildProjectSummer2024.Models.PageModels;
using System.Numerics;
using System.Reflection;
using System.Runtime.Intrinsics.X86;

namespace BuildProjectSummer2024.Pages.Patient
{
    public class EditModel : PageModel
    {
        private readonly BuildProject2024Context _context;

        public EditModel(BuildProject2024Context context)
        {
            _context = context;
        }

        [BindProperty]
        public PatientModel Patient { get; set; }
        public SelectList Hospitals { get; set; }
        public void OnGet(int? id)
        {
            if (id == null)
            {
                Patient = new PatientModel();
            }
            else
            {
                var dbPatient = _context.Patients.Find(id);

                if (dbPatient == null)
                {
                    Patient = new PatientModel();
                }
                else
                {
                    Patient = new PatientModel
                    {
                        FirstName = dbPatient.FirstName,
                        LastName = dbPatient.LastName,
                        Dob = dbPatient.Dob,
                        Gender = dbPatient.Gender,
                        Id = dbPatient.Id,
                        HospitalId = dbPatient.HospitalId,
                        Adress = dbPatient.Adress,
                        City = dbPatient.City,
                        InsuranceMemberId = dbPatient.InsuranceMemberId,
                        Mrn = dbPatient.Mrn,
                        Phone = dbPatient.Phone,
                        Ssn = dbPatient.Ssn,
                        State = dbPatient.State,
                        Zip = dbPatient.Zip
                    };
                }
            }

            var HospitalsList = _context.Hospitals.Select(x => new Models.Hospital { Id = x.Id, HospitalName = x.HospitalName });
                Hospitals = new SelectList(HospitalsList, "Id", "HospitalName");

           
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Patient.Id == 0)
            {
                var dbPatient = new Models.Patient
                {
                    FirstName = Patient.FirstName,
                    LastName =  Patient.LastName,
                    Dob = Patient.Dob,
                    Gender      = Patient.Gender,
                    InsuranceMemberId = Patient.InsuranceMemberId,
                    Zip = Patient.Zip,
                    Adress = Patient.Adress,
                    City = Patient.City,
                    HospitalId = Patient.HospitalId,
                    Mrn = Patient.Mrn,
                    Phone = Patient.Phone,
                    Ssn = Patient.Ssn,
                    State = Patient.State
                    
                };
                _context.Patients.Add(dbPatient);
            }
            else
            {
                var dbPatient = _context.Patients.Find(Patient.Id);

                dbPatient.FirstName = Patient.FirstName;
                dbPatient.LastName = Patient.LastName;
                dbPatient.Dob = Patient.Dob;
                dbPatient.Gender = Patient.Gender;
                dbPatient.InsuranceMemberId = Patient.InsuranceMemberId;
                dbPatient.Zip = Patient.Zip;
                dbPatient.Adress = Patient.Adress;
                dbPatient.City = Patient.City;
                dbPatient.HospitalId = Patient.HospitalId;
                dbPatient.Mrn = Patient.Mrn;
                dbPatient.Phone = Patient.Phone;
                dbPatient.Ssn = Patient.Ssn;
                dbPatient.State = Patient.State;
                _context.Patients.Update(dbPatient);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index"); // Adjust the redirection as per your routing setup
        }
    }
}
```
   

