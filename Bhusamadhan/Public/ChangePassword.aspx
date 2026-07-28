<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="Bhusamadhan.Public.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div class="card card-primary">
        <div class="card-header">

            <h3 class="card-title">Change Password</h3>

        </div>

        <div class="card-body">
            <div class=" text-danger font-weight-bold">
                <asp:Label ID="lblErrorMsg" runat="server" />
            </div>

            <div class="form-horizontal">

                <div class="form-group row">
                    <label for="inputUid" class="col-sm-3 col-form-label">User ID<span class="text-danger">*</span></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtUserID" runat="server" CssClass="form-control" placeholder="User ID" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="col-sm-3">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="required !" ControlToValidate="txtUserID"
                            ForeColor="#CC0000" ValidationGroup="aa"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="form-group row">
                    <label for="inputOldPwd" class="col-sm-3 col-form-label">Old Password<span class=" text-danger">*</span></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtOldPassword" runat="server" CssClass="form-control" placeholder="Old Password"
                            onfocus="javascript:BlankIt(this);" TextMode="Password" AutoComplete="off" ToolTip="Password is required."></asp:TextBox>
                    </div>
                    <div class="col-sm-3">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="required !" ControlToValidate="txtOldPassword"
                            ForeColor="#CC0000" ValidationGroup="aa"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group row">
                    <label for="inputNewPwd" class="col-sm-3 col-form-label">New Password<span class="text-danger">*</span></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" placeholder="New Password"
                            onfocus="javascript:BlankIt(this);" TextMode="Password" AutoComplete="off" ToolTip="Password is required."></asp:TextBox>
                    </div>
                    <div class="col-sm-5">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="required !" ControlToValidate="txtNewPassword"
                            ForeColor="#CC0000" ValidationGroup="aa"></asp:RequiredFieldValidator>

                        <asp:RegularExpressionValidator ID="revNewPassword" runat="server"
                            ControlToValidate="txtNewPassword"
                            ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$"
                            ErrorMessage="Password must be 8+ chars, include upper, lower, number & special char."
                            ForeColor="Red"
                            Display="Dynamic"
                            ValidationGroup="aa" />
                    </div>
                </div>

                <div class="form-group row">
                    <label for="inputConfirmPwd" class="col-sm-3 col-form-label">Confirm Password<span class="text-danger">*</span></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtConfirmPassowrd" runat="server" CssClass="form-control" placeholder="Confirm Password"
                            onfocus="javascript:BlankIt(this);" TextMode="Password" AutoComplete="off" ToolTip="Password is required."></asp:TextBox>
                    </div>
                    <div class="col-sm-5">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="required !" ControlToValidate="txtConfirmPassowrd"
                            ForeColor="#CC0000" ValidationGroup="aa"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revConfirmPassword" runat="server"
                            ControlToValidate="txtConfirmPassowrd"
                            ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$"
                            ErrorMessage="Password must be 8+ chars, include upper, lower, number & special char."
                            ForeColor="Red"
                            Display="Dynamic"
                            ValidationGroup="aa" />
                        <asp:CompareValidator ID="cvPasswords" runat="server"
                            ControlToCompare="txtNewPassword"
                            ControlToValidate="txtConfirmPassowrd"
                            ErrorMessage="Passwords do not match."
                            ForeColor="Red"
                            Display="Dynamic"
                            ValidationGroup="aa" />
                    </div>
                </div>
            </div>
            <div class="card-footer ">
                <asp:Button ID="btnChangePwd" runat="server" Text="Change Password" CssClass="btn btn-info" ValidationGroup="aa" OnClick="btnChangePwd_Click" />
            </div>
        </div>
    </div>
</asp:Content>
