<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LoginDetailsRPT.aspx.cs" Inherits="Bhusamadhan.LandDispute.Reports.LoginDetailsRPT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div class="container-fluid">

        <div class="card shadow-sm">

            <!-- Header -->
            <div class="card-header bg-primary text-white text-center">
                <h4 class="mb-0 font-weight-bold">LOGIN LIST REPORT
                </h4>
            </div>

            <div class="card-body">

                <!-- Message -->
                <div class="row mb-4">

                    <div class="col-md-12 text-center">

                        <asp:Label ID="lblMsg" runat="server" CssClass="font-weight-bold text-danger">  </asp:Label>

                        <asp:HiddenField ID="hdn1" runat="server" />

                    </div>

                </div>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">

                    <ContentTemplate>

                        <!-- Location Filters -->
                        <div class="row">

                            <div class="col-lg-2 col-md-4 mb-3">

                                <label class="form-label">Commissionary</label>

                                <asp:DropDownList ID="ddlCommissionary" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCommissionary_SelectedIndexChanged"></asp:DropDownList>

                            </div>

                            <div class="col-lg-2 col-md-4 mb-3">

                                <label class="form-label">District  </label>

                                <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>

                            </div>

                            <div class="col-lg-2 col-md-4 mb-3">

                                <label class="form-label">Sub-Division</label>

                                <asp:DropDownList ID="ddlSubDivision" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSubDivision_SelectedIndexChanged"></asp:DropDownList>

                            </div>

                            <div class="col-lg-2 col-md-4 mb-3">

                                <label class="form-label">Circle </label>

                                <asp:DropDownList ID="ddlBlock" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged"></asp:DropDownList>

                            </div>

                            <div class="col-lg-2 col-md-4 mb-3">

                                <label class="form-label">Police Station </label>

                                <asp:DropDownList ID="ddlPoliceStation" runat="server" CssClass="form-control"></asp:DropDownList>

                            </div>

                            <div class="col-lg-2 col-md-4 mb-3">

                                <label class="form-label">Role  </label>

                                <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control"></asp:DropDownList>

                            </div>

                        </div>

                    </ContentTemplate>

                </asp:UpdatePanel>

                <!-- Buttons -->
                <div class="row mt-2">

                    <div class="col-md-12 text-center">

                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary mr-2" OnClick="btnSearch_Click" />&nbsp;&nbsp;&nbsp;

                        <asp:Button ID="btn_Export" runat="server" Text="Export to Excel" CssClass="btn btn-success" OnClick="btn_Export_Click" />

                    </div>

                </div>

                <!-- Report Section -->
                <div class="row mt-3">
                    <div class="col-12">

                        <asp:Panel ID="Pnldata" runat="server" ScrollBars="Auto" Visible="false">

                            <div class="table-responsive">

                                <table class="table table-bordered table-hover table-striped table-sm align-middle">
                                    <thead class="table-dark text-center">
                                        <tr>
                                            <th style="width: 4%;">Sl. No.</th>
                                            <th style="width: 10%;">Division</th>
                                            <th style="width: 22%;">Location</th>
                                            <th style="width: 12%;">Police Station</th>
                                            <th style="width: 18%;">User Name</th>
                                            <th style="width: 12%;">Role</th>
                                            <th style="width: 10%;">User ID</th>
                                            <th style="width: 10%;">Mobile<br />
                                                Email</th>
                                            <%-- <th style="width: 18%;">Email</th>--%>
                                        </tr>
                                    </thead>

                                    <tbody>

                                        <asp:Repeater ID="rptLoginDetails" runat="server">
                                            <HeaderTemplate>

                                                <tr>
                                                    <td colspan="9" class="fw-bold text-center">
                                                        <asp:Label ID="lblHeaderInfo" runat="server"></asp:Label>
                                                    </td>
                                                </tr>

                                            </HeaderTemplate>
                                            <ItemTemplate>

                                                <tr>

                                                    <td class="text-center">
                                                        <%# Container.ItemIndex + 1 %>
                                                    </td>

                                                    <td>
                                                        <%# Eval("DIVISIONAME") %>
                                                    </td>

                                                    <td>
                                                        <strong>District :</strong> <%# Eval("DISTRICTNAME") %>
                                                        <br />

                                                        <strong>Sub-Division :</strong> <%# Eval("Sd_Name_En") %>
                                                        <br />

                                                        <strong>Block :</strong> <%# Eval("BlockName") %>
                                                    </td>

                                                    <td>
                                                        <%# Eval("Police_Station") %>
                                                    </td>

                                                    <td>
                                                        <asp:Literal ID="litUserName" runat="server" Text='<%# Eval("UserName") %>'> </asp:Literal>
                                                    </td>

                                                    <td>
                                                        <%# Eval("RoleDesc") %>
                                                    </td>

                                                    <td>
                                                        <%# Eval("UserID") %>
                                                    </td>
                                                    <td>
                                                        <strong>Mobile :</strong> <%# Eval("Mobile") %>
                                                        <br />

                                                        <strong>Email :</strong> <%# Eval("Email") %>
                                                       
                                                    </td>

                                                   <%-- <td>
                                                        <%# Eval("Mobile") %>
                                                    </td>

                                                    <td>
                                                        <%# Eval("Email") %>
                                                    </td>--%>

                                                </tr>

                                            </ItemTemplate>

                                        </asp:Repeater>



                                    </tbody>

                                </table>

                            </div>

                        </asp:Panel>

                    </div>
                </div>

            </div>

        </div>

    </div>
</asp:Content>
