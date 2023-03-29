// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



//Limit Date picker dates to Wednesday to Sunday

// Get the current date
//var currentDate = new Date();

// /*Loop through the next 30 days*/
//for (var i = 0; i < 30; i++) {
//     /*Calculate the next date*/
//    var nextDate = new Date(currentDate);
//    nextDate.setDate(currentDate.getDate() + i);

//     //Check if the day falls on a Wednesday through Sunday
//    if (nextDate.getDay() >= 3 && nextDate.getDay() <= 6) {
//         //Convert the date to a string in the format "yyyy-mm-dd"
//        var dateString = nextDate.toISOString().slice(0, 10);

//         //Set the minimum value for the date picker to the current day
//        if (i == 0) {
//            document.getElementById("datePicker").min = currentDate.toISOString().slice(0, 10);
//        }

//         //Set the maximum value for the date picker
//        document.getElementById("datePicker").max = dateString;
//    }
//}

var currentDate = new Date();

for (var i = 0; i < 30; i++) {
    var nextDate = new Date(currentDate);
    nextDate.setDate(currentDate.getDate() + i);

    // Skip over Monday and Tuesday
    if (nextDate.getDay() == 1 || nextDate.getDay() == 2) {
        continue;
    }

    // Check if the day falls on a Wednesday through Sunday
    if (nextDate.getDay() >= 3 && nextDate.getDay() <= 6) {
        var maxDate = nextDate.toISOString().slice(0, 10);

        // Set the minimum value for the date picker to the current day
        if (i == 0) {
            document.getElementById("datePicker").min = currentDate.toISOString().slice(0, 10);
        }

        // Set the maximum value for the date picker
        document.getElementById("datePicker").max = maxDate;
    }
}







