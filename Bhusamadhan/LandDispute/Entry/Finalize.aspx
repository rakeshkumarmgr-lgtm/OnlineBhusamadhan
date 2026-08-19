<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Finalize.aspx.cs" Inherits="Bhusamadhan.LandDispute.Entry.Finalize" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .unfinalized-title {
            font-size: 20px;
            font-weight: 600;
            color: #0056b3;
        }

        .unfinalized-grid {
            font-size: 13px;
        }

            .unfinalized-grid th {
                white-space: nowrap;
                vertical-align: middle !important;
                text-align: center;
            }

            .unfinalized-grid td {
                vertical-align: middle !important;
            }

        .action-link {
            white-space: nowrap;
            font-weight: 600;
        }

        .grid-wrapper {
            overflow-x: auto;
        }

        .empty-message {
            padding: 20px;
            text-align: center;
            color: #777;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div class="container-fluid">

        <div class="card shadow-sm">

            <div class="card-header bg-white">

                <div class="d-flex justify-content-between align-items-center">

                    <div class="unfinalized-title"><i class="fa fa-folder-open mr-2"></i>Finalized Applications  </div>

                    <asp:Label ID="lblTotal" runat="server" CssClass="badge badge-primary"> </asp:Label>

                </div>

            </div>


            <div class="card-body">

                <asp:Label ID="lblMsg" runat="server" CssClass="text-danger d-block mb-3"> </asp:Label>

                <div class="row mb-3">

                    <div class="col-md-5">

                        <label class="form-label">बैठक का निष्कर्ष (Action)  </label>
                        <asp:DropDownList ID="ddlaction" runat="server"  CssClass="form-control"  AutoPostBack="true" OnSelectedIndexChanged="ddlaction_SelectedIndexChanged" >
                            <asp:ListItem Value="0">--चुने--</asp:ListItem>
                            <asp:ListItem Value="1">प्रारंभिक निष्पादन</asp:ListItem>
                            <asp:ListItem Value="4">अस्वीकृत</asp:ListItem>
                            <asp:ListItem Value="2">मापी क़े लिए निर्धारित</asp:ListItem>
                            <asp:ListItem Value="3">प्रक्रियाधीन</asp:ListItem>
                            <asp:ListItem Value="5">अंतिम निष्पादन</asp:ListItem>
                            <asp:ListItem Value="6">न्यायालय में लंबित</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-5">

                        <label class="form-label">वादी (मोबाइल संख्या) / Application No  </label>

                        <div class="input-group">

                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="वादी का मोबाइल नंबर खोजें..." MaxLength="10" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged"> </asp:TextBox>

                            <div class="input-group-append">
                                <span class="input-group-text"><i class="fa fa-search"></i></span>
                            </div>

                        </div>

                    </div>

                </div>


                <div class="grid-wrapper">

                    <asp:GridView ID="gvFinalized" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover table-sm unfinalized-grid" HeaderStyle-CssClass="thead-light" GridLines="None" AllowPaging="True" PageSize="10" EmptyDataText="कोई Finalized Application उपलब्ध नहीं है।" OnPageIndexChanging="gvFinalized_PageIndexChanging">

                        <Columns>

                            <asp:BoundField DataField="ApplicationNo" HeaderText="Application No" />

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

                            <asp:BoundField DataField="vadi_Name" HeaderText="Vadi Name" />

                            <asp:BoundField DataField="Vadi_MobileNo" HeaderText="Vadi Mobile" />

                            <asp:BoundField DataField="pratiVadi_Name" HeaderText="Prati Vadi Name" />

                            <asp:BoundField DataField="Bhumitype" HeaderText="Bhumi Type" />

                            <asp:BoundField DataField="vivadtype" HeaderText="Vivad Type" />


                        </Columns>

                        <PagerStyle CssClass="pagination-outer" HorizontalAlign="Center" />

                    </asp:GridView>

                </div>

            </div>

        </div>

    </div>
</asp:Content>
