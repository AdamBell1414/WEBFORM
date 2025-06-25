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
        protected void txtSearchCustomer_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearchVendor.Text.Trim();
            LoadCustomers(keyword);
            ClearVendorForm();
        }

        private void LoadCustomers(string keyword)
        {
            // Implement the logic to load vendors based on the search keyword
            // This could involve querying a database or an API
            // For now, we will just clear the form



            // Example: var vendors = DatabaseHelper.GetallCustomers(keyword);

            ClearVendorForm();
        }


        protected void btnProcessPayment_Click(object sender, EventArgs e)
        {
            // Payment processing logic
            string customer = ddlCustomers.SelectedValue;
            string utility = ddlUtilities.SelectedValue.Split('|')[0];
            decimal amount = Convert.ToDecimal(txtPaymentAmount.Text);
            string paymentMethod = ddlPaymentMethod.SelectedValue;
            string reference = txtReferenceNumber.Text;

            // Process payment here
            // Update database, call payment gateway, etc.

            bool isSuccess = ProcessPayment(); // Replace with your actual payment processing logic

            PaymentMessageLabel.Text = "Payment processed successfully!";
            PaymentMessageLabel.CssClass = "text-success";
        }

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