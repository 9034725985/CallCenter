FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src 

COPY Server/CallCenter.Server.csproj Server/
COPY Client/CallCenter.Client.csproj Client/
COPY Data/CallCenter.Data.csproj Data/
COPY Shared/CallCenter.Shared.csproj Shared/
RUN dotnet restore Server/CallCenter.Server.csproj

COPY . .
WORKDIR /src/Server
RUN dotnet build CallCenter.Server.csproj -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish CallCenter.Server.csproj -c $BUILD_CONFIGURATION -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 7109
EXPOSE 5286
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CallCenter.Server.dll"]