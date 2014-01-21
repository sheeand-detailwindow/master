using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Services;
using System.ServiceModel.Channels;
//using Newtonsoft.Json.Serialization;
using System.Web.Caching;


namespace detailwindow.api
{
    /// <summary>
    /// Summary description for EmailService
    /// </summary>
    [WebService(Namespace = "http://www.detailwindow.com/api", Description = "Companion email service for www.detailwindow.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]

    public class EmailService : System.Web.Services.WebService
    {
        OleDbConnection _objConnection = new OleDbConnection(ConfigurationManager.AppSettings["ConnectString"]);

        [WebMethod(Description = "Loads cache object from database. This method is asynchroniously called upon requesting admin.aspx")]
        public void LoadCustomerCache()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();
            OleDbDataReader objReader = null;
            string strSQL = "SELECT * FROM Customer";
            OleDbCommand objCommand = new OleDbCommand(strSQL, _objConnection);
            _objConnection.Open();
            objReader = objCommand.ExecuteReader();
            while (objReader.Read())
            {
                data["LastName"] = objReader["LastName"];
                data["Email"] = objReader["Email"];
                data["LastLogin"] = objReader["LastLogin"];
                data["NextReminder"] = objReader["NextReminder"];
                data["Recurrency"] = objReader["Recurrency"];
                data["ReminderOptOut"] = objReader["ReminderOptOut"];
                data["SpecialsOptOut"] = objReader["SpecialsOptOut"];
                data["PromoSent"] = objReader["PromoSent"];
                dataList.Add(data);
            }
            Context.Cache.Insert("Data", dataList, null, DateTime.Now.AddHours(1), Cache.NoSlidingExpiration);
        }
        
        [WebMethod(Description = "This service has several uses, depending on the parameters given. For testing purposes, enter 'Reminder' for Type, 'WebmasterTest' for Rendition, and '1' for Row. This will send a test email to the webmaster.")]
        public List<string> SendEmail(string Type, string Rendition, string Row)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();
            object objCache = Context.Cache["Data"];
            
            if (objCache == null)
            {
                LoadCustomerCache();
            }
                
            dataList = (List<Dictionary<string, object>>)Context.Cache["Data"];

            data = dataList[Convert.ToInt32(Row)];
            string LastName = Convert.ToString(data["LastName"]);
            string Email = Convert.ToString(data["Email"]);
            DateTime LastLogin = Convert.ToDateTime(data["LastLogin"]);
            DateTime NextReminder = Convert.ToDateTime(data["NextReminder"]);
            int Recurrency = Convert.ToInt32(data["Recurrency"]);
            bool ReminderOptOut = Convert.ToBoolean(data["ReminderOptOut"]);
            bool SpecialsOptOut = Convert.ToBoolean(data["SpecialsOptOut"]);
            DateTime PromoSent = Convert.ToDateTime(data["PromoSent"]);

            string strHeader = "<img src=\"cid:image1\"/><div style='font-family:Arial, Vernada;font-size:12px;'><br /><br />";
            string strReminder = "<p style='font-size:16px; font-weight:bold;'>It's time to have your glass cleaned!<br /></p>";
            string strDeepHome = "<p style='font-size:16px; font-weight:bold;'>Deep Home Cleaning<br /></p><p style='font-weight:bold;'>Your home will <span style='text-decoration:underline;'>FEEL</span> and <span style='text-decoration:underline;'>SMELL</span> clean&#33;</p><p><ul style='font-size:12px;'><li>Woodwork (baseboards, doors and frames)</li><li>Wood blinds and walls</li><li>Basements and garages</li><li>Under and behind major appliances, heavy furniture, and other hard-to-get places</li><li>Ceiling fans, chandeliers, mirrors, light fixtures, and more!</li></ul></p>";
            string strSchedNow = "<p style='font-size:14px;'>Schedule your appointment now&#33;</p><p style='font-size:14px;'>Go to <a href='http://www.detailwindow.com'>www.DetailWindow.com</a>.<br />Or, call (317) 842-5326.</p>";
            string strWinterStart = "<hr><p style='font-size:16px; font-weight:bold;'>Winter discounts on window cleaning&#33<ul style='font-size:12px;'>";
            string strJanFeb = "<li>For work completed in January: 20% discount on window cleaning</li><li>For work completed in February: 20% discount on window cleaning</li>";
            string strMarch = "<li>For work completed in March: 15% discount on window cleaning</li>";
            string strWinterEnd = "</ul></p>";
            string strJuly = "<p style='font-size:16px; font-weight:bold;'>Summer savings on window cleaning&#33<ul style='font-size:12px;'><li>For work completed in July: 10% discount on window cleaning</li></ul></p>";
            string strFooter = "<p><span style='color:#aa0000; font-weight:bold;'>Detail Window Cleaning - RJJK, Inc.</span><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&#8226 Detail-minded professionals<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&#8226 Serving greater Indianapolis</p><br /></div>";

            string strSubject = "";
            string strBody = "";
            List<string> returnMessage = new List<string>();
            returnMessage[0] = "Email sent.";

            switch (Type)
            {
                case "Reminder":
                    strSubject = "A Friendly Reminder";
                    strBody = String.Concat(strHeader, strReminder, strSchedNow, strFooter);
                    break;

                case "JanFeb":
                    strSubject = "Winter Discounts!";
                    strBody = String.Concat(strHeader, strDeepHome, strSchedNow, strWinterStart, strJanFeb, strWinterEnd, strFooter);
                    break;

                case "March":
                    strSubject = "Winter Discounts!";
                    strBody = String.Concat(strHeader, strDeepHome, strSchedNow, strWinterStart, strMarch, strWinterEnd, strFooter);
                    break;

                case "July":
                    strSubject = "Summer Savings!";
                    strBody = String.Concat(strHeader, strJuly, strSchedNow, strFooter);
                    break;
            }

            switch (Rendition)
            {
                case "Live":
                    break;

                case "WebmasterTest":
                    Send(strSubject, strBody, "sheeand@hotmail.com");
                    break;

                case "AdministratorTest":
                    break;
            }

            return returnMessage;
        }

        private void Send(string subject, string strBody, string emailAddress)
        {
            SmtpClient client = new SmtpClient();
            MailMessage mail = new MailMessage();
            string strpath = Server.MapPath(@"detailLogoMini.gif");
            LinkedResource logo = new LinkedResource(strpath, MediaTypeNames.Image.Gif);
            logo.ContentId = "image1";
            logo.ContentType.Name = "detailLogoMini.gif";
            AlternateView av1 = AlternateView.CreateAlternateViewFromString(strBody, null, MediaTypeNames.Text.Html);
            av1.LinkedResources.Add(logo);
            mail.AlternateViews.Add(av1);
            client.Host = ConfigurationManager.AppSettings["SmtpServer"];
            mail.From = new MailAddress(ConfigurationManager.AppSettings["MailFrom"]);
            mail.To.Add(emailAddress);
            mail.IsBodyHtml = true;
            mail.Subject = subject;
            if (ConfigurationManager.AppSettings["SmtpServer"] == "smtp.comcast.net")
            {
                client.Port = 587;
                client.UseDefaultCredentials = false;
                System.Net.NetworkCredential cred = new System.Net.NetworkCredential("sheeand", "MSLKQcg");
                client.Credentials = cred;
            }
            using (client)
            {
                try
                {
                    client.Send(mail);
                }
                catch (Exception ex)
                {
                    Session["ErrorCode"] = ex.Message;
                }
                finally
                {
                    client.Dispose();
                }
            }
        }

        public string SendAnotherEmail(string Type, string Rendition, string Row)
        {
            string message = "";
            switch (Rendition)
            {
                case "JanFeb":
                    break;

                case "March":
                    break;

                case "July":
                    break;
            }

            return message;
        }

        private void ComposeAndSendEmail(string type, string emailAddress)
        {

        }

    }

}
