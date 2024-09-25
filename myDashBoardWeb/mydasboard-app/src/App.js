import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import MainView from './components/myDashbordMainView';
import FinanceView from './components/myDashboardFinanceView';
import TodoView from './components/myDashboardTodoView';

function App() {
    return (
        <Router>
            <MainView>
                <Routes>
                    <Route path="/FinanceView" element={<FinanceView />} />
                    <Route path="/TodoView" element={<TodoView />} />
                </Routes>
            </MainView>
        </Router>
    );
}

export default App;