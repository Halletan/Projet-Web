$(window).load(function () {


    var body = $("body"),
        universe = $("#universe"),
        solarsys = $("#solar-system");

    var init = function () {
        body.removeClass('view-2D opening').addClass("view-3D").delay(2000).queue(function () {
            $(this).removeClass('hide-UI').addClass("set-speed");
            $(this).dequeue();
        });
    };

    var setView = function (view) { universe.removeClass().addClass(view); };

    $("#toggle-data").click(function (e) {
        body.toggleClass("data-open data-close");
        e.preventDefault();
    });

    $("#toggle-controls").click(function (e) {
        body.toggleClass("controls-open controls-close");
        e.preventDefault();
    });

    $("#data a").click(function (e) {

        var ref = $(this).attr("class");
        console.log(ref);

        // Consuming API

        var settings = {
            "async": true,
            "crossDomain": true,
            "url": "https://planets-info-by-newbapi.p.rapidapi.com/api/v1/planet/list",
            "method": "GET",
            "headers": {
                "X-RapidAPI-Host": "planets-info-by-newbapi.p.rapidapi.com",
                "X-RapidAPI-Key": "3398f4ab61msh2fec552303aed05p1babbajsn4969aa1f509b"
            }
        };

        // AJAX Call to Planets Info by NewbAPI (RapidAPI)

        $.ajax(settings).done(function (response) {
            console.log(response);   // Planets

            switch (ref) {
                case "mercury":
                    toastr.info(response._items[7].description
                        + "<br>" +
                        "Mass : " + response._items[7].basicDetails[0].mass
                        + "<br>" +
                        "Volume : " + response._items[7].basicDetails[0].volume,
                        "Planet Info",
                    {
                        positionClass:"toast-bottom-right"
                    });
                break;
                case "venus":
                    toastr.info(response._items[4].description
                        + "<br>" +
                        "Mass : " + response._items[4].basicDetails[0].mass
                        + "<br>" +
                        "Volume : " + response._items[4].basicDetails[0].volume,
                        "Planet Info",
                        {
                            positionClass: "toast-bottom-right"
                        });
                break;
                case "saturn":
                    toastr.info(response._items[0].description
                        + "<br>" +
                        "Mass : " + response._items[0].basicDetails[0].mass
                        + "<br>" +
                        "Volume : " + response._items[0].basicDetails[0].volume,
                        "Planet Info",
                        {
                            positionClass: "toast-bottom-right"
                        });
                break;
                case "jupiter":
                    toastr.info(response._items[1].description
                        + "<br>" +
                        "Mass : " + response._items[1].basicDetails[0].mass
                        + "<br>" +
                        "Volume : " + response._items[1].basicDetails[0].volume,
                        "Planet Info",
                        {
                            positionClass: "toast-bottom-right"
                        });
                break;
                case "uranus":
                    toastr.info(response._items[2].description
                        + "<br>" +
                        "Mass : " + response._items[2].basicDetails[0].mass
                        + "<br>" +
                        "Volume : " + response._items[2].basicDetails[0].volume,
                        "Planet Info",
                        {
                            positionClass: "toast-bottom-right"
                        });
                break;

                case "neptune":
                    toastr.info(response._items[3].description
                        + "<br>" +
                        "Mass : " + response._items[3].basicDetails[0].mass
                        + "<br>" +
                        "Volume : " + response._items[3].basicDetails[0].volume,
                        "Planet Info",
                        {
                            positionClass: "toast-bottom-right"
                        });
                break;
                case "earth":
                    toastr.info(response._items[5].description
                        + "<br>" +
                        "Mass : " + response._items[5].basicDetails[0].mass
                        + "<br>" +
                        "Volume : " + response._items[5].basicDetails[0].volume,
                        "Planet Info",
                        {
                            positionClass: "toast-bottom-right"
                        });
                break;
                case "mars":
                    toastr.info(response._items[6].description
                        + "<br>" +
                        "Mass : " + response._items[6].basicDetails[0].mass
                        + "<br>" +
                        "Volume : " + response._items[6].basicDetails[0].volume,
                        "Planet Info",
                        {
                            positionClass: "toast-bottom-right"
                        });
                break;
            }
        });

        solarsys.removeClass().addClass(ref);
        $(this).parent().find('a').removeClass('active');
        $(this).addClass('active');
        e.preventDefault();
    });

    $(".set-view").click(function () { body.toggleClass("view-3D view-2D"); });
    $(".set-zoom").click(function () { body.toggleClass("zoom-large zoom-close"); });
    $(".set-speed").click(function () { setView("scale-stretched set-speed"); });
    $(".set-size").click(function () { setView("scale-s set-size"); });
    $(".set-distance").click(function () { setView("scale-d set-distance"); });

    init();

});