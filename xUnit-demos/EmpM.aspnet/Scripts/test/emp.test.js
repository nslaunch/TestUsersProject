// describe('Loader Visibility and Table Display Test', function () {
//     beforeEach(function () {
//         // Set up the DOM before each test
//         $('body').append('<div id="loader" style="display:block"></div>');
//         $('body').append('<div id="employeeTable" style="display:none"></div>');
        
//         // Simulate document ready behavior by executing the script
//         // $(document).ready(function () {
//         //     $("#loader").show();
//         //     $("#employeeTable").hide();
//         //     setTimeout(function () {
//         //         $("#loader").hide();
//         //         $("#employeeTable").show();
//         //     }, 1500); // Simulate the 1.5 seconds timeout
//         // });
//     });

//     afterEach(function () {
//         // Clean up after each test
//         $('#loader').remove();
//         $('#employeeTable').remove();
//     });

//     it('should display loader and then show the table', function (done) {
//         // Initially, the loader should be visible
//         expect($('#loader').is(':visible')).toBe(true);
        
//         // After a 1.5 seconds timeout, check if the loader is hidden
//         setTimeout(function () {
//             // The loader should be hidden
//             expect($('#loader').is(':visible')).toBe(false);
            
//             // The table should now be visible
//             expect($('#employeeTable').is(':visible')).toBe(true);
            
//             done(); // Mark the test as done after the async actions
//         }, 1600); // Wait for a little longer than the timeout (1.5s + 100ms)
//     });
// });
