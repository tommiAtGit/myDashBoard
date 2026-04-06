// jest-dom adds custom jest matchers for asserting on DOM nodes.
// allows you to do things like:
// expect(element).toHaveTextContent(/react/i)
// learn more: https://github.com/testing-library/jest-dom
import '@testing-library/jest-dom';

// Mock crypto.randomUUID for tests
if (!global.crypto) {
    global.crypto = {};
}
if (!global.crypto.randomUUID) {
    global.crypto.randomUUID = () => 'test-uuid-' + Math.random().toString(36).substring(2, 9);
}

// Global axios mock to avoid ESM issues
jest.mock('axios', () => ({
    get: jest.fn(() => Promise.resolve({ data: {} })),
    post: jest.fn(() => Promise.resolve({ data: {} })),
    put: jest.fn(() => Promise.resolve({ data: {} })),
    delete: jest.fn(() => Promise.resolve({ data: {} })),
    create: jest.fn(function () { return this; })
}));

// MutationObserver shim for jsdom
class MutationObserver {
    constructor(callback) {
        this.callback = callback;
    }
    disconnect() {}
    observe(element, init) {}
    takeRecords() { return [] }
}

global.MutationObserver = MutationObserver;
window.MutationObserver = MutationObserver;
