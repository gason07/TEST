using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEST.Models
{
    public class FullNews
    {
        public int ID { get; set; }
        //public string TenBanTin { get; set; }
        public string TieuDe { get; set; }

        public DateTime? NgayThucHien { get; set; }
    }
}