# Displaying data from database on the page
### This week we will be working on displaying data from database tables on pages of out Web application.

Let's Add new page to display hospitals:

Open your project in Vusual Studio.

1) Navigate to solution explorer -> Pages -> Add new folder for your first entity (I Added "Hospital" folder)

![image](https://github.com/user-attachments/assets/c42a1829-b990-429c-8d77-c56207c32d62)

2) Right-click on created folder -> Add -> Razor Page. Leave name of the page "Index.cshtlm"

3) After file is added we need to modify our cshtml.cs file: 

![image](https://github.com/user-attachments/assets/55c11d85-2ac8-41f2-ad8b-46b115358c7c)


 ```c#
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
            Hospitals = _context.Hospitals.ToList();
        }
    }
}
 ```
After all steps above our application is pulling hospitals fromm database and passing them to the web page. Let's modofy our chtml page to display list of hospitals:

1) Open Index.cshtml file from you folder:
   
![image](https://github.com/user-attachments/assets/20939d14-fc7e-4dea-8b2f-89a9b466bf9e)

2) Modify code of your web page to display data from myour model using my example:

```html
@page
@model BuildProjectSummer2024.Pages.Hospital.IndexModel
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
            </tr>
        }
    </tbody>
</table>
     ```
## Let's add link to our new page to the header for easy navigation:
In Solution Explorer go to Pages -> Shared -> _Layout.cshtml

![image](https://github.com/user-attachments/assets/ccaeeba5-2fc8-4acf-949c-a13bd2ccbe1d)

Find a place where link are defined and add a link to Your new page using example below:

![image](https://github.com/user-attachments/assets/7b1a6fb1-a09a-4f7c-ab89-ce8d0925e23c)

 ```html
  <li class="nav-item">
     <a class="nav-link text-dark" asp-area="" asp-page="/Hospital/Index">Hospitals</a>
 </li>
 ```

## Important!
### Please create minimum 3 pages using example of the page above (Ideally for all your instances)

In my case to cover all data from my database I would need to create pages for Hospitals, HealthCareProviders, Patients and Medical Claims.

Think what fields do You want to show and think of the order of the fields 

After all pages are added please test your application. Make sure all pages are visible and displaying data from your tables.
