using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TEST.DataAccess;
using TEST.Utilities;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using System.IO;

namespace TEST.TestForm
{
    public partial class Post_GiayPhep : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                using (TESTEntities dbContext = new TESTEntities())
                {
                    try
                    {
                        // Thực hiện truy vấn để lấy dữ liệu từ bảng "CapDo"
                        var capDoList = dbContext.CapDoes
                            .Select(cd => new { cd.ID, cd.TenLoaiCapDo })
                            .ToList();

                        // Thực hiện truy vấn để lấy dữ liệu từ bảng "CapDo"
                        var donviList = dbContext.DonViToChucs
                            .Select(cd => new { cd.ID, cd.TenDonVi })
                            .ToList();

                        // Gán danh sách mục cho DropDownList
                        ddlCapDo.DataTextField = "TenLoaiCapDo";
                        ddlCapDo.DataValueField = "ID";
                        ddlCapDo.DataSource = capDoList;
                        ddlCapDo.DataBind();

                        // Gán danh sách mục cho DropDownList
                        ddlDoiTuongCungCap.DataTextField = "TenDonVi";
                        ddlDoiTuongCungCap.DataValueField = "ID";
                        ddlDoiTuongCungCap.DataSource = donviList;
                        ddlDoiTuongCungCap.DataBind();

                        // Gán danh sách mục cho DropDownList
                        ddlDonViKyXacNhan.DataTextField = "TenDonVi";
                        ddlDonViKyXacNhan.DataValueField = "ID";
                        ddlDonViKyXacNhan.DataSource = donviList;
                        ddlDonViKyXacNhan.DataBind();

                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy giá trị MaCapDoDuBao từ DropDownList
                int maCapDo = int.Parse(ddlCapDo.SelectedValue);

                // Lấy giá trị DoiTuongCungCap từ DropDownList
                int maDoiTuongCungCap = int.Parse(ddlDoiTuongCungCap.SelectedValue);

                // Lấy giá trị DonViKyXacNhan từ DropDownList
                int maDonViKyXacNhan = int.Parse(ddlDonViKyXacNhan.SelectedValue);

                //Chuyển đổi kiểu dữ liệu ngày tháng
                DateTime ngayThang;
                DateTime.TryParse(txtNgayThang.Text, out ngayThang);

                // Lấy giá trị "Có" hoặc "Không" từ RadioButtonList 
                string giaHan = rblGiaHan.SelectedValue;

                // Thêm dữ liệu vào bảng BanTinDuBao
                using (TESTEntities dbContext = new TESTEntities())
                {
                    var giayPhep = new ThongTinGiayPhep
                    {
                        SoHieu = txtSoHieu.Text,
                        Nam = txtNam.Text,
                        NgayThang = ngayThang,
                        TenDonVi = txtTenDonVi.Text,
                        PhamViHoatDong = txtPhamViHoatDong.Text,
                        DoiTuongCungCap = maDoiTuongCungCap,
                        GiaHan = giaHan,
                        DonViKyXacNhan = maDonViKyXacNhan,
                        MaCapDoDuBao = maCapDo,
                        TrangThaiGiayPhep = txtTrangThaiGiayPhep.Text,
                        LyDoThuHoi = txtLiDoThuHoi.Text

                    };
                    dbContext.ThongTinGiayPheps.Add(giayPhep);
                    dbContext.SaveChanges();

                    // Hiển thị thông báo thành công
                    string successMessage = "Đăng tải thành công.";
                    lblMessage.Text = successMessage;
                    ClientScript.RegisterStartupScript(this.GetType(), "ShowModalScript", "showModal();", true);

                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Đăng tải không thành công.";
                lblMessage.Text = errorMessage;
                ClientScript.RegisterStartupScript(this.GetType(), "ShowModalScript", "showModal();", true);
            }
        }
            

        protected void lnkPickdate_Click(object sender, EventArgs e)
        {
            cld.Visible = true;
        }

        protected void cld_SelectionChanged(object sender, EventArgs e)
        {
            txtNgayThang.Text = cld.SelectedDate.Date.ToShortDateString();
            cld.Visible = false;

        }

        protected void btnQuayLai_Click(object sender, EventArgs e)
        {
            // Chuyển hướng về trang chủ "DichVuChiaSeDuLieuKTTV.aspx"
            Response.Redirect("DichVuChiaSeDuLieuKTTV.aspx");
        }
    }
}