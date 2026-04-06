import React, { useState, useEffect } from "react";
import { FaPen, FaTrash, FaPlus } from 'react-icons/fa';
import "./../todoModal.css";

const BalanceSheetModal = ({ isOpen, onClose, onSave, balanceSheet }) => {
    const [items, setItems] = useState([]);
    const [newItemName, setNewItemName] = useState("");
    const [newItemValue, setNewItemValue] = useState(0);
    const [newItemType, setNewItemType] = useState(0); // 0: ASSET, 1: LIABILITY
    const [editingItemId, setEditingId] = useState(null);

    useEffect(() => {
        if (balanceSheet && isOpen) {
            setItems(balanceSheet.items || []);
        } else {
            setItems([]);
        }
    }, [balanceSheet, isOpen]);

    const calculateTotals = () => {
        const assets = items.filter(i => i.type === 0).reduce((sum, i) => sum + i.value, 0);
        const liabilities = items.filter(i => i.type === 1).reduce((sum, i) => sum + i.value, 0);
        return { assets, liabilities, total: assets - liabilities };
    };

    const handleAddItem = () => {
        if (!newItemName) return;
        const newItem = {
            id: crypto.randomUUID(),
            itemName: newItemName,
            value: parseFloat(newItemValue),
            type: parseInt(newItemType),
            balanceSheetId: balanceSheet?.id
        };
        setItems([...items, newItem]);
        resetForm();
    };

    const handleEditItem = (item) => {
        setEditingId(item.id);
        setNewItemName(item.itemName);
        setNewItemValue(item.value);
        setNewItemType(item.type);
    };

    const handleUpdateItem = () => {
        setItems(items.map(i => i.id === editingItemId 
            ? { ...i, itemName: newItemName, value: parseFloat(newItemValue), type: parseInt(newItemType) } 
            : i));
        resetForm();
    };

    const handleDeleteItem = (id) => {
        setItems(items.filter(i => i.id !== id));
    };

    const resetForm = () => {
        setNewItemName("");
        setNewItemValue(0);
        setNewItemType(0);
        setEditingId(null);
    };

    const handleSaveSheet = () => {
        const updatedSheet = {
            ...balanceSheet,
            items: items,
            balanceSheetItemValue: calculateTotals().total,
            balanceSheetItemChanged: new Date().toISOString()
        };
        onSave(updatedSheet);
    };

    if (!isOpen) return null;

    const totals = calculateTotals();

    return (
        <div className="modal-overlay">
            <div className="modal-container" style={{ width: '600px' }}>
                <h2 className="modal-title">Edit Balance Sheet</h2>

                <div className="modal-section" style={{ marginBottom: '20px', border: '1px solid #eee', padding: '15px' }}>
                    <h4>{editingItemId ? "Edit Item" : "Add New Item"}</h4>
                    <div style={{ display: 'flex', gap: '10px', marginBottom: '10px' }}>
                        <input 
                            type="text" 
                            placeholder="Item Name" 
                            className="modal-input" 
                            style={{ flex: 2 }}
                            value={newItemName}
                            onChange={(e) => setNewItemName(e.target.value)}
                        />
                        <input 
                            type="number" 
                            placeholder="Value" 
                            className="modal-input" 
                            style={{ flex: 1 }}
                            value={newItemValue}
                            onChange={(e) => setNewItemValue(e.target.value)}
                        />
                    </div>
                    <div style={{ display: 'flex', gap: '10px', alignItems: 'center' }}>
                        <select 
                            className="modal-input" 
                            style={{ flex: 1 }}
                            value={newItemType}
                            onChange={(e) => setNewItemType(e.target.value)}
                        >
                            <option value={0}>ASSET</option>
                            <option value={1}>LIABILITY</option>
                        </select>
                        <button className="modal-save" style={{ height: '38px', marginBottom: '10px' }} onClick={editingItemId ? handleUpdateItem : handleAddItem}>
                            {editingItemId ? "Update" : <FaPlus />}
                        </button>
                        {editingItemId && <button className="modal-cancel" style={{ height: '38px', marginBottom: '10px' }} onClick={resetForm}>Cancel</button>}
                    </div>
                </div>

                <div style={{ display: 'flex', gap: '20px', maxHeight: '300px', overflowY: 'auto' }}>
                    <div style={{ flex: 1 }}>
                        <h5 style={{ borderBottom: '2px solid green' }}>Assets</h5>
                        {items.filter(i => i.type === 0).map(item => (
                            <div key={item.id} className="note-item" style={{ fontSize: '12px', padding: '5px' }}>
                                <span>{item.itemName}: ${item.value.toLocaleString()}</span>
                                <div className="note-item-actions">
                                    <FaPen onClick={() => handleEditItem(item)} />
                                    <FaTrash onClick={() => handleDeleteItem(item.id)} />
                                </div>
                            </div>
                        ))}
                    </div>
                    <div style={{ flex: 1 }}>
                        <h5 style={{ borderBottom: '2px solid red' }}>Liabilities</h5>
                        {items.filter(i => i.type === 1).map(item => (
                            <div key={item.id} className="note-item" style={{ fontSize: '12px', padding: '5px' }}>
                                <span>{item.itemName}: ${item.value.toLocaleString()}</span>
                                <div className="note-item-actions">
                                    <FaPen onClick={() => handleEditItem(item)} />
                                    <FaTrash onClick={() => handleDeleteItem(item.id)} />
                                </div>
                            </div>
                        ))}
                    </div>
                </div>

                <div style={{ marginTop: '20px', padding: '10px', background: '#f0f4ff', borderRadius: '4px', fontWeight: 'bold' }}>
                    Total Balance: ${totals.total.toLocaleString()}
                </div>

                <div className="modal-buttons" style={{ marginTop: '20px' }}>
                    <button onClick={onClose} className="modal-cancel">Cancel</button>
                    <button onClick={handleSaveSheet} className="modal-save">Save Sheet</button>
                </div>
            </div>
        </div>
    );
};

export default BalanceSheetModal;
