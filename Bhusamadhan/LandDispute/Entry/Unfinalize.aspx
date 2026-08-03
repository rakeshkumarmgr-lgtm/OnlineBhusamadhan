<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Unfinalize.aspx.cs" Inherits="Bhusamadhan.LandDispute.Entry.Unfinalize" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../assets/css/cssEntryPage.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

    <div class="container-fluid">

        <!-- Page Heading -->
        <div class="row mb-3">
            <div class="col-12 text-center">
                <h4 class="font-weight-bold text-dark">Unfinalize Application </h4>

                <asp:Label ID="lblMsg" runat="server" CssClass="text-danger font-weight-bold">   </asp:Label>
            </div>
        </div>

        <!-- Search Card -->
        <div class="card shadow-sm">

            <div class="card-header bg-light">
                <h5 class="mb-0">Search Application</h5>
            </div>

            <div class="card-body">

                <!-- Search Filters -->
                <div class="row">

                    <!-- Page Size -->
                    <div class="col-md-3 mb-3">

                        <label class="form-label">Page Size </label>

                        <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">

                            <asp:ListItem Text="10" Value="10" />
                            <asp:ListItem Text="25" Value="25" />
                            <asp:ListItem Text="50" Value="50" />

                        </asp:DropDownList>

                    </div>

                    <!-- Search -->
                    <div class="col-md-5 mb-3">

                        <label class="form-label">वादी (मोबाइल संख्या) / Application No.  </label>

                        <div class="input-group">

                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search..." AutoPostBack="true" OnTextChanged="txtSearch_TextChanged">  </asp:TextBox>

                            <div class="input-group-append">
                                <span class="input-group-text">
                                    <i class="fa fa-search"></i>
                                </span>
                            </div>

                        </div>

                    </div>

                </div>


                <div class="row mt-3">

                    <div class="col-12">

                        <div class="table-responsive">

                            <asp:GridView ID="grdMatterRegistration" runat="server" Width="100%" AutoGenerateColumns="false" CssClass="table table-bordered table-striped table-hover fontsize" DataKeyNames="a_id">

                                <Columns>


                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <asp:BoundField
                                        DataField="ApplicationNo"
                                        HeaderText="Application No" />


                                    <asp:TemplateField HeaderText="जिला / अनुमंडल / अंचल">

                                        <ItemTemplate>

                                            <strong><%# Eval("DISTRICTNAME") %></strong>

                                            <hr class="my-1" />

                                            <%# Eval("Sd_Name_En") %>

                                            <hr class="my-1" />

                                            <%# Eval("BlockName") %>
                                        </ItemTemplate>

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="थाना / ग्राम पंचायत / राजस्व ग्राम">

                                        <ItemTemplate>

                                            <strong><%# Eval("Police_Station") %></strong>

                                            <hr class="my-1" />

                                            <%# Eval("PanchayatName") %>

                                            <hr class="my-1" />

                                            <%# Eval("VILLNAME") %>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:BoundField DataField="vadi_Name" HeaderText="वादी का नाम" />

                                    <asp:BoundField DataField="Vadi_MobileNo" HeaderText="मोबाइल संख्या" />

                                    <asp:BoundField DataField="pratiVadi_Name" HeaderText="प्रतिवादी का नाम" />

                                    <asp:BoundField DataField="Bhumitype" HeaderText="भूमि का प्रकार" />

                                    <asp:BoundField DataField="vivadtype" HeaderText="भूमि विवाद का प्रकार" />


                                    <asp:TemplateField HeaderText="Action">

                                        <ItemStyle HorizontalAlign="Center" Width="6%" />

                                        <ItemTemplate>

                                            <asp:LinkButton ID="btnEdit" OnClick="lnkView_Click" runat="server" CssClass="btn btn-sm btn-success" CommandName="Modify" CommandArgument='<%# Eval("a_id") %>' OnClientClick="openwindow(this);">

                                                <i class="fa fa-pencil-square-o"></i>

                                            </asp:LinkButton>

                                        </ItemTemplate>

                                    </asp:TemplateField>

                                </Columns>

                            </asp:GridView>

                        </div>

                    </div>

                </div>

                <!-- Pager -->
                <div class="row mt-3">

                    <div class="col-12 text-center">

                        <asp:Repeater ID="rptPager" runat="server">

                            <ItemTemplate>

                                <asp:LinkButton ID="lnkPage" runat="server" CssClass="btn btn-outline-primary btn-sm mx-1" Text='<%# Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>' OnCommand="Page_Changed">  </asp:LinkButton>

                            </ItemTemplate>

                        </asp:Repeater>

                    </div>

                </div>

            </div>

        </div>

    </div>
</asp:Content>
