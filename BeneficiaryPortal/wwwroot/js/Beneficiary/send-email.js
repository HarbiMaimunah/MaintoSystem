function SendEmail() {
    var Email = $('#email').val();

    $.ajax({
        url: 'https://localhost:44370/Beneficiary/SendEmail',
        type: 'POST',
        data: Email,
        success: function (data) {
            alert("Success");
        }
    });
}