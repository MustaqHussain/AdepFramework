/// <reference path="../jquery-1.5.1-vsdoc.js" />

(function ($) {
    /*
    Client-side support for checking which button was clicked.
    This functionality has a dependency on the mvcvtkrequiredif
    validation functionality.
    */
    $(document).ready(function () {
        $('input[data-button-source-fieldid]').click(function () {
            var fieldid = $(this).data('button-source-fieldid');
            $('#' + fieldid).val($(this).val());
        });
    });
})(jQuery);
