import React from 'react';
import { Link, useLocation } from 'react-router-dom';

interface SidebarProps {
  isOpen: boolean;
}

const Sidebar: React.FC<SidebarProps> = ({ isOpen }) => {
  const location = useLocation();

  const navItems = [
    { path: '/', label: 'Dashboard', icon: 'fa-home' },
    { path: '/finance', label: 'Finance', icon: 'fa-dollar-sign' },
    { path: '/notes', label: 'Notes', icon: 'fa-pencil-alt' },
    { path: '/todo', label: 'Todo', icon: 'fa-wrench' }
  ];

  return (
    <aside className={`sidebar ${!isOpen ? 'closed' : ''}`}>
      <nav className="sidebar-nav">
        {navItems.map((item) => (
          <Link
            key={item.path}
            to={item.path}
            className={`nav-item ${location.pathname === item.path ? 'active' : ''}`}
          >
            <i className={`fas ${item.icon} nav-icon`}></i>
            <span className="nav-text">{item.label}</span>
          </Link>
        ))}
      </nav>
    </aside>
  );
};

export default Sidebar;
