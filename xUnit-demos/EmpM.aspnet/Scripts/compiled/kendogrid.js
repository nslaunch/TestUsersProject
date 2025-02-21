"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
// Import necessary types and libraries
var $ = require("jquery");
require("@progress/kendo-ui");
$(document).ready(function () {
    // Sample data for the Kendo Grid
    var sampleData = [
        {
            EmployeeName: "John Doe",
            Designation: "Manager",
            NID: "123456789",
            JoiningDate: "2023-01-01",
        },
        {
            EmployeeName: "Jane Smith",
            Designation: "Developer",
            NID: "987654321",
            JoiningDate: "2022-12-15",
        },
        {
            EmployeeName: "Alice Johnson",
            Designation: "HR",
            NID: "567890123",
            JoiningDate: "2023-02-10",
        },
    ];
    // Parse employee data from a data attribute
    var employeeData = JSON.parse($('#employeeGrid').attr('data-employee-data') || '[]');
    // Convert JoiningDate to JavaScript Date objects
    employeeData.forEach(function (employee) {
        if (typeof employee.JoiningDate === "string") {
            employee.JoiningDate = new Date(parseInt(employee.JoiningDate.substr(6)));
        }
    });
    // Initialize the Kendo Grid
    $("#employeeGrid").kendoGrid({
        dataSource: {
            data: sampleData, // Use the parsed employee data or sample data
            pageSize: 10, // Set the number of records per page
        },
        pageable: true, // Enable pagination
        sortable: true, // Enable sorting
        filterable: true, // Enable filtering
        columns: [
            { field: "EmployeeName", title: "Employee Name" },
            { field: "Designation", title: "Designation" },
            { field: "NID", title: "National ID" },
            { field: "JoiningDate", title: "Joining Date", format: "{0:yyyy-MM-dd}" },
            { field: "Code", title: "Code" },
            { field: "DepartmentName", title: "Department Name" },
            { field: "BloodGroup", title: "Blood Group" },
            {
                command: [
                    { name: "edit", text: "Edit" }, // Adds an Edit button
                    {
                        name: "destroy",
                        text: "Delete",
                        click: function (e) {
                            e.preventDefault(); // Prevent default KendoGrid delete action
                            var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
                            var employeeId = dataItem.EmployeeId; // Get Employee ID from the row data
                            // Perform the delete operation via AJAX
                            $.ajax({
                                url: '/Employee/Delete',
                                type: 'POST',
                                data: { id: employeeId },
                                success: function (response) {
                                    var _a;
                                    if (response.success) {
                                        alert(response.message);
                                        // Refresh grid or reload data
                                        (_a = $("#employeeGrid")
                                            .data("kendoGrid")) === null || _a === void 0 ? void 0 : _a.dataSource.read();
                                    }
                                    else {
                                        alert(response.message);
                                    }
                                },
                                error: function () {
                                    alert("Error deleting employee.");
                                },
                            });
                        },
                    },
                ],
                title: "Actions",
                width: "200px",
            },
        ],
        editable: {
            mode: "popup", // Use popup editing
            confirmation: "Are you sure you want to delete this employee?", // Confirm before deletion
        },
        save: function (e) {
            var updatedData = e.model.toJSON();
            $.ajax({
                url: '/Employee/Edit',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(updatedData),
                success: function (response) {
                    if (response === "Success") {
                        alert("Employee details updated successfully.");
                        e.sender.dataSource.read(); // Refresh the grid
                        window.location.reload();
                    }
                    else {
                        alert("Failed to update employee details.");
                    }
                },
                error: function () {
                    alert("Error updating employee details.");
                },
            });
            e.preventDefault();
        },
    });
});
//# sourceMappingURL=kendogrid.js.map