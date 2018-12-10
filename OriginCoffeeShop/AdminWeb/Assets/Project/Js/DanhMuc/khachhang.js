$(document).ready(function () {
    loadData('/KhachHang/List');
});
//Add Data Function   
function AddRecord() {
    var UrlAdd = '/KhachHang/Add';
    var empObj = {
        TenKhachHang: $('#TenKhachHang').val(),
        SDT: $('#SDT').val(),
        KyHieu: $('#KyHieu').val(),
        DiaChi: $('#DiaChi').val(),
        ActiveStatus: $('#ActiveStatus').is(":checked") ? true : false
    };
    Add('/KhachHang/List',UrlAdd, empObj);
}

//Function for getting the Data Based upon Employee ID  
function getbyID(ID) {
    $.ajax({
        url: "/KhachHang/getbyID/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#TenKhachHang').val(result.TenKhachHang);
            $('#Id').val(result.Id);
            $('#KyHieu').val(result.KyHieu),
            $('#SDT').val(result.SDT),
            $('#DiaChi').val(result.DiaChi);
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
    Delete('/KhachHang/List', '/KhachHang/Delete/', ID);
}
function UpdateRecord() {
    var empObj = {
        Id: $('#Id').val(),
        TenKhachHang: $('#TenKhachHang').val(),
        SDT: $('#SDT').val(),
        KyHieu: $('#KyHieu').val(),
        DiaChi: $('#DiaChi').val(),
        ActiveStatus: $('#ActiveStatus').is(":checked") ? true : false
    };
    
    Update('/KhachHang/List','/KhachHang/Update', empObj);

}
