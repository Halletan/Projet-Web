$(document).ready(function () {
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '/global/cars',
        success: function (data) {
            $("#cars").append(data.length);
        },
        cache: false
    });

    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '/global/customers',
        success: function (data) {
            $("#customers").append(data.length);
        },
        cache: false
    });

    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '/global/bookings',
        success: function (data) {
            $("#bookings").append(data.length);
        },
        cache: false
    });

    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '/global/invoices',
        success: function (data) {
            $("#invoices").append(data.length);
        },
        cache: false
    });
});

