using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace detailwindow
{
    public partial class contentProfile : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Redirect hackers from logging on as members
            if (Session["AccountType"] == null)
            {
                Response.Redirect("default.aspx");
            }

            if (!IsPostBack)
            {
                // Render form data
                RenderForm();

                // Fill the information label with an instruction to the user.
                int AcctType = (int)Session["AccountType"];

                if (txtName.Text != "")
                {
                    // There is a value in the required field txtName variable
                    // which means the user already has a profile and wants to edit it.
                    // Fill information label with an instruction to the user.
                    lblInfoMessage.Text = "Edit your profile below. &nbsp;Click 'Submit profile' when complete.";
                    lblHeading2.Text = "Edit Profile";
                }
                else
                {
                    // This is a new user
                    // Fill information label with an instruction to the user.
                    lblInfoMessage.Text = "To help us serve you, please enter the information requested below. &nbsp;Click 'Submit profile' when complete.";
                    lblHeading2.Text = "New User Profile";

                    // Add focus to Name textbox
                    txtName.Focus();
                }

            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)

            {

                Session["Name"] = txtName.Text;
                Session["LastName"] = txtLastName.Text;
                Session["Email"] = txtEmail.Text;

                // Use ConfigurationManager for localhost
                // Use WebConfigurationManager for remote host
                SqlConnection objConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["detailConnectionString"].ConnectionString);

                try
                {
                    // "Unbox" the Session object into an integer value
                    int intID = (int)Session["ID"];

                    // Open the connection.
                    objConnection.Open();

                    // Create a command string
                    string strSQL = "UPDATE Customer SET Name='" + txtName.Text +
                        "',LastName='" + txtLastName.Text + 
                        "',CompanyName='" + txtCompany.Text +
                        "',Subdivision='" + txtSubdivision.Text +
                        "',Address='" + txtAddress.Text +
                        "',City='" + txtCity.Text +
                        "',Zip='" + txtZip.Text +
                        "',Email='" + txtEmail.Text +
                        "',Phone1='" + txtPhone1.Text +
                        "',Phone2='" + txtPhone2.Text +
                        "',Phone3='" + txtPhone3.Text +
                        "',Fax='" + txtFax.Text +
                        "',Business=" + rdoBusiness.Checked +
                        ",Residence=" + rdoResidence.Checked +
                        " WHERE ID=@ID";

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
                        " <br /> Page: contentProfile <br /> Line: 98 <br /> Message: " + ex.Message.ToString();
                }
                finally
                {
                    objConnection.Close();
                }
                if (Session["Request"] == null)
                {
                    Response.Redirect("profileConfirm.aspx");
                }
                else
                {
                    string strReq = "";
                    strReq = Convert.ToString(Session["Request"]);
                    if (strReq == "appointment")
                    {
                        // The user originally requested to schedule an appointment
                        // Reset the session variable
                        // Redirect directly to appointment.aspx instead of confirmation page
                        Session["Request"] = "";
                        Response.Redirect("appointment.aspx");
                    }
                    else if (strReq == "estimate")
                    {
                        // The user originally requested an estimate
                        // Reset the session variable
                        // Redirect directly to estimate.aspx instead of confirmation page
                        Session["Request"] = "";
                        Response.Redirect("estimate.aspx");
                    }
                }
                Response.Redirect("profileConfirm.aspx");
            }

        }
        protected void RenderForm()
        {
            bool reminderOptOut = false;

            // Declare the connection
            // Use ConfigurationManager for localhost
            // Use WebConfigurationManager for remote host
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
                SqlCommand objCommand = new SqlCommand("SELECT * FROM Customer WHERE ID=@ID", objConnection);
                objCommand.Parameters.AddWithValue("@ID", intID);

                // Get a recordset.
                objReader = objCommand.ExecuteReader();

                if (objReader.Read())
                {
                    txtName.Text = Convert.ToString(objReader["Name"]);
                    txtLastName.Text = Convert.ToString(objReader["LastName"]);
                    txtCompany.Text = Convert.ToString(objReader["CompanyName"]);
                    txtSubdivision.Text = Convert.ToString(objReader["Subdivision"]);
                    txtAddress.Text = Convert.ToString(objReader["Address"]);
                    txtCity.Text = Convert.ToString(objReader["City"]);
                    txtZip.Text = Convert.ToString(objReader["Zip"]);
                    txtEmail.Text = Convert.ToString(objReader["Email"]);
                    txtPhone1.Text = Convert.ToString(objReader["Phone1"]);
                    txtPhone2.Text = Convert.ToString(objReader["Phone2"]);
                    txtPhone3.Text = Convert.ToString(objReader["Phone3"]);
                    txtFax.Text = Convert.ToString(objReader["Fax"]);
                    rdoBusiness.Checked = Convert.ToBoolean(objReader["Business"]);
                    rdoResidence.Checked = Convert.ToBoolean(objReader["Residence"]);
                    reminderOptOut = Convert.ToBoolean(objReader["ReminderOptOut"]);
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
                    " <br /> Page: contentProfile <br /> Line: 182 <br /> Message: " + ex.Message.ToString();
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

                if (reminderOptOut)
                {
                    GridView3.Visible = false;
                    lblReminderLabel.Visible = false;
                }
            }

        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("default2.aspx");
        }
    }
}