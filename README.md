# Getting Started with Simple Booking System

This project was created for the purpose of ProOffice tehcnical interview according to requirements in 

Prooffice interview technical evaluation task 2022.docx

# Starting the appilication

Simply open and run the included solution in VS 2022

# Technical implementation

The application is composed of:

 - React SPA in 'SimpleBookingSystem\ClientApp' for the UI 
   
 - ASP. NET Core Web API, Service layed and Data Access Layer in the SimpleBookingSystem project

 - SQlite database in the same folder

 In order to keep it simple due to time constrains I have decided to keep all logic under one project

# React SPA

There is no need to start the React app separately, the bootsraping in Startup.cs alredy starts the React development server for you :-)

If you wish to do so (ex. VS Code or from cmd), make sure to make appropriate adjustment to CORS settings on the API in the Startup class, otherwise you will not react the API !

# ASP. NET Core Web API

Simple api with 2 controllers 1 method each. 

To test the api standalone, navigate to http://localhost:20666/swagger/index.html

# Backend

The api gets requests, decides if request is valid (to keep it simple request in this case is same as command) than makes a command to be handled in middleware using a meditor pattern ( Booking ) or straigt away calls a service and gets data from db (Resources).

Under Business folder you will find Commands, Handlers, Services, Validation, Models and Extensions implementing simplified backend design patterns.

Some unit tests are invcluded in Tests

# Database

The data access layer is composed of a Sqlite database, EF Core, context and Entities

The context itsellf implements a unit of work pattern in this simple scenario so there was no need another repository abstraction layer.

Migrations are used to prepopulate database with mock data on each project startup.

# Troubleshooting 

The project uses .Net Core 3.1 so make sure you have it installed.

Running the project should install all packages, dependencies and also run npm install before starting the React server.

In case any one of these does not happen try 'Restore Nuget Packages' from the solution and manually running npm install from cmd in ClientApp root.

Feel free to contact me for further questions.

