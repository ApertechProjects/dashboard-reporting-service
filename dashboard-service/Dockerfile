FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

RUN dotnet nuget add source https://nuget.devexpress.com/3GAOGnkg582VWsphOjRwQPRun5YtpsKWXzrxaROQNsVmGqIlAq/api/v3/index.json
COPY AspNetCoreDashboardBackend.csproj .

RUN dotnet restore "./AspNetCoreDashboardBackend.csproj"
COPY . ./
RUN dotnet publish -c Release -o output

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/output .
ENTRYPOINT ["dotnet", "AspNetCoreDashboardBackend.dll"]