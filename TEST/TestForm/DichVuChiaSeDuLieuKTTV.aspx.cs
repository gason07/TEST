using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TEST.TestForm
{
    public partial class DichVuChiaSeDuLieuKTTV : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnChiTietA_Click(object sender, EventArgs e)
        {
            // Đảo ngược trạng thái hiển thị của Panel.
            pnlTuyChonA.Visible = !pnlTuyChonA.Visible;
        }

        protected void btnChiTietB_Click(object sender, EventArgs e)
        {
            // Đảo ngược trạng thái hiển thị của Panel.
            pnlTuyChonB.Visible = !pnlTuyChonB.Visible;
        }

        protected void btnChiTietC_Click(object sender, EventArgs e)
        {
            // Đảo ngược trạng thái hiển thị của Panel.
            pnlTuyChonC.Visible = !pnlTuyChonC.Visible;
        }

        protected void btnChiTietD_Click(object sender, EventArgs e)
        {
            // Đảo ngược trạng thái hiển thị của Panel.
            pnlTuyChonD.Visible = !pnlTuyChonD.Visible;
        }
    }
}