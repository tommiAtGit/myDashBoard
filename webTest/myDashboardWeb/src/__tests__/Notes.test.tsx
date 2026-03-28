import React from 'react';
import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import Notes from '../views/Notes';

describe('Notes Component', () => {
  test('renders notes container', () => {
    render(<Notes />);
    const container = document.querySelector('.notes-container');
    expect(container).toBeInTheDocument();
  });

  test('renders new document button', () => {
    render(<Notes />);
    const newDocButton = screen.getByText(/New Document/i);
    expect(newDocButton).toBeInTheDocument();
  });

  test('opens modal when new document button is clicked', async () => {
    render(<Notes />);
    const newDocButton = screen.getByText(/New Document/i);
    
    fireEvent.click(newDocButton);
    
    await waitFor(() => {
      const modal = document.querySelector('.modal-overlay');
      expect(modal).toBeInTheDocument();
    });
  });

  test('modal contains document title input', async () => {
    render(<Notes />);
    const newDocButton = screen.getByText(/New Document/i);
    
    fireEvent.click(newDocButton);
    
    await waitFor(() => {
      const titleInput = screen.getByPlaceholderText(/Enter document title/i);
      expect(titleInput).toBeInTheDocument();
    });
  });

  test('renders search input', () => {
    render(<Notes />);
    const searchInput = document.querySelector('.search-input');
    expect(searchInput).toBeInTheDocument();
  });

  test('renders notes and recall tabs', () => {
    render(<Notes />);
    const notesTab = screen.getByText(/^Notes$/);
    const recallTab = screen.getByText(/Recall/i);
    
    expect(notesTab).toBeInTheDocument();
    expect(recallTab).toBeInTheDocument();
  });

  test('switches between notes and recall tabs', () => {
    render(<Notes />);
    const recallTab = screen.getByText(/Recall/i);
    
    fireEvent.click(recallTab);
    
    const recallContent = screen.getByText(/Recall from memory/i);
    expect(recallContent).toBeInTheDocument();
  });

  test('recall tab contains required fields', () => {
    render(<Notes />);
    const recallTab = screen.getByText(/Recall/i);
    
    fireEvent.click(recallTab);
    
    expect(screen.getByText(/Recall from memory/i)).toBeInTheDocument();
    expect(screen.getByText(/Add missing information/i)).toBeInTheDocument();
    expect(screen.getByText(/Recall questions/i)).toBeInTheDocument();
  });

  test('renders notes list container', () => {
    render(<Notes />);
    const listContainer = document.querySelector('.notes-list-container');
    expect(listContainer).toBeInTheDocument();
  });

  test('renders note detail container', () => {
    render(<Notes />);
    const detailContainer = document.querySelector('.note-detail-container');
    expect(detailContainer).toBeInTheDocument();
  });
});
