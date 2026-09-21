<%@ Page Title="Admin - Reset User Password" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResetPasswordByAdmin.aspx.cs" Inherits="Bhusamadhan.LandDispute.Entry.ResetPasswordByAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .page-container {
            padding: 20px;
            max-width: 1200px;
            margin: 0 auto;
            font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, Helvetica, Arial, sans-serif;
        }

        .filter-card {
            background: #ffffff;
            border: 1px solid #e2e8f0;
            border-radius: 8px;
            padding: 18px 20px;
            margin-bottom: 20px;
            box-shadow: 0 2px 5px rgba(0,0,0,0.04);
        }

        .filter-row {
            display: flex;
            flex-wrap: wrap;
            gap: 15px;
            align-items: flex-end;
        }

        .filter-col {
            flex: 1;
            min-width: 240px;
        }

        .form-label {
            display: block;
            font-size: 13px;
            font-weight: 600;
            color: #334155;
            margin-bottom: 5px;
        }

        .form-control {
            width: 100%;
            height: 38px;
            padding: 6px 12px;
            border: 1px solid #cbd5e1;
            border-radius: 5px;
            font-size: 14px;
            box-sizing: border-box;
        }

            .form-control:focus {
                border-color: #2563eb;
                outline: none;
            }

        .btn-action {
            height: 38px;
            padding: 0 18px;
            font-size: 14px;
            font-weight: 600;
            border-radius: 5px;
            border: none;
            cursor: pointer;
            display: inline-flex;
            align-items: center;
            justify-content: center;
        }

        .btn-primary {
            background-color: #2563eb;
            color: #fff;
        }

        .btn-secondary {
            background-color: #64748b;
            color: #fff;
        }

        /* .btn-success {
            background-color: #16a34a;
            color: #fff;
        }

        .btn-danger {
            background-color: #dc2626;
            color: #fff;
        }*/

        .btn-reset-grid {
            background-color: #0284c7;
            color: #ffffff !important;
            padding: 5px 12px;
            font-size: 12px;
            font-weight: 600;
            border-radius: 4px;
            text-decoration: none;
            display: inline-block;
        }

            .btn-reset-grid:hover {
                background-color: #0369a1;
            }

        /* GridView Styling */
        .custom-grid {
            width: 100%;
            border-collapse: collapse;
            background: #ffffff;
            border: 1px solid #e2e8f0;
            border-radius: 8px;
            overflow: hidden;
            box-shadow: 0 2px 6px rgba(0,0,0,0.03);
        }

            .custom-grid th {
                background-color: #f1f5f9;
                color: #1e293b;
                font-size: 13px;
                font-weight: 700;
                text-align: left;
                padding: 12px 14px;
                border-bottom: 2px solid #cbd5e1;
            }

            .custom-grid td {
                padding: 10px 14px;
                font-size: 13px;
                color: #334155;
                border-bottom: 1px solid #f1f5f9;
            }

            .custom-grid tr:hover {
                background-color: #f8fafc;
            }

        /*   .badge {
            padding: 3px 8px;
            border-radius: 12px;
            font-size: 11px;
            font-weight: 600;
        }

        .badge-ok {
            background-color: #dcfce7;
            color: #15803d;
        }

        .badge-warning {
            background-color: #fee2e2;
            color: #b91c1c;
        }*/
        /* Paging Styling */
        .grid-pager table {
            margin: 15px auto;
        }

        .grid-pager td {
            padding: 2px 6px;
            border: none;
        }

        .grid-pager a, .grid-pager span {
            padding: 6px 12px;
            border: 1px solid #cbd5e1;
            border-radius: 4px;
            color: #2563eb;
            text-decoration: none;
            font-weight: 600;
        }

        .grid-pager span {
            background-color: #2563eb;
            color: #ffffff;
            border-color: #2563eb;
        }
        /* Alert notifications */
        .alert-box {
            padding: 12px 16px;
            border-radius: 6px;
            margin-bottom: 18px;
            font-size: 14px;
        }

        .alert-success {
            background: #dcfce7;
            color: #166534;
            border: 1px solid #bbf7d0;
        }

        .alert-danger {
            background: #fee2e2;
            color: #991b1b;
            border: 1px solid #fecaca;
        }
        /* Modal Overlay */
        /*.modal-overlay {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: rgba(15, 23, 42, 0.6);
            display: flex;
            align-items: center;
            justify-content: center;
            z-index: 9999;
        }

        .modal-content {
            background: #ffffff;
            width: 100%;
            max-width: 480px;
            border-radius: 8px;
            padding: 24px;
            box-shadow: 0 10px 25px rgba(0,0,0,0.2);
        }

        .modal-header {
            font-size: 18px;
            font-weight: 700;
            color: #1e293b;
            border-bottom: 1px solid #e2e8f0;
            padding-bottom: 10px;
            margin-bottom: 15px;
        }*/

        /* .user-summary-box {
            background: #f8fafc;
            border-left: 4px solid #0284c7;
            padding: 10px 14px;
            border-radius: 0 4px 4px 0;
            font-size: 13px;
            margin-bottom: 15px;
        }*/

        .text-error {
            color: #dc2626;
            font-size: 12px;
            margin-top: 3px;
            display: block;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div class="page-container">

        <h2 style="margin-top: 0; color: #0f172a;">User Password Reset (Admin)</h2>

        <div>
            <asp:Label ID="lblMsg" runat="server" Font-Bold="true"></asp:Label>
        </div>
        <br />
        <div class="filter-card">
            <div class="filter-row">

                <div class="filter-col" style="flex: 2;">
                    <label class="form-label">Select User Role:</label>
                    <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>

                <div class="filter-col" style="flex: 2;">
                    <label class="form-label">Search (User ID / Name / Mobile):</label>
                    <asp:TextBox ID="txtSearchUserId" runat="server" CssClass="form-control" placeholder="Type keyword..."></asp:TextBox>
                </div>

                <div>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn-action btn-primary" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn-action btn-secondary" OnClick="btnClear_Click" />
                </div>

            </div>
        </div>


        <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False" CssClass="custom-grid" AllowPaging="True" PageSize="50" OnPageIndexChanging="gvUsers_PageIndexChanging">
            <Columns>
                <asp:TemplateField HeaderText="Sl. No." ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">

                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>

                </asp:TemplateField>
                <asp:BoundField DataField="userid" HeaderText="User ID" />
                <asp:BoundField DataField="username" HeaderText="Username" />
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
                <asp:BoundField DataField="UserRole" HeaderText="Role" />
                <asp:BoundField DataField="Attempt_Count" HeaderText="Attempts" />
                <asp:BoundField DataField="PwdResetDate" HeaderText="Pass Reset Date" />
                <asp:TemplateField HeaderText="New Password">
                    <ItemTemplate>
                        <asp:TextBox ID="txtNewPassword" runat="server" placeholder="Enter new password" CssClass="form-control"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnReset" runat="server" Text="Reset Password" CssClass="btn-reset-grid" CommandArgument='<%# Eval("userid") %>' OnClick="btnReset_Click" OnClientClick="return confirm('Are you sure you want to reset password for this user?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pager-style" HorizontalAlign="Center" />
            <EmptyDataTemplate>
                <div style="padding: 15px; color: #777;">No users found for the selected role.</div>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>
