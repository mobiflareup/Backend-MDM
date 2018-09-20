

var CURRENT_URL = window.location.href.split('#')[0],
    $BODY = $('body'),
    $MENU_TOGGLE = $('#menu_toggle'),
    $SIDEBAR_MENU = $('#sidebar-menu'),
    $SIDEBAR_FOOTER = $('.sidebar-footer'),
    $LEFT_COL = $('.left_col'),
    $RIGHT_COL = $('.right_col'),
    $NAV_MENU = $('.nav_menu'),
    $FOOTER = $('footer');

// Sidebar
$(document).on("click", function (event) {
    var $trigger = $(".side-menu li");
    if ($trigger !== event.target && !$trigger.has(event.target).length) {
        $(".child_menu").slideUp("fast");
        $(".side-menu li").removeClass("active");
    }
});


$(document).ready(function () {

    $(".child_menu li a").click(function () {
        $(this).siblings('ul').toggle().closest('.child_menu>li').siblings('li').find('ul').hide();
    });

    $(".child_menu li").click(function () {
       
        $(slideToggle).removeClass('active');
        $(this).addClass("active").siblings().removeClass("active");
    });


});
$(document).ready(function () {
    $('.mainbody').click(function () {
        if ($('.child_menu').is(':visible')) {
            $('.child_menu').slideUp();
        }
    });
    // TODO: This is some kind of easy fix, maybe we can improve this
    var setContentHeight = function () {
        // reset height
        $RIGHT_COL.css('min-height', $(window).height());

        var bodyHeight = $BODY.height(),
            leftColHeight = $LEFT_COL.eq(1).height() + $SIDEBAR_FOOTER.height(),
            contentHeight = bodyHeight < leftColHeight ? leftColHeight : bodyHeight;

        // normalize content
        contentHeight -= $NAV_MENU.height() + $FOOTER.height();

        $RIGHT_COL.css('min-height', contentHeight);
    };

    $SIDEBAR_MENU.find('a').on('click', function (ev) {
        var $li = $(this).parent();

        if ($li.is('.active')) {
            $li.removeClass('active');
            $('ul:first', $li).slideUp(function () {
                setContentHeight();
            });
        } else {
            // prevent closing menu if we are on child menu
            if (!$li.parent().is('.child_menu')) {
                $SIDEBAR_MENU.find('li').removeClass('active');
                $SIDEBAR_MENU.find('li ul').slideUp();
            }

            $li.addClass('active');

            $('ul:first', $li).slideDown(function () {
                setContentHeight();
            });
            //$(".child_menu li").click(function () {
            //    $(".child_menu").slideUp();
            //    //$SIDEBAR_MENU.find('li').removeClass('active');
            //});
        }
    });

    // toggle small or large menu
    $MENU_TOGGLE.on('click', function () {
        if ($BODY.hasClass('nav-md')) {
            $BODY.removeClass('nav-md').addClass('nav-sm');

            if ($SIDEBAR_MENU.find('li').hasClass('active')) {
                $SIDEBAR_MENU.find('li.active').addClass('active-sm').removeClass('active');
            }
        } else {
            $BODY.removeClass('nav-sm').addClass('nav-md');

            if ($SIDEBAR_MENU.find('li').hasClass('active-sm')) {
                $SIDEBAR_MENU.find('li.active-sm').addClass('active').removeClass('active-sm');
            }
        }

        setContentHeight();
    });

    // check active menu
    $SIDEBAR_MENU.find('a[href="' + CURRENT_URL + '"]').parent('li').addClass('current-page');

    $SIDEBAR_MENU.find('a').filter(function () {
        return this.href == CURRENT_URL;
    }).parents('li').addClass('active');//.parents('ul').addClass('active');
    //    .parent('li').addClass('current-page').parents('ul').slideDown(function () {
    //    setContentHeight();        
    //}).parent().addClass('active');


    // recompute content when resizing
    $(window).smartresize(function () {
        setContentHeight();
    });

    // fixed sidebar
    if ($.fn.mCustomScrollbar) {
        $('.menu_fixed').mCustomScrollbar({
            autoHideScrollbar: true,
            theme: 'minimal',
            mouseWheel: { preventDefault: true }
        });
    }
});
// /Sidebar

