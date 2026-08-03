<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Finalize.aspx.cs" Inherits="Bhusamadhan.LandDispute.Entry.Finalize" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../assets/css/cssEntryPage.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

    <div class="container-fluid">

        <!-- Page Title -->
        <div class="row mb-3">
            <div class="col-12 text-center">
                <h4 class="fw-bold text-dark">Finalize Application</h4>
                <asp:Label ID="lblMsg" runat="server" CssClass="text-danger fw-bold"> </asp:Label>
            </div>
        </div>

        <div class="section-card">

            <div class="section-header">
                Search & Filter
            </div>

            <div class="section-body">

                <div class="row">

                    <!-- Action -->
                    <div class="col-lg-3 col-md-6 mb-3">

                        <label class="form-label">बैठक का निष्कर्ष (Action) </label>

                        <asp:DropDownList ID="ddlaction" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlaction_SelectedIndexChanged" >

                            <asp:ListItem Value="0">--चुने--</asp:ListItem>
                            <asp:ListItem Value="1">प्रारंभिक निष्पादन</asp:ListItem>
                            <asp:ListItem Value="4">अस्वीकृत</asp:ListItem>
                            <asp:ListItem Value="2">मापी के लिए निर्धारित</asp:ListItem>
                            <asp:ListItem Value="3">प्रक्रियाधीन</asp:ListItem>
                            <asp:ListItem Value="5">अंतिम निष्पादन</asp:ListItem>
                            <asp:ListItem Value="6">न्यायालय में लंबित</asp:ListItem>

                        </asp:DropDownList>

                    </div>

                    <!-- Page Size -->
                    <div class="col-lg-2 col-md-6 mb-3">

                        <label class="form-label">Page Size  </label>

                        <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">

                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="25">25</asp:ListItem>
                            <asp:ListItem Value="50">50</asp:ListItem>

                        </asp:DropDownList>

                    </div>

                    <!-- Search -->
                    <div class="col-lg-4 col-md-12 mb-3">

                        <label class="form-label">वादी (मोबाइल संख्या) / Application No. </label>

                        <div class="input-group">

                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search..." AutoPostBack="true" OnTextChanged="txtSearch_TextChanged" >
                            </asp:TextBox>

                            <div class="input-group-append">
                                <span class="input-group-text">  <i class="fa fa-search"></i> </span>
                            </div>

                        </div>

                    </div>

                </div>

            </div>

        </div>

        <!-- Report -->

        <div class="section-card mt-4">

            <div class="section-header">Application List </div>

            <div class="section-body">

                <div class="table-responsive">

                    <asp:GridView ID="grdMatterRegistration" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="table table-bordered table-hover table-striped" DataKeyNames="a_id"  >

                        <Columns>

                       
                            <asp:TemplateField HeaderText="Sl. No.">

                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Center" Width="5%" />

                            </asp:TemplateField>

                            <asp:BoundField DataField="ApplicationNo" HeaderText="Application No" />

                     

                            <asp:TemplateField HeaderText="Location">

                                <ItemTemplate>

                                    <strong>जिला :</strong>
                                    <%# Eval("DISTRICTNAME") %>

                                    <hr class="my-1" />

                                    <strong>अनुमंडल :</strong>
                                    <%# Eval("Sd_Name_En") %>

                                    <hr class="my-1" />

                                    <strong>अंचल :</strong>
                                    <%# Eval("BlockName") %>
                                </ItemTemplate>

                            </asp:TemplateField>

                          

                            <asp:TemplateField HeaderText="Police / Village">

                                <ItemTemplate>

                                    <strong>थाना :</strong>
                                    <%# Eval("Police_Station") %>

                                    <hr class="my-1" />

                                    <strong>पंचायत :</strong>
                                    <%# Eval("PanchayatName") %>

                                    <hr class="my-1" />

                                    <strong>राजस्व ग्राम :</strong>
                                    <%# Eval("VILLNAME") %>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:BoundField DataField="vadi_Name" HeaderText="वादी का नाम" />

                            <asp:BoundField DataField="Vadi_MobileNo" HeaderText="मोबाइल" />

                            <asp:BoundField DataField="pratiVadi_Name" HeaderText="प्रतिवादी" />

                            <asp:BoundField DataField="Bhumitype" HeaderText="भूमि का प्रकार" />

                            <asp:BoundField DataField="vivadtype" HeaderText="भूमि विवाद का प्रकार" />

                        

                            <asp:TemplateField HeaderText="Action">

                                <ItemStyle HorizontalAlign="Center" Width="80px" />

                                <ItemTemplate>

                                    <asp:LinkButton ID="btnEdit" OnClick="lnkView_Click" runat="server" CssClass="btn btn-sm btn-success" CommandName="Modify" CommandArgument='<%# Eval("a_id") %>' OnClientClick="openwindow(this);">

                                    <i class="fa fa-pencil-square-o"></i>

                                    </asp:LinkButton>

                                </ItemTemplate>

                            </asp:TemplateField>

                        </Columns>

                    </asp:GridView>

                </div>

          

                <div class="text-center mt-3">

                    <asp:Repeater ID="rptPager" runat="server">

                        <ItemTemplate>

                             <asp:LinkButton ID="lnkPage" OnCommand="Page_Changed" runat="server" CssClass="btn btn-outline-primary btn-sm mx-1" Text='<%# Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>'  > </asp:LinkButton>

                        </ItemTemplate>

                    </asp:Repeater>

                </div>

            </div>

        </div>

    </div>

   
</asp:Content>
