using BilklPaymentWenFormPortal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BilklPaymentWenFormPortal.AdminPages
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnCreateVendor_Click(object sender, EventArgs e)
        {
            try
            {
                var service = new Api.BillPaymentApiEndPoint();

                string vendorCode = txtVendorCode.Text.Trim();
                string vendorName = txtVendorName.Text.Trim();
                string contactEmail = txtVendorEmail.Text.Trim();
                string contactPhone = txtVendorPhone.Text.Trim();
                decimal balance = string.IsNullOrEmpty(txtVendorBalance.Text) ? 0 : Convert.ToDecimal(txtVendorBalance.Text);
                string password = txtVendorPassword.Text.Trim();
                int createdBy = Convert.ToInt32(Session["UserID"]);

                var response = service.CreateVendor(vendorCode, vendorName, contactEmail, contactPhone, password, balance, createdBy);


                lblVendorMessage.ForeColor = System.Drawing.Color.Green;
                lblVendorMessage.Text = "Vendor created successfully!";
                ClearVendorForm();

         lblVendorMessage.Text = "Failed to create vendor: " + response.Message;
                //}
            }
            catch (Exception ex)
            {
                lblVendorMessage.ForeColor = System.Drawing.Color.Red;
                lblVendorMessage.Text = "Error: " + ex.Message;
            }
        }




        // Create User
        protected void btnCreateCustomer_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string email = txtUserEmail.Text.Trim();
            string password = txtUserPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return;

            //DatabaseHelper.CreateUser(username, email, password);
            txtUserName.Text = txtUserEmail.Text = txtUserPassword.Text = "";
        }

        //utility
        protected void btnCreateUtility_Click(object sender, EventArgs e)
        {
            try
            {
               
              

                string utilityName = txtUtilityName.Text.Trim();
                string utilityCode = txtUtilityCode.Text.Trim();
                int createdBy = Convert.ToInt32(Session["UserID"]);

                if (string.IsNullOrEmpty(utilityName) || string.IsNullOrEmpty(utilityCode))
                {
                    Label1.ForeColor = System.Drawing.Color.Red;
                    Label1.Text = "All fields are required.";
                    return;
                }

                var service = new Api.BillPaymentApiEndPoint(); 

                var response = service.CreateUtility(utilityName, utilityCode, createdBy);

                if (response.Success)
                {
                    Label1.ForeColor = System.Drawing.Color.Green;
                    Label1.Text = "Utility created successfully.";

                    
                    txtUtilityName.Text = "";
                    txtUtilityCode.Text = "";

                   
                }
                else
                {
                    Label1.ForeColor = System.Drawing.Color.Red;
                    Label1.Text = "Error: " + response.Message;
                }
            }
            catch (Exception ex)
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "Exception: " + ex.Message;
            }
        }



        // Search Vendors
        protected void txtSearchVendor_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearchVendor.Text.Trim();
            LoadVendors(keyword);
        }

        // Load Vendors (with optional search)
        private void LoadVendors(string keyword = "")
        {
            //DataTable dt = DatabaseHelper.GetVendors(keyword);
            //gvVendors.DataSource = dt;
            gvVendors.DataBind();
        }


        private void ClearVendorForm()
        {
            txtVendorCode.Text = "";
            txtVendorName.Text = "";
            txtVendorEmail.Text = "";
            txtVendorPhone.Text = "";
            txtVendorBalance.Text = "";
        }
    }
}