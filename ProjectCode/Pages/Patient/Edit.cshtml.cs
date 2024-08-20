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
