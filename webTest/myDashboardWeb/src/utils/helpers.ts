import { Task, TaskStatus, Note, Asset, Liability, Stock, Fund } from '../types';

// Local Storage Keys
export const STORAGE_KEYS = {
  TASKS: 'dashboard_tasks',
  NOTES: 'dashboard_notes',
  ASSETS: 'dashboard_assets',
  LIABILITIES: 'dashboard_liabilities',
  STOCKS: 'dashboard_stocks',
  FUNDS: 'dashboard_funds',
  ACCOUNTS: 'dashboard_accounts'
};

// Date Formatting
export const formatDate = (date: Date): string => {
  const d = new Date(date);
  const day = String(d.getDate()).padStart(2, '0');
  const month = String(d.getMonth() + 1).padStart(2, '0');
  const year = d.getFullYear();
  return `${day}.${month}.${year}`;
};

// Currency Formatting
export const formatCurrency = (value: number): string => {
  return new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD',
    minimumFractionDigits: 2
  }).format(value);
};

// Generate Unique ID
export const generateId = (): string => {
  return `${Date.now()}-${Math.random().toString(36).substr(2, 9)}`;
};

// Task Storage Functions
export const loadTasks = (): Task[] => {
  try {
    const stored = localStorage.getItem(STORAGE_KEYS.TASKS);
    if (stored) {
      const tasks = JSON.parse(stored);
      return tasks.map((task: any) => ({
        ...task,
        createdAt: new Date(task.createdAt),
        updatedAt: new Date(task.updatedAt)
      }));
    }
  } catch (error) {
    console.error('Error loading tasks:', error);
  }
  return getDefaultTasks();
};

export const saveTasks = (tasks: Task[]): void => {
  try {
    localStorage.setItem(STORAGE_KEYS.TASKS, JSON.stringify(tasks));
  } catch (error) {
    console.error('Error saving tasks:', error);
  }
};

export const getDefaultTasks = (): Task[] => [
  {
    id: '1',
    title: 'Complete Project Proposal',
    description: 'Prepare and submit the Q1 project proposal',
    owner: 'John Doe',
    status: TaskStatus.OPEN,
    createdAt: new Date(),
    updatedAt: new Date()
  },
  {
    id: '2',
    title: 'Review Code Changes',
    description: 'Review pull requests from team members',
    owner: 'Jane Smith',
    status: TaskStatus.OPEN,
    createdAt: new Date(),
    updatedAt: new Date()
  },
  {
    id: '3',
    title: 'Update Documentation',
    description: 'Update API documentation with new endpoints',
    owner: 'Bob Johnson',
    status: TaskStatus.IN_PROGRESS,
    createdAt: new Date(),
    updatedAt: new Date()
  },
  {
    id: '4',
    title: 'Deploy to Production',
    description: 'Deploy version 2.0 to production environment',
    owner: 'Alice Brown',
    status: TaskStatus.DONE,
    createdAt: new Date(),
    updatedAt: new Date()
  }
];

// Note Storage Functions
export const loadNotes = (): Note[] => {
  try {
    const stored = localStorage.getItem(STORAGE_KEYS.NOTES);
    if (stored) {
      const notes = JSON.parse(stored);
      return notes.map((note: any) => ({
        ...note,
        createdAt: new Date(note.createdAt),
        updatedAt: new Date(note.updatedAt)
      }));
    }
  } catch (error) {
    console.error('Error loading notes:', error);
  }
  return getDefaultNotes();
};

export const saveNotes = (notes: Note[]): void => {
  try {
    localStorage.setItem(STORAGE_KEYS.NOTES, JSON.stringify(notes));
  } catch (error) {
    console.error('Error saving notes:', error);
  }
};

export const getDefaultNotes = (): Note[] => [
  {
    id: '1',
    title: 'Meeting Notes - Q1 Planning',
    keywords: ['planning', 'strategy', 'Q1'],
    content: 'Discussed key objectives for Q1 including product launches and team expansion.',
    summary: 'Q1 planning session covering strategic initiatives and resource allocation.',
    createdAt: new Date('2024-01-15'),
    updatedAt: new Date('2024-01-15')
  },
  {
    id: '2',
    title: 'Technical Architecture Review',
    keywords: ['architecture', 'technical', 'review'],
    content: 'Reviewed current system architecture and proposed improvements for scalability.',
    summary: 'Architecture review focusing on scalability and performance optimization.',
    createdAt: new Date('2024-01-20'),
    updatedAt: new Date('2024-01-20')
  }
];

