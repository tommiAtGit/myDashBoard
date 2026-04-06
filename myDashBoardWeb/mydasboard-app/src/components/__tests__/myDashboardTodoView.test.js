import React from 'react';
import { render, screen, waitFor, fireEvent } from '@testing-library/react';
import axios from 'axios';
import TodoView from '../myDashboardTodoView';

// axios is already mocked in setupTests.js
// but we want to provide specific implementations for this test

describe('TodoView Component', () => {
    const mockTasks = [
        { id: '1', name: 'Open Task', status: 1, description: 'Desc 1', dateReported: '2024-01-01' },
        { id: '2', name: 'Progress Task', status: 2, description: 'Desc 2', dateReported: '2024-01-01' },
        { id: '3', name: 'Done Task', status: 3, description: 'Desc 3', dateReported: '2024-01-01' }
    ];

    beforeEach(() => {
        jest.clearAllMocks();
        axios.get.mockImplementation((url) => {
            if (url.includes('taskByStatus/1')) return Promise.resolve({ data: [mockTasks[0]] });
            if (url.includes('taskByStatus/2')) return Promise.resolve({ data: [mockTasks[1]] });
            if (url.includes('taskByStatus/3')) return Promise.resolve({ data: [mockTasks[2]] });
            return Promise.resolve({ data: [] });
        });
    });

    test('renders tasks in correct columns after loading', async () => {
        render(<TodoView />);
        
        // Wait for all tasks to appear
        const openTask = await screen.findByText('Open Task');
        const progressTask = await screen.findByText('Progress Task');
        const doneTask = await screen.findByText('Done Task');

        expect(openTask).toBeInTheDocument();
        expect(progressTask).toBeInTheDocument();
        expect(doneTask).toBeInTheDocument();

        expect(screen.getByText('Open tasks')).toBeInTheDocument();
        expect(screen.getByText('Inprogress tasks')).toBeInTheDocument();
        expect(screen.getByText('Done tasks')).toBeInTheDocument();
    });

    test('opens modal when Add New Task is clicked', async () => {
        render(<TodoView />);
        
        await screen.findByText('Open tasks');

        fireEvent.click(screen.getByText(/Add New Task/i));
        const modalTitle = await screen.findByText('Task');
        expect(modalTitle).toBeInTheDocument();
    });
});
