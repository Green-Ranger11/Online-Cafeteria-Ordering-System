# Cafeteria Ordering System

This is a online food ordering system where you can order your meals online. This is for CS415 at the University of the South Pacific. This was built in .Net Core and Angular v9.

## Getting Started

### Prerequisites

* .NET Core Sdk v3.0 or greater <br />
* POSTMAN - For testing suites for the API  <br />
* SQL Server 2016 or greater <br />
 Note: This application was initially built on SQLite and configurations have already been made to take care of SQLite's drawbacks should you 
 choose to use SQLite as your database <br />
* Node.js v10 or greater <br />
* Angular CLI v9 or greater <br />
* Stripe CLI <br />
* Redis 64Bit - Probably want to use Chocolatey to install otherwise its a headache <br />
* Code Editor or IDE like VS Code, VS Studio or Rider <br />

### Installing

1. Install .NET Core Sdk and Runtime from [Microsoft](https://dotnet.microsoft.com/download)
2. If you're using Visual Studio 2017 or greater you may already have SQL Server if not go into Visual Studio Installer and tick SQL Server for installation. Otherwise
head [here](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) and get the developer version, its free.
3. Install Node.js if you don't already have (Its required for Angular) from [here](https://nodejs.org/en/) . Be sure to get version 10.0 or greater.
4. Install Angular CLI using npm from node using the command: <br />
``` 
npm install -g @angular/cli
```
5. Install Redis. If you're using Windows you may want to install a package manager like Chocolatey 
since Redis downloads dont have any precompiled files for installation on Windows. You can get chocolatey from [here](https://chocolatey.org/install). 
Once you have installed Chocolatey just search through the packages they have and look for Redis64bit. <br />
6. Create a Stripe account, and install Stripe CLI. If you are on Windows stripe CLI is not currently available on chocolatey but you may download an executable from [stripe](https://stripe.com/docs/stripe-cli)
7. Login to Stripe, get your Publishable and SecretKeys and paste them into your appsettings.json. Once thats done run the stripe CLI using the comand:
```
stripe listen -f https://localhost:5001/api/payments/webhook
```
8. Copy the key given there and go into Payments Controller in API Project and paste your key into WhSecret to get payment status updates.
9. Go into appsettings.json again and edit your connection strings

## Running
To run the application go into a new terminal, navigate to API project and run:
```
dotnet watch run
```
Open another terminal and run redis using the command:
```
redis-server
```
Open another terminal and run stripe CLI if you're not already running it using the command:
```
stripe listen -f https://localhost:5001/api/payments/webhook
```
Make sure your port number that angular will be running on will match above. <br />
Finally, open another terminal navigate to the client Project folder and run the command:
```
ng-serve
```
This should open a new tab in your default browser with this view:
![Image of Home Page](/API/wwwroot/images/HomePage.png)

## Running the tests

Open POSTman import the test suite from tests.zip and run to test functionality of API

## Built With

* [Net Core](https://dotnet.microsoft.com/download)
* [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
* [Angular](https://angular.io/)

## Authors

* **Avishay Mishra**
* **Moheez Kahn**
* **James Tuhuna**
* **Alesana Eteuati Jr**

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
