# ContactManagementsystem

Employee Contact Management System

Overview
The system allows users to view, add, edit, and search employee contacts, assign them to companies, and validate emails using a third-party API.
Technology Stack
- Frontend: React, TypeScript, Vite, Material UI
- Backend: ASP.NET Core Web API (.NET 8)
- Database: SQL Server with Entity Framework Core
- Email Validation: AbstractAPI
- State Management: Context API
- Form Validation: Yup & Data Annotations
- Axios for API requests
- Swagger for API documentation

Features
Frontend:
- Employee list with search & pagination
- Add/Edit forms with validation
- Company dropdown
- Email validation with API
- Phone input masking (Canada format)
- Global notifications and theme

Backend:
- CRUD operations for employees
- DTOs for input/output
-Entity Framework Core (Code-First)
-Separate logic layers for maintainability
- Proper validation and error handling
- Swagger documentation
Database:
- Normalized schema (Employees & Companies)
- Data of employees and companies present in github repository
Setup Instructions
1. Clone the repository
2. Setup Backend:
-if dotnet is not installed then https://dotnet.microsoft.com/download
   	- cd ContactManagementSystem-master\backend
   	- dotnet restore
 - dotnet ef database update (if EF is already not installed dotnet tool install --global       dotnet-ef)
   	- dotnet run --project ContactManagementAPIs/ContactManagementAPIs.csproj
3. Setup Frontend:
   	- cd ContactManagementSystem-master\frontend
	If node is not install then visit https://nodejs.org/en/download
   	- npm install
   	- npm run dev
4. Database Setup Instructions
1.	Open SQL Server Management Studio (SSMS) Make sure you're connected to your local SQL Server instance.
2.	Create the Database Run the script db/create_database.sql to create the EmployeeContactDB database.
3.	Create Tables Run db/create_tables.sql to create the Companies and Employees tables.
4.	Feed Data Run db/insert_records.sql to insert sample records into the tables.
Important Notes = These scripts are designed to run in SQL Server Management Studio (SSMS).

Third-Party API Integration
I integrated email validation using AbstractAPI during the employee form submission.
I have stored the the key inside .env file in client/frontend directory
VITE_ABSTRACT_API_KEY=your_abstract_api_key_here
Before saving, the app sends the email to AbstractAPI and checks the deliverability status.
If AbstractAPI responds with UNDELIVERABLE, I block the form and show a user-friendly error.
However, AbstractAPI sometimes returns DELIVERABLE even for fake-looking emails (like outlookk.com) because its free tier only does domain-level checks — not mailbox-level verification.
So, if AbstractAPI returns DELIVERABLE, the app proceeds with submission — which is expected behavior based on the API's response.
Example : https://emailvalidation.abstractapi.com/v1/?api_key=xxx&email=shoaib.malick009876@outlookk.com
Abstract API Response = {"email": "shoaib.malick009876@outlookk.com",
"autocorrect": "shoaib.malick009876@outlook.com",
"deliverability": "DELIVERABLE",
"quality_score": "0.80",
"is_valid_format": {
"value": true,
"text": "TRUE"},
"text": "TRUE"},




