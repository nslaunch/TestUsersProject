
// describe('Delete Employee', () => {
//   it('should successfully delete an employee and update the employee list', () => {
//     cy.visit('/Employee'); // Visit the employee listing page

//     // Get the first employee's row and capture the employee's name for comparison
//     cy.get('#employeeTable tbody tr').first().within(() => {
//       cy.get('td').eq(1).invoke('text').as('employeeName'); // Capture employee name
//       cy.get('@employeeName').then((employeeName) => {
//         cy.log('Captured employee name: ' + employeeName); // Log captured employee name
//       });
//       cy.get('.delete-btn').click(); // Click the delete button
//     });

//     // Wait for the page to reload after deletion
//     cy.url().should('include', '/Employee'); // Assert that we are back at the employee list page

//     // // Verify that the employee no longer exists in the table
//     // cy.get('@employeeName').then((employeeName) => {
//     //   cy.get('#employeeTable tbody tr').first().should('not.contain', employeeName); // Check if employee is removed
//     // });

//     cy.get('@employeeName').then((employeeName) => {
//       cy.log('Checking if employee name: ' + employeeName + ' is removed from the table');

//       cy.get('#employeeTable tbody tr').should('contain', employeeName); 
//       cy.log('Verified that employee name: ' + employeeName + ' is not in the table');
//       // Check if employee is removed
//     });
//   });
// });

it('should successfully delete an employee and update the employee list', () => {
  cy.visit('/Employee'); // Visit the employee listing page

  // Step 1: Capture the initial number of employees (rows in the table)
  cy.get('#employeeTable tbody tr').then(($rows) => {
    const initialCount = $rows.length; // Capture the initial row count
    cy.log(`Initial employee count: ${initialCount}`); // Log initial count for verification

    // Step 2: Click the delete button of the first employee
    cy.get('#employeeTable tbody tr').first().find('.delete-btn').click(); // Click the delete button

    // Wait for the page to reload or for the table to update after deletion
    cy.url().should('include', '/Employee'); // Ensure we are on the employee listing page after the action

    // Step 3: Capture the updated number of rows and check if it is reduced by 1
    cy.get('#employeeTable tbody tr').then(($updatedRows) => {
      const updatedCount = $updatedRows.length; // Capture the updated row count
      cy.log(`Updated employee count: ${updatedCount}`); // Log updated count for verification

      // Verify that the row count has decreased by 1
      expect(updatedCount).to.eq(initialCount - 1); // Assert that the row count has decreased by 1
    });
  });
});
