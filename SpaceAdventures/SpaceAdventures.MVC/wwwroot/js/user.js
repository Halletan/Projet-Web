

 /**
  * Checks if a certain user exists ?
  */

$(document).ready(function () {

    $("#email").on('change',
        function () {
            var email = $(this).val();

            $.ajax({
                type: "GET",
                crossDomain: true,
                url: "/Users/UserExists",
                data: { email: email },
                success: function (exists) {
                    if (exists) {
                        toastr.warning("A user with this email already exists", "Warning");
                        $("#email").val('');
                    }
                }
            });
        });
})