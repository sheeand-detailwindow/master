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
using System.Diagnostics;

namespace detailwindow
{
    public partial class contentLogin : System.Web.UI.UserControl
    {
        //        protected void Page_Load(object sender, EventArgs e)
        //       {
        //            try
        //            {
        //            }
        //            catch (Exception err)
        //            {
        //                EventLog log = new EventLog();
        //                log.Source = "contentLogin";
        //                log.WriteEntry(err.Message, EventLogEntryType.Error);
        //            }
        //        }

        protected void lnkRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
    }
}