using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Net.Mail;

namespace detailwindow
{
    public partial class contentEst : System.Web.UI.UserControl
    {
        protected int _intLine;

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSubmit_Click(object sender, System.EventArgs e)
        {
            // Declare the connection
            OleDbConnection objConnection = new OleDbConnection(ConfigurationManager.AppSettings["ConnectString"]);

            // Declare a null SqlDataReader.
            OleDbDataReader objReader = null;

            try
            {
                // "Unbox" the Session object into an integer value
                int intID = (int)Session["ID"];

                // Open the connection
                objConnection.Open();

                // Declare and create the command object
                OleDbCommand objCommand = new OleDbCommand("SELECT * FROM Customer WHERE ID=@ID", objConnection);
                objCommand.Parameters.AddWithValue("@ID", intID);

                // Get a recordset.
                objReader = objCommand.ExecuteReader();

                if (objReader.Read())
                {
                    _intLine = 49;

                    // Compose the email message to the administrator.
                    string strName = Convert.ToString(objReader["Name"]);
                    string strLastName = Convert.ToString(objReader["LastName"]);
                    string strCompany = Convert.ToString(objReader["CompanyName"]);
                    string strSubdivision = Convert.ToString(objReader["Subdivision"]);
                    string strAddress = Convert.ToString(objReader["Address"]);
                    string strCity = Convert.ToString(objReader["City"]);
                    string strZip = Convert.ToString(objReader["Zip"]);
                    string strPhone1 = Convert.ToString(objReader["Phone1"]);
                    string strPhone2 = Convert.ToString(objReader["Phone2"]);
                    string strPhone3 = Convert.ToString(objReader["Phone3"]);
                    string strEmail = Convert.ToString(objReader["Email"]);

                    SmtpClient smtp = new SmtpClient();
                    MailMessage mail = new MailMessage();
                    smtp.Host = ConfigurationManager.AppSettings["SmtpServer"];
                    mail.To.Add(ConfigurationManager.AppSettings["MailTo"]);
                    mail.From = new MailAddress(ConfigurationManager.AppSettings["MailFrom"]);
                    mail.IsBodyHtml = true;
                    mail.Subject = "Customer: " + strName + " " + strLastName + " - Requesting An Estimate";
                    mail.Body = "<font style='font-family:Arial'> <br />";
                    mail.Body += "This is an automated response from www.detailwindow.com  <br />  <br />";
                    mail.Body += "The following customer wants an ESTIMATE: <br /> <br />";
                    mail.Body += "  " + strName + " " + strLastName + "<br />";
                    mail.Body += "  " + strCompany + "<br />";
                    _intLine = 75;
                    if (strCompany.Length > 1)
                    {
                        mail.Body += " Company: " + strCompany + "<br />";

                    }
                    _intLine = 81;
                    if (strSubdivision == "none")
                    {
                        mail.Body += " &#040;no subdivision&#041;<br />";
                    }
                    else if (strSubdivision.Length > 1)
                    {
                        mail.Body += strSubdivision + " subdivision<br />";
                    }
                    _intLine = 90;
                    mail.Body += "  " + strAddress + "<br />";
                    _intLine = 92;
                    mail.Body += "  " + strCity + "  " + strZip + "<br /> <br />";
                    _intLine = 90;
                    mail.Body += "<br /><br /><font style='text-decoration:underline'>Inside</font><br />";
                    _intLine = 96;
                    if (chkAllWindows.Checked)
                    {
                        mail.Body += "- Windows <br />";
                    }
                    _intLine = 101;
                    if (chkChandelier.Checked)
                    {
                        mail.Body += "- Chandelier(s) <br />";
                    }
                    _intLine = 106;
                    if (chkIntLighting.Checked)
                    {
                        mail.Body += "- Inside light fixtures <br />";
                    }
                    _intLine = 111;
                    if (chkFans.Checked)
                    {
                        mail.Body += "- Ceiling fan(s) <br />";
                    }
                    _intLine = 116;
                    if (chkBlinds.Checked)
                    {
                        mail.Body += "- Wood blinds <br />";
                    }
                    _intLine = 121;
                    if (chkDeepClean.Checked)
                    {
                        mail.Body += "- Interior deep cleaning <br />";
                    }
                    _intLine = 131;
                    mail.Body += "<br /><br /><font style='text-decoration:underline'>Outside</font><br />";
                    _intLine = 133;
                    if (chkOutside.Checked | chkAllWindows.Checked)
                    {
                        mail.Body += "- Windows <br />";
                    }
                    _intLine = 138;
                    if (chkExtLighting.Checked)
                    {
                        mail.Body += "- Outside light fixtures <br />";
                    }
                    _intLine = 143;
                    if (chkGutters.Checked)
                    {
                        mail.Body += "- Gutters <br />";
                    }
                    _intLine = 148;
                    mail.Body += "<br /><br /><font style='text-decoration:underline'>Dates, days of week</font><br />";
                    _intLine = 155;
                    if (TextBox1.Text == "")
                    {
                        mail.Body += " - None -";
                    }
                    else
                    {
                        mail.Body += Server.HtmlEncode(TextBox1.Text) + "<br />";
                    }
                    _intLine = 164;
                    mail.Body += "<br /><br /><font style='text-decoration:underline'>Comments, questions</font><br />";
                    _intLine = 166;
                    if (TextBox2.Text == "")
                    {
                        mail.Body += " - None -";
                    }
                    else
                    {
                        mail.Body += Server.HtmlEncode(TextBox2.Text) + "<br />";
                    }
                    _intLine = 175;
                    if (rdoPhone.Checked)
                    {
                        mail.Body += "<br /> <br />The customer prefers to be contacted by PHONE: <br />Home phone: " + strPhone1 + " <br />Cell phone: " + strPhone2 + " <br />Work phone: " + strPhone3 + " <br />";
                    }
                    else if (rdoEmail.Checked)
                    {
                        mail.Body += "<br /> <br />Please contact the customer by E-MAIL: <a href='mailto:" + strEmail + "?subject=Message From www.DetailWindow.com'>" + strEmail + "</a> <br />Home phone: " + strPhone1 + " <br />Cell phone: " + strPhone2 + " <br />Work phone: " + strPhone3 + " <br /><br /></font>";
                    }
                    else
                    {
                        mail.Body += "<br /> <br />The customer has no contact preference: <br />Home phone: " + strPhone1 + " <br />Cell phone: " + strPhone2 + " <br />Work phone: " + strPhone3 + " <br /><a href='mailto:" + strEmail + "?subject=Message From www.DetailWindow.com'>" + strEmail + "</a>";
                    }

                    // Sent the email message to the administrator.
                    _intLine = 190;
                    smtp.Send(mail);

                }
                else
                {
                    lblErrorMessage.Text = "Could not retrieve user record.";
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message.ToString();
                Session["ErrorCode"] = "Customer ID: " + Session["ID"] +
                    " <br /> Page: contentEst <br /> Line: " + _intLine + "<br /> Message: " + ex.Message.ToString();
            }
            finally
            {
                if (objReader != null)
                {
                    objReader.Close();
                }
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
            Response.Redirect("appointmentConfirm.aspx");
        }
    }
}