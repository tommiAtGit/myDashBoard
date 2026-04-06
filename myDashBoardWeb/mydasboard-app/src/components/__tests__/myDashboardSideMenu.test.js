import React from 'react';
import { render, screen } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import SideMenu from '../myDashboardSideMenu';

describe('SideMenu Component', () => {
    test('renders SideMenu with navigation links', () => {
        render(
            <MemoryRouter>
                <SideMenu />
            </MemoryRouter>
        );
        
        expect(screen.getByText(/myDasboard/i)).toBeInTheDocument();
        expect(screen.getByText(/my Finance/i)).toBeInTheDocument();
        expect(screen.getByText(/my Todo/i)).toBeInTheDocument();
        expect(screen.getByText(/my Notes/i)).toBeInTheDocument();
    });

    test('links have correct "to" attributes', () => {
        render(
            <MemoryRouter>
                <SideMenu />
            </MemoryRouter>
        );
        
        expect(screen.getByText(/myDasboard/i).closest('a')).toHaveAttribute('href', '/DashboardView');
        expect(screen.getByText(/my Finance/i).closest('a')).toHaveAttribute('href', '/FinanceView');
        expect(screen.getByText(/my Todo/i).closest('a')).toHaveAttribute('href', '/TodoView');
        expect(screen.getByText(/my Notes/i).closest('a')).toHaveAttribute('href', '/NotesView');
    });
});
