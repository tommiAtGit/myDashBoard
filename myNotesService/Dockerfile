# Use the official .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the .csproj file and restore dependencies
# Copy the csproj file into /src directory in the container
COPY myNotesService.csproj ./  
RUN dotnet restore "myNotesService.csproj"

# Copy the remaining files (including Program.cs and all other files) into the container
# This will copy everything from your local directory to /src in the container
COPY . .  
WORKDIR "/src"
RUN dotnet build "myNotesService.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "myNotesService.csproj" -c Release -o /app/publish

# Final stage to create the runtime container
# Copy the published files to /app in the container
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .  
# Run the app when the container starts
ENTRYPOINT ["dotnet", "myNotesService.dll"]  