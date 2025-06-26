using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static BilklPaymentWenFormPortal.NewForm.User;

namespace BilklPaymentWenFormPortal.CustomerPages
{
    public partial class AdminDashBoard : System.Web.UI.Page
    {

        //protected void Page_Load(object sender, EventArgs e)
        //{

        //}
        protected void Page_Init(object sender, EventArgs e)
        {
            //if (Session["UserID"] == null)
            //{
            //    Response.Redirect("~/Login.aspx");
            //}
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            // Optional: Clear auth cookies
            Response.Cookies.Clear();

            // Redirect to login
            Response.Redirect("~/WebPaymentLoginPage.aspx");
        }


        protected void btnCreateVendor_Click(object sender, EventArgs e)
        {
            bool shouldClearForm = false; // Flag to determine if we should clear the form at the end
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


                // Your logic to create a vendor
                bool isSuccess = true; // Assume this is the result of your operation

                if (isSuccess)
                {
                    response = service.CreateVendor(vendorCode, vendorName, contactEmail, contactPhone, password, balance, createdBy);
                    ClearCustomerForm();



                    MessageLabel.Text = "Vendor created successfully!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowToast", "showToast('success', '" + MessageLabel.Text + "');", true);
                }
                else
                {
                    ClearCustomerForm();
                    Label3.Text = "Error creating vendor!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowToast", "showToast('error', '" + Label3.Text + "');", true);
                }
            }
            catch (Exception ex)
            {
                ClearCustomerForm();
                shouldClearForm = true;
                Label444.ForeColor = System.Drawing.Color.Red;
                Label444.Text = "Error: " + ex.Message;

                
            }
            finally
            {
                if (shouldClearForm)
                {
                    ClearCustomerForm();
                    ClearVendorForm();
                }
            }
        }





            // Create User


        protected void btnCreateCustomer_Click(object sender, EventArgs e)
        {
           bool shouldClearForm = false;

          try 
            { 

            string CustomerName = txtUserName.Text.Trim();
            string email = txtUserEmail.Text.Trim();
           string phoneNumer = txtUserPhone.Text.Trim();
            string password = txtUserPassword.Text.Trim();
            string ReferenceNumber = txtReferenceNumber.Text.Trim();
            string UtilityCodse = txtUtilityCode.Text.Trim();
           int createdBy = Convert.ToInt32(Session["UserID"]);







            // Check if any required fields are empty
            if (string.IsNullOrEmpty(CustomerName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                Label444.ForeColor = System.Drawing.Color.Red;
                Label444.Text = "All fields are required.";
                return;
            }


            //DatabaseHelper.CreateUser(CustomerName, email, password);
            bool isSuccess = true; // Assume this is the result of your operation
            var service = new Api.BillPaymentApiEndPoint();

            if (isSuccess)
            {
                var response = service.CreateCustomer(ReferenceNumber, CustomerName, email, phoneNumer, password, UtilityCodse, createdBy);

                ClearVendorForm();

                MessageLabel1.Text = "Customer created successfully!";
               ScriptManager.RegisterStartupScript(this, GetType(), "ShowToast", "showToast('success', '" + MessageLabel.Text + "');", true);
            }
            else
            {
                MessageLabel1.Text = "Error creating customer!";
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowToast", "showToast('error', '" + Label3.Text + "');", true);
            }


            txtUserName.Text = txtUserEmail.Text = txtUserPassword.Text = "";
           }

            catch (Exception ex)
           {  shouldClearForm = true;
               Label444.ForeColor = System.Drawing.Color.Red;
                Label444.Text = "Error: " + ex.Message;

                 ClearVendorForm();
            }
           finally
            {
                if (shouldClearForm)
               {
                    ClearCustomerForm();
               }
           }
}








        //protected void btnCreateCustomer_Click(object sender, EventArgs e)
        //        {
        //            bool shouldClearForm = false;

        //            try
        //            {
        //                string CustomerName = txtUserName.Text.Trim();
        //                string email = txtUserEmail.Text.Trim();
        //                string phoneNumber = txtUserPhone.Text.Trim();
        //                string password = txtUserPassword.Text.Trim();
        //                string ReferenceNumber = txtReferenceNumber.Text.Trim();
        //                string UtilityCode = txtUtilityCode.Text.Trim();
        //                int createdBy = Convert.ToInt32(Session["UserID"]);

        //                // Check if any required fields are empty
        //                if (string.IsNullOrEmpty(CustomerName) || string.IsNullOrEmpty(email) ||
        //                    string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(password))
        //                {
        //                    Label444.ForeColor = System.Drawing.Color.Red;
        //                    Label444.Text = "All fields are required.";
        //                    return; // Don't clear on validation error
        //                }

        //                var service = new Api.BillPaymentApiEndPoint();

        //                bool isSuccess = true;
        //                // Mark for clearing since we attempted the API call
        //                shouldClearForm = true;

        //                if (isSuccess)
        //                {
        //                    var response = service.CreateCustomer(ReferenceNumber, CustomerName, email, phoneNumber, password, UtilityCode, createdBy);
        //                    Label444.ForeColor = System.Drawing.Color.Green;
        //                    Label444.Text = "Customer created successfully!";
        //                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowToast",
        //                        "showToast('success', 'Customer created successfully!');", true);
        //                }
        //                else
        //                {
        //                    Label444.ForeColor = System.Drawing.Color.Red;
        //                    Label444.Text = "Error creating customer!";
        //                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowToast",
        //                        "showToast('error', 'Error creating customer!');", true);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                shouldClearForm = true; // Clear even on exception
        //                Label444.ForeColor = System.Drawing.Color.Red;
        //                Label444.Text = "Error: " + ex.Message;
        //                ScriptManager.RegisterStartupScript(this, GetType(), "ShowToast",
        //                    "showToast('error', 'Error: " + ex.Message.Replace("'", "\\'") + "');", true);
        //            }
        //            finally
        //            {
        //                if (shouldClearForm)
        //                {
        //                    ClearCustomerForm();
        //                }
        //            }
        //        }




        //utility
        protected void btnCreateUtility_Click(object sender, EventArgs e)
        {
            bool shouldClearForm = false;
            try
            {


                string utilityName = txtUtilityName.Text.Trim();
                string utilityCode = txtUtilityCode.Text.Trim();
                int createdBy = Convert.ToInt32(Session["UserID"]);


                if (string.IsNullOrEmpty(utilityName) || string.IsNullOrEmpty(utilityCode))
                {
                    Label444.ForeColor = System.Drawing.Color.Red;
                    Label444.Text = "All fields are required.";
                    return;
                }

              

                var service = new Api.BillPaymentApiEndPoint();

                bool isSuccess = true; // Assume this is the result of your operation

                if (isSuccess)
                {
                    var response = service.CreateUtility(utilityName, utilityCode, createdBy);
                    ClearVendorForm();

                    Label1.Text = "Payment completed successfully!";
                    Label1.ForeColor = System.Drawing.Color.Green;
                    MessageLabel.Text = "Utility created successfully!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowToast", "showToast('success', '" + MessageLabel.Text + "');", true);
                }
                else
                {

                    Label444.ForeColor = System.Drawing.Color.Red;
                    Label444.Text = "Error While creating vendor!";
                    ClearVendorForm();
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowToast", "showToast('error', '" + Label3.Text + "');", true);
                }



               
            }
            catch (Exception ex)
            {
                shouldClearForm = true;
                Label444.ForeColor = System.Drawing.Color.Red;
                 Label444.Text = "Error: " + ex.Message;
                //  ClearVendorForm();
            }
            finally
            {
                if (shouldClearForm)
                {
                    ClearCustomerForm();
                }
            }

        }



        // Search Vendors
        protected void txtSearchVendor_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearchVendor.Text.Trim();
            LoadVendors(keyword);

            //string keyword = txtSearchVendor.Text.Trim();
            //LoadVendors(keyword);
            //ClearVendorForm();
        }

        // Load Vendors (with optional search)
        private void LoadVendors(string keyword = "")
        {
            //DataTable dt = DatabaseHelper.GetVendors(keyword);
            //gvVendors.DataSource = dt;
            gvVendors.DataBind();
        }


        public void ClearVendorForm()
        {
            txtVendorCode.Text = "";
            txtVendorName.Text = "";
            txtVendorEmail.Text = "";
            txtVendorPhone.Text = "";
            txtVendorBalance.Text = "";
        }

        // Add this method to your code-behind file (.aspx.cs)
        private void ClearCustomerForm()
        {

            txtUserName.Text = "";
            txtUserEmail.Text = "";
            txtUserPhone.Text = "";
            txtUserPassword.Text = "";
            txtReferenceNumber.Text = "";
            txtUtilityCode.Text = "";
            txtUtilityName.Text = "";

            txtVendorCode.Text = "";
            txtVendorName.Text = "";
            txtVendorEmail.Text = "";
            txtVendorPhone.Text = "";
            txtVendorBalance.Text = "";

            txtVendorCode.Text = "";


            // If you have any other controls in the customer form, clear them too
            // For example:
            // txtAnyOtherField.Text = "";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvTransactions.DataSource = GetDummyTransactions();
               // gvTransactions.DataBind();
            }
        }

        public class Vendors
        {
            public string Vendorcode { get; set; }
            public string VendorName { get; set; }
           
            public string ContactEmail { get; set; }
            public int  ContactPhone { get; set; } // "Success" or "Failed"

            public DateTime Date { get; set; }
            public string TransactionID { get; internal set; }
            public string ReferenceID { get; internal set; }
            public string Status { get; internal set; }
            
        }


        private List<Vendors> GetDummyTransactions()
        {

            return new List<Vendors>
            {
                new Vendors { TransactionID = "TXN001", Status = "Success",  ReferenceID = "REF001", Date = DateTime.Now.AddDays(-1) },
                new Vendors { TransactionID = "TXN002", Status = "Fail", ReferenceID = "REF002", Date = DateTime.Now.AddDays(-2) },
                new Vendors { TransactionID = "TXN003", Status = "Success", ReferenceID = "REF003", Date = DateTime.Now }
            };
        }


    }
}