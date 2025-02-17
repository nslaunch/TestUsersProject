// Load Data in Table when document is ready
$(document).ready(function () {
    loadData();
});

function setListingError(msg) {
    let html = '';
    $('.tbody').empty();
    html += '<tr class="table-danger">';
    html += '<td colspan="6">'+msg+'</td>';
    html += '</tr>';
    $('.tbody').html(html);
}

// Load Data function
function loadData() {
    $.ajax({
        url: "/api/employee/all",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (obj) {
            var result = obj.result;
            let html = '';
            $('.tbody').empty();
            $.each(result, function (key, item) {
                let dateObj = new Date(item.createDate);
                let month = ('0' + (dateObj.getMonth() + 1)).slice(-2);
                // make date 2 digits
                let date = ('0' + dateObj.getDate()).slice(-2);
                // get 4 digit year
                let year = dateObj.getFullYear();

                html += '<tr>';
                html += '<td>' + item.id + '</td>';
                html += '<td>' + item.fullName + '</td>';
                html += '<td>' + item.email + '</td>';
                html += '<td>' + month + "/" + date + "/" + year + '</td>';
                html += '<td>' + (item.status ? 'Active' : 'Inactive') + '</td>';
                html += '<td><a href="#" onclick="return getbyID(' + item.id + ')">Edit</a> | <a href="#" onclick="Delele(' + item.id + ')">Delete</a></td>';//<a href="#" onclick="return getbyID(' + item.Id + ')">Edit</a> | 
                html += '</tr>';
            });

            if (result.length == 0) {
                html += '<tr>';
                html += '<td colspan="6">No records exist</td>';
                html += '</tr>';
            }
            $('.tbody').html(html);
        },
        error: function (errorMessage) {
            setListingError('No records exist');
            //alert(errorMessage.responseText);
        }
    });
}

const validateEmail = (email) => {
    return email.match(
        /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
    );
};

function validate() {
    let errors = [];
    //reset
    $('#Email').css('border-color', 'lightgrey');
    $('#FullName').css('border-color', 'lightgrey');

    let fullName = $('#FullName').val();
    if (!fullName) {
        errors.push("Please enter full name");
        $('#FullName').css('border-color', 'red');
    }

    let email = $('#Email').val();
    if (!email) {
        errors.push("Please enter email address");
        $('#Email').css('border-color', 'red');
    } else if (!validateEmail(email)) {
            errors.push("Please enter valid email address");
            $('#Email').css('border-color', 'red');
    }

    if (errors.length > 0) {
        let strError = '';
        $(errors).each(function (i, e) {
            strError += e + "\n";
        });
        alert(strError);
    }

    return errors.length == 0;
}

// Add Data Function
function Add() {
    let res = validate();
    if (res == false) {
        return false;
    }

    var curID = $('#Id').val();

    let empObj = {
        id: (curID===""?0:parseInt(curID)),
        email: $('#Email').val(),
        fullName: $('#FullName').val(),
        createDate: $('#CreatedDate').val(),
        status: $('#Status').val()==="true"
    };

    $.ajax({
        url: (empObj.id === 0 ? "/api/employee/add" : "/api/employee/update"),
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (obj) {
            var result = obj.result;
            loadData();
            $('#myModal').modal('hide');
            clearTextBox();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

// Function for getting the Data Based upon Employee ID
function getbyID(EmpID) {
    $('#Email').css('border-color', 'lightgrey');
    $('#FullName').css('border-color', 'lightgrey');
    $('#CreatedDate').css('border-color', 'lightgrey');
    $('#Status').css('border-color', 'lightgrey');
    $.ajax({
        url: "/api/employee/" + EmpID,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (obj) {
            var result = obj.result;
            $('#Id').val(result.id);
            $('#Email').val(result.email);
            $('#FullName').val(result.fullName);
            $('#CreatedDate').val(result.createDate);
            $('#Status').val(result.status+"");

            $('#myModal').modal('show');
            //$('#btnUpdate').show();
            $('#btnAdd').show();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

// Function for deleting employee's record
function Delele(ID) {
    let ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/api/employee/" + ID,
            type: "DELETE",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (obj) {
                var result = obj.result;
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

// Function for clearing the textboxes
function clearTextBox() {
    $('#Id').val("");
    $('#Email').val("");
    $('#FullName').val("");
    $('#CreatedDate').val("");
    $('#Status').val("true");
    //$('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#Email').css('border-color', 'lightgrey');
    $('#FullName').css('border-color', 'lightgrey');
    $('#CreatedDate').css('border-color', 'lightgrey');
    $('#Status').css('border-color', 'lightgrey');
}