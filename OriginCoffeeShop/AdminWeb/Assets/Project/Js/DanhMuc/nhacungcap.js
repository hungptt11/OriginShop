$(document).ready(function () {
    loadData('/NhaCungCap/List');
});
//Add Data Function   
function AddRecord() {
    var UrlAdd = '/NhaCungCap/Add';
    var empObj = {
        TenNhaCungCap: $('#TenNhaCungCap').val(),
        SDT: $('#SDT').val(),
        DiaChi: $('#DiaChi').val(),
        ActiveStatus: $('#ActiveStatus').is(":checked") ? true : false,
        KyHieu: $('#KyHieu').val()
    };
    Add('/NhaCungCap/List', UrlAdd, empObj);
}

//Function for getting the Data Based upon Employee ID  
function getbyID(ID) {
    $.ajax({
        url: "/NhaCungCap/getbyID/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#TenNhaCungCap').val(result.TenNhaCungCap);
            $('#Id').val(result.Id);
            $('#SDT').val(result.SDT);
            $('#DiaChi').val(result.DiaChi);
            $('#KyHieu').val(result.KyHieu);
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
    Delete('/NhaCungCap/List', '/NhaCungCap/Delete/', ID);
}
function UpdateRecord() {
    var empObj = {
        Id: $('#Id').val(),
        TenNhaCungCap: $('#TenNhaCungCap').val(),
        SDT: $('#SDT').val(),
        DiaChi: $('#DiaChi').val(),
        ActiveStatus: $('#ActiveStatus').is(":checked") ? true : false,
        KyHieu: $('#KyHieu').val()
    };
    
    Update('/NhaCungCap/List', '/NhaCungCap/Update', empObj);

}
