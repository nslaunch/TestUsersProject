$(document).ready(() => {
    console.log("---------------Validation.ts loaded and msbuild re check--------------");
    
    const validateInput = (
        input: JQuery<HTMLElement>,
        type: 'text' | 'alpha' | 'numeric' | 'date' | 'optional' | 'select' | 'kendo-select'
    ): boolean => {
        let isValid = false;
        switch (type) {
            case 'text': // Generic non-empty check
                isValid = !!input.val()?.toString().trim();
                break;
            case 'alpha': // Only alphabets
                isValid = /^[A-Za-z\s]+$/.test(input.val()?.toString() ?? '');
                break;
            case 'numeric': // Only numbers
                isValid = /^[0-9]+$/.test(input.val()?.toString() ?? '');
                break;
            case 'date': // Validate if a date is selected and in the format YYYY-MM-DD
                isValid = !!input.val() && /^\d{4}-\d{2}-\d{2}$/.test(input.val()?.toString() ?? '');
                break;
            case 'select': // Validate standard dropdowns
                isValid = input.val() !== '0';
                break;
                case 'kendo-select': // Validate Kendo DropDownList
                const dropdownlist = input.data('kendoDropDownList');
                isValid = dropdownlist ? dropdownlist.value() !== '' && dropdownlist.value() !== '0' : false;
                break;
            case 'optional': // Optional field (always valid)
                isValid = true;
                break;
        }

        input.closest('.form-group').css('border', isValid ? '1px solid green' : '1px solid red');
        return isValid;
    };

    // Validation bindings for other fields (as before)

    $('input[name="EmployeeName"]').on('input blur', function () {
        validateInput($(this), 'alpha');
    });

    $('input[name="Designation"]').on('input blur', function () {
        validateInput($(this), 'alpha');
    });

    $('input[name="NID"]').on('input blur', function () {
        validateInput($(this), 'numeric');
    });

    $('input[name="JoiningDate"]').on('input blur', function () {
        validateInput($(this), 'date');
    });

    // Kendo DropDownList validation
    $('#BloodGroup').on('change blur', function () {
        validateInput($(this), 'kendo-select');
    });

    $('#DepartmentId').on('change blur', function () {
        validateInput($(this), 'kendo-select');
    });

    // On form submission
    $('form').on('submit', function (e: JQuery.TriggeredEvent) {
        const isEmployeeNameValid = validateInput($('input[name="EmployeeName"]'), 'alpha');
        const isDesignationValid = validateInput($('input[name="Designation"]'), 'alpha');
        const isNIDValid = validateInput($('input[name="NID"]'), 'numeric');
        const isJoiningDateValid = validateInput($('input[name="JoiningDate"]'), 'date');
        const isDepartmentIdValid = validateInput($('#DepartmentId'), 'kendo-select');
        const isBloodGroupValid = validateInput($('#BloodGroup'), 'kendo-select');

        if (!isEmployeeNameValid || !isDesignationValid || !isNIDValid || !isJoiningDateValid || !isDepartmentIdValid || !isBloodGroupValid) {
            e.preventDefault(); // Prevent form submission
            alert('Please fill out all required fields correctly.');
        }

    });
});
