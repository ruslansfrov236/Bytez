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
               
                location.reload();
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
               
                location.reload();
                
            },
            error: function (error) {
                console.error(error);
                button.prop('disabled', false);
            }
        });
    });

    $(".btn-heart-active").click(function () {
        let id = $(this).data('wishlistid');
        console.log(id)



      

        $.ajax({
            url: 'Wishlist/Delete',
            type: 'Delete',
            data: {
                id: id
            },
            success: function (response) {
               
                location.reload();
            },
            error: function (error) {
                console.error(error);
                button.prop('disabled', false);
            }
        });
    })

    $('.btn-dark-shop').click(function () {
        const id = $(this).data('id');
        const quantity = $(this).data('quantity');

        console.log(`id:${id}` +`quantity:${quantity}`)
        $.ajax({
            url: "Basket/AddBasket",
            type: 'POST',
            data: {
                id: id,
                quantity:quantity
            },
            success: function (response) {
               
                location.reload();
            },
            error: function (error) {
                console.error(error);
                
            }
        })

    })
});
$(document).ready(function () {
  
    
    var stock = +$('#quantity').data('stock');
    var quantity = +$('#quantity').val();
    $('.btn-minus').click(function () {
     

       
        if (quantity >0) {
            +quantity--;
        }
         if (quantity == 0) {
            quantity = 1;
        }
        $('#quantity').val(quantity);
      
    });

    $('.btn-pilus').click(function () {
      
        if (+quantity <stock) {
            +quantity++;
        }
     
        $('#quantity').val(quantity);
    });

    $.ajax({
        url: "Basket/AddBasket",
        type: 'POST',
        data: {
            id: id,
            quantity: quantity
        },
        success: function (response) {
           
            location.reload();

        },
        error: function (error) {
            console.error(error);

        }
    })
});
$(document).ready(function () {
    var slider = $("#slider");
    var thumb = $("#thumb");
    var slidesPerPage = 4; //globaly define number of elements per page
    var syncedSecondary = true;
    slider.owlCarousel({
        items: 1,
        slideSpeed: 2000,
        nav: false,
        autoplay: false,
        dots: false,
        loop: true,
        responsiveRefreshRate: 200
    }).on('changed.owl.carousel', syncPosition);
    thumb
        .on('initialized.owl.carousel', function () {
            thumb.find(".owl-item").eq(0).addClass("current");
        })
        .owlCarousel({
            items: slidesPerPage,
            dots: false,
            nav: true,
            item: 4,
            smartSpeed: 200,
            slideSpeed: 500,
            slideBy: slidesPerPage,
            navText: ['<svg width="18px" height="18px" viewBox="0 0 11 20"><path style="fill:none;stroke-width: 1px;stroke: #000;" d="M9.554,1.001l-8.607,8.607l8.607,8.606"/></svg>', '<svg width="25px" height="25px" viewBox="0 0 11 20" version="1.1"><path style="fill:none;stroke-width: 1px;stroke: #000;" d="M1.054,18.214l8.606,-8.606l-8.606,-8.607"/></svg>'],
            responsiveRefreshRate: 100
        }).on('changed.owl.carousel', syncPosition2);
    function syncPosition(el) {
        var count = el.item.count - 1;
        var current = Math.round(el.item.index - (el.item.count / 2) - .5);
        if (current < 0) {
            current = count;
        }
        if (current > count) {
            current = 0;
        }
        thumb
            .find(".owl-item")
            .removeClass("current")
            .eq(current)
            .addClass("current");
        var onscreen = thumb.find('.owl-item.active').length - 1;
        var start = thumb.find('.owl-item.active').first().index();
        var end = thumb.find('.owl-item.active').last().index();
        if (current > end) {
            thumb.data('owl.carousel').to(current, 100, true);
        }
        if (current < start) {
            thumb.data('owl.carousel').to(current - onscreen, 100, true);
        }
    }
    function syncPosition2(el) {
        if (syncedSecondary) {
            var number = el.item.index;
            slider.data('owl.carousel').to(number, 100, true);
        }
    }
    thumb.on("click", ".owl-item", function (e) {
        e.preventDefault();
        var number = $(this).index();
        slider.data('owl.carousel').to(number, 300, true);
    });



});





