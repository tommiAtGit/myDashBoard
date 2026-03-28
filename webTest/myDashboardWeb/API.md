# API Documentation

## Overview

Currently, My Dashboard Web is a fully client-side application with no backend API. All data is stored in the browser's local storage. However, this document describes the data models and local storage API for future reference and potential backend integration.

## Local Storage API

### Storage Keys

All data is stored under specific keys in localStorage:

```typescript
STORAGE_KEYS = {
  TASKS: 'dashboard_tasks',
  NOTES: 'dashboard_notes',
  ASSETS: 'dashboard_assets',
  LIABILITIES: 'dashboard_liabilities',
  STOCKS: 'dashboard_stocks',
  FUNDS: 'dashboard_funds',
  ACCOUNTS: 'dashboard_accounts'
}
```

## Data Models

### Task

```typescript
interface Task {
  id: string;              // Unique identifier
  title: string;           // Task title
  description: string;     // Task description
  owner: string;           // Task owner name
  status: TaskStatus;      // Current status
  createdAt: Date;         // Creation timestamp
  updatedAt: Date;         // Last update timestamp
}

enum TaskStatus {
  OPEN = 'open',
  IN_PROGRESS = 'in_progress',
  DONE = 'done'
}
```

**Example:**
```json
{
  "id": "1706198400000-abc123def",
  "title": "Complete Project Proposal",
  "description": "Prepare and submit the Q1 project proposal",
  "owner": "John Doe",
  "status": "open",
  "createdAt": "2024-01-25T10:00:00.000Z",
  "updatedAt": "2024-01-25T10:00:00.000Z"
}
```

### Note

```typescript
interface Note {
  id: string;              // Unique identifier
  title: string;           // Note title
  keywords: string[];      // Keywords for search
  content: string;         // Main note content
  summary: string;         // Note summary
  createdAt: Date;         // Creation timestamp
  updatedAt: Date;         // Last update timestamp
}
```

**Example:**
```json
{
  "id": "1706198400000-xyz789ghi",
  "title": "Meeting Notes - Q1 Planning",
  "keywords": ["planning", "strategy", "Q1"],
  "content": "Discussed key objectives for Q1 including product launches and team expansion.",
  "summary": "Q1 planning session covering strategic initiatives.",
  "createdAt": "2024-01-15T14:30:00.000Z",
  "updatedAt": "2024-01-15T14:30:00.000Z"
}
```

### Asset

```typescript
interface Asset {
  id: string;              // Unique identifier
  name: string;            // Asset name
  value: number;           // Asset value in currency
}
```

**Example:**
```json
{
  "id": "1706198400000-asset123",
  "name": "Savings Account",
  "value": 50000
}
```

### Liability

```typescript
interface Liability {
  id: string;              // Unique identifier
  name: string;            // Liability name
  value: number;           // Liability value in currency
}
```

**Example:**
```json
{
  "id": "1706198400000-liab456",
  "name": "Mortgage",
  "value": 250000
}
```

### Stock

```typescript
interface Stock {
  id: string;              // Unique identifier
  name: string;            // Stock name
  price: number;           // Current stock price
  quantity: number;        // Number of shares owned
  currentValue: number;    // Total value (price × quantity)
}
```

**Example:**
```json
{
  "id": "1706198400000-stock789",
  "name": "Apple Inc.",
  "price": 175.50,
  "quantity": 50,
  "currentValue": 8775
}
```

### Fund

```typescript
interface Fund {
  id: string;              // Unique identifier
  name: string;            // Fund name
  price: number;           // Current fund price
}
```

**Example:**
```json
{
  "id": "1706198400000-fund321",
  "name": "Vanguard 500 Index Fund",
  "price": 425.30
}
```

### Account

```typescript
interface Account {
  currentBalance: number;  // Current account balance
  savingsBalance: number;  // Savings account balance
}
```

**Example:**
```json
{
  "currentBalance": 15000,
  "savingsBalance": 50000
}
```

## Local Storage Operations

### Read Operations

#### Load Tasks
```typescript
const tasks: Task[] = loadTasks();
```
Returns array of all tasks, or default tasks if none exist.

#### Load Notes
```typescript
const notes: Note[] = loadNotes();
```
Returns array of all notes, or default notes if none exist.

#### Load Assets
```typescript
const assets: Asset[] = loadAssets();
```
Returns array of all assets, or default assets if none exist.

#### Load Liabilities
```typescript
const liabilities: Liability[] = loadLiabilities();
```
Returns array of all liabilities, or default liabilities if none exist.

#### Load Stocks
```typescript
const stocks: Stock[] = loadStocks();
```
Returns array of all stocks, or default stocks if none exist.

#### Load Funds
```typescript
const funds: Fund[] = loadFunds();
```
Returns array of all funds, or default funds if none exist.

#### Load Accounts
```typescript
const accounts: Account = loadAccounts();
```
Returns account balances, or default balances if none exist.

### Write Operations

#### Save Tasks
```typescript
saveTasks(tasks: Task[]): void
```
Saves array of tasks to local storage.

#### Save Notes
```typescript
saveNotes(notes: Note[]): void
```
Saves array of notes to local storage.

#### Save Assets
```typescript
saveAssets(assets: Asset[]): void
```
Saves array of assets to local storage.

#### Save Liabilities
```typescript
saveLiabilities(liabilities: Liability[]): void
```
Saves array of liabilities to local storage.

#### Save Stocks
```typescript
saveStocks(stocks: Stock[]): void
```
Saves array of stocks to local storage.

#### Save Funds
```typescript
saveFunds(funds: Fund[]): void
```
Saves array of funds to local storage.

#### Save Accounts
```typescript
saveAccounts(accounts: Account): void
```
Saves account balances to local storage.

