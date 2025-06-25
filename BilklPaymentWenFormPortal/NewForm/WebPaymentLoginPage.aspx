<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebPaymentLoginPage.aspx.cs" Inherits="BilklPaymentWenFormPortal.NewForm.WebPaymentLoginPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login - Elgon Payment Portal</title>

   <style>
    body {
        margin: 0;
        padding: 0;
        font-family: 'Inter', sans-serif;
        background: linear-gradient(135deg, #667eea, #764ba2);
        background-size: cover;
        height: 100vh;
        display: flex;
        justify-content: center;
        align-items: center;
        color: #1e293b;
    }

    .login-container {
        background: rgba(255, 255, 7, 0.12);
        border: 1px solid rgba(255, 255, 255, 0.3);
        border-radius: 16px;
        padding: 40px;
        box-shadow: 0 20px 50px rgba(255, 255, 255, 0.3);
        backdrop-filter: blur(14px);
        width: 100%;
        max-width: 420px;
        transition: transform 0.3s ease-in-out;
    }

    .login-container:hover {
        transform: translateY(-4px);
    }

    .login-title {
        text-align: center;
        font-size: 32px;
        font-weight: 700;
        margin-bottom: 30px;
        color: #ffffff;
        text-shadow: 0 1px 3px rgba(0,0,0,0.3);
    }

    .form-group {
        margin-bottom: 20px;
    }

    .form-group label {
        font-weight: 600;
        margin-bottom: 8px;
        display: block;
        color: #f1f5f9;
    }

    .form-group input {
        width: 100%;
        padding: 14px;
        font-size: 16px;
        border: none;
        border-radius: 10px;
        background-color: rgba(255, 255, 255, 0.15);
        color: #fff;
        backdrop-filter: blur(3px);
        outline: none;
        transition: background 0.3s, box-shadow 0.3s;
    }

    .form-group input:focus {
        background-color: rgba(255, 255, 255, 0.25);
        box-shadow: 0 0 0 3px rgba(99, 102, 241, 0.3);
    }

    .btn-submit {
        width: 100%;
        padding: 14px;
        background: linear-gradient(to right, #4facfe, #00f2fe);
        border: none;
        border-radius: 10px;
        font-weight: 600;
        font-size: 16px;
        color: white;
        cursor: pointer;
        transition: box-shadow 0.3s ease-in-out;
    }

    .btn-submit:hover {
        box-shadow: 0 8px 20px rgba(0, 242, 254, 0.3);
    }

    .message {
        margin-top: 15px;
        text-align: center;
        font-weight: bold;
        color: #ff4d4f;
    }

    .remember-me {
        display: flex;
        align-items: center;
        color: #e2e8f0;
        margin-top: 12px;
    }

    .remember-me input {
        margin-right: 8px;
    }

    .footer {
        text-align: center;
        margin-top: 25px;
        font-size: 13px;
        color: rgba(255, 255, 255, 0.7);
    }
</style>



    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;600;700&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <div class="login-title">
                Welcome to Bill Payments
            </div>
            <!-- Email/Username -->
            <div class="form-group">
                <label for="txtUsernameOrEmail">Email or Username</label>
                <asp:TextBox ID="txtUsernameOrEmail" runat="server" CssClass="form-control" placeholder="Enter Email or Username" />
            </div>
            <!-- Password -->
            <div class="form-group">
                <label for="txtPassword">Password</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Enter Password" />
            </div>
            <!-- Remember Me -->
           

      Submit Button -->

           <%-- <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn-submit" OnClick="btnLogins_Click" />--%>
            <!-- Message -->
   <asp:Button ID="btnLogin" runat="server" OnClick="btnLogins_Click" Text="Login" />


            
            

            <asp:Label ID="lblMessage" runat="server" CssClass="message" />

            <!-- Optional Footer -->
            <div class="footer">
                &copy; 2025 Elgon Portal. All rights reserved.
            </div>
        </div>
    </form>
</body>
</html>