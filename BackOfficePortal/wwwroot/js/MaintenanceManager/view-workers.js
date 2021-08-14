function ViewWorkers() {
    debugger

    $.ajax({
        url: 'https://localhost:44370/MaintenanceManager/GetWorkers',
        type: 'GET',
        dataType: 'JSON',
        success: OnSuccess
    });
}

function OnSuccess(response) {

    var ticketsList = response;
    list = document.getElementById("WorkersList");


    ticketsList.forEach((item) => {
        li = document.createElement("li");
        li.innerText = item;
        list.appendChild(li);
    })
}