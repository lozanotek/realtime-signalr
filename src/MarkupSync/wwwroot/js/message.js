
$(function(){
    $(".message").click(function(e){
        var button = e.target;
        sendMessage(button);
    });

    $("#clear").click(function(){
        $("#gm").val("");
    });
});

function sendMessage(action) {
    var key = action.dataset.key;
    var content = $("#" + key).html();

    if(key === "gm"){
        content = $("#" + key).val();
    }

    $.ajax({
        contentType: "application/json",
        type: "POST",
        url: "/sync/message",
        data: JSON.stringify({
            key: key,
            content: content
        })
    });
};
