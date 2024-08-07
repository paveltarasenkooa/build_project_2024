
# Week 2. Db Creation
### On this week You should have MS SQL Server and MS SQL Managment Studio installed on your computers.
### Let's connect to your database server and create database

✅ **Connect to database server:**
-  In MS SQL Managment Studio app go to File -> Connect
-  put **localhost** as Server Name and hit connect

✅ **Create Database**
- Right-click of mouse on "Databases" folder -> "New Database.."
- Put name for your database. My database would have name "BUILD_PROJECT_2024"
- Now You have Your database ready! That was easy :-)

 ## Lest's come up with our database structure

 Let's assume that we need to store data about next objects:
  - **Hospital**
    - Hospital Name
    - Hospital Adress
    - City
    - State
    - Zip
    - Phone
  - **Health Care Provider**
    - First name
    - Last name
    - DOB
    - Gender
    - NPI
    - Adress
    - City
    - State
    - Zip
    - Phone
    - Hospital of practice
    - Role
  - **Patient**
    - First name
    - Last name
    - DOB
    - Gender
    - MRN
    - SSN
    - Insurance ID
    - Hospital
  - **Medical Claim**
    - Patient
    - Health Care Provider
    - NDC (National drug code)
    - Quantity
    - Total Amount
    - Days Supply
    - Prescriton number
    - Date Filled
You can add any additional columns that you think make sense.
### Fist let's get familiar with T-SQL commands that we would need to Create database structure:
 - [SQL CREATE TABLE Statement](https://www.w3schools.com/sql/sql_create_table.asp)
 - [SQL PRIMARY KEY Constraint](https://www.w3schools.com/sql/sql_primarykey.asp)
 - [Auto increment colum](https://www.w3schools.com/sql/sql_autoincrement.asp) - please see "Syntax for SQL Server"
 - [SQL FOREIGN KEY Constraint](https://www.w3schools.com/sql/sql_foreignkey.asp)
### We would also need to get familiar with SQL Server Datatypes:
 - [SQL Server Data Types](https://www.w3schools.com/sql/sql_datatypes.asp)

And now we are ready to create our first table! 
### let's create table for hospital: 
 ```sql
CREATE TABLE Hospital (
    Id int NOT NULL IDENTITY(1,1),
    HospitalName varchar(255) NOT NULL,
    Adress varchar(255),
    City varchar(255),
    State varchar(50),
    ZIP  varchar(50),
    Phone  varchar(50),
    CONSTRAINT PK_Hospital PRIMARY KEY (ID)
);
```
Hospital table does not have references to any other tables in our database. That's why it's the best candidate for a table to be created first.
Now let's create table for Health Care Provider. This table would have reference to Hospital table. 
 ```sql
CREATE TABLE HealthCareProvider (
    Id int NOT NULL IDENTITY(1,1),
    FirstName varchar(255) NOT NULL,
    LastName varchar(255) NOT NULL,
    Gender  varchar(10)  NULL,
    DOB  DATE  NULL,
    Adress varchar(255),
    City varchar(255),
    State varchar(50),
    ZIP  varchar(50),
    Phone  varchar(50),
    NPI  varchar(50),
	  HospitalId INT NOT NULL,
    CONSTRAINT PK_HealthCareProvider PRIMARY KEY (ID)
);
```
Here column "HospitalId" serve as connection between HealthCareProvider and Hospital tables. But there is no constraint yet created.
lets create constraint:

 ```sql
ALTER TABLE HealthCareProvider
ADD CONSTRAINT FK_HealthCareProvider_Hospital
FOREIGN KEY (HospitalId) REFERENCES Hospital(Id);
```
Now let's add role column to the HealthCareProvider table.
But before we do this let's add "enum" table for HealthCareProvider role. And then let's add a reference to this table to the HealthCareProvider table:
```sql
CREATE TABLE HealthCareProviderRole (
    Id int NOT NULL IDENTITY(1,1),
    RoleName varchar(100),
	  CONSTRAINT PK_HealthCareProviderRole PRIMARY KEY (ID)
);

ALTER TABLE HealthCareProvider
ADD RoleId INT NOT NULL

ALTER TABLE HealthCareProvider
ADD CONSTRAINT FK_HealthCareProvider_Role
FOREIGN KEY (RoleId) REFERENCES HealthCareProviderRole(Id);
```
I want You to create other tables by Your own using code snippets above as an example.

Let's add next connections for your tables:

 - HealthCareProvider (HospitalId) -> Hospital

 - HealthCareProvider (RoleId) -> HealthCareProviderRole

 - Patient (HospitalId) -> Hospital

 - MedicalClaim (PatientId) -> Patient

 - MedicalClaim (HealthCareProviderId) -> HealthCareProvider

After all tables are created please publish Your code to Your git repository into separate .sql file. 
To get code of Your database after all the tables are creates do the next from your SQL Server Managment studio:
![image](https://github.com/user-attachments/assets/f938139f-f597-40af-bc4f-2f43a44b5dbf)

Right-click on your Db -> Tasks ->  Generate Scripts.

Then hit "Next" on the first page -> Select "Script Entire Database and all database objects" -> One next step select whete to save the script (I usually recoment to save script to the file). Then hit next on all other windows and after file is created put it to Your repo.
Easy! ;-) 

Hope You will enjoy and learn something new! :-)
See You on our next session!

