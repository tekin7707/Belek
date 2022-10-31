FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app
EXPOSE 80
COPY Shared/Belek.Shared/*.csproj Shared/Belek.Shared/
COPY Services/Catalog/Belek.Services.Catalog.Api/*.csproj Services/Catalog/Belek.Services.Catalog.Api/
RUN dotnet restore Services/Catalog/Belek.Services.Catalog.Api/*.csproj
COPY . .
RUN dotnet publish Services/Catalog/Belek.Services.Catalog.Api/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","Belek.Services.Catalog.Api.dll" ]




