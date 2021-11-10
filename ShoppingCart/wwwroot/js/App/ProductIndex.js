$(document).ready(function () {

    $(".add-product").on("click", function () {

        let dataForm = {
            Id: 0,
            BasketId: 0,
            ProductId: $(this).data("product-id"),
            Quantity: $("#quantity_" + $(this).data("index")).val(),
        }

        $.post($(this).data("url"), dataForm, function (data) {
            if (data.statusResponse = true) {

                $("#itemsCount").html(data.numberItem);

                $.toast({
                    heading: 'Success',
                    text: 'Product added to cart',
                    stack: 1,
                    showHideTransition: "slide",
                    icon: 'success',
                    hideAfter: 5000,
                    position: "bottom-center", 
                    textAlign: "left",
                });

            } else {
                $.toast({
                    heading: 'Error',
                    text: dataResponse.message,
                    stack: 1,
                    showHideTransition: "slide",
                    icon: 'error',
                    hideAfter: 5000,
                    position: "bottom-center", 
                    textAlign: "left",
                });
            }
        }, "json");
    });    

    $.post($("#urlProducts").val(), function (data) {
            $("#itemsCount").html(data);

        }, "json");

});

