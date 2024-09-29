import React from 'react';
import MenuBar from './myDashboardMenuBar';
import SideMenu from './myDashboardSideMenu';

const MainView = ({ children }) => {
    return (
        <div>
            <div>
            <MenuBar />
            </div>
            <div style={{ display: 'flex' }}>
                <SideMenu />
                <div style={{ marginLeft: '300px', padding: '10px' }}>
                    {children}
                </div>
            </div>
            <div>
                <h2> my Dashboard</h2>
                
            </div>
        </div>
       
    );
};

export default MainView;
