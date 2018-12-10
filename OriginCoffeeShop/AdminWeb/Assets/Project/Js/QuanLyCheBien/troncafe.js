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
        url: '/QuanLyCheBien/GetListMeTron',
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
function OnchangeHinhThucTron()
{
    var cbo = document.getElementById("hinhthuctronselected");
    if(cbo.options[cbo.selectedIndex].value == 1)
    {
        if (ListSanPhams == null) {
            $.ajax({
                url: "/QuanLyCheBien/ListSanPhams",
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
        html = '';
        for (i = 0; i < ListSanPhams.length; i++) {
            html += '<option value="' + ListSanPhams[i].Id + '">' + ListSanPhams[i].TenSanPham + '</option>';
        }
        $("#sanphamselected").html(html);
        $("#thanhphamdebanselected").html('');
        $("#thanhphamdebanselected").attr('disabled', 'disabled');
        $("#sanphamselected").removeAttr('disabled');
    } else {
        if(ListThanhPhams == null)
        {
            $.ajax({
                url: "/QuanLyCheBien/ListThanhPham",
                type: "GET",
                contentType: "application/json;charset=UTF-8",
                async: false,
                dataType: "json",
                success: function (result) {
                    ListThanhPhams = result;
                },
                error: function (errormessage) {
                    alert(errormessage.responseText);
                }
            });
        }
        var i;
        html = '';
        for (i = 0; i < ListThanhPhams.length; i++) {
            html += '<option value="' + ListThanhPhams[i].Id + '">' + ListThanhPhams[i].TenThanhPham + '</option>';
        }
        $("#thanhphamdebanselected").html(html);
        $("#sanphamselected").html('');
        $("#sanphamselected").attr('disabled', 'disabled');
        $("#thanhphamdebanselected").removeAttr('disabled');
    }
}
var tablechitiet = [];
function ThemChiTiet() {    
    var cbo1 = document.getElementById("thanhphamselected");
    var addnew = true;
    $.each(tablechitiet, function (index, value) {
        if (value.mamerangortron == $('#MaMeRangOrTron').val() && value.mathanhpham == cbo1.options[cbo1.selectedIndex].value) {
            addnew = false;
            ShowErrorMessCallback("Đã tồn tại thông tin thành phẩm và hóa đơn tại chi tiết mẻ trộn", function () {
                $('#myModal').modal('show');
            }, function () {
                $('#myModal').modal('show');
            });
        }
    });
    if (addnew) {
        var cbo = document.getElementById("loaihoadonselected");
        var object = {
            duoctrontu: cbo.options[cbo.selectedIndex].value,
            mamerangortron: $('#MaMeRangOrTron').val(),
            mathanhpham: cbo1.options[cbo1.selectedIndex].value,
            sokgthanhpham: $('#sokgthanhpham').val(),
            dongia:$('#dongia').val()
        }
        tablechitiet[tablechitiet.length] = object;
        Reloadchitietdh();
    }
}
var ListNguyenLieu;
function GetThanhPhamName(MaTP, typeofInvoice) {
    if (typeofInvoice == null || typeofInvoice == 1) {
        if (ListThanhPhams == null) {
            $.ajax({
                url: "/QuanLyCheBien/ListThanhPham",
                type: "GET",
                contentType: "application/json;charset=UTF-8",
                async: false,
                dataType: "json",
                success: function (result) {
                    ListThanhPhams = result;
                },
                error: function (errormessage) {
                    alert(errormessage.responseText);
                }
            });
        }
        var i;
        for (i = 0; i < ListThanhPhams.length; i++) {
            if (ListThanhPhams[i].Id == MaTP) {
                return ListThanhPhams[i].TenThanhPham;
            }
        }
    }
    else {
        if (ListNguyenLieu == null) {
            $.ajax({
                url: "/QuanLyCheBien/ListNguyenLieu",
                type: "GET",
                contentType: "application/json;charset=UTF-8",
                async: false,
                dataType: "json",
                success: function (result) {
                    ListNguyenLieu = result;
                },
                error: function (errormessage) {
                    alert(errormessage.responseText);
                }
            });
        }
        var i;
        for (i = 0; i < ListNguyenLieu.length; i++) {
            if (ListNguyenLieu[i].Id == MaTP) {
                return ListNguyenLieu[i].TenNguyenLieu;
            }
        }
    }
}
var thanhPhamStatus = [];
var lblsokgthanhphamtitle = "Số Kg Thành Phẩm";
function HDValidate() {
    mahoadonnhap = $('#MaMeRangOrTron').val();
    var cbo = document.getElementById("loaihoadonselected");

    if (mahoadonnhap == "")
        return;
    $.ajax({
        url: "/QuanLyCheBien/ValidateHD?mahd=" + mahoadonnhap + '&&maloaihinhhd=' + cbo.options[cbo.selectedIndex].value,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            if (result.Return_Cd == false) {
                $('#mahoadonnhap').val('');
                $('#myModal').modal('hide');
                ShowErrorMessCallback(result.ErrorMess, function () {
                    $('#myModal').modal('show');
                    $("#thanhphamselected").html('');
                });
            } else {
                var i;
                var html = '';
                for (i = 0; i < result.Data.length; i++) {
                    html += '<option value="' + result.Data[i].Id + '">' + result.Data[i].TenThanhPham + '</option>';
                }
                $("#thanhphamselected").html(html);
                thanhPhamStatus = result.Data;
                $('#lblsokgthanhpham').html(lblsokgthanhphamtitle + "(Còn lại " + (thanhPhamStatus[0].SoKg - thanhPhamStatus[0].SoDaBan).toString() + " KG)");
                $('#dongia').val(thanhPhamStatus[0].DonGia);
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function ShowAvaiableKG() {
    if (thanhPhamStatus.length == 0)
        return;
    var cbo = document.getElementById("thanhphamselected");
    var i;
    var html = '';
    for (i = 0; i < thanhPhamStatus.length; i++) {
        if (cbo.options[cbo.selectedIndex].value == thanhPhamStatus[i].Id) {
            $('#lblsokgthanhpham').html($('lblsokgthanhpham').html() + "(Còn lại " + (thanhPhamStatus[i].SoKg - thanhPhamStatus[i].SoDaBan).toString() + " KG)")
            $('#dongia').val(thanhPhamStatus[i].DonGia);
            break;
        }
    }

}
function ValidateKG() {
    var sokgthanhpham = $('#sokgthanhpham').val();
    if (sokgthanhpham == 0 || thanhPhamStatus == null || thanhPhamStatus.length == 0)
        return;
    else {
        var i;
        var cbo = document.getElementById("thanhphamselected");
        for (i = 0; i < thanhPhamStatus.length; i++) {
            if (cbo.options[cbo.selectedIndex].value == thanhPhamStatus[i].Id) {
                if (sokgthanhpham > (thanhPhamStatus[i].SoKg - thanhPhamStatus[i].SoDaBan)) {
                    $('#myModal').modal('hide');
                    $('#sokgthanhpham').val('');
                    ShowErrorMessCallback("Bạn đã nhập quá số KG có thể Rang<br> Hiện chỉ còn " + (thanhPhamStatus[i].SoKg - thanhPhamStatus[i].SoDaBan) + " KG", function () {
                        $('#myModal').modal('show');
                    });
                }
            }
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
    var totalthanhphamkg = 0;
    for (i = 0; i < tablechitiet.length; i++) {
        var thanhPName = '';
        if (tablechitiet[i].mamerangortron[6] == 'N')
        {
            thanhPName = GetThanhPhamName(tablechitiet[i].mathanhpham, 2);
        } else {
            thanhPName = GetThanhPhamName(tablechitiet[i].mathanhpham);
        }
        html += '<tr><td>' + (i + 1) + '</td>';
        html += '<td>' + tablechitiet[i].mamerangortron + '</td>';
        html += '<td>' + thanhPName + '</td>';
        html += '<td>' + tablechitiet[i].sokgthanhpham + '</td>';
        html += '<td><a href="#" onclick="DeleteDetailInvoce(' + i + ')">Delete</a></td>';
        totalthanhphamkg += parseInt(tablechitiet[i].sokgthanhpham);
    }
    $('#sokg').val(totalthanhphamkg);
    $('#chitietmetrontablebody').html(html);
}
function AddRecord() {
    if (tablechitiet == null || tablechitiet.length == 0)
        return;
    var cbo = document.getElementById("thanhphamdebanselected");
    var cbo1 = document.getElementById("hinhthuctronselected");
    var cbo2 = document.getElementById("sanphamselected");
    var obj = {
        NgayTron: $('#NgayTron').val(),
        SoKg: $('#sokg').val(),
        HinhThucTron: cbo1.options[cbo1.selectedIndex].value,
        masanpham: cbo1.options[cbo1.selectedIndex].value == 2 ? 0 : cbo2.options[cbo2.selectedIndex].value,
        mathanhpham: cbo1.options[cbo1.selectedIndex].value == 1 ? 0 : cbo.options[cbo.selectedIndex].value,
        chitietmetron: tablechitiet
    }
    $.ajax({
        url: "/QuanLyCheBien/AddMeTron",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        async: false,
        dataType: "json",
        success: function (result) {
            if (result.Return_Cd) {
                GenHtml(result.Data);
                $("#formmetron").hide();
                thanhPhamStatus = [];
                SuccesMess("Thêm Mẻ Trộn Thành công");
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
