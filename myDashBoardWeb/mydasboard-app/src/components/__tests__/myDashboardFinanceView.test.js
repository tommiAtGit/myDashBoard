import React from 'react';
import { render, screen, waitFor, fireEvent } from '@testing-library/react';
import axios from 'axios';
import FinanceView from '../myDashboardFinanceView';

// axios is already mocked in setupTests.js

describe('FinanceView Component', () => {
    const mockBalanceSheet = [{
        id: 'bs1',
        items: [
            { id: 'i1', itemName: 'Cash', value: 5000, type: 0 },
            { id: 'i2', itemName: 'Debt', value: 2000, type: 1 }
        ]
    }];

    const mockTransactions = [
        { id: 't1', account: 'FI123', type: 1, amount: 1000, description: 'Salary', actionDate: '2024-01-01' },
        { id: 't2', account: 'FI123', type: 2, amount: 200, description: 'Rent', actionDate: '2024-01-02' }
    ];

    const mockBudgets = [
        { id: 'b1', budgetTitle: 'Food', budgetValue: 500, budgetStartDate: '2024-01-01', budgetEndDate: '2024-01-31' }
    ];

    beforeEach(() => {
        jest.clearAllMocks();
        axios.get.mockImplementation((url) => {
            if (url.includes('BalanceSheet')) return Promise.resolve({ data: mockBalanceSheet });
            if (url.includes('FinanceTracker/account')) return Promise.resolve({ data: mockTransactions });
            if (url.includes('FinanceTracker')) return Promise.resolve({ data: mockTransactions });
            if (url.includes('Budget/account')) return Promise.resolve({ data: mockBudgets });
            return Promise.resolve({ data: [] });
        });
    });

    test('renders loading state initially', () => {
        render(<FinanceView />);
        expect(screen.getByText(/Loading Finance.../i)).toBeInTheDocument();
    });

    test('renders finance data after loading', async () => {
        render(<FinanceView />);
        
        // Wait for specific data to appear
        const salaryElements = await screen.findAllByText(/Salary/i);
        expect(salaryElements.length).toBeGreaterThan(0);

        expect(screen.getByText(/Financial Dashboard/i)).toBeInTheDocument();
        expect(screen.getByText(/Balance Sheet Overview/i)).toBeInTheDocument();
        
        // Assets should be 5000 (toLocaleString adds commas)
        expect(screen.getByText(/\$5,000/i)).toBeInTheDocument();
        // Liabilities should be 2000
        expect(screen.getByText(/\$2,000/i)).toBeInTheDocument();
        
        expect(screen.getByText(/Food/i)).toBeInTheDocument();
    });

    test('opens FinanceModal when Add Transaction is clicked', async () => {
        render(<FinanceView />);
        
        await screen.findByText(/Financial Dashboard/i);

        fireEvent.click(screen.getByText(/Add Transaction/i));
        const modalTitle = await screen.findByText('Transaction');
        expect(modalTitle).toBeInTheDocument();
    });
});
