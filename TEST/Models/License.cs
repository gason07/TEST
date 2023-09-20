using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEST.Models
{
    public class License
    {
        public int ID { get; set; }
        public string SoHieu { get; set; }
        public string Nam { get; set; }
        public DateTime? NgayThang { get; set; }
        public string TenDonViDuocCapPhep { get; set; }
        public string PhamViHoatDongDuBao { get; set; }
        public string DoiTuongCungCap { get; set; }
        public string ThoiHanCap { get; set; }
        public string GiaHan { get; set; }
        public int? DonViKyXacNhan { get; set; }
        public string LoaiCapDo { get; set; }
        public string TrangThaiGiayPhep { get; set; }
        public string LyDoThuHoi { get; set; }
    }
}