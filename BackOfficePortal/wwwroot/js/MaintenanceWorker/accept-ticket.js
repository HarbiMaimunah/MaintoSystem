function AcceptTicket() {

    var Id = $('#TicketId').val()
   
    
    $.ajax({
        url: 'https://localhost:44370/MaintenanceWorker/AcceptTicket',
        type: 'POST',
        data: Id,
        success: function (data) {
            alert("Success");
        }
    });
}
