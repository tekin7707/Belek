FROM mcr.microsoft.com/dotnet/sdk:3.1 as build
WORKDIR /app
EXPOSE 80
COPY Shared/Belek.Shared/*.csproj Shared/Belek.Shared/
COPY IdentityServer/Belek.IdentityServer/*.csproj IdentityServer/Belek.IdentityServer/ 
RUN dotnet restore  IdentityServer/Belek.IdentityServer/*.csproj
COPY . .
RUN dotnet publish IdentityServer/Belek.IdentityServer/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:3.1 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","Belek.IdentityServer.dll" ]




