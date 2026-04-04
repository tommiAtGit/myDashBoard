import React, { useState, useEffect } from "react";
import "./../todoModal.css";

const BudgetModal = ({ isOpen, onClose, onSave, budget }) => {
    const [id, setId] = useState(crypto.randomUUID());
    const [budgetAccount, setBudgetAccount] = useState("");
    const [budgetTitle, setBudgetTitle] = useState("");
    const [budgetValue, setBudgetValue] = useState(0);
    const [startDate, setStartDate] = useState(new Date().toISOString().split('T')[0]);
    const [endDate, setEndDate] = useState(new Date().toISOString().split('T')[0]);

    useEffect(() => {
        if (budget && isOpen) {
            setId(budget.id || crypto.randomUUID());
            setBudgetAccount(budget.budgetAccount || "");
            setBudgetTitle(budget.budgetTitle || "");
            setBudgetValue(budget.budgetValue || 0);
            setStartDate(budget.budgetStartDate ? budget.budgetStartDate.split('T')[0] : new Date().toISOString().split('T')[0]);
            setEndDate(budget.budgetEndDate ? budget.budgetEndDate.split('T')[0] : new Date().toISOString().split('T')[0]);
        } else if (isOpen) {
            setId(crypto.randomUUID());
            setBudgetAccount("");
            setBudgetTitle("");
            setBudgetValue(0);
            setStartDate(new Date().toISOString().split('T')[0]);
            setEndDate(new Date().toISOString().split('T')[0]);
        }
    }, [budget, isOpen]);

    const handleSave = () => {
        const newBudget = {
            id,
            budgetAccount,
            budgetTitle,
            budgetValue: parseFloat(budgetValue),
            budgetStartDate: new Date(startDate).toISOString(),
            budgetEndDate: new Date(endDate).toISOString(),
        };
        onSave(newBudget);
        onClose();
    };

    if (!isOpen) return null;

    return (
        <div className="modal-overlay">
            <div className="modal-container">
                <h2 className="modal-title">Budget</h2>

                {/* Account Input */}
                <input
                    type="text"
                    placeholder="Enter account number"
                    value={budgetAccount}
                    onChange={(e) => setBudgetAccount(e.target.value)}
                    className="modal-input"
                />

                {/* Title Input */}
                <input
                    type="text"
                    placeholder="Enter budget title"
                    value={budgetTitle}
                    onChange={(e) => setBudgetTitle(e.target.value)}
                    className="modal-input"
                />

                {/* Value Input */}
                <input
                    type="number"
                    placeholder="0.00"
                    value={budgetValue}
                    onChange={(e) => setBudgetValue(e.target.value)}
                    className="modal-input"
                />

                {/* Start Date Input */}
                <label className="area-header">Start Date
                    <input
                        type="date"
                        value={startDate}
                        onChange={(e) => setStartDate(e.target.value)}
                        className="modal-input"
                    />
                </label>

                {/* End Date Input */}
                <label className="area-header">End Date
                    <input
                        type="date"
                        value={endDate}
                        onChange={(e) => setEndDate(e.target.value)}
                        className="modal-input"
                    />
                </label>

                {/* Buttons */}
                <div className="modal-buttons">
                    <button onClick={onClose} className="modal-cancel">
                        Cancel
                    </button>
                    <button onClick={handleSave} className="modal-save">
                        Save
                    </button>
                </div>
            </div>
        </div>
    );
};

export default BudgetModal;
