import React from 'react';
import { render, screen } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import MainView from '../myDashbordMainView';

describe('MainView Component', () => {
    test('renders MainView with MenuBar, SideMenu and children', () => {
        render(
            <MemoryRouter>
                <MainView>
                    <div data-testid="child-element">Test Child</div>
                </MainView>
            </MemoryRouter>
        );
        
        expect(screen.getByText(/my Dashboard/i)).toBeInTheDocument();
        expect(screen.getByText(/myDasboard/i)).toBeInTheDocument();
        expect(screen.getByTestId('child-element')).toBeInTheDocument();
    });
});
