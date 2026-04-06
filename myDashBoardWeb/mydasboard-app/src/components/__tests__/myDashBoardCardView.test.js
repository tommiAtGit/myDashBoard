import React from 'react';
import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import axios from 'axios';
import Card from '../myDashBoardCardView';

jest.mock('axios');

describe('Card Component', () => {
    const mockProps = {
        id: '1',
        name: 'Test Card',
        dateReported: '2024-01-01T00:00:00.000Z',
        description: 'Test Description',
        onEdit: jest.fn(),
        onDelete: jest.fn()
    };

    test('renders card details', () => {
        render(<Card {...mockProps} />);
        expect(screen.getByText('Test Card')).toBeInTheDocument();
        expect(screen.getByText('Test Description')).toBeInTheDocument();
        // Date format helper should convert to 01.01.2024
        expect(screen.getByText('01.01.2024')).toBeInTheDocument();
    });

    test('calls GetTaskById and onEdit when pen icon clicked', async () => {
        const mockTask = { id: '1', name: 'Test Card Full' };
        axios.get.mockResolvedValueOnce({ status: 200, data: mockTask });
        
        render(<Card {...mockProps} />);
        
        // Find pen icon (first svg)
        const icons = document.querySelectorAll('svg');
        fireEvent.click(icons[0]);
        
        await waitFor(() => {
            expect(axios.get).toHaveBeenCalledWith(expect.stringContaining('/1'));
            expect(mockProps.onEdit).toHaveBeenCalledWith(mockTask);
        });
    });

    test('calls axios.delete and onDelete when trash icon clicked', async () => {
        axios.delete.mockResolvedValueOnce({ status: 204 });
        
        render(<Card {...mockProps} />);
        
        // Find trash icon (second svg)
        const icons = document.querySelectorAll('svg');
        fireEvent.click(icons[1]);
        
        await waitFor(() => {
            expect(axios.delete).toHaveBeenCalledWith(expect.stringContaining('/delete/1'));
            expect(mockProps.onDelete).toHaveBeenCalledWith('1');
        });
    });
});
