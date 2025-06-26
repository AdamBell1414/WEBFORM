<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Vendor.aspx.cs" Inherits="BilklPaymentWenFormPortal.VendorPages.Vendor" %>

 <!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vendor Payment</title>
    <style>
        body { font-family: Arial; margin: 40px; }
        .form-group { margin-bottom: 15px; }
        label { display: block; font-weight: bold; }
        input[type="text"], input[type="email"], input[type="number"] {
            width: 300px; padding: 8px; margin-top: 5px;
        }
        .btn { padding: 10px 20px; background: #007bff; color: white; border: none; cursor: pointer; }
        .btn:hover { background: #0056b3; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Vendor Utility Payment</h2>

        <div class="form-group">
            <label for="txtVendorCode">Vendor Code</label>
            <asp:TextBox ID="txtVendorCode" runat="server" />
        </div>

        <div class="form-group">
            <label for="txtReference">Reference Number</label>
            <asp:TextBox ID="txtReference" runat="server" />
        </div>

        <asp:Button ID="btnValidate" runat="server" CssClass="btn" Text="Validate Reference" OnClick="btnValidate_Click" />

        <hr />

        <asp:Panel ID="pnlCustomerInfo" runat="server" Visible="false">
            <div class="form-group">
                <label for="txtCustomerName">Customer Name</label>
                <asp:TextBox ID="txtCustomerName" runat="server" ReadOnly="true" />
            </div>

            <div class="form-group">
                <label for="txtEmail">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" ReadOnly="true" />
            </div>

            <div class="form-group">
                <label for="txtPhone">Phone</label>
                <asp:TextBox ID="txtPhone" runat="server" ReadOnly="true" />
            </div>

            <div class="form-group">
                <label for="txtAmount">Amount</label>
                <asp:TextBox ID="txtAmount" runat="server" TextMode="Number" />
            </div>

            <asp:HiddenField ID="hiddenUtilityCode" runat="server" />
            <asp:HiddenField ID="hiddenVendorUserID" runat="server" />

            <asp:Button ID="btnContinue" runat="server" CssClass="btn" Text="Continue & Initiate Payment" OnClick="btnContinue_Click" />
        </asp:Panel>

        <asp:Label ID="lblMessage" runat="server" ForeColor="Green" />
    </form>
</body>
</html>
