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
                string userName = txtUsernameOrEmail.Text.Trim();
                string password = txtPassword.Text.Trim();

                var service = new Api.BillPaymentApiEndPoint();
                var response = service.LoginUser2(userName, password);  // Call LoginUser2

                if (response != null && response.Success && response.Data != null)
                {
                    var loginInfo = response.Data;

                    Session["UserID"] = loginInfo.UserID;
                    Session["UserName"] = userName;
                    Session["RoleID"] = loginInfo.RoleID;

                    // Optional: If your BillPaymnet object contains VendorName or other info, set here
                    if (!string.IsNullOrEmpty(loginInfo.VendorName))
                    {
                        Session["VendorName"] = loginInfo.VendorName;
                    }

                    switch (loginInfo.RoleID)
                    {
                        case 1:
                            Response.Redirect("~/AdminPages/AdminDashboard.aspx");
                            break;
                        case 2:
                            Response.Redirect("~/UserDashboard.aspx");
                            break;
                        case 3:
                            Response.Redirect("~/NewForm/Vendor.aspx");
                            break;
                        case 4:
                            Response.Redirect("~/CustomerPages/Customer.aspx");
                            break;
                        default:
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            lblMessage.Text = "Login failed or unknown role.";
                            break;
                    }
                }
                else
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Invalid username or password.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Login error: " + ex.Message;
            }
        }


    }
}