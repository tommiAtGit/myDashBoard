import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import BalanceSheetModal from '../myDashboardBalanceSheetModal';

describe('BalanceSheetModal Component', () => {
    const mockOnClose = jest.fn();
    const mockOnSave = jest.fn();
    const mockBS = {
        id: '1',
        items: [
            { id: 'item1', itemName: 'Cash', value: 1000, type: 0 },
            { id: 'item2', itemName: 'Loan', value: 500, type: 1 }
        ]
    };

    test('does not render when isOpen is false', () => {
        const { queryByText } = render(
            <BalanceSheetModal isOpen={false} onClose={mockOnClose} onSave={mockOnSave} />
        );
        expect(queryByText(/Edit Balance Sheet/i)).not.toBeInTheDocument();
    });

    test('renders with correct title and items when isOpen is true', () => {
        render(
            <BalanceSheetModal isOpen={true} onClose={mockOnClose} onSave={mockOnSave} balanceSheet={mockBS} />
        );
        expect(screen.getByText(/Edit Balance Sheet/i)).toBeInTheDocument();
        expect(screen.getByText(/Cash: \$1,000/i)).toBeInTheDocument();
        expect(screen.getByText(/Loan: \$500/i)).toBeInTheDocument();
    });

    test('calculates and displays total balance correctly', () => {
        render(
            <BalanceSheetModal isOpen={true} onClose={mockOnClose} onSave={mockOnSave} balanceSheet={mockBS} />
        );
        // Assets(1000) - Liabilities(500) = 500
        expect(screen.getByText(/Total Balance: \$500/i)).toBeInTheDocument();
    });

    test('can add a new item', () => {
        render(
            <BalanceSheetModal isOpen={true} onClose={mockOnClose} onSave={mockOnSave} balanceSheet={mockBS} />
        );
        
        fireEvent.change(screen.getByPlaceholderText(/Item Name/i), { target: { value: 'Stocks' } });
        fireEvent.change(screen.getByPlaceholderText(/Value/i), { target: { value: '2000' } });
        // Default type is ASSET (0)
        
        // Find the Add button (it has FaPlus icon, but we can find it by its class or as the only button in that section)
        const addBtn = screen.getByRole('button', { name: '' }); // FaPlus has no text
        fireEvent.click(addBtn);
        
        expect(screen.getByText(/Stocks: \$2,000/i)).toBeInTheDocument();
        expect(screen.getByText(/Total Balance: \$2,500/i)).toBeInTheDocument();
    });

    test('calls onSave with updated sheet when Save Sheet is clicked', () => {
        render(
            <BalanceSheetModal isOpen={true} onClose={mockOnClose} onSave={mockOnSave} balanceSheet={mockBS} />
        );
        
        fireEvent.click(screen.getByText(/Save Sheet/i));
        expect(mockOnSave).toHaveBeenCalledWith(expect.objectContaining({
            items: expect.arrayContaining([
                expect.objectContaining({ itemName: 'Cash' }),
                expect.objectContaining({ itemName: 'Loan' })
            ])
        }));
    });
});
