$(document).ready(function () {
    $("#selectList").change(function () {

        var value = $('#selectList option:selected').text();

        switch (value) {
        case "Admin":
            toastr.info(value +  " : Can R/W Messages & Read Users", "Access");
            break;
        case "Agent":
                toastr.info(value + " : Can only Read Messages", "Access");
            break;
        case "Owner":
                toastr.info(value + " : Can R/W Users & Messages", "Access");
            break;
        case "User":
                toastr.info(value + " : Can R/W Messages", "Access");
            break;
        }
    });
});