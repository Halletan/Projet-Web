$(document).ready(function () {
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '',
        success: function (data) {
            $("#cars").append(data.length);
        },
        cache: false
    });

    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: "/Clients/GetClientsCount",
        success: function (data) {
            console.log(data);
            $("#customers").append(data);
        },
        cache: false
    });

    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '',
        success: function (data) {
            $("#bookings").append(data.length);
        },
        cache: false
    });

    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '',
        success: function (data) {
            $("#invoices").append(data.length);
        },
        cache: false
    });
});