// Panel toolbox
$(document).ready(function () {
    $('.collapse-link').on('click', function () {
        var $BOX_PANEL = $(this).closest('.x_panel'),
            $ICON = $(this).find('i'),
            $BOX_CONTENT = $BOX_PANEL.find('.x_content');

        // fix for some div with hardcoded fix class
        if ($BOX_PANEL.attr('style')) {
            $BOX_CONTENT.slideToggle(200, function () {
                $BOX_PANEL.removeAttr('style');
            });
        } else {
            $BOX_CONTENT.slideToggle(200);
            $BOX_PANEL.css('height', 'auto');
        }

        $ICON.toggleClass('fa-chevron-up fa-chevron-down');
    });

    $('.close-link').click(function () {
        var $BOX_PANEL = $(this).closest('.x_panel');

        $BOX_PANEL.remove();
    });
});
// /Panel toolbox

// Tooltip
$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip({
        container: 'body'
    });
});
// /Tooltip

// Progressbar
if ($(".progress .progress-bar")[0]) {
    $('.progress .progress-bar').progressbar(); // bootstrap 3
}
// /Progressbar

// Switchery
$(document).ready(function () {
    if ($(".js-switch")[0]) {
        var elems = Array.prototype.slice.call(document.querySelectorAll('.js-switch'));
        elems.forEach(function (html) {
            var switchery = new Switchery(html, {
                color: '#26B99A'
            });
        });
    }
});
// /Switchery

// iCheck
$(document).ready(function () {
    if ($("input.flat")[0]) {
        $(document).ready(function () {
            $('input.flat').iCheck({
                checkboxClass: 'icheckbox_flat-green',
                radioClass: 'iradio_flat-green'
            });
        });
    }
});
// /iCheck

// Table
$('table input').on('ifChecked', function () {
    checkState = '';
    $(this).parent().parent().parent().addClass('selected');
    countChecked();
});
$('table input').on('ifUnchecked', function () {
    checkState = '';
    $(this).parent().parent().parent().removeClass('selected');
    countChecked();
});

var checkState = '';

$('.bulk_action input').on('ifChecked', function () {
    checkState = '';
    $(this).parent().parent().parent().addClass('selected');
    countChecked();
});
$('.bulk_action input').on('ifUnchecked', function () {
    checkState = '';
    $(this).parent().parent().parent().removeClass('selected');
    countChecked();
});
$('.bulk_action input#check-all').on('ifChecked', function () {
    checkState = 'all';
    countChecked();
});
$('.bulk_action input#check-all').on('ifUnchecked', function () {
    checkState = 'none';
    countChecked();
});

function countChecked() {
    if (checkState === 'all') {
        $(".bulk_action input[name='table_records']").iCheck('check');
    }
    if (checkState === 'none') {
        $(".bulk_action input[name='table_records']").iCheck('uncheck');
    }

    var checkCount = $(".bulk_action input[name='table_records']:checked").length;

    if (checkCount) {
        $('.column-title').hide();
        $('.bulk-actions').show();
        $('.action-cnt').html(checkCount + ' Records Selected');
    } else {
        $('.column-title').show();
        $('.bulk-actions').hide();
    }
}

// Accordion
$(document).ready(function () {
    $(".expand").on("click", function () {
        $(this).next().slideToggle(200);
        $expand = $(this).find(">:first-child");

        if ($expand.text() == "+") {
            $expand.text("-");
        } else {
            $expand.text("+");
        }
    });
});

// NProgress
if (typeof NProgress != 'undefined') {
    $(document).ready(function () {
        NProgress.start();
    });

    $(window).load(function () {
        NProgress.done();
    });
}

/**
 * Resize function without multiple trigger
 * 
 * Usage:
 * $(window).smartresize(function(){  
 *     // code here
 * });
 */
(function ($, sr) {
    // debouncing function from John Hann
    // http://unscriptable.com/index.php/2009/03/20/debouncing-javascript-methods/
    var debounce = function (func, threshold, execAsap) {
        var timeout;

        return function debounced() {
            var obj = this, args = arguments;
            function delayed() {
                if (!execAsap)
                    func.apply(obj, args);
                timeout = null;
            }

            if (timeout)
                clearTimeout(timeout);
            else if (execAsap)
                func.apply(obj, args);

            timeout = setTimeout(delayed, threshold || 100);
        };
    };

    // smartresize 
    jQuery.fn[sr] = function (fn) { return fn ? this.bind('resize', debounce(fn)) : this.trigger(sr); };

})(jQuery, 'smartresize');

// Edit Table

