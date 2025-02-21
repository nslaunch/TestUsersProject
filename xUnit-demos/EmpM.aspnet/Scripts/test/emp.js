
// $(document).ready(function () {
//     console.log("--------------------jquet button clicked!--------------");

//     $("#loader").show();
//     $("#employeeTable").hide();
//     setTimeout(function () {
//         $("#loader").hide();
//         $("#employeeTable").show();
//     }, 1500);

//     // $(document).ajaxStart(function () {
//     //     $("#loader").show();
//     // });

//     // $(document).ajaxStop(function () {
//     //     $("#loader").hide();
//     // });

//     //$(".edit-btn").click(function () {
//     //    var row = $(this).closest("tr");
//     //    console.log('Before: View Mode Visible:', row.find('.view-mode').is(":visible"));

//     //    row.find(".view-mode").hide();
//     //    row.find(".edit-mode").show();
//     //    row.find(".edit-btn").hide();
//     //    row.find(".save-btn").show();
//     //    row.find(".delete-btn").hide();

//     //    console.log('After: View Mode Visible:', row.find('.view-mode').is(":visible"));
//     //});
//     $(".edit-btn").click(function () {
//         console.log("Save button clicked!--------------");

//         var row = $(this).closest("tr");
//         console.log('Before: View Mode Visible:', row.find('.view-mode').is(":visible"));
//         console.log('Before: Edit Mode Visible:', row.find('.edit-mode').is(":visible"));

//         // Hide view-mode and show edit-mode
//         row.find(".view-mode").hide();
//         row.find(".edit-mode").show();
//         row.find(".edit-btn").hide();
//         row.find(".save-btn").show();
//         row.find(".delete-btn").hide();

//         console.log('After: View Mode Visible:', row.find('.view-mode').is(":visible"));
//         console.log('After: Edit Mode Visible:', row.find('.edit-mode').is(":visible"));
//     });
//     //$(document).on("click", ".save-btn", function () {
//     //    // Click handler logic
//     //    console.log("Save button clicked!");
//     //});

//     //$(document).on("click", ".save-btn", function () {
//     //    //$(".save-btn").click(function () {
//     //    console.log("Save button clicked!--------------");

//     //    var row = $(this).closest("tr");
//     //    var employeeId = row.data("employee-id");

//     //    var updatedData = {
//     //        EmployeeId: employeeId,
//     //        EmployeeName: row.find("input").eq(0).val(),
//     //        Designation: row.find("input").eq(1).val(),
//     //        NID: row.find("input").eq(2).val(),
//     //        JoiningDate: row.find("input").eq(3).val(),
//     //        Code: row.find("input").eq(4).val(),
//     //        DepartmentId: row.find("select").eq(0).val(),
//     //        BloodGroup: row.find("select").eq(1).val()
//     //    };

//     //    fetch('/Employee/Edit', {
//     //        method: 'POST',
//     //        headers: {
//     //            'Content-Type': 'application/json'
//     //        },
//     //        body: JSON.stringify(updatedData)
//     //    })
//     //        .then(response => {
//     //            console.log(response)
//     //            if (!response.ok) {
//     //                // If the response status is not OK (e.g., 400 or 500), throw an error
//     //                throw new Error(`HTTP error! status: ${response.status}`);
//     //            }
//     //            return response.text(); // Assuming server responds with plain text
//     //        })
//     //        .then(responseText => {
//     //            console.log("API Response:", responseText); // Debugging the actual response
//     //            location.reload();

//     //            //if (responseText.trim().toLowerCase() === "success") {
//     //            //    // Reload the page if the update was successful
//     //            //    location.reload();
//     //            //} else {
//     //            //    alert("Failed to update employee details: " + responseText);
//     //            //}
//     //        })
//     //        .catch(error => {
//     //            // Handle errors like network issues or non-OK HTTP responses
//     //            console.error("Error:", error);
//     //            alert("Error updating employee details: " + error.message);
//     //        });
//     //});

//     $(".save-btn").click(function () {
//         var row = $(this).closest("tr");
//         var employeeId = row.data("employee-id");

//         var updatedData = {
//             EmployeeId: employeeId,
//             EmployeeName: row.find("input").eq(0).val(),
//             Designation: row.find("input").eq(1).val(),
//             NID: row.find("input").eq(2).val(),
//             JoiningDate: row.find("input").eq(3).val(),
//             Code: row.find("input").eq(4).val(),
//             DepartmentId: row.find("select").eq(0).val(),
//             BloodGroup: row.find("select").eq(1).val()
//         };

//         $.ajax({
//             url: '/Employee/Edit',
//             type: 'POST',
//             contentType: 'application/json',
//             data: JSON.stringify(updatedData),
//             success: function (response) {
//                 if (response === "Success") {
//                     location.reload();
//                 } else {
//                     alert("Failed to update employee details.");
//                 }
//             },
//             error: function () {
//                 alert("Error updating employee details.");
//             }
//         });
//     });
// });
