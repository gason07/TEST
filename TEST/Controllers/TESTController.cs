using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TEST.DataAccess;
using TEST.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Web.Http.Description;
using System.Web;
using Newtonsoft.Json;
using System.Data.Entity;
using TEST.Utilities;
using System.Globalization;

namespace TEST.Controllers
{
    public class TESTController : ApiController
    {
        TESTEntities dbContext = new TESTEntities();

        //    //METHOD: GET
        //    //-----------GET All List
        //public IEnumerable<Employees1> Get() //truy van ta ca du lieu tu database cua sql
        //{
        //    using (TESTEntities dbContext = new TESTEntities())
        //    {
        //        return dbContext.Employees1.ToList();
        //    }
        //}

        //Upload Ảnh cùng với các thông tin liên quan(ĐÚNG)
        //[HttpPost]
        //public IHttpActionResult PostWithImage()
        //{

        //    //Xử lý ảnh
        //    try
        //    {
        //        var httpRequest = HttpContext.Current.Request;
        //        string nameCompany = httpRequest.Form["nameCompany"];
        //        string addressCompany = httpRequest.Form["addressCompany"];
        //        if (httpRequest.Files.Count > 0)
        //        {
        //            foreach (string file in httpRequest.Files)
        //            {
        //                var postedFile = httpRequest.Files[file];
        //                var fileName = postedFile.FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault();
        //                var filePath = HttpContext.Current.Server.MapPath("/TestImage/" + fileName);
        //                postedFile.SaveAs(filePath);

        //                //Lưu thông tin cùng với đường dẫn vào DB
        //                using (TESTEntities dbContext = new TESTEntities())
        //                {
        //                    var cp = new CompanyST()
        //                    {
        //                        NameCompany = nameCompany,
        //                        AddressCompany = addressCompany,
        //                        ThoiGianCapNhat = DateTime.Now,
        //                        AddressURL = filePath
        //                    };

        //                    dbContext.CompanySTs.Add(cp);
        //                    dbContext.SaveChanges();
        //                }
        //                return Ok("Success.");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //    return BadRequest("No Success.");

        //}


        ////Xem Ảnh(ĐÚNG)
        //[HttpGet]
        //[Route("api/TEST/images/{imageId}")]
        //public HttpResponseMessage GetImage(int imageId)
        //{
        //    try
        //    {
        //        var imageData = dbContext.CompanySTs.FirstOrDefault(image => image.ID == imageId);
        //        if (imageData != null)
        //        {
        //            // Đọc dữ liệu ảnh từ đường dẫn và tạo phản hồi với dữ liệu ảnh
        //            byte[] imageBytes = File.ReadAllBytes(imageData.AddressURL);
        //            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
        //            response.Content = new ByteArrayContent(imageBytes);
        //            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg"); // Đổi loại ảnh nếu cần
        //            return response;
        //        }
        //        else
        //        {
        //            return Request.CreateResponse(HttpStatusCode.NotFound, "Image not found.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}


        ////Xem thông tin cùng với ảnh ở dạng Binary
        //[HttpGet]
        //[Route("api/images/{imageId}")]
        //public IHttpActionResult GetImageAndData(int imageId)
        //{
        //    try
        //    {
        //        var imageData = dbContext.CompanySTs.FirstOrDefault(image => image.ID == imageId);
        //        if (imageData != null)
        //        {
        //            // Đọc dữ liệu ảnh từ đường dẫn, ảnh ở dạng binary
        //            byte[] imageBytes = File.ReadAllBytes(imageData.AddressURL);

        //            // Tạo đối tượng CompanyImage để chứa thông tin ảnh và dữ liệu ảnh
        //            var responseImageData = new CompanyImage
        //            {
        //                ID = imageData.ID,
        //                NameCompany = imageData.NameCompany,
        //                AddressCompany = imageData.AddressCompany,
        //                AddressURL = imageData.AddressURL,
        //                ThoiGianCapNhat = DateTime.Now,
        //                Image = imageBytes
        //            };

        //            return Ok(responseImageData); // Trả về dữ liệu ảnh và thông tin
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}

       [HttpGet]
       public List<AnhPhim> GetTatCaThongTinPhimAnh()
        {
            var data = dbContext.PhimHinhAnhs.Where(s => s.IsDeleted == false)
                .Select(sp => new AnhPhim
                {
                    ID = sp.ID,
                    TieuDe = sp.TieuDe,
                }).OrderBy(s => s.ID);
            return data.ToList();
        }

