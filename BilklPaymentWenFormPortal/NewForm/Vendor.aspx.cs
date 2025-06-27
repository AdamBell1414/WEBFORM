using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BilklPaymentWenFormPortal.NewForm
{
    public partial class Vendor : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Expires = -1;

            if (!IsPostBack)
            {
                if (Session["UserID"] == null || Session["RoleID"] == null)
                {
                    Response.Redirect("~/WebPaymentLoginPage.aspx");
                    return;
                }

                int roleId = Convert.ToInt32(Session["RoleID"]);
                if (roleId != 3) // Vendor
                {
                    Response.Redirect("~/Unauthorized.aspx");
                    return;
                }

                int vendorId = Convert.ToInt32(Session["UserID"]);
                LoadVendorDashboard(vendorId);
                LoadCustomerTable(vendorId);
            }
        }



        private void LoadVendorDashboard(int vendorId)
        {
            try
            {
               
                var service = new Api.BillPaymentApiEndPoint();
                var response = service.GetVendorDashboardInfo(vendorId);

                if (response != null && response.Success && response.Data != null)
                {
                    var dashboard = response.Data;

                    vendor_name.InnerText = dashboard.VendorName ?? "N/A";
                    total_payments.InnerText = dashboard.TotalPaymentsMade.ToString("N0");
                    account_balance.InnerText = dashboard.AccountBalance.HasValue ? dashboard.AccountBalance.Value.ToString("N0") : "0";
                    customers_worked.InnerText = dashboard.CustomersWorked.ToString();
                }
                else
                {
                    ShowDashboardMessage("Failed to load dashboard info.", false);
                }
            }
            catch (Exception ex)
            {
                ShowDashboardMessage("Error loading dashboard: " + ex.Message, false);
            }
        }

        private void LoadCustomerTable(int vendorId)
        {
            try
            {
                var service = new Api.BillPaymentApiEndPoint();
                var response = service.GetCompletedTransactions(vendorId);

                if (response != null && response.Success && response.Data != null)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    foreach (var txn in response.Data)
                    {
                        sb.Append("<tr>");
                        sb.AppendFormat("<td>{0}</td>", txn.ReferenceNumber);
                        sb.AppendFormat("<td>{0}</td>", txn.CustomerName);
                        sb.AppendFormat("<td>{0}</td>", txn.Email);
                        sb.AppendFormat("<td>{0:N0}</td>", txn.Amount);
                        sb.AppendFormat("<td>{0}</td>", txn.ProcessedAt.HasValue ? txn.ProcessedAt.Value.ToString("yyyy-MM-dd HH:mm") : "");
                        sb.AppendFormat("<td>{0}</td>", txn.UtilityToken);
                        sb.AppendFormat("<td>{0}</td>", txn.UtilityReceiptNo);
                        sb.Append("</tr>");
                    }

                    customer_body.InnerHtml = sb.ToString();
                }
                else
                {
                    customer_body.InnerHtml = "<tr><td colspan='7'>No customer transactions found.</td></tr>";
                }
            }
            catch (Exception ex)
            {
                customer_body.InnerHtml = $"<tr><td colspan='7'>Error loading customers: {ex.Message}</td></tr>";
            }
        }






        protected void btnValidate_Click(object sender, EventArgs e)
        {
            var service = new Api.BillPaymentApiEndPoint();
            string vendorCode = txtVendorCode.Text;
            string reference = txtReference.Text;


            var response = service.ValidateReference(vendorCode, reference);

            if (response.Success)
            {
                dynamic customer = JsonConvert.DeserializeObject(response.Data);

                txtCustomerName.Text = customer.CustomerName;
                txtEmail.Text = customer.Email;



                txtPhone.Text = customer.Phone;
                hiddenUtilityCode.Value = customer.UtilityCode;


                pnlCustomerInfo.Visible = true;
                lblMessage.Text = "Reference validated. Proceed with payment.";
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Validation failed: " + response.Message;
            }
        }


        protected void btnContinue_Click(object sender, EventArgs e)
        {
            var service = new Api.BillPaymentApiEndPoint();

            string vendorCode = txtVendorCode.Text;
            string reference = txtReference.Text;
            string utilityCode = hiddenUtilityCode.Value;
            decimal amount = decimal.Parse(txtAmount.Text);

            int vendorUserId = Convert.ToInt32(Session["UserID"]);

            var result = service.InitiateVendorPayment(vendorCode, reference, utilityCode, amount, vendorUserId);

            if (result.Success)
            {

                ClearPaymentForm();
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Payment initiated. Vendors ID: " + result.Data;
                ShowDashboardMessage(lblMessage.Text, true);
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Payment failed: " + result.Message;
                ShowDashboardMessage(lblMessage.Text, false);
            }
        }




        private void ShowDashboardMessage(string message, bool isSuccess)
        {
            lblDashboardMessage.Text = message;
            lblDashboardMessage.CssClass = isSuccess ? "text-success fw-bold" : "text-danger fw-bold";
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            // Optional: explicitly clear UserID cookie if exists
            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddDays(-1);
            }

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Expires = -1;

            Response.Redirect("~/WebPaymentLoginPage.aspx");
        }





        private void ClearPaymentForm()
        {
            txtVendorCode.Text = "";
            txtReference.Text = "";
            txtCustomerName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtAmount.Text = "";
            hiddenUtilityCode.Value = "";
            pnlCustomerInfo.Visible = false;
            lblMessage.Text = "";
        }



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

        // Clear the vendor form fields
        //protected void txtSearchCustomer_TextChanged(object sender, EventArgs e)
        //{
        //    string keyword = txtSearchVendor.Text.Trim();
        //    LoadCustomers(keyword);
        //    ClearVendorForm();
        //}

        private void LoadCustomers(string keyword)
        {
            // Implement the logic to load vendors based on the search keyword
            // This could involve querying a database or an API
            // For now, we will just clear the form



            // Example: var vendors = DatabaseHelper.GetallCustomers(keyword);

            ClearVendorForm();
        }


        //protected void btnProcessPayment_Click(object sender, EventArgs e)
        //{
        //    // Payment processing logic
        //    string customer = ddlCustomers.SelectedValue;
        //    string utility = ddlUtilities.SelectedValue.Split('|')[0];
        //    decimal amount = Convert.ToDecimal(txtPaymentAmount.Text);
        //    string paymentMethod = ddlPaymentMethod.SelectedValue;
        //    string reference = txtReferenceNumber.Text;

        //    // Process payment here
        //    // Update database, call payment gateway, etc.

        //    bool isSuccess = ProcessPayment(); // Replace with your actual payment processing logic

        //    PaymentMessageLabel.Text = "Payment processed successfully!";
        //    PaymentMessageLabel.CssClass = "text-success";
        //}

        protected void ddlUtilities_SelectedIndexChanged(object sender, EventArgs e)
        {
            // This can be used for server-side logic if needed
            // The JavaScript handles the client-side auto-population
        }


        //protected void btnProcessPayment_Click(object sender, EventArgs e)
        //{
        //    // Assume payment processing logic here
        //    bool isSuccess = ProcessPayment(); // Replace with your actual payment processing logic

        //    // Clear the fields after processing
        //    txtPaymentAmount.Text = string.Empty;
        //    txtReferenceNumber.Text = string.Empty;
        //    ddlCustomers.SelectedIndex = 0; // Reset to the default option
        //    ddlUtilities.SelectedIndex = 0; // Reset to the default option
        //    ddlPaymentMethod.SelectedIndex = 0; // Reset to the default option

        //    // Show a toast notification based on the result
        //    if (isSuccess)
        //    {
        //        showToast("success", "Payment processed successfully!");
        //        // Add transaction to the list (you will implement this)
        //        AddTransaction("Customer Name", "Utility Name", txtPaymentAmount.Text, "Success");
        //    }
        //    else
        //    {
        //        showToast("error", "Payment failed. Please try again.");
        //        // Add transaction to the list (you will implement this)
        //        AddTransaction("Customer Name", "Utility Name", txtPaymentAmount.Text, "Failed");
        //    }
        //}
        private bool ProcessPayment()
        {
            // Implement your payment processing logic here
            // Return true if successful, false otherwise
            return true; // Placeholder for success
        }


        private List<Transaction> transactions = new List<Transaction>();
        private void AddTransaction(string customerName, string utilityName, string amount, string status)
        {
            transactions.Add(new Transaction
            {
                CustomerName = customerName,
                UtilityName = utilityName,
                Amount = amount,
                Status = status
            });

            // Optionally, you can bind this list to a GridView or display it in another way
            BindTransactionList();
        }

        private class Transaction
        {
            public string CustomerName { get; set; }
            public string UtilityName { get; set; }
            public string Amount { get; set; }
            public string Status { get; set; } // "Success" or "Failed"
        }

        private void BindTransactionList()
        {
            // Implement the logic to bind the transactions list to a GridView or other control
            // For example: gvTransactions.DataSource = transactions;
            // gvTransactions.DataBind();
        }

        public void ClearVendorForm()
        {
            txtUserName.Text = "";
            txtUserEmail.Text = "";
            txtUserPassword.Text = "";

        }


    }
}