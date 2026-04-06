import React from 'react';
import { render, screen, waitFor, fireEvent } from '@testing-library/react';
import axios from 'axios';
import NotesView from '../myDashboardNotesView';

jest.mock('axios');

describe('NotesView Component', () => {
    const mockNotes = [
        { id: '1', notesTiltle: 'Note 1', notes: 'Content 1', owner: 'User', dateCreatad: '2024-01-01', keyWords: ['test'] }
    ];

    beforeEach(() => {
        axios.get.mockResolvedValue({ data: mockNotes });
    });

    test('renders Notes tab by default', async () => {
        render(<NotesView />);
        
        await waitFor(() => {
            expect(screen.getAllByText('Note 1').length).toBeGreaterThan(0);
        });

        // Use more specific selector for the tab
        const notesTab = screen.getAllByText('Notes').find(el => el.classList.contains('tab-item'));
        expect(notesTab).toHaveClass('active');
        expect(screen.getByText('Summary')).toBeInTheDocument();
    });

    test('switches to Recall tab', async () => {
        render(<NotesView />);
        
        await waitFor(() => {
            expect(screen.getAllByText('Note 1').length).toBeGreaterThan(0);
        });

        fireEvent.click(screen.getByText('Recall', { selector: '.tab-item' }));
        
        const recallTab = screen.getAllByText('Recall').find(el => el.classList.contains('tab-item'));
        expect(recallTab).toHaveClass('active');
        expect(screen.getByText('Recall from memory')).toBeInTheDocument();
    });

    test('opens new document when button clicked', async () => {
        render(<NotesView />);
        
        await waitFor(() => {
            expect(screen.getAllByText('Note 1').length).toBeGreaterThan(0);
        });

        fireEvent.click(screen.getByText('New Document'));
        
        // The list should now have "New Document"
        expect(screen.getAllByText('New Document').length).toBeGreaterThan(0);
    });
});
