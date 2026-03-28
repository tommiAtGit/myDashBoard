import React, { useState } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Header from './components/Header';
import Sidebar from './components/Sidebar';
import Dashboard from './views/Dashboard';
import Todo from './views/Todo';
import Notes from './views/Notes';
import Finance from './views/Finance';
import './styles.css';

const App: React.FC = () => {
  const [sidebarOpen, setSidebarOpen] = useState(true);

  const toggleSidebar = () => {
    setSidebarOpen(!sidebarOpen);
  };

  return (
    <Router>
      <div className="app-container">
        <Header onMenuClick={toggleSidebar} />
        <Sidebar isOpen={sidebarOpen} />
        <main className={`main-content ${!sidebarOpen ? 'expanded' : ''}`}>
          <Routes>
            <Route path="/" element={<Dashboard />} />
            <Route path="/finance" element={<Finance />} />
            <Route path="/notes" element={<Notes />} />
            <Route path="/todo" element={<Todo />} />
          </Routes>
        </main>
      </div>
    </Router>
  );
};

export default App;
