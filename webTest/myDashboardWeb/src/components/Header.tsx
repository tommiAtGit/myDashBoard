import React from 'react';

interface HeaderProps {
  onMenuClick: () => void;
}

const Header: React.FC<HeaderProps> = ({ onMenuClick }) => {
  return (
    <header className="app-header">
      <div className="header-left">
        <div className="menu-button" onClick={onMenuClick}>
          <i className="fas fa-bars menu-icon"></i>
        </div>
      </div>
      <div className="header-right">
        <div className="search-button">
          <i className="fas fa-search menu-icon"></i>
        </div>
        <div className="more-button">
          <i className="fas fa-ellipsis-v menu-icon"></i>
        </div>
      </div>
    </header>
  );
};

export default Header;