var target = '#data';
var customFields = {
    data: {
        1: {
            RequestNo: '',
            EmployeeName: '',
            PickupLocation: '',
            DropoffLocation: '',
            TripType: '',
            RequestType: '',
            PaymentStatus: '',
            CancelDate: ''



        },
        2: {

            RequestNo: '',
            EmployeeName: '',
            PickupLocation: '',
            DropoffLocation: '',
            TripType: '',
            RequestType: '',
            PaymentStatus: '',
            CancelDate: ''

            //PickupLocation: 'CLIENTID',
            //DropoffLocation: 'Client ID',
            //TripType: 'OEM',
            //RequestType: ''
        },

    },
    xref: { // Key ['Label','column width',0|1] 1=admin only
        RequestNo: ['Request No'],
        EmployeeName: ['EmployeeName'],
        PickupLocation: ['Pickup Location'],
        DropoffLocation: ['Dropoff Location'],
        TripType: ['Trip Type'],
        RequestType: ['Request Type'],
        PaymentStatus: ['Payment Status'],
        CancelDate: ['Cancel Date']

    },
    TripTypes: [
		[' ', ' '],
		['1', '1'],
		['2', '2']

    ],
    RequestTypes: [
          [' ', ' '],
		['1', '1'],
		['2', '2']
    ],
    mode: {
        row1: 'view',
        row2: 'view',

    },
    multiedit: false,
    admin: true
}

function getCustomFields(id) {
    if (id) { // return requested array id
        var data = customFields.data[id];
    } else { // return all
        var data = customFields.data;
    }

    return data;
}
function replaceSpecialChar(string) {
    if (string) {
        var newString = string.replace(/ /g, '_'); // convert spaces to underscores
        newString = newString.replace(/^_+|_+$/g, ''); // remove leading & trailing underscores
        newString = newString.trim();
        newString = newString.replace(/[^a-zA-Z0-9_+ ]/ig, ''); // remove special characters
        newString = newString.replace(/_+/g, '_'); // converts multiple underscores to single
        return newString.toUpperCase();
    }
}
function getTableHeaders() {
    var headers = customFields.xref;
    return headers;
}
function buildTripTypeSelect(selected, id) {
    var t = customFields.TripTypes;
    var options = '';
    var limit = t.length;

    if (id > 2) { // limit OEM to Custom 1 & 2 only
        limit = limit - 1;
    }
    for (i = 0; i < limit; i++) {
        if (t[i][0] == selected) {
            options += '<option value="' + t[i][0] + '" selected>' + t[i][1] + '</option>'
        } else {
            options += '<option value="' + t[i][0] + '">' + t[i][1] + '</option>'
        }
    }
    return options;
}

function buildRequestTypeSelect(selected, id) {
    var t = customFields.RequestTypes;
    var options = '';
    var limit = t.length;

    if (id > 2) { // limit OEM to Custom 1 & 2 only
        limit = limit - 1;
    }
    for (i = 0; i < limit; i++) {
        if (t[i][0] == selected) {
            options += '<option value="' + t[i][0] + '" selected>' + t[i][1] + '</option>'
        } else {
            options += '<option value="' + t[i][0] + '">' + t[i][1] + '</option>'
        }
    }
    return options;
}

