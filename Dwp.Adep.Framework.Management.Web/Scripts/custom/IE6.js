/* v2.3 */
$(document).ready(function () {
    addMenuBarStyles();
    doIE6Rendering();
});

function doIE6Rendering() {
    var id = "";
    if (arguments.length !== 0) {
        id = arguments[0];
        id = id + " ";
    }

    var rows = $(id + '.row');

    var len = rows.length;
    for (i = 0; i < len; i++) {
        var widths = $(rows[i]).find('[class*="w1"], [class*="w2"], [class*="w3"], [class*="w4"], [class*="w5"], [class*="w6"], [class*="w7"], [class*="w8"], [class*="w9"]');
        $(widths[0]).addClass("gridFirst");

        var widthLength = widths.length;
        for (j = 0; j < widthLength; j++) {
            $(widths[j]).addClass("gridItem");
        }
    }
    rows.find(id + '[type="checkbox"]').addClass('IE6-checkbox');
    $(id + 'input [disabled]').addClass('disabled-field');
}

function addMenuBarStyles() {
    $('.navbar .nav > li > a').addClass('menubar');
    $('.navbar .nav > li').addClass('menubarli');
}