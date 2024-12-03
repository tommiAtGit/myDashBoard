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
                <div style={{height:'100%', marginLeft: '10px', padding: '10px' }}>
                    {children}
                </div>
            </div>
        </div>
       
    );
};

export default MainView;
