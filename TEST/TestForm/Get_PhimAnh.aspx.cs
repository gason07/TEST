using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TEST.DataAccess;
using TEST.Utilities;
using TEST.Models;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using System.IO;

namespace TEST.TestForm
{
    public partial class Get_PhimAnh : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Đặt URL của API bạn muốn sử dụng
            string apiUrl = "http://localhost:10241/api/TEST/GetTatCaThongTinPhimAnh";

            using (var client = new HttpClient())
            {
                // Gửi yêu cầu GET đến API và nhận dữ liệu trả về
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;

                // Kiểm tra xem yêu cầu đã thành công hay không
                if (response.IsSuccessStatusCode)
                {
                    // Đọc dữ liệu JSON từ API và chuyển thành đối tượng phimHinhAnh
                    var phimHinhAnh = response.Content.ReadAsAsync<List<AnhPhim>>().Result;

                    // Kiểm tra xem có dữ liệu chi tiết bản tin hay không
                    if (phimHinhAnh != null && phimHinhAnh.Any())
                    {
                        // Gán dữ liệu cho GridView để hiển thị thông tin.
                        gvPhimAnh.DataSource = phimHinhAnh;
                        gvPhimAnh.DataBind();

                        // Xóa thông báo lỗi nếu có
                        lblErrorMessage.Text = "";
                    }
                    else
                    {
                        // Hiển thị thông báo lỗi nếu không tìm thấy ID
                        lblErrorMessage.Text = "Không tìm thấy thông tin với ID này.";

                        gvPhimAnh.DataSource = null; // Gán null hoặc danh sách rỗng cho GridView
                        gvPhimAnh.DataBind();
                    }
                }
            }
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            LinkButton lnkView = (LinkButton)sender;
            string idpa = lnkView.CommandArgument;

            // Chuyển hướng sang trang "ChiTietBanTin.aspx" với tham số ID
            Response.Redirect($"Get_ChiTietPhimAnh.aspx?idpa={idpa}");
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            LinkButton lnkDelete = (LinkButton)sender;
            string id = lnkDelete.CommandArgument;

            ClientScript.RegisterStartupScript(this.GetType(), "ShowModalScript", "showModal();", true);

            int idbt;
            if (int.TryParse(id, out idbt))
            {
                // Thực hiện xoá
                DeleteBantin(idbt);
            }
        }

        private void DeleteBantin(int idbt)
        {
            using (TESTEntities dbContext = new TESTEntities())
            {
                var banTin = dbContext.PhimHinhAnhs.FirstOrDefault(bt => bt.ID == idbt && bt.IsDeleted == false);
                if (banTin != null)
                {
                    //dbContext.BanTinDuBaos.Remove(banTin);
                    banTin.IsDeleted = true; // Đánh dấu bản tin đã bị xóa.

                    dbContext.SaveChanges();
                    string successMessage = "Xoá thành công.";
                    lblMessage.Text = successMessage;
                    ClientScript.RegisterStartupScript(this.GetType(), "ShowModalScript", "showModal();", true);
                }
                else
                {
                    string errorMessage = "Thông tin đã bị xoá hoặc không tồn tại.";
                    lblMessage.Text = errorMessage;
                    ClientScript.RegisterStartupScript(this.GetType(), "ShowModalScript", "showModal();", true);
                }
            }
        }
    }
}