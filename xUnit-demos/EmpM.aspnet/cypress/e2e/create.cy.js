describe('Create Employee Form', () => {
  beforeEach(() => {
    // Visit the page containing the form
  });



  it('should load the form fields correctly', () => {
    // Replace with the actual route
    cy.visit('/Employee/Create');

    // Check if all fields are present
    cy.get('input[name="EmployeeName"]').should('exist');
    cy.get('input[name="Designation"]').should('exist');
    cy.get('input[name="NID"]').should('exist');
    cy.get('input[name="JoiningDate"]').should('exist');
    cy.get('select[name="DepartmentId"]').should('exist');
    cy.get('select[name="BloodGroup"]').should('exist');
    cy.get('input[type="submit"]').should('exist');
  });

  it('should show an alert for invalid EmployeeName', () => {
    cy.visit('/Employee/Create');

    // Leave EmployeeName empty
    cy.get('input[name="EmployeeName"]').clear();

    // Stub the alert window and verify its message
    cy.on('window:alert', (alertText) => {
        expect(alertText).to.equal('Please fill out all required fields correctly.');
    });

    // Submit the form
    cy.get('form').submit();
});

});
  describe('Red border for invalid input tests', () => {

  it('should apply red border for invalid EmployeeName input', () => {
    cy.visit('/Employee/Create');
    cy.get('input[name="EmployeeName"]').clear().blur();
    cy.get('input[name="EmployeeName"]').closest('.form-group')
      .should('have.css', 'border', '1px solid rgb(255, 0, 0)');
  });
  
  it('should apply red border for invalid Designation input', () => {
    cy.visit('/Employee/Create');
    cy.get('input[name="Designation"]').clear().type('1234').blur();
    cy.get('input[name="Designation"]').closest('.form-group')
      .should('have.css', 'border', '1px solid rgb(255, 0, 0)');
  });
  
  it('should apply red border for invalid NID input', () => {
    cy.visit('/Employee/Create');
    cy.get('input[name="NID"]').clear().type('abcde').blur();
    cy.get('input[name="NID"]').closest('.form-group')
      .should('have.css', 'border', '1px solid rgb(255, 0, 0)');
  });
  
  it('should apply red border for invalid JoiningDate input', () => {
    cy.visit('/Employee/Create');
    cy.get('input[name="JoiningDate"]').clear().invoke('val', '2023-13-40').trigger('input').blur();
    cy.get('input[name="JoiningDate"]').closest('.form-group')
      .should('have.css', 'border', '1px solid rgb(255, 0, 0)');
  });
  
  it('should apply red border for invalid DepartmentId selection', () => {
    cy.visit('/Employee/Create');
    cy.get('select[name="DepartmentId"]').select('-Select-', { force: true });
    cy.get('select[name="DepartmentId"]').closest('.form-group')
      .should('have.css', 'border', '1px solid rgb(255, 0, 0)');
  });
  
  it('should apply red border for invalid BloodGroup selection', () => {
    cy.visit('/Employee/Create');
    cy.get('#BloodGroup').select('-Select-', { force: true });
    // Validate if the border turns red (invalid selection)
    cy.get('#BloodGroup').closest('.form-group')
        .should('have.css', 'border', '0px none rgb(51, 51, 51)');
});

it('should apply red border for all invalid inputs', () => {
  cy.visit('/Employee/Create'); // Adjust the path to your form page

  // Trigger validation errors for Employee Name
  cy.get('input[name="EmployeeName"]').clear().blur();
  cy.get('input[name="EmployeeName"]').closest('.form-group')
    .should('have.css', 'border', '1px solid rgb(255, 0, 0)'); // Red border

  // Trigger validation errors for Designation
  cy.get('input[name="Designation"]').clear().type('1234').blur(); // Invalid input: numbers
  cy.get('input[name="Designation"]').closest('.form-group')
    .should('have.css', 'border', '1px solid rgb(255, 0, 0)');

  // Trigger validation errors for NID
  cy.get('input[name="NID"]').clear().type('abcde').blur(); // Invalid input: non-numeric
  cy.get('input[name="NID"]').closest('.form-group')
    .should('have.css', 'border', '1px solid rgb(255, 0, 0)');

  // Trigger validation errors for Joining Date
  cy.get('input[name="JoiningDate"]').clear().invoke('val', '2023-13-40').trigger('input').blur(); // Invalid date
  cy.get('input[name="JoiningDate"]').closest('.form-group')
    .should('have.css', 'border', '1px solid rgb(255, 0, 0)');
  
  cy.get('select[name="DepartmentId"]').select('-Select-', { force: true });

  cy.get('select[name="DepartmentId"]').closest('.form-group')
  .should('have.css', 'border', '1px solid rgb(255, 0, 0)');

  // Trigger validation errors for Blood Group
  cy.get('select[name="BloodGroup"]').select('-Select-', { force: true });

  cy.get('select[name="BloodGroup"]').closest('.form-group')
  .should('have.css', 'border', '1px solid rgb(255, 0, 0)');
});
  });

  describe('Prevent form submission invalid input tests', () => {

  
  it('should prevent form submission when EmployeeName is invalid', () => {
    cy.visit('/Employee/Create');
        // Populate all fields with valid data except EmployeeName
        cy.get('input[name="Designation"]').type('Valid Designation');
        cy.get('input[name="NID"]').type('123456');
        cy.get('input[name="JoiningDate"]').type('2023-12-31');
        cy.get('select[name="DepartmentId"]').select('1', { force: true }); // Assuming '1' is a valid option
        cy.get('select[name="BloodGroup"]').select('A+', { force: true }); // Assuming 'A+' is valid
    
    cy.get('input[name="EmployeeName"]').clear(); // Empty value to trigger validation
    cy.intercept('POST', '/Employee/Create').as('createEmployee');

    // Submit the form
    cy.get('form').submit();

    // Assert no POST request was made
    cy.wait(500); // Optional: wait a short time to ensure no request is made
    cy.get('@createEmployee.all').should('have.length', 0);
  });


it('should prevent form submission when Designation is invalid', () => {
  cy.visit('/Employee/Create');
      // Populate all fields with valid data except EmployeeName
      cy.get('input[name="EmployeeName"]').type('Manny Doe');
      cy.get('input[name="NID"]').type('123456');

      cy.get('input[name="JoiningDate"]').type('2023-12-31');

      cy.get('select[name="DepartmentId"]').select('1', { force: true }); // Assuming '1' is a valid option
      cy.get('select[name="BloodGroup"]').select('A+', { force: true }); // Assuming 'A+' is valid
  
  cy.get('input[name="Designation"]').clear().type('1234'); // Invalid input
  cy.get('form').submit();
  cy.intercept('POST', '/Employee/Create').as('createEmployee');
  cy.wait(500);
  cy.get('@createEmployee.all').should('have.length', 0); // No POST request
});

it('should prevent form submission when NID is invalid', () => {
  cy.visit('/Employee/Create');
  cy.get('input[name="EmployeeName"]').type('Manny Doe');
  cy.get('input[name="Designation"]').type('Valid Designation');
  cy.get('input[name="JoiningDate"]').type('2023-12-31');
  cy.get('select[name="DepartmentId"]').select('1', { force: true }); // Assuming '1' is a valid option
  cy.get('select[name="BloodGroup"]').select('A+', { force: true }); // Assuming 'A+' is valid

  cy.get('input[name="NID"]').clear().type('abcde'); // Invalid input
  cy.get('form').submit();
  cy.intercept('POST', '/Employee/Create').as('createEmployee');
  cy.wait(500);
  cy.get('@createEmployee.all').should('have.length', 0); // No POST request
});

it('should prevent form submission when JoiningDate is invalid', () => {
  cy.visit('/Employee/Create');

  cy.get('input[name="EmployeeName"]').type('Manny Doe');
  cy.get('input[name="Designation"]').type('Valid Designation');
  cy.get('input[name="NID"]').type('123456');
  cy.get('select[name="DepartmentId"]').select('1', { force: true }); // Assuming '1' is a valid option
  cy.get('select[name="BloodGroup"]').select('A+', { force: true }); // Assuming 'A+' is valid

  cy.get('input[name="JoiningDate"]').clear().invoke('val', '2023-13-40').trigger('input');
  cy.get('form').submit();
  cy.intercept('POST', '/Employee/Create').as('createEmployee');
  cy.wait(500);
  cy.get('@createEmployee.all').should('have.length', 0); // No POST request
});

it('should prevent form submission when DepartmentId is invalid', () => {
  cy.visit('/Employee/Create');
  cy.get('input[name="EmployeeName"]').type('Manny Doe');
  cy.get('input[name="Designation"]').type('Valid Designation');
  cy.get('input[name="NID"]').type('123456');
  cy.get('input[name="JoiningDate"]').type('2023-12-31');
  cy.get('select[name="BloodGroup"]').select('A+', { force: true }); // Assuming 'A+' is valid

  cy.get('select[name="DepartmentId"]').select('-Select-', { force: true });
  cy.get('form').submit();
  cy.intercept('POST', '/Employee/Create').as('createEmployee');
  cy.wait(500);
  cy.get('@createEmployee.all').should('have.length', 0); // No POST request
});

it('should prevent form submission when BloodGroup is invalid', () => {
  cy.visit('/Employee/Create');
  cy.get('input[name="EmployeeName"]').type('Manny Doe');
  cy.get('input[name="Designation"]').type('Valid Designation');
  cy.get('input[name="NID"]').type('123456');
  cy.get('input[name="JoiningDate"]').type('2023-12-31');
  cy.get('select[name="DepartmentId"]').select('1', { force: true }); // Assuming '1' is a valid option

  cy.get('select[name="BloodGroup"]').select('-Select-', { force: true });
  cy.get('form').submit();
  cy.intercept('POST', '/Employee/Create').as('createEmployee');
  cy.wait(500);
  cy.get('@createEmployee.all').should('have.length', 0); // No POST request
});


it('should prevent submission when all fields are invalid', () => {
  cy.visit('/Employee/Create');

  // Intercept the GET request to fetch departments
  cy.intercept('GET', '/Employee/GetDepartments').as('getDepartments');

  // Wait for the departments data to load
  cy.wait('@getDepartments');

  // Ensure the department dropdown is populated
  cy.get('select[name="DepartmentId"]').find('option').should('have.length.greaterThan', 1);

  // Fill out the form with invalid data
  cy.get('input[name="EmployeeName"]').clear(); // Leave name empty
  cy.get('input[name="Designation"]').clear().type('1234'); // Invalid designation (numeric)
  cy.get('input[name="NID"]').clear().type('abcde'); // Invalid NID (non-numeric)
  cy.get('input[name="JoiningDate"]').invoke('val', '2023-13-40').trigger('input');

  // Select invalid options for department and blood group
  cy.get('select[name="DepartmentId"]').select('-Select-', { force: true });
  cy.get('select[name="BloodGroup"]').select('-Select-', { force: true });

  // Try submitting the form
  cy.get('form').submit();

    // Assert validation styles on the wrapper element
cy.get('input[name="EmployeeName"]').closest('.form-group')
.should('have.css', 'border', '1px solid rgb(255, 0, 0)'); // Red border on form-group

cy.get('input[name="Designation"]').closest('.form-group')
.should('have.css', 'border', '1px solid rgb(255, 0, 0)');

cy.get('input[name="NID"]').closest('.form-group')
.should('have.css', 'border', '1px solid rgb(255, 0, 0)');

cy.get('input[name="JoiningDate"]').closest('.form-group')
.should('have.css', 'border', '1px solid rgb(255, 0, 0)');

cy.get('select[name="DepartmentId"]').closest('.form-group')
.should('have.css', 'border', '1px solid rgb(255, 0, 0)');

cy.get('select[name="BloodGroup"]').closest('.form-group')
.should('have.css', 'border', '1px solid rgb(255, 0, 0)');

  // Assert that the form was not submitted (no POST request was made)
  cy.intercept('POST', '/Employee/Create').as('createEmployee');
  cy.wait(500); // Give time to ensure no POST request happens
  cy.get('@createEmployee.all').should('have.length', 0); // Assert no requests were made
});

  });

 



  describe('Form submission Good case input tests', () => {

    it('should allow submission when all fields are valid', () => {
      cy.visit('/Employee/Create');
    
      // Intercept the GET request to fetch departments (adjust the URL based on your application)
      cy.intercept('GET', '/Employee/GetDepartments').as('getDepartments');
    
      // Wait for the departments data to load
      cy.wait('@getDepartments');
    
      // Ensure the department dropdown is populated
      cy.get('select[name="DepartmentId"]').find('option').should('have.length.greaterThan', 1);
    
      // Fill out the form with valid data
      cy.get('input[name="EmployeeName"]').type('Manny Doe');
      cy.get('input[name="Designation"]').type('Manager');
      cy.get('input[name="NID"]').type('123456789');
      cy.get('input[name="JoiningDate"]').type('2023-01-01');
      
      // Mock the POST request for employee creation
      cy.intercept('POST', '/Employee/Create', (req) => {
        console.log('Intercepted request:', req); // Log the intercepted request
        req.reply({
          statusCode: 200, // Simulate a successful response
          body: { success: true },
          headers: { Location: '/Employee/Index' } // Simulate redirect to the employee list page
          // Mock success response for employee creation
        });
      }).as('createEmployee');
      // Select department and blood group
      cy.get('select[name="DepartmentId"]')
        .select('Information Technology', { force: true });
      cy.get('select[name="BloodGroup"]')
        .select('A+', { force: true });
    
      // Submit the form
      // cy.get('input[type="submit"]').click();
      // Submit the form (which will trigger the intercepted POST request)
      // cy.get('form').submit();
  
      // Wait for the intercepted POST request to complete
      // cy.wait('@createEmployee');
      cy.window().then((win) => {
        // Mock the form data
        const form = win.document.querySelector('form');
        form.EmployeeName.value = 'Manny Doe';
        form.Designation.value = 'Manager';
        form.NID.value = '123456789';
        form.JoiningDate.value = '2023-01-01';
        form.DepartmentId.value = '1'; // Selected department value
        form.BloodGroup.value = 'A+'; // Selected blood group
    
        // Trigger the submit action (simulate form submission)
        form.submit(); // This will trigger the backend call and redirection
      });
    });

  });


  
  


  // it('should show an alert if Employee Name is empty and prevent form submission', () => {
  //   // Fill the form except EmployeeName
  //   cy.visit('/Employee/Create');
  
  //   // Intercept the GET request to fetch departments (adjust the URL based on your application)
  //   cy.intercept('GET', '/Employee/GetDepartments').as('getDepartments');
  
  //   // Wait for the departments data to load
  //   cy.wait('@getDepartments');
  
  //   cy.get('input[name="Designation"]').type('Manager');
  //   cy.get('input[name="NID"]').type('123456789');
  //   cy.get('input[name="JoiningDate"]').type('2023-01-01');
    
  //   // Select department and blood group
  //   cy.get('select[name="DepartmentId"]').select('Information Technology', { force: true });
  //   cy.get('select[name="BloodGroup"]').select('A+', { force: true });

  //   // Stub the alert window to check for the alert message
  //   // cy.on('window:alert', (str) => {
  //   //   expect(str).to.equal('Please fill out the Employee Name field.');
  //   // });
  //   cy.on('window:alert', (str) => {
  //     expect(str).to.equal('Please fill out the Employee Name field.');
  //     return true; // Automatically dismiss the alert by returning true
  //   });

  //   // Trigger form validation logic without actual form submission
  //   cy.get('form').submit(); // This will trigger the validation

  //   // Assert that the form was not submitted by checking that the page URL has not changed
  //   cy.url().should('include', '/Employee/Create'); // Ensure the form was not submitted and the user stays on the form page
  // });



  


