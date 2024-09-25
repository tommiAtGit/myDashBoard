import React from 'react';
import { Link } from 'react-router-dom';
import MenuBar from './myDashboardMenuBar';
import SideMenu from './myDashboardSideMenu';

const MainView = ({ children }) => {
    return (
        <div>
            <MenuBar />
            <div style={{ display: 'flex' }}>
                <SideMenu />
                <div style={{ marginLeft: '300px', padding: '10px' }}>
                    {children}
                </div>
            </div>
        </div>
    );
};

export default MainView;
