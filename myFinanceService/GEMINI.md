# GEMINI.md - myFinanceService Guidelines

## Service Goal
Manage financial information including balances and budgets.

## Architectural Patterns
- **Standard N-Tier:** Controller -> Service -> Repository.
- **In-Memory Storage:** Uses mock repositories with `List<T>` as the data store.
- **DTO Mapping:** Extensive use of AutoMapper between Domain (DTOs) and Models.

## Namespaces and Naming
- **Conventions:** Follow standard C#/.NET naming (PascalCase).

## Development Workflow
- **Verification:** Run `dotnet test` in `myFinanceService.Tests` after any changes.