function buildTable(target) {
    clearTable(target);
    var data = getCustomFields(); // returns array
    var header = getTableHeaders();
    var hdata = '';
    var rdata = '';
    hdata += '<thead>\n<tr>';
    for (key in header) {
        if (!customFields.admin && header[key][2] == 1) {
            delete header[key];
        } else {
            hdata += '<th width="' + header[key][1] + '">' + header[key][0] + '</th>'
        }
    }
    hdata += '</tr>\n</thead>'
    $(target).append(hdata);
    rdata += '<tbody>';
    for (key in data) {
        rdata += ('<tr data-id="' + key + '">');


        var count = 0;

        for (subkey in data[key]) {

            switch (subkey) {

                case 'TripType':
                    var TripTypeName = '';
                    var TripTypeVal = '';
                    $.each(customFields.TripTypes, function (i, l) {
                        if (l[0] == data[key][subkey]) {
                            TripTypeName = customFields.TripTypes[i][1];
                            TripTypeVal = customFields.TripTypes[i][0];
                        }
                    })
                    rdata += '<td><span class="field" data-field="' + subkey + '">' + TripTypeName + '</span><select class="form-control input-sm" data-field="' + subkey + '" style="display:none;">';
                    var options = buildTripTypeSelect(data[key][subkey], key);
                    rdata += options + '</select></td>';
                    break;
                case 'RequestType':
                    var RequestTypeName = '';
                    var RequestTypeVal = '';
                    $.each(customFields.RequestTypes, function (i, l) {
                        if (l[0] == data[key][subkey]) {
                            RequestTypeName = customFields.RequestTypes[i][1];
                            RequestTypeVal = customFields.RequestTypes[i][0];
                        }
                    })
                    rdata += '<td><span class="field" data-field="' + subkey + '">' + RequestTypeName + '</span><select class="form-control input-sm" data-field="' + subkey + '" style="display:none;">';
                    var options = buildRequestTypeSelect(data[key][subkey], key);
                    rdata += options + '</select></td>';
                    break;
                default:
                    rdata += '<td><span class="field" data-field="' + subkey + '">' + data[key][subkey] + '</span><input type="text" class="form-control input-sm" data-field="' + subkey + '" value="' + data[key][subkey] + '" style="display:none;" /></td>';
            }
            count++;
        }
        if (customFields.admin) {
            rdata += '<td class="text-center"><button class="btn btn-xs btn-info" data-edit="' + key + '"><span class="fa fa-pencil"></span></button><button class="btn btn-xs btn-success" style="display:none;" data-save="' + key + '"><span class="fa fa-floppy-o"></span></button><button class="btn btn-xs btn-danger" style="display:none;" data-cancel="' + key + '"><span class="fa fa-ban"></span></button></td>';
        }

        rdata += '</tr>';
    }
    rdata += ('</tbody>');
    $(target).append(rdata);

    initializeButtons();
}
function clearTable(target) {
    $(target).empty();
}
function resetForm(target) {
    var currentID = $(target + ' tfoot tr').data('id');
    var newID = parseInt(currentID) + 1;
    $(target + ' tfoot tr').attr('data-id', newID);
}

