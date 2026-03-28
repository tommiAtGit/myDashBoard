import React from 'react';
import { render, screen } from '@testing-library/react';
import App from '../App';

describe('App Component', () => {
  test('renders without crashing', () => {
    render(<App />);
  });

  test('renders header', () => {
    render(<App />);
    const menuButton = document.querySelector('.menu-button');
    expect(menuButton).toBeInTheDocument();
  });

  test('renders sidebar', () => {
    render(<App />);
    const sidebar = document.querySelector('.sidebar');
    expect(sidebar).toBeInTheDocument();
  });

  test('renders main content area', () => {
    render(<App />);
    const mainContent = document.querySelector('.main-content');
    expect(mainContent).toBeInTheDocument();
  });
});
