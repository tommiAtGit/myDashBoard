# Use the official .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /myNotesService
EXPOSE 80

# Use the SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["myNotesService/myNotesService.csproj", "myNotesService/"]
RUN dotnet restore "myNotesService/myNotesService.csproj"
COPY . .
WORKDIR "/src/myNotesService"
RUN dotnet build "myNotesService.csproj" -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "myNotesService.csproj" -c Release -o /app/publish


# Final stage to create the runtime container
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "myNotesService.dll"]
