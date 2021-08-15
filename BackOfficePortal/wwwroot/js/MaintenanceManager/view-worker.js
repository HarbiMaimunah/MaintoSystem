function ViewWorker() {
    debugger
    $.ajax({
        url: 'https://localhost:44370/MaintenanceManager/GetWorker',
        type: 'GET',
        success: OnSuccess
    });
}
function OnSuccess(response) {
    var ticket = response;
    $('#Id').html(ticket.Id);
    $('#Name').html(ticket.Name);
    $('#Phone').html(ticket.Phone);
    $('#Email').html(ticket.Email);
  
}
