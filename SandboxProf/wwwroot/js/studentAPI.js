// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(() => {

    GetNationalities();
    LoadData();

    $(document).on('submit', '#student-entry-form', function () {

        if (!$('#id').val()) {

            Add();
        } else {

            UpdateStudent();
        }
        return false;
    });
})


function UpdateStudent() {

    var student = {
        id: $('#id').val(),
        name: $('#name').val(),
        email: $('#email').val(),
        password: $('#password').val(),
        nationalityId: parseInt($('#nationality').val())

    };

    var nationality = {

        id: parseInt($('#nationality').val()),
        name: $('#nationality').find('option:selected').text()

    };

    student.nationality = nationality;

    if (student.name == '' || student.email == '' || student.password == '' || nationality.name == 'Select your nationality') {

        $('#validation').text("Please complete the form");
        $('#validation').css('color', 'red');

    } else {

        $.ajax({
            url: "/Student/Put",
            data: JSON.stringify(student), //converte la variable estudiante en tipo json
            type: "PUT",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                $('#validation').text("Updated successfully");
                $('#validation').css('color', 'green');
                $('#name').val('');
                $('#email').val('');
                $('#password').val('');
                $('#nationality').val($("#nationality option:first").val());

                LoadData();
            },
            error: function (errorMessage) {
                if (errorMessage === "no connection") {
                    $('#validation').text("Error en la conexión.");
                }
                $('#validation').text("User not added");
                $('#validation').css('color', 'red');
                $('#password').val('');
                $('#id').val('');
            }
        });

    }
}
function Add() {

    var student = {

        name: $('#name').val(),
        email: $('#email').val(),
        password: $('#password').val(),
        nationality_id: parseInt($('#nationality').val())
    };

    var nationality = {
        id: parseInt($('#nationality').val()),
        name: $('#nationality').find('option:selected').text()
    }

    student.nationality = nationality;

    if (student.name == '' || student.email == '' || student.password == '' || nationality.name == 'Select your nationality') {
        $('#validation').text('Please complete the form');
        $('#validation').css('color', 'red');

    } else {
        $.ajax({
            url: "/Student/Post",
            data: JSON.stringify(student), //converte la variable estudiante en tipo json
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                $('#name').val('');
                $('#email').val('');
                $('#password').val('');
                $('#validation').text('Registered succesfully');
                $('#validation').css('color', 'green');

                $('#nationality').prop('selectedIndex', 0);

                LoadData();

            },
            error: function (errorMessage) {
                $('#password').val('');
                alert(errorMessage.responseText)
                $('#validation').text("An error occurred");
                $('#validation').css('color', 'red');

                $('#nationality').prop('selectedIndex', 0);

            }
        });
    }
}
function GetNationalities() {

    $.ajax({
        url: "/Home/GetNationalities",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            var htmlSelect = ''
            $.each(result, (key, item) => {

                htmlSelect += '<option value ="' + item.id + '">' + item.name + '</option>';
            })

            $('#nationality').append(htmlSelect);
        },
        error: function (errorMessage) {

        }
    });
}


function LoadData() {

    $.ajax({
        url: "/Student/Get",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            var htmlTable = ''
            $.each(result, (key, item) => {

                htmlTable += '<tr>';
                htmlTable += '<td>' + item.id + '</td>';
                htmlTable += '<td>' + item.name + '</td>';
                htmlTable += '<td>' + item.email + '</td>';
                htmlTable += '<td>' + item.nationality.name + '</td>';
                htmlTable += '<td>' + '<a href = "#about" onclick = "GetById(\'' + item.id + '\')"> Edit </a> |' +
                    '<a href = "#about" onclick = "Delete(\'' + item.id + '\')"> Delete </a>' + '</td>';
                htmlTable += "</tr>";

            });

            $('#students-tbody').html(htmlTable);

        },
        error: function (errorMessage) {

            alert(errorMessage.responseText)
        }
    });
}

//NO TIENE MÉTODO EN EL 
function GetStudentByEmail(email) {

    $.ajax({
        url: "/Student/GetById",
        type: "GET",
        data: { email },
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            $('#id').val(result.id);
            $('#name').val(result.name);
            $('#email').val(result.email);
            $('#nationality').val(result.nationality.id);
        },
        error: function (errorMessage) {
            alert(errorMessage.responseText)

        }
    });


}

function DeleteStudent(email) {

    var retVal = confirm("Are you sure you want to delete the selected user?");
    if (retVal) {

        $.ajax({
            url: "/Student/Delete",
            type: "POST",
            data: { id },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                $('#validation').text("Student deleted succesfully");
                $('#validation').css('color', 'red');

                LoadData();
            },
            error: function (errorMessage) {

                alert(errorMessage.responseText)
            }
        });
    }
}

