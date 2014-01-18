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
    public partial class AppointmentConfirm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Redirect hackers from logging on as members
            if (Session["AccountType"] == null)
            {
                    Response.Redirect("default.aspx");
            }
        }

        private void lnkEditProfile_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("profile.aspx");
        }

        private void lnkLogOut_Click(object sender, System.EventArgs e)
        {
            FormsAuthentication.SignOut();

            // Set query string "Force=1" to tell login.aspx not to log in the user again.
            Response.Redirect("login.aspx?Force=1");
        }

        private void lnkDeAutoLogin_Click(object sender, System.EventArgs e)
        {
            // Look for the cookie that determines if user wants to log in automatically.
            HttpCookie Cookie = Request.Cookies["DetailWindow"];

            // If the cookie exists,
            if (Cookie != null)
            {
                // Get rid of the cookie.
                Cookie.Expires = DateTime.Now;
                Response.AppendCookie(Cookie);
            }

            FormsAuthentication.SignOut();

            // Set query string "Force=1" to tell login.aspx not to log in the user again.
            Response.Redirect("login.aspx?Force=1");
        }

        private void lnkAppointment_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("appointment.aspx");
        }
    }
}
