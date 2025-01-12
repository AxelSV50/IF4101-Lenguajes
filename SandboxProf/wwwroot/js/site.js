﻿// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function Add() {

    var student = {

        name: $('#name').val(),
        email: $('#email').val(),
        password: $('#password').val()
    };

    if (student.name == '' || student.email == '' || student.password == '') {
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
                $('#name').val('');
                $('#email').val('');
                $('#password').val('');
                $('#validation').text('Registered succesfully');
                $('#validation').css('color', 'green');
            },
            error: function (errorMessage) {
                $('#password').val('');
                $('#validation').text("An error occurred");
                $('#validation').css('color', 'red');
            }
        });
    }
}
