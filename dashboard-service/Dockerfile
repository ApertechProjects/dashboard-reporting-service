FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build-env
WORKDIR /app

RUN apt update
RUN apt install -y software-properties-common
RUN add-apt-repository 'deb http://deb.debian.org/debian bullseye main'
RUN apt update
RUN apt install -y libc6 -f -o APT::Immediate-Configure=0
RUN apt install -y libgdiplus
RUN apt install -y libicu-dev
RUN apt install -y libharfbuzz0b
RUN apt install -y libfontconfig1
RUN apt install -y libfreetype6
# RUN apt install -y libpango
# RUN apt install -y libpangocairo

RUN dotnet nuget add source https://nuget.devexpress.com/Sb8dllglGunvyRoI8MGQcum4lWh2VmRVccFj8Uxg3WaMEMROu8/api/v3/index.json
COPY AspNetCoreDashboardBackend.csproj .

RUN dotnet restore "./AspNetCoreDashboardBackend.csproj"
COPY . ./
RUN dotnet publish -c Release -o output

FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim
WORKDIR /app
COPY --from=build-env /app/output .
ENTRYPOINT ["dotnet", "AspNetCoreDashboardBackend.dll"]