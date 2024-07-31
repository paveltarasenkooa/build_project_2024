# Database data populating. Sceleton of an application
## Populating database with data

### [Insert](https://www.w3schools.com/sql/sql_insert.asp) Statement
Insert statement is useful to insert small amounts of data quickly 
Example of insert statement for Hospital table:
 ```sql
INSERT INTO [dbo].[Hospital]
           ([HospitalName]
           ,[Adress]
           ,[City]
           ,[State]
           ,[ZIP]
           ,[Phone])
     VALUES
           ('RWJBH Barnabas'
           ,'100 Net Street'
           ,'President City'
           ,'NY'
           ,'06025'
           ,'+12568871235')
 ```
To populate table with bigger amounts of data you can use import wizard for MS SQL Managment studio: 
(if you don't have import wizard download and install  [Download SQL Server Data Tools (SSDT)](https://learn.microsoft.com/en-us/sql/ssdt/download-sql-server-data-tools-ssdt?view=sql-server-ver16).)
Let's populate patients table with patiens from csv file (coma delimited text file)
Right click on your database -> Tasks -> Import data. Hit next on first window and select Flat File Source 
select your file and check "Column Names in first data row" checkbox :
![image](https://github.com/user-attachments/assets/11b0c32b-9f13-4600-86e1-ff222a1a18ab)

On destination screen select Sql server native client and select your server name in "Server name" dropdown

![image](https://github.com/user-attachments/assets/b8cd9c89-c01d-45ac-9660-a5c4cc8a6966)

on next window select your table in destination column:

![image](https://github.com/user-attachments/assets/6259a559-aa7e-4b4d-8469-2e08dd6886f3)

Hit on "Edit mappings" to check whether all your columns mapped correctly

![image](https://github.com/user-attachments/assets/3ab2bede-1f4d-4045-a5d3-966ff1afffbd)

hit next 
select "run imiddiately" and hit next. 
Now your table should be populated with records.
Please populate your database with 5 hospitals, 10 Healthcare providers, 20 patients and 50 medical claims
## Creating a sceleton of our application

### Download Visual Studio
- Go to the [Visual Studio Downloads](https://visualstudio.microsoft.com/downloads/) page and download the Community Edition, which is free for individual developers, open source projects, academic research, and classrooms.

### Install Visual Studio
- Run the installer and select the **.NET Core cross-platform development** workload, which includes all necessary components for ASP.NET Core development.

## 2. Create a New ASP.NET Core Web Application using Visual Studio

- **Launch Visual Studio** and select **Create a new project**.
- **Search for ASP.NET Core Web Application**, select it, and click **Next**.
- **Configure your project**:
  - Enter a **Project Name** and location.
  - Click **Create**.
- **Select the project type**:
  - Choose **Web Application (Model-View-Controller)** for an MVC project.
  - Make sure **.NET Core** and **ASP.NET Core 8.0** (or your current version) are selected.
  - Click **Create**.
  
## 3. Prepare the Database

Ensure you have access to your existing database and its connection string ready for the next steps.

## 4. Scaffold the DbContext and Models

### Add Required NuGet Packages for Entity Framework Core
Before scaffolding the DbContext and models, make sure to add the necessary NuGet packages to your project. The specific packages depend on your database provider (e.g., SQL Server, PostgreSQL). For a SQL Server database, you will typically need:

- **Entity Framework Core Design**: This package is required for the scaffolding process.
- **Entity Framework Core SQL Server Provider**: This package allows your application to communicate with SQL Server.
To add these packages, use the Package Manager Console in Visual Studio with the following commands:

 ```powershell

Install-Package Microsoft.EntityFrameworkCore.Design
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.Tools 
```
Replace Microsoft.EntityFrameworkCore.SqlServer with the corresponding package for your database provider if you are not using SQL Server.

### Install Entity Framework Core Tools
Open the Package Manager Console by going to **Tools > NuGet Package Manager > Package Manager Console.**
Run the following command to install Entity Framework Core tools:

```powershell
dotnet tool install --global dotnet-ef
```

Visual Studio simplifies the process of scaffolding your database context and models using the Package Manager Console.

- **Open Package Manager Console**: Go to **Tools > NuGet Package Manager > Package Manager Console**.
- **Run Scaffold-DbContext Command**: Execute the following command, replacing placeholders with your actual data:
  ```powershell
  Scaffold-DbContext "YourConnectionString" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
  ```
  example of my connection string : Data Source=LAPTOP-ML8R4NJG;Initial Catalog=BUILD_PROJECT_2024;Integrated Security=True;Encrypt=True;TrustServerCertificate=True
  This command generates the DbContext and entity models based on your database schema.

If you are getting error doing this from Visual Studio you can try to do next:

  - Open your terminal or command prompt.

  - Navigate to the root directory of your project.

  - Run the following command to scaffold the DbContext and entity classes:

 ```powershell
    dotnet ef dbcontext scaffold "YourConnectionString" Microsoft.EntityFrameworkCore.SqlServer -o Models
 ```

## 5. Add the DbContext to the Services Container


- **Add Connection String to `appsettings.json`**
     ```json
    "ConnectionStrings": {
   "DefaultConnection": "Data Source=LAPTOP-ML8R4NJG;Initial Catalog=BUILD_PROJECT_2024;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"
 }
    ```
- **Open `Startup.cs` or `Program.cs`** (depending on your project template version) to configure the services.

- **Register DbContext**:
  - For `Startup.cs`, locate the `ConfigureServices` method and add:
    ```csharp
    services.AddDbContext<YourDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("YourConnectionStringName")));
    ```
  - For `Program.cs` in .NET 6 or later, add:
    ```csharp
    builder.Services.AddDbContext<YourDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("YourConnectionStringName")));
    ```


## 6. Add a Controller and View

- **Right-click on the Controllers folder** in Solution Explorer, select **Add > Controller**.
- Choose **MVC Controller - Empty**, click **Add**.
- Name your controller (e.g., `MyDataController`) and click **Add**.
- Register dbContext in your controller
   ```csharp
  private readonly YourDbContext _context;

  public MyDataController(YourDbContext context)
  {
      _context = context;
  }
    ```


### Add a View

- Right-click inside an action method in the controller and select **Add View**.
- Name the view (matching the action method name), ensure the template is **Empty (without model)**, and click **Add**.
- **Update the View** to display data, using Razor syntax to iterate over the model passed from the controller.

## 7. Run Your Application

- Click the **IIS Express** button in Visual Studio's toolbar to build and run your application.
- Visual Studio will open your default web browser and navigate to your application's home page.

By following these instructions, you've leveraged Visual Studio Community Edition to create, develop, and run a .NET Core ASP.NET MVC application using the Database-First approach. Visual Studio provides a comprehensive development environment with powerful features for managing databases, editing code, debugging, and more, enhancing your productivity as a developer.


