using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TEST.Models;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TEST.TestForm
{
    public partial class Get_XemTepBanTin : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            // Kiểm tra xem có tham số idbt trong URL không
            if (Request.QueryString["idbt"] != null)
            {
                int idbt;
                // Lấy giá trị idbt từ URL và chuyển nó thành số nguyên
                if (int.TryParse(Request.QueryString["idbt"], out idbt))
                {
                    // Gọi hàm để lấy và hiển thị ảnh dựa trên idbt
                    LayVaHienThiAnh(idbt);
                }
                else
                {
                    // Xử lý trường hợp idbt không hợp lệ
                    Response.Write("ID không hợp lệ.");
                }
            }
            else
            {
                // Xử lý trường hợp không có tham số idbt trong URL
                Response.Write("Không có ID được truyền.");
            }
        }

        private void LayVaHienThiAnh(int idbt)
        {
            // Đặt URL của API bạn muốn sử dụng
            string apiUrl = $"http://localhost:10241/api/TEST/LayTepDinhKemCuaBanTin?idbt={idbt}";

            using (var client = new HttpClient())
            {
                // Gửi yêu cầu GET đến API và nhận dữ liệu trả về
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;

                // Kiểm tra xem yêu cầu đã thành công hay không
                if (response.IsSuccessStatusCode)
                {
                    // Đọc dữ liệu JSON từ API và chuyển thành đối tượng dựa trên mô hình dữ liệu
                    var result = response.Content.ReadAsAsync<File_DK>().Result;

                    // Kiểm tra xem có đường dẫn ảnh hay không
                    if (!string.IsNullOrEmpty(result?.DuongDanURL))
                    {
                        // Đặt đường dẫn ảnh vào thuộc tính src của thẻ <img>
                        imgAnh.Src = result.DuongDanURL;
                    }
                    else
                    {
                        Response.Write("Không tìm thấy ảnh.");
                    }
                }
                else
                {
                    // Xử lý trường hợp API trả về lỗi
                    Response.Write("Không thể truy cập dữ liệu từ API. Vui lòng thử lại sau.");
                }
            }
        }
    }          
}