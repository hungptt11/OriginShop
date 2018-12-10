function clearTextBox(id) {
    $(id + " input").each(function () {
        if ($(this).is('[type=text]')) {
            $(this).val("");
        }
    });
    $('#btnUpdate').hide();
    $('#btnAdd').show();
}

function clearFormInfor(object)
{
    var i = 0;
    $(object.inputform).find("input[type=text], textarea").val("");
    $.each(object.selectinput, function (index, value) {
        $(value).html('');
    });
}
function loadData(Url) {
    $.ajax({
        url: Url,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            GenHtml(result)
            $('.overlay').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}  
function Add(URList, UrlAdd, empObj) {
    $.ajax({
        url: UrlAdd,
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Return_Cd == false) {
                $('#myModal').modal('hide');
                ShowErrorMessCallback(result.ErrorMess, function () {
                    $('#myModal').modal('show');
                });
            } else {
                $('#myModal').modal('hide');
                loadData(URList);
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function Delete(URList, UrlDelete, ID) {
    var rt_cd = $.sweetModal.confirm('Are you sure you want to delete this Record?', function () {
        $.ajax({
            url: UrlDelete + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                if (result.Return_Cd == false) {
                    ShowErrorMess(result.ErrorMess);
                } else {
                    loadData(URList);
                }
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    });    
}
function Update(URList, UrlUpdate, empObj) {
    $.ajax({
        url: UrlUpdate,
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Return_Cd) {
                $('#myModal').modal('hide');
                loadData(URList);
                $('#btnUpdate').hide();
                $('#btnAdd').show();
            } else {                
                $('#myModal').modal('hide');
                ShowErrorMessCallback(result.ErrorMess, function () {
                    $('#myModal').modal('show');
                });
            }
        },
        error: function (errormessage) {          
            alert(errormessage.responseText);
        }
    });
}

function reloadJs(src) {
    src = $('script[src$="' + src + '"]').attr("src");
    $('script[src$="' + src + '"]').remove();
    $('<script/>').attr('src', src).appendTo('body');
}

function ShowErrorMessCallback(errorMess, callback)
{
    $.sweetModal.confirm('Error', errorMess, function () {
        callback();
    }, function () {
        callback();
    });
    
}
function ShowErrorMess(errorMess) {
    $.sweetModal({
        content: errorMess,
        icon: $.sweetModal.ICON_ERROR
    });
}

function ShowWarningMess(errorMess) {
    $.sweetModal({
        content: errorMess,
        icon: $.sweetModal.ICON_WARNING
    });
}

function SuccesMess(SuccessMess)
{
    $.sweetModal({
        content: SuccessMess,
        icon: $.sweetModal.ICON_SUCCESS
    });
}

function formatcurrency(itemvalue)
{
    return new Intl.NumberFormat().format(itemvalue);
}

function lostfocuscurrency(item) {
    $(item).val(formatcurrency($(item).val().replace(/\./g, '')));
    //$(item).val(parseFloat( $(item).val()));
}

function calculatemoney(scr1,scr2,des)
{
    var item1 = $(scr1).val();
    var item2 = $(scr2).val();
    if(item1 != '' && item2 != '')
    {
        item2 = item2.replace(/\./g, ''); // inside /content want to replace/ is the content want to replace, and flow bay g is match all the entire string.
        $(des).val(formatcurrency(item1 * item2));
    }
}

function ConvertJsonDateString(jsonDate) {
    var shortDate = null;
    if (jsonDate) {
        var regex = /-?\d+/;
        var matches = regex.exec(jsonDate);
        var dt = new Date(parseInt(matches[0]));
        var month = dt.getMonth() + 1;
        var monthString = month > 9 ? month : '0' + month;
        var day = dt.getDate();
        var dayString = day > 9 ? day : '0' + day;
        var year = dt.getFullYear();
        shortDate = dayString + '/' + monthString + '/' + year;
    }
    return shortDate;
};