## Utility Functions

### Generate ID
```typescript
generateId(): string
```
Generates a unique ID using timestamp and random string.

**Returns:** String in format `{timestamp}-{random}`

**Example:** `"1706198400000-abc123def"`

### Format Date
```typescript
formatDate(date: Date): string
```
Formats a date object to DD.MM.YYYY format.

**Parameters:**
- `date`: Date object to format

**Returns:** Formatted date string

**Example:** `"25.01.2024"`

### Format Currency
```typescript
formatCurrency(value: number): string
```
Formats a number as USD currency.

**Parameters:**
- `value`: Number to format

**Returns:** Formatted currency string

**Example:** `"$1,234.56"`

### Calculate Balance
```typescript
calculateBalance(assets: Asset[], liabilities: Liability[]): number
```
Calculates net balance from assets and liabilities.

**Parameters:**
- `assets`: Array of assets
- `liabilities`: Array of liabilities

**Returns:** Net balance (total assets - total liabilities)

**Example:** 
```typescript
calculateBalance(
  [{ id: '1', name: 'Asset', value: 100000 }],
  [{ id: '1', name: 'Liability', value: 50000 }]
) // Returns: 50000
```

### Calculate Stock Value
```typescript
calculateStockValue(price: number, quantity: number): number
```
Calculates total stock value.

**Parameters:**
- `price`: Stock price per share
- `quantity`: Number of shares

**Returns:** Total value (price × quantity)

**Example:**
```typescript
calculateStockValue(175.50, 50) // Returns: 8775
```

## Future Backend API Specification

If a backend API is implemented, the following RESTful endpoints are recommended:

### Tasks API

```
GET    /api/tasks           - Get all tasks
GET    /api/tasks/:id       - Get specific task
POST   /api/tasks           - Create new task
PUT    /api/tasks/:id       - Update task
DELETE /api/tasks/:id       - Delete task
PATCH  /api/tasks/:id/status - Update task status
```

### Notes API

```
GET    /api/notes           - Get all notes
GET    /api/notes/:id       - Get specific note
POST   /api/notes           - Create new note
PUT    /api/notes/:id       - Update note
DELETE /api/notes/:id       - Delete note
GET    /api/notes/search    - Search notes
```

### Finance API

```
GET    /api/assets          - Get all assets
POST   /api/assets          - Create asset
PUT    /api/assets/:id      - Update asset
DELETE /api/assets/:id      - Delete asset

GET    /api/liabilities     - Get all liabilities
POST   /api/liabilities     - Create liability
PUT    /api/liabilities/:id - Update liability
DELETE /api/liabilities/:id - Delete liability

GET    /api/stocks          - Get all stocks
POST   /api/stocks          - Create stock
PUT    /api/stocks/:id      - Update stock
DELETE /api/stocks/:id      - Delete stock

GET    /api/funds           - Get all funds
POST   /api/funds           - Create fund
PUT    /api/funds/:id       - Update fund
DELETE /api/funds/:id       - Delete fund

GET    /api/accounts        - Get account balances
PUT    /api/accounts        - Update account balances
```

### Response Format

All API responses should follow this format:

**Success Response:**
```json
{
  "success": true,
  "data": { ... },
  "message": "Optional success message"
}
```

**Error Response:**
```json
{
  "success": false,
  "error": {
    "code": "ERROR_CODE",
    "message": "Human-readable error message"
  }
}
```

### Authentication (Future)

When implementing authentication:

```
POST   /api/auth/register   - Register new user
POST   /api/auth/login      - Login
POST   /api/auth/logout     - Logout
POST   /api/auth/refresh    - Refresh token
GET    /api/auth/profile    - Get user profile
```

**Headers:**
```
Authorization: Bearer {token}
```

## Error Handling

### Local Storage Errors

All storage operations include try-catch blocks:

```typescript
try {
  const data = localStorage.getItem(key);
  return JSON.parse(data);
} catch (error) {
  console.error('Error loading data:', error);
  return defaultData;
}
```

Common errors:
- **QuotaExceededError**: Storage limit exceeded (5-10MB)
- **SecurityError**: Access denied (private browsing)
- **SyntaxError**: Invalid JSON data

## Data Migration

### Version 1.0.0 Data Format

Current data format as of version 1.0.0. Future versions should maintain backward compatibility or provide migration functions.

### Migration Strategy

When updating data models:

1. Check version in localStorage
2. Apply migration functions
3. Update version number
4. Save migrated data

Example migration:
```typescript
function migrateV1toV2(data: any): any {
  // Add new fields
  // Transform existing fields
  // Return updated data
}
```

## Performance Considerations

### Local Storage Limits

- **Size**: 5-10MB per domain
- **Synchronous**: Blocking operations
- **String Only**: All data must be serialized

### Best Practices

1. **Minimize Writes**: Batch updates when possible
2. **Compress Data**: For large datasets
3. **Lazy Loading**: Load data only when needed
4. **Debouncing**: Debounce save operations

## Security Notes

### Current Implementation

- No encryption (plain text in localStorage)
- No access control
- Client-side only

### Recommendations for Production

1. **Encrypt Sensitive Data**: Use crypto libraries
2. **Validate Input**: Sanitize all user input
3. **Implement CSP**: Content Security Policy headers
4. **Use HTTPS**: Secure communication
5. **Add Authentication**: User accounts and sessions

## Testing

### Mock Data

Test data is available in helper functions:
- `getDefaultTasks()`
- `getDefaultNotes()`

### Test Storage

Use separate storage keys for testing:
```typescript
const TEST_STORAGE_KEY = 'test_dashboard_data';
```

Clear test data after tests:
```typescript
afterEach(() => {
  localStorage.clear();
});
```
