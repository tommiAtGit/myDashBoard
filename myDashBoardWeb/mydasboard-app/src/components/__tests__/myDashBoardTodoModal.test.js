import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import TodoModal from '../myDashBoardTodoModal';

describe('TodoModal Component', () => {
    const mockOnClose = jest.fn();
    const mockOnSave = jest.fn();
    const mockTask = {
        id: '1',
        name: 'Test Task',
        description: 'Test Description',
        dateReported: '2024-01-01T00:00:00.000Z',
        status: 1,
        owner: 'User1',
        reporter: 'User2'
    };

    test('does not render when isOpen is false', () => {
        const { queryByText } = render(
            <TodoModal isOpen={false} onClose={mockOnClose} onSave={mockOnSave} />
        );
        expect(queryByText(/Task/i)).not.toBeInTheDocument();
    });

    test('renders with correct title when isOpen is true', () => {
        render(
            <TodoModal isOpen={true} onClose={mockOnClose} onSave={mockOnSave} />
        );
        expect(screen.getByText('Task')).toBeInTheDocument();
    });

    test('populates fields when task is provided', () => {
        render(
            <TodoModal isOpen={true} onClose={mockOnClose} onSave={mockOnSave} task={mockTask} />
        );
        expect(screen.getByPlaceholderText(/Enter task title/i).value).toBe('Test Task');
        expect(screen.getByPlaceholderText(/Enter task description/i).value).toBe('Test Description');
        expect(screen.getByPlaceholderText(/Assigned To/i).value).toBe('User1');
        expect(screen.getByPlaceholderText(/Created By/i).value).toBe('User2');
    });

    test('calls onClose when Cancel is clicked', () => {
        render(
            <TodoModal isOpen={true} onClose={mockOnClose} onSave={mockOnSave} />
        );
        fireEvent.click(screen.getByText(/Cancel/i));
        expect(mockOnClose).toHaveBeenCalled();
    });

    test('calls onSave with correct data when Save is clicked', () => {
        render(
            <TodoModal isOpen={true} onClose={mockOnClose} onSave={mockOnSave} />
        );
        
        fireEvent.change(screen.getByPlaceholderText(/Enter task title/i), { target: { value: 'New Task' } });
        fireEvent.change(screen.getByPlaceholderText(/Enter task description/i), { target: { value: 'New Description' } });
        
        fireEvent.click(screen.getByText(/Save/i));
        
        expect(mockOnSave).toHaveBeenCalledWith(expect.objectContaining({
            name: 'New Task',
            description: 'New Description'
        }));
    });
});
