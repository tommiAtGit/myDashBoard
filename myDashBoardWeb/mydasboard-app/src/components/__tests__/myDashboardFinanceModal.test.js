import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import FinanceModal from '../myDashboardFinanceModal';

describe('FinanceModal Component', () => {
    const mockOnClose = jest.fn();
    const mockOnSave = jest.fn();
    const mockTransaction = {
        id: '1',
        account: 'FI123',
        type: 1,
        description: 'Test Transaction',
        amount: 100.50,
        actionDate: '2024-01-01T00:00:00.000Z'
    };

    test('does not render when isOpen is false', () => {
        const { queryByText } = render(
            <FinanceModal isOpen={false} onClose={mockOnClose} onSave={mockOnSave} />
        );
        expect(queryByText(/Transaction/i)).not.toBeInTheDocument();
    });

    test('renders with correct title when isOpen is true', () => {
        render(
            <FinanceModal isOpen={true} onClose={mockOnClose} onSave={mockOnSave} />
        );
        expect(screen.getByText('Transaction')).toBeInTheDocument();
    });

    test('populates fields when transaction is provided', () => {
        render(
            <FinanceModal isOpen={true} onClose={mockOnClose} onSave={mockOnSave} transaction={mockTransaction} />
        );
        expect(screen.getByPlaceholderText(/Enter account number/i).value).toBe('FI123');
        expect(screen.getByPlaceholderText(/Enter description/i).value).toBe('Test Transaction');
        expect(screen.getByPlaceholderText('0.00').value).toBe('100.5');
    });

    test('calls onClose when Cancel is clicked', () => {
        render(
            <FinanceModal isOpen={true} onClose={mockOnClose} onSave={mockOnSave} />
        );
        fireEvent.click(screen.getByText(/Cancel/i));
        expect(mockOnClose).toHaveBeenCalled();
    });

    test('calls onSave with correct data when Save is clicked', () => {
        render(
            <FinanceModal isOpen={true} onClose={mockOnClose} onSave={mockOnSave} />
        );
        
        fireEvent.change(screen.getByPlaceholderText(/Enter account number/i), { target: { value: 'FI999' } });
        fireEvent.change(screen.getByPlaceholderText(/Enter description/i), { target: { value: 'Salary' } });
        fireEvent.change(screen.getByPlaceholderText('0.00'), { target: { value: '5000' } });
        
        fireEvent.click(screen.getByRole('button', { name: /Save/i }));
        
        expect(mockOnSave).toHaveBeenCalledWith(expect.objectContaining({
            account: 'FI999',
            description: 'Salary',
            amount: 5000
        }));
    });
});
