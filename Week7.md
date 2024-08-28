# Working on Design. Polishing user experience.

## Let's add some colors! 

Main file that contains style for our application is located in "wwwroot" -> "css" folder: site.css.

Lets add some fance background to our appllication using gradient: 

Add next lines to ste.css file "body" section: 

```css
  body {
      margin-bottom: 60px;
      background: #6a11cb;
      /* NEW LINES ADDED BELOW*/
      background: linear-gradient(to top, rgba(106, 17, 203, 1), rgba(37, 117, 252, 1));
      color: white !important;
  }
```

To pick your own color you can use [colorpicker](https://rgbacolorpicker.com/);

Let's add some styles to our header menu: 

add style for "nav" element:

```css
    nav {
        background: linear-gradient(to top, rgba(37, 117, 252, 1), rgba(106, 17, 203, 1));
        color: white !important;
    }

```

to make link visible on such background let's make them white:

```css

    a {
        color: white !important;
    }

```

let's also add some color for our buttons: 

```css

    .btn-primary {
        background-color: orange !important;
        color: black !important;
    }

    .btn-secondary {
        background-color: yellow !important;
        color: black !important;
    }
```

Now let's switch to Patient -> index.cshtml page and add some styling for our table:

add new div around your table: 

```html
<div class="table-container">
<table class="table">
    <thead>
        <tr>
            <th>First Name</th>
            <th>Last Name</th>
            <th>DOB</th>
            <th>City</th>
            <th>Adress</th>
            <th>Phone</th>
            <th>Name of Hospital</th>
            <th># of Claims</th>
            <th>Action</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var patient in Model.Patients)
        {
            <tr>
                <td>@patient.FirstName</td>
                <td>@patient.LastName</td>
                <td>@patient.Dob</td>
                <td>@patient.City</td>
                <td>@patient.Adress</td>
                <td>@patient.Phone</td>
                <td>@patient.HospitalName</td>
                <td>
                    @patient.ClaimsCount
                </td>
                <td>
                    <a class="btn btn-secondary" asp-page="./Edit" asp-route-id="@patient.Id">Edit</a>
                </td>
            </tr>
        }
    </tbody>
       
</table>
    <div class="pagination">
        @if (Model.HasPreviousPage)
        {
            <a class="btn btn-primary m-1" asp-page="./Index"
               asp-route-hospitalId="@Request.Query["hospitalId"]"
               asp-route-pageIndex="@(Model.PageIndex - 1)">Previous</a>
        }

        @if (Model.HasNextPage)
        {
            <a class="btn btn-primary m-1" asp-page="./Index"
               asp-route-hospitalId="@Request.Query["hospitalId"]"
               asp-route-pageIndex="@(Model.PageIndex + 1)">Next</a>
        }
    </div>
</div>

```

after container div is added we can switch back to site.css file and add some style to it: 

```css
    .table-container {
        border: 5px white solid;
        border-radius: 10px;
    }
```

Add such containers for all your tables on all your pages.

Feel free to style your app the way you like. Make it look unique. use your favourite colors or images.

## Let's add some additional navigation buttons.

Let's add "Back buttons" to add/edit pages that will redirect users to pages with tables. Modify Patient-> Edit.cshtml add 

```html
<p>
    <a class="btn btn-secondary m-1" asp-page="./Index">Back</a>
</p>
```

![image](https://github.com/user-attachments/assets/1e0b1249-6d42-4dc9-8499-da4f1e90671d)

Now users would be able to navigate to patient page list using "Back" button. 

## Let's add some additional information for filtered pages.

Add hospital name to patients list pages when filtered by hospital: 

Modify Patient ->  Index.cshtml.cs and add "HospitalName property". Populate this property with value if page is filtered by hospital Id:

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

// THIS LINE IS ADDED
        public string  HospitalName { get; set; }

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

// THIS LINE IS ADDED
                HospitalName = _context.Hospitals.Find(hospitalId).HospitalName;
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
                HospitalName = x.Hospital.HospitalName,
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

open Patient -> Index.cshtml file and change h2 element from:

```html
<h2>Patient List </h2>
```

to

```html
@if (Model.HospitalName != null){
    <h2>Patient List for @Model.HospitalName</h2>}
    else{
    <h2>Patient List </h2>}
```

Now when we will navigate from hospital page then we will see the name of hospital on the page.

### Please think of additional adjustments that You can do to the style/user experience of your application. 
