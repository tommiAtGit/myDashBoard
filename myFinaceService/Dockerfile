# Use the official .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /myFinaceService
EXPOSE 80

# Use the SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["myFinaceService/myFinaceService.csproj", "myFinaceService/"]
RUN dotnet restore "myFinaceService/myFinaceService.csproj"
COPY . .
WORKDIR "/src/myFinaceService"
RUN dotnet build "myFinaceService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "myFinaceService.csproj" -c Release -o /app/publish


# Final stage to create the runtime container
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "myFinaceService.dll"]
