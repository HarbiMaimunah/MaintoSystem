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