        //Xem bản đồ
        [HttpGet]
        [Route("api/TEST/Share/ThongTinBao/BanDo/{idbd}")]
        public HttpResponseMessage GetImage(int idbd)
        {
            try
            {
                var mapData = dbContext.BanDoes.FirstOrDefault(map => map.ID == idbd && map.IsDeleted == false);
                if (mapData != null)
                {
                    // Đọc dữ liệu ảnh từ đường dẫn và tạo phản hồi với dữ liệu ảnh
                    byte[] mapBytes = File.ReadAllBytes(mapData.DuongDanURL);
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new ByteArrayContent(mapBytes);
                    response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg"); // Đổi loại ảnh nếu cần
                    return response;
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Image not found.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //Xem thông tin chi tiết
        [HttpGet]
        public List<Maps> LayThongTinBanDoTheoID(int idbd)
        {
            var bandos = dbContext.BanDoes.Where(map => map.ID == idbd && map.IsDeleted == false)
            .Select(sp => new Maps
            {
                ID = sp.ID,
                TenBanDo = sp.TenBanDo,
                //LoaiBanDo = sp.LoaiBanDo,
                TenTapTin = sp.TenTapTin,
                MoTa = sp.MoTa,
                DuongDan = sp.DuongDanURL,
                ThoiGianCapNhat = sp.ThoiGianCapNhat
            }).OrderBy(s => s.ID);
            return bandos.ToList();
        }

        //[HttpPost]
        //[Route("api/Share/PhimAnh")]
        //public IHttpActionResult PostWithPhimAnh()
        //{
        //    try
        //    {

        //        var httpRequest = HttpContext.Current.Request;
        //        string tieuDe = httpRequest.Form["tieuDe"];
        //        string tenTapTin = httpRequest.Form["tenTapTin"];
        //        string moTa = httpRequest.Form["moTa"];
        //        //string testName = null;
        //        bool isDeleted;
        //        bool isDeletedResult = bool.TryParse(httpRequest.Form["isDeleted"], out isDeleted);
        //        bool isVideo;
        //        bool isVideoResult = bool.TryParse(httpRequest.Form["isVideo"], out isVideo);
        //        bool isImage;
        //        bool isImageResult = bool.TryParse(httpRequest.Form["isImage"], out isImage);
        //        if (httpRequest.Files.Count > 0)
        //        {
        //            foreach (string file in httpRequest.Files)
        //            {
        //                var postedFile = httpRequest.Files[file];
        //                var fileName = postedFile.FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault();
        //                var filePath = HttpContext.Current.Server.MapPath("/PhimAnhBanDo/" + fileName);
        //                postedFile.SaveAs(filePath);


        //                //Lưu thông tin cùng với đường dẫn vào DB
        //                using (TESTEntities dbContext = new TESTEntities())
        //                {
        //                    var cp = new PhimHinhAnh()
        //                    {
        //                        TieuDe = tieuDe,
        //                        NgayThang = DateTime.Today,
        //                        TenTapTin = tenTapTin,
        //                        DuongDanURL = "/PhimAnhBanDo/" + fileName,
        //                        MoTa = moTa,
        //                        ThoiGianCapNhat = DateTime.Now,
        //                        IsDeleted = isDeleted,
        //                        IsImage = isImage,
        //                        IsVideo = isVideo
        //                    };

        //                    dbContext.PhimHinhAnhs.Add(cp);
        //                    dbContext.SaveChanges();
        //                }
        //                return Ok("Success.");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //    return BadRequest("No Success.");

        //}

        [HttpGet]
        public IHttpActionResult GetImageAndData(int idpa)
        {
            try
            {
                var videoimageData = dbContext.PhimHinhAnhs.FirstOrDefault(pa => pa.ID == idpa);
                if (videoimageData != null)
                {
                    // Đọc dữ liệu ảnh, video
                    //byte[] videoimageBytes = File.ReadAllBytes(videoimageData.DuongDanURL);

                    // Tạo đối tượng CompanyImage để chứa thông tin ảnh và dữ liệu ảnh
                    var responseImageData = new AnhPhim
                    {
                        ID = videoimageData.ID,
                        TieuDe = videoimageData.TieuDe,
                        NgayThang = DateTime.Today,
                        TenTapTin = videoimageData.TenTapTin,
                        MoTa = videoimageData.MoTa,
                        ThoiGianCapNhat = DateTime.Now,
                        DuongDanURL = videoimageData.DuongDanURL,
                        //View = videoimageBytes
                    };

                    return Ok(responseImageData); // Trả về dữ liệu ảnh và thông tin
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //public async Task<string> PostWithImage()//CompanyST cp)
        //{
        //    System.Collections.Specialized.NameValueCollection form = HttpContext.Current.Request.Form;
        //    string NameCompany = form["NameCompany"];
        //    string AddressCompany = form["AddressCompany"];
        //    string filePath = null;
        //    Dictionary<string, object> dict = new Dictionary<string, object>();
        //    try
        //    {
        //        var httpRequest = HttpContext.Current.Request;
        //        if (httpRequest.Files.Count > 0)
        //        {
        //            foreach (string file in httpRequest.Files)
        //            {
        //                var postedFile = httpRequest.Files[file];
        //                var fileName = postedFile.FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault();
        //                filePath = HttpContext.Current.Server.MapPath("/Image/" + fileName);
        //                postedFile.SaveAs(filePath);
        //                CompanyST cp = new CompanyST();
        //                cp.AddressURL = "/Image/" + fileName;
        //                cp.ThoiGianCapNhat = DateTime.Now;
        //                cp.NameCompany = NameCompany;
        //                cp.AddressCompany = AddressCompany;
        //                dbContext.CompanySTs.Add(cp);
        //                dbContext.SaveChanges();
        //                return "/Image/" + fileName;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //    return "No Files";

        //}

        //Lay ban do
        [HttpGet]
        public HttpResponseMessage LayTepDinhKemCuaBanDo(int idtbd)
        {
            TESTEntities dbContext = new TESTEntities();
            var banDoDinhKem = dbContext.BanDoes.FirstOrDefault(bd => bd.ID == idtbd && bd.IsDeleted == false);
            if (banDoDinhKem == null)
            {
                //return new HttpResponseMessage(HttpStatusCode.NotFound);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Không tìm thấy bản đồ đính kèm.");
                return response;
            }
            string urlHost = ClsFtp.GetHostFTP();
            string UserName = ClsFtp.GetUserNameFTP();
            string Password = ClsFtp.GetPassWordFTP();
            string folderPath = "/BanDo/";
            var ftpRequest = (FtpWebRequest)WebRequest.Create(ClsFtp.GetHostFTP() + folderPath + banDoDinhKem.TenTapTin);
            ftpRequest.Credentials = new NetworkCredential(ClsFtp.GetUserNameFTP(), ClsFtp.GetPassWordFTP());
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
            var ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            if (ftpResponse.StatusCode != FtpStatusCode.CommandOK && ftpResponse.StatusCode != FtpStatusCode.ClosingData)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.Content = new StringContent("Lỗi khi kết nối đến FTP hoặc không tìm thấy thư mục trên FTP.");
                return errorResponse;
            }
            var ftpStream = ftpResponse.GetResponseStream();
            var contentType = MimeMapping.GetMimeMapping(banDoDinhKem.TenTapTin);
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(ftpStream)
            };
            result.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = banDoDinhKem.TenTapTin
                };
            result.Content.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
            return result;
        }

        //LayAnh
        [HttpGet]
        public HttpResponseMessage LayAnhDinhKemCuaPhimAnh(int idpa)
        {
            TESTEntities dbContext = new TESTEntities();
            var anhDinhKem = dbContext.PhimHinhAnhs.FirstOrDefault(s => s.ID == idpa && s.IsDeleted == false);
            if (anhDinhKem == null)
            {
                //return new HttpResponseMessage(HttpStatusCode.NotFound);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new StringContent("Không tìm thấy phim/ảnh đính kèm.");
                return response;
            }
            string urlHost = ClsFtp.GetHostFTP();
            string UserName = ClsFtp.GetUserNameFTP();
            string Password = ClsFtp.GetPassWordFTP();
            string folderPath = "/PhimAnh/";
            var ftpRequest = (FtpWebRequest)WebRequest.Create(ClsFtp.GetHostFTP() + folderPath + anhDinhKem.TenTapTin);
            ftpRequest.Credentials = new NetworkCredential(ClsFtp.GetUserNameFTP(), ClsFtp.GetPassWordFTP());
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
            var ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            if (ftpResponse.StatusCode == FtpStatusCode.CommandOK && ftpResponse.StatusCode != FtpStatusCode.ClosingData)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                errorResponse.Content = new StringContent("Lỗi khi kết nối đến FTP hoặc không tìm thấy thư mục trên FTP.");
                return errorResponse;
            }
            var ftpStream = ftpResponse.GetResponseStream();
            var contentType = MimeMapping.GetMimeMapping(anhDinhKem.TenTapTin);
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(ftpStream)
            };
            result.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = anhDinhKem.TenTapTin
                };
            result.Content.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
            return result;
        }