// Finance Storage Functions
export const loadAssets = (): Asset[] => {
  try {
    const stored = localStorage.getItem(STORAGE_KEYS.ASSETS);
    if (stored) {
      return JSON.parse(stored);
    }
  } catch (error) {
    console.error('Error loading assets:', error);
  }
  return [
    { id: '1', name: 'Savings Account', value: 50000 },
    { id: '2', name: 'Investment Portfolio', value: 120000 },
    { id: '3', name: 'Real Estate', value: 350000 }
  ];
};

export const saveAssets = (assets: Asset[]): void => {
  try {
    localStorage.setItem(STORAGE_KEYS.ASSETS, JSON.stringify(assets));
  } catch (error) {
    console.error('Error saving assets:', error);
  }
};

export const loadLiabilities = (): Liability[] => {
  try {
    const stored = localStorage.getItem(STORAGE_KEYS.LIABILITIES);
    if (stored) {
      return JSON.parse(stored);
    }
  } catch (error) {
    console.error('Error loading liabilities:', error);
  }
  return [
    { id: '1', name: 'Mortgage', value: 250000 },
    { id: '2', name: 'Car Loan', value: 15000 }
  ];
};

export const saveLiabilities = (liabilities: Liability[]): void => {
  try {
    localStorage.setItem(STORAGE_KEYS.LIABILITIES, JSON.stringify(liabilities));
  } catch (error) {
    console.error('Error saving liabilities:', error);
  }
};

export const loadStocks = (): Stock[] => {
  try {
    const stored = localStorage.getItem(STORAGE_KEYS.STOCKS);
    if (stored) {
      return JSON.parse(stored);
    }
  } catch (error) {
    console.error('Error loading stocks:', error);
  }
  return [
    { id: '1', name: 'Apple Inc.', price: 175.50, quantity: 50, currentValue: 8775 },
    { id: '2', name: 'Microsoft Corp.', price: 380.20, quantity: 30, currentValue: 11406 },
    { id: '3', name: 'Tesla Inc.', price: 245.80, quantity: 25, currentValue: 6145 }
  ];
};

export const saveStocks = (stocks: Stock[]): void => {
  try {
    localStorage.setItem(STORAGE_KEYS.STOCKS, JSON.stringify(stocks));
  } catch (error) {
    console.error('Error saving stocks:', error);
  }
};

export const loadFunds = (): Fund[] => {
  try {
    const stored = localStorage.getItem(STORAGE_KEYS.FUNDS);
    if (stored) {
      return JSON.parse(stored);
    }
  } catch (error) {
    console.error('Error loading funds:', error);
  }
  return [
    { id: '1', name: 'Vanguard 500 Index Fund', price: 425.30 },
    { id: '2', name: 'Fidelity Total Market Index', price: 112.45 }
  ];
};

export const saveFunds = (funds: Fund[]): void => {
  try {
    localStorage.setItem(STORAGE_KEYS.FUNDS, JSON.stringify(funds));
  } catch (error) {
    console.error('Error saving funds:', error);
  }
};

export const loadAccounts = (): { current: number; savings: number } => {
  try {
    const stored = localStorage.getItem(STORAGE_KEYS.ACCOUNTS);
    if (stored) {
      return JSON.parse(stored);
    }
  } catch (error) {
    console.error('Error loading accounts:', error);
  }
  return { current: 15000, savings: 50000 };
};

export const saveAccounts = (accounts: { current: number; savings: number }): void => {
  try {
    localStorage.setItem(STORAGE_KEYS.ACCOUNTS, JSON.stringify(accounts));
  } catch (error) {
    console.error('Error saving accounts:', error);
  }
};

// Calculate total balance
export const calculateBalance = (assets: Asset[], liabilities: Liability[]): number => {
  const totalAssets = assets.reduce((sum, asset) => sum + asset.value, 0);
  const totalLiabilities = liabilities.reduce((sum, liability) => sum + liability.value, 0);
  return totalAssets - totalLiabilities;
};

// Calculate stock current value
export const calculateStockValue = (price: number, quantity: number): number => {
  return price * quantity;
};
