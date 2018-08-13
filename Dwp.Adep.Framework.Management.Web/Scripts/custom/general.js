/* v2.3 */
$(document).ready(function () {
    $.ajaxSetup({
        cache: false
    });
    initInputs();
    jQueryUIInit();
    restyleDropDown($('select:not(.notdropdown):not([name^="OperationalReportViewer"])'));
    redrawGrid();
    accesibilitiseForms();
});

$(window).load(function () {
    setWidthEqualToDiv('.webgrid-title', '.webgrid');
})

function accesibilitiseForms() {
    id = "";
    if (arguments.length !== 0) {
        id = arguments[0];
        id = id + " ";
    }
    $(id + ' .field-validation-valid').attr('role', "alert");
    $(id + ' .field-validation-error').attr('role', "alert");
    $(id + ' .date').each(function () {
        var $this = $(this);
        var tooltip = $this.prop("title");
        tooltip += " Format DD/MM/YYYY";
        $this.prop("title", tooltip);
    });

    $(id + '.ui-selectmenu').each(function () {
        var title = $(this).parent().siblings('select').eq(0).attr('title');
        $(this).attr('title', title);
    });
}


function initInputs() {
    var id = "";
    if (arguments.length !== 0) {
        id = arguments[0];
        id = id + " ";
    }

    $(id + 'input[data-val-required][type="text"]').each(function (index, e) {
        addRequired(e);
    });
    $(id + 'input[data-val-mvcvtkrequiredif][type="text"]').each(function (index, e) {
        addRequired(e);
    });
    $(id + 'input[data-val-required][type="radio"]').each(function (index, e) {
        addRequired(e);
    });
    $(id + 'input[data-val-required][type="hidden"]').each(function (index, e) {
        addRequired(e);
    });
    $(id + 'select[data-val-required]').each(function (index, e) {
        addRequired(e);
    });
    $(id + 'textarea[data-val-required]').each(function (index, e) {
        addRequired(e);
    });
    $(id + 'select.input-validation-error').each(function () {
        restyleErrorDropDowns($(this)[0])
    });
}

function addRequired(e) {
    var elementid = e.id;
    var label = $('label[for =' + elementid + ']');
    label.append('<span class="required-input">*</span>');
};

function restyleDropDown(e) {
    e.selectmenu({
        style: 'dropdown'
    });
    e.change(function (e1) {
        id = e1.delegateTarget.id;
        var form = $('form');
        if (!form.validate().element(e1.delegateTarget)) {
            $('#' + id + '-button').addClass("ui-state-error")
        }
        else {
            $('#' + id + '-button').removeClass("ui-state-error")
        }
    });
};

function restyleErrorDropDowns(e) {
    var id = e.id;
    $('#' + id + '-button').addClass("ui-state-error");
};

function disableFormElement(elementName) {
    $(elementName).attr('disabled', 'disabled');
}

function enableFormElement(elementName) {
    $(elementName).removeAttr('disabled');
}
var winWidth = $(window).width(),
    winHeight = $(window).height();
$(window).resize(function () {
    onResize = function () {
        windowResizeActions();
    };
    var winNewWidth = $(window).width(),
        winNewHeight = $(window).height();
    var resizeTimeout = null;
    if (winWidth != winNewWidth || winHeight != winNewHeight) {
        window.clearTimeout(resizeTimeout);
        resizeTimeout = window.setTimeout(onResize, 500);
    }
    winWidth = winNewWidth;
    winHeight = winNewHeight;
})

function windowResizeActions() {
    setWidthEqualToDiv('.webgrid-title', '.webgrid');
    redrawSelectMenus();
    redrawGrid();
}

function redrawGrid() {
    if ($.browser.msie && $.browser.version == "6.0") {
        try {
            var mainSection = $('#main');
            var pageMarginLeft = ($(window).width() - $('.page').outerWidth()) / 2;
            $('.page').css("margin-left", pageMarginLeft);
            var pageGrid = mainSection.find('.gridItem');
            var marginLeft = $(pageGrid[1]).css("margin-left");
            pageGrid.css("margin-left", marginLeft);
        }
        catch (e) { }
    }
}

function setWidthEqualToDiv(divToAdjust, divWithWidth) {
    try {
        var width = $(divWithWidth).width();
        var div = $(divToAdjust);
        width -= parseInt(div.css('padding-left'), 10);
        width -= parseInt(div.css('padding-right'), 10);
        width -= 1;
        $(divToAdjust).css('width', width);
    } catch (err) { }
}


var isDirty = false;

function createDirtyDialogs() {
    // If the isExitConfirmed element presenet then add dirty dialogs for inputs.
    // you can put a class of ignore-dirty-check on a form and this will be ignored. 
    if (document.getElementById("IsExitConfirmed") != undefined) {
        var $form = $('#IsExitConfirmed').parents('form');
        var $notDirtyCheck = $form.not(".ignore-dirty-check");
        if ($notDirtyCheck.length > 0) {
            $notDirtyCheck.find(':input').change(function () {
                if (!isDirty) {
                    isDirty = true;
                }
            });
        }

        var $myDialogExit = $('<div id=\'confirm\'></div>')
        .html('Are you sure you want to exit?<br/>Click OK to confirm.  Click Cancel to stop this action.')
        .dialog({
            autoOpen: false,
            modal: true,
            title: 'Confirmation Required',
            buttons: {
                "OK": function () {
                    window.location = $(this).dialog('option', 'href'); ;
                }
                , "Cancel": function () {
                    $(this).dialog("close");
                    return false;
                }
            }

        });

        $('a:not([href*="#"])').click(function (e) {
            if (isDirty == true) {
                e.preventDefault();
                var href = $(this).attr("href");
                var returnValue = $myDialogExit.dialog('option', 'href', $(this).attr('href')).dialog('open');
                return returnValue;
            }
            return true;
        });
    }
}


