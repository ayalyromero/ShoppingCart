$(document).ready(function () {


    $.post("GetProductsInBasket", function (data) {
        $("#itemsCount").html(data);

    }, "json");

});