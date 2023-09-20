<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Get_XemTepBanTin.aspx.cs" Inherits="TEST.TestForm.Get_XemTepBanTin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Xem Ảnh</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Xem Ảnh</h1>
    <div>
        <!-- Thẻ <img> để hiển thị ảnh -->
        <img id="imgAnh" runat="server" alt="Ảnh" style="max-width: 100%; max-height: 400px;" />
    </div>
</asp:Content>

   </div>
    </form>
</body>
</html>
