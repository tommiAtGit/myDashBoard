import React from 'react';
import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import Todo from '../views/Todo';

describe('Todo Component', () => {
  test('renders todo container', () => {
    render(<Todo />);
    const container = document.querySelector('.todo-container');
    expect(container).toBeInTheDocument();
  });

  test('renders new task button', () => {
    render(<Todo />);
    const newTaskButton = screen.getByText(/New Task/i);
    expect(newTaskButton).toBeInTheDocument();
  });

  test('renders task columns', () => {
    render(<Todo />);
    const openTasks = screen.getByText(/Open Tasks/i);
    const inProgressTasks = screen.getByText(/In Progress Tasks/i);
    const doneTasks = screen.getByText(/Done Tasks/i);
    
    expect(openTasks).toBeInTheDocument();
    expect(inProgressTasks).toBeInTheDocument();
    expect(doneTasks).toBeInTheDocument();
  });

  test('opens task modal when new task button is clicked', async () => {
    render(<Todo />);
    const newTaskButton = screen.getByText(/New Task/i);
    fireEvent.click(newTaskButton);
    
    await waitFor(() => {
      const modal = document.querySelector('.modal-overlay');
      expect(modal).toBeInTheDocument();
    });
  });

  test('renders task board', () => {
    render(<Todo />);
    const todoBoard = document.querySelector('.todo-board');
    expect(todoBoard).toBeInTheDocument();
  });
});
