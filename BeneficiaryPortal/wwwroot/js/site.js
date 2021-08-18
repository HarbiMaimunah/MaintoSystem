// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function () {
    $("#BuildingNumber").change(function () {
        $.get("http://localhost:16982/api/BeneficiaryEntry/ListFloors", { buildingID: $("#BuildingNumber").val() }, function (data) {
            $("#FloorNumber").empty();
            $.each(data, function (index, row) {
                $("#FloorNumber").append("<option value='" + row.Id + "'>" + row.Number + "</option>")
            });
        });
    });
});

