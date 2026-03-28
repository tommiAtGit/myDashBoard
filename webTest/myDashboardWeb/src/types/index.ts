// Task Types
export interface Task {
  id: string;
  title: string;
  description: string;
  owner: string;
  status: TaskStatus;
  createdAt: Date;
  updatedAt: Date;
}

export enum TaskStatus {
  OPEN = 'open',
  IN_PROGRESS = 'in_progress',
  DONE = 'done'
}

export interface TaskColumn {
  id: TaskStatus;
  title: string;
  tasks: Task[];
}

// Note Types
export interface Note {
  id: string;
  title: string;
  keywords: string[];
  content: string;
  summary: string;
  createdAt: Date;
  updatedAt: Date;
}

export interface RecallItem {
  id: string;
  noteId: string;
  type: 'missing' | 'question';
  content: string;
  createdAt: Date;
}

// Finance Types
export interface Asset {
  id: string;
  name: string;
  value: number;
}

export interface Liability {
  id: string;
  name: string;
  value: number;
}

export interface Stock {
  id: string;
  name: string;
  price: number;
  quantity: number;
  currentValue: number;
}

export interface Fund {
  id: string;
  name: string;
  price: number;
}

export interface Account {
  currentBalance: number;
  savingsBalance: number;
}

export interface BalanceData {
  assets: Asset[];
  liabilities: Liability[];
}

export interface BudgetData {
  account: Account;
  stocks: Stock[];
  funds: Fund[];
}

// Common Types
export interface DragResult {
  draggableId: string;
  source: {
    droppableId: string;
    index: number;
  };
  destination: {
    droppableId: string;
    index: number;
  } | null;
}
