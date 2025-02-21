// const { chromium } = require('playwright');

// describe('Employee Page', () => {
//   let browser;
//   let page;

//   beforeAll(async () => {
//     // Launch browser (headless: false to see the browser)
//     browser = await chromium.launch({ headless: false });
//     page = await browser.newPage();
//   });

//   afterAll(async () => {
//     // Close browser after tests
//     await browser.close();
//   });

//   it('should display loader and then show the table', async () => {
//     // Navigate to the employee page
//     await page.goto('http://localhost:3000/Employee'); // Replace with your URL

//     // Check that loader is visible initially
//     const loaderVisible = await page.isVisible('#loader');
//     expect(loaderVisible).toBe(true);

//     // Wait for the table to be visible after 1.5 seconds (mimicking the setTimeout in your JS code)
//     await page.waitForSelector('#employeeTable', { state: 'visible' });

//     // Check if the loader is no longer visible
//     const loaderHidden = await page.isHidden('#loader');
//     expect(loaderHidden).toBe(true);

//     // Check if the table is now visible
//     const tableVisible = await page.isVisible('#employeeTable');
//     expect(tableVisible).toBe(true);
//   });
// });
// describe("Addition", function() {
//     it("should add two positive numbers correctly", function() {
//         console.log("Test running...");
//         var result = 2 + 3;
//         expect(result).toBe(5);
//     });

//     it("should add two numbers with same values correctly", function() {
//         console.log("Test running...");
//         var result = 2 + 3;
//         expect(result).toBe(5);
//     });
// });
