import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import BudgetModal from '../myDashboardBudgetModal';

describe('BudgetModal Component', () => {
    const mockOnClose = jest.fn();
    const mockOnSave = jest.fn();
    const mockBudget = {
        id: '1',
        budgetAccount: 'FI123',
        budgetTitle: 'Food',
        budgetValue: 500,
        budgetStartDate: '2024-01-01T00:00:00.000Z',
        budgetEndDate: '2024-01-31T00:00:00.000Z'
    };

    test('does not render when isOpen is false', () => {
        const { queryByText } = render(
            <BudgetModal isOpen={false} onClose={mockOnClose} onSave={mockOnSave} />
        );
        expect(queryByText('Budget')).not.toBeInTheDocument();
    });

    test('renders with correct title when isOpen is true', () => {
        render(
            <BudgetModal isOpen={true} onClose={mockOnClose} onSave={mockOnSave} />
        );
        expect(screen.getByText('Budget')).toBeInTheDocument();
    });

    test('populates fields when budget is provided', () => {
        render(
            <BudgetModal isOpen={true} onClose={mockOnClose} onSave={mockOnSave} budget={mockBudget} />
        );
        expect(screen.getByPlaceholderText(/Enter account number/i).value).toBe('FI123');
        expect(screen.getByPlaceholderText(/Enter budget title/i).value).toBe('Food');
        expect(screen.getByPlaceholderText('0.00').value).toBe('500');
    });

    test('calls onSave with correct data when Save is clicked', () => {
        render(
            <BudgetModal isOpen={true} onClose={mockOnClose} onSave={mockOnSave} />
        );
        
        fireEvent.change(screen.getByPlaceholderText(/Enter account number/i), { target: { value: 'FI456' } });
        fireEvent.change(screen.getByPlaceholderText(/Enter budget title/i), { target: { value: 'Rent' } });
        fireEvent.change(screen.getByPlaceholderText('0.00'), { target: { value: '1200' } });
        
        fireEvent.click(screen.getByText(/Save/i));
        
        expect(mockOnSave).toHaveBeenCalledWith(expect.objectContaining({
            budgetAccount: 'FI456',
            budgetTitle: 'Rent',
            budgetValue: 1200
        }));
    });
});
