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


        protected void btnCreateVendor_Click(object sender, EventArgs e)
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

                ClearVendorForm();

                MessageLabel.Text = "Vendor created successfully!";
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowToast", "showToast('success', '" + MessageLabel.Text + "');", true);
            }
            else
            {
                Label3.Text = "Error creating vendor!";
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowToast", "showToast('error', '" + Label3.Text + "');", true);
            }
        }

        //protected void btnCreateVendor_Click(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        var service = new Api.BillPaymentApiEndPoint();

        //        string vendorCode = txtVendorCode.Text.Trim();
        //        string vendorName = txtVendorName.Text.Trim();
        //        string contactEmail = txtVendorEmail.Text.Trim();
        //        string contactPhone = txtVendorPhone.Text.Trim();
        //        decimal balance = string.IsNullOrEmpty(txtVendorBalance.Text) ? 0 : Convert.ToDecimal(txtVendorBalance.Text);
        //        string password = txtVendorPassword.Text.Trim();
        //        int createdBy = Convert.ToInt32(Session["UserID"]);


        //        try
        //        {
        //            var response = service.CreateVendor(vendorCode, vendorName, contactEmail, contactPhone, password, balance, createdBy);

        //            lblVendorMessage.ForeColor = System.Drawing.Color.Green;
        //            lblVendorMessage.Text = "Vendor created successfully!";
        //            ClearVendorForm();
        //        }
        //        catch (Exception ex)
        //        {
        //            lblVendorMessage.ForeColor = System.Drawing.Color.Red;
        //            lblVendorMessage.Text = "Failed to create vendor: ";
        //           // lblVendorMessage.Text = $"Error: {ex.Message}";
        //        }


        //       // lblVendorMessage.Text = "Failed to create vendor: " + response.Message;
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        lblVendorMessage.ForeColor = System.Drawing.Color.Red;
        //        lblVendorMessage.Text = "Error: " + ex.Message;
        //    }
        //}




        // Create User


        protected void btnCreateCustomer_Click(object sender, EventArgs e)
        {

            string username = txtUserName.Text.Trim();
            string email = txtUserEmail.Text.Trim();
            string password = txtUserPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return;

            //DatabaseHelper.CreateUser(username, email, password);
            bool isSuccess = true; // Assume this is the result of your operation

            if (isSuccess)
            {
                //var response = service.CreateVendor(vendorCode, vendorName, contactEmail, contactPhone, password, balance, createdBy);

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

                bool isSuccess = true; // Assume this is the result of your operation

                if (isSuccess)
                {
                    var response = service.CreateUtility(utilityName, utilityCode, createdBy);
                    ClearVendorForm();


                    Label1.ForeColor = System.Drawing.Color.Green;
                    MessageLabel.Text = "Utility created successfully!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowToast", "showToast('success', '" + MessageLabel.Text + "');", true);
                }
                else
                {

                    Label1.ForeColor = System.Drawing.Color.Red;
                    Label3.Text = "Error While creating vendor!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowToast", "showToast('error', '" + Label3.Text + "');", true);
                }



                //if (response.Success)
                //{
                //    Label1.ForeColor = System.Drawing.Color.Green;
                //    Label1.Text = "Utility created successfully.";


                //    txtUtilityName.Text = "";
                //    txtUtilityCode.Text = "";


                //}
                //    else
                //    {
                //        Label1.ForeColor = System.Drawing.Color.Red;
                //        Label1.Text = "Error: " + response.Message;
                //    }
                //}
                //catch (Exception ex)
                //{
                //    Label1.ForeColor = System.Drawing.Color.Red;
                //    Label1.Text = "Exception: " + ex.Message;
                //}
            }
            catch (Exception ex)
            {
                // Label1.ForeColor = System.Drawing.Color.Red;
                // Label1.Text = "Error: " + ex.Message;
            }
        }



        // Search Vendors
        protected void txtSearchVendor_TextChanged(object sender, EventArgs e)
        {
            //string keyword = txtSearchVendor.Text.Trim();
            //LoadVendors(keyword);
            ClearVendorForm();
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