using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.ServiceModel.Channels;

namespace detailwindow.api
{
    /// <summary>
    /// Summary description for EmailService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]

    public class EmailService : System.Web.Services.WebService
    {
        [WebMethod]
        public string SendEmail(string Type, string Rendition, string Row)
        {
            string message = "";
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

            return message;
        }

        public string SendAnotherEmail(string Type, string Rendition, string Row)
        {
            string message = "";
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

            return message;
        }

    }
}
