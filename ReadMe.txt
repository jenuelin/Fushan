Reference
https://github.com/erdkse/adminlte-3-angular
https://medium.com/swlh/securing-your-net-core-3-api-using-identity-93d6426d6311
https://fullstackmark.com/post/13/jwt-authentication-with-aspnet-core-2-web-api-angular-5-net-core-identity-and-facebook-login


========================================= Migration ===============================================
step.1 please set Fushan as startup project

step.2 set DataServices as default project in nuget manager console

add migration
PM> EntityFrameworkCore\Add-Migration {migration name}

update database
PM> EntityFrameworkCore\Update-Database

====================================================================================================


TODO fix:
breadcrumb
https://medium.com/applantic/https-medium-com-applantic-how-to-implement-breadcrumb-navigation-in-angular-and-primeng-52573e49b97a