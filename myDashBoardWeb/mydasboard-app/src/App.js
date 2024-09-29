import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import MainView from './components/myDashbordMainView';
import DasboardView from './components/myDashboardView';
import FinanceView from './components/myDashboardFinanceView';
import TodoView from './components/myDashboardTodoView';
import NotesView from './components/myDashboardNotesView';

function App() {
    return (
        <Router>
            <MainView>
                <Routes>
                    <Route path="/DashboardView" element={<DasboardView />} />
                    <Route path="/FinanceView" element={<FinanceView />} />
                    <Route path="/TodoView" element={<TodoView />} />
                    <Route path="/NotesView" element={<NotesView />} />
                </Routes>
            </MainView>
        </Router>
        
    );
}

export default App;