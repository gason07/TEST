//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TEST.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class FileDinhKem
    {
        public int ID { get; set; }
        public string TenFileDinhKem { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
        public string MoTa { get; set; }
        public string DuongDanURL { get; set; }
        public bool IsVideo { get; set; }
        public bool IsImage { get; set; }
        public bool IsDeleted { get; set; }
    }
}