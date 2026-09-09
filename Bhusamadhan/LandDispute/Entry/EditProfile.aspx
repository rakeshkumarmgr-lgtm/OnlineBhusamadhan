<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Inherits="Bhusamadhan.LandDispute.Entry.EditProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .profile-wrapper {
            max-width: 1100px;
            margin: 25px auto;
        }

        .profile-card {
            border: none;
            border-radius: 12px;
            box-shadow: 0 4px 18px rgba(0,0,0,0.10);
            overflow: hidden;
            margin-bottom: 25px;
        }

        .profile-header {
            padding: 20px 25px;
            background: #343a40;
            color: white;
        }

            .profile-header h4 {
                margin: 0;
                font-weight: 600;
            }

            .profile-header p {
                margin: 5px 0 0;
                font-size: 14px;
                opacity: .85;
            }

        .profile-body {
            padding: 25px;
        }

        .form-label {
            font-weight: 600;
            color: #495057;
        }

        .required {
            color: #dc3545;
        }

        .readonly-field {
            background-color: #e9ecef !important;
            cursor: not-allowed;
        }

        .user-grid {
            margin-top: 20px;
        }

            .user-grid th {
                background-color: #343a40;
                color: white;
                font-weight: 600;
                white-space: nowrap;
            }

            .user-grid td {
                vertical-align: middle;
            }

        .edit-section {
            margin-top: 25px;
        }

        .edit-section-header {
            background-color: #f8f9fa;
            border-bottom: 1px solid #dee2e6;
            padding: 15px 20px;
            font-weight: 600;
        }

        .profile-footer {
            padding-top: 20px;
            margin-top: 15px;
            border-top: 1px solid #dee2e6;
        }

        .btn-update {
            min-width: 140px;
        }

        .message {
            margin-bottom: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div class="container-fluid">

        <div class="profile-wrapper">

            <asp:UpdatePanel ID="upUsers" runat="server" UpdateMode="Conditional">

                <ContentTemplate>
                    <div class="card profile-card">

                        <div class="profile-header">

                            <h4><i class="fa fa-user-edit"></i>Edit User Profile  </h4>

                            <p>NICADMIN can update user profile information & User login failed attempt </p>

                        </div>



                        <div class="profile-body">


                            <%--<div class="form-group row">

                                <div class="col-md-7">
                                    <div class="col-md-2">
                                        <label class=" col-form-label">Select Role <span class="required">*</span> </label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>

                            </div>--%>

                            <div class="form-group row mt-1">

                                <label class="col-md-2 col-form-label">Select Role  </label>

                                <div class="col-md-4">

                                    <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged"></asp:DropDownList>

                                </div>

                                <div class="col-md-3">

                                    <asp:TextBox ID="txtSearchUser" runat="server" CssClass="form-control" placeholder="Search User ID, Name, Email or Mobile" AutoPostBack="true" OnTextChanged="txtSearchUser_TextChanged">  </asp:TextBox>

                                </div>

                                <div class="col-md-2">

                                    <asp:LinkButton ID="btnClearSearch" runat="server" CssClass="btn btn-secondary" CausesValidation="false" OnClick="btnClearSearch_Click">  <i class="fa fa-times"></i> Clear </asp:LinkButton>

                                </div>

                            </div>


                            <div class="mb-1 mt-1">
                                <asp:Label ID="lblMsg" runat="server" Visible="false" CssClass="alert alert-info d-block message"> </asp:Label>
                            </div>
                            <div class="user-grid">

                                <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover" EmptyDataText="No users found for the selected role." AllowPaging="true" PageSize="20" OnRowCancelingEdit="gvUsers_RowCancelingEdit" OnRowEditing="gvUsers_RowEditing" OnRowUpdating="gvUsers_RowUpdating" OnPageIndexChanging="gvUsers_PageIndexChanging">

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl. No." ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">

                                            <ItemTemplate>
                                                <%# (gvUsers.PageIndex * gvUsers.PageSize) + Container.DataItemIndex + 1 %>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="User ID">

                                            <ItemTemplate>

                                                <asp:Label ID="lblUserID" runat="server" Text='<%# Eval("UserID") %>'> </asp:Label>

                                            </ItemTemplate>

                                            <EditItemTemplate>

                                                <asp:TextBox ID="txtGridUserID" runat="server" Text='<%# Bind("UserID") %>' CssClass="form-control readonly-field" ReadOnly="true"> </asp:TextBox>

                                            </EditItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Username">

                                            <ItemTemplate>

                                                <%# Eval("username") %>
                                            </ItemTemplate>

                                            <EditItemTemplate>

                                                <asp:TextBox ID="txtGridUsername" runat="server" Text='<%# Bind("username") %>' CssClass="form-control readonly-field" ReadOnly="true">  </asp:TextBox>

                                            </EditItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name">

                                            <ItemTemplate>

                                                <%# Eval("Name") %>
                                            </ItemTemplate>

                                            <EditItemTemplate>

                                                <asp:TextBox ID="txtGridName" runat="server" Text='<%# Bind("Name") %>' CssClass="form-control" MaxLength="150"> </asp:TextBox>

                                            </EditItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Email">

                                            <ItemTemplate>

                                                <%# Eval("Email") %>
                                            </ItemTemplate>

                                            <EditItemTemplate>

                                                <asp:TextBox ID="txtGridEmail" runat="server" Text='<%# Bind("Email") %>' CssClass="form-control" MaxLength="150"> </asp:TextBox>

                                            </EditItemTemplate>

                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Mobile">

                                            <ItemTemplate>

                                                <%# Eval("Mobile") %>
                                            </ItemTemplate>

                                            <EditItemTemplate>

                                                <asp:TextBox ID="txtGridMobile" runat="server" Text='<%# Bind("Mobile") %>' CssClass="form-control" MaxLength="15">  </asp:TextBox>

                                            </EditItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Attempt Count" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">

                                            <ItemTemplate>

                                                <asp:Label ID="lblAttemptCount" runat="server" Text='<%# Eval("Attempt_Count") %>'> </asp:Label>

                                            </ItemTemplate>

                                            <EditItemTemplate>

                                                <asp:TextBox ID="txtGridAttemptCount" runat="server" Text='<%# Bind("Attempt_Count") %>' CssClass="form-control readonly-field" ReadOnly="true"> </asp:TextBox>

                                            </EditItemTemplate>

                                        </asp:TemplateField>

                                        <%--<asp:CommandField HeaderText="Action" ShowEditButton="true"  EditText="Edit"  UpdateText="Update"  CancelText="Cancel" ButtonType="Link" ControlStyle-CssClass="btn btn-sm btn-primary" />--%>


                                        <asp:TemplateField HeaderText="Action">

                                            <ItemTemplate>

                                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CssClass="btn btn-sm btn-primary"> <i class="fa fa-edit"></i> Edit </asp:LinkButton>

                                            </ItemTemplate>


                                            <EditItemTemplate>

                                                <asp:LinkButton ID="btnUpdate" runat="server" CommandName="Update" CssClass="btn btn-sm btn-success"> <i class="fa fa-save"></i> Update </asp:LinkButton>


                                                <asp:LinkButton ID="btnCancel" runat="server" CommandName="Cancel" CausesValidation="false" CssClass="btn btn-sm btn-secondary ml-1"> <i class="fa fa-times"></i> Cancel  </asp:LinkButton>

                                            </EditItemTemplate>

                                        </asp:TemplateField>

                                    </Columns>

                                </asp:GridView>

                            </div>

                        </div>

                    </div>
                </ContentTemplate>

            </asp:UpdatePanel>

        </div>
    </div>
</asp:Content>
