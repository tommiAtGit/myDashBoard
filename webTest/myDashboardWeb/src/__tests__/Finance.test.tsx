import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import Finance from '../views/Finance';

describe('Finance Component', () => {
  test('renders finance container', () => {
    render(<Finance />);
    const container = document.querySelector('.finance-container');
    expect(container).toBeInTheDocument();
  });

  test('renders budget and balance tabs', () => {
    render(<Finance />);
    const budgetTab = screen.getByText(/Current Budget/i);
    const balanceTab = screen.getByText(/^Balance$/);
    
    expect(budgetTab).toBeInTheDocument();
    expect(balanceTab).toBeInTheDocument();
  });

  test('displays current budget by default', () => {
    render(<Finance />);
    const accountCards = document.querySelector('.account-cards');
    expect(accountCards).toBeInTheDocument();
  });

  test('switches to balance tab', () => {
    render(<Finance />);
    const balanceTab = screen.getByText(/^Balance$/);
    
    fireEvent.click(balanceTab);
    
    const balanceSummary = document.querySelector('.balance-summary');
    expect(balanceSummary).toBeInTheDocument();
  });

  test('renders account cards in budget tab', () => {
    render(<Finance />);
    const currentAccountCard = screen.getByText(/Current Account Balance/i);
    const savingsAccountCard = screen.getByText(/Savings Account Balance/i);
    
    expect(currentAccountCard).toBeInTheDocument();
    expect(savingsAccountCard).toBeInTheDocument();
  });

  test('renders stocks section in budget tab', () => {
    render(<Finance />);
    const stocksSection = screen.getByText(/^Stocks$/);
    expect(stocksSection).toBeInTheDocument();
  });

  test('renders funds section in budget tab', () => {
    render(<Finance />);
    const fundsSection = screen.getByText(/^Funds$/);
    expect(fundsSection).toBeInTheDocument();
  });

  test('renders assets and liabilities in balance tab', () => {
    render(<Finance />);
    const balanceTab = screen.getByText(/^Balance$/);
    fireEvent.click(balanceTab);
    
    const assetsSection = screen.getByText(/^Assets$/);
    const liabilitiesSection = screen.getByText(/Liabilities/i);
    
    expect(assetsSection).toBeInTheDocument();
    expect(liabilitiesSection).toBeInTheDocument();
  });

  test('renders add buttons', () => {
    render(<Finance />);
    const addButtons = document.querySelectorAll('.add-item-button');
    expect(addButtons.length).toBeGreaterThan(0);
  });
});
