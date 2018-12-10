$(document).ready(function () {
    loadData();
    //$("#ModulId").select2();
});
function UpdateRecord() {
    var cbo = document.getElementById("Id");
    var empObj = {
        Id: cbo.options[cbo.selectedIndex].value,
        ModulId: $('#ModulId').val().toString(),
    };
    
    Update('/PhanQuyen/Update', empObj);

}
function loadData()
{
    $.ajax({
        url: '/PhanQuyen/PrepareDataForFunctionAccessPermision',
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            GenHtml(result);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}