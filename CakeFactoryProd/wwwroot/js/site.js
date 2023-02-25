// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



//Limit Date picker dates to Wednesday to Sunday
$(document).ready(function () {
    var now = new Date();
    var thirtyDaysFromNow = new Date(now.getFullYear(), now.getMonth(), now.getDate() + 30);

    $('.datePicker').datepicker({
        minDate: 0,
        maxDate: thirtyDaysFromNow,
        beforeShowDay: function (date) {
            var day = date.getDay();
            return [(day == 3 || day == 4 || day == 5 || day == 6 || day == 0), ''];
        }
    });
})

