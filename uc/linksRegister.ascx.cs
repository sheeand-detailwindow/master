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

namespace detailwindow
{
    public partial class linksRegister : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here

            // Add MouseOver effects to links
            btnHome.Attributes["onMouseOver"] = "src='../Images/HomeBright.gif'";
            btnHome.Attributes["onMouseOut"] = "src='../Images/HomeNormal.gif'";
            btnAboutUs.Attributes["onMouseOver"] = "src='../Images/AboutBright.gif'";
            btnAboutUs.Attributes["onMouseOut"] = "src='../Images/AboutNormal.gif'";
            btnContactUs.Attributes["onMouseOver"] = "src='../Images/ContactBright.gif'";
            btnContactUs.Attributes["onMouseOut"] = "src='../Images/ContactNormal.gif'";


            // Add focus to username textbox
            txtUserName.Focus();
        }

        // Note: Scope is Protected (rather tha Private) since this user control
        // is called from another web form.
        protected void btnLogin_Click(object sender, System.EventArgs e)
        {
            // To allow users to still use the navigation controls if a validation error is thrown, set EnableClientScript="false".
            // This will keep the control from firing until the Login button is clicked.  Then,
            // if (!Page.IsValid) return; keeps the click event from firing if there is any validation control flag
            if (!Page.IsValid) return;
            
                // Call LoginUser() method which does the database access
                int nLogin = LogUser();

                // If the returned value is 0, the credentials are valid.
                if (nLogin == 0)
                {
                    //Get a non-case sensitive Username and password.
                    string strUserName = txtUserName.Text.Trim().ToUpper();
                    string strPassword = txtPassword.Text.Trim().ToUpper();

                    // If the user wants to activate AutoLogin,
                    if (chkAutoLogin.Checked)
                    {
                        // Create the AutoLogin authentication cookie and set values
                        HttpCookie Cookie = new HttpCookie("DetailWindow");
                        Cookie.Values.Add("UserName", strUserName);
                        Cookie.Values.Add("Password", strPassword);
                        Cookie.Values.Add("AutoLogin", Convert.ToString(chkAutoLogin.Checked));
                        Cookie.Expires = DateTime.MaxValue;
                        Response.AppendCookie(Cookie);
                    }

                    // Redirect to the next page
                    // But first check to see if the user wanted to request an appt.
                    // If so, a session variable should have been set in login.aspx.cs

                    string strReq = "";
                    strReq = Convert.ToString(Session["Request"]);

                    //// If the user requested a cleaning (appointment.aspx) from linksPublic.aspx                   
                    //if (strReq == "appointment")
                    //{
                    //    // If this is a new user loging in for the first time
                    //    //if (chkNewUser.Checked)
                    //    //{
                    //        // NEW USERS MUST HAVE AN ESTIMATE FIRST
                    //        // Change Session["Request"] variable
                    //        // Redirect to the edit profile page
                    //        Session["Request"] = "estimate";
                    //        Response.Redirect("profile.aspx");
                    //    //}
                    //    //else
                    //    //{
                    //    //    // Reset the session variable
                    //    //    // Redirect to the appointment form
                    //    //    Session["Request"] = "";
                    //    //    Response.Redirect("appointment.aspx");
                    //    //}
                    //}
                    //// Else if the user requested an estimate (estimate.aspx) from linksPublic.aspx
                    //else if (strReq == "estimate")
                    //{
                    //    // If this is a new user loging in for the first time
                    //    //if (chkNewUser.Checked)
                    //    //{
                    //    //    // Redirect straight to the edit profile page
                    //    //    // Retain Session["Request"] variable
                    //        Response.Redirect("profile.aspx");
                    //    //}
                    //    //else
                    //    //{
                    //    //    // Reset the session variable
                    //    //    // Redirect to the estimate form
                    //    //    Session["Request"] = "";
                    //    //    Response.Redirect("estimate.aspx");
                    //    //}
                    //}
                    //else
                    //{
                    //    // The user did not previously request an estimate or an appointment
                    //    // from linksPublic.ascx
                    //    // Refresh the home page with the "linksMembers" links box.
                        Response.Redirect("Profile.aspx");
                    }
                //}

                    // Login failure
                else if (nLogin == 1)
                {
                    lblErrorMessage.Text = "The Username/Password combination is incorrect.";
                }

                    // Attempt was made to create a new account using an existing UserName.
                else if (nLogin == 2)
                {
                    lblErrorMessage.Text = "The Username and/or Password is invalid";
                }
            
        }

        private int LogUser()
        {
            /*
             * Return values:
             * 0 = login successful
             * 1 = UserName is not in database
             * 2 = password mismatch
             * 3 = no UserName was given, or was too large
             */

            // Clear the error message labels.
            lblErrorMessage.Text = "";
            lblErrorMessage2.Text = "";

            // Create the connection object.
            OleDbConnection objConnection = new OleDbConnection(ConfigurationManager.AppSettings["ConnectString"]);

            // Declare a null SqlDataReader.
            OleDbDataReader objReader = null;

            // Get a non-case-sensitive UserName
            string strUserName = txtUserName.Text.Trim().ToUpper();

            // The user needs to enter something (whether a new user or not).
            // Bail out if they entered no UserName.
            if (strUserName.Length == 0)
            {
                //if (chkNewUser.Checked)
                //{
                    lblErrorMessage.Text = "Please enter the Username and Password you wish to use.";
                //}

                lblErrorMessage2.Text = "You must have at least one non-blank character in the UserName field.";
                return (3);
            }

            // Get a non-case-sensitive password.
            string strPassword = txtPassword.Text.Trim().ToUpper();

            // Default return value for LogUser() is 0 = login successful.
            int nRet = 0;

            try
            {
                // Open the connection.
                objConnection.Open();

                // Create a command object.
                OleDbCommand objCommand = new OleDbCommand("SELECT * FROM Customer WHERE UserName='" + strUserName + "'", objConnection);

                // Get a recordset.
                objReader = objCommand.ExecuteReader();

                // If there are records that match the query string, then:
                if (objReader.Read())
                {

                    // First see if they specified that this is a new user.
                    //if (chkNewUser.Checked)
                    //{
                        lblErrorMessage.Text = "That Username already exists.  Please try another name.";
                        return (3);
                    //}
                    //else
                    //{
                    //    // There is a Username in the database that is a match.
                    //    // Compare the entered password with the password from the database.
                    //    if (strPassword != Convert.ToString(objReader["Passwd"]).ToUpper())
                    //    {
                    //        // Return value is changed to 2 = Password is incorrect.
                    //        nRet = 2;
                    //    }
                    //    else
                    //    {
                    //        // The user has successfully been identified.
                    //        // Set the session variables.
                    //        Session["ID"] = Convert.ToInt32(objReader["ID"]);
                    //        Session["Name"] = Convert.ToString(objReader["Name"]);
                    //        Session["AccountType"] = Convert.ToInt32(objReader["AccountType"]);
                    //    }
                    //}
                }
                else
                // No records match the query string
                {
                    // Create new user record.
                    //if (chkNewUser.Checked)
                    //{
                        // Close the reader.
                        objReader.Close();

                        // Set Session ["ID"] = -1 for debugging purposes
                        int LastID = -1;
                        Session["ID"] = LastID;

                        // Cookie has already been placed.
                        // User will be redirected via FormsAuthentication.RedirectFromLoginPage
                        // Then, new user will have to fill out profile form.

                        // Scrubs the ' characters
                        strUserName = SafeSQL(strUserName);
                        strPassword = SafeSQL(strPassword);

                        // Create a command object to insert Username and Password.
                        objCommand = new OleDbCommand("INSERT INTO Customer (UserName, Passwd) VALUES (" + strUserName + ", " + strPassword + ");", objConnection);

                        // Execute the query.
                        objCommand.ExecuteNonQuery();

                        // Create a command object to get the identity value of the row that has just been inserted.
                        // The system variable @@IDENTITY in SQL returns the Identity column value for the most recent row.
                        objCommand = new OleDbCommand("SELECT @@IDENTITY", objConnection);
                        LastID = Convert.ToInt32(objCommand.ExecuteScalar());

                        // Store the ID, UserName & Account Type in session variables.
                        Session["ID"] = LastID;
                        Session["Name"] = "";
                        Session["LastName"] = "";
                        Session["AccountType"] = 0;
                        Session["Email"] = "";
                    //}
                    //else
                    //{
                    //    // UserName is not in database
                    //    nRet = 1;
                    //}
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message.ToString();
                Session["ErrorCode"] = "Customer ID: " + Session["ID"] +
                    " <br /> Page: linksRegister <br /> Line: 254 <br /> Message: " + ex.Message.ToString();
                Response.Redirect("ErrorPublic.aspx");
                // nRet = 3;
            }
            finally
            {
                // Close the reader.
                if (objReader != null)
                {
                    objReader.Close();
                }
                // Close the connection.
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }

            if (nRet == 3) return(3);

            // Update the database LastLogin field

            try
            {
                // "Unbox" the Session object into an integer value
                int intID = (int)Session["ID"];

                // Open the connection.
                objConnection.Open();

                // Create a command string
                string strNow = DateTime.Now.ToShortDateString();
                string strSQL = "UPDATE Customer SET LastLogin='" + strNow +
                "' WHERE ID=@ID";

                //  Declare and create the command object.
                OleDbCommand objCommand = new OleDbCommand(strSQL, objConnection);
                objCommand.Parameters.AddWithValue("@ID", intID);

                // Execute the UPDATE
                objCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message.ToString();
                Session["ErrorCode"] = "Customer ID: " + Session["ID"] +
                    " <br /> Page: linksRegister <br /> Line: 298 <br /> Message: " + ex.Message.ToString();
            }
            finally
            {
                objConnection.Close();
            }


            return (nRet);

        }

        // This method changes the extra information user interface objects.
        // When the check box is selected the extra objects appear, otherwise
        // they are hidden.
        private void chkNewUser_CheckedChanged(object sender, System.EventArgs e)
        {
            //if (chkNewUser.Checked)
            //{
                chkAutoLogin.Checked = false;
                chkAutoLogin.Enabled = false;
            //}
            //else
            //{
            //    chkAutoLogin.Enabled = true;
            //}
        }

        // This method scrubs the ' character.
        string SafeSQL(string sql)
        {
            if (sql == null)
            {
                sql = "''";
                return sql;
            }
            if (sql == "")
            {
                return sql;
            }
            return string.Format("'{0}'", sql.Replace("'", "''"));
        }


        protected void btnHome_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("default.aspx");
        }

        protected void btnAboutUs_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("aboutUs.aspx");
        }
        protected void btnContactUs_Click(object sender, EventArgs e)
        {
            Response.Redirect("contactUs.aspx");
        }

    }
}