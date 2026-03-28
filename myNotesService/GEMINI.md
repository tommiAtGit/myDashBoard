# GEMINI.md - myNotesService Guidelines

## Service Goal
Manage general notes.

## Architectural Patterns
- **Standard N-Tier:** Controller -> Service -> Repository.
- **In-Memory Storage:** Uses repositories (e.g., `GeneralNotesRepository`) with `List<T>`.
- **DTO Mapping:** Uses AutoMapper for conversion between domain and model.

## Naming Conventions
- **Note:** Standard PascalCase for classes and methods.
- **Namespaces:** `myNotesService.Services`, `myNotesService.Repository`, etc.

## Development Workflow
- **Verification:** Run `dotnet test` in `myNotesService.Tests` after any changes.
