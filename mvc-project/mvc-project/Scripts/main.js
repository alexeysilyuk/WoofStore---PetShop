$(function () {
    var phone, email, lname, fname, balance, pass, username, photo_url;


    $(".del_btn").click(function () {
        var idUser = $(this).attr("id");
        var r = confirm("Are you really want to delete user: " + idUser);
        if (r == true) {
            $.post("/Admin/DeleteUser", { username: idUser });
            $(this).parent().parent().fadeOut();

        } else {
            console.log("Cancel!");
        }
    });

    $(".edit_btn").click(function () {
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


    $(".cls").click(function () {
        $(".shadow, .userModal").fadeOut();
    });


    $(".save_changes").click(function () {
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

        $("#res_query").text("Successfuly changed");

        fname.text($("#fname").val());
        lname.text($("#lname").val());
        phone.text($("#phone").val());
        balance.text($("#balance").val());
        email.text($("#email").val());
        pass.text($("#password").val());



        setTimeout(function () { $(".shadow, .userModal").fadeOut(); }, 2000);
    });


});