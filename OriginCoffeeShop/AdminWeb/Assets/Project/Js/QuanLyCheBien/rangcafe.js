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
        url: '/QuanLyCheBien/GetListMR',
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
    var cbo = document.getElementById("nguyenlieuselected");
    var addnew = true;
    $.each(tablechitiet, function (index, value) {
        if (value.mahoadonnhap == $('#mahoadonnhap').val() && value.manguyenlieu == cbo.options[cbo.selectedIndex].value) {
            addnew = false;
            ShowErrorMessCallback("Đã tồn tại thông tin nguyên liệu và hóa đơn tại chi tiết mẻ rang", function () {
                $('#myModal').modal('show');
            }, function () {
                $('#myModal').modal('show');
            });
        }
    });
    if (addnew) {
        var cbo1 = document.getElementById("thanhphamselected");
        
        var object = {
            manguyenlieu: cbo.options[cbo.selectedIndex].value,
            mathanhpham: cbo1.options[cbo1.selectedIndex].value,
            mahoadonnhap: $('#mahoadonnhap').val(),
            soknguyenlieu: $('#soknguyenlieu').val(),
            sokgthanhpham: $('#sokgthanhpham').val(),
            chiphiphugia: $('#chiphiphugia').val(),
            dongia: $('#dongiangl').val()
        }
        tablechitiet[tablechitiet.length] = object;
        Reloadchitietdh();
    }
}
function GetNguyenLieuName(MaNL) {
    if (ListNguyenLieus == null) {
        $.ajax({
            url: "/QuanLyCheBien/ListNguyenLieu",
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
function GetThanhPhamName(MaTP) {
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
var nguyeLieuStatus = [];
function HDValidate()
{
    mahoadonnhap = $('#mahoadonnhap').val();
    if(mahoadonnhap == "")
        return;
    $.ajax({
        url: "/QuanLyCheBien/ValidateHDNhap?mahoadonnhap=" + mahoadonnhap,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            if (result.Return_Cd == false) {
                $('#mahoadonnhap').val('');
                $('#myModal').modal('hide');
                ShowErrorMessCallback(result.ErrorMess, function () {
                    $('#myModal').modal('show');
                    $("#nguyenlieuselected").html('');
                });
            } else {
                var i;
                var html = '';
                for (i = 0; i < result.Data.length; i++) {
                    html += '<option value="' + result.Data[i].Id + '">' + result.Data[i].TenNguyenLieu + '</option>';
                }
                $("#nguyenlieuselected").html(html);
                nguyeLieuStatus = result.Data;
                $('#lblsokgnguyenlieu').html($('#lblsokgnguyenlieu').text() + "(Còn lại " + (nguyeLieuStatus[0].SoKG - nguyeLieuStatus[0].SoKGTieuThu).toString() + " KG)");
                $("#dongiangl").val(nguyeLieuStatus[0].DonGia);
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function ShowAvaiableKG()
{
    if(nguyeLieuStatus.length == 0)
        return;
    var cbo = document.getElementById("nguyenlieuselected");
    var i;
    var html = '';
    for (i = 0; i < nguyeLieuStatus.length; i++) {
        if(cbo.options[cbo.selectedIndex].value == nguyeLieuStatus[i].Id)
        {
            $('#lblsokgnguyenlieu').html($('lblsokgnguyenlieu').html() + "(Còn lại " + (nguyeLieuStatus[i].SoKG - nguyeLieuStatus[i].SoKGTieuThu).toString() + " KG)")
            $("#dongiangl").val(nguyeLieuStatus[i].DonGia);
            break;
        }
    }
    
}
function ValidateKG()
{
    var soknguyenlieu = $('#soknguyenlieu').val();
    if (soknguyenlieu == 0 || nguyeLieuStatus == null || nguyeLieuStatus.length == 0)
        return;
    else 
    {
        var i;
        var cbo = document.getElementById("nguyenlieuselected");
        for (i = 0; i < nguyeLieuStatus.length; i++)
        {
            if(cbo.options[cbo.selectedIndex].value == nguyeLieuStatus[i].Id)
            {
                if(soknguyenlieu > (nguyeLieuStatus[i].SoKG - nguyeLieuStatus[i].SoKGTieuThu))
                {
                    $('#myModal').modal('hide');
                    $('#soknguyenlieu').val('');
                    ShowErrorMessCallback("Bạn đã nhập quá số KG có thể Rang<br> Hiện chỉ còn " + (nguyeLieuStatus[i].SoKG - nguyeLieuStatus[i].SoKGTieuThu) + " KG", function () {
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
    var totalnguyenlieukg = 0;
    var totalthanhphamkg = 0;
    for (i = 0; i < tablechitiet.length; i++) {
        html += '<tr><td>' + (i + 1) + '</td>';
        html += '<td>' + tablechitiet[i].mahoadonnhap + '</td>';
        html += '<td>' + GetNguyenLieuName(tablechitiet[i].manguyenlieu) + '</td>';
        html += '<td>' + tablechitiet[i].soknguyenlieu + '</td>';
        html += '<td>' + GetThanhPhamName(tablechitiet[i].mathanhpham) + '</td>';
        html += '<td>' + tablechitiet[i].sokgthanhpham + '</td>';
        html += '<td>' + tablechitiet[i].chiphiphugia + '</td>';
        html += '<td><a href="#" onclick="DeleteDetailInvoce(' + i + ')">Delete</a></td>';
        totalnguyenlieukg += parseInt(tablechitiet[i].soknguyenlieu);
        totalthanhphamkg += parseInt(tablechitiet[i].sokgthanhpham);
    }
    $('#SoKgThanhPham').val(totalthanhphamkg);
    $('#SoKgNguyenLieu').val(totalnguyenlieukg);
    $('#chitietmerangtablebody').html(html);
}
function AddRecord() {
    if (tablechitiet == null || tablechitiet.length == 0)
        return;
    var obj = {
        SoKgNguyenLieu: $('#SoKgNguyenLieu').val(),
        NgayRang: $('#NgayRang').val(),
        SoKgThanhPham: $('#SoKgThanhPham').val(),
        chitietmerang: tablechitiet
    }
    $.ajax({
        url: "/QuanLyCheBien/AddMeRang",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        async: false,
        dataType: "json",
        success: function (result) {
            if (result.Return_Cd) {
                GenHtml(result.Data);
                $("#formmerang").hide();
                nguyeLieuStatus = [];
                SuccesMess("Thêm Mẻ Rang Thành công");
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
