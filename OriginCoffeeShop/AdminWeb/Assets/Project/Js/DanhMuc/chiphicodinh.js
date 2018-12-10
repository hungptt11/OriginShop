$(document).ready(function () {
    loadData('/ChiPhiCoDinh/List');
});
//Add Data Function   
function AddRecord() {
    var UrlAdd = '/ChiPhiCoDinh/Add';
    var empObj = {
        FeeName: $('#FeeName').val(),
        ActiveStatus: $('#ActiveStatus').is(":checked") ? true : false,
        MoneyPerMonth: $('#FeeName').val()
    };
    Add('/ChiPhiCoDinh/List', UrlAdd, empObj);
}

//Function for getting the Data Based upon Employee ID  
function getbyID(ID) {
    $.ajax({
        url: "/ChiPhiCoDinh/getbyID/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#FeeName').val(result.FeeName);
            $('#Id').val(result.Id);
            if (result.ActiveStatus) {
                $('#ActiveStatus').prop('checked', true);
            }
            else {
                $('#ActiveStatus').prop('checked', false);
            }
            $('#MoneyPerMonth').val(result.MoneyPerMonth);
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
    Delete('/ChiPhiCoDinh/List', '/ChiPhiCoDinh/Delete/', ID);
}
function UpdateRecord() {
    var empObj = {
        Id: $('#Id').val(),
        FeeName: $('#FeeName').val(),
        MoneyPerMonth: $('#MoneyPerMonth').val(),
        ActiveStatus: $('#ActiveStatus').is(":checked") ? true : false
    };

    Update('/ChiPhiCoDinh/List', '/ChiPhiCoDinh/Update', empObj);

}