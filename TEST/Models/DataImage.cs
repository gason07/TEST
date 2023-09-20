using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace TEST.Models
{
    public class DataImage
    {
        public IFormFile fileimage { get; set; }
        public string datajson { get; set; }
    }
}