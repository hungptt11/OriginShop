$(document).ready(function () {
    loadData('/NguyenLieu/List');
});
//Add Data Function   
function AddRecord() {
    var UrlAdd = '/NguyenLieu/Add';
    var cbo = document.getElementById("loainguyenlieu");
    var empObj = {
        TenNguyenLieu: $('#TenNguyenLieu').val(),
        ActiveStatus: $('#ActiveStatus').is(":checked") ? true : false,
        LoaiNguyenLieu : cbo.options[cbo.selectedIndex].value
    };
    Add('/NguyenLieu/List', UrlAdd, empObj);
}

//Function for getting the Data Based upon Employee ID  
function getbyID(ID) {
    $.ajax({
        url: "/NguyenLieu/getbyID/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#TenNguyenLieu').val(result.TenNguyenLieu);
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
    Delete('/NguyenLieu/List','/NguyenLieu/Delete/', ID);
}
function UpdateRecord() {
    var empObj = {
        Id: $('#Id').val(),
        TenNguyenLieu: $('#TenNguyenLieu').val(),
        ActiveStatus: $('#ActiveStatus').is(":checked") ? true : false
    };
    
    Update('/NguyenLieu/List','/NguyenLieu/Update', empObj);

}