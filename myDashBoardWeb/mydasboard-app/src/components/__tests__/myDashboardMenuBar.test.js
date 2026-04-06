import React from 'react';
import { render, screen } from '@testing-library/react';
import MenuBar from '../myDashboardMenuBar';

describe('MenuBar Component', () => {
    test('renders MenuBar with correct title', () => {
        render(<MenuBar />);
        const titleElement = screen.getByText(/my Dashboard/i);
        expect(titleElement).toBeInTheDocument();
    });
});
