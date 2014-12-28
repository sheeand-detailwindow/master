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
            Context.Cache.Remove("Data");
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
            Context.Cache.Insert("Data", dataList, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration);
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
            string errorMessage = "";

            // Declare email components
            //string strHeader = "<img src=\"cid:image1\"/><div style='font-family:Arial, Vernada;font-size:12px;'><br /><br />";
            string strHeader = "<body style='background-color: #ffffff;'><img src='http://www.detailwindow.com/api/detailLogoMini.png'><div style='font-family:Arial, Vernada;font-size:12px;'><br /><br />";
            string strReminder = "<p style='font-size:18px; font-weight:bold; color:#aa0000;'>It's time to have your glass cleaned!<br /></p>";
            string strDeepHome = "<p style='font-size:18px; font-weight:bold; color:#aa0000;'>Deep Home Cleaning<br />(December 15 to March 15)<br /></p><p style='font-weight:bold;'>Your home will <span style='text-decoration:underline;'>FEEL</span> and <span style='text-decoration:underline;'>SMELL</span> clean&#33;</p><p><ul style='font-size:12px;'><li>Woodwork (baseboards, doors and frames)</li><li>Wood blinds and walls</li><li>Basements and garages</li><li>Under and behind major appliances, heavy furniture, and other hard-to-get places</li><li>Ceiling fans, chandeliers, mirrors, light fixtures, and more!</li></ul></p>";
            string strSchedNow = "<p style='font-size:14px;'><em>Schedule your appointment now&#33;</em></p><p style='font-size:14px;'>Go to <a href='http://www.detailwindow.com'>www.DetailWindow.com</a>.<br /><br />Or, call (317) 842-5326.</p>";
            string strWinterStart = "<hr><p style='font-size:18px; font-weight:bold; color:#aa0000;'>Winter discounts on window cleaning&#33</p>";
            string strJanFeb = "<ul style='font-size:14px;'><li>December 15th to 31st: <span style='font-weight:bold;'>10%</span> discount on window cleaning</li><li>Gift cards are available! Receive a <span style='font-weight:bold;'>$10</span> gift card for every $100 spent.</li>";
            strJanFeb = String.Concat(strJanFeb, "<li>For work completed in January and February: <span style='font-weight:bold;'>25%</span> discount on window cleaning</li>");
            strJanFeb = String.Concat(strJanFeb, "<li>For work completed in March: <span style='font-weight:bold;'>15%</span> discount on window cleaning</li></ul>");
            string strMarch = "";
            string strWinterEnd = "<p style='font-size:14px;'><em>Schedule your appointment now&#33;</em></p>";
            string strSnowRemoval = "<hr><p style='font-size:18px; font-weight:bold; color:#aa0000;'>NEW THIS YEAR<br />Personalized snow removal</p><p style='font-size:14px;'>No big truck used, only snowblower and shovel used.<br /><em>Call for free estimate.</em></p>";
            string strJuly = "<p style='font-size:18px; font-weight:bold; color:#aa0000;'>Summer savings on window cleaning&#33<ul style='font-size:14px;'><li>For work completed in August and September: 10% discount on window cleaning</li></ul></p>";
            string strSchedDiscount = "<p style='font-size:14px;'>Go to <a href='http://www.detailwindow.com'>www.DetailWindow.com</a><br /><br />Or call (317) 842-5326 and mention the discount to our customer service representative.</p>";
            string strFooter = "<hr><p><span style='color:#aa0000; font-weight:bold;'>Detail Window Cleaning - RJJK, Inc.</span><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&#8226 Detail-minded professionals<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&#8226 Serving greater Indianapolis</p></body>";
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
                    strBody = String.Concat(strHeader, strDeepHome, strSchedNow, strWinterStart, strJanFeb, strSchedDiscount, strSnowRemoval, strFooter, strPromoOptOut);
                    break;

                case "March":
                    strSubject = "Winter Discounts!";
                    strBody = String.Concat(strHeader, strDeepHome, strSchedNow, strWinterStart, strMarch, strWinterEnd, strSchedDiscount, strFooter, strPromoOptOut);
                    break;

                case "July":
                    strSubject = "Summer Savings!";
                    strBody = String.Concat(strHeader, strJuly, strSchedNow, strFooter, strPromoOptOut);
                    break;

                default:
                    break;
            }

            switch (Rendition)
            {
                case "Live":

                        bool hasCustomerNotOptedOut = false;
                        bool previousPromoSentBeforeGivenDate = false;
                        bool isAccountTypeCorrect = false;
                        bool isEmailNotMissing = false;
                        bool isEmailAddressgood = false;

                        if (Type != "Reminder")
                    {
                        // Promo iteration routine
                        // Test row for 1) proper account type and 2) valid email address (not null or empty)
                        // if row is no good, row++ and test again
                        // when a good row is found, run email routine and bail out


                        // Set max email count 
                        // Max. limit = 230 emails (GoDaddy email account allows 250 referrers per day)

                        // while (row < maxRowCount && emailCount < 230)

                            while (row < maxRowCount)
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
                            hasCustomerNotOptedOut = (!SpecialsOptOut);
                            previousPromoSentBeforeGivenDate = (PromoSent < cutoffDate);
                            isAccountTypeCorrect = (AccountType < 2);
                            isEmailNotMissing = (Email != null && Email != "");
                            //isEmailAddressgood = IsGoodEmailAddress(Email);
                            isEmailAddressgood = true;

                            if (hasCustomerNotOptedOut && previousPromoSentBeforeGivenDate && isAccountTypeCorrect && isEmailNotMissing && isEmailAddressgood)
                            {
                                //********************************************************//
                                // The account is correct and the email address is good   //
                                //                                                        //
                                //********************************************************//

                                errorMessage = Send(strSubject, strBody, Email);

                                if (errorMessage == "")
                                {
                                    // Add 1 to the email count
                                    emailCount++;

                                    // Update database
                                    UpdateCustomerRecord(ID, "Sent");

                                    // Advance the row counter to the next row
                                    row++;
                                }
                                else
                                {
                                    // do something with errorMessage
                                }

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
                            Message = String.Concat("No emails sent. hasCustomerNotOptedOut=", Convert.ToString(hasCustomerNotOptedOut), " previousPromoSentBeforeGivenDate=", Convert.ToString(previousPromoSentBeforeGivenDate), " isAccountTypeCorrect=", Convert.ToString(isAccountTypeCorrect), " isEmailNotMissing=", Convert.ToString(isEmailNotMissing));
                        }

                        // Is this the last zero-based row, or has the fail safe of 230 been reached?
                        // if (row >= maxRowCount || emailCount >= 230)
                        if (row >= maxRowCount || emailCount >= 1000)
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
                        errorMessage = Send(strSubject, strBody, Email);

                        Message = "Email sent";

                    }

                    break;

                case "WebmasterTest":
                    errorMessage = Send(strSubject, strBody, "sheeand@hotmail.com");
                    break;

                case "AdministratorTest":
                    errorMessage = Send(strSubject, strBody, "detailwc@yahoo.com");
                    //Send(strSubject, strBody, "david52tv@yahoo.com");
                    break;

                default:
                    break;
            }

            // Prepare return message & notify webmaster
            List<string> returnMessage = new List<string>();

            if (Type == "Reminder")
            {
                if (errorMessage == "")
                {
                    // Send confirmation email to webmaster
                    Send(String.Concat("Reminder Sent To ", Email), strBody, ConfigurationManager.AppSettings["MailToWebmaster"]);
                }
                else
                {
                    Send(errorMessage, strBody, ConfigurationManager.AppSettings["MailToWebmaster"]);
                }
            }
            else
            {
                if (errorMessage == "")
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
                else
                {
                    returnMessage.Add(errorMessage);
                }
            }
            return returnMessage;
        }

        private string Path()
        {
            string path = String.Concat("http://", HttpContext.Current.Request.Url.Authority);
            return path;
        }

        private string Send(string subject, string strBody, string emailAddress)
        {
            MailMessage mail = new MailMessage();
            mail.Body = strBody;
            mail.IsBodyHtml = true;
            mail.From = new MailAddress(ConfigurationManager.AppSettings["MailFrom"], "Detail Window Cleaning");
            mail.To.Add(emailAddress);
            mail.Subject = subject;

            SmtpClient client = new SmtpClient();
            client.Host = ConfigurationManager.AppSettings["SmtpServer"];
            string message = "";

            switch (client.Host)
            {
                case "smtp.comcast.net":
                    client.Port = 587;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential("sheeand", "MSLKQcg");
                    break;

                case "relay-hosting.secureserver.net":
                    client.Port = 25;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential("webmaster@detailwindow.com", "tbitwog1");
                    break;
            }

                try
                {
                    client.Send(mail);
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }
                finally
                {
                    client.Dispose();
                }
                return message;
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
            SqlConnection objConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["detailConnectionString"].ConnectionString);
            string strSQL = String.Concat("UPDATE Customer SET PromoSent = '", DateTime.Now.ToShortDateString(), "', SentStatus = '", status, "' WHERE ID = ", ID);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);
            using (objConnection)
            {
                objConnection.Open();
                objCommand.ExecuteNonQuery();
                objConnection.Close();
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
