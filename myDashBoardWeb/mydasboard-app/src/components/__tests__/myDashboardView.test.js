import React from 'react';
import { render, screen } from '@testing-library/react';
import DashboardView from '../myDashboardView';

describe('DashboardView Component', () => {
    test('renders DashboardView with title and placeholder text', () => {
        render(<DashboardView />);
        expect(screen.getByText(/Dashboard/i)).toBeInTheDocument();
        expect(screen.getByText(/Add some usefull dasboards here/i)).toBeInTheDocument();
    });
});
