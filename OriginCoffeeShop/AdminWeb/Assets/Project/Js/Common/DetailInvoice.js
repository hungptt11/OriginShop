function DetailInvoice(id) {
    $("#formdetailinvoice").show();
    $('html, body').animate({
        scrollTop: $("#formdetailinvoice").offset().top
    }, 1000);
    $.ajax({
        url: urlDetailInvoice + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        async: false,
        dataType: "json",
        success: function (result) {
            if (result.Return_Cd) {
                var chitietdonhang = [];
                chitietdonhang = result.data.chitiethdban;
                var i;
                var html = '';
                for (i = 0; i < chitietdonhang.length; i++) {
                    html += '<tr><td>' + (i + 1) + '</td>';
                    html += '<td>' + chitietdonhang[i].mametron + '</td>';
                    html += '<td>' + GetSanPhamName(chitietdonhang[i].maSanPham) + '</td>';
                    html += '<td>' + formatcurrency(chitietdonhang[i].dongia) + '</td>';
                    html += '<td>' + chitietdonhang[i].sokg + '</td>';
                    html += '<td>' + formatcurrency(chitietdonhang[i].thanhtien) + '</td>';
                    html += '<td>' + formatcurrency(chitietdonhang[i].loinhuan) + '</td>';
                }
                $('#tongsotienct').val(formatcurrency(result.data.tongsotien));
                $('#khachhang').val(GetKhachHangName(result.data.makhachhang));
                $('#ngaybanct').val(ConvertJsonDateString(result.data.ngayban));
                $('#mahoadonct').val(result.data.mahoadonban);
                $('#chihoadondetailbody').html(html);
            } else {
                ShowErrorMess(result.ErrorMess);
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function RemoveHD(hd) {
    $(hd).hide("slow");
}