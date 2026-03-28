# GEMINI.md - myTodoService Guidelines

## Service Goal
Manage todo tasks and their status.

## Architectural Patterns
- **Standard N-Tier:** Controller -> Service -> Repository.
- **In-Memory Storage:** Uses mock repositories (e.g., `TaskRepositoryMoc`) with `List<T>`.
- **DTO Mapping:** Uses AutoMapper between `MyTask` model and `MyTaskDTO`.

## Naming Conventions
- **Typo:** Be aware of the `Moc` typo in filenames and classes (e.g., `ITaskRepositoryMoc.cs`). Follow existing naming for consistency.
- **Status:** Use the `TodoStatus` enum for task states (OPEN, CLOSED, etc.).

## Development Workflow
- **Verification:** Run `dotnet test` in `myTodoService.Tests` after any changes.