        //[HttpGet]
        //public HttpResponseMessage LayAnhTuFTP(int idpa)
        //{
        //    try
        //    {
        //        using (TESTEntities dbContext = new TESTEntities())
        //        {
        //            var phimHinhAnh = dbContext.PhimHinhAnhs.FirstOrDefault(p => p.ID == idpa);
        //            if (phimHinhAnh == null)
        //            {
        //                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Không tìm thấy ảnh.");
        //            }

        //            string urlHost = ClsFtp.GetHostFTP();
        //            string UserName = ClsFtp.GetUserNameFTP();
        //            string Password = ClsFtp.GetPassWordFTP();

        //            string folderPath = "/PhimHinhAnh/";
        //            string ftpPath = phimHinhAnh.TenTapTin;

        //            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ClsFtp.GetHostFTP()+ folderPath + ftpPath);
        //            ftpRequest.Credentials = new NetworkCredential(ClsFtp.GetUserNameFTP(), ClsFtp.GetPassWordFTP());
        //            ftpRequest.UseBinary = true;
        //            ftpRequest.UsePassive = true;
        //            ftpRequest.KeepAlive = true;
        //            ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;

        //            FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
        //            Stream ftpStream = ftpResponse.GetResponseStream();

        //            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
        //            {
        //                Content = new StreamContent(ftpStream)
        //            };

        //            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
        //            {
        //                FileName = phimHinhAnh.TenTapTin
        //            };

        //            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg"); // Điều chỉnh loại dữ liệu tương ứng

        //            return response;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}

        [HttpGet]
        public List<FullNews> TatCaBanTinDuBao()
        {
            var data = dbContext.BanTinDuBaos.Where(s => s.IsDeleted == false)
                .Select(sp => new FullNews
                {
                    ID = sp.ID,
                    TieuDe = sp.TieuDe,
                    NgayThucHien = sp.NgayThucHien,
                }).OrderBy(s => s.ID);
            return data.ToList();
        }

        [HttpGet]
        public List<FullNews> LayBanTinTheoTieuDe(string tieuDe)
        {
            // Chuyển tiêu đề thành chữ thường và loại bỏ khoảng trống ở cả hai đầu
            tieuDe = tieuDe.Trim().ToLower();
            // Truy vấn
            var data = dbContext.BanTinDuBaos.Where(s => s.TieuDe.ToLower().Trim() == tieuDe && s.IsDeleted == false)
                .Select(sp => new FullNews
                {
                    ID = sp.ID,
                    TieuDe = sp.TieuDe,
                    NgayThucHien = sp.NgayThucHien,
                }).OrderBy(s => s.ID);
            return data.ToList();
        }

