$(document).ready(function() {
    $(document).on('submit', '#register-form', function() {
        var data = $(this).serialize();
        $.ajax({
            type: 'POST',
            url: './php/register.php',
            data: data,
            success: function(response) {
                if (response.includes('Registered')) {

                    swal({
                        html: true,
                        title: "<span style= \"color:#fce600\" >Succesfully registered!</span>",
                        text: "<span style= \"color:#ff9933\" >Now you are going to be redirected to your personal page!</span>",
                        confirmButtonColor: "#333399",
                        type: "success",
                        confirmButtonText: "Okay"
                    }, function(isConfirm) {
                        if (isConfirm) {
                            window.location = "../PERSONAL-PAGE";
                        }
                    });

                } else {
                    swal({
                        html: true,
                        title: "<span style= \"color:#fce600\" >Unsuccessfull registration</span>",
                        text: "<span style= \"color:#ff9933\" >Please insert correct data!</span>",
                        type: "error",
                        confirmButtonColor: "#333399",
                        confirmButtonText: "Try again"
                    });
                }
            }
        });
        return false;
    });
});
