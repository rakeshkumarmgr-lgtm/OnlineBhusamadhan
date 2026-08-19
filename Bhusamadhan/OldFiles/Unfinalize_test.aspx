<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Unfinalize_test.aspx.cs" Inherits="Bhusamadhan.LandDispute.Entry.Unfinalize_test" %>

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

                    <div class="unfinalized-title"><i class="fa fa-folder-open mr-2"></i>Unfinalized Applications  </div>

                    <asp:Label ID="lblTotal" runat="server" CssClass="badge badge-primary"> </asp:Label>

                </div>

            </div>


            <div class="card-body">

                <asp:Label ID="lblMsg" runat="server" CssClass="text-danger d-block mb-3"> </asp:Label>

                <div class="row mb-3">

                    <div class="col-md-5">

                        <label class="form-label">
                            वादी (मोबाइल संख्या)
                        </label>

                        <div class="input-group">

                            <asp:TextBox  ID="txtSearch" runat="server"  CssClass="form-control"  placeholder="वादी का मोबाइल नंबर खोजें..."  MaxLength="10" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged"  > </asp:TextBox>

                            <div class="input-group-append">
                                <span class="input-group-text"> <i class="fa fa-search"></i> </span>
                            </div>

                        </div>

                    </div>

                </div>


                <div class="grid-wrapper">

                    <asp:GridView ID="gvUnfinalized" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover table-sm unfinalized-grid" HeaderStyle-CssClass="thead-light" GridLines="None" AllowPaging="True" PageSize="10" EmptyDataText="कोई Unfinalized Application उपलब्ध नहीं है।" OnPageIndexChanging="gvUnfinalized_PageIndexChanging" OnRowCommand="gvUnfinalized_RowCommand">

                        <Columns>

                            <asp:BoundField DataField="a_id" HeaderText="Application ID" />

                            <%--<asp:BoundField DataField="DISTRICTNAME" HeaderText="District" />

                            <asp:BoundField  DataField="Sd_Name_En" HeaderText="Sub Division" />

                            <asp:BoundField DataField="BlockName"  HeaderText="Block" />--%>
                            <asp:TemplateField HeaderText="जिला / अनुमंडल / अंचल">

                                <ItemTemplate>

                                    <strong><%# Eval("DISTRICTNAME") %></strong>

                                    <hr class="my-1" />

                                    <%# Eval("Sd_Name_En") %>

                                    <hr class="my-1" />

                                    <%# Eval("BlockName") %>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <%-- <asp:BoundField DataField="Police_Station" HeaderText="Police Station" />

                            <asp:BoundField DataField="PanchayatName" HeaderText="Panchayat" />

                            <asp:BoundField DataField="VILLNAME" HeaderText="Village" />--%>

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


                            <asp:TemplateField HeaderText="Action">

                                <ItemTemplate>

                                    <asp:LinkButton ID="lnkContinue" runat="server" CssClass="btn btn-sm btn-primary action-link" CommandName="ContinueApplication" CommandArgument='<%# Eval("a_id") %>'> <i class="fa fa-edit mr-1"></i> Continue </asp:LinkButton>

                                </ItemTemplate>

                            </asp:TemplateField>

                        </Columns>

                        <PagerStyle CssClass="pagination-outer" HorizontalAlign="Center" />

                    </asp:GridView>

                </div>

            </div>

        </div>

    </div>
</asp:Content>
