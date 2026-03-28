import { 
  formatDate, 
  formatCurrency, 
  generateId,
  calculateBalance,
  calculateStockValue 
} from '../utils/helpers';

describe('Helper Functions', () => {
  describe('formatDate', () => {
    test('formats date correctly', () => {
      const date = new Date('2024-01-15');
      const formatted = formatDate(date);
      expect(formatted).toMatch(/\d{2}\.\d{2}\.\d{4}/);
    });
  });

  describe('formatCurrency', () => {
    test('formats positive currency', () => {
      const formatted = formatCurrency(1234.56);
      expect(formatted).toBe('$1,234.56');
    });

    test('formats negative currency', () => {
      const formatted = formatCurrency(-1234.56);
      expect(formatted).toBe('-$1,234.56');
    });

    test('formats zero', () => {
      const formatted = formatCurrency(0);
      expect(formatted).toBe('$0.00');
    });
  });

  describe('generateId', () => {
    test('generates unique ids', () => {
      const id1 = generateId();
      const id2 = generateId();
      expect(id1).not.toBe(id2);
    });

    test('generates string ids', () => {
      const id = generateId();
      expect(typeof id).toBe('string');
      expect(id.length).toBeGreaterThan(0);
    });
  });

  describe('calculateBalance', () => {
    test('calculates positive balance', () => {
      const assets = [
        { id: '1', name: 'Asset 1', value: 1000 },
        { id: '2', name: 'Asset 2', value: 2000 }
      ];
      const liabilities = [
        { id: '1', name: 'Liability 1', value: 500 }
      ];
      const balance = calculateBalance(assets, liabilities);
      expect(balance).toBe(2500);
    });

    test('calculates negative balance', () => {
      const assets = [
        { id: '1', name: 'Asset 1', value: 1000 }
      ];
      const liabilities = [
        { id: '1', name: 'Liability 1', value: 2000 }
      ];
      const balance = calculateBalance(assets, liabilities);
      expect(balance).toBe(-1000);
    });

    test('handles empty arrays', () => {
      const balance = calculateBalance([], []);
      expect(balance).toBe(0);
    });
  });

  describe('calculateStockValue', () => {
    test('calculates stock value correctly', () => {
      const value = calculateStockValue(100, 50);
      expect(value).toBe(5000);
    });

    test('handles zero quantity', () => {
      const value = calculateStockValue(100, 0);
      expect(value).toBe(0);
    });

    test('handles zero price', () => {
      const value = calculateStockValue(0, 50);
      expect(value).toBe(0);
    });
  });
});
