using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net;
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
            List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();
            OleDbDataReader objReader = null;
            string strSQL = "SELECT * FROM Customer";
            OleDbCommand objCommand = new OleDbCommand(strSQL, _objConnection);
            _objConnection.Open();
            objReader = objCommand.ExecuteReader();
            while (objReader.Read())
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                data["LastName"] = DbNullCleaner(objReader["LastName"]);
                data["Email"] = DbNullCleaner(objReader["Email"]);
                data["AccountType"] = DbNullCleaner(objReader["AccountType"]);
                data["LastLogin"] = DbNullCleaner(objReader["LastLogin"]);
                data["NextReminder"] = DbNullCleaner(objReader["NextReminder"]);
                data["Recurrency"] = DbNullCleaner(objReader["Recurrency"]);
                data["ReminderOptOut"] = DbNullCleaner(objReader["ReminderOptOut"]);
                data["SpecialsOptOut"] = DbNullCleaner(objReader["SpecialsOptOut"]);
                data["PromoSent"] = DbNullCleaner(objReader["PromoSent"]);
                dataList.Add(data);
            }
            _objConnection.Close();
            Context.Cache.Insert("Data", dataList, null, DateTime.Now.AddHours(1), Cache.NoSlidingExpiration);
        }

        private object DbNullCleaner(object obj)
        {
            if (obj == DBNull.Value) obj = null;
            return obj;
        }
        
        [WebMethod(Description = "This service has several uses, depending on the parameters given. For testing purposes, enter 'Reminder' for Type, 'WebmasterTest' for Rendition, and '1' for Row. This will send a test email to the webmaster.")]
        public List<string> SendEmail(string Type, string Rendition, string Row, string Count)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();
            object objCache = Context.Cache["Data"];
            
            if (objCache == null)
            {
                LoadCustomerCache();
            }
            
            // Get cached data object
            dataList = (List<Dictionary<string, object>>)Context.Cache["Data"];
            int maxRowCount = dataList.Count();
            List<string> returnMessage;

            // Get specific row
            if (Type == "Reminder")
            {
                List<Dictionary<string, object>> reminderDataList = GetReminderDataList(dataList);

                // Run email routine
                returnMessage = EmailRoutine(Type, Rendition, 0, 0, 0, reminderDataList);
            }
            else
            {
                int row = Convert.ToInt32(Row);
                int count = Convert.ToInt32(Count);

                // Failsafe in case the javascript to bail out didn't stop looping
                if (row < maxRowCount)
                {
                    // Run email routine
                    returnMessage = EmailRoutine(Type, Rendition, count, row, maxRowCount, dataList);
                }
                else
                {
                    returnMessage = null;
                }
            }

            return returnMessage;
        }

        
        private List<string> EmailRoutine(string Type, string Rendition, int count, int row, int maxRowCount, List<Dictionary<string, object>> dataList)
        {

            // Get data from row
            string LastName = Convert.ToString(dataList[row]["LastName"]);
            string Email = Convert.ToString(dataList[row]["Email"]);
            int AccountType = Convert.ToInt32(dataList[row]["AccountType"]);
            DateTime LastLogin = Convert.ToDateTime(dataList[row]["LastLogin"]);
            DateTime NextReminder = Convert.ToDateTime(dataList[row]["NextReminder"]);
            int Recurrency = Convert.ToInt32(dataList[row]["Recurrency"]);
            bool ReminderOptOut = Convert.ToBoolean(dataList[row]["ReminderOptOut"]);
            bool SpecialsOptOut = Convert.ToBoolean(dataList[row]["SpecialsOptOut"]);
            DateTime PromoSent = Convert.ToDateTime(dataList[row]["PromoSent"]);

            // Declare email components
            //string strHeader = "<img src=\"cid:image1\"/><div style='font-family:Arial, Vernada;font-size:12px;'><br /><br />";
            string strHeader = "<img src='http://www.detailwindow.com/api/detailLogoMini.png'><div style='font-family:Arial, Vernada;font-size:12px;'><br /><br />";
            string strReminder = "<p style='font-size:16px; font-weight:bold;'>It's time to have your glass cleaned!<br /></p>";
            string strDeepHome = "<p style='font-size:16px; font-weight:bold;'>Deep Home Cleaning<br /></p><p style='font-weight:bold;'>Your home will <span style='text-decoration:underline;'>FEEL</span> and <span style='text-decoration:underline;'>SMELL</span> clean&#33;</p><p><ul style='font-size:12px;'><li>Woodwork (baseboards, doors and frames)</li><li>Wood blinds and walls</li><li>Basements and garages</li><li>Under and behind major appliances, heavy furniture, and other hard-to-get places</li><li>Ceiling fans, chandeliers, mirrors, light fixtures, and more!</li></ul></p>";
            string strSchedNow = "<p style='font-size:14px;'>Schedule your appointment now&#33;</p><p style='font-size:14px;'>Go to <a href='http://www.detailwindow.com'>www.DetailWindow.com</a>.<br />Or, call (317) 842-5326.</p>";
            string strWinterStart = "<hr><p style='font-size:16px; font-weight:bold;'>Winter discounts on window cleaning&#33<ul style='font-size:12px;'>";
            string strJanFeb = "<li>For work completed in February: <span style='text-decoration:line-through;'>20%</span> <span style='font-weight:bold;'>NOW 25%</span> discount on window cleaning</li>";
            string strMarch = "<li>For work completed in March: 15% discount on window cleaning</li>";
            string strWinterEnd = "</ul></p>";
            string strJuly = "<p style='font-size:16px; font-weight:bold;'>Summer savings on window cleaning&#33<ul style='font-size:12px;'><li>For work completed in July: 10% discount on window cleaning</li></ul></p>";
            string strSchedDiscount = "<p style='font-size:14px;'>Schedule your appointment now&#33;</p><p style='font-size:14px;'>Go to <a href='http://www.detailwindow.com'>www.DetailWindow.com</a> and mention the discount in the comments.<br />Or call (317) 842-5326 and mention the discount to our customer service representative.</p>";
            string strFooter = "<p><span style='color:#aa0000; font-weight:bold;'>Detail Window Cleaning - RJJK, Inc.</span><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&#8226 Detail-minded professionals<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&#8226 Serving greater Indianapolis</p><br /></div>";

            string strSubject = "";
            string strBody = "";

            // Declare return message
            string Message = "Email sent";

            // Compose email
            switch (Type)
            {
                case "Reminder":
                    strSubject = "A Friendly Reminder";
                    strBody = String.Concat(strHeader, strReminder, strSchedNow, strFooter);
                    break;

                case "JanFeb":
                    strSubject = "Winter Discounts!";
                    strBody = String.Concat(strHeader, strDeepHome, strSchedNow, strWinterStart, strJanFeb, strWinterEnd, strSchedDiscount, strFooter);
                    break;

                case "March":
                    strSubject = "Winter Discounts!";
                    strBody = String.Concat(strHeader, strDeepHome, strSchedNow, strWinterStart, strMarch, strWinterEnd, strSchedDiscount, strFooter);
                    break;

                case "July":
                    strSubject = "Summer Savings!";
                    strBody = String.Concat(strHeader, strJuly, strSchedNow, strFooter);
                    break;
            }

            switch (Rendition)
            {
                case "Live":

                    if (Type != "Reminder")
                    {
                        // Promo iteration routine
                        // Test row for 1) proper account type and 2) valid email address (not null or empty)
                        // if row is no good, row++ and test again
                        // when a roow row is found, run email routine and bail out
                        // *********************************************************************************************************
                        Dictionary<string, object> data;

                    Loop:

                        if (row < maxRowCount)
                        {
                            // The zero-based row will not be exceeding the max row count

                            // Get specific row data
                            data = dataList[row];

                            // Get data from row
                            LastName = Convert.ToString(dataList[row]["LastName"]);
                            Email = Convert.ToString(dataList[row]["Email"]);
                            AccountType = Convert.ToInt32(dataList[row]["AccountType"]);
                            LastLogin = Convert.ToDateTime(dataList[row]["LastLogin"]);
                            NextReminder = Convert.ToDateTime(dataList[row]["NextReminder"]);
                            Recurrency = Convert.ToInt32(dataList[row]["Recurrency"]);
                            ReminderOptOut = Convert.ToBoolean(dataList[row]["ReminderOptOut"]);
                            SpecialsOptOut = Convert.ToBoolean(dataList[row]["SpecialsOptOut"]);
                            PromoSent = Convert.ToDateTime(dataList[row]["PromoSent"]);

                            // Is the account is correct and the email address is good?
                            if ((AccountType == 0 && !String.IsNullOrEmpty(Email)))
                            {
                                // The account is correct and the email address is good
                                count++;
                                Message = String.Concat("Email ", count.ToString(), " sent to ", Email);

                                // *************************************
                                Send(strSubject, strBody, Email);

                                // Is this the last zero-based row?
                                if (row + 1 >= maxRowCount)
                                {
                                    // The end of the list has been reached
                                    // Append the message to flag the javascript to bail out
                                    Message = String.Concat(Message, " **Done**");
                                }

                                // Advance the row counter
                                row++;
                            }
                            else
                            {
                                // The account is incorrect or the email address is missing
                                // Advance to the next row
                                row++;
                                goto Loop;
                            }
                        }
                        else
                        {
                            // We left the loop because the end of the list has been reached
                            // Append the message to flag the javascript to bail out
                            Message = String.Concat(Message, "**Done**");
                        }



                        // *********************************************************************************************************
                    }
                    else
                    {
                        // Live Reminder email (automated call)

                        // ************************************
                        //Send(strSubject, strBody, Email);

                    }

                    break;

                case "WebmasterTest":
                    Send(strSubject, strBody, "sheeand@hotmail.com");
                    break;

                case "AdministratorTest":
                    Send(strSubject, strBody, "janicg61@yahoo.com");
                    break;
            }

            // Prepare return message & notify webmaster
            List<string> returnMessage = new List<string>();
            if (Type == "Reminder")
            {
                // Send confirmation email to webmaster
                Send(String.Concat("Reminder Sent To ", Email), strBody, ConfigurationManager.AppSettings["MailToWebmaster"]);
            }
            else
            {
                string Row = row.ToString();
                string Count = count.ToString();
                returnMessage.Add(Message);
                returnMessage.Add(Row);
                returnMessage.Add(Count);

                if (Message.IndexOf("**Done**") > -1)
                {
                    // Send confirmation email to webmaster
                    Send(Message, strBody, ConfigurationManager.AppSettings["MailToWebmaster"]);
                }
            }
            return returnMessage;
        }
        
        private void Send(string subject, string strBody, string emailAddress)
        {
            //LinkedResource logo = new LinkedResource(Server.MapPath(".") + @"\detailLogoMini.gif", MediaTypeNames.Image.Gif);
            //logo.ContentId = "image1";
            //logo.ContentType.Name = "detailLogoMini.gif";
            //logo.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;

            //AlternateView av1 = AlternateView.CreateAlternateViewFromString(strBody, null, MediaTypeNames.Text.Html);
            //av1.LinkedResources.Add(logo);

            MailMessage mail = new MailMessage();
            //mail.AlternateViews.Add(av1);
            mail.Body = strBody;
            mail.IsBodyHtml = true;
            mail.From = new MailAddress(ConfigurationManager.AppSettings["MailFrom"], "Detail Window Cleaning");
            mail.To.Add(emailAddress);
            mail.Subject = subject;

            SmtpClient client = new SmtpClient();
            client.Host = ConfigurationManager.AppSettings["SmtpServer"];
            System.Net.NetworkCredential credComcast = new System.Net.NetworkCredential("sheeand", "MSLKQcg");
            System.Net.NetworkCredential credGoDaddy = new System.Net.NetworkCredential("webmaster@detailwindow.com", "tbitwog1");
            switch (ConfigurationManager.AppSettings["SmtpServer"])
            {
                case "smtp.comcast.net":
                    client.Port = 587;
                    client.UseDefaultCredentials = false;
                    client.Credentials = credComcast;
                    break;

                case "smtpout.secureserver.net":
                    client.Port = 25;
                    client.UseDefaultCredentials = false;
                    client.Credentials = credGoDaddy;
                    break;
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

        private List<Dictionary<string, object>> GetReminderDataList(List<Dictionary<string, object>> dataList)
        {
            List<Dictionary<string, object>> reminderDataList = new List<Dictionary<string, object>>();
            Dictionary<string, object> dataRow = new Dictionary<string, object>();
            foreach (Dictionary<string, object> row in dataList)
            {
                if ((DateTime)row["NextReminder"] < DateTime.Now)
                {
                    reminderDataList.Add(row);
                }
            }

            return reminderDataList;
        }

    }

}
