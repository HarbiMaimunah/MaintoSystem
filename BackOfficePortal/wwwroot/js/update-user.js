function UpdateUser() {
    debugger
   
    var user =
    {
        Name: $('#updateName').val(),
        Phone: $('#updatePhone').val(),
        Email: $('#updateEmail').val(),
        floor: $('#updateFloor').val(),
        building: $('#updateBuilding').val(),
        Password: $('#updatePassword').val() 
    }
    var userStringfy = JSON.stringify(user);
    $.ajax({
        url: 'https://localhost:44370/Home/PutUser',
        type: 'POST',
        data: userStringfy,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            alert("Success");
        }

    });
}