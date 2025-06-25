<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDashBoard.aspx.cs" Inherits="BilklPaymentWenFormPortal.CustomerPages.AdminDashBoard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Portal - BilklPayment</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Font Awesome for icons -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet" />
    <!-- Google Fonts -->
    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@300;400;500;600;700&display=swap" rel="stylesheet">

    <style>
      :root {
          --primary-color: #6366f1;
          --primary-dark: #4f46e5;
          --secondary-color: #f8fafc;
          --success-color: #10b981;
          --warning-color: #f59e0b;
          --danger-color: #ef4444;
          --dark-color: #1e293b;
          --light-gray: #f1f5f9;
          --border-color: #e2e8f0;
          --text-primary: #0f172a;
          --text-secondary: #64748b;
          --shadow-sm: 0 1px 2px 0 rgb(0 0 0 / 0.05);
          --shadow-md: 0 4px 6px -1px rgb(0 0 0 / 0.1);
          --shadow-lg: 0 10px 15px -3px rgb(0 0 0 / 0.1);
      }

      * {
          box-sizing: border-box;
      }

      body, html {
          margin: 0;
          padding: 0;
          height: 100%;
          font-family: 'Inter', -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, sans-serif;
          background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
          color: var(--text-primary);
          font-size: 14px;
          line-height: 1.6;
      }

      .layout {
          display: flex;
          min-height: 100vh;
          background-color: var(--secondary-color);
      }

      /* Improved Sidebar */
      .sidebar {
          width: 280px;
          background: linear-gradient(180deg, var(--dark-color) 0%, #334155 100%);
          color: white;
          padding: 0;
          flex-shrink: 0;
          box-shadow: var(--shadow-lg);
          transition: all 0.3s ease;
          position: relative;
          overflow: hidden;
      }

      .sidebar::before {
          content: '';
          position: absolute;
          top: 0;
          left: 0;
          right: 0;
          height: 4px;
          background: linear-gradient(90deg, var(--primary-color), var(--success-color));
      }

      .sidebar-header {
          padding: 30px 25px;
          border-bottom: 1px solid rgba(255, 255, 255, 0.1);
          background: rgba(255, 255, 255, 0.05);
      }

      .sidebar-header h1 {
          font-size: 24px;
          font-weight: 700;
          margin: 0 0 8px 0;
          background: linear-gradient(45deg, #fff, #e2e8f0);
          -webkit-background-clip: text;
          -webkit-text-fill-color: transparent;
          background-clip: text;
      }

      .sidebar-header p {
          font-size: 13px;
          color: rgba(255, 255, 255, 0.7);
          margin: 0;
          line-height: 1.4;
      }

      .nav-section {
          padding: 20px 0;
      }

      .nav-section-title {
          padding: 0 25px 10px;
          font-size: 11px;
          font-weight: 600;
          text-transform: uppercase;
          letter-spacing: 0.5px;
          color: rgba(255, 255, 255, 0.5);
          margin-bottom: 8px;
      }

      .sidebar .nav-link {
          display: flex;
          align-items: center;
          color: rgba(255, 255, 255, 0.85);
          padding: 14px 25px;
          text-decoration: none;
          font-size: 15px;
          font-weight: 500;
          border: none;
          border-left: 3px solid transparent;
          transition: all 0.3s ease;
          position: relative;
      }

      .sidebar .nav-link i {
          width: 20px;
          margin-right: 12px;
          font-size: 16px;
          text-align: center;
      }

      .sidebar .nav-link:hover {
          background: rgba(255, 255, 255, 0.1);
          color: white;
          border-left-color: var(--primary-color);
          transform: translateX(2px);
      }

      .sidebar .nav-link.active {
          background: rgba(99, 102, 241, 0.2);
          color: white;
          border-left-color: var(--primary-color);
      }

      /* Main Content Area */
      .main {
          flex-grow: 1;
          padding: 0;
          overflow-y: auto;
          background-color: var(--light-gray);
      }

      .main-header {
          background: white;
          padding: 25px 35px;
          box-shadow: var(--shadow-sm);
          border-bottom: 1px solid var(--border-color);
          margin-bottom: 30px;
      }

      .main-header h2 {
          font-size: 28px;
          font-weight: 700;
          color: var(--text-primary);
          margin: 0 0 8px 0;
      }

      .main-header .subtitle {
          color: var(--text-secondary);
          font-size: 16px;
          margin: 0;
      }

      .main-content {
          padding: 0 35px 35px;
      }

      /* Enhanced Cards */
      .stats-grid {
          display: grid;
          grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
          gap: 25px;
          margin-bottom: 35px;
      }

      .stat-card {
          background: white;
          border-radius: 16px;
          padding: 28px;
          box-shadow: var(--shadow-md);
          border: 1px solid var(--border-color);
          transition: all 0.3s ease;
          position: relative;
          overflow: hidden;
      }

      .stat-card::before {
          content: '';
          position: absolute;
          top: 0;
          left: 0;
          right: 0;
          height: 4px;
          background: linear-gradient(90deg, var(--primary-color), var(--success-color));
      }

      .stat-card:hover {
          transform: translateY(-4px);
          box-shadow: var(--shadow-lg);
      }

      .stat-card-header {
          display: flex;
          align-items: center;
          justify-content: space-between;
          margin-bottom: 16px;
      }

      .stat-card-title {
          font-size: 15px;
          font-weight: 600;
          color: var(--text-secondary);
          text-transform: uppercase;
          letter-spacing: 0.5px;
          margin: 0;
      }

      .stat-card-icon {
          width: 48px;
          height: 48px;
          border-radius: 12px;
          display: flex;
          align-items: center;
          justify-content: center;
          font-size: 20px;
          color: white;
      }

      .stat-card-icon.users { background: linear-gradient(45deg, var(--success-color), #059669); }
      .stat-card-icon.utilities { background: linear-gradient(45deg, var(--warning-color), #d97706); }
      .stat-card-icon.payments { background: linear-gradient(45deg, var(--primary-color), var(--primary-dark)); }

      .stat-card-value {
          font-size: 36px;
          font-weight: 700;
          color: var(--text-primary);
          margin: 0;
          line-height: 1.2;
      }

      /* Enhanced Section */
      .section {
          background: white;
          border-radius: 16px;
          box-shadow: var(--shadow-md);
          border: 1px solid var(--border-color);
          overflow: hidden;
          margin-bottom: 30px;
      }

      .section-header {
          padding: 25px 30px;
          border-bottom: 1px solid var(--border-color);
          background: linear-gradient(135deg, #f8fafc 0%, #f1f5f9 100%);
      }

      .section-header h4 {
          font-size: 20px;
          font-weight: 700;
          color: var(--text-primary);
          margin: 0 0 8px 0;
      }

      .section-header p {
          color: var(--text-secondary);
          margin: 0;
          font-size: 14px;
      }

      .section-body {
          padding: 30px;
      }

      /* Form Controls */
      .form-control {
          border: 2px solid var(--border-color);
          border-radius: 12px;
          padding: 14px 16px;
          font-size: 15px;
          transition: all 0.3s ease;
          background: white;
      }

      .form-control:focus {
          border-color: var(--primary-color);
          box-shadow: 0 0 0 3px rgba(99, 102, 241, 0.1);
          outline: none;
      }

      .search-container {
          position: relative;
          margin-bottom: 25px;
      }

      .search-container i {
          position: absolute;
          left: 16px;
          top: 50%;
          transform: translateY(-50%);
          color: var(--text-secondary);
          font-size: 16px;
      }

      .search-container .form-control {
          padding-left: 50px;
      }

      /* Enhanced Table */
      .table-container {
          background: white;
          border-radius: 12px;
          overflow: hidden;
          box-shadow: var(--shadow-sm);
      }

      .table {
          margin: 0;
          font-size: 14px;
      }

      .table th {
          background: linear-gradient(135deg, #f8fafc 0%, #f1f5f9 100%);
          border: none;
          font-weight: 600;
          color: var(--text-primary);
          text-transform: uppercase;
          font-size: 12px;
          letter-spacing: 0.5px;
          padding: 18px 20px;
      }

      .table td {
          border: none;
          border-bottom: 1px solid #f1f5f9;
          padding: 18px 20px;
          vertical-align: middle;
      }

      .table tbody tr:hover {
          background-color: #f8fafc;
      }

      /* Enhanced Buttons */
      .btn {
          border-radius: 12px;
          font-weight: 600;
          padding: 12px 24px;
          font-size: 14px;
          transition: all 0.3s ease;
          border: none;
          text-transform: uppercase;
          letter-spacing: 0.5px;
      }

      .btn-primary {
          background: linear-gradient(45deg, var(--primary-color), var(--primary-dark));
          box-shadow: 0 4px 12px rgba(99, 102, 241, 0.3);
      }

      .btn-primary:hover {
          transform: translateY(-2px);
          box-shadow: 0 6px 20px rgba(99, 102, 241, 0.4);
      }

      .btn-success {
          background: linear-gradient(45deg, var(--success-color), #059669);
          box-shadow: 0 4px 12px rgba(16, 185, 129, 0.3);
      }

      .btn-success:hover {
          transform: translateY(-2px);
          box-shadow: 0 6px 20px rgba(16, 185, 129, 0.4);
      }

      /* Enhanced Modals */
      .modal-content {
          border: none;
          border-radius: 20px;
          box-shadow: var(--shadow-lg);
          overflow: hidden;
      }

      .modal-header {
          border: none;
          padding: 25px 30px 20px;
      }

      .modal-title {
          font-size: 22px;
          font-weight: 700;
      }

      .modal-body {
          padding: 20px 30px 30px;
      }

      .modal-footer {
          border: none;
          padding: 20px 30px 30px;
          justify-content: center;
      }

      /* Toast Improvements */
      .toast {
          border: none;
          border-radius: 12px;
          box-shadow: var(--shadow-lg);
      }

      .toast-header {
          border: none;
          font-weight: 600;
      }

      /* Responsive Design */
      @media (max-width: 1024px) {
          .sidebar {
              width: 260px;
          }
          
          .main-content {
              padding: 0 25px 25px;
          }
      }

      @media (max-width: 768px) {
          .sidebar {
              width: 100%;
              position: fixed;
              top: 0;
              left: -100%;
              z-index: 1000;
              transition: left 0.3s ease;
          }
          
          .sidebar.active {
              left: 0;
          }
          
          .main {
              width: 100%;
          }
          
          .main-header {
              padding: 20px;
          }
          
          .main-content {
              padding: 0 20px 20px;
          }
          
          .stats-grid {
              grid-template-columns: 1fr;
              gap: 20px;
          }
          
          .section-body {
              padding: 20px;
          }
      }

      /* Loading States */
      .loading {
          opacity: 0.6;
          pointer-events: none;
      }

      /* Empty State */
      .empty-state {
          text-align: center;
          padding: 60px 20px;
          color: var(--text-secondary);
      }

      .empty-state i {
          font-size: 48px;
          margin-bottom: 20px;
          opacity: 0.5;
      }



      .transaction-table {
           width: 100%;
           border-collapse: collapse;
           font-family: Arial, sans-serif;
           margin-top: 20px;
       }
       .transaction-table th, .transaction-table td {
           border: 1px solid #ccc;
           padding: 10px;
           text-align: center;
       }
       .transaction-table th {
           background-color: #007bff;
           color: white;
       }
       .success { background-color: #d4edda; }
       .fail { background-color: #f8d7da; }
  </style>
</head>

<body>
    <form id="form1" runat="server">
        <div class="layout">
            <!-- Enhanced Sidebar -->
            <div class="sidebar">
                <div class="sidebar-header">
                    <h1><i class="fas fa-tools me-2"></i>Admin Portal</h1>
                    <p>Manage vendors, users, and utilities efficiently</p>
                </div>

                <div class="nav-section">
                    <div class="nav-section-title">Management</div>
                    <ul class="nav flex-column">
                        <li class="nav-item">
                            <a class="nav-link" href="#" data-bs-toggle="modal" data-bs-target="#vendorModal">
                                <i class="fas fa-store"></i>
                                Create Vendor
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="#" data-bs-toggle="modal" data-bs-target="#userModal">
                                <i class="fas fa-user-plus"></i>
                                Create User
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="#" data-bs-toggle="modal" data-bs-target="#utilityModal">
                                <i class="fas fa-wrench"></i>
                                Create Utility
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link active" href="#list">
                                <i class="fas fa-table"></i>
                                Vendor Table
                            </a>
                        </li>
                    </ul>
                </div>

                <div class="nav-section">
                    <div class="nav-section-title">Support</div>
                    <ul class="nav flex-column">
                        <li class="nav-item">
                            <a class="nav-link" href="#contact">
                                <i class="fas fa-headset"></i>
                                Contact Support
                            </a>
                        </li>
                    </ul>
                </div>
            </div>

            <!-- Enhanced Main Content -->
            <div class="main">
                <div class="main-header">
                    <h2>Welcome, Admin</h2>
                    <p class="subtitle">Here's a summary of your system</p>
                </div>

                <div class="main-content">
                    <!-- Enhanced Stats Cards -->
                    <div class="stats-grid">
                        <div class="stat-card">
                            <div class="stat-card-header">
                                <h5 class="stat-card-title">Total Vendors</h5>
                                <div class="stat-card-icon vendors">
                                    <i class="fas fa-store"></i>
                                </div>
                            </div>
                            <p class="stat-card-value">120</p>
                        </div>

                        <div class="stat-card">
                            <div class="stat-card-header">
                                <h5 class="stat-card-title">Total Users</h5>
                                <div class="stat-card-icon users">
                                    <i class="fas fa-users"></i>
                                </div>
                            </div>
                            <p class="stat-card-value">45</p>
                        </div>

                        <div class="stat-card">
                            <div class="stat-card-header">
                                <h5 class="stat-card-title">Utilities</h5>
                                <div class="stat-card-icon utilities">
                                    <i class="fas fa-wrench"></i>
                                </div>
                            </div>
                            <p class="stat-card-value">30</p>
                        </div>
                    </div>

                   <!-- Enhanced Vendor Table Section -->
                   <div id="list" class="section">
                        <div class="section-header">
                            <h4><i class="fas fa-store me-2"></i>Vendor  Management Tables </h4>
                            <%--<p>Search and manage vendors</p>--%>
                        

                      
                     <%--   <div class="section-body">
                            <div class="search-container">
                                <i class="fas fa-search"></i>
                                <asp:TextBox ID="txtSearchVendor" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtSearchVendor_TextChanged" Placeholder="Search Vendor..." />
                            </div>--%>

                            <div class="table-container">

                            

                                 <table class="transaction-table">
                                
                     <asp:GridView ID="gvTransactions" runat="server" CssClass="transaction-table" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="TransactionID" HeaderText="Transaction ID" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
                <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:C}" />
                <asp:BoundField DataField="ReferenceID" HeaderText="Reference ID" />
                <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd}" />
            </Columns>
        </asp:GridView>
                                
                                </table>

                                   </div>
                            </div>






                                <asp:GridView ID="gvVendors" runat="server" AutoGenerateColumns="False" CssClass="table table-hover">
                                    <Columns>
                                        <asp:BoundField DataField="VendorCode" HeaderText="Vendor Code" />
                                        <asp:BoundField DataField="VendorName" HeaderText="Vendor Name" />
                                    </Columns>
                                     <EmptyDataTemplate>
                                        <div class="empty-state">
                                            <i class="fas fa-inbox"></i>
                                            <h5>No vendors found</h5>
                                            <p>Try adjusting your search criteria or add new vendors</p>
                                        </div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            
        

        <!-- Vendor Modal -->
        <div class="modal fade" id="vendorModal" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header bg-primary text-white">
                        <h5 class="modal-title">Create Vendor</h5>
                        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                    </div>
                    <div class="modal-body">
                        <label class="form-label">Vendor Code</label>
                        <asp:TextBox ID="txtVendorCode" runat="server" CssClass="form-control mb-2" />
                        <label class="form-label">Vendor Name</label>
                        <asp:TextBox ID="txtVendorName" runat="server" CssClass="form-control mb-2" />
                        <label class="form-label">Contact Email</label>
                        <asp:TextBox ID="txtVendorEmail" runat="server" CssClass="form-control mb-2" TextMode="Email" />
                        <label class="form-label">Contact Phone</label>
                        <asp:TextBox ID="txtVendorPhone" runat="server" CssClass="form-control mb-2" />
                        <label class="form-label">Password</label>
                        <asp:TextBox ID="txtVendorPassword" runat="server" CssClass="form-control mb-2" TextMode="Password" />
                        <label class="form-label">Initial Balance</label>
                        <asp:TextBox ID="txtVendorBalance" runat="server" CssClass="form-control mb-2" TextMode="Number" />
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnCreateVendor" runat="server" Text="Create Vendor" CssClass="btn btn-primary" OnClick="btnCreateVendor_Click" />
                    </div>
                    <asp:Label ID="Label2" runat="server" CssClass="text-success mb-2" EnableViewState="false" />
                </div>
            </div>
        </div>

        <!-- User Modal -->
        <div class="modal fade" id="userModal" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header bg-success text-white">
                        <h5 class="modal-title">Create User</h5>
                        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                    </div>
                    <div class="modal-body">
                        <label class="form-label">Username</label>
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control mb-2" />
                        <label class="form-label">Email</label>
                        <asp:TextBox ID="txtUserEmail" runat="server" TextMode="Email" CssClass="form-control mb-2" />
                        <label class="form-label">Phone Number</label>
                        <asp:TextBox ID="txtUserPhone" runat="server" CssClass="form-control mb-2" />
                        <label class="form-label">Password</label>
                        <asp:TextBox ID="txtUserPassword" runat="server" TextMode="Password" CssClass="form-control mb-2" />
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnCreateUser" runat="server" Text="Create User" CssClass="btn btn-success" OnClick="btnCreateCustomer_Click" />
                    </div>
                    <asp:Label ID="MessageLabel1" runat="server" CssClass="text-success mb-2" EnableViewState="false" />
                </div>
            </div>
        </div>

        <!-- Utility Modal -->
        <div class="modal fade" id="utilityModal" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header bg-warning text-dark">
                        <h5 class="modal-title">Create Utility</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                    </div>
                    <div class="modal-body">
                        <label class="form-label">Utility Name</label>
                        <asp:TextBox ID="txtUtilityName" runat="server" CssClass="form-control mb-2" />
                        <label class="form-label">Utility Code</label>
                        <asp:TextBox ID="txtUtilityCode" runat="server" CssClass="form-control mb-2" />
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnCreateUtility" runat="server" Text="Create Utility" CssClass="btn btn-warning text-dark" OnClick="btnCreateUtility_Click" />
                    </div>
                    <asp:Label ID="Label1" runat="server" CssClass="text-success mb-2" EnableViewState="false" />
                </div>
            </div>
        </div>
    </form>

    <asp:Label runat="server" ID="MessageLabel" CssClass="text-success" />
    <asp:Label ID="Label3" runat="server" CssClass="text-danger" />
    <asp:Label ID="Label5" runat="server" CssClass="text-success mb-2" EnableViewState="false" />

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>

    <div class="toast-container position-fixed top-0 end-0 p-3">
        <div id="toastMessage" class="toast" role="alert" aria-live="assertive" aria-atomic="true">
            <div class="toast-header">
                <i class="fas fa-bell text-primary me-2"></i>
                <strong class="me-auto">Notification</strong>
                <small class="text-muted">now</small>
                <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
            </div>
            <div class="toast-body" id="toastBody">
                <!-- Message will be inserted here -->
            </div>
        </div>
    </div>

    <script>
        // Enhanced Toast Function
        function showToast(type, message) {
            const toastBody = document.getElementById('toastBody');
            const toast = document.getElementById('toastMessage');
            const toastHeader = toast.querySelector('.toast-header');

            // Set the message
            toastBody.innerHTML = message;

            // Remove existing classes
            toast.classList.remove('bg-success', 'bg-danger', 'bg-warning', 'bg-info');
            toastHeader.classList.remove('bg-success', 'bg-danger', 'bg-warning', 'bg-info');

            // Add appropriate classes based on type
            switch (type) {
                case 'success':
                    toast.classList.add('bg-success', 'text-white');
                    toastHeader.classList.add('bg-success', 'text-white');
                    break;
                case 'error':
                    toast.classList.add('bg-danger', 'text-white');
                    toastHeader.classList.add('bg-danger', 'text-white');
                    break;
                case 'warning':
                    toast.classList.add('bg-warning', 'text-dark');
                    toastHeader.classList.add('bg-warning', 'text-dark');
                    break;
                case 'info':
                    toast.classList.add('bg-info', 'text-white');
                    toastHeader.classList.add('bg-info', 'text-white');
                    break;
            }

            // Show the toast
            const bsToast = new bootstrap.Toast(toast, {
                delay: 5000
            });
            bsToast.show();
        }

        // Mobile sidebar toggle (for future enhancement)
        function toggleSidebar() {
            const sidebar = document.querySelector('.sidebar');
            sidebar.classList.toggle('active');
        }

        // Smooth scrolling for internal links
        document.querySelectorAll('a[href^="#"]').forEach(anchor => {
            anchor.addEventListener('click', function (e) {
                const target = document.querySelector(this.getAttribute('href'));
                if (target) {
                    e.preventDefault();
                    target.scrollIntoView({
                        behavior: 'smooth',
                        block: 'start'
                    });
                }
            });
        });

        // Add loading state to forms
        document.querySelectorAll('form').forEach(form => {
            form.addEventListener('submit', function () {
                const submitButton = form.querySelector('input[type="submit"], button[type="submit"]');
                if (submitButton) {
                    submitButton.classList.add('loading');
                    submitButton.disabled = true;

                    // Re-enable after 5 seconds as fallback
                    setTimeout(() => {
                        submitButton.classList.remove('loading');
                        submitButton.disabled = false;
                    }, 5000);
                }
            });
        });
    </script>
</body>
</html>