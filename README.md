# CakeFactoryProd

This is a sample marketplace/content management application for a fictional cake bakery - Cake Factory.

Cake Factory was built in C# using ASP.NET Core with Razor Pages. The application allows users to use the site in one of two ways:
- Guests can order different, customizable cakes, checkout using Paypal, and view past orders.
- Administrators can add new products to the site, perform CRUD operations on cake properties, and view/edit incoming orders.

## Technologies Used

  - C#
  - ASP.NET Core
  - Razor Pages
  - Entity Framework Core
  - Microsoft SQL Server Management Studio (MSSMS)
    
## Features

  - Users can perform CRUD operations on database items
  - PayPal integration
  - Email verification
  - ReCAPTCHA integration
  - Fully customizable cake ordering
  
## Installation

  1. Clone the repository: <b>`git clone https://github.com/ceilidh-ashcroft-bcit/CakeFactory/`</b>.
  2. Open the project in <a href="https://visualstudio.microsoft.com">Visual Studio</a> and install the required packages.
  3. Create a database server using <a href= "https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16">Microsoft SQL Server Management Studio</a>.
  4. Add your database connection string in user secrets.
  5. Delete the `Migrations` folder from the project directory.
  6. In NuGet Package Manager console run: 
      - <b>`add-migration InitialCreate`</b>
      - <b>`update-database`</b>
  7. Change lines 310 and 315 of the seed script located at `<project_DIR>/DB/cake-factory_db-script.sql` to point to your project directory.
       e.g. `BULK N'K:\<project_DIR>\CakeFactory\CakeFactoryProd\wwwroot\images\chocolateCake.jpg`  
  8. Run the seed script in MSSMS.
  9. Build and run the application in Visual Studio.
  
  ## Usage
  
To create the first admin user, first register a new user from the application, then in SSMS:
    
      SELECT * FROM AspNetUsers;
 
      -- Copy the user id from the desired user.

      INSERT INTO AspNetUserRoles VALUES ('<YOUR_USER_ID>', 1);
  
Once signed in as an Admin, you can user the 'User Roles' dashboard to set other users' roles.


***DO NOT INPUT REAL PAYPAL INFORMATION IN THE CHECKOUT SCREEN***

 PayPal testing accounts can be created <a href="https://developer.paypal.com">here.</a>
