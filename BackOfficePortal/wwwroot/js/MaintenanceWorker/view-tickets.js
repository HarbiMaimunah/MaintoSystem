function ViewTickets() {
    debugger

    $.ajax({
        url: 'https://localhost:44370/MaintenanceWorker/GetTickets',
        type: 'GET',
        dataType: 'JSON',
        success: OnSuccess
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