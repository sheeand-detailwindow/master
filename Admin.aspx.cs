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
using detailwindow.api;

namespace detailwindow
{
    public partial class Admin : System.Web.UI.Page
    {
        private EmailService client = new EmailService();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Redirect hackers from logging on as members
            if (Session["AccountType"] == null)
            {
                Response.Redirect("default.aspx");
            }
            else
            {
                object objCache = Cache["Data"];
                if (objCache == null)
                {
                    client.LoadCustomerCache();
                }
            }
        }

        //private void LoadCacheCompleted(IAsyncResult arg)
        //{
        //    EmailService client = arg.AsyncState as EmailService;
        //    if (client != null)
        //    {

        //    }
        //}
    }
}
