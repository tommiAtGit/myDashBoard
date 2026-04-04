import React, { useState, useEffect } from "react";
import "./../todoModal.css";

const FinanceModal = ({ isOpen, onClose, onSave, transaction }) => {
    const [id, setId] = useState(crypto.randomUUID());
    const [account, setAccount] = useState("");
    const [type, setType] = useState(0); // ActionType
    const [description, setDescription] = useState("");
    const [amount, setAmount] = useState(0);
    const [actionDate, setActionDate] = useState(new Date().toISOString().split('T')[0]);

    useEffect(() => {
        if (transaction && isOpen) {
            setId(transaction.id || crypto.randomUUID());
            setAccount(transaction.account || "");
            setType(transaction.type || 0);
            setDescription(transaction.description || "");
            setAmount(transaction.amount || 0);
            setActionDate(transaction.actionDate ? transaction.actionDate.split('T')[0] : new Date().toISOString().split('T')[0]);
        } else if (isOpen) {
            setId(crypto.randomUUID());
            setAccount("");
            setType(0);
            setDescription("");
            setAmount(0);
            setActionDate(new Date().toISOString().split('T')[0]);
        }
    }, [transaction, isOpen]);

    const handleSave = () => {
        const newTransaction = {
            id,
            account,
            type: parseInt(type),
            description,
            amount: parseFloat(amount),
            actionDate: new Date(actionDate).toISOString(),
        };
        onSave(newTransaction);
        onClose();
    };

    if (!isOpen) return null;

    return (
        <div className="modal-overlay">
            <div className="modal-container">
                <h2 className="modal-title">Transaction</h2>

                {/* Account Input */}
                <input
                    type="text"
                    placeholder="Enter account number"
                    value={account}
                    onChange={(e) => setAccount(e.target.value)}
                    className="modal-input"
                />

                {/* Type Input */}
                <label className="area-header">Type
                    <select 
                        value={type} 
                        onChange={(e) => setType(e.target.value)}
                        className="modal-input"
                    >
                        <option value={0}>UNDEFINED</option>
                        <option value={1}>DEPOSIT</option>
                        <option value={2}>WITHDRAWAL</option>
                        <option value={3}>LOAN</option>
                        <option value={4}>SAVE</option>
                    </select>
                </label>

                {/* Description Input */}
                <input
                    type="text"
                    placeholder="Enter description"
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                    className="modal-input"
                />

                {/* Amount Input */}
                <input
                    type="number"
                    placeholder="0.00"
                    value={amount}
                    onChange={(e) => setAmount(e.target.value)}
                    className="modal-input"
                />

                {/* Date Input */}
                <label className="area-header">Date
                    <input
                        type="date"
                        value={actionDate}
                        onChange={(e) => setActionDate(e.target.value)}
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

export default FinanceModal;
