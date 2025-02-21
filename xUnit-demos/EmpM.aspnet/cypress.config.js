const { defineConfig } = require('cypress');

module.exports = defineConfig({
  e2e: {
    baseUrl: 'https://localhost:44343/', // Replace with your app's URL
    supportFile: false, // Optional, depending on your setup
  },
});
