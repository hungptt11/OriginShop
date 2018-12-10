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
        url: '/QuanLyBan/GetListHD',
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
function ThemChiTiet() {
    var cbo = document.getElementById("sanphamselected");
    var object = {
        mametron : $('#metron').val(),
        maSanPham: cbo.options[cbo.selectedIndex].value,
        dongia: $('#sotien').val(),
        sokg: $('#sokg').val(),
        thanhtien: $('#thanhtien').val(),
        dongiathanhpham: $('#dongiathanhpham').val()
    }
    tablechitiet[tablechitiet.length] = object;
    Reloadchitietdh();
}
var sanPhamStatus;
var sokglable = "Số Kg";
function ValidateMeTron()
{
    var mametron = $('#metron').val();
    $.ajax({
        url: "/QuanLyBan/Validatehdban?mahdban=" + mametron,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        async: false,
        dataType: "json",
        success: function (result) {
            if (result.Return_Cd == false) {
                $('#metron').val('');
                $('#myModal').modal('hide');
                ShowErrorMessCallback(result.ErrorMess, function () {
                    $('#myModal').modal('show');
                    $("#sanphamselected").html('');
                });
            } else {
                var i;
                var html = '';
                sanPhamStatus = result.Data;
                if (result.Data.Id == null) {
                    for (i = 0; i< result.Data.length; i++ )
                    {
                        html += '<option value="' + result.Data[i].Id + '">' + result.Data[i].TenSanPham + '</option>';                        
                    }
                    $('#lblsokgsanpham').html(sokglable + "(Còn lại " + (sanPhamStatus[0].SoKg - sanPhamStatus[0].SoDaBan).toString() + " KG)");
                    $('#dongiathanhpham').val(sanPhamStatus[0].DonGia);
                } else {
                    html += '<option value="' + result.Data.Id + '">' + result.Data.TenSanPham + '</option>';
                    $('#lblsokgsanpham').html(sokglable + "(Còn lại " + (sanPhamStatus.SoKg - sanPhamStatus.SoDaBan).toString() + " KG)");
                    $('#dongiathanhpham').val(sanPhamStatus.DonGia);
                }
                $("#sanphamselected").html(html);
                
                
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function Validatesokgsanpham()
{
    if (sanPhamStatus == null)
        return true;
    else {
        var cbo = document.getElementById("sanphamselected");
        var skg = $('#sokg').val();
        if (sanPhamStatus.Id == null)
        {
            for (i = 0; i< sanPhamStatus.length; i++ )
            {
                if(cbo.options[cbo.selectedIndex].value == sanPhamStatus[i].Id)
                {
                    if(skg > (sanPhamStatus[i].SoKg - sanPhamStatus[i].SoDaBan))
                    {
                        $('#sokg').val('');
                        $('#myModal').modal('hide');
                        ShowErrorMessCallback("Không được nhập quá " + (sanPhamStatus[i].SoKg - sanPhamStatus[i].SoDaBan)+ " KG", function () {
                            $('#myModal').modal('show');
                        });
                        return false;
                    }
                    return true;
                }
            }
        } else {
            if (skg > (sanPhamStatus.SoKg - sanPhamStatus.SoDaBan)) {
                $('#sokg').val('');
                $('#myModal').modal('hide');
                ShowErrorMessCallback("Không được nhập quá " + (sanPhamStatus.SoKg - sanPhamStatus.SoDaBan) + " KG", function () {
                    $('#myModal').modal('show');
                });
                return false;
            }
        }        
    }
    return true;
}
function ChangTheMaximumKGAllow()
{
    if (sanPhamStatus.Id != null)
        return;
    var cbo = document.getElementById("sanphamselected");
    for (i = 0; i < sanPhamStatus.length; i++) {
        if (cbo.options[cbo.selectedIndex].value == sanPhamStatus[i].Id) {
            $('#lblsokgsanpham').html(sokglable + "(Còn lại " + (sanPhamStatus[i].SoKg - sanPhamStatus[i].SoDaBan).toString() + " KG)");
            $('#dongiathanhpham').val(sanPhamStatus[i].DonGia);
        }
    }
}
function GetSanPhamName(MaNL) {
    if (ListSanPhams == null) {
        $.ajax({
            url: "/QuanLyBan/ListSanPhams",
            type: "GET",
            contentType: "application/json;charset=UTF-8",
            async: false,
            dataType: "json",
            success: function (result) {
                ListSanPhams = result;
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
    var i;
    for (i = 0; i < ListSanPhams.length; i++) {
        if (ListSanPhams[i].Id == MaNL) {
            return ListSanPhams[i].TenSanPham;
        }
    }
}
function GetNguyenLieuName(MaNL) {
    if (ListSanPhams == null) {
        $.ajax({
            url: "/QuanLyBan/ListNguyenLieu",
            type: "GET",
            contentType: "application/json;charset=UTF-8",
            async: false,
            dataType: "json",
            success: function (result) {
                ListSanPhams = result;
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
    var i;
    for (i = 0; i < ListSanPhams.length; i++) {
        if (ListSanPhams[i].Id == MaNL) {
            return ListSanPhams[i].TenNguyenLieu;
        }
    }
}
function DeleteDetailInvoce(id) {
    if (tablechitiet != null) {
        tablechitiet.splice(id, 1);
        Reloadchitietdh();
    }
}
function Reloadchitietdh() {
    var i;
    var html = '';
    var totalInvoiceMoney = 0;
    for (i = 0; i < tablechitiet.length; i++) {
        html += '<tr><td>' + (i + 1) + '</td>';
        html += '<td>' + tablechitiet[i].mametron + '</td>';
        if (tablechitiet[i].mametron[6] == 'N')
        {
            html += '<td>' + GetNguyenLieuName(tablechitiet[i].maSanPham) + '</td>';
        } else {
            html += '<td>' + GetSanPhamName(tablechitiet[i].maSanPham) + '</td>';
        }        
        html += '<td>' + tablechitiet[i].dongia + '</td>';
        html += '<td>' + tablechitiet[i].sokg + '</td>';
        html += '<td>' + tablechitiet[i].thanhtien + '</td>';
        html += '<td><a href="#" onclick="DeleteDetailInvoce(' + i + ')">Delete</a></td>';
        totalInvoiceMoney += parseInt(tablechitiet[i].thanhtien.replace(/\./g, ''));
    }
    $('#tongsotien').val(formatcurrency(totalInvoiceMoney));
    $('#sanphamtablebody').html(html);
}
function AddRecord() {
    if (tablechitiet == null || tablechitiet.length == 0)
        return;
    var cbo = document.getElementById("khachhangselected");
    var obj = {
        makhachhang: cbo.options[cbo.selectedIndex].value,
        ngayban: $('#ngayban').val(),
        tongsotien: $('#tongsotien').val(),
        chitiethdban: tablechitiet
    }
    $.ajax({
        url: "/QuanLyBan/AddInvoice",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        async: false,
        dataType: "json",
        success: function (result) {
            if (result.Return_Cd) {
                GenHtml(result.Data);
                $("#formhoadonban").hide();
                SuccesMess("Thêm Hóa Đơn Bán Thành công");
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
