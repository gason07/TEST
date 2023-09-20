using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Net.Http;
using TEST.Models;
using TEST.Utilities;
using System.Web.UI.WebControls;
using System.IO;

namespace TEST.TestForm
{
    public partial class Get_ChiTietPhimAnh : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Kiểm tra xem có tham số idbt trong URL không
                if (Request.QueryString["idpa"] != null)
                {
                    int idpa;
                    // Lấy giá trị idbt từ URL và chuyển nó thành số nguyên
                    if (int.TryParse(Request.QueryString["idpa"], out idpa))
                    {
                        // Gọi hàm để lấy và hiển thị chi tiết bản tin dựa trên idbt
                        LayVaHienThiChiTietPhimAnh(idpa);
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

        private void LayVaHienThiChiTietPhimAnh(int idpa)
        {
            // Đặt URL của API bạn muốn sử dụng
            string apiUrl = $"http://localhost:10241/api/TEST/LayThongTinPhimAnhTheoID?idpa=" + idpa;

            using (var client = new HttpClient())
            {
                // Gửi yêu cầu GET đến API và nhận dữ liệu trả về
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;

                // Kiểm tra xem yêu cầu đã thành công hay không
                if (response.IsSuccessStatusCode)
                {
                    // Đọc dữ liệu JSON từ API và chuyển thành danh sách chi tiết phim ảnh
                    var chiTietPhimAnh = response.Content.ReadAsAsync<List<AnhPhim>>().Result;

                    // Kiểm tra xem có dữ liệu chi tiết bản tin hay không
                    if (chiTietPhimAnh != null && chiTietPhimAnh.Count > 0)
                    {
                        // Gán dữ liệu cho DataList để hiển thị thông tin
                        gvPhimAnhs.DataSource = chiTietPhimAnh;
                        gvPhimAnhs.DataBind();

                        // Xóa thông báo lỗi nếu có
                        lblErrorMessage.Text = "";
                    }
                    else
                    {
                        // Hiển thị thông báo lỗi nếu không tìm thấy bản tin với ID
                        lblErrorMessage.Text = "Không tìm thấy bản tin với ID này.";
                    }
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