function clearValidationSummary() {
    var $form = $("#mainForm");

    $form.find("[data-valmsg-summary=true]")
            .removeClass("validation-summary-errors")
            .addClass("validation-summary-valid")
            .find("ul").empty();

    $form.find("[data-valmsg-replace]")
            .removeClass("field-validation-error")
            .addClass("field-validation-valid")
            .empty();
}

function jQueryUIInit() {
    createDatePickers();
    drawTabs();
    addMenuTab();
    jQueryUIStyling();
}

function jQueryUIStyling() {
    var id = "";
    if (arguments.length !== 0) {
        id = arguments[0];
        id = id + " ";
    }

    $(id + 'input:button:not([name^="OperationalReportViewer"]), input:submit:not(.IgnoreJQueryStyling), .ui-button, .btn').button();
    $(id + '.webgrid-wrapper').addClass('ui-corner-all');
    $(id + '.webgrid-title').addClass('ui-widget-header');
    jQueryTableStyling();
}

function drawTabs() {
    $("#tab-bar").tabs();
}

function createDatePickers() {
    var id = "";
    if (arguments.length !== 0) {
        id = arguments[0];
        id = id + " ";
    }
    $(id + '.date,' + id + '.hasDatePicker,' + id + '.standardDate').datepicker({
        dateFormat: "dd/mm/yy",
        onSelect: function () {
            $(this).valid();
            $(this).change();
        }
    });
    $(id + '.dateWithMYSelector, ' + id + '.hasDatePickerWithMYSelector, ' + id + '.customDate').datepicker({
        dateFormat: "dd/mm/yy",
        changeMonth: true,
        changeYear: true,
        yearRange: "-120:+10",
        onSelect: function () {
            $(this).valid();
            $(this).change();
        }
    });
}

function redrawSelectMenus() {
    var id = "";
    if (arguments.length !== 0) {
        id = arguments[0];
        id = id + " ";
    }
    $(id + 'select:not(.notdropdown):not([name^="OperationalReportViewer"])').selectmenu('destroy').selectmenu();
}

function addMenuTab() {
    $("#MenuItems").addClass('removestuff');
    $("#accordion").accordion({
        active: false,
        collapsible: true,
        autoHeight: false,
        header: "h3"
    });
    $("#accordionSub").accordion({
        collapsible: true,
        active: false,
        autoHeight: false,
        header: "h4"
    });
    createMenuBar();
}

function createMenuBar() {
    var menuDropdown = $('a[href="#openMenu"]');
    menuDropdown.click(function () {
        $('.slide-out-div').slideToggle();
    });

    menuDropdown.keydown(function (event) {
        var code = event.keyCode || event.which;
        if (code === 32 || code === 40 || code === 38) {
            event.preventDefault();
            $('.slide-out-div').slideToggle();
        }
    });

    $(document).click(function (e) {
        var menuContainer = $('#menu');
        if (!menuContainer.is(e.target) && menuContainer.has(e.target).length === 0) {
            $('.slide-out-div').slideUp();
        }
    })

    var dropdown = $('#menu-dropdown-li');
    var next = $(dropdown.next());
    next.focusin(function () {
        $('.slide-out-div').slideUp();
    })
}

function jQueryTableStyling() {
    var webgrid = $('.webgrid');
    webgrid.addClass('ui-widget-content styled-table');
}

function renderPartialView(id) {
    initInputs(id);
    jQueryUIStyling(id);
    createDatePickers(id);
    redrawSelectMenus(id);
    attachValidator(id);
    accesibilitiseForms(id);
}

function attachValidator(id) {
    $.validator.unobtrusive.parse(id);
    $(id).validate();
}

//  USAGE: disableDivs("#divToDisable", ".classToDisable", "#divToDisable");
function disableDivs() {
    var len = arguments.length;
    for (var i = 0; i < len; i++) {
        disableDiv(arguments[i]);
    }
}

//  USAGE: enableDivs("#divToEnable", ".classToEnable", "#any", ".amount", ".of", ".Elements");
function enableDivs() {
    var len = arguments.length;
    for (var i = 0; i < len; i++) {
        enableDiv(arguments[i]);
    }
}

function disableDiv(divIdToDisable) {
    $divToDisable = $(divIdToDisable);
    disableChildSelects($divToDisable);
    $($divToDisable.find("input")).attr('disabled', 'disable');
    disableChildButton($divToDisable);
}

function enableDiv(divIdToEnable) {
    $divToEnable = $(divIdToEnable);
    enableChildSelects($divToEnable);
    $divToDisable.find("input").removeAttr('disabled');
    enableChildButton($divToEnable);
}

function disableChildButton($jQueryButtonLocation) {
    $buttons = $($jQueryButtonLocation.find('input:button:not([name^="OperationalReportViewer"]), input:submit:not(.IgnoreJQueryStyling), .ui-button, .btn'))
    $buttons.attr('disabled', 'disable');
    $buttons.button('disable');
}

function enableChildButton($jQueryButtonLocation) {
    $buttons = $($jQueryButtonLocation.find('input:button:not([name^="OperationalReportViewer"]), input:submit:not(.IgnoreJQueryStyling), .ui-button, .btn'))
    $buttons.removeAttr('disabled');
    $buttons.button('enable');
}

function disableChildSelects($jQuerySelectLocation) {
    $selects = $($jQuerySelectLocation.find("select"));
    $selects.attr('disabled', 'disable');
    $selects.selectmenu('disable');
}

function enableChildSelects($jQuerySelectLocation) {
    $selects = $($jQuerySelectLocation.find("select"));
    $selects.removeAttr('disabled');
    $selects.selectmenu('enable');
}
