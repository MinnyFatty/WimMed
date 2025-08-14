# WimMed

## Overview
WimMed is a full-stack web application for efficient patient record management, optimized for both desktop and Android tablet use. The backend is built with ASP.NET Core (.NET 8), the frontend with Angular, and it supports any SQL-compliant database (e.g., SQL Server, PostgreSQL, MySQL).

---

## Features

### User Experience
- **Minimal Navigation:** The sidebar contains only the Dashboard for a focused experience. The top bar is minimal.
- **Patient List:** The dashboard displays a searchable, sortable list of patients. Use the search bar to filter by name, ID, or other fields.
- **View & Edit Patient Info:** Expand a patient’s details to view Age, Height, Weight, BMI, and BMI Status. Edit Age, Height, and Weight via a modal dialog. BMI and BMI Status are recalculated automatically.
- **Delete Patients:** Remove a patient and all related info with a confirmation dialog to prevent accidental deletion.
- **Error Handling:** All errors (e.g., invalid data, API failures) are shown in a clear, user-friendly alert box.

### Technical
- **Patient Creation:** Capture Name, Surname, South African ID Number, Phone Number, and Date of Birth. SA ID number is validated.
- **Patient Information Update:** Edit Weight (kg), Height (cm), and Age. BMI and Age are auto-calculated.
- **Patient Deletion:** Full deletion of patient records and related info.
- **Patient List and Search:** List all patients with Name, Surname, and ID Number. Search by Name, Surname, or ID Number.
- **Simulated Patients:** The database is seeded with at least 100 simulated patients.
- **Optimized for Android Tablets:** Responsive design and tested for usability on Android tablets.

---

## API Endpoints

| Action                        | Endpoint                                                        | Method |
|-------------------------------|-----------------------------------------------------------------|--------|
| Get All Patients              | `/api/Patients/all`                                             | GET    |
| Search Patients               | `/api/Patients/search?query={searchString}`                     | GET    |
| Get Patient Info by PatientId | `/api/PatientInfos/GetPatientInfoByPatientId/{PatientId}`       | GET    |
| Edit Patient                  | `/api/Patients/EditPatient/{PatientId}`                         | PUT    |
| Edit Patient Info             | `/api/PatientInfos/EditPatientInfo/{PatientInfoId}`             | PUT    |
| Delete Patient                | `/api/Patients/DeletePatient/{PatientId}`                       | DELETE |

All API errors are returned in a user-friendly format and displayed in the UI.

---

## Setup Instructions

### Backend (ASP.NET Core)
1. Clone the repository.
2. Configure the database connection string in `appsettings.json`.
3. Run database migrations:
4. Start the backend server:

### Frontend (Angular)
1. Navigate to the frontend project directory.
2. Install dependencies:
3. Start the Angular application:
4. Open your browser to `http://localhost:4200`.
5. Ensure the API URL in `src/environments/environment.ts` matches your backend server URL.

### Requirements
- .NET 8 SDK
- Node.js and npm
- SQL-compliant database (SQL Server, PostgreSQL, MySQL, etc.)

---

## Screenshots

See the [WimMed Admin User Manual with Screenshots](UI/wimmed-admin.web/WimMed_Admin_User_Manual_with_Screenshots.md) for detailed UI walkthroughs and images.

---

## Contributing

Contributions are welcome! Please use standard GitHub workflow for issues and pull requests.

---

*For further support, contact your WimMed system administrator.*


