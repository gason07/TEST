using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Net.Http;
using TEST.Models;
using System.Web.UI.WebControls;
using System.IO;

namespace TEST.TestForm
{
    public partial class Get_FileDK : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Kiểm tra xem có tham số idbt trong URL không
                if (Request.QueryString["idbt"] != null)
                {
                    int idbt;
                    // Lấy giá trị idbt từ URL và chuyển nó thành số nguyên
                    if (int.TryParse(Request.QueryString["idbt"], out idbt))
                    {
                        // Đặt URL của API bạn muốn sử dụng
                        GetFile(idbt);
                       
                    }
                    else
                    {
                        // Xử lý trường hợp idbt không hợp lệ
                        lblErrorMessage.Text = "ID không hợp lệ.";
                    }
                }
                else
                {
                    // Xử lý trường hợp không có tham số idbt trong URL
                    lblErrorMessage.Text = "Không có ID được truyền.";
                }
            }
        }

        private void GetFile(int idbt)
        {
            string apiUrl = $"http://localhost:10241/api/TEST/LayTepDinhKemCuaBanTin?idbt=" + idbt;
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    lblErrorMessage.Text = "";
                }
                else
                {
                    // Xử lý trường hợp API trả về lỗi
                    lblErrorMessage.Text = "Không thể truy cập dữ liệu từ API. Vui lòng thử lại sau.";
                }
            }
        }

    }
}