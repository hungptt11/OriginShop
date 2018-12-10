$(document).ready(function () {
    loadHoaDon();
});
function loadHoaDon() {
    var cbo = document.getElementById("conditionfilterselected");
    var obj = {
        conditonfilter: cbo.options[cbo.selectedIndex].value,
        tungay: $('#conditionfiltertungay').val(),
        denngay: $('#conditionfilterdenngay').val()
    }
    $.ajax({
        url: '/QuanLyNhap/GetListHD',
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            GenHtml(result);
            $('.overlay').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
var tablechitiet = [];
function ThemChiTiet()
{
    var cbo = document.getElementById("nguyenlieuselected");
    if (tablechitiet.length != 0)
    {
        var i;
        for(i= 0 ; i < tablechitiet.length; i++)
        {
            if (cbo.options[cbo.selectedIndex].value == tablechitiet[i].manguyenlieu)
            {
                ShowErrorMessCallback("Đã tồn tại thông tin nguyên liệu tại chi tiết hóa đơn nhập", function () {
                    $('#myModal').modal('show');
                }, function () {
                    $('#myModal').modal('show');
                });
                return;
            }
        }
    }
    var object = {
        manguyenlieu: cbo.options[cbo.selectedIndex].value,
        dongia: $('#sotien').val(),
        sokg: $('#sokg').val(),
        thanhtien: $('#thanhtien').val()
    }
    tablechitiet[tablechitiet.length] = object;
    Reloadchitietdh();
}
function GetNguyenLieuName(MaNL) {
    if (ListNguyenLieus == null) {
        $.ajax({
            url: "/QuanLyNhap/ListNguyenLieu",
            type: "GET",
            contentType: "application/json;charset=UTF-8",
            async: false,
            dataType: "json",
            success: function (result) {
                ListNguyenLieus = result;
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
    var i;
    for (i = 0; i < ListNguyenLieus.length; i++) {
        if (ListNguyenLieus[i].Id == MaNL) {
            return ListNguyenLieus[i].TenNguyenLieu;
        }
    }
}
function DeleteDetailInvoce(id)
{
    if(tablechitiet != null)
    {
        tablechitiet.splice(id, 1);
        Reloadchitietdh();
    }    
}
function Reloadchitietdh()
{
    var i;
    var html = '';
    var totalInvoiceMoney = 0;
    for(i = 0; i < tablechitiet.length; i++)
    {
        html += '<tr><td>'+ (i+1) +'</td>';
        html += '<td>' + GetNguyenLieuName(tablechitiet[i].manguyenlieu) + '</td>';
        html += '<td>'+ tablechitiet[i].dongia +'</td>';
        html += '<td>'+ tablechitiet[i].sokg +'</td>';
        html += '<td>'+ tablechitiet[i].thanhtien +'</td>';
        html += '<td><a href="#" onclick="DeleteDetailInvoce(' + i + ')">Delete</a></td>';
        totalInvoiceMoney += parseInt(tablechitiet[i].thanhtien.replace(/\./g, ''));
    }
    $('#tongsotien').val(formatcurrency(totalInvoiceMoney));
    $('#nguyenlieutablebody').html(html);
}
function AddRecord()
{
    if (tablechitiet == null || tablechitiet.length == 0)
        return;
    var cbo = document.getElementById("nhacungcapselected");
    var obj = {
        manhacungcap: cbo.options[cbo.selectedIndex].value,
        ngaynhap: $('#ngaynhap').val(),
        tongsotien: $('#tongsotien').val(),
        chitietdonhang: tablechitiet
    }
    $.ajax({
        url: "/QuanLyNhap/AddInvoice",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        async: false,
        dataType: "json",
        success: function (result) {
            if (result.Return_Cd)
            {
                GenHtml(result.Data);
                $("#formhoadonnhap").hide();
                SuccesMess("Thêm Hóa Đơn Nhập Thành công");
                // Set default to the filter
                SetDefaultFilter();
            } else {
                ShowErrorMess(result.ErrorMess)
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}
