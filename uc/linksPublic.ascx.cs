using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Principal;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace detailwindow
{
    public partial class linksPublic : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here

            // Add MouseOver effects to links

            btnRegister.Attributes["onMouseOver"] = "src='../Images/RegisterBright.gif'";
            btnRegister.Attributes["onMouseOut"] = "src='../Images/RegisterNormal.gif'";
            btnEstimate.Attributes["onMouseOver"] = "src='../Images/GetBright.gif'";
            btnEstimate.Attributes["onMouseOut"] = "src='../Images/GetNormal.gif'";
            btnAppointment.Attributes["onMouseOver"] = "src='../Images/ScheduleBright.gif'";
            btnAppointment.Attributes["onMouseOut"] = "src='../Images/ScheduleNormal.gif'";
            btnLogin.Attributes["onMouseOver"] = "src='../Images/LoginBright.gif'";
            btnLogin.Attributes["onMouseOut"] = "src='../Images/LoginNormal.gif'";
            btnContactUs.Attributes["onMouseOver"] = "src='../Images/ContactBright.gif'";
            btnContactUs.Attributes["onMouseOut"] = "src='../Images/ContactNormal.gif'";

            Session["Name"] = "";
            Session["LastName"] = "";

            if (!IsPostBack)
            {
                // nForce=1 : the user was redirected here from another page upon a logout.
                int nForce = Convert.ToInt32(Request.QueryString["Force"]);

                // If the user was NOT redirected here from another page upon a logout:
                if (nForce != 1)
                {
                    // Test for an autologin cookie.  
                    HttpCookie Cookie = Request.Cookies["DetailWindow"];

                    // If the cookie exists,
                    if (Cookie != null)
                    {
                        // Retrieve the cookie values.
                        bool bAutoLogin = Convert.ToBoolean(Cookie.Values["AutoLogin"]);
                        string strUserName = Convert.ToString(Cookie.Values["UserName"]);
                        string strPassword = Convert.ToString(Cookie.Values["Password"]);

                        // If the user wants to log in automatically, then do it.
                        if (bAutoLogin)
                        {
                            // Populate the session variables.
                            // Declare the connection
                            SqlConnection objConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["detailConnectionString"].ConnectionString);

                            // Declare a null SqlDataReader.
                            SqlDataReader objReader = null;

                            try
                            {
                                // Open the connection
                                objConnection.Open();

                                // Fill session objects:
                                // Declare and create the command object
                                // SELECT the database data that relates to the UserName cookie value
                                SqlCommand objCommand = new SqlCommand("SELECT ID, Name, LastName, AccountType FROM Customer WHERE UserName='" + strUserName + "'", objConnection);

                                // Get a recordset.
                                objReader = objCommand.ExecuteReader();

                                // If the recordset exists (if the cookie matches a database record)...
                                if (objReader.Read())
                                {
                                    // Fill session variables.
                                    Session["ID"] = Convert.ToInt32(objReader["ID"]);
                                    Session["Name"] = Convert.ToString(objReader["Name"]);
                                    Session["LastName"] = Convert.ToString(objReader["LastName"]);
                                    Session["AccountType"] = Convert.ToInt32(objReader["AccountType"]);
                                    Session["Email"] = Convert.ToString(objReader["Email"]);
                                }
                                else
                                {
                                    lblErrorMessage.Text = "A user profile no longer exists for this customer.  Please try a new Username and Password.";
                                }
                            }
                            catch (Exception ex)
                            {
                                lblErrorMessage.Text = ex.Message.ToString();
                                Session["ErrorCode"] = "Customer ID: " + Session["ID"] +
                                    " <br /> Page: linksPublic <br /> Line: 100 <br /> Message: " + ex.Message.ToString();
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
                                SqlCommand objCommand = new SqlCommand(strSQL, objConnection);
                                objCommand.Parameters.AddWithValue("@ID", intID);

                                // Execute the UPDATE
                                objCommand.ExecuteNonQuery();

                            }
                            catch (Exception ex)
                            {
                                lblErrorMessage.Text = ex.Message.ToString();
                                Session["ErrorCode"] = "Customer ID: " + Session["ID"] +
                                    " <br /> Page: linksPublic <br /> Line: 142 <br /> Message: " + ex.Message.ToString();
                            }
                            finally
                            {
                                objConnection.Close();
                            }


                            Response.Redirect("default2.aspx");

                        }
                    }
                    // Otherwise, no cookie exists - the user does not want auto-login,
                    // and ID, Username and AccountType are unknown
                    // so these session variables have not yet been filled.
                }
                // If the user was redirected here from another page upon a logout, 
                // then don't bother retrieving the cookie values, and 
                // don't log-in the user again.
            }
        }

        protected void btnHome_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
        protected void btnAboutUs_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("AboutUs.aspx");
        }
        protected void btnEstimate_Click(object sender, EventArgs e)
        {
            // Set a session variable "Request" = "estimate"
            Session["Request"] = "estimate";
            Response.Redirect("Login.aspx");
        }
        protected void btnAppointment_Click(object sender, EventArgs e)
        {
            // Set a session variable "Request" = "appointment"
            Session["Request"] = "appointment";
            Response.Redirect("Login.aspx");
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
        protected void btnContactUs_Click(object sender, EventArgs e)
        {
            Response.Redirect("ContactUs.aspx");
        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
    }
}