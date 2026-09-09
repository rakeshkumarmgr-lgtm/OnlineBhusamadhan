<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MenuAccessControl.aspx.cs" Inherits="Bhusamadhan.LandDispute.Entry.MenuAccessControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <h2>Menu Access Control</h2>

    <div class="container-fluid">

        <div class="card shadow-sm">

            <div class="card-header bg-primary text-white">
                <h5 class="mb-0">
                    <i class="fas fa-user-shield mr-2"></i>
                    Menu Access Control
                </h5>
            </div>


            <div class="card-body">

                <div class="alert alert-info">
                    <i class="fas fa-info-circle mr-2"></i>

                    Select a role to manage its menu/page access.
                You can add new menu access or grant/revoke access
                for an existing permission.
                </div>

                <div class="row mb-4">

                    <div class="col-md-5">

                        <label class="font-weight-bold">Select Role </label>

                        <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged"></asp:DropDownList>

                    </div>


                </div>

                <asp:Label ID="lblMsg" runat="server" CssClass="d-block mb-3" ForeColor="Red">  </asp:Label>

                <div class="card border-success mb-4">

                    <div class="card-header bg-light">

                        <h6 class="mb-0 text-success">

                            <i class="fas fa-plus-circle mr-2"></i>

                            Add New Menu / Page Access

                        </h6>

                    </div>


                    <div class="card-body">

                        <div class="alert alert-secondary">

                            <i class="fas fa-info-circle mr-1"></i>

                            Use this section only when the selected menu/page does not already exist for the selected role.
                            Clicking <strong>Grant Access</strong> will insert a new record.

                        </div>

                        <div class="row">


                            <div class="col-md-4">

                                <label class="font-weight-bold">Parent Menu </label>

                                <asp:DropDownList ID="ddlParentMenu" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlParentMenu_SelectedIndexChanged"></asp:DropDownList>

                            </div>

                            <div class="col-md-5">

                                <label class="font-weight-bold">Menu / Page </label>

                                <asp:DropDownList ID="ddlChildMenu" runat="server" CssClass="form-control"></asp:DropDownList>

                            </div>

                            <div class="col-md-3">

                                <label class="d-block">
                                    &nbsp;
                                </label>

                                <asp:Button ID="btnGrantAccess" runat="server" Text="Grant Access New Menu" CssClass="btn btn-success btn-block" OnClick="btnGrantAccess_Click" OnClientClick="return confirm('Are you sure you want to grant access to this menu/page?');" />

                            </div>

                        </div>

                    </div>

                </div>


                <div class="card border-primary">

                    <div class="card-header bg-light">

                        <h6 class="mb-0 text-primary"><i class="fas fa-list mr-2"></i>Existing Menu Permissions </h6>

                    </div>


                    <div class="card-body">


                        <div class="table-responsive">

                            <asp:GridView ID="gvMenuPermission" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" DataKeyNames="ParentMenuID,ChildMenuID,MenuType" OnRowCommand="gvMenuPermission_RowCommand">

                                <Columns>
                                    <asp:BoundField DataField="SL_No" HeaderText="Access SL" />

                                    <asp:TemplateField HeaderText="Type">

                                        <ItemTemplate>

                                            <asp:Label ID="lblMenuType" runat="server" Text='<%# Eval("MenuType") %>' CssClass="font-weight-bold"> </asp:Label>

                                        </ItemTemplate>

                                        <ItemStyle Width="100px" />

                                    </asp:TemplateField>


                                    <asp:BoundField DataField="ParentMenuName" HeaderText="Parent Menu" />


                                    <asp:BoundField DataField="ChildMenuName" HeaderText="Menu / Page" />

                                    <asp:BoundField DataField="NavigateUrl" HeaderText="Navigate URL" />

                                    <asp:TemplateField
                                        HeaderText="Access Status">

                                        <ItemTemplate>

                                            <asp:Label ID="lblAccessStatus" runat="server" Text='<%# Eval("AccessStatus") %>' CssClass='<%# GetStatusCss(Eval("AccessStatus").ToString()) %>'> </asp:Label>

                                        </ItemTemplate>

                                        <ItemStyle Width="160px" />

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Action">

                                        <ItemTemplate>

                                            <asp:LinkButton ID="btnPermission" runat="server" CommandName="TogglePermission" CommandArgument='<%# Container.DataItemIndex %>' CssClass='<%# GetButtonCss(Eval("AccessStatus").ToString()) %>' OnClientClick='<%# GetConfirmMessage(Eval("AccessStatus").ToString()) %>'>

                                            <i class='<%# GetButtonIcon(Eval("AccessStatus").ToString()) %>'></i>

                                            <%# GetButtonText(Eval("AccessStatus").ToString()) %>

                                            </asp:LinkButton>

                                        </ItemTemplate>

                                        <ItemStyle Width="130px" />

                                    </asp:TemplateField>


                                </Columns>


                                <HeaderStyle CssClass="thead-dark" />


                                <EmptyDataTemplate>

                                    <div class="alert alert-warning mb-0">

                                        <i class="fas fa-info-circle mr-1"></i>

                                        No menu/page permission has been
                                       assigned to the selected role.

                                    </div>

                                </EmptyDataTemplate>

                            </asp:GridView>

                        </div>

                    </div>

                </div>


            </div>

        </div>

    </div>
</asp:Content>
