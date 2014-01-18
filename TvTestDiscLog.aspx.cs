using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using detailwindow.com.mytvtestdisc.www;

namespace detailwindow
{
    public partial class TvTestDiscLog : System.Web.UI.Page
    {
        //private string xml { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            string xml = "";
            WebService1 TvTestDisc = new WebService1();
            xml = TvTestDisc.EventLog();
            DataSet ds = new DataSet();
            ds.ReadXml(new XmlTextReader(new StringReader(xml)));

            DateTime dateTimeLowerLimit = DateTime.Now.AddMonths(-1);
            string expression = String.Concat("Convert(TIMESTAMP, System.DateTime) > '", dateTimeLowerLimit, "'");

            for (int i = ds.Tables["EVENT_LOG"].Rows.Count - 1; i > -1 ; i--)
            {
                DateTime timestamp = Convert.ToDateTime(ds.Tables["EVENT_LOG"].Rows[i]["TIMESTAMP"]);
                if (timestamp < dateTimeLowerLimit)
                {
                    ds.Tables["EVENT_LOG"].Rows[i].Delete();
                }
            }

            for (int i = 0; i < ds.Tables["EVENT_LOG"].Rows.Count; i++)
            {
                ds.Tables["EVENT_LOG"].Rows[i]["TIMESTAMP"] = String.Format("{0:M/dd/yyyy  hh:mm}", Convert.ToDateTime(ds.Tables["EVENT_LOG"].Rows[i]["TIMESTAMP"]));
            }

            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
    }
}