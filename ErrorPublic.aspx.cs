using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Configuration;

namespace detailwindow
{
    public partial class ErrorPublic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strErrorCode = "";
            if (Session["ErrorCode"] == null)
            {
                strErrorCode = "No error code.";
            }
            else
            {
                strErrorCode = (string)Session["ErrorCode"];
            }
            //MailMessage mail = new MailMessage();
            //mail.IsBodyHtml = true;
            //mail.To.Add(ConfigurationManager.AppSettings["MailToWebmaster"]);
            //mail.From = new MailAddress(ConfigurationManager.AppSettings["MailFrom"]); // This domain must match the SmtpServer domain.
            //mail.Subject = "DetaiWindow.com - Exception Thrown";
            //mail.Body = strErrorCode;

            //// Sent the email message to the administrator.
            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = ConfigurationManager.AppSettings["SmtpServer"];
            //smtp.Send(mail);

        }
    }
}
