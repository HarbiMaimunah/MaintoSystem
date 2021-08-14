function ViewBuilding() {
    debugger
    $.ajax({
        url: 'https://localhost:44370/MaintenanceWorker/GetTicket',
        type: 'GET',
        success: OnSuccess
    });
}
function OnSuccess(response) {
    var ticket = response;
    $('#TicketId').html(ticket.Id);
    $('#BuildingManagerComment').html(ticket.BuildingManagerComment);
    $('#BeneficiaryID').html(ticket.BeneficiaryID);
    $('#Status').html(ticket.Status);
    $('#Date').html(ticket.Date);
    $('#Description').html(ticket.Description);
}
