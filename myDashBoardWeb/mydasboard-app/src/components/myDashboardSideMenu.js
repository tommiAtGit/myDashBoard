import React from 'react';
import { Link } from 'react-router-dom';
import FinanceView from './myDashboardFinanceView';
import TodoView from './myDashboardTodoView';

const SideMenu = () => {
    return (
        <div style={{
            width: '200px',
            backgroundColor: '#f4f4f4',
            padding: '20px',
            position: 'fixed',
            height: '100%',
        }}>
            <ul style={{ listStyle: 'none', padding: '0' }}>
                <li><Link to="/FinanceView">my Finance</Link></li>
                <li><Link to="/TodoView">my Todo</Link></li>
            </ul>
        </div>
    );
};

export default SideMenu;