    [HttpPost]
        public IHttpActionResult UploadBanTinDuBao()
        {
            try
            {
                // Lấy thông tin từ request form
                System.Collections.Specialized.NameValueCollection form = HttpContext.Current.Request.Form;
                string TieuDe = form["TieuDe"];
                string TomTatNoiDung = form["TomTatNoiDung"];
                string NoiDung = form["NoiDung"];
                string NguoiDang = form["NguoiDang"];
                string TenToChucDuBao = form["TenToChucDuBao"];
                string NguonTinDuBao = form["NguonTinDuBao"];
                string KEY_SEARCH = form["KEY_SEARCH"];
                int MaCapDoDuBao;
                int.TryParse(form["MaCapDoDuBao"], out MaCapDoDuBao);
                int MaKhuVuc;
                int.TryParse(form["MaKhuVuc"], out MaKhuVuc);
                int MaKVHC;
                int.TryParse(form["MaKVHC"], out MaKVHC);
                int MaLoaiThienTai;
                int.TryParse(form["MaLoaiThienTai"], out MaLoaiThienTai);
                int MaLoaiBanTin;
                int.TryParse(form["MaLoaiBanTin"], out MaLoaiBanTin);
                int ThoiGianDuBao;
                int.TryParse(form["ThoiGianDuBao"], out ThoiGianDuBao);
                bool TrangThaiDuyet;
                bool TrangThaiDuyetResult = bool.TryParse(form["TrangThaiDuyet"], out TrangThaiDuyet);
                //bool IsVideo;
                //bool IsVideoResult = bool.TryParse(form["IsVideo"], out IsVideo);
                //bool IsHinhAnh;
                //bool IsImageResult = bool.TryParse(form["IsHinhAnh"], out IsHinhAnh);
                string urlHost = ClsFtp.GetHostFTP();
                string UserName = ClsFtp.GetUserNameFTP();
                string Password = ClsFtp.GetPassWordFTP();

                // Thực hiện lưu thông tin và tệp đính kèm vào cơ sở dữ liệu
                using (TESTEntities dbContext = new TESTEntities())
                {
                    var bantinDuBao = new BanTinDuBao
                    {
                        TieuDe = TieuDe,
                        TomTatNoiDung = TomTatNoiDung,
                        NoiDung = NoiDung,
                        NguoiDang = NguoiDang,
                        MaCapDoDuBao = MaCapDoDuBao,
                        MaKhuVuc = MaKhuVuc,
                        MaKVHC = MaKVHC,
                        MaLoaiThienTai = MaLoaiThienTai,
                        MaLoaiBanTin = MaLoaiBanTin,
                        TenToChucDuBao = TenToChucDuBao,
                        NgayThucHien = DateTime.Now,
                        ThoiGianDuBao = ThoiGianDuBao,
                        TrangThaiDuyet = TrangThaiDuyet,
                        NguonTinDuBao = NguonTinDuBao,
                        BanTin_Status = true,
                        IsDeleted = false,
                        KEY_SEARCH = KEY_SEARCH

                    };
                    dbContext.BanTinDuBaos.Add(bantinDuBao);
                    dbContext.SaveChanges();

                    // Thêm tệp đính kèm (File_DK)

                    for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                    {
                        var filepath = HttpContext.Current.Request.Files.Count > 0 ?
                        HttpContext.Current.Request.Files[i] : null;
                        if (filepath != null && filepath.ContentLength > 0)
                        {
                            string fileName = Path.GetFileName(filepath.FileName);
                            string path = Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data"), fileName);
                            filepath.SaveAs(path);

                            // Xác định kiểu của tệp đính kèm bằng cách sử dụng ClsFtp
                            string mimeType = ClsFtp.GetMimeTypeByFileName(fileName);

                            // Kiểm tra kiểu MIME để xác định xem tệp là hình ảnh hay video
                            bool IsImage = mimeType.StartsWith("image/");
                            bool IsVideo = mimeType.StartsWith("video/");


                            // Lưu tệp đính kèm vào cơ sở dữ liệu và thư mục FTP
                            string folderToSave = IsImage ? "HinhAnh/" : (IsVideo ? "Video/" : "VanBan/");
                            var tapTinDinhKem = new TepBanTinDinhKem
                            {
                                TenTapTin = filepath.FileName,
                                MaBanTinDuBao = bantinDuBao.ID,
                                DuongDan = "/FileUpload/" + folderToSave + fileName,
                                NgayDinhKem = DateTime.Now,
                                IsHinhAnh = IsImage,
                                IsVideo = IsVideo
                            };
                            dbContext.TepBanTinDinhKems.Add(tapTinDinhKem);
                            bantinDuBao.TepBanTinDinhKems.Add(tapTinDinhKem);
                            dbContext.SaveChanges();
                            // Lưu tệp đính kèm lên thư mục FTP

                            string ten_FolderSaveToFTP = "/FileUpload/" + folderToSave;
                            bool createFolder = ClsFtp.CreateFolder(urlHost, ten_FolderSaveToFTP, UserName, Password);
                            if (createFolder)
                            {
                                bool fileUploaded = ClsFtp.UploadFile(urlHost, ten_FolderSaveToFTP, fileName, path, UserName, Password);
                            }
                            File.Delete(path);
                        }
                        else
                        {
                            return Ok("Không có files đính kèm. Bản tin đã thêm thành công");
                        }
                    }
                }

                return Ok("Bản tin đã thêm thành công");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        public List<DuBaoBanTin> LayChiTietBanTinDuBaoTheoID(int idbt)
        {
            var bantins = dbContext.BanTinDuBaos.Where(s => s.ID == idbt && s.IsDeleted == false)
                .Select(sp => new DuBaoBanTin
                {
                    ID = sp.ID,
                    TieuDe = sp.TieuDe,
                    NoiDung = sp.NoiDung,
                    NgayThucHien = sp.NgayThucHien,
                    NguoiThucHien = sp.NguoiDang,
                    //TenBanTin = sp.LoaiBanTin.TenLoaiBanTin,
                    //ThongTinDiaDiem = sp.KhuVucHanhChinh.TenKVHC,
                    KhuVucDuBao = sp.KhuVuc.TenKhuVuc,
                    //TuThoiGian = sp.ThoiHanDuBao.TuThoiGian,
                    //DenThoiGian = sp.ThoiHanDuBao.DenThoiGian,
                    //CapDoDuBao = sp.CapDo.TenLoaiCapDo,
                    //LoaiThienTai = sp.LoaiThienTai.TenLoaiThienTai,
                    TenToChucDuBao = sp.TenToChucDuBao,
                    TrangThaiDuyet = sp.TrangThaiDuyet,
                    TepDinhKem = sp.TepBanTinDinhKems.Select(fd => new File_DK
                    {
                        ID = fd.ID,
                        TenTapTin = fd.TenTapTin,
                        DuongDanURL = fd.DuongDan,
                        MoTa = fd.MoTa,
                        NgayDinhKem = fd.NgayDinhKem,
                        IsImage = fd.IsHinhAnh,
                        IsVideo = fd.IsVideo
                    }).ToList()
                }).OrderBy(s => s.ID);
            return bantins.ToList();
        }



        [HttpGet]
        public HttpResponseMessage LayTepDinhKemCuaBanTin(int idbt)
        {
            TESTEntities dbContext = new TESTEntities();
            var tepDinhKem = dbContext.TepBanTinDinhKems.FirstOrDefault(s => s.MaBanTinDuBao == idbt && s.IsDeleted == false);
            string urlHost = ClsFtp.GetHostFTP();
            string UserName = ClsFtp.GetUserNameFTP();
            string Password = ClsFtp.GetPassWordFTP();
            string folderPath = "/FileUpload/" ;
            if (tepDinhKem == null)
            {
                throw new Exception("Không tìm thấy tệp đính kèm.");
            }
            if (tepDinhKem.IsHinhAnh == true)
            {
                folderPath += "HinhAnh/";
            }
            else if (tepDinhKem.IsVideo == true)
            {
                folderPath += "Video/";
            }
            else
            {
                folderPath += "VanBan/";
            }
            var ftpRequest = (FtpWebRequest)WebRequest.Create(ClsFtp.GetHostFTP() + folderPath + tepDinhKem.TenTapTin);
            ftpRequest.Credentials = new NetworkCredential(ClsFtp.GetUserNameFTP(), ClsFtp.GetPassWordFTP());
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
            var ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            var ftpStream = ftpResponse.GetResponseStream();
            var contentType = MimeMapping.GetMimeMapping(tepDinhKem.TenTapTin);
            
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(ftpStream)
            };
            result.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = tepDinhKem.TenTapTin
                };
            result.Content.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
            return result;
        }


        //---------METHOD: PUT----------

