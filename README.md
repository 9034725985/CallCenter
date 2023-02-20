# CallCenter

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=9034725985_CallCenter&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=9034725985_CallCenter)

[![Technical Debt](https://sonarcloud.io/api/project_badges/measure?project=9034725985_CallCenter&metric=sqale_index)](https://sonarcloud.io/summary/new_code?id=9034725985_CallCenter)

[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=9034725985_CallCenter&metric=coverage)](https://sonarcloud.io/summary/new_code?id=9034725985_CallCenter)

[![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=9034725985_CallCenter&metric=ncloc)](https://sonarcloud.io/summary/new_code?id=9034725985_CallCenter)

[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=9034725985_CallCenter&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=9034725985_CallCenter)

[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=9034725985_CallCenter&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=9034725985_CallCenter)

[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=9034725985_CallCenter&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=9034725985_CallCenter)

[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=9034725985_CallCenter&metric=bugs)](https://sonarcloud.io/summary/new_code?id=9034725985_CallCenter)

[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=9034725985_CallCenter&metric=vulnerabilities)](https://sonarcloud.io/summary/new_code?id=9034725985_CallCenter)

[![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=9034725985_CallCenter&metric=duplicated_lines_density)](https://sonarcloud.io/summary/new_code?id=9034725985_CallCenter)

[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=9034725985_CallCenter&metric=reliability_rating)](https://sonarcloud.io/summary/new_code?id=9034725985_CallCenter)



## Installation instructions

1. Please set up the database using the scripts in the `Data/SqlScripts` folder. 
2. Now go to the `server` directory. 
    For example, `cd ~/src/dotnet/CallCenter/Server`
3. Next set up the dotnet secret. 
    `dotnet user-secrets set "ConnectionStrings:Default" "Host=server;Database=databasename;User Id=userid;Password=password;"`
4. You are now ready to run the application. 
    I like to add an entry to my crontab like this 
    `@reboot cd ~/src/dotnet/CallCenter/Server; git remote update && git reset --hard @{u}; time git pull; time dotnet build; dotnet run --configuration Release`



