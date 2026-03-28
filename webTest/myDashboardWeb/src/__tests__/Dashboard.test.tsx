import React from 'react';
import { render, screen } from '@testing-library/react';
import Dashboard from '../views/Dashboard';

describe('Dashboard Component', () => {
  test('renders dashboard container', () => {
    render(<Dashboard />);
    const container = document.querySelector('.dashboard-container');
    expect(container).toBeInTheDocument();
  });

  test('displays welcome message', () => {
    render(<Dashboard />);
    const welcomeText = screen.getByText(/Welcome to Your Dashboard/i);
    expect(welcomeText).toBeInTheDocument();
  });

  test('displays empty state', () => {
    render(<Dashboard />);
    const emptyState = document.querySelector('.empty-state');
    expect(emptyState).toBeInTheDocument();
  });
});
