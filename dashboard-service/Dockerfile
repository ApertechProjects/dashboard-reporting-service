FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

RUN dotnet nuget add source https://nuget.devexpress.com/w1pUFolFqmyFfjuGPRqZRdsnasfwmAZUiZtveakOztKkalG4JF/api/v3/index.json
COPY AspNetCoreDashboardBackend.csproj .

RUN dotnet restore "./AspNetCoreDashboardBackend.csproj"
COPY . ./
RUN dotnet publish -c Release -o output

FROM mcr.microsoft.com/dotnet/aspnet
WORKDIR /app
COPY --from=build-env /app/output .
ENTRYPOINT ["dotnet", "AspNetCoreDashboardBackend.dll"]