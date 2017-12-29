$(function () {


    $(".edit_btn").click(function () {
        $(".shadow, .userModal").fadeIn();

        var phone = $(this).parent().prev().prev().text();
        var email = $(this).parent().prev().prev().prev().text();
        var lname = $(this).parent().prev().prev().prev().prev().text();
        var fname = $(this).parent().prev().prev().prev().prev().prev().text();
        var balance = $(this).parent().prev().prev().prev().prev().prev().prev().text();
        var pass = $(this).parent().prev().prev().prev().prev().prev().prev().prev().text();
        var username = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().text();

        var photo_url = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().text();


        $("#phone").val(phone);
        $("#email").val(email);
        $("#lname").val(lname);
        $("#fname").val(fname);
        $("#balance").val(balance);
        $("#password").val(pass);
        $("#username").val(username);
        $("#photo_url").val(photo_url);

    });


    $(".cls").click(function () {
        $(".shadow, .userModal").fadeOut();
    });


    $(".save_changes").click(function () {
        $.post("Admin/EditUser",
            {
                fname: $("#fname").val(),
                lname: $("#lname").val(),
                balance: $("#money").val(),
                photo: "",
                isAdmin: false,
                email: $("#email").val(),
                username: $("#username").val(),
                password: $("#password").val()
                
            }, function (data) {
                console.log("send");
            console.log(data);
        });
    });


});