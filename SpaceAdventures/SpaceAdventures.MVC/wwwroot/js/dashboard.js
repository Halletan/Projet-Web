$(document).ready(function() {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Global/GetAircraftsCount",
        success: function(data) {
            $("#aircraft").append(data);
        },
        cache: false
    });

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Clients/GetClientsCount",
        success: function(data) {
            console.log(data);
            $("#customers").append(data);
        },
        cache: false
    });

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Global/GetBookingsCount",
        success: function(data) {
            $("#bookings").append(data);
        },
        cache: false
    });

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Global/GetUsersCount",
        success: function(data) {
            $("#users").append(data);
        },
        cache: false
    });
});