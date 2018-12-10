$(document).ready(function () {
    loadData('/PhanQuyen/List', '#phanquyentable');
    $("#ModulId").select2();
});
//Add Data Function  

//Function for getting the Data Based upon Employee ID  
function getbyID() {
    $('#myModal').modal('show');
    $('#btnUpdate').show();
    $('#btnAdd').hide();
}
function UpdateRecord() {
    var cbo = document.getElementById("Id");
    var empObj = {
        Id: cbo.options[cbo.selectedIndex].value,
        ModulId: $('#ModulId').val().toString(),
    };
    
    Update('/PhanQuyen/List', '/PhanQuyen/Update', empObj);

}
