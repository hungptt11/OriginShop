$(document).ready(function () {
    loadData('/User/List', '#usertable');
    
});
//Add Data Function   
function AddRecord() {
    if (validation())
        return;
    var cbo = document.getElementById("RoleId");
    var UrlAdd = '/User/Add';
    var empObj = {
        Account: $('#Account').val(),
        Password: $('#Password').val(),
        RoleId:cbo.options[cbo.selectedIndex].value,
        ActiveStatus: $('#ActiveStatus').is(":checked") ? true : false
    };
    Add('/User/List', UrlAdd, empObj);
}

//Function for getting the Data Based upon Employee ID  
function getbyID(ID) {
    $.ajax({
        url: "/User/getbyID/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#Id').val(result.Id);
            $('#Account').val(result.Account);
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
    Delete('/User/List', '/User/Delete/', ID);
}
function UpdateRecord() {
    if (validation())
        return;
    var cbo = document.getElementById("RoleId");
    var empObj = {
        Id: $('#Id').val(),       
        Account: $('#Account').val(),
        Password: $('#Password').val(),
        RoleId:cbo.options[cbo.selectedIndex].value,
        ActiveStatus: $('#ActiveStatus').is(":checked") ? true : false
    };
    
    Update('/User/List', '/User/Update', empObj);

}

function validation()
{
    if ($('#Password').val() != $('#Password2').val())
    {
        alert("Password confirm không đúng");
        return true;
    }
    return false;
}
