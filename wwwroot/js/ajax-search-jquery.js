$(document).ready(function () {
    $('#productSearch').on('keyup', function () {
        let query = $(this).val();
        $('#loader').show();

        $.ajax({
            url: '/Products/Search',
            type: 'GET',
            data: { query: query },
            success: function (result) {
                $('#productResults').html(result);
            },
            error: function () {
                $('#productResults').html('<p>Error loading results.</p>');
            },
            complete: function () {
                $('#loader').hide();
            }
        });
    });
});
