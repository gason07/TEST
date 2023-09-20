using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEST.Models
{
    public class NhanVienVaCongTy
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public Nullable<int> Salary { get; set; }
        public string NameCompany { get; set; }
        public string AddressCompany { get; set; }
        public Nullable<System.DateTime> ThoiGianCapNhat { get; set; }
    }
}