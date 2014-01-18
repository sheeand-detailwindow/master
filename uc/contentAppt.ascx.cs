using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Net.Mail;

namespace detailwindow
{
    public partial class contentAppt : System.Web.UI.UserControl
    {
        protected int _intLine;
        protected SmtpClient smtp = new SmtpClient();

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
                    _intLine = 47;
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

                    MailMessage mail = new MailMessage();
                    smtp.Host = ConfigurationManager.AppSettings["SmtpServer"];
                    mail.To.Add(ConfigurationManager.AppSettings["MailTo"]);
                    mail.From = new MailAddress(ConfigurationManager.AppSettings["MailFrom"]);
                    mail.IsBodyHtml = true;
                    mail.Subject = "Customer: " + strName + " " + strLastName + " - Requesting a Cleaning";
                    mail.Body = "<font style='font-family:Arial'> <br />";
                    mail.Body += "This is an automated response from www.detailwindow.com  <br />  <br />";
                    mail.Body += "The following customer wants a CLEANING: <br /> <br />";
                    mail.Body += "  " + strName + " " + strLastName + "<br />";
                    _intLine = 72;
                    if (strCompany.Length > 1)
                    {
                        mail.Body += " Company: " + strCompany + "<br /> ";

                    }
                    _intLine = 78;
                    if (strSubdivision == "none")
                    {
                        mail.Body += " &#040;no subdivision&#041;<br />";
                    }
                    else if (strSubdivision.Length > 1)
                    {
                        mail.Body += strSubdivision + " subdivision<br />";
                    }
                    _intLine = 89;
                    mail.Body += "  " + strAddress + "<br />";
                    _intLine = 91;
                    mail.Body += "  " + strCity + "  " + strZip + "<br /> <br />";
                    if (rdoMorning.Checked)
                    {
                        mail.Body += "Prefers a MORNING <br />";
                    }
                    _intLine = 97;
                    if (rdoAfternoon.Checked)
                    {
                        mail.Body += "Prefers an AFTERNOON <br />";
                    }
                    _intLine = 102;
                    if (rdoEither.Checked)
                    {
                        mail.Body += "Either MORNING or AFTERNOON <br />";
                    }
                    _intLine = 107;
                    mail.Body += "<br /><font style='text-decoration:underline'>Inside</font><br />";
                    _intLine = 109;
                    if (chkAllWindows.Checked)
                    {
                        mail.Body += "- Windows <br />";
                    }
                    _intLine = 114;
                    if (chkChandelier.Checked)
                    {
                        mail.Body += "- Chandelier(s) <br />";
                    }
                    _intLine = 119;
                    if (chkIntLighting.Checked)
                    {
                        mail.Body += "- Inside light fixtures <br />";
                    }
                    _intLine = 124;
                    if (chkFans.Checked)
                    {
                        mail.Body += "- Ceiling fan(s) <br />";
                    }
                    _intLine = 129;
                    if (chkBlinds.Checked)
                    {
                        mail.Body += "- Wood blinds <br />";
                    }
                    _intLine = 134;
                    if (chkHouseclean.Checked)
                    {
                        mail.Body += "- House cleaning services <br />";
                    }
                    _intLine = 139;
                    if (chkDeepClean.Checked)
                    {
                        mail.Body += "- Interior deep cleaning <br />";
                    }
                    _intLine = 144;
                    mail.Body += "<br /><font style='text-decoration:underline'>Outside</font><br />";
                    _intLine = 146;
                    if (chkOutside.Checked | chkAllWindows.Checked)
                    {
                        mail.Body += "- Windows <br />";
                    }
                    _intLine = 151;
                    if (chkExtLighting.Checked)
                    {
                        mail.Body += "- Outside light fixtures <br />";
                    }
                    _intLine = 156;
                    if (chkGutters.Checked)
                    {
                        mail.Body += "- Gutters <br />";
                    }
                    _intLine = 161;
                    mail.Body += "<br /><br /><font style='text-decoration:underline'>Dates, days of week</font><br />";
                    _intLine = 168;
                    if (TextBox1.Text == "")
                    {
                        mail.Body += " - None -";
                    }
                    else
                        _intLine = 174;
                    {
                        mail.Body += Server.HtmlEncode(TextBox1.Text) + "<br />";
                    }
                    _intLine = 178;
                    mail.Body += "<br /><br /><font style='text-decoration:underline'>Comments, questions</font><br />";
                    _intLine = 180;
                    if (TextBox2.Text == "")
                    {
                        mail.Body += " - None -";
                    }
                    else
                        _intLine = 186;
                    {
                        mail.Body += Server.HtmlEncode(TextBox2.Text) + "<br />";
                    }
                    _intLine = 190;
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
                    _intLine = 199;

                    // Without this the connection is idle too long and not terminated, times out at the server and gives sequencing errors
                    // It is fixed in .NET Framework 4 RC
                    // Note: It is strongly recommend to NOT use the workaround of setting MaxIdleTime or ConnectionLimit if you're on an IIS7 server. Setting these properties to 1 on IIS7 leads to a very prolonged period of high CPU. My workaround is to wrap Send() in a Try-Catch, and if an exception occurs, I immediately re-try Send() ing. On the second attempt a new connection to the mail server is opened up and the email goes thru. Btw, the exception I often receive is:
                    // "Unable to write data to the transport connection: An existing connection was forcibly closed by the remote host."
                    // The exception occurs when .NET doesn't properly close its connection to the mail server, and then when attempting to send a second email about 30 seconds later, .NET tries using the same open connection which the mail server finally closed on its end. .NET throws that error when it discovers its connection to the mail server was closed. If .NET would simply issue a QUIT command (per the spec) and disconnect from the mail server, this exception would not occur. 
                    smtp.ServicePoint.MaxIdleTime = 1; 
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
                    " <br /> Page: contentAppt <br /> Line: " + _intLine + "<br /> Message: " + ex.Message.ToString();
                Response.Redirect("ErrorPublic.aspx");
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