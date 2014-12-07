using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace detailwindow
{
    public partial class linksMember : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here

            // Add MouseOver effects to links
            btnHome.Attributes["onMouseOver"] = "src='../Images/HomeBright.gif'";
            btnHome.Attributes["onMouseOut"] = "src='../Images/HomeNormal.gif'";
            btnAboutUs.Attributes["onMouseOver"] = "src='../Images/AboutBright.gif'";
            btnAboutUs.Attributes["onMouseOut"] = "src='../Images/AboutNormal.gif'";
            btnEstimate.Attributes["onMouseOver"] = "src='../Images/GetBright.gif'";
            btnEstimate.Attributes["onMouseOut"] = "src='../Images/GetNormal.gif'";
            btnAppointment.Attributes["onMouseOver"] = "src='../Images/ScheduleBright.gif'";
            btnAppointment.Attributes["onMouseOut"] = "src='../Images/ScheduleNormal.gif'";
            btnEditProfile.Attributes["onMouseOver"] = "src='../Images/EditBright.gif'";
            btnEditProfile.Attributes["onMouseOut"] = "src='../Images/EditNormal.gif'";
            btnContactUs.Attributes["onMouseOver"] = "src='../Images/ContactBright.gif'";
            btnContactUs.Attributes["onMouseOut"] = "src='../Images/ContactNormal.gif'";

            lblName.Text = Session["Name"].ToString() + " " + Session["LastName"].ToString() + "<br /> is logged in.";
            int iTest = testProfileContents();
            if (iTest == 0)
            {
                lblName.Visible = false;
            }
            else
            {
                lblName.Visible = true;
            }

            // Make Administrator link visible to administrators
            // (where Session ["AccountType" = 2] )

            btnAdmin.Visible = false;
            int intOne = 1;
            int AcctType = (int)Session["AccountType"];
            if (AcctType == intOne)
            {
                btnAdmin.Visible = true;
                btnAdmin.Attributes["onMouseOver"] = "src='../Images/CustomerBright.gif'";
                btnAdmin.Attributes["onMouseOut"] = "src='../Images/CustomerNormal.gif'";
            }

            btnLogOut.Visible = false;
            btnDeAutoLogin.Visible = false;
            // Look for the cookie that determines if user wants to log in automatically.
            HttpCookie Cookie = Request.Cookies["DetailWindow"];

            // If the cookie exists,
            if (Cookie != null)
            {
                // Expose lnkDeAutoLogin
                btnDeAutoLogin.Visible = true;
                btnDeAutoLogin.Attributes["onMouseOver"] = "src='../Images/LogoutBright.gif'";
                btnDeAutoLogin.Attributes["onMouseOut"] = "src='../Images/LogoutNormal.gif'";
            }
            else
            {
                // Expose lnkLogOut
                btnLogOut.Visible = true;
                btnLogOut.Attributes["onMouseOver"] = "src='../Images/LogoutBright.gif'";
                btnLogOut.Attributes["onMouseOut"] = "src='../Images/LogoutNormal.gif'";
            }

        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

        protected void btnHome_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("default2.aspx");
        }

        protected void btnAboutUs_Click(object sender, System.EventArgs e)
        {

            Response.Redirect("aboutUs2.aspx");
        }

        protected void btnLogOut_Click(object sender, System.EventArgs e)
        {
            // FormsAuthentication.SignOut();

            // Set query string "Force=1" to tell login.aspx not to log in the user again.
            Response.Redirect("default.aspx?Force=1");
        }

        protected void btnEstimate_Click(object sender, System.EventArgs e)
        {
            // Test to see if user profile has been filled out
            int iTest = testProfileContents();
            if (iTest == 1)
            {
                //User profile has been filled out
                Response.Redirect("estimate.aspx");
            }
            else
            {
                // User profile has not been filled out
                Response.Redirect("profile.aspx");
            }
        }

        protected void btnAppointment_Click(object sender, System.EventArgs e)
        {
            // Test to see if user profile has been filled out
            int iTest = testProfileContents();
            if (iTest == 1)
            {
                //User profile has been filled out
                Response.Redirect("appointment.aspx");
            }
            else
            {
                // User profile has not been filled out
                Response.Redirect("profile.aspx");
            }
        }

        protected void btnEditProfile_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("profile.aspx");
        }

        protected void btnDeAutoLogin_Click(object sender, System.EventArgs e)
        {
            // Look for the cookie that determines if user wants to log in automatically.
            HttpCookie Cookie = Request.Cookies["DetailWindow"];

            // If the cookie exists,
            if (Cookie != null)
            {
                // Get rid of the cookie.
                Cookie.Expires = DateTime.Now.AddYears(-10);
                Response.AppendCookie(Cookie);
            }

            // FormsAuthentication.SignOut();

            Response.Redirect("default.aspx");
        }

        protected void btnAdmin_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("admin.aspx");
        }

        protected int testProfileContents()
        {
            // This function returns a "1" if the user profile has been filled out
            int nRet = 0;

            // Declare the connection
            SqlConnection objConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["detailConnectionString"].ConnectionString);

            // Declare a null SqlDataReader.
            SqlDataReader objReader = null;

            try
            {
                // "Unbox" the Session object into an integer value
                int intID = (int)Session["ID"];

                // Open the connection
                objConnection.Open();

                // Declare and create the command object
                SqlCommand objCommand = new SqlCommand("SELECT Name FROM Customer WHERE ID=@ID", objConnection);
                objCommand.Parameters.AddWithValue("@ID", intID);

                // Get a recordset.
                objReader = objCommand.ExecuteReader();

                if (objReader.Read())
                {

                    string strName = Convert.ToString(objReader["Name"]);
                    if (strName != "")
                    {
                        nRet = 1;
                    }
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
                    " <br /> Page: linksMember <br /> Line: 221 <br /> Message: " + ex.Message.ToString();
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
            return (nRet);
        }

        protected void btnContactUs_Click(object sender, EventArgs e)
        {
            Response.Redirect("contactUs2.aspx");
        }
    }
}