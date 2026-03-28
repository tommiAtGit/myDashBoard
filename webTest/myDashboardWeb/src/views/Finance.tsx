import React, { useState, useEffect } from 'react';
import {
  Asset,
  Liability,
  Stock,
  Fund
} from '../types';
import {
  loadAssets,
  saveAssets,
  loadLiabilities,
  saveLiabilities,
  loadStocks,
  saveStocks,
  loadFunds,
  saveFunds,
  loadAccounts,
  saveAccounts,
  formatCurrency,
  calculateBalance,
  calculateStockValue,
  generateId
} from '../utils/helpers';

type ActiveTab = 'balance' | 'budget';

const Finance: React.FC = () => {
  const [activeTab, setActiveTab] = useState<ActiveTab>('budget');
  
  // Balance tab state
  const [assets, setAssets] = useState<Asset[]>([]);
  const [liabilities, setLiabilities] = useState<Liability[]>([]);
  
  // Budget tab state
  const [currentAccount, setCurrentAccount] = useState(0);
  const [savingsAccount, setSavingsAccount] = useState(0);
  const [stocks, setStocks] = useState<Stock[]>([]);
  const [funds, setFunds] = useState<Fund[]>([]);

  useEffect(() => {
    // Load all data
    setAssets(loadAssets());
    setLiabilities(loadLiabilities());
    setStocks(loadStocks());
    setFunds(loadFunds());
    const accounts = loadAccounts();
    setCurrentAccount(accounts.current);
    setSavingsAccount(accounts.savings);
  }, []);

  // Save data whenever it changes
  useEffect(() => {
    saveAssets(assets);
  }, [assets]);

  useEffect(() => {
    saveLiabilities(liabilities);
  }, [liabilities]);

  useEffect(() => {
    saveStocks(stocks);
  }, [stocks]);

  useEffect(() => {
    saveFunds(funds);
  }, [funds]);

  useEffect(() => {
    saveAccounts({ current: currentAccount, savings: savingsAccount });
  }, [currentAccount, savingsAccount]);

  // Asset handlers
  const handleAddAsset = () => {
    const newAsset: Asset = {
      id: generateId(),
      name: 'New Asset',
      value: 0
    };
    setAssets(prev => [...prev, newAsset]);
  };

  const handleUpdateAsset = (id: string, field: keyof Asset, value: string | number) => {
    setAssets(prev =>
      prev.map(asset =>
        asset.id === id ? { ...asset, [field]: value } : asset
      )
    );
  };

  const handleDeleteAsset = (id: string) => {
    setAssets(prev => prev.filter(asset => asset.id !== id));
  };

  // Liability handlers
  const handleAddLiability = () => {
    const newLiability: Liability = {
      id: generateId(),
      name: 'New Liability',
      value: 0
    };
    setLiabilities(prev => [...prev, newLiability]);
  };

  const handleUpdateLiability = (id: string, field: keyof Liability, value: string | number) => {
    setLiabilities(prev =>
      prev.map(liability =>
        liability.id === id ? { ...liability, [field]: value } : liability
      )
    );
  };

  const handleDeleteLiability = (id: string) => {
    setLiabilities(prev => prev.filter(liability => liability.id !== id));
  };

  // Stock handlers
  const handleAddStock = () => {
    const newStock: Stock = {
      id: generateId(),
      name: 'New Stock',
      price: 0,
      quantity: 0,
      currentValue: 0
    };
    setStocks(prev => [...prev, newStock]);
  };

  const handleUpdateStock = (id: string, field: keyof Stock, value: string | number) => {
    setStocks(prev =>
      prev.map(stock => {
        if (stock.id === id) {
          const updatedStock = { ...stock, [field]: value };
          if (field === 'price' || field === 'quantity') {
            updatedStock.currentValue = calculateStockValue(
              Number(updatedStock.price),
              Number(updatedStock.quantity)
            );
          }
          return updatedStock;
        }
        return stock;
      })
    );
  };

  const handleDeleteStock = (id: string) => {
    setStocks(prev => prev.filter(stock => stock.id !== id));
  };

  // Fund handlers
  const handleAddFund = () => {
    const newFund: Fund = {
      id: generateId(),
      name: 'New Fund',
      price: 0
    };
    setFunds(prev => [...prev, newFund]);
  };

  const handleUpdateFund = (id: string, field: keyof Fund, value: string | number) => {
    setFunds(prev =>
      prev.map(fund =>
        fund.id === id ? { ...fund, [field]: value } : fund
      )
    );
  };

  const handleDeleteFund = (id: string) => {
    setFunds(prev => prev.filter(fund => fund.id !== id));
  };

  const totalBalance = calculateBalance(assets, liabilities);
  const totalAssets = assets.reduce((sum, asset) => sum + asset.value, 0);
  const totalLiabilities = liabilities.reduce((sum, liability) => sum + liability.value, 0);
  const totalStockValue = stocks.reduce((sum, stock) => sum + stock.currentValue, 0);
  const totalFundValue = funds.reduce((sum, fund) => sum + fund.price, 0);

  return (
    <div className="finance-container">
      <div className="finance-tabs">
        <button
          className={`finance-tab ${activeTab === 'budget' ? 'active' : ''}`}
          onClick={() => setActiveTab('budget')}
        >
          Current Budget
        </button>
        <button
          className={`finance-tab ${activeTab === 'balance' ? 'active' : ''}`}
          onClick={() => setActiveTab('balance')}
        >
          Balance
        </button>
      </div>

      {activeTab === 'balance' ? (
        <div className="finance-content">
          <div className="balance-summary">
            <div className="balance-title">Current Balance</div>
            <div className={`balance-value ${totalBalance >= 0 ? 'balance-positive' : 'balance-negative'}`}>
              {formatCurrency(totalBalance)}
            </div>
            <div style={{ marginTop: '12px', fontSize: '14px', color: 'var(--text-secondary)' }}>
              Assets: {formatCurrency(totalAssets)} | Liabilities: {formatCurrency(totalLiabilities)}
            </div>
          </div>

          <div className="finance-section">
            <div className="section-header">
              <h3 className="section-title">Assets</h3>
              <button className="add-item-button" onClick={handleAddAsset}>
                <i className="fas fa-plus"></i> Add Asset
              </button>
            </div>
            <table className="finance-table">
              <thead>
                <tr>
                  <th>Asset Name</th>
                  <th>Value</th>
                  <th>Actions</th>
                </tr>
              </thead>
              <tbody>
                {assets.map(asset => (
                  <tr key={asset.id}>
                    <td>
                      <input
                        type="text"
                        className="table-input"
                        value={asset.name}
                        onChange={(e) => handleUpdateAsset(asset.id, 'name', e.target.value)}
                      />
                    </td>
                    <td>
                      <input
                        type="number"
                        className="table-input"
                        value={asset.value}
                        onChange={(e) => handleUpdateAsset(asset.id, 'value', Number(e.target.value))}
                      />
                    </td>
                    <td>
                      <button
                        className="delete-row-button"
                        onClick={() => handleDeleteAsset(asset.id)}
                      >
                        <i className="fas fa-trash"></i>
                      </button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>

          <div className="finance-section">
            <div className="section-header">
              <h3 className="section-title">Liabilities</h3>
              <button className="add-item-button" onClick={handleAddLiability}>
                <i className="fas fa-plus"></i> Add Liability
              </button>
            </div>
            <table className="finance-table">
              <thead>
                <tr>
                  <th>Liability Name</th>
                  <th>Value</th>
                  <th>Actions</th>
                </tr>
              </thead>
              <tbody>
                {liabilities.map(liability => (
                  <tr key={liability.id}>
                    <td>
                      <input
                        type="text"
                        className="table-input"
                        value={liability.name}
                        onChange={(e) => handleUpdateLiability(liability.id, 'name', e.target.value)}
                      />
                    </td>
                    <td>
                      <input
                        type="number"
                        className="table-input"
                        value={liability.value}
                        onChange={(e) => handleUpdateLiability(liability.id, 'value', Number(e.target.value))}
                      />
                    </td>
                    <td>
                      <button
                        className="delete-row-button"
                        onClick={() => handleDeleteLiability(liability.id)}
                      >
                        <i className="fas fa-trash"></i>
                      </button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      ) : (
        <div className="finance-content">
          <div className="account-cards">
            <div className="account-card">
              <div className="account-card-title">Current Account Balance</div>
              <div className="account-card-value">{formatCurrency(currentAccount)}</div>
            </div>
            <div className="account-card">
              <div className="account-card-title">Savings Account Balance</div>
              <div className="account-card-value">{formatCurrency(savingsAccount)}</div>
            </div>
            <div className="account-card">
              <div className="account-card-title">Total Stock Value</div>
              <div className="account-card-value">{formatCurrency(totalStockValue)}</div>
            </div>
            <div className="account-card">
              <div className="account-card-title">Total Fund Value</div>
              <div className="account-card-value">{formatCurrency(totalFundValue)}</div>
            </div>
          </div>

          <div className="finance-section">
            <div className="section-header">
              <h3 className="section-title">Account Balances</h3>
            </div>
            <table className="finance-table">
              <thead>
                <tr>
                  <th>Account Type</th>
                  <th>Balance</th>
                </tr>
              </thead>
              <tbody>
                <tr>
                  <td>Current Account</td>
                  <td>
                    <input
                      type="number"
                      className="table-input"
                      value={currentAccount}
                      onChange={(e) => setCurrentAccount(Number(e.target.value))}
                    />
                  </td>
                </tr>
                <tr>
                  <td>Savings Account</td>
                  <td>
                    <input
                      type="number"
                      className="table-input"
                      value={savingsAccount}
                      onChange={(e) => setSavingsAccount(Number(e.target.value))}
                    />
                  </td>
                </tr>
              </tbody>
            </table>
          </div>

          <div className="finance-section">
            <div className="section-header">
              <h3 className="section-title">Stocks</h3>
              <button className="add-item-button" onClick={handleAddStock}>
                <i className="fas fa-plus"></i> Add Stock
              </button>
            </div>
            <table className="finance-table">
              <thead>
                <tr>
                  <th>Stock Name</th>
                  <th>Stock Price</th>
                  <th>Quantity</th>
                  <th>Current Value</th>
                  <th>Actions</th>
                </tr>
              </thead>
              <tbody>
                {stocks.map(stock => (
                  <tr key={stock.id}>
                    <td>
                      <input
                        type="text"
                        className="table-input"
                        value={stock.name}
                        onChange={(e) => handleUpdateStock(stock.id, 'name', e.target.value)}
                      />
                    </td>
                    <td>
                      <input
                        type="number"
                        className="table-input"
                        value={stock.price}
                        onChange={(e) => handleUpdateStock(stock.id, 'price', Number(e.target.value))}
                        step="0.01"
                      />
                    </td>
                    <td>
                      <input
                        type="number"
                        className="table-input"
                        value={stock.quantity}
                        onChange={(e) => handleUpdateStock(stock.id, 'quantity', Number(e.target.value))}
                      />
                    </td>
                    <td>{formatCurrency(stock.currentValue)}</td>
                    <td>
                      <button
                        className="delete-row-button"
                        onClick={() => handleDeleteStock(stock.id)}
                      >
                        <i className="fas fa-trash"></i>
                      </button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>

          <div className="finance-section">
            <div className="section-header">
              <h3 className="section-title">Funds</h3>
              <button className="add-item-button" onClick={handleAddFund}>
                <i className="fas fa-plus"></i> Add Fund
              </button>
            </div>
            <table className="finance-table">
              <thead>
                <tr>
                  <th>Fund Name</th>
                  <th>Fund Price</th>
                  <th>Actions</th>
                </tr>
              </thead>
              <tbody>
                {funds.map(fund => (
                  <tr key={fund.id}>
                    <td>
                      <input
                        type="text"
                        className="table-input"
                        value={fund.name}
                        onChange={(e) => handleUpdateFund(fund.id, 'name', e.target.value)}
                      />
                    </td>
                    <td>
                      <input
                        type="number"
                        className="table-input"
                        value={fund.price}
                        onChange={(e) => handleUpdateFund(fund.id, 'price', Number(e.target.value))}
                        step="0.01"
                      />
                    </td>
                    <td>
                      <button
                        className="delete-row-button"
                        onClick={() => handleDeleteFund(fund.id)}
                      >
                        <i className="fas fa-trash"></i>
                      </button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      )}
    </div>
  );
};

export default Finance;
