

// ToastR Script

$(document).ready(function() {
    var message = $("#message").text();
    if (message.indexOf("Warning") >= 0)
        toastr.warning(message);
    else if (message.indexOf("Error") >= 0)
        toastr.error(message);
    else if (message.indexOf("Info") >= 0)
        toastr.info(message);
    else if (message !== "") {
        toastr.success(message);
    }
});