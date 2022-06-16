$(window).load(function() {


    var body = $("body"),
        universe = $("#universe"),
        solarsys = $("#solar-system");

    var init = function() {
        body.removeClass("view-2D opening").addClass("view-3D").delay(2000).queue(function() {
            $(this).removeClass("hide-UI").addClass("set-speed");
            $(this).dequeue();
        });
    };

    var setView = function(view) { universe.removeClass().addClass(view); };

    $("#toggle-data").click(function(e) {
        body.toggleClass("data-open data-close");
        e.preventDefault();
    });

    $("#toggle-controls").click(function(e) {
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

        var sunSettings = {
            "async": true,
            "crossDomain": true,
            "url": "https://sunrise-sunset-times.p.rapidapi.com/getSunriseAndSunset?date=2022-05-25&latitude=50.8476&longitude=4.3572&timeZoneId=Europe%2FBrussels",
            "method": "GET",
            "headers": {
                "X-RapidAPI-Host": "sunrise-sunset-times.p.rapidapi.com",
                "X-RapidAPI-Key": "3398f4ab61msh2fec552303aed05p1babbajsn4969aa1f509b"
            }
        };

        // AJAX Call to Planets Info by NewbAPI (RapidAPI)

        $.ajax(settings).done(function (response) {
            console.log(response);   // Planets

            switch (ref) {
                case "saturn":

                    console.log(response[0].basicDetails.length);

                    toastr.info(response[0].description
                        + "<br>" +
                        "Mass : " + response[0].imgSrc.img
                        + "<br>" + 
                        "Volume : " + response[0].basicDetails.volume,
                        response[0].name,
                        {
                        positionClass:"toast-bottom-right"
                        });
                break;
                case "jupiter":
                    toastr.info(response[1].description
                        + "<br>" +
                        "Mass : " + response[1].basicDetails.mass
                        + "<br>" +
                        "Volume : " + response[1].basicDetails.volume,
                        response[1].name,
                        {
                            positionClass: "toast-bottom-right"
                        });
                    break;
                case "uranus":
                    toastr.info(response[2].description
                        + "<br>" +
                        "Mass : " + response[2].basicDetails.mass
                        + "<br>" +
                        "Volume : " + response[2].basicDetails.volume,
                        response[2].name,
                        {
                            positionClass: "toast-bottom-right"
                        });
                    break;
                case "neptune":
                    toastr.info(response[3].description
                        + "<br>" +
                        "Mass : " + response[3].basicDetails.mass
                        + "<br>" +
                        "Volume : " + response[3].basicDetails.volume,
                        response[3].name,
                        {
                            positionClass: "toast-bottom-right"
                        });
                    break;
                case "venus":
                    toastr.info(response[4].description
                        + "<br>" +
                        "Mass : " + response[4].basicDetails.mass
                        + "<br>" +
                        "Volume : " + response[4].basicDetails.volume,
                        response[4].name,
                        {
                            positionClass: "toast-bottom-right"
                        });
                    break;
                case "earth":
                    toastr.info(response[5].description
                        + "<br>" +
                        "Mass : " + response[5].basicDetails.mass
                        + "<br>" +
                        "Volume : " + response[5].basicDetails.volume,
                        response[5].name,
                        {
                            positionClass: "toast-bottom-right"
                        });
                    break;
                case "mars":
                    toastr.info(response[6].description
                        + "<br>" +
                        "Mass : " + response[6].basicDetails.mass
                        + "<br>" +
                        "Volume : " + response[6].basicDetails.volume,
                        response[6].name,
                        {
                            positionClass: "toast-bottom-right"
                        });
                    break;
                case "mercury":
                    toastr.info(response[7].description
                        + "<br>" +
                        "Mass : " + response[7].basicDetails.mass
                        + "<br>" +
                        "Volume : " + response[7].basicDetails.volume,
                        response[7].name,
                        {
                            positionClass: "toast-bottom-right"
                        });
                    break;
                case "sun": $.ajax(sunSettings).done(function (response) {
                    toastr.success("Sunrise : " + response.sunrise + "<br>" + "Sunset : " + response.sunset, "Europe - Brussels :",
                        {
                            positionClass: "toast-bottom-left"
                        });
                });
                break;
            }
        });

        solarsys.removeClass().addClass(ref);
        $(this).parent().find("a").removeClass("active");
        $(this).addClass("active");
        e.preventDefault();
    });

    $(".set-view").click(function() { body.toggleClass("view-3D view-2D"); });
    $(".set-zoom").click(function() { body.toggleClass("zoom-large zoom-close"); });
    $(".set-speed").click(function() { setView("scale-stretched set-speed"); });
    $(".set-size").click(function() { setView("scale-s set-size"); });
    $(".set-distance").click(function() { setView("scale-d set-distance"); });

    init();

});