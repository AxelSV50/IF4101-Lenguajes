// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(() => {

    GetNationalities();
    LoadData();

})
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
            url: "/Home/Insert",
            data: JSON.stringify(student), //converte la variable estudiante en tipo json
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                //Tarea moral: Separar esto en un método
                $('#name').val('');
                $('#email').val('');
                $('#password').val('');
                $('#validation').text('Registered succesfully');
                $('#validation').css('color', 'green');

                $('#nationality').prop('selectedIndex', 0);
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

function LoadData() {

    $.ajax({
        url: "/Home/GetAllStudents",
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
                htmlTable += '<td>' + '<a href = "#about" onclick = "GetStudentByEmail(\'' + item.email + '\')"> Edit </a> |' +
                    '<a href = "#about" onclick = "DeleteStudent(\'' + item.email + '\')"> Delete </a>' + '</td>';
                htmlTable += "</tr>";

            });

            $('#students-tbody').html(htmlTable);

        },
        error: function (errorMessage) {

            alert(errorMessage.responseText)
        }
    });
}

function GetStudentByEmail(email) {

    $.ajax({
        url: "/Home/GetStudentByEmail",
        type: "GET",
        data: { email },
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

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

    $.ajax({
        url: "/Home/DeleteStudent",
        type: "GET",
        data: { email },
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            $('#validation').text("Student deleted succesfully");
            LoadData();
        },
        error: function (errorMessage) {

            alert(errorMessage.responseText)
        }
    });

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


function GetMajors() {

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