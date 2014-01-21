using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.ServiceModel.Channels;
using Newtonsoft.Json.Serialization;
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
        public string SendEmail(string Type, string Rendition, string Row)
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

            string test = String.Concat(LastName, " ", Email);


            switch (Rendition)
            {
                case "Live":
                    switch (Type)
                    {
                        case "Reminder":
                            break;

                        case "JanFeb":
                            break;

                        case "March":
                            break;

                        case "July":
                            break;
                    }

                    break;

                case "WebmasterTest":
                    switch (Type)
                    {
                        case "Reminder":
                            break;

                        case "JanFeb":
                            break;

                        case "March":
                            break;

                        case "July":
                            break;
                    }
                    
                    break;

                case "AdministratorTest":
                    switch (Type)
                    {
                        case "Reminder":
                            break;

                        case "JanFeb":
                            break;

                        case "March":
                            break;

                        case "July":
                            break;
                    }
                    
                    break;
            }

            return null;
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
