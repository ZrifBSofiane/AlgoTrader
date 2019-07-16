# AlgoTrader


# TODO 
- IObjectFactory works fine when need to create a single object. But have some issues when an Object has several other that need to be created using their own Factory ... SOLUTION : Actually, all entity have their own Factory, need to change that to only have an IObject factory ----> NOT URGENT
- AJAX file User.js need to be cleaned, some function are useless --> IMPORTANT 
- Change and update the PartialView of User Details in Admin page, add all User informations --> IMPORTANT
- REMOVE all critical informations when calling GetUserDetails form controller. because theses info are called form AJAX ---> IMPORTANT AND URGENT
- Clean and reorder the above PartialView --> IMPORTANT
- Create all the other Factory, Service Repositories --> URGENT 
- Start the same things with Product Page





Until a remote database, please use a local MSSQL. 

1- https://www.microsoft.com/en-us/sql-server/sql-server-downloads
Install SQL Express, install local server, then install SSMS

2- In SSMS, create a new Database with name AlgoTrader
The connectionString is already in the code, so no need to change it  

3- Build, launch it then enjoy
