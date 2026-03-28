import React from 'react';

const Dashboard: React.FC = () => {
  return (
    <div className="dashboard-container">
      <div className="empty-state">
        <div className="empty-state-icon">
          <i className="fas fa-home"></i>
        </div>
        <div className="empty-state-text">Welcome to Your Dashboard</div>
        <div className="empty-state-subtext">
          Navigate using the sidebar to access different sections
        </div>
      </div>
    </div>
  );
};

export default Dashboard;
