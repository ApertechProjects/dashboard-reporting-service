FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build-env
WORKDIR /app

RUN dotnet nuget add source https://nuget.devexpress.com/Sb8dllglGunvyRoI8MGQcum4lWh2VmRVccFj8Uxg3WaMEMROu8/api/v3/index.json
COPY AspNetCoreDashboardBackend.csproj .

RUN dotnet restore "./AspNetCoreDashboardBackend.csproj"
COPY . ./
RUN dotnet publish -c Release -o output

FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim
WORKDIR /app
COPY --from=build-env /app/output .
ENTRYPOINT ["dotnet", "AspNetCoreDashboardBackend.dll"]