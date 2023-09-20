using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEST.Models
{
    public class File_DK
    {
        public int ID { get; set; }

        public int? MaBanTinDuBao { get; set; }
        public string TenTapTin { get; set; }

        public string MoTa { get; set; }
        public DateTime? NgayDinhKem { get; set; }
        public string DuongDanURL { get; set; }
        public bool? IsImage { get; set; }
        public bool? IsVideo { get; set; }
    }
}