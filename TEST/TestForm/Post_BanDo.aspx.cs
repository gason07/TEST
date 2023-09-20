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
    public partial class Post_BanDo : System.Web.UI.Page
    {
        string urlHost = ClsFtp.GetHostFTP();
        string UserName = ClsFtp.GetUserNameFTP();
        string Password = ClsFtp.GetPassWordFTP();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            using (TESTEntities dbContext = new TESTEntities())
            {
                if (fileDinhKem.HasFile)
                {
                    // Lấy thông tin về tệp đính kèm
                    string fileName = fileDinhKem.FileName;
                    byte[] fileContent = fileDinhKem.FileBytes;
                    string path = Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data"), fileName);
                    fileDinhKem.SaveAs(path);
                    int max_ID = dbContext.BanDoes.Max(p => p.ID) + 1;

                    var banDo = new BanDo
                    {
                        TenBanDo = txtTenBanDo.Text,
                        TenTapTin = fileName,
                        DuongDanURL = "/BanDo/" + max_ID.ToString() + @"/" + fileName,// Đường dẫn đến tệp trên máy chủ FTP
                        MoTa = txtMoTa.Text,
                        ThoiGianCapNhat = DateTime.Now,
                    };
                    dbContext.BanDoes.Add(banDo);
                    dbContext.SaveChanges();


                    // Tải lên máy chủ FTP
                    string ten_FolderSaveToFTP = "/BanDo/";
                    bool createFolder = ClsFtp.CreateFolder(urlHost, ten_FolderSaveToFTP, UserName, Password);
                    if (createFolder)
                    {
                        bool fileUploaded = ClsFtp.UploadFile(urlHost, ten_FolderSaveToFTP, fileName, path, UserName, Password);
                    }
                    File.Delete(path);

                    // Hiển thị thông báo thành công
                    string successMessage = "Đăng tải thành công.";
                    lblMessage.Text = successMessage;
                    ClientScript.RegisterStartupScript(this.GetType(), "ShowModalScript", "showModal();", true);
                }
                else
                {
                    string errorMessage = "Đăng tải không thành công.";
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