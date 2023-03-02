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



## Local deployment notes 

For deployment to your own Linux machine (I use Fedora, highly recommended as it comes with dotnet), 
I want to share a poor man's deployment option 
you can write your appsettings.config file somewhere outside the repository
then, you can copy this file into your publish folder to overwrite what you got from dotnet publish. 
Here is some sample script: 

```bash
cd ~/src/dotnet/CallCenter;
git remote update && git reset --hard @{u};
time git pull;
time dotnet build;
cd ~/src/dotnet/CallCenter/Server;
time rm -r ~/src/dotnet/CallCenter/Server/bin/;
time dotnet publish --configuration Release --os linux --self-contained true --verbosity detailed;
time cp ~/src/dotnet/callcenterappconfig.json ~/src/dotnet/CallCenter/Server/bin/Release/net7.0/linux-x64/publish/appsettings.json;
cd /home/kushal/src/dotnet/CallCenter/Server/bin/Release/net7.0/linux-x64/publish/ && ./CallCenter.Server --urls "https://0.0.0.0:7109;http://0.0.0.0:5286";
```

Here is my `dotnet --info` on the fedora machine 

```bash
$ dotnet --info
.NET SDK:
 Version:   7.0.103
 Commit:    276c71d299

Runtime Environment:
 OS Name:     fedora
 OS Version:  37
 OS Platform: Linux
 RID:         fedora.37-x64
 Base Path:   /usr/lib64/dotnet/sdk/7.0.103/

Host:
  Version:      7.0.3
  Architecture: x64
  Commit:       0a2bda10e8

.NET SDKs installed:
  3.1.424 [/usr/lib64/dotnet/sdk]
  6.0.114 [/usr/lib64/dotnet/sdk]
  7.0.103 [/usr/lib64/dotnet/sdk]

.NET runtimes installed:
  Microsoft.AspNetCore.App 3.1.30 [/usr/lib64/dotnet/shared/Microsoft.AspNetCore.App]
  Microsoft.AspNetCore.App 6.0.14 [/usr/lib64/dotnet/shared/Microsoft.AspNetCore.App]
  Microsoft.AspNetCore.App 7.0.3 [/usr/lib64/dotnet/shared/Microsoft.AspNetCore.App]
  Microsoft.NETCore.App 3.1.30 [/usr/lib64/dotnet/shared/Microsoft.NETCore.App]
  Microsoft.NETCore.App 6.0.14 [/usr/lib64/dotnet/shared/Microsoft.NETCore.App]
  Microsoft.NETCore.App 7.0.3 [/usr/lib64/dotnet/shared/Microsoft.NETCore.App]

Other architectures found:
  None

Environment variables:
  DOTNET_ROOT       [/usr/lib64/dotnet]

global.json file:
  Not found

Learn more:
  https://aka.ms/dotnet/info

Download .NET:
  https://aka.ms/dotnet/download
```

And there is my `dnf info dotnet`

```bash
$ dnf info dotnet
Fedora 36 - x86_64                                                                                                                                                                                                                                       35 kB/s |  25 kB     00:00
Fedora Modular 36 - x86_64                                                                                                                                                                                                                               39 kB/s |  24 kB     00:00
Fedora 36 - x86_64 - Updates                                                                                                                                                                                                                             37 kB/s |  22 kB     00:00
Fedora 36 - x86_64 - Updates                                                                                                                                                                                                                            895 kB/s | 3.1 MB     00:03
Fedora Modular 36 - x86_64 - Updates                                                                                                                                                                                                                     36 kB/s |  23 kB     00:00
Installed Packages
Name         : dotnet
Version      : 7.0.102
Release      : 1.fc36
Architecture : x86_64
Size         : 0.0
Source       : dotnet7.0-7.0.102-1.fc36.src.rpm
Repository   : @System
From repo    : updates
Summary      : .NET CLI tools and runtime
URL          : https://github.com/dotnet/
License      : 0BSD AND Apache-2.0 AND (Apache-2.0 WITH LLVM-Exception) AND APSL-2.0 AND BSD-2-Clause AND BSD-3-Clause AND BSD-4-Clause AND BSL-1.0 AND bzip2-1.0.6 AND CC0-1.0 AND CC-BY-3.0 AND CC-BY-4.0 AND CC-PDDC AND CNRI-Python AND EPL-1.0 AND GPL-2.0-only AND (GPL-2.0-only
             : WITH GCC-exception-2.0) AND GPL-2.0-or-later AND GPL-3.0-only AND ICU AND ISC AND LGPL-2.1-only AND LGPL-2.1-or-later AND LicenseRef-Fedora-Public-Domain AND LicenseRef-ISO-8879 AND MIT AND MIT-Wu AND MS-PL AND MS-RL AND NCSA AND OFL-1.1 AND OpenSSL AND
             : Unicode-DFS-2015 AND Unicode-DFS-2016 AND W3C-19980720 AND X11 AND Zlib
Description  : .NET is a fast, lightweight and modular platform for creating
             : cross platform applications that work on Linux, macOS and Windows.
             :
             : It particularly focuses on creating console applications, web
             : applications and micro-services.
             :
             : .NET contains a runtime conforming to .NET Standards a set of
             : framework libraries, an SDK containing compilers and a 'dotnet'
             : application to drive everything.
```



cd ~/src/dotnet/CallCenter; git remote update && git reset --hard @{u}; time git pull; time dotnet build; cd ~/src/dotnet/CallCenter/Server; time rm -r ~/src/dotnet/CallCenter/Server/bin/; time rm -r ~/src/dotnet/CallCenter/Server/obj/; time rm -r ~/src/dotnet/CallCenter/Client/bin/; time rm -r ~/src/dotnet/CallCenter/Client/obj/; time rm -r ~/src/dotnet/CallCenter/Shared/bin/; time rm -r ~/src/dotnet/CallCenter/Shared/obj/; time rm -r ~/src/dotnet/CallCenter/Data/bin/; time rm -r ~/src/dotnet/CallCenter/Data/obj/; time dotnet publish --configuration Release --os linux --self-contained true --verbosity detailed; time cp ~/src/dotnet/callcenterappconfig.json ~/src/dotnet/CallCenter/Server/bin/Release/net7.0/linux-x64/publish/appsettings.json; cd /home/kushal/src/dotnet/CallCenter/Server/bin/Release/net7.0/linux-x64/publish/ && ./CallCenter.Server --urls "https://0.0.0.0:7109;http://0.0.0.0:5286";

// Invalid regular expression '**/Client/wwwroot/css/bootstrap/**/*' for setting 'sonar.issue.ignore.allfile'. Update your settings with a valid regular expression. ?? 