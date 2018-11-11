To run whole system (Client and API):
Open SEBTask.sln with Visual Studio 2017;
Run 'SEBTask' project;
Brief description of the system:
.NET core 2.1 is used for back-end and React.js is used for front-end side. To save coding time and make run process as easy as possible, visual studio .net core + react web application template was used.  In project there are 4 folders:
ClientApp - folder is used to separate back-end (API) from front-end (whole front-end code is in this folder); 
Controllers - folder for API endpoints;
Models - folder for API models;
Services - folder for API services;
Caching is implemented in service layer (this could also be done in controller layer if needed). Service configuration is set in appsettings.json file, so 3rd party service url or caching time could be changed without rebuilding the project. API architecture based on SOLID principles.

Regarding front-end, React.js was selected because I wanted to create single page application (SPA). 