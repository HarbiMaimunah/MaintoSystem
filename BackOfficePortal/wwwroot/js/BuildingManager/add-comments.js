function AddComments() {
    debugger
    var BuildingManagerComment = $('#comments').val();
       $.ajax({
           url: 'https://localhost:44370/BuildingManager/PostComment',
           type: 'POST',
           dataType: 'JSON',
           data: JSON.stringify(BuildingManagerComment),
           contentType: 'application/json; charset=utf-8',
            success: function (data) {
                alert("Success");
            }
        });
 }
