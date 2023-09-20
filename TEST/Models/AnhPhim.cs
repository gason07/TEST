using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEST.Models
{
    public class AnhPhim
    {
        public int ID { get; set; }
        public string TieuDe { get; set; }
        public Nullable<System.DateTime> NgayThang { get; set; }
        public string TenTapTin { get; set; }
        public string MoTa { get; set; }
        public Nullable<System.DateTime> ThoiGianCapNhat { get; set; }
        public string DuongDanURL { get; set; }

       // public byte[] View { get; set; }
        //public bool IsDeleted { get; set; }
        //public bool IsVideo { get; set; }
        //public bool IsImage { get; set; }
    }
}