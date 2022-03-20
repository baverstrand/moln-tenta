# EpiCalc  

[Try EpiCalc out!](https://epicalc.azurewebsites.net) You will find yourself in a purple rain :)   

## My app
### What it does
The application is a calculator. It takes two integers and an operator on the index page, and returns the resolved calculation and a list of 10 previous calculations ordered by date descending. Also the result is saved in the database.  
The application calls two different Azure functions depending on which operation is to be executed. 
 
### How it's structured
The whole application consists of an Aspnet Core Webb App with Razor pages, two Azure Functions, a CosmosDB (MongoDB), a Key Vault and Application Insights. 
All components are deployed to or created with Azure. I have also added a CI-pipeline in Github to ensure the code I push builds properly.  
Since the calculator only performs addition and subtraction I chose to present the alternatives to the user with radio buttons instead of opening up for mistakes by letting the user submitting the operator themselves. Also only integers are allowed - this mainly to avoid issues with cultures in punctuation but also to make it easier to do it right. If you try to submit a number which is not an integer you will get a message:  

![Message when trying to do wrong](/Documentation/intsonly.jpg)

When the input has passed the first obstacle (the Go! button) the data flows like this:  

![Dataflow](/Documentation/dataflow.jpg)

For logging Serilog is my weapon of choice. I have tried to log everything meaningful happening (for instance what I send into the database and what I get from it when asking). With a stop watch I can easily measure the time a certain operation takes and log that aswell. 

In Azure Key Vault I have put the Connection String for the database and the Instrumentation Key that Serilog needs to access Application Insights. 

### Other things worth mentioning
- Making things work with Azure Key Vault took the most time. Suddenly I got an exception when trying to fetch the Azure Insights Instrumentation Key for Serilog. It turned out a 0 (zero) was missing to indicate a level in appsettings.  
![Serilog issue](/Documentation/serilog.jpg)

- When I deployed the application I got a  "401 Unauthorized" when trying to call on my Azure Functions. This was an easy fix with changing the authorization level in the HttpTrigger. Not the safest way of doing it, but certainly the easiest.  

- The NuGet Package Microsoft.Aspnetcore.Http.Features had to be downgraded from 5.0.11 to 3.1.20 to get the Azure Functions to work. This was NOT an easy one to figure out!