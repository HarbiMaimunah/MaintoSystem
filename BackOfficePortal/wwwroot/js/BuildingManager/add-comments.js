function AddComments() {
    debugger
    //var BuildingManagerComment = { BuildingManagerComment: $('#comments').val() }
    var BuildingManagerComment = $('#comments').val();
    //var comment = JSON.stringify(BuildingManagerComment);
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