        /// <summary>
        /// Dịch vụ cập nhật bản tin dự báo theo ID
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult CapNhatDuLieuBanTinDuBao(int idubt)
        {
            try
            {
                // Lấy thông tin từ request form
                System.Collections.Specialized.NameValueCollection form = HttpContext.Current.Request.Form;
                string TieuDe = form["TieuDe"];
                string TomTatNoiDung = form["TomTatNoiDung"];
                string NoiDung = form["NoiDung"];
                string NguoiDang = form["NguoiDang"];
                string TenToChucDuBao = form["TenToChucDuBao"];
                string NguonTinDuBao = form["NguonTinDuBao"];
                string KEY_SEARCH = form["KEY_SEARCH"];
                int MaCapDoDuBao;
                int.TryParse(form["MaCapDoDuBao"], out MaCapDoDuBao);
                int MaKhuVuc;
                int.TryParse(form["MaKhuVuc"], out MaKhuVuc);
                int MaKVHC;
                int.TryParse(form["MaKVHC"], out MaKVHC);
                int MaLoaiThienTai;
                int.TryParse(form["MaLoaiThienTai"], out MaLoaiThienTai);
                int MaLoaiBanTin;
                int.TryParse(form["MaLoaiBanTin"], out MaLoaiBanTin);
                int ThoiGianDuBao;
                int.TryParse(form["ThoiGianDuBao"], out ThoiGianDuBao);
                bool TrangThaiDuyet;
                bool.TryParse(form["TrangThaiDuyet"], out TrangThaiDuyet);
                string urlHost = ClsFtp.GetHostFTP();
                string UserName = ClsFtp.GetUserNameFTP();
                string Password = ClsFtp.GetPassWordFTP();

                // Thực hiện cập nhật thông tin vào cơ sở dữ liệu
                using (TESTEntities dbContext = new TESTEntities())
                {
                    var bantinDuBao = dbContext.BanTinDuBaos.Include("TepBanTinDinhKems").FirstOrDefault(bt => bt.ID == idubt && bt.IsDeleted == false);

                    if (bantinDuBao == null)
                    {
                        return BadRequest("Không tồn tại bản tin để cập nhật.");
                    }

                    //Xoá tệp đính kèm cũ của bản tin trước khi cập nhật tệp đính kèm mới
                    dbContext.TepBanTinDinhKems.RemoveRange(bantinDuBao.TepBanTinDinhKems);

                    bantinDuBao.TieuDe = TieuDe;
                    bantinDuBao.TomTatNoiDung = TomTatNoiDung;
                    bantinDuBao.NoiDung = NoiDung;
                    bantinDuBao.NguoiDang = NguoiDang;
                    bantinDuBao.MaCapDoDuBao = MaCapDoDuBao;
                    bantinDuBao.MaKhuVuc = MaKhuVuc;
                    bantinDuBao.MaKVHC = MaKVHC;
                    bantinDuBao.MaLoaiThienTai = MaLoaiThienTai;
                    bantinDuBao.MaLoaiBanTin = MaLoaiBanTin;
                    bantinDuBao.TenToChucDuBao = TenToChucDuBao;
                    bantinDuBao.NgayThucHien = DateTime.Now;
                    bantinDuBao.ThoiGianDuBao = ThoiGianDuBao;
                    bantinDuBao.TrangThaiDuyet = TrangThaiDuyet;
                    bantinDuBao.NguonTinDuBao = NguonTinDuBao;
                    bantinDuBao.BanTin_Status = true;
                    bantinDuBao.KEY_SEARCH = KEY_SEARCH;

                    dbContext.SaveChanges();

                    // Cập nhật tệp đính kèm (Attachments)
                    foreach (string fileKey in HttpContext.Current.Request.Files)
                    {
                        var filepath = HttpContext.Current.Request.Files[fileKey];
                        if (filepath != null && filepath.ContentLength > 0)
                        {

                            string fileName = Path.GetFileName(filepath.FileName);
                            string path = Path.Combine(HttpContext.Current.Server.MapPath("~/FileUpload"), fileName);
                            filepath.SaveAs(path);

                            // Xác định kiểu của tệp đính kèm bằng cách sử dụng ClsFtp
                            string mimeType = ClsFtp.GetMimeTypeByFileName(fileName);

                            // Kiểm tra kiểu MIME để xác định xem tệp là hình ảnh hay video
                            bool IsImage = mimeType.StartsWith("image/");
                            bool IsVideo = mimeType.StartsWith("video/");

                            // Lưu tệp đính kèm vào cơ sở dữ liệu và thư mục FTP
                            //Nếu là hình ảnh thì lưu vào thư mục hình ảnh, video thì lưu vào mục video
                            string folderToSave = IsImage ? "HinhAnh/" : (IsVideo ? "Video/" : "VanBan/");
                            var tapTinDinhKem = new TepBanTinDinhKem
                                {
                                    TenTapTin = filepath.FileName,
                                    MaBanTinDuBao = bantinDuBao.ID,
                                    DuongDan = "/FileUpload/" + folderToSave + fileName,
                                    NgayDinhKem = DateTime.Now,
                                    IsHinhAnh = IsImage,
                                    IsVideo = IsVideo
                                };
                                bantinDuBao.TepBanTinDinhKems.Add(tapTinDinhKem);
                                dbContext.SaveChanges();
                            
                            // Lưu tệp đính kèm lên thư mục FTP
                            string ten_FolderSaveToFTP = "/FileUpload/" + folderToSave;
                            bool createFolder = ClsFtp.CreateFolder(urlHost, ten_FolderSaveToFTP, UserName, Password);
                            if (createFolder)
                            {
                                bool fileUploaded = ClsFtp.UploadFile(urlHost, ten_FolderSaveToFTP, fileName, path, UserName, Password);
                            }
                            File.Delete(path); 
                        }
                        else
                        {
                            return Ok("Không có files đính kèm. Bản tin đã được cập nhật thành công.");
                        }
                    }
                }
                return Ok("Bản tin đã được cập nhật thành công.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }





        //
        //----METHOD: DELETE----
        //

        /// <summary>
        /// Dịch vụ xoá bản tin theo ID
        /// </summary>
        /// <returns></returns>
        /// [HttpDelete]
        [HttpDelete]
        public IHttpActionResult XoaBanTinDuBaoTheoID(int idbt)
        {
            try
            {
                var banTin = dbContext.BanTinDuBaos.FirstOrDefault(bt => bt.ID == idbt && bt.IsDeleted == false);

                if (banTin == null)
                {
                    return BadRequest("Bản tin không tồn tại hoặc đã bị xoá"); // Không tìm thấy bản tin cần xóa hoặc đã bị xóa trước đó.
                }

                foreach (var tep in banTin.TepBanTinDinhKems)
                {
                    // Thực hiện xóa tệp đính kèm ở đây
                    tep.IsDeleted = true;
                }

                banTin.IsDeleted = true; // Đánh dấu bản tin đã bị xóa.

                dbContext.SaveChanges();

                return Ok("Bản tin với ID = " + idbt.ToString() + " đã được xoá"); // Trả về mã thành công nếu xóa thành công.
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        public IHttpActionResult XoaPhimAnhTheoID(int idpa)
        {
            try
            {
                var banTin = dbContext.PhimHinhAnhs.FirstOrDefault(bt => bt.ID == idpa && bt.IsDeleted == false);

                if (banTin == null)
                {
                    return BadRequest("Dữ liệu không tồn tại hoặc đã bị xoá"); // Không tìm thấy bản tin cần xóa hoặc đã bị xóa trước đó.
                }

                banTin.IsDeleted = true; // Đánh dấu bản tin đã bị xóa.

                dbContext.SaveChanges();

                return Ok("Dữ liệu phim ảnh với ID = " + idpa.ToString() + " đã được xoá"); // Trả về mã thành công nếu xóa thành công.
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }



        //[HttpPost]
        //public IHttpActionResult ThemDuLieuPhimHinhAnh()
        //{
        //    try
        //    {
        //        System.Collections.Specialized.NameValueCollection form = HttpContext.Current.Request.Form;
        //        string TieuDe = form["TieuDe"];
        //        string TenTapTin = form["TenTapTin"];
        //        string MoTa = form["MoTa"];
        //        string path = null;
        //        string fileName = null;
        //        string ngayThangStr = form["NgayThang"];
        //        DateTime? NgayThang = null;
        //        if (!string.IsNullOrEmpty(ngayThangStr))
        //        {
        //            NgayThang = ClsFunctionDate.ConvertDate(ngayThangStr);
        //        }
        //        string urlHost = ClsFtp.GetHostFTP();
        //        string UserName = ClsFtp.GetUserNameFTP();
        //        string Password = ClsFtp.GetPassWordFTP();
        //        bool IsVideo;
        //        bool IsVideoResult = bool.TryParse(form["IsVideo"], out IsVideo);
        //        bool IsImage;
        //        bool IsImageResult = bool.TryParse(form["IsImage"], out IsImage);
        //        for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
        //        {
        //            var filepath = HttpContext.Current.Request.Files.Count > 0 ?
        //            HttpContext.Current.Request.Files[i] : null;
        //            if (filepath != null && filepath.ContentLength > 0)
        //            {
        //                fileName = Path.GetFileName(filepath.FileName);
        //                path = Path.Combine(HttpContext.Current.Server.MapPath("~/PhimHinhAnh"), fileName);
        //                filepath.SaveAs(path);
        //                int max_ID = dbContext.PhimHinhAnhs.Max(p => p.ID) + 1;
        //                string FileName = System.IO.Path.GetFileName(filepath.FileName);
        //                string FileTest = System.IO.Path.GetFullPath(filepath.FileName);
        //                using (TESTEntities dbContext = new TESTEntities())
        //                {
        //                    var cp = new PhimHinhAnh()
        //                    {
        //                        TieuDe = TieuDe,
        //                        NgayThang = NgayThang,
        //                        TenTapTin = fileName,
        //                        DuongDanURL = "/PhimHinhAnh/" + max_ID.ToString() + @"/" + fileName,
        //                        MoTa = MoTa,
        //                        ThoiGianCapNhat = DateTime.Now,
        //                        IsDeleted = false,
        //                        IsImage = IsImage,
        //                        IsVideo = IsVideo
        //                    };

        //                    string ten_FolderSaveToFTP = "PhimHinhAnh";
        //                    bool createFolder = ClsFtp.CreateFolder(urlHost, ten_FolderSaveToFTP, UserName, Password);
        //                    if (createFolder)
        //                    {
        //                        bool fileUploaded = ClsFtp.UploadFile(urlHost, ten_FolderSaveToFTP, fileName, path, UserName, Password);
        //                    }
        //                    dbContext.PhimHinhAnhs.Add(cp);
        //                    dbContext.SaveChanges();
        //                    File.Delete(path);
        //                }
        //            }
        //            else
        //            {
        //                return BadRequest("Vui lòng chọn files để thêm");
        //            }
        //        }
        //        return Ok("Dữ liệu đã được thêm thành công");
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}

        [HttpPost]
        public IHttpActionResult ThemDuLieuPhimHinhAnh()
        {
            try
            {
                System.Collections.Specialized.NameValueCollection form = HttpContext.Current.Request.Form;
                string TieuDe = form["TieuDe"];
                string TenTapTin = form["TenTapTin"];
                string MoTa = form["MoTa"];
                string path = null;
                string fileName = null;
                string ngayThangStr = form["NgayThang"];
                DateTime? NgayThang = null;
                if (!string.IsNullOrEmpty(ngayThangStr))
                {
                    NgayThang = ClsFunctionDate.ConvertDate(ngayThangStr);
                }
                string urlHost = ClsFtp.GetHostFTP();
                string UserName = ClsFtp.GetUserNameFTP();
                string Password = ClsFtp.GetPassWordFTP();
                for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                {
                    var filepath = HttpContext.Current.Request.Files.Count > 0 ?
                    HttpContext.Current.Request.Files[i] : null;
                    if (filepath != null && filepath.ContentLength > 0)
                    {
                        fileName = Path.GetFileName(filepath.FileName);
                        path = Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data"), fileName);
                        filepath.SaveAs(path);

                        // Xác định kiểu của tệp đính kèm bằng cách sử dụng ClsFtp
                        string mimeType = ClsFtp.GetMimeTypeByFileName(fileName);

                        // Kiểm tra kiểu MIME để xác định xem tệp là hình ảnh hay video
                        bool IsImage = mimeType.StartsWith("image/");
                        bool IsVideo = mimeType.StartsWith("video/");

                        int max_ID = dbContext.PhimHinhAnhs.Max(p => p.ID) + 1;
                        string ten_FolderSaveToFTP = "PhimAnh";
                        bool createFolder = ClsFtp.CreateFolder(urlHost, ten_FolderSaveToFTP, UserName, Password);
                        if (createFolder)
                        {
                            bool fileUploaded = ClsFtp.UploadFile(urlHost, ten_FolderSaveToFTP, fileName, path, UserName, Password);
                        }
                        using (TESTEntities dbContext = new TESTEntities())
                        {
                            var cp = new PhimHinhAnh()
                            {
                                TieuDe = TieuDe,
                                NgayThang = NgayThang,
                                TenTapTin = fileName,
                                DuongDanURL = "/PhimAnh/" + max_ID.ToString() + @"/" + fileName,
                                MoTa = MoTa,
                                ThoiGianCapNhat = DateTime.Now,
                                IsDeleted = false,
                                IsImage = IsImage,
                                IsVideo = IsVideo
                            };
                            dbContext.PhimHinhAnhs.Add(cp);
                            dbContext.SaveChanges();
                            File.Delete(path); //xoá App_Data
                        }
                    }
                    else
                    {
                        return BadRequest("Vui lòng chọn files để thêm");
                    }
                }
                return Ok("Dữ liệu đã được thêm thành công");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Dịch vụ cập nhật phim, ảnh
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult CapNhatDuLieuPhimHinhAnh(int idupa)
        {
            try
            {
                System.Collections.Specialized.NameValueCollection form = HttpContext.Current.Request.Form;
                string TieuDe = form["TieuDe"];
                string MoTa = form["MoTa"];
                string ngayThangStr = form["NgayThang"];
                DateTime? NgayThang = null;
                if (!string.IsNullOrEmpty(ngayThangStr))
                {
                    NgayThang = ClsFunctionDate.ConvertDate(ngayThangStr);
                }
                string urlHost = ClsFtp.GetHostFTP();
                string UserName = ClsFtp.GetUserNameFTP();
                string Password = ClsFtp.GetPassWordFTP();
                string ten_FolderSaveToFTP = "PhimHinhAnh";

                using (TESTEntities dbContext = new TESTEntities())
                {
                    var phimHinhAnh = dbContext.PhimHinhAnhs.FirstOrDefault(p => p.ID == idupa && p.IsDeleted == false);

                    if (phimHinhAnh == null)
                    {
                        return BadRequest("Không tìm thấy dữ liệu để cập nhật.");
                    }

                    //Cập nhật các thông tin
                    phimHinhAnh.TieuDe = TieuDe;
                    phimHinhAnh.NgayThang = NgayThang;
                    phimHinhAnh.MoTa = MoTa;
                    phimHinhAnh.ThoiGianCapNhat = DateTime.Now;
              

                    dbContext.SaveChanges();

                    if (HttpContext.Current.Request.Files.Count > 0)
                    {
                        var filepath = HttpContext.Current.Request.Files[0];
                        if (filepath != null && filepath.ContentLength > 0)
                        {
                            string fileName = Path.GetFileName(filepath.FileName);
                            string path = Path.Combine(HttpContext.Current.Server.MapPath("~/PhimHinhAnh"), fileName);
                            filepath.SaveAs(path);
                            // Xác định kiểu của tệp đính kèm bằng cách sử dụng ClsFtp
                            string mimeType = ClsFtp.GetMimeTypeByFileName(fileName);

                            // Kiểm tra kiểu MIME để xác định xem tệp là hình ảnh hay video
                            bool IsImage = mimeType.StartsWith("image/");
                            bool IsVideo = mimeType.StartsWith("video/");
     
                            // Xóa tệp đính kèm cũ trên máy chủ FTP
                            ClsFtp.DeleteFile(path, UserName, Password);

                            // Tải lên tệp đính kèm mới lên máy chủ FTP
                            bool fileUploaded = ClsFtp.UploadFile(urlHost, ten_FolderSaveToFTP, fileName, path, UserName, Password);

                            // Cập nhật tên tệp đính kèm mới trong cơ sở dữ liệu
                            phimHinhAnh.TenTapTin = fileName;
                            phimHinhAnh.DuongDanURL = "/PhimHinhAnh/" + phimHinhAnh.ID.ToString() + @"/" + fileName;
                            phimHinhAnh.IsImage = IsImage;
                            phimHinhAnh.IsVideo = IsVideo;
                            dbContext.SaveChanges();

                            File.Delete(path);
                        }
                        else
                        {
                            return Ok("Giữ nguyên file đính kèm. Cập nhật dữ liệu thông tin thành công");
                        }
                    }
                    return Ok("Dữ liệu đã được cập nhật thành công");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpPost]
        public IHttpActionResult ThemDuLieuBanDo()
        {
            try
            {
                System.Collections.Specialized.NameValueCollection form = HttpContext.Current.Request.Form;
                string TenBanDo = form["TenBanDo"];
                string TenTapTin = form["TenTapTin"];
                string MoTa = form["MoTa"];
                string path = null;
                string fileName = null;
                string urlHost = ClsFtp.GetHostFTP();
                string UserName = ClsFtp.GetUserNameFTP();
                string Password = ClsFtp.GetPassWordFTP();
                for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                {
                    var filepath = HttpContext.Current.Request.Files.Count > 0 ?
                    HttpContext.Current.Request.Files[i] : null;
                    if (filepath != null && filepath.ContentLength > 0)
                    {
                        fileName = Path.GetFileName(filepath.FileName);
                        path = Path.Combine(HttpContext.Current.Server.MapPath("~/BanDo"), fileName);
                        filepath.SaveAs(path);
                        int max_ID = dbContext.BanDoes.Max(p => p.ID) + 1;
                        string FileName = System.IO.Path.GetFileName(filepath.FileName);
                        string FileTest = System.IO.Path.GetFullPath(filepath.FileName);
                        using (TESTEntities dbContext = new TESTEntities())
                        {
                            var bd = new BanDo()
                            {
                                TenBanDo = TenBanDo,
                                TenTapTin = fileName,
                                DuongDanURL = "/BanDo/" + max_ID.ToString() + @"/" + fileName,
                                MoTa = MoTa,
                                ThoiGianCapNhat = DateTime.Now,
                                IsDeleted = false,
                            };
                            string ten_FolderSaveToFTP = "BanDo";
                            bool createFolder = ClsFtp.CreateFolder(urlHost, ten_FolderSaveToFTP, UserName, Password);
                            if (createFolder)
                            {
                                bool fileUploaded = ClsFtp.UploadFile(urlHost, ten_FolderSaveToFTP, fileName, path, UserName, Password);
                            }
                            dbContext.BanDoes.Add(bd);
                            dbContext.SaveChanges();
                        }
                    }
                    else
                    {
                        return BadRequest("Vui lòng chọn files để thêm");
                    }
                }
                return Ok("Dữ liệu đã được thêm thành công!");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Dịch vụ cập nhật bản đồ
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public List<License> LayThongTinGiayPhepTheoID(int idgp)
        {
            var giaypheps = dbContext.ThongTinGiayPheps.Where(s => s.ID == idgp && s.IsDeleted == false)
                .Select(sp => new License
                {
                    ID = sp.ID,
                    SoHieu = sp.SoHieu,
                    Nam = sp.Nam,
                    NgayThang = sp.NgayThang,
                    TenDonViDuocCapPhep = sp.TenDonVi,
                    PhamViHoatDongDuBao = sp.PhamViHoatDong,
                    //DoiTuongCungCap = sp.DonViToChuc.TenDonVi,
                    GiaHan = sp.GiaHan,
                    DonViKyXacNhan = sp.DonViKyXacNhan,
                    //LoaiCapDo = sp.CapDo.TenLoaiCapDo,
                    TrangThaiGiayPhep = sp.TrangThaiGiayPhep,
                    LyDoThuHoi = sp.LyDoThuHoi
                }).OrderBy(s => s.ID).ToList();
            if (giaypheps.Count == 0)
            {
                throw new Exception("Không tồn tại thông tin giấy phép.");
            }
            return giaypheps;
        }

        /// <summary>
        /// Dịch vụ thêm dữ liệu thông tin giấy phép
        /// </summary>
        /// <param name="giayphep"></param>
        [HttpPost]
        public void ThemDuLieuGiayPhep(ThongTinGiayPhep giayphep)
        {
            string ngayThangStr = null;
            DateTime? NgayThang = null;
            if (!string.IsNullOrEmpty(ngayThangStr)) //chuyển đổi chuỗi thành kiểu DateTime
            {
                NgayThang = ClsFunctionDate.ConvertDate(ngayThangStr);
            }
            var gp = new ThongTinGiayPhep()
            {
                SoHieu = giayphep.SoHieu,
                Nam = giayphep.Nam,
                NgayThang = giayphep.NgayThang,  //nhập theo định dạng yyyy/mm/dd
                TenDonVi = giayphep.TenDonVi,
                PhamViHoatDong = giayphep.PhamViHoatDong,
                DoiTuongCungCap = giayphep.DoiTuongCungCap,
                GiaHan = giayphep.GiaHan,
                DonViKyXacNhan = giayphep.DonViKyXacNhan,
                MaCapDoDuBao = giayphep.MaCapDoDuBao,
                TrangThaiGiayPhep = giayphep.TrangThaiGiayPhep,
                LyDoThuHoi = giayphep.LyDoThuHoi,
                IsDeleted = false
            };
            dbContext.ThongTinGiayPheps.Add(gp);
            dbContext.SaveChanges();
        }


        [HttpPatch]
        public IHttpActionResult CapNhatDuLieuThongTinGiayPhep(int idugp, ThongTinGiayPhep giayphep)
        {
            try
            {
                string ngayThangStr = null;
                DateTime? NgayThang = null;
                if (!string.IsNullOrEmpty(ngayThangStr)) //chuyển đổi chuỗi thành kiểu DateTime
                {
                    NgayThang = ClsFunctionDate.ConvertDate(ngayThangStr);
                }
                var giayPhepUpdate = dbContext.ThongTinGiayPheps.FirstOrDefault(gp => gp.ID == idugp && gp.IsDeleted == false);
                if (giayPhepUpdate == null)
                {
                    return BadRequest("Không tìm thấy giấy phép để cập nhật.");
                }
                //Cập nhật các thông tin
                giayPhepUpdate.SoHieu = giayphep.SoHieu;
                giayPhepUpdate.Nam = giayphep.Nam;
                giayPhepUpdate.NgayThang = giayphep.NgayThang; //theo định dạng yyyy/mm/dd
                giayPhepUpdate.TenDonVi = giayphep.TenDonVi;
                giayPhepUpdate.PhamViHoatDong = giayphep.PhamViHoatDong;
                giayPhepUpdate.DoiTuongCungCap = giayphep.DoiTuongCungCap;
                giayPhepUpdate.GiaHan = giayphep.GiaHan;
                giayPhepUpdate.DonViKyXacNhan = giayphep.DonViKyXacNhan;
                giayPhepUpdate.MaCapDoDuBao = giayphep.MaCapDoDuBao;
                giayPhepUpdate.TrangThaiGiayPhep = giayphep.TrangThaiGiayPhep;
                giayPhepUpdate.LyDoThuHoi = giayphep.LyDoThuHoi;
                giayPhepUpdate.IsDeleted = false;

                dbContext.SaveChanges();

                return Ok("Dữ liệu đã được cập nhật.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpGet]
        public List<AnhPhim> LayThongTinPhimAnhTheoID(int idpa)
        {
            var phimanhs = dbContext.PhimHinhAnhs.Where(pa => pa.ID == idpa && pa.IsDeleted == false)
                  .Select(sp => new AnhPhim
                  {
                      ID = sp.ID,
                      TieuDe = sp.TieuDe,
                      NgayThang = sp.NgayThang,
                      TenTapTin = sp.TenTapTin,
                      MoTa = sp.MoTa,
                      DuongDanURL = sp.DuongDanURL,
                      ThoiGianCapNhat = sp.ThoiGianCapNhat
                  }).OrderBy(s => s.ID);
            return phimanhs.ToList();
        }
    }
}
