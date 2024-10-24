# ASP.NET MVC uchun Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
#Projectlarni ko'chirish
COPY ./ChatApplication.Domain ./ChatApplication.Domain
COPY ./ChatApplication.Server ./ChatApplication.Server
COPY ./ChatApplication.Web ./ChatApplication.Web

# Sertifikatlarni ko'chirish
COPY ./https/mycert.pfx /https/mycert.pfx

# HTTPS portini ochish
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/mycert.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=2002


WORKDIR /src/ChatApplication.Web

RUN dotnet restore ChatApplication.Web.csproj
RUN dotnet build ChatApplication.Web.csproj -c Release -o /app/build
RUN dotnet publish ChatApplication.Web.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT [ "dotnet" , "ChatApplication.Web.dll"]