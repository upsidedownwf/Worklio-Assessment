# Worklio-Assessment

 Worklio-Assessment is an ASP.NET core 3.1 web app built to view a list of countries and their details.
 
The project is built using ASP .NET core razor pages (after approval from Ondrej Staud) for the UI and webapi for its backend.
It sources data from a public api (its url can be found in the appsettings.json file)

## Build

Run `dotnet build` from a terminal in the application folder to build the app.

## Running application

Run `dotnet run` in the web project's folder from a terminal to run the application and visit `http://localhost:5000` or `https://localhost:5001` on your browser to access the web app.

### Note:
The app is built to be localizable and supports a set of already specified languages which can be found in the project's appsettings.json file.
The content strings that have been localized for sake of testing are:
1. The welcome message in the Index page.
2. The privacy policy string in the privacy page.
3. The header content in the countries page.
4. Today's date which can be found in the header/navigation menu of the web app (i.e @_Layout.cshtml)
