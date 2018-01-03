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



        // dynamic added
    $("#items").on("click", "button.item_edit_btn", function () {
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

    $("#shopItemDynamic").on("click", "button.item_del_btn", function () {
        var idItem = $(this).attr("id");
        var r = confirm("Are you really want to delete item: " + idItem);
        if (r == true) {
            $.post("/Admin/DeleteItem", { id: idItem });
            $(this).parent().parent().fadeOut();

        } else {
            console.log("Cancel!");
        }
    });

    $(".item_save_changes").click(function () {
        $.post("/Admin/EditItem",
            {
                iId: $("#iId").val(),
                iName: $("#iName").val(),
                iDescription: $("#iDescription").val(),
                iPrice: $("#iPrice").val(),
                iPhoto_url: $("#iPhoto_url").val()
                // call back
            }, function (d)
            {
                if ( !($.isEmptyObject(d))) {
                    console.log(d);
                    var tbl = $("#shopItemDynamic");
                    var tr = "<tr><th>" + d["Id"] + "</th><th>" + d["Name"] + "</th><th>" + d["Description"] + "</th><th>" + d["price"] + "</th><th><a href='" + d["photo_url"] + "' target='_blank'><img src='" + d["photo_url"] + "' height='50' width='50' /></a></th><th><button class='btn btn-primary item_edit_btn'>Edit</button><button id='" + d["Id"] + "' class='btn btn-danger item_del_btn'>X</button></th></tr>";

                    tbl.prepend(tr);
                }
            }
            );

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


    // ORDERS ASYNC BLOCK
    var oStatus, oId;


    $(".order_del_btn").click(function () {
        var idItem = $(this).attr("title");
        var r = confirm("Are you really want to delete item: " + idItem);
        if (r == true) {
            $.post("/Admin/DeleteOrder", { id: idItem });
            $(this).parent().parent().fadeOut();

        } else {
            console.log("Cancel!");
        }
    });


    $(".order_deliver_btn").click(function () {
        oStatus = $(this).parent().prev();
        oId = $(this);
        var idItem = $(this).attr("title");
        
        $.post("/Admin/setStatusOrder", { id: idItem, status: "Delivered" },
            function (data) {
                console.log(data);
                oStatus.text(data["status"]);
                oId.attr("title", data["orderID"]);
                oId.next().attr("title", data["orderID"]);
                oId.prev().attr("title", data["orderID"]);
                oId.prev().prev().attr("title", data["orderID"]);
            }
        );
        
    });

    $(".order_cancel_btn").click(function () {
        oStatus = $(this).parent().prev();
        oId = $(this);
        var idItem = $(this).attr("title");

        $.post("/Admin/setStatusOrder", { id: idItem, status: "Cancel" }, 
                        function (data) {
                            console.log(data);
                            oStatus.text(data["status"]);
                            oId.attr("title", data["orderID"]);
                            oId.next().attr("title", data["orderID"]);
                            oId.prev().attr("title", data["orderID"]);
                            oId.next().next().attr("title", data["orderID"]);
                        }
            );
    });

    $(".order_sent_btn").click(function () {
        oStatus = $(this).parent().prev();
        oId = $(this);
        var idItem = $(this).attr("title");

        $.post("/Admin/setStatusOrder", { id: idItem, status: "Sended" }, 
                        function (data) {
                            console.log(data);
                            oStatus.text(data["status"]);
                            oId.attr("title", data["orderID"]);
                            oId.next().attr("title", data["orderID"]);
                            oId.next().next().attr("title", data["orderID"]);
                            oId.next().next().next().attr("title", data["orderID"]);
                        }
            );
    });



    // Bye items create orders

    $(".cart_add").click(function () {
        var itemPrice, itemId, itemTitle, itemPhoto;
        itemId = $(this).attr("id");
        itemPrice = $(this).prev().text();
        itemTitle = $(this).attr("title");
        itemPhoto = $(this).parent().prev().attr("src");
       

        $.post("/Shop/makeOrder", { itemId: itemId, itemPrice: itemPrice, itemTitle: itemTitle, itemPhoto: itemPhoto },
            function (data) {
                alert("Added to your list of orders\n");
                $("#ubalance").text(data+" $");
            }
            );

        return false;
    });



    $(".buy").click(function () {
        var itemPrice, itemId, itemTitle, itemPhoto;
        itemId = $(this).attr("id");
        itemTitle = $(this).attr("title");
        itemPrice = $(this).prev().prev().prev().text();
        itemPhoto = $(this).prev().text();


        $.post("/Shop/makeOrder", { itemId: itemId, itemPrice: itemPrice, itemTitle: itemTitle, itemPhoto: itemPhoto },
            function (data) {
                alert("Added to your list of orders\n");
                $("#ubalance").text(data + " $");
            }
            );

        return false;
    });


    // update profile
    function callBackProfile(res) {
        if (res === "OK") {

            $(".success_msg").text("Your request succesfully operated");
            $(".alert-success").fadeIn();

            setTimeout(function () {
                $(".alert-success").hide().find(".success_msg").text("");
            }, 5000);
        }

        else {

            $(".error_msg").text("The are some errors in your request");
            $(".alert-danger").fadeIn();

            setTimeout(function () {
                $(".alert-danger").hide().find(".error_msg").text("");
            }, 5000);

        }
    }


    $("#updateProfile").submit(function () {
        var data = $(this).serialize()
        console.log(data);

        $.post("/User/updateProfile", data, 
            function (res) {
                callBackProfile(res);
            });

        return false;
    });


    $("#updatePassword").submit(function () {
        var data = $(this).serialize()
        console.log(data);

        var password = $("#password")
        var confirm_password = $("#cpassword");

        if (password.val() === confirm_password.val()) {
            $.post("/User/updatePassword", data,
            function (res) {
                callBackProfile(res);
            });
        }
        else {
            callBackProfile("Error");
        }

        return false;
    });

    
    $("#updatePhoto").submit(function () {
        var img = $(this).find("#photo_profile_dyn");
        var data = $(this).serialize()
        console.log(data);

        $.post("/User/updatePhoto", data,
            function (res) {
                if (res === "ERROR") {
                    $(".error_msg").text("The are some errors while saving");
                    $(".alert-danger").fadeIn();

                    setTimeout(function () {
                        $(".alert-danger").hide().find(".error_msg").text("");
                    }, 5000);
                }
                else {
                    img.fadeOut().delay(1000).attr("src", res).fadeIn();
                    $("body").find("#photo_top_avatar").fadeOut().delay(1000).attr("src", res).fadeIn();
                }
            });

        return false;
    });


    // contact us 
    $("#contactform").submit(function () {

        var data = $(this).serialize()
        console.log(data);

        $.post("/Home/contactUs", data,
            function (res) {
                callBackProfile(res);
            });

        return false;

        return false;
    });


    $(".clear_history").click(function () {

        var r = confirm("Are you really want to delete all messages ");
        if (r == true) {

            var r2 = confirm("Thing again maybe you don't want to do this ?");
            if (r2 == true) {

                var r3 = confirm("Still want delete all messages ?");
                if (r3 == true) {
                    $.post("/Admin/DeleteMessages", { order: "all" }, function (d) { $(".messages_table tbody").fadeOut(); } );
                }
            }
        }
         
    });

});