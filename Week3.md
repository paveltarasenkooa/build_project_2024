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
