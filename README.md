# Worklio-Assessment

 Worklio-Assessment is an ASP.NET core 3.1 web app built to view a list of countries and their details.
 
The project is built using ASP .NET core razor pages (after approval from Ondrej Staud) for the UI and webapi for its backend.
It sources data from a public api (its url can be found in the appsettings.json file under the key **CountriesApi**)

## Build

Run `dotnet build` from a terminal in the application folder to build the app.

## Running application

Run `dotnet run` in the web project's folder from a terminal to run the application and visit `http://localhost:5000` or `https://localhost:5001` on your browser to access the web app.

### Note:
The app is built to be localizable and supports a set of already specified languages which can be found in the project's appsettings.json file under the key **Cultures**.
The content strings that have been localized for sake of testing are:
1. The welcome message in the Index page.
2. The privacy policy string in the privacy page.
3. The header content in the countries page.
4. Today's date which can be found in the header/navigation menu of the web app (i.e @_Layout.cshtml)

The application logs information and errors to a folder `c:/worklioassessment/logs` on your hardisk as specified in the asssessment question. Please note that I implemented a small logger to use because Microsoft's default logger doesn't have a file logging provider and I didn't want to implement my own custom file logging provider for this assessment (no over-achieving😀).

I also implemented a small custom emailer class to send mails which is a wrapper around Microsoft's `SmtpClient` class. The settngs required to configure this emailer can be found in the appsettings.json file under the key **EmailSettings**

The assessment instructions said I shouldn't use a third party library which is why I did not use logging packages like Serilog, Log4net, etc, that support logging to files and email packages like Mailkit to send emails.
