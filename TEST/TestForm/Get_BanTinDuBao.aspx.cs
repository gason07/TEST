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
    public partial class Get_BanTinDuBao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                using (TESTEntities dbContext = new TESTEntities())
                {
                    try
                    {

                        // Thực hiện truy vấn để lấy dữ liệu từ bảng "BanTinDuBao"
                        var banTinList = dbContext.BanTinDuBaos.Where(bt => bt.TieuDe != "" && bt.IsDeleted == false)
                            .Select(bt => new { bt.TieuDe })
                            .ToList();

                        // Tạo một danh sách duy nhất không chứa các tiêu đề trùng tên
                        var uniqueTieuDe = banTinList.Distinct().ToList();

                        // Gán danh sách tiêu đề duy nhất cho DropDownList
                        ddlTieuDe.DataTextField = "TieuDe";
                        ddlTieuDe.DataSource = uniqueTieuDe;
                        ddlTieuDe.DataBind();

                        // Gán danh sách mục cho DropDownList
                        //ddlTieuDe.DataTextField = "TieuDe";
                        //ddlTieuDe.DataSource = banTinList;
                        //ddlTieuDe.DataBind();
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            // Đặt URL của API bạn muốn sử dụng
            string apiUrl = "http://localhost:10241/api/TEST/TatCaBanTinDuBao";

            using (var client = new HttpClient())
            {
                // Gửi yêu cầu GET đến API và nhận dữ liệu trả về
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;

                // Kiểm tra xem yêu cầu đã thành công hay không
                if (response.IsSuccessStatusCode)
                {
                    // Đọc dữ liệu JSON từ API và chuyển thành đối tượng ChiTietBanTin
                    var tatCaBanTin = response.Content.ReadAsAsync<List<FullNews>>().Result;

                    // Kiểm tra xem có dữ liệu chi tiết bản tin hay không
                    if (tatCaBanTin != null && tatCaBanTin.Any())
                    {
                        // Gán dữ liệu cho GridView để hiển thị thông tin.
                        gvFullNews.DataSource = tatCaBanTin;
                        gvFullNews.DataBind();

                        // Xóa thông báo lỗi nếu có
                        lblErrorMessage.Text = "";
                    }
                    else
                    {
                        // Hiển thị thông báo lỗi nếu không tìm thấy ID
                        lblErrorMessage.Text = "Không tìm thấy bản tin với ID này.";

                        gvFullNews.DataSource = null; // Gán null hoặc danh sách rỗng cho GridView
                        gvFullNews.DataBind();
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // Lấy giá trị được chọn từ DropDownList
            string tieuDe = ddlTieuDe.SelectedValue;

            // Kiểm tra xem đã chọn một tiêu đề nào đó chưa
            if (!string.IsNullOrEmpty(tieuDe))
            {
                // Chuyển hướng sang trang Get_BanTinByTieuDe.aspx và truyền tham số tiêu đề
                Response.Redirect("Get_BanTinByTieuDe.aspx?tieuDe=" + tieuDe);
            }
            else
            {
                // Hiển thị thông báo nếu không có tiêu đề nào được chọn
                lblErrorMessage.Text = "Vui lòng chọn một tiêu đề.";
            }
        }




        protected void lnkView_Click(object sender, EventArgs e)
        {
            LinkButton lnkView = (LinkButton)sender;
            string idbt = lnkView.CommandArgument;

            // Chuyển hướng sang trang "ChiTietBanTin.aspx" với tham số ID
            Response.Redirect($"Get_ChiTietBanTin.aspx?idbt={idbt}");
        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            LinkButton lnkUpdate = (LinkButton)sender;
            string idbt = lnkUpdate.CommandArgument;

            // Chuyển hướng sang trang "ChiTietBanTin.aspx" với tham số ID
            Response.Redirect($"Update_BanTin.aspx?idbt={idbt}");
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            LinkButton lnkDelete = (LinkButton)sender;
            string id = lnkDelete.CommandArgument;

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
                var banTin = dbContext.BanTinDuBaos.FirstOrDefault(bt => bt.ID == idbt && bt.IsDeleted == false);
                if (banTin != null)
                {
                    dbContext.BanTinDuBaos.Remove(banTin);
                    banTin.IsDeleted = true; // Đánh dấu bản tin đã bị xóa.

                    dbContext.SaveChanges();
                }
            }
        }
    }
}