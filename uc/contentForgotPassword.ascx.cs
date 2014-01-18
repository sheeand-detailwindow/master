using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

namespace detailwindow.uc
{
    public partial class contentForgotPassword : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            // Create the connection object.
            OleDbConnection objConnection = new OleDbConnection(ConfigurationManager.AppSettings["ConnectString"]);

            // Declare a null SqlDataReader.
            OleDbDataReader objReader = null;

            // Open the connection.
            objConnection.Open();

            // Create a command string
            string strFirstName = txtName.Text;
            string strLastName = txtLastName.Text;
            string strZIP = txtZip.Text;
            string strSQL = "SELECT USERNAME, PASSWD, EMAIL FROM CUSTOMER WHERE (NAME=@NAME) AND (LASTNAME=@LASTNAME) AND (ZIP=@ZIP)";


            //  Declare and create the command object.
            OleDbCommand objCommand = new OleDbCommand(strSQL, objConnection);
            objCommand.Parameters.AddWithValue("@NAME", strFirstName);
            objCommand.Parameters.AddWithValue("@LASTNAME", strLastName);
            objCommand.Parameters.AddWithValue("@ZIP", strZIP);

            try
            {
                // Execute the UPDATE
                objReader = objCommand.ExecuteReader();

            if (objReader.Read())
            {
                lblInvalid.Visible = false;

                // The user exists in the database
                string strUsername = objReader.GetValue(0).ToString().ToLowerInvariant();
                string strPassword = objReader.GetValue(1).ToString().ToLowerInvariant();
                string strEmail = objReader.GetValue(2).ToString();

                // Send email
                SmtpClient client = new SmtpClient();
                MailMessage mail = new MailMessage();
                client.Host = ConfigurationManager.AppSettings["SmtpServer"];
                mail.From = new MailAddress(ConfigurationManager.AppSettings["MailFrom"]);
                mail.To.Add(strEmail);
                mail.IsBodyHtml = true;
                mail.Subject = "Message from www.DetailWindow.com";
                mail.Body = "<font style='font-family:Arial'> <br /><br />";
                mail.Body += "You requested that your username and password be sent to you.<br /><br />";
                mail.Body += "Your username is " + strUsername + "<br /><br />";
                mail.Body += "Your password is " + strPassword + "<br /><br />";
                mail.Body += "Please revisit <a href='http://www.detailwindow.com/login.aspx'>www.DetailWindow.com</a> and log in.<br /><br />";
                client.Send(mail);
                Response.Redirect("EmailConfirm.aspx");

            }
            else
            {
                // The user does not exist in the database
                lblInvalid.Visible = true;
            }
            }
            catch (Exception ex)
            {
                Session["ErrorCode"] = "LinksLogin:297 - " + ex.Message.ToString();
                Session["ErrorCode"] = "Customer ID: " + Session["ID"] +
                    " <br /> Page: contentProfile <br /> Line: 308 <br /> Message: " + ex.Message.ToString();
            }
            finally
            {
                // Close the reader.
                if (objReader != null)
                {
                    objReader.Close();
                }
                objConnection.Close();
            }

        }
    }
}