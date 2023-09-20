using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEST.Models
{
    public class DuBaoBanTin
    {
        public int ID { get; set; }
        public string TieuDe { get; set; }
        public string TomTatNoiDung { get; set; }
        public string NoiDung { get; set; }
        public DateTime? NgayThucHien { get; set; }
        public string NguoiThucHien { get; set; }
        public string TenBanTin { get; set; }
        public string ThongTinDiaDiem { get; set; }
        public string KhuVucDuBao { get; set; }
        //public int? ThoiGianDuBao { get; set; }
        public DateTime? TuThoiGian { get; set; }
        public DateTime? DenThoiGian { get; set; }
        public string CapDoDuBao { get; set; }
        public string LoaiThienTai { get; set; }
        public string TenToChucDuBao { get; set; }
        public bool? TrangThaiDuyet { get; set; }
        public List<File_DK> TepDinhKem { get; set; }
    }
}