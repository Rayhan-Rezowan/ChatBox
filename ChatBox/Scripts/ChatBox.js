function userVerification() {
    var name = $("#name").val();
    if (name == '') {
        $("#errMsg").val("Required");
    }
    else {
        insertUser(name);
    }
};

function insertUser(name) {
    var model = {
        userName: name
    };

    $.ajax({
        type: "POST",
        url: "/ChatBox/InsertUser",
        data: JSON.stringify(model),
        contentType: "application/json; charset-utf8",
        dataType: "JSON",
        success: function (data) {
            if (data != null && data.userId > 0) {
                sessionStorage.setItem('userId', data.userId);
                sessionStorage.setItem('currentUser', name);
                getchatMessages();
            }
        }
    });
}

function insertMessege(uid, messege) {
    
    var msgData = {
        message: messege,
        userId: uid
    };
    $.ajax({
        type: "POST",
        url: "/ChatBox/InsertMessage",
        data: JSON.stringify(msgData),
        contentType: "application/json; charset-utf8",
        dataType: "JSON",
        success: function (data) {

            getchatMessages();
        }

    })
}

function sendMessage() {
    var messege = $("#messege").val();
    if (messege != '') {
        var uid=sessionStorage.getItem('userId')
        insertMessege(uid,messege);
        $("#messege").val("");
    }
}


function getchatMessages() {
    $.ajax({
        type: "GET",
        url: "/ChatBox/GetAllMessages",
        contentType: "application/json; charset-utf8",
        dataType: "JSON",
        success: function (data) {
            var chatMessage = "";
            $.each(data.ChatMessages, function (index, message) {
                chatMessage += "<li>";
                chatMessage += "<span class='name'>" + message.UserName + "</span>";
                if (message.UserName == sessionStorage.getItem('currentUser')) {
                    chatMessage += ": <span class='message' style='color:blue'>" + message.Message + "</span>";
                }
                else {
                    chatMessage += ": <span class='message'>" + message.Message + "</span>";
                }
                
                chatMessage += "</li>"
            });
            $("#chatMessages").empty();

            $("#chatMessages").append(chatMessage);
        }
    });
}

$(document).ready(function () {
    $("#name").click(function () {
        $("#errMsg").val("");
    });
    
    if (sessionStorage.getItem('currentUser')) {
        $("#name").val(sessionStorage.getItem('currentUser'));
        getchatMessages();
        $("#name").attr("readonly", "readonly");
    }
});