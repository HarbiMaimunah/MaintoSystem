$(document).ready(function () {
    $('#buildingsList').change(function () {
        let Id = $('#buildingsList option:selected').val();
        var BuildingNumber = Id;
        $.ajax({
            type: 'GET',
            data: BuildingNumber ,
            url: '/BeneficiaryEntry/ListFloors',
            success: function (data) {
                let items = '';
                $.each(data, function (i, floor) {
                    items += '<option value="' + floor.value + '">' + floor.text + '</option>';
                });

                $("#floorsList").html(items);
            }
        });
    });
});