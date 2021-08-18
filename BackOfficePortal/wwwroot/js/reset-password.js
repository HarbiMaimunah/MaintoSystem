function ResetPassword() {

    var TempPassword = $('#TempPassword').val();
    var Password = $('#Password').val();
    var ReEnteredPassword = $('#ReEnteredPassword').val();

    $.ajax({
        url: 'https://localhost:44370/Beneficiary/PostPassword',
        type: 'POST',
        data: TempPassword, Password, ReEnteredPassword,
        success: function (data) {
            alert("Success");
        }
    });
}