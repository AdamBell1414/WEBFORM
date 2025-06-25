using BilklPaymentWenFormPortal.Api;
using BilklPaymentWenFormPortal.CustomerPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace BilklPaymentWenFormPortal.VendorPages
{
    public partial class Vendor : System.Web.UI.Page
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {

        //        if (Session["UserID"] == null || Session["RoleID"] == null)
        //        {
        //            Response.Redirect("~/WebPaymentLoginPage.aspx");
        //            return;
        //        }


        //        int roleId = Convert.ToInt32(Session["RoleID"]);
        //        if (roleId != 3) // 3 = Vendor
        //        {
        //            Response.Redirect("~/Unauthorized.aspx"); // Or show message
        //            return;
        //        }


        //        hiddenVendorUserID.Value = Session["UserID"].ToString();
        //    }
        //}


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
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Payment initiated. Vendors ID: " + result.Data;
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Payment failed: " + result.Message;
            }
        }

        protected void txtSearchVendor(object sender, EventArgs e)
        {

        }
    }
    }