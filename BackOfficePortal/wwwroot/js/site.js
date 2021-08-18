// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function () {
    $('#myTable').DataTable();
});


function ViewTickets() {
    debugger

    $.ajax({
        url: '/BuildingManager/GetTickets',
        type: 'GET',
        dataType: 'JSON',
        success: function () {
            console.log("success")
        }
    });
}

function OnSuccess(response) {

    var ticketsList = response;
    list = document.getElementById("TicketsList");


    ticketsList.forEach((item) => {
        li = document.createElement("li");
        li.innerText = item;
        list.appendChild(li);
    })
}