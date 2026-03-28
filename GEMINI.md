# GEMINI.md - Project-Wide Mandates

## Core Technologies
- **Backend:** .NET 8.0 with ASP.NET Core.
- **Frontend:** React (Create React App template) with TypeScript.
- **Infrastructure:** Docker and Docker Compose.
- **Libraries:** AutoMapper for DTO mapping, Moq for mocking in tests, xUnit for testing.

## Architectural Patterns
- **Services:** Follow a N-tier architecture (Controller -> Service -> Repository).
- **Repositories:** Currently use in-memory storage (e.g., `List<T>`) within "Mock" or "Moc" repository classes.
- **DI:** Use Dependency Injection for services and repositories. (Note: Currently some repositories are manually instantiated in services; aim to refactor to standard DI).
- **DTOs:** Separate internal Models from external Domain/DTOs. Use AutoMapper for conversion.
- **Naming:** Follow standard C#/.NET naming conventions (PascalCase for classes, methods, and properties).

## Testing
- **Mandatory:** Every service must have a corresponding `.Tests` project.
- **Isolation:** Use Moq to isolate components during unit testing.
- **Naming:** Follow the naming convention `[Component]Test.cs`.

## Coding Standards
- **Implicit Usings:** Enabled in .csproj.
- **Nullable:** Enabled in .csproj. Use nullable types where appropriate.
- **Namespaces:** Follow the directory structure (e.g., `myFinanceService.Services`).

## Development Workflow
- **Docker:** Use `docker-compose.yml` for local development and integration testing.
- **Verification:** Always run existing tests (`dotnet test`) after any changes to backend services.
