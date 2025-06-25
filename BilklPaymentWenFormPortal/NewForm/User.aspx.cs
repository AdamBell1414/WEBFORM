using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BilklPaymentWenFormPortal.NewForm
{
    public partial class User : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                gvTransactions.DataSource = GetDummyTransactions();
                gvTransactions.DataBind();
            }
        }

        protected void ddlUtilities_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Your logic here when a utility is selected
            Session.Clear();
        }
        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            // Your logic here when the amount is changed
            Session.Clear();
        }
        protected void BtnProcessPayment_Click(object sender, EventArgs e)
        {
            // Get the selected utility and amount
            string selectedUtility = ddlUtilities.SelectedValue;
            // decimal amount = Convert.ToDecimal(txtAmount.Text.Trim());


            // Your logic to process the payment
            // For example, call an API to make the payment

            //lblMessage.Text = $"Payment of {amount:C} for {selectedUtility} has been processed.";
            Session.Clear();
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            // Clear the form fields
            ddlUtilities.SelectedIndex = 0;
            // txtAmount.Text = string.Empty;

            // lblMessage.Text = string.Empty;
            Session.Clear();
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Logic to log out the user
            Session.Clear();
            Response.Redirect("~/WebPaymentLoginPage.aspx");
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // Logic to search for utilities or payments
            // string searchQuery = txtSearch.Text.Trim();
            // For example, filter the utilities based on the search query
            // This could involve calling an API or filtering a list
            // lblMessage.Text = $"Search results for '{searchQuery}' will be displayed here.";
        }


        protected void btnViewHistory_Click(object sender, EventArgs e)
        {
            // Logic to view payment history
            // This could involve fetching data from a database or an API
            //lblMessage.Text = "Payment history will be displayed here.";

        }



        protected void txtSearchCustomer_TextChanged(object sender, EventArgs e)
        {
            //string keyword = txtSearchVendor.Text.Trim();
            // LoadCustomers(keyword);
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
            //string customer = ddlCustomers.SelectedValue;
            //string utility = ddlUtilities.SelectedValue.Split('|')[0];
            //decimal amount = Convert.ToDecimal(txtPaymentAmount.Text);
            //string paymentMethod = ddlPaymentMethod.SelectedValue;
            //string reference = txtReferenceNumber.Text;

            // Process payment here
            // Update database, call payment gateway, etc.

            bool isSuccess = ProcessPayment(); // Replace with your actual payment processing logic

            //PaymentMessageLabel.Text = "Payment processed successfully!";
            //PaymentMessageLabel.CssClass = "text-success";
        }

        //protected void ddlUtilities_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    // This can be used for server-side logic if needed
        //    // The JavaScript handles the client-side auto-population
        //}


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
        private void AddTransaction(string customerName, string utilityName, decimal amount, string status)
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

        public class Transaction
        {
            public string CustomerName { get; set; }
            public string UtilityName { get; set; }
            public decimal Amount { get; set; }
            public string Status { get; set; } // "Success" or "Failed"

            public DateTime Date { get; set; }
            public string TransactionID { get; internal set; }
            public string ReferenceID { get; internal set; }
        }


        public void ClearVendorForm()
        {
            //txtUserName.Text ="";
            //txtUserEmail.Text = "";
            //txtUserPassword.Text ="";

        }


        private void BindTransactionList()
        {
            // This method can be used to bind the transactions list to a GridView or any other control
            // For example:
            // gvTransactions.DataSource = transactions;
            // gvTransactions.DataBind();
        }




      

        private List<Transaction> GetDummyTransactions()
        {

            return new List<Transaction>
            {
                new Transaction { TransactionID = "TXN001", Status = "Success", Amount = 2500.00m, ReferenceID = "REF001", Date = DateTime.Now.AddDays(-1) },
                new Transaction { TransactionID = "TXN002", Status = "Fail", Amount = 1200.00m, ReferenceID = "REF002", Date = DateTime.Now.AddDays(-2) },
                new Transaction { TransactionID = "TXN003", Status = "Success", Amount = 7600.00m, ReferenceID = "REF003", Date = DateTime.Now }
            };
        }


    }
}