<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Get_PhimAnh.aspx.cs" Inherits="TEST.TestForm.Get_PhimAnh" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Thông tin phim, hình ảnh</title>
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
            window.location.href = "Get_PhimAnh.aspx"; //Sau khi đóng hộp thoại thì reset về lại trang Get_PhimAnh
        }

        function confirmDelete() {
            return confirm('Bạn có muốn xoá?');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center>
                <h1>THÔNG TIN PHIM, HÌNH ẢNH</h1>
                <h1>*********************</h1>
            </center>
        </div>
    <div>

    <asp:GridView ID="gvPhimAnh" runat="server" AutoGenerateColumns="false">
    <Columns>
        <asp:BoundField DataField="ID" HeaderText="ID" />
        <asp:BoundField DataField="TieuDe" HeaderText="Tiêu Đề" />
        <asp:TemplateField HeaderText="Tác Vụ">
            <ItemTemplate>
                <asp:LinkButton ID="lnkView" runat="server" Text="Xem" OnClick="lnkView_Click" CommandArgument='<%# Eval("ID") %>' />  
                <asp:LinkButton ID="lnkDelete" runat="server" Text="Xoá" OnClientClick="return confirmDelete();" OnClick="lnkDelete_Click" CommandArgument='<%# Eval("ID") %>' />  
   
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    </asp:GridView>
    </div>

          <div id="myModal" class="modal">
               <div class="modal-content">
                <span class="close" onclick="closeModal()">&times;</span>
                <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Green"></asp:Label>
               </div>
            </div>
      <div>
    <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>
    </div>
    </form>
</body>
</html>
