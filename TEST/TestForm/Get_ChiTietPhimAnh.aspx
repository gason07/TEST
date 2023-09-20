<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Get_ChiTietPhimAnh.aspx.cs" Inherits="TEST.TestForm.Get_ChiTietPhimAnh" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chi tiết thông tin phim, ảnh</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center>
                <h1>THÔNG TIN CHI TIẾT PHIM, ẢNH</h1>
                <h1>*********************</h1>
            </center>
        </div>

        <div>
            <asp:GridView ID="gvPhimAnhs" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" />
                    <asp:BoundField DataField="TieuDe" HeaderText="Tiêu Đề" />
                    <asp:BoundField DataField="NgayThang" HeaderText="Ngày Tháng" />
                    <asp:BoundField DataField="ThoiGianCapNhat" HeaderText="Thời Gian Cập Nhật" />
                </Columns>
            </asp:GridView>
        </div>

        <div>
            <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>
        </div>
    </form>
</body>
</html>
