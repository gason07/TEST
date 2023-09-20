<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Update_Bantin.aspx.cs" Inherits="TEST.TestForm.Update_Bantin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cập nhật bản tin</title>

    <style>
        /* Định dạng cho label */
        label {
            display: block; /* Hiển thị mỗi label trên một dòng */
            font-weight: bold; /* Đặt độ đậm cho văn bản label */
            margin-bottom: 5px; /* Khoảng cách dưới của mỗi label */
        }

        /* Định dạng cho các textbox */
        input[type="text"] {
            width: 100%; /* Chiều rộng 100% của parent container */
            padding: 5px; /* Khoảng cách padding bên trong */
            margin-bottom: 10px; /* Khoảng cách dưới của mỗi textbox */
            border: 1px solid #ccc; /* Đường viền của textbox */
            border-radius: 4px; /* Góc bo tròn cho textbox */
        }

        /* Định dạng cho tiêu đề trang */
        h1 {
            text-align: center; /* Căn giữa tiêu đề */
            font-size: 24px; /* Kích thước chữ tiêu đề */
        }

        /* Định dạng cho đường kẻ ngăn cách */
        h2 {
            text-align: center; /* Căn giữa dòng */
            font-size: 18px; /* Kích thước chữ dòng */
            margin-top: 10px; /* Khoảng cách phía trên dòng */
            margin-bottom: 20px; /* Khoảng cách phía dưới dòng */
            color: #999; /* Màu chữ dòng */
        }

        /* Định dạng cho hộp thoại */
        .modal {
            display: none;
            position: fixed;
            z-index: 1;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            overflow: auto;
            background-color: rgba(0, 0, 0, 0.4);
        }

        /* Nội dung của hộp thoại */
        .modal-content {
            background-color: #fff;
            margin: 15% auto;
            padding: 20px;
            border: 1px solid #888;
            width: 50%;
            text-align: center;
         }

        /* Đóng hộp thoại */
         .close {
            color: #aaa;
            float: right;
            font-size: 28px;
            font-weight: bold;
         }

         .close:hover {
             color: black;
             text-decoration: none;
             cursor: pointer;
          }

         .checkbox-container {
             border: 1px solid #ccc;
             padding: 10px;
             margin-top: 10px;
             background-color: #f5f5f5;
          }

         #pnlContent {
             border: 1px solid #ccc;
             padding: 10px;
             margin-top: 10px;
             background-color: #f5f5f5;
          }

         .horizontal-checkbox {
             display: flex;
             align-items: center;
         }
        .horizontal-checkbox label {
             margin-right: 50px; /* Khoảng cách giữa các ô checkbox */
         }

        .back-button {
             position: fixed;
             top: 10px;
             right: 10px;
             z-index: 9999; /* Điều này đảm bảo rằng nút sẽ hiển thị trên tất cả các phần khác của trang */
         }

    </style>
    <script>
        // Hiển thị hộp thoại
        function showModal() {
            var modal = document.getElementById("myModal");
            modal.style.display = "block";
        }

        // Đóng hộp thoại
        function closeModal() {
            var modal = document.getElementById("myModal");
            modal.style.display = "none";
            window.location.href = "Get_BanTinDuBao.aspx"; //Sau khi đóng hộp thoại thì reset về lại trang Post_BanTinDuBao
        }

        function confirmUpdate() {
            return confirm('Bạn có muốn cập nhật bản tin?');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <h1>Nội Dung Bản Tin Cập Nhật</h1>
            <h1>*************************</h1>
        </center>
    </div>
        <div>
            <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <div>
            <div style="display: flex; justify-content: space-around;">
             <%--<label for="ddlLoaiBanTin">Loại Bản Tin:</label>
             <asp:DropDownList ID="ddlLoaiBanTin" runat="server" AppendDataBoundItems="true">
             <asp:ListItem Text="Chọn Loại Bản Tin" Value="" />
             </asp:DropDownList>

             <label for="ddlThienTai">Loại Thiên Tai:</label>
             <asp:DropDownList ID="ddlThienTai" runat="server" AppendDataBoundItems="true">
             <asp:ListItem Text="Chọn Loại Thiên Tai" Value="" />
             </asp:DropDownList>

             <label for="ddlCapDo">Loại Cấp Độ:</label>
             <asp:DropDownList ID="ddlCapDo" runat="server" AppendDataBoundItems="true">
             <asp:ListItem Text="Chọn Loại Cấp Độ" Value="" />
             </asp:DropDownList>--%>

             <label for="ddlKhuVuc">Khu Vực:</label>
             <asp:DropDownList ID="ddlKhuVuc" runat="server" AppendDataBoundItems="true">
             <asp:ListItem Text="Chọn Khu Vực" Value="" />
             </asp:DropDownList>
             </div>

             <label for="txtTieuDe">Tiêu Đề:</label>
             <textarea id="txtTieuDe" name="txtTieuDe" runat="server" cols="70" rows ="2"></textarea>

             <label for="txtTomTatNoiDung">Tóm Tắt Nội Dung:</label>
             <textarea id="txtTomTatNoiDung" name="txtTomTatNoiDung" runat="server" cols="70" rows ="4"></textarea>


             <label for="txtNoiDung">Nội Dung:</label>
             <textarea id="txtNoiDung" name="txtNoiDung" runat="server" cols="120" rows ="15"></textarea>

             <label for="txtToChucDuBao">Tên Tổ Chức Dự Báo:</label>
             <asp:TextBox ID="txtToChucDuBao" runat="server" placeholder="Tên Tổ Chức Dự Báo"></asp:TextBox>
             
             <label for="txtNguoiDang">Người Đăng:</label>
             <asp:TextBox ID="txtNguoiDang" runat="server" placeholder="Người Đăng"></asp:TextBox>

             <label for="txtNguonTinDuBao">Nguồn Tin Dự Báo:</label>
             <asp:TextBox ID="txtNguonTinDuBao" runat="server" placeholder="Nguồn Tin Dự Báo"></asp:TextBox>

             <label for="txtKeySearch">KEY_SEARCH:</label>
             <asp:TextBox ID="txtKeySearch" runat="server" placeholder="KEY_SEARCH"></asp:TextBox>

             <asp:Panel ID="pnlContent" runat="server">
             <div>
                <label for="txtMoTa">Mô Tả Tập Tin Đính Kèm:</label>
                <asp:TextBox ID="txtMoTa" runat="server" placeholder="Nhập mô tả"></asp:TextBox>
             </div>
             <div>
               <label for="fileDinhKem">Chọn file đính kèm:</label>
               <asp:FileUpload ID="fileDinhKem" runat="server" />
             </div>
             </asp:Panel>

             <div>
                <center>
                    <asp:Button ID="btnUpdate" runat="server" Text="CẬP NHẬT BẢN TIN" OnClientClick="return confirmUpdate();" OnClick="btnUpdate_Click" BackColor="#009900"/>
                </center>
            </div>
            <div id="myModal" class="modal">
               <div class="modal-content">
                <span class="close" onclick="closeModal()">&times;</span>
                <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Green"></asp:Label>
               </div>
            </div>
             
            <div id="btnBack" runat="server" class="back-button">
            <asp:Button ID="btnQuayLai" runat="server" Text="Quay Về" OnClick="btnQuayLai_Click" BackColor="#0099FF" />
            </div>

        </div>
    </form>
</body>
</html>