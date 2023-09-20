using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEST.Models
{
    public class Maps
    {
        public int ID { get; set; }
        public string TenBanDo { get; set; }
        //public Nullable<System.DateTime> LoaiBanDo { get; set; }
        public string TenTapTin { get; set; }
        public string MoTa { get; set; }
        public Nullable<System.DateTime> ThoiGianCapNhat { get; set; }
        public string DuongDan { get; set; }

        //public byte[] maps { get; set; }
        //public bool IsDeleted { get; set; }
    }
}