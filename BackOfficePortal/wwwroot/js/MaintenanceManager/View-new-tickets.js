function ViewNewTickets() {
    debugger

    $.ajax({
        url: 'https://localhost:44370/MaintenanceManager/GetNewTickets',
        type: 'GET',
        dataType: 'JSON',
        success: OnSuccess
    });
}

function OnSuccess(response) {

    var ticketsList = response;
    list = document.getElementById("NewTicketsList");


    ticketsList.forEach((item) => {
        li = document.createElement("li");
        li.innerText = item;
        list.appendChild(li);
    })
}