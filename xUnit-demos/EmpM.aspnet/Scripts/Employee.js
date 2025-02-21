

$(document).ready(function () {
    console.log("--------------------Employee.js loaded--------------");
    // console.log(jQuery.fn);

    $("#loader").show();
    $("#employeeTable").hide();
    setTimeout(function () {
        $("#loader").hide();
        $("#employeeTable").show();
    }, 1500);

    if ($("#kendoButton").length > 0) {
        $("#kendoButton").kendoButton({
            click: function () {
                alert("Kendo Button clicked!");
            }
        });
    }
    if (window.kendo) {
        console.log("Kendo UI is loaded and available!");

        // Initialize Kendo DropDownList
        $("#BloodGroup").kendoDropDownList({
            optionLabel: "-Select-",
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "A+", value: "A+" },
                { text: "B+", value: "B+" },
                { text: "AB+", value: "AB+" },
                { text: "O+", value: "O+" },
                { text: "A-", value: "A-" },
                { text: "B-", value: "B-" },
                { text: "O-", value: "O-" }
            ]
        });
    } else {
        console.error("Kendo UI is not available.");
    }

    console.log("@Url.Action('GetDepartments', 'Employee')"); // Log URL for debugging
    var departmentsUrl2 = $('#sel').data('departments-url');
    console.log("Departments URL through data: ", departmentsUrl2);
    var departmentsUrl = '/Employee/GetDepartments';
    console.log("Departments URL: ", departmentsUrl); // Check if URL is correct in console

    $.ajax({
        url: departmentsUrl,
        type: 'GET',
        success: function (response) {
            console.log("Departments:", response);
        },
        error: function (xhr, status, error) {
            console.error("Error fetching departments:", error);
        }
    });
    $("#DepartmentId").kendoDropDownList({
        optionLabel: "-Select-",
        dataTextField: "text",
        dataValueField: "value",
        dataSource: {
            transport: {
                read: {
                    url: departmentsUrl, // The URL rendered
                    dataType: "json",
                    success: function (data) {
                        console.log("Departments fetched successfully:", data); // Log response
                    },
                    error: function (xhr, status, error) {
                        console.log("Error fetching departments:", error); // Log errors
                    }
                }
            }
        }
    });

    $(".edit-btn").kendoButton({

        click: function () {
            var row = $(this.element).closest("tr");
            console.log('Before: View Mode Visible:', row.find('.view-mode').is(":visible"));
            console.log('Before: Edit Mode Visible:', row.find('.edit-mode').is(":visible"));

            // Hide view-mode and show edit-mode
            row.find(".view-mode").hide();
            row.find(".edit-mode").show();
            row.find(".edit-btn").hide();
            row.find(".save-btn").show();
            row.find(".delete-btn").hide();

            console.log('After: View Mode Visible:', row.find('.view-mode').is(":visible"));
            console.log('After: Edit Mode Visible:', row.find('.edit-mode').is(":visible"));        }
    });


    $(".save-btn").click(function () {
        var row = $(this).closest("tr");
        var employeeId = row.data("employee-id");

        var updatedData = {
            EmployeeId: employeeId,
            EmployeeName: row.find("input").eq(0).val(),
            Designation: row.find("input").eq(1).val(),
            NID: row.find("input").eq(2).val(),
            JoiningDate: row.find("input").eq(3).val(),
            Code: row.find("input").eq(4).val(),
            DepartmentId: row.find("select").eq(0).val(),
            BloodGroup: row.find("select").eq(1).val()
        };

        $.ajax({
            url: '/Employee/Edit',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(updatedData),
            success: function (response) {
                if (response === "Success") {
                    location.reload();
                } else {
                    alert("Failed to update employee details.");
                }
            },
            error: function () {
                alert("Error updating employee details.");
            }
        });
    });

    //$(document).on("click", ".save-btn", function () {
    //    // Click handler logic
    //    console.log("Save button clicked!");
    //});

    //$(document).on("click", ".save-btn", function () {
    //    //$(".save-btn").click(function () {
    //    console.log("Save button clicked!--------------");

    //    var row = $(this).closest("tr");
    //    var employeeId = row.data("employee-id");

    //    var updatedData = {
    //        EmployeeId: employeeId,
    //        EmployeeName: row.find("input").eq(0).val(),
    //        Designation: row.find("input").eq(1).val(),
    //        NID: row.find("input").eq(2).val(),
    //        JoiningDate: row.find("input").eq(3).val(),
    //        Code: row.find("input").eq(4).val(),
    //        DepartmentId: row.find("select").eq(0).val(),
    //        BloodGroup: row.find("select").eq(1).val()
    //    };

    //    fetch('/Employee/Edit', {
    //        method: 'POST',
    //        headers: {
    //            'Content-Type': 'application/json'
    //        },
    //        body: JSON.stringify(updatedData)
    //    })
    //        .then(response => {
    //            console.log(response)
    //            if (!response.ok) {
    //                // If the response status is not OK (e.g., 400 or 500), throw an error
    //                throw new Error(`HTTP error! status: ${response.status}`);
    //            }
    //            return response.text(); // Assuming server responds with plain text
    //        })
    //        .then(responseText => {
    //            console.log("API Response:", responseText); // Debugging the actual response
    //            location.reload();

    //            //if (responseText.trim().toLowerCase() === "success") {
    //            //    // Reload the page if the update was successful
    //            //    location.reload();
    //            //} else {
    //            //    alert("Failed to update employee details: " + responseText);
    //            //}
    //        })
    //        .catch(error => {
    //            // Handle errors like network issues or non-OK HTTP responses
    //            console.error("Error:", error);
    //            alert("Error updating employee details: " + error.message);
    //        });
    //});

   
});
