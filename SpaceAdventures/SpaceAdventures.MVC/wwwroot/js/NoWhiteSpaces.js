
    $(document).ready(function() {

        $(".nowhitespaces").on('change',
            function () {

                var value = $(this).val();
                if (value.search(" ") != -1) {
                    var replacedStr = value.replace(/\s/g, '');

                    $(this).val(replacedStr);
                    toastr.warning("Username cannot contain a white space", "Warning");
                }
            });
    })