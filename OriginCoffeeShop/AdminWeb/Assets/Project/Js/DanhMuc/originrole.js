$(document).ready(function () {
    loadData('/RoleOrigin/List');
});
//Add Data Function   
function AddRecord() {
    var UrlAdd = '/RoleOrigin/Add';
    var empObj = {
        RoleName: $('#RoleName').val(),
        ActiveStatus: $('#ActiveStatus').is(":checked") ? true : false
    };
    Add('/RoleOrigin/List', UrlAdd, empObj);
}

//Function for getting the Data Based upon Employee ID  
function getbyID(ID) {
    $.ajax({
        url: "/RoleOrigin/getbyID/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#RoleName').val(result.RoleName);
            $('#Id').val(result.Id);
            if (result.ActiveStatus)
            {
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
    Delete('/RoleOrigin/List', '/RoleOrigin/Delete/', ID);
}
function UpdateRecord() {

    var empObj = {
        Id: $('#Id').val(),
        RoleName: $('#RoleName').val(),
        ActiveStatus: $('#ActiveStatus').is(":checked") ? true : false
    };
    
    Update('/RoleOrigin/List', '/RoleOrigin/Update', empObj);

}
