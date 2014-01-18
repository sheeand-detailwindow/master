using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Net.Mail;

namespace detailwindow.uc
{
    public partial class contentContactUs2 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Add focus to textbox
            txtBox.Focus();

            if ((Session["Email"] == null) || (Session["Email"] == ""))
            {
                emailAddress.Visible = true;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string strEmail = "";

            if (Session["Name"] == null)
            {
                Session["Name"] = "";
            }
            if (Session["LastName"] == null)
            {
                Session["LastName"] = "";
            }
            if (txtEmail.Visible)
            {
                strEmail = txtEmail.Text;
            }
            else
            {
                strEmail = (string)Session["Email"];
            }

            SmtpClient client = new SmtpClient();
            MailMessage mail = new MailMessage();
            client.Host = ConfigurationManager.AppSettings["SmtpServer"];
            mail.From = new MailAddress(ConfigurationManager.AppSettings["MailFrom"]);
            mail.To.Add(ConfigurationManager.AppSettings["MailTo"]);
            mail.IsBodyHtml = true;
            mail.Subject = "Message from www.DetailWindow.com";
            mail.Body = "<font style='font-family:Arial'> <br /><br />";
            mail.Body += "The following message was sent by a visitor at www.detailwindow.com:<br /><br />";
            mail.Body += Server.HtmlEncode(txtBox.Text);
            mail.Body += "<br /><br />";
            mail.Body += Server.HtmlEncode((string)Session["Name"]) + " ";
            mail.Body += Server.HtmlEncode((string)Session["LastName"]) + "<br /><br />";
            mail.Body += Server.HtmlEncode((string)Session["Email"]) + "<br /><br />";
            client.Send(mail);
            Response.Redirect("ContactUsConfirm2.aspx");
        }
    }
}