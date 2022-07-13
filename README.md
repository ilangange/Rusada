
# Rusada Aviation

Below are the technologies used to develop the solution
1) Angular 14
2) ASP.NET CORE Web API
3) MSSQL Server
4) Entity FrameworkCore
5) NUnit for Unit Testing

## Steps to be followed
1) Clone repository
2) Change the connection string in the appsettings.json file
3) Database migration will automatically run the application start
4) Navigate to "Rusada.Aviation.WebUI" folder and open command promt and run the below command

    "npm install"
    "npm start"

5) Then the Angular application will be run on "http://localhost:4200/"
6) Open the solution file in Visual studio
7) Restore Nuget packages
8) Set "Rusada.Aviation.API" as the startup project
9) Run the WebAPI application

## Login process
Token-based authentication is implemented for the communication between the Angular application and the WebAPI. Below are the credentials to be used for logging in to the application.

username: rusada

password: rusada123