function AddComments() {
            
      var Id = $('#updateId').val();
      var BuildingManagerComment = $('#comments').val();

       $.ajax({
           url: 'https://localhost:44370/BuildingManager/PostComment',
           type: 'POST',
           data: Id, BuildingManagerComment,
            success: function (data) {
                alert("Success");
            }
        });
 }
