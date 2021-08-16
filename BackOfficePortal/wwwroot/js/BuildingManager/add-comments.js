function AddComments() {
            
      var BuildingManagerComment = $('#comments').val();

       $.ajax({
           url: 'https://localhost:44370/BuildingManager/PostComment',
           type: 'POST',
           data: BuildingManagerComment,
            success: function (data) {
                alert("Success");
            }
        });
 }
