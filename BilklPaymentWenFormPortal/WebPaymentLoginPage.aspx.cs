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

                // Call your Web Service login method
                var service = new Api.BillPaymentApiEndPoint();

                var response = service.LoginUser(userName, password);

                if (response != null && response.Success)
                {
                    int roleId = response.Data;

                    
                    Session["UserID"] = roleId; 
                    Session["UserName"] = userName;
                    Session["RoleID"] = roleId;

                    

                    // Redirect based on role
                    switch (roleId)
                    {
                        case 1: // Admin
                            Response.Redirect("~/AdminPages/AdminDashboard.aspx");
                            break;
                        case 2: // User
                            Response.Redirect("~/UserDashboard.aspx");
                            break;
                        case 3: // Vendor
                            Response.Redirect("~/VendorPages/Vendor.aspx");
                            break;
                        case 4: // Customer
                            Response.Redirect("~/CustomerPages/Customer.aspx");
                            break;
                        default:
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            lblMessage.Text = "Login failed or unknown role. Please contact support.";
                            break;
                    }
                }
                else
                {
                    // Login failed
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Invalid username or password.";
                }
            }
            catch (ArgumentException ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = $"Input Error: {ex.Message}";
            }
            catch (UnauthorizedAccessException ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = $"Access Denied: {ex.Message}";
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = $"Unexpected Error: {ex.Message}";
            }
        }


    }
}