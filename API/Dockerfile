# Use the official .NET SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src


# Copy the .csproj file and restore dependencies
COPY ["API.csproj", "API/"]
WORKDIR /src/API
RUN dotnet restore


# Copy the rest of the application files and build the app
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Use the official ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5000 
ENTRYPOINT ["dotnet", "API.dll"]
