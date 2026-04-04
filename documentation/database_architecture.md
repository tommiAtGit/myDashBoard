# Database Architecture

This document describes the database schema for the myDashBoard application. The system is designed to use a relational database (PostgreSQL/MySQL) to store data for all three backend services.

## ER Diagram

```mermaid
erDiagram
    FINANCE {
        guid id PK
        int type
        string account
        string description
        double amount
        datetime action_date
    }
    FINANCE_CATEGORY {
        guid id PK
        guid finance_id FK
        string category
    }

    FINANCE ||--o{ FINANCE_CATEGORY : "has"
    
    BALANCE {
        guid id PK
        string account
        double account_balance
        datetime balance_date
    }
    BUDGET {
        guid id PK
        string budget_account
        string budget_title
        double budget_value
        datetime budget_start_date
        datetime budget_end_date
    }
    BALANCE_SHEET{
        guid id PK
        double balance_sheet_item_value 
        datetime balance_sheet_item_created 
        datetime balance_sheet_item_changed 
    }

    BALANCE_SHEET_ITEM{
        guid id PK
        balance_sheet_id FK
        string item_name
    }
    BALANCE_SHEET ||--o{ BALANCE_SHEET_ITEM : "has"

    GENERAL_NOTE {
        guid id PK
        string title
        string notes
        string conclusion
        string owner
        string modified_by
        datetime date_created
        datetime date_modified
    }
    NOTE_KEYWORD {
        guid id PK
        guid note_id FK
        string keyword
    }
    TODO_TASK {
        guid id PK
        string name
        string description
        string reporter
        string owner
        int status
        datetime date_reported
        datetime date_opened
        datetime date_completed
        datetime date_closed
    }

    GENERAL_NOTE ||--o{ NOTE_KEYWORD : "has"
```

## Schema Definitions

### 1. Finance Service Tables

#### FINANCE_TRANSACTION
Stores individual financial transactions.
- `id`: GUID, Primary Key.
- `type`: Integer (Enum: 0=UNDEFINED, 1=DEPOSIT, 2=WITHDRAWAL, 3=LOAN, 4=SAVE).
- `account`: String, the account number/IBAN.
- `description`: String, transaction details.
- `amount`: Double, the monetary value.
- `action_date`: DateTime, when the transaction occurred.

#### FINANCE_CATEGORY
Stores categories associated with transactions (One-to-Many).
- `id`: GUID, Primary Key.
- `transaction_id`: GUID, Foreign Key to `FINANCE_TRANSACTION(id)`.
- `category`: String, tag for searching.

#### BALANCE
Stores the current balance for accounts.
- `id`: GUID, Primary Key.
- `account`: String, unique identifier for the account.
- `account_balance`: Double, current amount in the account.
- `balance_date`: DateTime, timestamp of the balance record.

#### BUDGET
Stores budget targets for accounts.
- `id`: GUID, Primary Key.
- `budget_account`: String, the account this budget applies to.
- `budget_title`: String, name of the budget.
- `budget_value`: Double, the target amount.
- `budget_start_date`: DateTime, start of the budget period.
- `budget_end_date`: DateTime, end of the budget period.

### 2. Notes Service Tables

#### GENERAL_NOTE
Stores general user notes.
- `id`: GUID, Primary Key.
- `title`: String, title of the note.
- `notes`: Text, main content of the note.
- `conclusion`: Text, summary or conclusion.
- `owner`: String, user who created the note.
- `modified_by`: String, last user to modify.
- `date_created`: DateTime, creation timestamp.
- `date_modified`: DateTime, last modification timestamp.

#### NOTE_KEYWORD
Stores keywords associated with notes (One-to-Many).
- `id`: GUID, Primary Key.
- `note_id`: GUID, Foreign Key to `GENERAL_NOTE(id)`.
- `keyword`: String, tag for searching.

### 3. Todo Service Tables

#### TODO_TASK
Stores tasks and their statuses.
- `id`: GUID, Primary Key.
- `name`: String, name of the task.
- `description`: Text, task details.
- `reporter`: String, user who reported the task.
- `owner`: String, user assigned to the task.
- `status`: Integer (Enum: 0=UNDEFINED, 1=OPEN, 2=PROGRESS, 3=DONE, 4=DEPRECATED, 5=CLOSED).
- `date_reported`: DateTime.
- `date_opened`: DateTime.
- `date_completed`: DateTime.
- `date_closed`: DateTime.
