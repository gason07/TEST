<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Get_ChiTietBanTin.aspx.cs" Inherits="TEST.TestForm.Get_ChiTietBanTin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chi Tiết Bản Tin</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center>
                <h1>Chi Tiết Bản Tin</h1>
                <h1>****************</h1>
            </center>
        </div>
        <div>
            <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <div>
            <asp:DataList ID="dataListChiTiet" runat="server" RepeatLayout="Flow">
                <ItemTemplate>
                    <div>
                        <p><strong>Tiêu Đề:</strong> <%# Eval("TieuDe") %></p>
                        <p><strong>Tóm Tắt Nội Dung:</strong> <%# Eval("TomTatNoiDung") %></p>
                        <p><strong>Nội Dung:</strong> <%# Eval("NoiDung") %></p>
                        <p><strong>Ngày thực hiện:</strong> <%# Eval("NgayThucHien") %></p>
                        <p><strong>Tên Bản Tin:</strong> <%# Eval("TenBanTin") %></p>
                        <p><strong>Thông Tin Địa Điểm:</strong> <%# Eval("ThongTinDiaDiem") %></p>
                        <p><strong>Khu Vực Dự Báo:</strong> <%# Eval("KhuVucDuBao") %></p>
                        <p><strong>Từ Thời Gian:</strong> <%# Eval("TuThoiGian") %></p>
                        <p><strong>Đến Thời Gian:</strong> <%# Eval("DenThoiGian") %></p>
                        <p><strong>Cấp Độ Dự Báo:</strong> <%# Eval("CapDoDuBao") %></p>
                        <p><strong>Loại Thiên Tai:</strong> <%# Eval("LoaiThienTai") %></p>
                        <p><strong>Tổ Chức Dự Báo:</strong> <%# Eval("TenToChucDuBao") %></p>
                        <p><strong>Người Thực Hiện:</strong> <%# Eval("NguoiThucHien") %></p>
                        <p><strong>Tệp Đính Kèm:</strong></p>
                        <asp:Repeater ID="rptAttachments" runat="server" DataSource='<%# Eval("TepDinhKem") %>'>
                            <ItemTemplate>
                              <asp:HyperLink ID="HyperLink1" runat="server" 
                NavigateUrl='<%# "Get_FileDK.aspx?idbt=" + Eval("ID") %>' 
                Text='<%# Eval("TenTapTin") %>' Target="_blank"></asp:HyperLink>

                              <br />
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </form>
</body>
</html>
