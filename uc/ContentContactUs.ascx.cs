using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Net.Mail;


namespace detailwindow
{
    public partial class ContentContactUs1 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Add focus to textbox
            txtBox.Focus();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string strEmail = txtEmail.Text;

            SmtpClient client = new SmtpClient();
            MailMessage mail = new MailMessage();
            client.Host = ConfigurationManager.AppSettings["SmtpServer"];
            mail.From = new MailAddress(ConfigurationManager.AppSettings["MailFrom"]);
            mail.To.Add(ConfigurationManager.AppSettings["MailTo"]);
            mail.IsBodyHtml = true;
            mail.Subject = "Message from www.DetailWindow.com";
            mail.Body = "<font style='font-family:Arial'> <br /><br />";
            mail.Body += "The following message was sent by a visitor at www.detailwindow.com:<br /><br />";
            mail.Body += Server.HtmlEncode(txtBox.Text) + "<br /><br />";
            mail.Body += strEmail;
            mail.Body += "<br /><br />";
            client.Send(mail);
            Response.Redirect("ContactUsConfirm.aspx");
        }
    }
}