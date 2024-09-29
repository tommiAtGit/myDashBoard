import React from 'react';

const MenuBar = () => {
    return (
        <div style={{ backgroundColor: '#333', padding: '10px', color: 'white' }}>
            <h1>my Dashboard</h1>
            <nav>
                <span style={{ marginRight: '20px' }}>Menu Item 1</span>
                <span>Menu Item 2</span>
            </nav>
        </div>
    );
};

export default MenuBar;