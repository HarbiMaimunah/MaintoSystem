function RespondToTicket() {

    var Id = $('#TicketId').val();
    var TicketRespond = {
        Id: $('#Status').val(),
        Id: $('#reason').val()
    }
    $.ajax({
        url: 'https://localhost:44370/MaintenanceManager/PostRespond',
        type: 'POST',
        data: Id, TicketRespond,
        success: function (data) {
            alert("Success");
        }
    });
}
