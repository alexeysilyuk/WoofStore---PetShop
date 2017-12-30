$(function () {

    $(".cls").click(function () {
        $(".shadow, .modal").fadeOut();
    });


    // USER ASYNC BLOCK
    var phone, email, lname, fname, balance, pass, username, photo_url;


    $(".user_del_btn").click(function () {
        var idUser = $(this).attr("id");
        var r = confirm("Are you really want to delete user: " + idUser);
        if (r == true) {
            $.post("/Admin/DeleteUser", { username: idUser });
            $(this).parent().parent().fadeOut();

        } else {
            console.log("Cancel!");
        }
    });

    $(".user_edit_btn").click(function () {
        $(".shadow, .userModal").fadeIn();

         phone = $(this).parent().prev().prev();
         email = $(this).parent().prev().prev().prev();
         lname = $(this).parent().prev().prev().prev().prev();
         fname = $(this).parent().prev().prev().prev().prev().prev();
         balance = $(this).parent().prev().prev().prev().prev().prev().prev();
         pass = $(this).parent().prev().prev().prev().prev().prev().prev().prev();
         username = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev();
         photo_url = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev();


        $("#phone").val(phone.text());
        $("#email").val(email.text());
        $("#lname").val(lname.text());
        $("#fname").val(fname.text());
        $("#balance").val(balance.text());
        $("#password").val(pass.text());
        $("#username").val(username.text());
        $("#photo_url").val(photo_url.text());

    });





    $(".user_save_changes").click(function () {
        $.post("/Admin/EditUser",
            {
                username: $("#username").val(),
                fname: $("#fname").val(),
                lname: $("#lname").val(),
                phone: $("#phone").val(),
                email: $("#email").val(),
                balance: $("#balance").val(),
                password: $("#password").val()
               
            });

        $("#user_res_query").text("Successfuly changed");

        fname.text($("#fname").val());
        lname.text($("#lname").val());
        phone.text($("#phone").val());
        balance.text($("#balance").val());
        email.text($("#email").val());
        pass.text($("#password").val());



        setTimeout(function () {
            $(".shadow, .userModal").fadeOut();
            $("#user_res_query").text("");
        }, 2000);
    });




    //////////////////////////////////////////////////////////////////////////////////

    // ITEMS ASYNC BLOCK
    var iName, iId, iDescription, iPrice, iPhoto_url;


    $(".item_del_btn").click(function () {
        var idItem = $(this).attr("id");
        var r = confirm("Are you really want to delete item: " + idItem);
        if (r == true) {
            $.post("/Admin/DeleteItem", { id: idItem });
            $(this).parent().parent().fadeOut();

        } else {
            console.log("Cancel!");
        }
    });


    $(".item_edit_btn").click(function () {
        $(".shadow, .itemModal").fadeIn();

        iPhoto_url = $(this).parent().prev().children();

        iPrice = $(this).parent().prev().prev();
        iDescription = $(this).parent().prev().prev().prev();
        iName = $(this).parent().prev().prev().prev().prev();
        iId = $(this).parent().prev().prev().prev().prev().prev();
       


        $("#iPhoto_url").val(iPhoto_url.attr("href"));
        $("#iPrice").val(iPrice.text());
        $("#iDescription").val(iDescription.text());
        $("#iName").val(iName.text());
        $("#iId").val(iId.text());

       

    });


    $(".item_save_changes").click(function () {
        $.post("/Admin/EditItem",
            {
                iId: $("#iId").val(),
                iName: $("#iName").val(),
                iDescription: $("#iDescription").val(),
                iPrice: $("#iPrice").val(),
                iPhoto_url: $("#iPhoto_url").val()
            });

        $("#item_res_query").text("Successfuly changed");


        iPhoto_url.attr("href", $("#iPhoto_url").val());
        iPhoto_url.children().attr("src", $("#iPhoto_url").val());
        iName.text($("#iName").val());
        iDescription.text($("#iDescription").val());
        iPrice.text($("#iPrice").val());

       
        setTimeout(function () {
            $(".shadow, .itemModal").fadeOut();
            $("#item_res_query").text("");
        }, 2000);
    });




});