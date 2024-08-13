# Adding cross page reference
## Navigation from Hospitals page to Patients page with filtering of patients by Hospital.

Before start of work please make sure you have at least three pages with data displaying implemented. 

In my case I would use page for Hospitals and Patients. I will add link to hospiptals page that will redirect users to Patients page where Patients will be filtered by hospital that was selected.

### 1) Let's add extension model for Hospitals to store Patients Count.
   - Add new folder to "Models" folder and name it "ModelExtensions";
   - Add new class with name "Hospital.cs" - name of this file can be the same as Model that You already have in solution.
     Add new filed that You need to display on the page to the model:

```c#
using System;
using System.Collections.Generic;

namespace BuildProjectSummer2024.Models;

public partial class Hospital
{
    public int PatientsCount { get; set; } 
    public int HealthcareProvidersCount { get; set; }
}
 ```

In my case I added two additional fields - *PatientsCount* and *HealthcareProvidersCount*;

Please make sure that namespace for this class should be the same as namespace of your initial model.

### 2) Let's change "Index.cshtml.cs" file from Pages -> Hospital folder for these new fields to be populated:

```c#
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
```
Changes above are connected only to "OnGet" method.

### 3) Let's modify cshtml code of the page and add new column "# of Patients":

```html
   <table class="table">
    <thead>
        <tr>
            <th>Name</th>
            <th>State</th>
            <th>City</th>
            <th>Adress</th>
            <th>Phone</th>
            <th># of Patients</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var hospital in Model.Hospitals)
        {
            <tr>
                <td>@hospital.HospitalName</td>
                <td>@hospital.State</td>
                <td>@hospital.City</td>
                <td>@hospital.Adress</td>
                <td>@hospital.Phone</td>
                <td>
                    <a asp-page="/Patient/Index" asp-route-hospitalId="@hospital.Id">
                        @hospital.PatientsCount
                    </a>
                </td>
            </tr>
        }
    </tbody>
</table>
```
We have added link to patient page with "hospitalId" parameter. This will allow us to add filtering by this fields during patients extraction.

Below you can find code of my "Patient" page before modifications and filters: 

ccshtml code: 

```html
@page
@model BuildProjectSummer2024.Pages.Patient.IndexModel
@{
    // title that will be displayed on the top of the page
    ViewData["Title"] = "List of hospitals";
}

<h2>Hospitals List</h2>

<table class="table">
    <thead>
        <tr>
            <th>Name</th>
            <th>State</th>
            <th>City</th>
            <th>Adress</th>
            <th>Phone</th>
            <th>Name of Hospital</th>
            <th># of Claims</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var patient in Model.Patients)
        {
            <tr>
                <td>@patient.FirstName</td>
                <td>@patient.LastName</td>
                <td>@patient.City</td>
                <td>@patient.Adress</td>
                <td>@patient.Phone</td>
                <td>@patient.Hospital.HospitalName</td>
                <td>
                    @patient.ClaimsCount
                </td>
            </tr>
        }
    </tbody>
</table>
```

c# code: 

```c#
using BuildProjectSummer2024.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BuildProjectSummer2024.Pages.Patient
{
    public class IndexModel : PageModel
    {
        private readonly BuildProject2024Context _context;
        public List<Models.Patient> Patients;        

        public IndexModel(BuildProject2024Context context)
        {
            _context = context;
        }

        public void OnGet( )
        {
            Patients = _context.Patients.Select(x => new Models.Patient
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
            }).ToList();
        }
    }
}
```

### 4) Let's add filtering by hospital Id:

This is updated "OnGet' method for Patients Page:

```c#
 public void OnGet(int? hospitalId  )
 {
     Patients = _context.Patients.Where(x=> !hospitalId.HasValue || x.HospitalId == hospitalId).Select(x => new Models.Patient
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
     }).ToList();
 }
```

