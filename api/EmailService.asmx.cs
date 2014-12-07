using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Services;
using System.ServiceModel.Channels;
//using Newtonsoft.Json.Serialization;
using System.Web.Caching;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Security.Cryptography;


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
        SqlConnection objConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["detailConnectionString"].ConnectionString);

        #region WEB METHODS

        [WebMethod(Description = "Loads cache object from database. This method is asynchroniously called upon requesting admin.aspx")]
        public void LoadCustomerCache()
        {
            List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();
            SqlDataReader objReader = null;
            string strSQL = "SELECT * FROM Customer";
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);
            using (objConnection)
            {
                objConnection.Open();
                objReader = objCommand.ExecuteReader();
                while (objReader.Read())
                {
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data["ID"] = DbNullCleaner(objReader["ID"]);
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
                objConnection.Close();
            }
            Context.Cache.Insert("Data", dataList, null, DateTime.Now.AddHours(1), Cache.NoSlidingExpiration);
        }

        [WebMethod(Description = "This service has several uses, depending on the parameters given. For testing purposes, enter 'Reminder' for Type, 'WebmasterTest' for Rendition, and '1' for Row. This will send a test email to the webmaster.")]
        public List<string> SendEmail(string Type, string Rendition, string Row, string Count, string CutoffDate)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();
            object objCache = Context.Cache["Data"];

            DateTime sendCutoffDate = Convert.ToDateTime(CutoffDate);

            if (objCache == null)
            {
                LoadCustomerCache();
            }

            // Get cached data object
            dataList = (List<Dictionary<string, object>>)Context.Cache["Data"];
            int maxRowCount = dataList.Count();
            List<string> returnMessage;

            if (Type == "Reminder")
            {
                // Get specific row
                List<Dictionary<string, object>> reminderDataList = GetReminderDataList(dataList);

                // Run email routine
                returnMessage = EmailRoutine(Type, Rendition, 0, 0, 0, reminderDataList, DateTime.MinValue);
            }
            else
            {
                // Promo
                int row = Convert.ToInt32(Row);
                int emailCount = Convert.ToInt32(Count);

                // Failsafe in case the javascript to bail out didn't stop looping
                if (row < maxRowCount)
                {
                    // Run email routine
                    returnMessage = EmailRoutine(Type, Rendition, emailCount, row, maxRowCount, dataList, sendCutoffDate);
                }
                else
                {
                    returnMessage = null;
                }
            }

            return returnMessage;
        }

        [WebMethod(Description = "This is a test service.")]
        public string Test(string x)
        {
            string message = "This is your server speaking.";
            return message;
        }


        #endregion

        #region METHODS

        private List<string> EmailRoutine(string Type, string Rendition, int emailCount, int row, int maxRowCount, List<Dictionary<string, object>> dataList, DateTime cutoffDate)
        {
            // Get data from row
            long ID = Convert.ToInt64(dataList[row]["ID"]);
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
            string strFooter = "<p><span style='color:#aa0000; font-weight:bold;'>Detail Window Cleaning - RJJK, Inc.</span><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&#8226 Detail-minded professionals<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&#8226 Serving greater Indianapolis</p>";
            string strPromoOptOut = String.Concat("<p style='font-size:10px;'>To opt out of receiving these promotional emails, <a href='", Path(), "/OptOut.aspx?t=p&e=", Encrypt(ID.ToString()), "'>click here</a>.</p></div>");
            string strReminderOptOut = String.Concat("<p style='font-size:10px;'>To opt out of receiving these reminder emails, <a href='", Path(), "/OptOut.aspx?t=r&e=", Encrypt(ID.ToString()), "'>click here</a>.</p></div>");
            string strSubject = "";
            string strBody = "";

            // Declare return message
            string Message = "";

            // Compose email
            switch (Type)
            {
                case "Reminder":
                    strSubject = "A Friendly Reminder";
                    strBody = String.Concat(strHeader, strReminder, strSchedNow, strFooter, strReminderOptOut);
                    break;

                case "JanFeb":
                    strSubject = "Winter Discounts!";
                    strBody = String.Concat(strHeader, strDeepHome, strSchedNow, strWinterStart, strJanFeb, strWinterEnd, strSchedDiscount, strFooter, strPromoOptOut);
                    break;

                case "March":
                    strSubject = "Winter Discounts!";
                    strBody = String.Concat(strHeader, strDeepHome, strSchedNow, strWinterStart, strMarch, strWinterEnd, strSchedDiscount, strFooter, strPromoOptOut);
                    break;

                case "July":
                    strSubject = "Summer Savings!";
                    strBody = String.Concat(strHeader, strJuly, strSchedNow, strFooter, strPromoOptOut);
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
                        // when a good row is found, run email routine and bail out


                        // Set max email count 
                        // Max. limit = 230 emails (GoDaddy email account allows 250 referrers per day)

                        while (row < maxRowCount && emailCount < 230)
                        {
                            // The zero-based row will not be exceeding the max row count

                            // Get data from row
                            ID = Convert.ToInt32(dataList[row]["ID"]);
                            LastName = Convert.ToString(dataList[row]["LastName"]);
                            Email = Convert.ToString(dataList[row]["Email"]);
                            AccountType = Convert.ToInt32(dataList[row]["AccountType"]);
                            LastLogin = Convert.ToDateTime(dataList[row]["LastLogin"]);
                            NextReminder = Convert.ToDateTime(dataList[row]["NextReminder"]);
                            Recurrency = Convert.ToInt32(dataList[row]["Recurrency"]);
                            ReminderOptOut = Convert.ToBoolean(dataList[row]["ReminderOptOut"]);
                            SpecialsOptOut = Convert.ToBoolean(dataList[row]["SpecialsOptOut"]);
                            PromoSent = Convert.ToDateTime(dataList[row]["PromoSent"]);

                            // Email permitters
                            bool hasCustomerNotOptedOut = (!SpecialsOptOut);
                            bool isPromoSentBeforeGivenDate = (PromoSent < cutoffDate);
                            bool isAccountTypeCorrect = (AccountType < 2);
                            bool isEmailNotMissing = (Email != null && Email != "");
                            //bool isEmailAddressgood = IsGoodEmailAddress(Email);
                            bool isEmailAddressgood = true;

                            if (hasCustomerNotOptedOut && isPromoSentBeforeGivenDate && isAccountTypeCorrect && isEmailNotMissing && isEmailAddressgood)
                            {
                                //********************************************************//
                                // The account is correct and the email address is good   //
                                //                                                        //
                                //********************************************************//

                                Send(strSubject, strBody, Email);

                                // Add 1 to the email count
                                emailCount++;

                                // Update database
                                UpdateCustomerRecord(ID, "Sent");

                                // Advance the row counter to the next row
                                row++;

                                // Break out of the while loop
                                break;
                            }
                            else
                            {
                                // The account is incorrect or the email address is no good or missing
                                // Update database
                                // UpdateCustomerRecord(Email, "Not sent");

                                // Advance row counter to the next row
                                row++;
                            }
                        }

                        // Build message
                        if (emailCount > 0)
                        {
                            Message = String.Concat("Email ", emailCount.ToString(), " sent to ", Email);
                        }
                        else
                        {
                            Message = "No emails sent ";
                        }

                        // Is this the last zero-based row?
                        if (row >= maxRowCount || emailCount >= 230)
                        {
                            // The end of the list has been reached
                            // Append the message to flag the javascript to bail out
                            Message = String.Concat(Message, " **Done**");
                        }
                    }

                    else
                    {
                        // Live Reminder email (automated call)

                        // ************************************
                        Send(strSubject, strBody, Email);

                        Message = "Email sent";

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
                returnMessage.Add(Message);

                // This is a promo cycle
                if (Message.IndexOf("**Done**") > -1)
                {
                    // Send confirmation email to webmaster
                    Send(Message, strBody, ConfigurationManager.AppSettings["MailToWebmaster"]);
                }
                else
                {
                    // Send message to repeat
                    string Row = row.ToString();
                    string Count = emailCount.ToString();
                    returnMessage.Add(Row);
                    returnMessage.Add(Count);
                }
            }
            return returnMessage;
        }

        private string Path()
        {
            string path = String.Concat("http://", HttpContext.Current.Request.Url.Authority);
            return path;
        }

        private void Send(string subject, string strBody, string emailAddress)
        {
            MailMessage mail = new MailMessage();
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

                case "relay-hosting.secureserver.net":
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

        private bool IsGoodEmailAddress(string emailAddrUnderTest)
        {
            TcpClient tClient = new TcpClient("gmail-smtp-in.l.google.com", 25);
            string CRLF = "\r\n";
            byte[] dataBuffer;
            string responseString;
            NetworkStream netStream = tClient.GetStream();
            StreamReader reader = new StreamReader(netStream);
            responseString = reader.ReadLine();

            /* Perform HELO to SMTP Server and get Response */
            dataBuffer = Encoding.ASCII.GetBytes("HELO KirtanHere" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            responseString = reader.ReadLine();
            dataBuffer = Encoding.ASCII.GetBytes(String.Concat("MAIL FROM:<", emailAddrUnderTest, ">", CRLF));
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            responseString = reader.ReadLine();

            /* Read Response of the RCPT TO Message to know from google if it exist or not */
            dataBuffer = Encoding.ASCII.GetBytes("RCPT TO:<senderemail@domainname.com>" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            responseString = reader.ReadLine();

            /* QUITE CONNECTION */
            dataBuffer = Encoding.ASCII.GetBytes("QUITE" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            tClient.Close();

            if (GetResponseCode(responseString) == 550)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private int GetResponseCode(string ResponseString)
        {
            return int.Parse(ResponseString.Substring(0, 3));
        }

        private void UpdateCustomerRecord(long ID, string status)
        {
            SqlConnection _objConnection = new SqlConnection(ConfigurationManager.AppSettings["ConnectString"]);
            string strSQL = String.Concat("UPDATE Customer SET PromoSent = '", DateTime.Now.ToShortDateString(), "', SentStatus = '", status, "' WHERE ID = ", ID);
            SqlCommand objCommand = new SqlCommand(strSQL, _objConnection);
            using (_objConnection)
            {
                _objConnection.Open();
                objCommand.ExecuteNonQuery();
                _objConnection.Close();
            }
        }

        private object DbNullCleaner(object obj)
        {
            if (obj == DBNull.Value) obj = null;
            return obj;
        }

        private static string Encrypt(string plainText)
		{
            string Hash = "P@@Sw0rd";
            string SaltKey = "S@LT&KEY";
            string VIKey = "@1B2c3D4e5F6g7H8";

			byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

			byte[] keyBytes = new Rfc2898DeriveBytes(Hash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
			var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
			var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
			
			byte[] cipherTextBytes;

			using (var memoryStream = new MemoryStream())
			{
				using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
				{
					cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
					cryptoStream.FlushFinalBlock();
					cipherTextBytes = memoryStream.ToArray();
					cryptoStream.Close();
				}
				memoryStream.Close();
			}
			return Convert.ToBase64String(cipherTextBytes);
		}

        #endregion
    }

}
