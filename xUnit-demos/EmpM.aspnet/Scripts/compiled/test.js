"use strict";
$(document).ready(function () {
    console.log("---------------extrafunc.ts loaded and msbuild re check--------------");
    var validateInput = function (input) {
        var value = input.val();
        if (typeof value === 'string') {
            var trimmedValue = value.trim();
            if (!trimmedValue) {
                input.css('border', '1px solid red');
                return false; // Invalid input
            }
            else {
                input.css('border', '1px solid green');
                return true; // Valid input
            }
        }
        else {
            input.css('border', '1px solid red');
            return false; // Invalid input
        }
    };
    // On input: Validate whenever the value changes
    $('input[name="EmployeeName"]').on('input', function () {
        validateInput($(this));
    });
    // On blur: Validate when the user leaves the input field
    $('input[name="EmployeeName"]').on('blur', function () {
        validateInput($(this));
    });
    // On focus: Reset the border (optional)
    $('input[name="EmployeeName"]').on('focus', function () {
        $(this).css('border', '1px solid #ccc'); // Reset to default border color
    });
    $('form').on('submit', function (e) {
        var isEmployeeNameValid = validateInput($('input[name="EmployeeName"]'));
        if (!isEmployeeNameValid) {
            e.preventDefault(); // Prevent form submission
            alert('Please fill out the Employee Name field.');
        }
    });
});
//# sourceMappingURL=test.js.map