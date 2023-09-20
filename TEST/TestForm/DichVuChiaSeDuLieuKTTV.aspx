<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DichVuChiaSeDuLieuKTTV.aspx.cs" Inherits="TEST.TestForm.DichVuChiaSeDuLieuKTTV" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dịch vụ chia sẻ dữ liệu Khí tượng thuỷ văn</title>
    <style>
    .button-container {
        text-align: center;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
             <h1> DỊCH VỤ CHIA SẺ DỮ LIỆU KHÍ TƯỢNG THUỶ VĂN </h1>
             <h3> ******************************************</h3>
        </center>
    </div>
    <div style="display: flex; justify-content: space-around;">
        <div class="button-container">
            <h2>Bản tin dự báo</h2>
            <asp:Button ID="btnChiTietA" runat="server" Text="Chi Tiết" OnClick="btnChiTietA_Click"/>
            <asp:Panel ID="pnlTuyChonA" runat="server" Visible="false">
                    <ul>
                        <li><a href="Get_BanTinDuBao.aspx">Xem bản tin</a></li>
                        <li><a href="Post_BanTinDuBao.aspx">Thêm bản tin</a></li>
                        <li><a href="#">Cập nhật bản tin</a></li>
                        <li><a href="#">Xoá bản tin</a></li>
                    </ul>
            </asp:Panel>
        </div>
        <div class="button-container">
            <h2>Thông tin phim, ảnh</h2>
             <asp:Button ID="btnChiTietB" runat="server" Text="Chi Tiết" OnClick="btnChiTietB_Click"/>
             <asp:Panel ID="pnlTuyChonB" runat="server" Visible="false">
                    <ul>
                        <li><a href="Get_PhimAnh.aspx">Xem thông tin phim, ảnh</a></li>
                        <li><a href="Post_PhimHinhAnh.aspx">Thêm thông tin phim, ảnh</a></li>
                    </ul>
            </asp:Panel>
        </div>
        <div class="button-container">
            <h2>Thông tin bản đồ</h2>
             <asp:Button ID="btnChiTietC" runat="server" Text="Chi Tiết" OnClick="btnChiTietC_Click"/>
             <asp:Panel ID="pnlTuyChonC" runat="server" Visible="false">
                    <ul>
                        <li><a href="Get_BanDo.aspx">Xem thông tin bản đồ</a></li>
                        <li><a href="Post_BanDo.aspx">Thêm thông tin bản đồ</a></li>
                        <li><a href="#">Cập nhật thông tin bản đồ</a></li>
                        <li><a href="#">Xoá bản đồ</a></li>
                    </ul>
            </asp:Panel>
        </div>
        <div class="button-container">
            <h2>Thông tin giấy phép</h2>
             <asp:Button ID="btnChiTietD" runat="server" Text="Chi Tiết" OnClick="btnChiTietD_Click"/>
             <asp:Panel ID="pnlTuyChonD" runat="server" Visible="false">
                    <ul>
                        <li><a href="Get_GiayPhep.aspx">Xem thông tin giấy phép</a></li>
                        <li><a href="Post_GiayPhep.aspx">Thêm thông tin giấy phép</a></li>
                        <li><a href="#">Cập nhật thông tin giấy phép</a></li>
                        <li><a href="#">Xoá giấy phép</a></li>
                    </ul>
            </asp:Panel>
        </div>
        </div>
    </form>
</body>
</html>

