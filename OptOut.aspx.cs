using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Net.Mail;

namespace detailwindow
{
    public partial class OptOut : System.Web.UI.Page
    {
        private string Hash = "P@@Sw0rd";
        private string SaltKey = "S@LT&KEY";
        private string VIKey = "@1B2c3D4e5F6g7H8";

        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["e"];
            string t = Request.QueryString["t"];
            string type = "";

            if (String.IsNullOrEmpty(id))
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                id = Decrypt(id);
                SqlConnection objConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["detailConnectionString"].ConnectionString);
                string strSQL = "";
                try
                {
                    objConnection.Open();

                    switch (t)
                    {
                        case "p":
                            strSQL = String.Concat("UPDATE CUSTOMER SET [SpecialsOptOut] = TRUE WHERE ID = ", id);
                            lblType.Text = type = "promotional";
                            break;
                        case "r":
                            strSQL = String.Concat("UPDATE CUSTOMER SET [ReminderOptOut] = TRUE WHERE ID = ", id);
                            lblType.Text = type = "reminder";
                            break;
                    }

                    SqlCommand objCommand = new SqlCommand(strSQL, objConnection);
                    objCommand.ExecuteNonQuery();

                    SendEmailToWebMaster(id, type);
                }
                catch (Exception ex)
                {
                    Session["ErrorCode"] = ex.Message;
                    Response.Redirect("ErrorPublic.aspx");
                }
                finally
                {
                    objConnection.Close();
                }
            }
        }

        private void SendEmailToWebMaster(string id, string type)
        {
            string strBody = String.Concat("Client ", id, " has opted out of ", type, " emails.");
            MailMessage mail = new MailMessage();
            mail.Body = strBody;
            mail.From = new MailAddress(ConfigurationManager.AppSettings["MailFrom"], "Detail Window Cleaning");
            mail.Subject = "Opt Out Confirmation";
            mail.To.Add(ConfigurationManager.AppSettings["MailToWebmaster"]);
            SmtpClient client = new SmtpClient();
            client.Host = ConfigurationManager.AppSettings["SmtpServer"];
            if (ConfigurationManager.AppSettings["SmtpServer"] == "smtp.comcast.net")
            {
                client.Port = 587;
                client.UseDefaultCredentials = false;
                System.Net.NetworkCredential cred = new System.Net.NetworkCredential("sheeand", "MSLKQcg");
                client.Credentials = cred;
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
                    Response.Redirect("ErrorPublic.aspx");
                }
                finally
                {
                    client.Dispose();
                }
            }
        }

        private string Decrypt(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(Hash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }

    }
}