using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace TEST.Models
{
    public class CompanyImage
    {
        public int ID { get; set; }
        public string NameCompany { get; set; }
        public string AddressCompany { get; set; }
        public Nullable<System.DateTime> ThoiGianCapNhat { get; set; }
        public string AddressURL { get; set; }

        public byte[] Image { get; set; }
        //public IFormFile fileimage { get; set; }
        //public string datajson { get; set; }
    }
}