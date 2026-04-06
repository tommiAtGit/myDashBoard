import React, { useState, useEffect, useMemo } from 'react';
import axios from 'axios';
import { FaPen, FaTrash } from 'react-icons/fa';
import FinanceModal from './myDashboardFinanceModal';
import BudgetModal from './myDashboardBudgetModal';
import BalanceSheetModal from './myDashboardBalanceSheetModal';
import './../App.css';

const FinanceView = () => {
    const [balanceSheets, setBalanceSheets] = useState([]);
    const [transactions, setTransactions] = useState([]);
    const [budgets, setBudgets] = useState([]);
    const [selectedAccount, setSelectedAccount] = useState('');
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    // Modal states
    const [isFinanceModalOpen, setFinanceModalOpen] = useState(false);
    const [isBudgetModalOpen, setBudgetModalOpen] = useState(false);
    const [isBSModalOpen, setBSModalOpen] = useState(false);
    const [editingTransaction, setEditingTransaction] = useState(null);
    const [editingBudget, setEditingBudget] = useState(null);

    const baseUrl = "http://localhost:5002/api";

    useEffect(() => {
        fetchInitialData();
    }, []);

    useEffect(() => {
        if (selectedAccount) {
            fetchAccountData(selectedAccount);
        }
    }, [selectedAccount]);

    const fetchInitialData = async () => {
        setLoading(true);
        try {
            console.log("Fetching BalanceSheet from:", `${baseUrl}/BalanceSheet`);
            console.log("Fetching FinanceTracker from:", `${baseUrl}/FinanceTracker`);
            const [bsRes, transRes] = await Promise.all([
                axios.get(`${baseUrl}/BalanceSheet`),
                axios.get(`${baseUrl}/FinanceTracker`)
            ]);
            console.log("BalanceSheet Response:", bsRes.data);
            console.log("FinanceTracker Response:", transRes.data);
            setBalanceSheets(bsRes.data);
            
            // Extract unique accounts from transactions to populate selector
            const allTrans = transRes.data;
            const uniqueAccounts = [...new Set(allTrans.map(t => t.account))];
            console.log("Unique Accounts found:", uniqueAccounts);
            if (uniqueAccounts.length > 0 && !selectedAccount) {
                setSelectedAccount(uniqueAccounts[0]);
            }
        } catch (err) {
            console.error("Error fetching initial finance data:", err);
            if (err.response) {
                console.error("Response data:", err.response.data);
                console.error("Response status:", err.response.status);
            }
            setError("Failed to load data: " + (err.message || "Unknown error"));
        } finally {
            setLoading(false);
        }
    };

    const fetchAccountData = async (account) => {
        try {
            const [transRes, budgetRes] = await Promise.all([
                axios.get(`${baseUrl}/FinanceTracker/account/${account}`),
                axios.get(`${baseUrl}/Budget/account/${account}`).catch(() => ({ data: [] }))
            ]);
            setTransactions(transRes.data);
            setBudgets(budgetRes.data || []);
        } catch (err) {
            console.error("Error fetching account specific data:", err);
        }
    };

    // Calculated balance: Deposits - Withdrawals
    const calculatedBalance = useMemo(() => {
        return transactions.reduce((acc, curr) => {
            if (curr.type === 1) return acc + curr.amount; // DEPOSIT
            if (curr.type === 2) return acc - curr.amount; // WITHDRAWAL
            return acc;
        }, 0);
    }, [transactions]);

    // Balance Sheet Totals
    const bsTotals = useMemo(() => {
        if (balanceSheets.length === 0) return { assets: 0, liabilities: 0, balance: 0 };
        // Using the first balance sheet for simplicity in this prototype
        const latestBS = balanceSheets[0];
        const assets = latestBS.items.filter(i => i.type === 0).reduce((sum, i) => sum + i.value, 0);
        const liabilities = latestBS.items.filter(i => i.type === 1).reduce((sum, i) => sum + i.value, 0);
        return {
            assets,
            liabilities,
            balance: assets - liabilities
        };
    }, [balanceSheets]);

    const handleSaveTransaction = async (trans) => {
        try {
            if (editingTransaction) {
                await axios.put(`${baseUrl}/FinanceTracker/${trans.id}`, trans);
            } else {
                await axios.post(`${baseUrl}/FinanceTracker`, trans);
            }
            fetchAccountData(selectedAccount);
            setFinanceModalOpen(false);
            setEditingTransaction(null);
        } catch (err) {
            console.error("Error saving transaction:", err);
        }
    };

    const handleSaveBudget = async (budget) => {
        try {
            if (editingBudget) {
                await axios.put(`${baseUrl}/Budget/id?id=${budget.id}`, budget);
            } else {
                await axios.post(`${baseUrl}/Budget`, budget);
            }
            fetchAccountData(selectedAccount);
            setBudgetModalOpen(false);
            setEditingBudget(null);
        } catch (err) {
            console.error("Error saving budget:", err);
        }
    };

    const handleSaveBalanceSheet = async (updatedSheet) => {
        try {
            await axios.put(`${baseUrl}/BalanceSheet/${updatedSheet.id}`, updatedSheet);
            setBalanceSheets(balanceSheets.map(bs => bs.id === updatedSheet.id ? updatedSheet : bs));
            setBSModalOpen(false);
        } catch (err) {
            console.error("Error saving balance sheet:", err);
        }
    };

    const handleDeleteTransaction = async (id) => {
        if (!window.confirm("Delete this transaction?")) return;
        try {
            await axios.delete(`${baseUrl}/FinanceTracker/Id?Id=${id}`);
            fetchAccountData(selectedAccount);
        } catch (err) {
            console.error("Error deleting transaction:", err);
        }
    };

    if (loading) return <div>Loading Finance...</div>;
    if (error) return <div className="error-box">{error}</div>;

    return (
        <div className="finance-container">
            <h2>Financial Dashboard</h2>

            {/* 1. Balance Sheet Section */}
            <section className="finance-section">
                <div className="new-doc-btn-container" style={{ position: 'relative', marginBottom: '15px' }}>
                    <h3 style={{ margin: 0, border: 'none', position: 'absolute', left: 0 }}>Balance Sheet Overview</h3>
                    <div style={{ width: '30%', visibility: 'hidden' }}></div>
                    <button className="add-btn" onClick={() => setBSModalOpen(true)}>
                        Edit Balance Sheet
                    </button>
                </div>
                <div className="balance-sheet-grid">
                    <div className="bs-card asset">
                        <h4>Assets</h4>
                        <p className="balance-display">${bsTotals.assets.toLocaleString()}</p>
                    </div>
                    <div className="bs-card liability">
                        <h4>Liabilities</h4>
                        <p className="balance-display">${bsTotals.liabilities.toLocaleString()}</p>
                    </div>
                    <div className="bs-card balance">
                        <h4>Total Balance</h4>
                        <p className="balance-display">${bsTotals.balance.toLocaleString()}</p>
                    </div>
                </div>
            </section>

            {/* 2. Finance Section (Transactions) */}
            <section className="finance-section">
                <div className="new-doc-btn-container" style={{ position: 'relative', marginBottom: '15px' }}>
                    <h3 style={{ margin: 0, border: 'none', position: 'absolute', left: 0 }}>Finance - Transactions</h3>
                    <div style={{ width: '30%', visibility: 'hidden' }}></div>
                    <button className="add-btn" onClick={() => { setEditingTransaction(null); setFinanceModalOpen(true); }}>
                        Add Transaction
                    </button>
                </div>
                
                <div className="account-selector">
                    <label>Selected Account: </label>
                    <select value={selectedAccount} onChange={(e) => setSelectedAccount(e.target.value)}>
                        {[...new Set(transactions.map(t => t.account))].map(acc => (
                            <option key={acc} value={acc}>{acc}</option>
                        ))}
                    </select>
                </div>

                <table className="finance-table">
                    <thead>
                        <tr>
                            <th>Date</th>
                            <th>Description</th>
                            <th>Type</th>
                            <th>Amount</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {transactions.map(t => (
                            <tr key={t.id}>
                                <td>{new Date(t.actionDate).toLocaleDateString()}</td>
                                <td>{t.description}</td>
                                <td>{t.type === 1 ? 'DEPOSIT' : t.type === 2 ? 'WITHDRAWAL' : 'OTHER'}</td>
                                <td style={{ color: t.type === 2 ? 'red' : 'green' }}>
                                    {t.type === 2 ? '-' : '+'}${t.amount.toLocaleString()}
                                </td>
                                <td>
                                    <FaPen onClick={() => { setEditingTransaction(t); setFinanceModalOpen(true); }} style={{ cursor: 'pointer', marginRight: '10px' }} />
                                    <FaTrash onClick={() => handleDeleteTransaction(t.id)} style={{ cursor: 'pointer' }} />
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </section>

            {/* 3. Balance Section */}
            <section className="finance-section">
                <h3>Account Balance (Calculated)</h3>
                <p>Account: <strong>{selectedAccount}</strong></p>
                <p className="balance-display">${calculatedBalance.toLocaleString()}</p>
            </section>

            {/* 4. Budget Section */}
            <section className="finance-section">
                <div className="new-doc-btn-container" style={{ position: 'relative', marginBottom: '15px' }}>
                    <h3 style={{ margin: 0, border: 'none', position: 'absolute', left: 0 }}>Budget Planning</h3>
                    <div style={{ width: '30%', visibility: 'hidden' }}></div>
                    <button className="add-btn" onClick={() => { setEditingBudget(null); setBudgetModalOpen(true); }}>
                        Add Budget Item
                    </button>
                </div>

                <table className="budget-table">
                    <thead>
                        <tr>
                            <th>Title</th>
                            <th>Value</th>
                            <th>Start Date</th>
                            <th>End Date</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {budgets.map(b => (
                            <tr key={b.id}>
                                <td>{b.budgetTitle}</td>
                                <td>${b.budgetValue.toLocaleString()}</td>
                                <td>{new Date(b.budgetStartDate).toLocaleDateString()}</td>
                                <td>{new Date(b.budgetEndDate).toLocaleDateString()}</td>
                                <td>
                                    <FaPen onClick={() => { setEditingBudget(b); setBudgetModalOpen(true); }} style={{ cursor: 'pointer' }} />
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </section>

            {/* Modals */}
            <FinanceModal 
                isOpen={isFinanceModalOpen} 
                onClose={() => setFinanceModalOpen(false)} 
                onSave={handleSaveTransaction}
                transaction={editingTransaction}
            />
            <BudgetModal 
                isOpen={isBudgetModalOpen} 
                onClose={() => setBudgetModalOpen(false)} 
                onSave={handleSaveBudget}
                budget={editingBudget}
            />
            <BalanceSheetModal
                isOpen={isBSModalOpen}
                onClose={() => setBSModalOpen(false)}
                onSave={handleSaveBalanceSheet}
                balanceSheet={balanceSheets[0]}
            />
        </div>
    );
};

export default FinanceView;
