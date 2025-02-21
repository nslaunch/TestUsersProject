describe('ASP.NET Home Page Test', () => {
    it('should load the home page and check the title', () => {
      // Visit the home page
      cy.visit('/'); // Base URL is automatically prepended
  
      // Verify the page title
      cy.title().should('include', 'Home'); // Adjust as per your app's title
  
      // Check for an element rendered from the CSHTML
      cy.get('h1').should('contain', 'Welcome'); // Adjust selector based on your CSHTML content
    });
  });
  