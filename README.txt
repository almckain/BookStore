ProjectName: BookStore
Respository Structure:

////DISCLAIMER////
The readme only elaborares on files that were generated or edited by the team. Files generated by Visual Studio or files that were created when the project was created will not be elaborated on. 
//////////////////

/Pages: Razor pages that make up the front-end of the web app. The shared folder also contains the cshtml for the header used on all razor pages.
/wwwroot: Static assets used by the web app
  /css: Stylesheets for the web application
  /images: Cover images for the books
/Models: Data models represnting the objects used for business logic and data access
/Services: Services used for business logic and data access. Each service is represented by an interface and file that implements that interface.
/ViewModels: Predetermined values stored for views, easier representation, and data access
/Helpers: Files used to extend or set session properties
Program.cs: Entry point for the applications. Sets all the interfaces.

Documentation:
- All Razor pages are paired with a C# files that contains the pages model and associated methods and properties.
- The wwwroot directory in /Models is used to style the webpages with css files. It also contains the images folder used to store book cover images.
- The models are used to structure the data exchanged between the services and the database.
- The services contains business logic and handles how files interact with the database. For each service there exists an interface that contains method signatures and a file that implements the method.
- The program.cs are responsible for being the applications entry point. setting up the interfaces, and launching ASP .NET Core application.

Database Documentation:
* This documentation covers database conections and how a method is connected to a view to display data from the database.
1. Create interface with method signature.
2. Create C# files that implements interface
3. Method used an abstract class called DatabaseService.cs, found in the services folder, to initialize the connect to the database.
4. use the method to call a procedure and return the data type.
5. In a razor page views cshtml.cs files create an instance of the interface.
6. Initialize the interfaces property through the OnGet method.
7. Call the method through the interface. Example, _exampleInterface.NewMethod();




























