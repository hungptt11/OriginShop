$(document).ready(function () {
    loadData('/SanPham/List');
});
//Add Data Function   
function AddRecord() {
    var UrlAdd = '/SanPham/Add';
    var empObj = {
        TenSanPham: $('#TenSanPham').val(),
        ActiveStatus: $('#ActiveStatus').is(":checked") ? true : false
    };
    Add('/SanPham/List', UrlAdd, empObj);
}

//Function for getting the Data Based upon Employee ID  
function getbyID(ID) {
    $.ajax({
        url: "/SanPham/getbyID/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#TenSanPham').val(result.TenSanPham);
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
    Delete('/SanPham/List','/SanPham/Delete/', ID);
}
function UpdateRecord() {
    var empObj = {
        Id: $('#Id').val(),
        TenSanPham: $('#TenSanPham').val(),
        ActiveStatus: $('#ActiveStatus').is(":checked") ? true : false
    };
    
    Update('/SanPham/List','/SanPham/Update', empObj);

}