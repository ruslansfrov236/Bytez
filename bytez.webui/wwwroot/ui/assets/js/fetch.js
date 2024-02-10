$(document).ready(function () {
    $(".btn-message").click(function (e) {
        e.preventDefault();

        let name = $("#name").val();
        let email = $("#emails").val();
        let phone = $("#phone").val();
        let message = $("#message").val();

        if (!name || !email || !phone || !message) {
            alert("Please fill in all required fields.");
            return;
        }

        $.ajax({
            url: 'Contact/Create',
            type: 'POST',
            data: {
                name: name,
                email: email,
                phone: phone,
                messageInfo: message
            },
            success: function (response) {
                console.log(response);
                $("#name, #emails, #phone, #message").val('');
            },
            error: function (error) {
                console.error(error);
            }
        });
    });

    $(".btn-heart").click(function () {
        let id = $(this).data('id');
        console.log(id)
        let button = $(this);

        button.prop('disabled', true);

        $.ajax({
            url: 'Wishlist/Create',
            type: 'POST',
            data: {
                ProductId: id
            },
            success: function (response) {
                console.log(response);
                button.prop('disabled', false);
            },
            error: function (error) {
                console.error(error);
                button.prop('disabled', false);
            }
        });
    });

    $(".btn-heart-active").click(function () {
        let id = $(this).data('Wishlistid');
        console.log(id)
        let button = $(this);

        button.prop('disabled', true);

        $.ajax({
            url: 'Wishlist/Delete',
            type: 'POST',
            data: {
                id: id
            },
            success: function (response) {
                console.log(response);
                button.prop('disabled', false);
            },
            error: function (error) {
                console.error(error);
                button.prop('disabled', false);
            }
        });
    })
});
