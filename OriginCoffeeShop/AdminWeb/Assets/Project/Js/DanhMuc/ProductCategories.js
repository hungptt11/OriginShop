$(document).ready(function () {
    loadData('/ProductCategories/List');
});
//Add Data Function   
function AddRecord() {
    var UrlAdd = '/ProductCategories/Add';
    var empObj = {
        TenThanhPham: $('#TenThanhPham').val(),
        ActiveStatus: $('#ActiveStatus').is(":checked") ? true : false
    };
    Add('/ProductCategories/List', UrlAdd, empObj);
}

//Function for getting the Data Based upon Employee ID  
function getbyID(ID) {
    $.ajax({
        url: "/ProductCategories/getbyID/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#TenThanhPham').val(result.TenThanhPham);
            $('#Id').val(result.Id);
            if (result.ActiveStatus) {
                $('#ActiveStatus').prop('checked', true);
            }
            else {
                $('#ActiveStatus').prop('checked', false);
            }
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}
function DeleteRecord(ID) {
    Delete('/ProductCategories/List', '/ProductCategories/Delete/', ID);
}
function UpdateRecord() {
    var empObj = {
        Id: $('#Id').val(),
        TenThanhPham: $('#TenThanhPham').val(),
        ActiveStatus: $('#ActiveStatus').is(":checked") ? true : false
    };
    
    Update('/ProductCategories/List', '/ProductCategories/Update', empObj);

}