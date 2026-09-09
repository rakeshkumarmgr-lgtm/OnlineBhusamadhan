<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddMenu.aspx.cs" Inherits="Bhusamadhan.LandDispute.Entry.AddMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .menu-card {
            margin-bottom: 20px;
        }

        .menu-header {
            font-weight: 600;
        }

        .child-row {
            padding-left: 35px;
        }

        .permission-child {
            margin-left: 30px;
        }

        .table td,
        .table th {
            vertical-align: middle;
        }

        .status-active {
            color: green;
            font-weight: 600;
        }

        .status-inactive {
            color: red;
            font-weight: 600;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div class="container-fluid mt-4">

        <div class="card shadow">

            <div class="card-header bg-primary text-white">

                <h5 class="mb-0"><i class="fas fa-bars mr-2"></i>Add Menu </h5>

            </div>

            <div class="card-body">

                <asp:Label ID="lblMsg" runat="server" ForeColor="Red" CssClass="text-danger mb-1"> </asp:Label>

                <div class="card menu-card">

                    <div class="card-header">

                        <span class="menu-header"><i class="fas fa-file mr-2"></i>Child Menu / Page Management </span>

                    </div>

                    <div class="card-body">

                        <asp:HiddenField ID="hfChildMenuID" runat="server" Value="0" />


                        <div class="row">

                            <div class="col-md-3 mb-3">

                                <label>Parent Menu</label>

                                <asp:DropDownList ID="ddlChildParent" runat="server" CssClass="form-control"></asp:DropDownList>

                            </div>


                            <div class="col-md-3 mb-3">

                                <label>Page / Menu Name</label>

                                <asp:TextBox ID="txtChildMenuName" runat="server" CssClass="form-control" MaxLength="100"> </asp:TextBox>

                            </div>


                            <div class="col-md-3 mb-3">

                                <label>Navigate URL</label>

                                <asp:TextBox ID="txtChildNavigateUrl" runat="server" CssClass="form-control" MaxLength="250"> </asp:TextBox>

                            </div>

                            <div class="col-md-3 mb-3">

                                <label>Display Order</label>

                                <asp:TextBox ID="txtChildDisplayOrder" runat="server" CssClass="form-control"> </asp:TextBox>

                            </div>

                        </div>

                        <div class="form-check mb-3">

                            <asp:CheckBox ID="chkChildActive" runat="server" Checked="true" />

                            <label class="form-check-label">Active </label>

                        </div>


                        <asp:Button ID="btnAddChild" runat="server" Text="Add Page" CssClass="btn btn-success mr-2" OnClientClick="return confirm('Are you sure you want to add menu/page?');" OnClick="btnAddChild_Click" />


                    </div>

                </div>

                <div class="card menu-card">

                    <div class="card-header">

                        <b>Existing Pages</b>

                    </div>

                    <div class="card-body">

                        <asp:GridView ID="gvChildMenus" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover" DataKeyNames="ChildMenuID">

                            <Columns>

                                <asp:TemplateField HeaderText="Sl. No." ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">

                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:BoundField DataField="ChildMenuID" HeaderText="Child ID" ReadOnly="true" />

                                <asp:TemplateField HeaderText="Parent Menu">

                                    <ItemTemplate>

                                        <asp:DropDownList ID="ddlGridParentMenu" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>

                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Menu Name">

                                    <ItemTemplate>

                                        <asp:TextBox ID="txtGridMenuName" runat="server" Text='<%# Eval("MenuName") %>' CssClass="form-control form-control-sm" MaxLength="100"></asp:TextBox>

                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="URL">

                                    <ItemTemplate>

                                        <asp:TextBox ID="txtGridNavigateUrl" runat="server" Text='<%# Eval("NavigateUrl") %>' CssClass="form-control form-control-sm" MaxLength="250"> </asp:TextBox>

                                    </ItemTemplate>

                                </asp:TemplateField>

                             

                                <asp:TemplateField HeaderText="Order">

                                    <ItemTemplate>

                                        <asp:TextBox ID="txtGridDisplayOrder" runat="server" Text='<%# Eval("DisplayOrder") %>' CssClass="form-control form-control-sm" Width="70px"> </asp:TextBox>

                                    </ItemTemplate>

                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Active">

                                    <ItemTemplate>

                                        <asp:CheckBox ID="chkGridActive" runat="server" Checked='<%# Convert.ToBoolean(Eval("IsActive")) %>' />

                                    </ItemTemplate>

                                </asp:TemplateField>

                            </Columns>

                        </asp:GridView>


                        <div class="mt-3">

                            <asp:Button ID="btnSaveChildMenus" runat="server" Text="Save All Changes" CssClass="btn btn-success"  OnClick="btnSaveChildMenus_Click" />

                        </div>

                    </div>

                </div>



            </div>

        </div>

    </div>
</asp:Content>
