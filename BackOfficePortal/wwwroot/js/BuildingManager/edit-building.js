function EditBuilding() {
            debugger
             var Id = $('#updatefloor').val();
             var BuildingManagerComment = $('#Owned').val();


            $.ajax({
                url: 'https://localhost:44370/BuildingManager/PutBuilding',
                type: 'POST',
                data: Id, BuildingManagerComment,
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                     alert("Success");
                        }

            });
        }
