<%@ Page Title="Admin - Reset User Password" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResetPasswordByAdmin.aspx.cs" Inherits="Bhusamadhan.LandDispute.Entry.ResetPasswordByAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .reset-card {
            max-width: 650px;
            margin: 25px auto;
            border: 1px solid #e0e0e0;
            border-radius: 8px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.05);
            background: #ffffff;
            padding: 24px;
            font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, Helvetica, Arial, sans-serif;
        }

        .form-group {
            margin-bottom: 15px;
        }

        .form-label {
            display: block;
            font-weight: 600;
            margin-bottom: 5px;
            color: #333;
        }

        .form-control {
            width: 100%;
            padding: 8px 12px;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
            font-size: 14px;
        }

            .form-control:focus {
                border-color: #0066cc;
                outline: none;
            }

        .btn-row {
            display: flex;
            gap: 10px;
        }

        .btn-custom {
            padding: 8px 16px;
            border-radius: 4px;
            border: none;
            cursor: pointer;
            font-size: 14px;
            font-weight: 600;
        }

        .btn-primary-custom {
            background-color: #0d6efd;
            color: #fff;
        }

        .btn-success-custom {
            background-color: #198754;
            color: #fff;
        }

        .btn-secondary-custom {
            background-color: #6c757d;
            color: #fff;
        }

        .user-details-panel {
            background-color: #f8f9fa;
            border-left: 4px solid #0d6efd;
            padding: 12px 16px;
            margin: 15px 0;
            border-radius: 0 4px 4px 0;
        }

            .user-details-panel table {
                width: 100%;
                border-collapse: collapse;
            }

            .user-details-panel td {
                padding: 4px 8px;
                font-size: 13px;
            }

        .alert-box {
            padding: 10px 14px;
            border-radius: 4px;
            margin-bottom: 15px;
            font-size: 14px;
        }

        .alert-success {
            background-color: #d1e7dd;
            color: #0f5132;
            border: 1px solid #badbcc;
        }

        .alert-danger {
            background-color: #f8d7da;
            color: #842029;
            border: 1px solid #f5c2c7;
        }

        .text-danger {
            color: #dc3545;
            font-size: 12px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div class="reset-card">
        <h3 style="margin-top: 0; border-bottom: 2px solid #f0f0f0; padding-bottom: 10px; color: #2c3e50;">Reset User Password (Admin)
        </h3>
        <!-- Status Message Banner -->
        <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="alert-box">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </asp:Panel>
        <!-- Step 1: Search User by UserID -->
        <div class="form-group">
            <label class="form-label" for="<%= txtSearchUserID.ClientID %>">Enter User ID:</label>
            <div style="display: flex; gap: 8px;">
                <asp:TextBox ID="txtSearchUserID" runat="server" CssClass="form-control" placeholder="e.g. shoopt5102001"></asp:TextBox>
                <asp:Button ID="btnSearchUser" runat="server" Text="Search User" CssClass="btn-custom btn-primary-custom"  CausesValidation="false" />
            </div>
            <asp:RequiredFieldValidator ID="rfvSearchUserID" runat="server" ControlToValidate="txtSearchUserID"
                ValidationGroup="SearchGroup" ErrorMessage="Please enter a User ID." CssClass="text-danger" Display="Dynamic" />
        </div>
        <!-- Step 2: Show Fetched User Information -->
        <asp:Panel ID="pnlUserDetails" runat="server" Visible="false" CssClass="user-details-panel">
            <table>
                <tr>
                    <td style="width: 25%; font-weight: bold;">User ID:</td>
                    <td>
                        <asp:Label ID="lblUserID" runat="server" Font-Bold="true"></asp:Label></td>
                </tr>
                <tr>
                    <td style="font-weight: bold;">Full Name:</td>
                    <td>
                        <asp:Label ID="lblName" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="font-weight: bold;">Username / Office:</td>
                    <td>
                        <asp:Label ID="lblUsername" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="font-weight: bold;">Role:</td>
                    <td>
                        <asp:Label ID="lblRole" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="font-weight: bold;">Mobile:</td>
                    <td>
                        <asp:Label ID="lblMobile" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="font-weight: bold;">Lockout Attempts:</td>
                    <td>
                        <asp:Label ID="lblAttemptCount" runat="server"></asp:Label></td>
                </tr>
            </table>
        </asp:Panel>
        <!-- Step 3: Set New Password Form -->
        <asp:Panel ID="pnlPasswordReset" runat="server" Visible="false">
            <div class="form-group">
                <label class="form-label" for="<%= txtNewPassword.ClientID %>">New Password:</label>
                <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Enter new password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ControlToValidate="txtNewPassword"
                    ValidationGroup="ResetGroup" ErrorMessage="New password is required." CssClass="text-danger" Display="Dynamic" />
            </div>
            <div class="form-group">
                <label class="form-label" for="<%= txtConfirmPassword.ClientID %>">Confirm New Password:</label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Re-enter new password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword"
                    ValidationGroup="ResetGroup" ErrorMessage="Please confirm new password." CssClass="text-danger" Display="Dynamic" />
                <asp:CompareValidator ID="cvPasswords" runat="server" ControlToValidate="txtConfirmPassword"
                    ControlToCompare="txtNewPassword" ValidationGroup="ResetGroup"
                    ErrorMessage="Passwords do not match." CssClass="text-danger" Display="Dynamic" />
            </div>
            <div class="btn-row" style="margin-top: 20px;">
                <asp:Button ID="btnResetPassword" runat="server" Text="Update Password"
                    CssClass="btn-custom btn-success-custom" ValidationGroup="ResetGroup" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel / Clear"
                    CssClass="btn-custom btn-secondary-custom" CausesValidation="false"  />
            </div>
        </asp:Panel>
    </div>
</asp:Content>
