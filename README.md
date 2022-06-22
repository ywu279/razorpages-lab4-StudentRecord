# razorpages-lab4-StudentRecord
To create a ASP.NET Razor Pages web application that connects with the database.
<*CST8256 Web Programming Languages I - lab 4*>

## Built with
- ASP.NET [Razor Pages](https://docs.microsoft.com/en-us/aspnet/core/razor-pages/?view=aspnetcore-6.0&tabs=visual-studio) Web Application(.NET framework 6.0)
- Microsoft Entity Framework Core
- StudentRecoreDbBuilder.sql 

## Objectives
1. Use Visual Studio SQL Server Object Explorer to manage SQL Server databases
2. Use **Entity Framework Core** to generate **entity classes** and DB **Context class**.
3. Generate CRUD (Create, Read, Update, Delete) data accessing Razor pages to an entity class

## Get started
1. Download Visual Studio 2022
2. Configure and Manage SQL Server with Visual Studio: View > SQL Server Object Explorer > Add SQL Server > Connect with **MSSQLLocalDB** (a version of SQL Server for use by developers)
3. Create a new database **StudentRecord**. Run **StudentRecoreDbBuilder.sql** script to create tables shown below.
   <br><img src="https://user-images.githubusercontent.com/58931129/174700152-021be978-b085-4135-8198-0503a7bdb7c6.png" width=20%>
   <br>Only data in **AcademicRecord**, **Course**, **Student** tables will be used in this lab.
4. Install the following Microsoft Entity Framework (a set of class libraries available on NuGet public domain) to the project:
    - Microsoft.EntityFrameworkCore.SqlServer (6.0.6)
    - Microsoft.EntityFrameworkCore.Tools (6.0.6)
    - Microsoft.VisualStudio.Web.CodeGeneration.Design (6.0.6) 
    <br>After installation, you should see these 3 packages under the project's Dependencies.
5. Use a Entity Framework tool called **Scaffold-DbContext** to automatically generate entity classes from tables and a DB Context class (for accessing the database). 
   <br>Create a folder **DataAccess** in project to contain the generated data access code
   <br>Tools > NuGet Package Manager > Package Manager Console > run the following command:
   ```
   Scaffold-DbContext "Server=(localdb)\mssqllocaldb;Database=StudentRecord;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir DataAccess
   ```
   <br><img src="https://user-images.githubusercontent.com/58931129/174702154-88f7bb98-e424-4473-af69-59e06ab25db8.png" width=20%>
6. In **StudentRecordContext.cs**, remove #warining and the hardcoded DB Connection string inside `optionsBuilder.UseSqlServer()`
   <br><img src="https://user-images.githubusercontent.com/58931129/174702919-5d0b6a79-1849-48c5-a2e9-ecba6535ddfc.png" width=70%>
   <br>In **appsetting.json**, add `ConnectionStrings` section with one connection string `StudentRecord` to the file as shown below:
   
   ```
     "ConnectionStrings": {
     "StudentRecord": "Server=(localdb)\\mssqllocaldb;Database=StudentRecord;Trusted_Connection=True;"
     }
   ```
 7. In **Program.cs**, Register the DB Access Service with the application 
    ```
    using Microsoft.EntityFrameworkCore;
    using Lab4.DataAccess;

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddRazorPages();

    // Register the DB Access Service with the Application
    string dbConnStr = builder.Configuration.GetConnectionString("StudentRecord");
    builder.Services.AddDbContext<StudentRecordContext>(options => options.UseSqlServer(dbConnStr));
    ```
    Rebuild Lab4 solution

8. Create three folders **AcademicRecordManagement**, **CourseManagement**, **StudentManagement**. Add Razor pages using Entity Framework (CRUD) under each folder. For example, Visual Studio will automatically generate Razor pages(Create, Delete, Details, Edit, Index) inside CourseManagement folder for fully functional CRUD accessing of database’s Course table.
   <br><img src="https://user-images.githubusercontent.com/58931129/174705667-9f38e3ab-f3f0-439d-9e1a-331cc5461865.png" width=20%>

## Features
### 1. Modify the generated StudentManagement/Index page 
- Show the number of courses and the average grade for each student. 
- Sorting the students based on the column header. 
- Remove the Edit links.
  <br><img src="https://user-images.githubusercontent.com/58931129/174706149-88d58133-cbdc-48c4-931b-fe084faff010.png" width=50%>
- Remove Delete.cshtml to avoid unnecessary roundtrips to the server-side, the delete confirmation prompts should happen at the client-side.
  <br>***Note***: You must delete the associated academic records first, then delete the student. Otherwise, a DB referential integrity error will happen. 
  <br><img src="https://user-images.githubusercontent.com/58931129/174706543-ba91bc93-c422-44fa-8b67-9cb4b3fee117.png" width=50%>
  

### 2. Modify the generated StudentManagement/Detail page
- Show more details of each student - academic records
  <br><img src="https://user-images.githubusercontent.com/58931129/174707115-12d0d87f-a238-4e9a-92d7-c75a5089e3df.png" width=50%>
 
### 3. Modify the generated StudentManagement/Create page
- Validation - show an error message if the student Id already exists in the database's Student table
  <br><img src="https://user-images.githubusercontent.com/58931129/174707572-6e0add35-550c-43f8-af5b-d0809c9caa30.png" width=50%>

## Acknowledgements
A list of recourses I found helpful to learn Razor Pages:
- [Razor Pages with Entity Framework Core in ASP.NET Core - Tutorial 1 of 8](https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/intro?view=aspnetcore-6.0&tabs=visual-studio)
- [Razor Pages for ASP.NET Core(.NET 6) - freeCodeCamp.org](https://youtu.be/eru2emiqow0)

## Disclaimer
This project is to be used for personal and educational purposes only.
