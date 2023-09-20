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
    public partial class Post_BanTinDuBao : System.Web.UI.Page
    {
        string urlHost = ClsFtp.GetHostFTP();
        string UserName = ClsFtp.GetUserNameFTP();
        string Password = ClsFtp.GetPassWordFTP();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                using (TESTEntities dbContext = new TESTEntities())
                {
                    try
                    {
                        // Thực hiện truy vấn để lấy dữ liệu từ bảng "KhuVuc"
                        var khuVucList = dbContext.KhuVucs
                            .Select(kv => new { kv.ID, kv.TenKhuVuc })
                            .ToList();

                        // Thực hiện truy vấn để lấy dữ liệu từ bảng "KVHC"
                        var khuVucHCList = dbContext.KhuVucHanhChinhs
                            .Select(kvhc => new { kvhc.MaKVHC, kvhc.TenKVHC })
                            .ToList();

                        // Thực hiện truy vấn để lấy dữ liệu từ bảng "LoaiThienTai"
                        var thienTaiList = dbContext.LoaiThienTais
                            .Select(tt => new { tt.ID, tt.TenLoaiThienTai })
                            .ToList();

                        // Thực hiện truy vấn để lấy dữ liệu từ bảng "CapDo"
                        var capDoList = dbContext.CapDoes
                            .Select(cd => new { cd.ID, cd.TenLoaiCapDo })
                            .ToList();

                        // Thực hiện truy vấn để lấy dữ liệu từ bảng "LoaiBanTin"
                        var banTinList = dbContext.LoaiBanTins
                            .Select(bt => new { bt.ID, bt.TenLoaiBanTin })
                            .ToList();

                        // Gán danh sách mục cho DropDownList
                        ddlKhuVuc.DataTextField = "TenKhuVuc";
                        ddlKhuVuc.DataValueField = "ID";
                        ddlKhuVuc.DataSource = khuVucList;
                        ddlKhuVuc.DataBind();

                        // Gán danh sách mục cho DropDownList
                        //ddlKhuVuc.DataTextField = "TenKVHC";
                        //ddlKhuVuc.DataValueField = "MaKVHC";
                        //ddlKhuVuc.DataSource = khuVucHCList;
                        //ddlKhuVuc.DataBind();

                        // Gán danh sách mục cho DropDownList
                        ddlThienTai.DataTextField = "TenLoaiThienTai";
                        ddlThienTai.DataValueField = "ID";
                        ddlThienTai.DataSource = thienTaiList;
                        ddlThienTai.DataBind();

                        // Gán danh sách mục cho DropDownList
                        ddlCapDo.DataTextField = "TenLoaiCapDo";
                        ddlCapDo.DataValueField = "ID";
                        ddlCapDo.DataSource = capDoList;
                        ddlCapDo.DataBind();

                        // Gán danh sách mục cho DropDownList
                        ddlLoaiBanTin.DataTextField = "TenLoaiBanTin";
                        ddlLoaiBanTin.DataValueField = "ID";
                        ddlLoaiBanTin.DataSource = banTinList;
                        ddlLoaiBanTin.DataBind();


                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            // Lấy giá trị MaKhuVuc từ DropDownList
            int maKhuVuc = int.Parse(ddlKhuVuc.SelectedValue);

            // Lấy giá trị MaKVHC từ DropDownList
            //int maKhuVucHC = int.Parse(ddlKhuVucHC.SelectedValue);

            // Lấy giá trị MaLoaiThienTai từ DropDownList
            int maLoaiThienTai = int.Parse(ddlThienTai.SelectedValue);

            // Lấy giá trị MaLoaiBanTin từ DropDownList
            int maLoaiBanTin = int.Parse(ddlLoaiBanTin.SelectedValue);

            // Lấy giá trị MaCapDoDuBao từ DropDownList
            int maCapDo = int.Parse(ddlCapDo.SelectedValue);

            // Thêm dữ liệu vào bảng BanTinDuBao
            using (TESTEntities dbContext = new TESTEntities())
            {
                var bantinDuBao = new BanTinDuBao
                {
                    TieuDe = Request.Form["txtTieuDe"],
                    TomTatNoiDung = Request.Form["txtTomTatNoiDung"],
                    //NoiDung = txtNoiDung.Text,
                    NoiDung = Request.Form["txtNoiDung"],
                    NgayThucHien = DateTime.Now,
                    MaLoaiBanTin = maLoaiBanTin,
                    MaLoaiThienTai = maLoaiThienTai,
                    MaKhuVuc = maKhuVuc,
                    //MaKVHC = maKhuVucHC,
                    MaCapDoDuBao = maCapDo,
                    TenToChucDuBao = txtToChucDuBao.Text,
                    NguoiDang = txtNguoiDang.Text,
                    NguonTinDuBao = txtNguonTinDuBao.Text,
                    KEY_SEARCH = txtKeySearch.Text 
                };
                dbContext.BanTinDuBaos.Add(bantinDuBao);
                dbContext.SaveChanges();

                //Thêm tệp đính kèm
                if (fileDinhKem.HasFile)
                {
                    // Lấy thông tin về tệp đính kèm
                    string fileName = fileDinhKem.FileName;
                    byte[] fileContent = fileDinhKem.FileBytes;
                    string path = Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data"), fileName);
                    fileDinhKem.SaveAs(path);

                    // Xác định kiểu của tệp đính kèm bằng cách sử dụng ClsFtp
                    string mimeType = ClsFtp.GetMimeTypeByFileName(fileName);

                    // Kiểm tra kiểu MIME để xác định xem tệp là hình ảnh hay video
                    bool IsImage = mimeType.StartsWith("image/");
                    bool IsVideo = mimeType.StartsWith("video/");

                    // Xác định loại tệp dựa trên IsImage và IsVideo
                    string folderToSave = IsImage ? "HinhAnh/" : (IsVideo ? "Video/" : "VanBan/");
                    var tapTin = new TepBanTinDinhKem
                        {
                            TenTapTin = fileName,
                            NgayDinhKem = DateTime.Now,
                            MaBanTinDuBao = bantinDuBao.ID,
                            IsHinhAnh = IsImage,
                            IsVideo = IsVideo,
                            MoTa = txtMoTa.Text,
                            DuongDan = "/FileUpload/" + folderToSave + fileName // Đường dẫn đến tệp trên máy chủ FTP
                        };
                        bantinDuBao.TepBanTinDinhKems.Add(tapTin);
                        dbContext.TepBanTinDinhKems.Add(tapTin);
                        dbContext.SaveChanges();

                   
                    // Tải lên máy chủ FTP
                    string ten_FolderSaveToFTP = "/FileUpload/" + folderToSave;
                    bool createFolder = ClsFtp.CreateFolder(urlHost, ten_FolderSaveToFTP, UserName, Password);
                    if (createFolder)
                    {
                        bool fileUploaded = ClsFtp.UploadFile(urlHost, ten_FolderSaveToFTP, fileName, path, UserName, Password);
                    }
                    File.Delete(path);

                    // Hiển thị thông báo thành công
                    string successMessage = "Đăng tải bản tin thành công.";
                    lblMessage.Text = successMessage;
                    ClientScript.RegisterStartupScript(this.GetType(), "ShowModalScript", "showModal();", true);
                }
                else
                {
                    string errorMessage = "Đăng tải bản tin thành công (Không đính kèm files).";
                    lblMessage.Text = errorMessage;
                    ClientScript.RegisterStartupScript(this.GetType(), "ShowModalScript", "showModal();", true);
                }
            }
        }

        protected void btnQuayLai_Click(object sender, EventArgs e)
        {
            // Chuyển hướng về trang chủ "DichVuChiaSeDuLieuKTTV.aspx"
            Response.Redirect("DichVuChiaSeDuLieuKTTV.aspx");
        }


    }
}