using BilklPaymentWenFormPortal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BilklPaymentWenFormPortal
{
    public partial class WebPaymentLoginPage : System.Web.UI.Page
    {
         protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // Get user input from form
                string userName = txtUsernameOrEmail.Text.Trim();
                string password = txtPassword.Text.Trim();
                var Service = new Api.BillPaymentApiEndPoint();


                var response = Service.LoginUser(userName, password);

                if (response != null && response.Success)
                {
                    int roleId = response.Data;

                    // Now use roleId to redirect user
                    switch (roleId)
                    {

                        case 1:
                            Response.Redirect("~/AdminPages/AdminDashboard.aspx");
                            break;
                        case 2:
                            Response.Redirect("~/UserDashboard.aspx");
                            break;
                        case 3:
                            Response.Redirect("~/VendorPages/Vendor.aspx");
                            break;
                        case 4:
                            Response.Redirect("~/Customer.aspx");
                            break;
                        default:
                            lblMessage.Text = "Login failed or unknown role. Please contact support.";
                            break;
                    }
                }
            }
            catch (ArgumentException ex)
            {
                lblMessage.Text = $"Input Error: {ex.Message}";
            }
            catch (UnauthorizedAccessException ex)
            {
                lblMessage.Text = $"Access Denied: {ex.Message}";
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Unexpected Error: {ex.Message}";
            }
        }

    }
}