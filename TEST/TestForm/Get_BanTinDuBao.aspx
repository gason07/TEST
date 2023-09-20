﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Get_BanTinDuBao.aspx.cs" Inherits="TEST.TestForm.Get_BanTinDuBao" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tất cả bản tin</title>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center>
                <h1>TẤT CẢ BẢN TIN DỰ BÁO</h1>
                <h2>*********************</h2>
            </center>
        </div>

     
            <div style="display: flex; justify-content: space-around;">
             <div  style="display: flex; align-items: center;">
             <asp:DropDownList ID="ddlTieuDe" runat="server" AppendDataBoundItems="true">
             <asp:ListItem Text="Chọn Tiêu Đề" Value="" />
             </asp:DropDownList>
             <asp:Button ID="btnSearch" runat="server" Text="Tìm Kiếm" OnClick="btnSearch_Click" />
             </div>
            </div>
     
    <h2></h2>
    <div style="display: flex; justify-content: space-around;">
    <asp:GridView ID="gvFullNews" runat="server" AutoGenerateColumns="false">
    <Columns>
        <asp:BoundField DataField="ID" HeaderText="ID" />
         <asp:BoundField DataField="TieuDe" HeaderText="Tiêu Đề" />
        <asp:BoundField DataField="NgayThucHien" HeaderText="Ngày Thực Hiện" />
        <asp:TemplateField HeaderText="Tác Vụ">
            <ItemTemplate>
                <asp:LinkButton ID="lnkView" runat="server" Text="Xem" OnClick="lnkView_Click" CommandArgument='<%# Eval("ID") %>' />  
                <asp:LinkButton ID="lnkUpdate" runat="server" Text="Sửa" OnClick="lnkUpdate_Click" CommandArgument='<%# Eval("ID") %>' />  
                <asp:LinkButton ID="lnkDelete" runat="server" Text="Xoá" OnClick="lnkDelete_Click" CommandArgument='<%# Eval("ID") %>' />  
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    </asp:GridView>

    </div>
      <div>
    <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>
    </div>
    </form>
</body>
</html>
