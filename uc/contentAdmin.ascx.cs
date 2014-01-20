using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace detailwindow
{
    public partial class contentAdmin : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string Path
        {
            get
            {
                string path = String.Concat("http://", HttpContext.Current.Request.Url.Host, "/EmailService.asmx");
                return path;
            }
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("default2.aspx");
        }
    }
}