function updateFields(id, type) {
    // Read Currently stored
    var RequestNo = customFields.data[id].RequestNo;
    var EmployeeName = customFields.data[id].EmployeeName;
    var PickupLocation = customFields.data[id].PickupLocation;
    var DropoffLocation = customFields.data[id].DropoffLocation;
    var TripType = customFields.data[id].TripType;
    var RequestType = customFields.data[id].RequestType;
    var PaymentStatus = customFields.data[id].PaymentStatus;
    var CancelDate = customFields.data[id].CancelDate;
    if (type == 'save') {
        // Update from fields
        var RequestNo = $(target + ' tr[data-id="' + id + '"] input[data-field="RequestNo"]').val();
        var EmployeeName = $(target + ' tr[data-id="' + id + '"] input[data-field="EmployeeName"]').val();

        var PickupLocation = $(target + ' tr[data-id="' + id + '"] input[data-field="PickupLocation"]').val();
        var DropoffLocation = $(target + ' tr[data-id="' + id + '"] input[data-field="DropoffLocation"]').val();
        var TripType = $(target + ' tr[data-id="' + id + '"] select[data-field="TripType"] option:selected').val();
        var RequestType = $(target + ' tr[data-id="' + id + '"] select[data-field="RequestType"] option:selected').val();
        var PaymentStatus = $(target + ' tr[data-id="' + id + '"] input[data-field="PaymentStatus"]').val();
        var CancelDate = $(target + ' tr[data-id="' + id + '"] input[data-field="CancelDate"]').val();
        var enabled = $(target + ' tr[data-id="' + id + '"] input[data-field="enabled"]').is(':checked');
    }
    if (type == 'cancel') {
        // Update from fields
        $(target + ' tr[data-id="' + id + '"] input[data-field="RequestNo"]').val(RequestNo);
        $(target + ' tr[data-id="' + id + '"] input[data-field="EmployeeName"]').val(EmployeeName);

        $(target + ' tr[data-id="' + id + '"] input[data-field="PickupLocation"]').val(PickupLocation);
        $(target + ' tr[data-id="' + id + '"] input[data-field="DropoffLocation"]').val(DropoffLocation);
        $(target + ' tr[data-id="' + id + '"] select[data-field="TripType"]').val(TripType);
        $(target + ' tr[data-id="' + id + '"] input[data-field="RequestType"]').val(RequestType);

        $(target + ' tr[data-id="' + id + '"] input[data-field="PaymentStatus"]').val(PaymentStatus);
        $(target + ' tr[data-id="' + id + '"] input[data-field="CancelDate"]').val(CancelDate);

    }
    // Update stored values
    var nData = {}

    nData.RequestNo = RequestNo;
    nData.EmployeeName = EmployeeName;
    nData.PickupLocation = replaceSpecialChar(PickupLocation);
    nData.DropoffLocation = DropoffLocation;
    nData.TripType = TripType;
    nData.RequestType = RequestType;
    nData.PaymentStatus = PaymentStatus;
    nData.CancelDate = CancelDate;
    customFields.data[id] = nData;
    console.log(customFields.data[id]);
    $(target + ' tr[data-id="' + id + '"] .field[data-field="RequestNo"]').html(RequestNo);
    $(target + ' tr[data-id="' + id + '"] .field[data-field="EmployeeName"]').html(EmployeeName);
    $(target + ' tr[data-id="' + id + '"] .field[data-field="PickupLocation"]').html(PickupLocation);
    $(target + ' tr[data-id="' + id + '"] .field[data-field="DropoffLocation"]').html(DropoffLocation);
    $(target + ' tr[data-id="' + id + '"] .field[data-field="TripType"]').html(TripType);
    $(target + ' tr[data-id="' + id + '"] .field[data-field="RequestType"]').html(RequestType);
    $(target + ' tr[data-id="' + id + '"] .field[data-field="PaymentStatus"]').html(PaymentStatus);
    $(target + ' tr[data-id="' + id + '"] .field[data-field="CancelDate"]').html(CancelDate);
};
function toggleRow(row) {
    console.log("row=" + row);
    $(target + ' tr[data-id="' + row + '"]').toggleClass('success');
    $(target + ' tr[data-id="' + row + '"] td button[data-edit]').toggle();
    $(target + ' tr[data-id="' + row + '"] td button[data-save]').toggle();
    $(target + ' tr[data-id="' + row + '"] td button[data-cancel]').toggle();
    $(target + ' tr[data-id="' + row + '"] td').children('input:not([type="checkbox"])').toggle();
    $(target + ' tr[data-id="' + row + '"] td').children('select').toggle();
    $(target + ' tr[data-id="' + row + '"] td').children('span.field').toggle();
}
function changeMode(row, element, type) {
    switch (type) {
        case 'edit': // 

            toggleRow(row);
            $(target + ' tr[data-id="' + row + '"] td input[data-field="enabled"]').prop({ disabled: false });
            break;
        case 'save':
            updateFields(row, type);
            toggleRow(row);
            $(target + ' tr[data-id="' + row + '"] td input[data-field="enabled"]').prop({ disabled: true });
            break;
        case 'cancel':
            updateFields(row, type);
            toggleRow(row);
            $(target + ' tr[data-id="' + row + '"] td input[data-field="enabled"]').prop({ disabled: true });
        default:
            break;
    }
}
function initializeButtons() {

    $('button[data-edit]').on('click', function (e) {
        e.preventDefault();
        var row = $(this).data('edit');
        customFields.mode['row' + row] = 'edit';
        changeMode(row, $(this), 'edit');
    })
    $('button[data-cancel]').on('click', function (e) {
        e.preventDefault();
        var row = $(this).data('cancel');
        customFields.mode['row' + row] = 'cancel';
        changeMode(row, $(this), 'cancel');
    })
    $('button[data-save]').on('click', function (e) {
        e.preventDefault();
        var row = $(this).data('save');
        customFields.mode['row' + row] = 'save';
        changeMode(row, $(this), 'save');
    })
}

$(document).ready(function () {
    buildTable(target);

});

$(document).ready(function () {

    $(".custom-add-profile").click(function () {
        $(".cusrom-profile-form").slideToggle("3000");
    });

});

$(document).ready(function () {
    $(".btn-select").each(function (e) {
        var value = $(this).find("ul li.selected").html();
        if (value != undefined) {
            $(this).find(".btn-select-input").val(value);
            $(this).find(".btn-select-value").html(value);
        }
    });
});

$(document).on('click', '.btn-select', function (e) {
    e.preventDefault();
    var ul = $(this).find("ul");
    if ($(this).hasClass("active")) {
        if (ul.find("li").is(e.target)) {
            var target = $(e.target);
            target.addClass("selected").siblings().removeClass("selected");
            var value = target.html();
            $(this).find(".btn-select-input").val(value);
            $(this).find(".btn-select-value").html(value);
        }
        ul.hide();
        $(this).removeClass("active");
    }
    else {
        $('.btn-select').not(this).each(function () {
            $(this).removeClass("active").find("ul").hide();
        });
        ul.slideDown(300);
        $(this).addClass("active");
    }
});

$(document).on('click', function (e) {
    var target = $(e.target).closest(".btn-select");
    if (!target.length) {
        $(".btn-select").removeClass("active").find("ul").hide();
    }
});

