$(document).ready(function () {
    SetDefaultFilter();
});

function SetDefaultFilter()
{
    document.getElementById("conditionfilterdenngay").valueAsDate = new Date();
    document.getElementById("conditionfiltertungay").valueAsDate = new Date((new Date()).valueOf() - 1000 * 3600 * 24 * 90);
    $("#conditionfilterselected").val("2").change();
}

$("#conditionfilterselected").change(function () {
    ResetFilter();
});
function ResetFilter()
{
    var cboconditionfilterselected = document.getElementById("conditionfilterselected");
    if (cboconditionfilterselected.options[cboconditionfilterselected.selectedIndex].value == 1) // Hiển Thị Tất Cả Danh Sách
    {
        $('#conditionfilterdenngay').attr('disabled', 'disabled');
        $('#conditionfiltertungay').attr('disabled', 'disabled');
    } else {
        $('#conditionfiltertungay').removeAttr('disabled');
        $('#conditionfilterdenngay').removeAttr('disabled');
    }
    $('#displaycheck').show('slow');
}
$("#conditionfiltertungay").change(function () {    
    ResetFilter();
});

$("#conditionfilterdenngay").change(function () {
    ResetFilter();
});

function ConditionFilter()
{
    loadHoaDon();
    $('#displaycheck').hide('slow');
    $('html, body').animate({
        scrollTop: $("#ListInvoice").offset().top
    }, 1000);


}