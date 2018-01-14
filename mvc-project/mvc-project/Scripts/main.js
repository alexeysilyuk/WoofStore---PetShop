// Javascript used in VIEWS

$(function () {

    $(".cls").click(function () {
        $(".shadow, .modal").fadeOut();
    });

    $(".osahanloading").hide();
    // USER ASYNC BLOCK
    var phone, email, lname, fname, balance, pass, username, photo_url;


    // action delete user from admin panel
    $(".user_del_btn").click(function () {
        var idUser = $(this).attr("id");
        var responce = confirm("Are you really want to delete user: " + idUser);
        if (responce == true) {
            $.post("/Admin/DeleteUser", { username: idUser });  // make post requeste to delete user
            $(this).parent().parent().fadeOut();

        } else {
            console.log("Cancel!");
        }
    });

    // edit user profile
    $(".user_edit_btn").click(function () {
        $(".shadow, .userModal").fadeIn();

        // get user fields data from form
         phone = $(this).parent().prev().prev();
         email = $(this).parent().prev().prev().prev();
         lname = $(this).parent().prev().prev().prev().prev();
         fname = $(this).parent().prev().prev().prev().prev().prev();
         balance = $(this).parent().prev().prev().prev().prev().prev().prev();
         pass = $(this).parent().prev().prev().prev().prev().prev().prev().prev();
         username = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev();
         photo_url = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev();

        // set values into form fields
        $("#phone").val(phone.text());
        $("#email").val(email.text());
        $("#lname").val(lname.text());
        $("#fname").val(fname.text());
        $("#balance").val(balance.text());
       
        $("#username").val(username.text());
        $("#photo_url").val(photo_url.text());

    });




    //change user info
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

        // print responce result - SUCCESS
        $("#user_res_query").text("Successfuly changed");

        fname.text($("#fname").val());
        lname.text($("#lname").val());
        phone.text($("#phone").val());
        balance.text($("#balance").val());
        email.text($("#email").val());
        pass.text($("#password").val());


        // Hide Modal window after 2 seconds
        setTimeout(function () {
            $(".shadow, .userModal").fadeOut();
            $("#user_res_query").text("");
        }, 2000);
    });




    //////////////////////////////////////////////////////////////////////////////////

    // ITEMS ASYNC BLOCK
    var iName, iId, iDescription, iPrice, iPhoto_url;



        // dynamic added, edit shop item 
    $("#items").on("click", "button.item_edit_btn", function () {
        $(".shadow, .itemModal").fadeIn();

        iPhoto_url = $(this).parent().prev().children();

        iPrice = $(this).parent().prev().prev();
        iDescription = $(this).parent().prev().prev().prev();
        iName = $(this).parent().prev().prev().prev().prev();
        iId = $(this).parent().prev().prev().prev().prev().prev();

        // get fields from item Modal window
        var photo_name = iPhoto_url.attr("href").split('/');
        $("#iPhoto_url").val(photo_name[3]);
        $("#iPrice").val(iPrice.text());
        $("#iDescription").val(iDescription.text());
        $("#iName").val(iName.text());
        $("#iId").val(iId.text());
    });

    // delete shop item
    $("#shopItemDynamic").on("click", "button.item_del_btn", function () {
        var idItem = $(this).attr("id");
        var responce = confirm("Do you really want to delete item: " + idItem);

        // if admin choosed to remove item
        if (responce == true) {
            $.post("/Admin/DeleteItem", { id: idItem });
            $(this).parent().parent().fadeOut();

        } else {
            console.log("Cancel!");
        }
    });


    // action on save item button click in admin
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
                // if there are items in shop, add them dinamicaly into table
                if ( !($.isEmptyObject(d))) {
                    var tbl = $("#shopItemDynamic");
                    var row = "<tr><th>" + d["Id"] + "</th><th>" + d["Name"] + "</th><th>" + d["Description"] + "</th><th>" + d["price"] + "</th><th><a href='" + d["photo_url"] + "' target='_blank'><img src='/Content/shop/" + d["photo_url"] + "' height='50' width='50' /></a></th><th><button class='btn btn-primary item_edit_btn'>Edit</button><button id='" + d["Id"] + "' class='btn btn-danger item_del_btn'>X</button></th></tr>";

                    tbl.prepend(row);
                }
            }
            );

        //show MOdal window to user with SUCCESS message
        $("#item_res_query").text("Successfuly changed");


        iPhoto_url.attr("href", $("#iPhoto_url").val());
        iPhoto_url.children().attr("src","/Content/shop/"+ $("#iPhoto_url").val());
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

    //action on delete order button in admin panel, removes items asynchroniously
    $(".order_del_btn").click(function () {
        var idItem = $(this).attr("title");
        var responce = confirm("Are you really want to delete item: " + idItem);

        if (responce == true) {
            $.post("/Admin/DeleteOrder", { id: idItem });
            $(this).parent().parent().fadeOut();

        } else {
            console.log("Cancel!");
        }
    });

    // set status for order to DELIVERED
    $(".order_deliver_btn").click(function () {
        $(".osahanloading").fadeIn();
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
        $(".osahanloading").fadeOut();  // spinner while loading
        
    });

    // action on cancel order button in admin menu
    $(".order_cancel_btn").click(function () {
        $(".osahanloading").fadeIn();
        oStatus = $(this).parent().prev();
        oId = $(this);
        var idItem = $(this).attr("title");

        $.post("/Admin/setStatusOrder", { id: idItem, status: "Cancel" }, 
                        function (data) {
                            oStatus.text(data["status"]);
                            oId.attr("title", data["orderID"]);
                            oId.next().attr("title", data["orderID"]);
                            oId.prev().attr("title", data["orderID"]);
                            oId.next().next().attr("title", data["orderID"]);
                        }
            );
        $(".osahanloading").fadeOut();
    });

    // action on markering order as SENT
    $(".order_sent_btn").click(function () {
        $(".osahanloading").fadeIn();
        oStatus = $(this).parent().prev();
        oId = $(this);
        var idItem = $(this).attr("title");

        $.post("/Admin/setStatusOrder", { id: idItem, status: "Sent" }, 
                        function (data) {
                            console.log(data);
                            oStatus.text(data["status"]);
                            oId.attr("title", data["orderID"]);
                            oId.next().attr("title", data["orderID"]);
                            oId.next().next().attr("title", data["orderID"]);
                            oId.next().next().next().attr("title", data["orderID"]);
                        }
            );
        $(".osahanloading").fadeOut();
    });



    // Buy items, create order
    // while user clicks on BUY in main SHOP page

    $("#dynamicLoad").on("click", ".cart_add", function () {
        var itemPrice, itemId, itemTitle, itemPhoto;
        itemId = $(this).attr("id");
        itemPrice = $(this).prev().text();
        itemTitle = $(this).attr("title");
        itemPhoto = $(this).parent().prev().children().attr("src");


        $.post("/Shop/makeOrder", { itemId: itemId, itemPrice: itemPrice, itemTitle: itemTitle, itemPhoto: itemPhoto },
            function (data) {
                // if makeOrder action returned some data, show success popup message 
                if (data!== 0) {
                    alert("Added to your list of orders\n");
                    // and update balance after ordering
                    $("#ubalance").text(data + " $");
                }
                else {
                    callBackProfile("Error");
                    alert("You must login to make order\n");
                }
            }
            );

        return false;
    });


    // action while clicked BUY in item page
    $(".buy").click(function () {
        var itemPrice, itemId, itemTitle, itemPhoto;
        itemId = $(this).attr("id");
        itemTitle = $(this).attr("title");
        itemPrice = $(this).prev().prev().prev().text();
        itemPhoto = $(this).prev().text();


        $.post("/Shop/makeOrder", { itemId: itemId, itemPrice: itemPrice, itemTitle: itemTitle, itemPhoto: itemPhoto },
            function (data) {
                // if makeOrder action returned some data, show success popup message 
                if (data !== 0) {
                    alert("Added to your list of orders\n");
                    $("#ubalance").text(data + " $");
                }
                else {
                    callBackProfile("Error");
                    alert("You must login to make order\n");
                }
            }
            );

        return false;
    });


    // update profile, popup MODAL window for printing status for user after some operation
    function callBackProfile(res) {
        // if received 'OK' message, print 
        if (res === "OK") {

            $(".success_msg").text("Your request succesfully operated");
            $(".alert-success").fadeIn();

            // hide window after 5000 msec
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

    // update profile action, user area
    $("#updateProfile").submit(function () {
        $(".osahanloading").fadeIn();       // spinner on
        var data = $(this).serialize();

        $.post("/User/updateProfile", data, 
            function (res) {
                callBackProfile(res);
            });
        $(".osahanloading").fadeOut();      // spinner off
        return false;
    });

    // update user password in user area page
    $("#updatePassword").submit(function () {
        $(".osahanloading").fadeIn();
        var data = $(this).serialize()

        var password = $("#password")
        var confirm_password = $("#cpassword");

        // check if password been 
        if (password.val() === confirm_password.val()) {
            $.post("/User/updatePassword", data,
            function (res) {
                callBackProfile(res);
            });
        }
        else {
            callBackProfile("Error");
        }
        $(".osahanloading").fadeOut();
        return false;
    });

    // action on updating user image
    $("#updatePhoto").submit(function () {
        $(".osahanloading").fadeIn();
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
                    // show avatar preview
                    img.fadeOut().delay(1000).attr("src", res).fadeIn();
                    $("body").find("#photo_top_avatar").fadeOut().delay(1000).attr("src", res).fadeIn();
                }
            });
        $(".osahanloading").fadeOut();
        return false;
    });


    // contact us 
    $("#contactform").submit(function () {

        var data = $(this).serialize();
        $(this).each(function () { this.reset(); });
        console.log(data);

        $.post("/Home/contactUs", data,
            function (res) {
                callBackProfile(res);
            });

        return false; 
    });

    // delete all messages from users
    $(".clear_history").click(function () {

        var responce = confirm("Are you really want to delete all messages ");
        if (responce == true) {

            var responce2 = confirm("Thing again maybe you don't want to do this ?");
            if (responce2 == true) {

                var responce3 = confirm("Still want delete all messages ?");
                if (responce3 == true) {
                    $.post("/Admin/DeleteMessages", { order: "all" }, function (d) { $(".messages_table tbody").fadeOut(); } );
                }
            }
        }
         
    });


    // dynamic search in shop page 
    var dynSearhcStr;
    $("#dynSearch").keyup(function () {
        dynSearhcStr = $(this).val();
        // downcase all letters from search line
        dynSearhcStr = dynSearhcStr.toLocaleLowerCase();
        if (dynSearhcStr.length != 0) {
            //if there are letters in search line
            $("#dynamicLoad h4").each(function () {            
                if (!($(this).text().trim().toLocaleLowerCase().match("^" + dynSearhcStr))) {
                    $(this).parent().fadeOut();
                    //hide unmached results
                }
                else {
                    //show matched results
                    $(this).parent().fadeIn();
                }
            });
        }
        // if search line cleaned, show all items
        else {
            $("#dynamicLoad h4").each(function () {
                    $(this).parent().fadeIn();
            });
        }
    });

});