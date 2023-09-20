<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Post_BanDo.aspx.cs" Inherits="TEST.TestForm.Post_BanDo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Đăng thông tin bản đồ</title>

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

         .textBoxContainer {
             position: relative;
         }

          #lnkPickdate {
             position: absolute;
             right: 0;
             top: 0;
             height: 100%;
             padding: 0 5px; /* Điều chỉnh padding theo nhu cầu của bạn */
             background: transparent;
             border: none;
             cursor: pointer;
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
            window.location.href = "Post_BanDo.aspx";
        }

        function confirmUpload() {
            return confirm('Bạn chắc chắn muốn đăng thông tin bản đồ ?');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <h1>Nội Dung Thông Tin Bản Đồ</h1>
            <h2>*************************</h2>
        </center>
    </div>
    <div>
        <label for="txtTenBanDo">Tên Bản Đồ:</label>
        <asp:TextBox ID="txtTenBanDo" runat="server" placeholder="Nhập Tên Bản Đồ"></asp:TextBox>

        <label for="txtMoTa">Mô Tả:</label>
        <asp:TextBox ID="txtMoTa" runat="server" placeholder="Nhập mô tả"></asp:TextBox>

        <label for="fileDinhKem">Chọn file đính kèm:</label>
        <asp:FileUpload ID="fileDinhKem" runat="server" />

        <div>
                <center>
                    <asp:Button ID="btnUpload" runat="server" Text="ĐĂNG TẢI" OnClientClick="return confirmUpload();" OnClick="btnUpload_Click" BackColor="#009900"/>
                </center>
        </div>
        <div id="myModal" class="modal">
            <div class="modal-content">
                <span class="close" onclick="closeModal()">&times;</span>
                <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Green"></asp:Label>
            </div>
        </div>

            <div id="btnBack" runat="server" class="back-button">
            <asp:Button ID="btnQuayLai" runat="server" Text="Quay Về Trang Chủ" OnClick="btnQuayLai_Click" BackColor="#0099FF"/>
            </div>
    </div>
    </form>
</body>
</html>
