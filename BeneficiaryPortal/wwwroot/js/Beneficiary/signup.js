﻿////$(document).ready(function () {
////    $('#buildingsList').change(function () {
////        debugger
////        let Id = $('#buildingsList option:selected').val();
////        $.ajax({
////            type: 'GET',
////            url: '@Url.Action("ListFloors", "BeneficiaryEntry")' + "?BuildingNumber=" + Id,
////            success: function (data) {
////                let items = '';
////                debugger
////                $.each(data, function (i, floor) {
////                    items += '<option value="' + floor.value + '">' + floor.text + '</option>';
////                });

////                $("#floorsList").html(items);
////            }
////        });
////    });
////});


function  GetFloor( URL){
    let Id = $('#buildingsList option:selected').val();
    $.ajax({
        type: 'GET',
        url: URL + "?BuildingNumber=" + Id,
        success: function (data) {
            let items = '<option disabled selected>Select Floor</option>';
            $.each(data, function (i, floor) {
                items += '<option value="' + floor.value + '">' + floor.text + '</option>';
            });

            $("#floorsList").html(items);
        }
    });
}