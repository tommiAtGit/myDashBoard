# GEMINI.md - myFinaceService Guidelines

## Service Goal
Manage financial information including balances and budgets.

## Architectural Patterns
- **Standard N-Tier:** Controller -> Service -> Repository.
- **In-Memory Storage:** Uses mock repositories with `List<T>` as the data store.
- **DTO Mapping:** Extensive use of AutoMapper between Domain (DTOs) and Models.

## Namespaces and Naming
- **Namespace:** Note that while the directory and project name are `myFinaceService` (typo), the internal namespaces used are `myFinanceService` (corrected).
- **Conventions:** Follow standard C#/.NET naming (PascalCase).

## Development Workflow
- **Verification:** Run `dotnet test` in `myFinanceService.Tests` after any changes.
