    function ViewBuilding() {
        debugger
        $.ajax({
            url: 'https://localhost:44370/BuildingManager/GetBuilding',
            type: 'GET',
            success: OnSuccess
        });
        }
        function OnSuccess(response) {
            var building = response;
            $('#BuildingId').html(building.Id);
            $('#BuildingNumber').html(building.Number);
            $('#BuildingCity').html(building.city);
        }
 