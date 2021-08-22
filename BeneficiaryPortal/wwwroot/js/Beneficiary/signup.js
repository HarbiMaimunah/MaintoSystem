function  GetFloor(URL){
    debugger
    let Id = $('#buildingsList option:selected').val();
    $.ajax({
        type: 'GET',
        url: URL + "?BuildingNumber=" + Id,
        success: function (data) {
            let items = '<option disabled selected>Select Floor</option>';
            debugger
            $.each(data, function (i, floor) {
                items += '<option value="' + floor.value + '">' + floor.text + '</option>';
            });

            $("#floorsList").html(items);
        }
    });
}