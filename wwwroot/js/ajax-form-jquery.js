$(document).ready(function () {
    $('#orderForm').on('submit', function (e) {
        e.preventDefault();
        $('#orderMessage').html('');
        $('#orderLoader').show();

        let form = $(this);
        let formData = form.serialize();

        $.ajax({
            url: '/Orders/Create',
            type: 'POST',
            data: formData,
            success: function (response) {
                if (response.success) {
                    $('#orderMessage').html(
                        `<div class="alert alert-success">${response.message}</div>`
                    );
                    form.trigger("reset");
                } else {
                    $('#orderMessage').html(
                        `<div class="alert alert-danger">${response.message}</div>`
                    );
                }
            },
            error: function () {
                $('#orderMessage').html(
                    '<div class="alert alert-danger">An unexpected error occurred.</div>'
                );
            },
            complete: function () {
                $('#orderLoader').hide();
            }
        });
    });
});
