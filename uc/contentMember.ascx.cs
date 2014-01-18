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
    public partial class contentMember : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here

            // Add MouseOver effects to links
            HyperLink1.Attributes["onMouseOver"] = "style.color='red'";
            HyperLink1.Attributes["onMouseOut"] = "style.color='blue'";

        }
        protected void btnAppointment_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/appointment.aspx");
        }
        protected void btnApptointment2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/appointment.aspx");
        }
    }
}