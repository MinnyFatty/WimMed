# WimMed
/Generate a ReadMe File for the solution based on below summary and design instructions:
///<summary>
Technical Requirements – Full-Stack Web Application

Tech stack:

Backend: ASP.NET Core (C#)
Frontend: Angular
Database: Any SQL-compliant option (e.g., PostgreSQL, MySQL, SQL Server)
Application functionality:

Patient Creation
Form must capture: Name, Surname, South African ID Number, Phone Number, and Date of Birth
Validate the SA ID number format
Patient Information Update
View/edit: Weight (kg), Height (cm), and Gender
Auto-calculate and display: BMI and Age
Patient Deletion
Allow full deletion of patient records
Patient List and Search
List all patients with Name, Surname, and ID Number
Search by Name and ID Number

Include at least 100 simulated patients in your database
Ensure the POC is optimised for Android tablet use
///</summary>
# WimMed - Full-Stack Web Application
WimMed is a full-stack web application designed to manage patient records efficiently. The application is built using ASP.NET Core for the backend, Angular for the frontend, and supports any SQL-compliant database such as PostgreSQL, MySQL, or SQL Server.
## Features
- **Patient Creation**: Capture essential patient details including Name, Surname, South African ID Number, Phone Number, and Date of Birth. The application validates the SA ID number format to ensure data integrity.
- **Patient Information Update**: Users can view and edit patient details such as Weight (kg), Height (cm), and BMI. The application automatically calculates and displays the BMI and Age based on the provided data.
- **Patient Deletion**: Full deletion of patient records is supported, allowing for complete removal of patient data when necessary.
- **Patient List and Search**: The application lists all patients with their Name, Surname, and ID Number. Users can search for patients by Name and ID Number to quickly find specific records.
- **Simulated Patients**: The database includes at least 100 simulated patients to demonstrate the application's functionality and performance.
- **Optimized for Android Tablets**: The application is designed to be user-friendly and optimized for use on Android tablets, ensuring a smooth user experience on mobile devices.
- **Technology Stack**:
  - **Backend**: ASP.NET Core (C#)
  - **Frontend**: Angular
  - **Database**: Any SQL-compliant option (e.g., PostgreSQL, MySQL, SQL Server)
	- Code Quality: The code is structured to follow best practices, ensuring maintainability and scalability.
		- Code-First Approach: The application is built with a code-first approach, allowing for easy modifications and updates to the database schema as needed.
		-Instructions for Setup and Usage:
		- 1. Clone the repository to your local machine.
			1. Install the required dependencies for both the backend and frontend.
			2. Configure the database connection string in the backend application settings in `appsettings.json`.
			3. Run the database migrations to set up the initial schema by executing the command `dotnet ef database update`.
			4. Start the backend server by running `dotnet run` in the backend project directory.
			5. Navigate to the frontend project directory and run `ng serve` to start the Angular application.
			6. Open your web browser and go to `http://localhost:4200` to access the application.
		- 2. Ensure that you have the .NET SDK and Node.js installed on your machine to run the application successfully.
		- 3. For Android tablet optimization, ensure that the application is responsive and test it on an Android tablet to verify usability.
- **Contributing**: Contributions are welcome! Please follow the standard Git workflow for submitting pull requests and issues.
		
			


