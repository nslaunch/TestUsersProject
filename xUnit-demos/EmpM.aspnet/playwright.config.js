// playwright.config.js
module.exports = {
    // testDir: './tests', // Directory containing the test files
    timeout: 30000, // Test timeout
    use: {
      headless: true, // Run tests in headless mode by default
    //   baseURL: 'http://localhost:3000', // Set a base URL for tests
    },
    // You can also specify patterns if you want to include/exclude certain test files
    testMatch: [
      '**/__tests__/**/*.test.js', // Run all spec.js files under the tests folder
      // '**/tests/specificTest.spec.js'  // You can specify a single test file
    ],
  };
  