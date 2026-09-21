<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApplicationDistConsolidateDashboard.aspx.cs" Inherits="Bhusamadhan.LandDispute.Reports.Consolidate.ApplicationDistConsolidateDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

    <div class="container-fluid">
        <div class="card">
            <div class="card-body">

                <div class="row mb-3">
                    <div class="col-12 text-center">
                        <h4 class="text-dark font-weight-bold mb-0">District Wise Application Consolidated Report </h4>
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-12 text-center">
                        <asp:Label ID="lblMsg" runat="server" Visible="false" CssClass="text-danger font-weight-bold"> </asp:Label>
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-12 text-center">
                        <asp:Label ID="lbltext" runat="server" Visible="false" CssClass="text-dark" Style="text-align: center; font-weight: bold;"></asp:Label>
                    </div>
                </div>

                <div class="row align-items-end mb-3">


                    <div class="col-md-2 mb-2">
                        <asp:Button ID="btnback" runat="server" Text="Back" Visible="false" CssClass="form-control btn btn-danger" />
                    </div>

                    <!-- From Date -->
                    <div class="col-md-2 mb-2">
                        <label for="txtFromdate" class="font-weight-bold">From Date </label>

                        <div class="input-group">
                            <asp:TextBox ID="txtFromdate" runat="server" ReadOnly="true" placeholder="dd-mm-yyyy" CssClass="form-control"> </asp:TextBox>

                            <div class="input-group-append">

                                <cc1:CalendarExtender ID="popCalendarFrom" runat="server" TargetControlID="txtFromdate" Format="dd-MM-yyyy" />

                                <asp:RequiredFieldValidator ID="RFVFromDate" runat="server" ControlToValidate="txtFromdate" ValidationGroup="a" ForeColor="Red" ErrorMessage="*" />
                            </div>
                        </div>
                    </div>

                    <div class="col-md-2 mb-2">
                        <label for="txTodate" class="font-weight-bold">To Date </label>

                        <div class="input-group">
                            <asp:TextBox ID="txTodate" runat="server" ReadOnly="true" placeholder="dd-mm-yyyy" CssClass="form-control"> </asp:TextBox>

                            <div class="input-group-append">

                                <cc1:CalendarExtender ID="popCalendarTo" runat="server" TargetControlID="txTodate" Format="dd-MM-yyyy" />
                                <asp:RequiredFieldValidator ID="RFVToDate" runat="server" ControlToValidate="txTodate" ValidationGroup="a" ForeColor="Red" ErrorMessage="*" />
                            </div>
                        </div>
                    </div>

                    <div class="col-md-2 mb-2">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="form-control btn btn-primary" OnClick="btnSearch_Click" />
                    </div>

                    <div class="col-md-2 mb-2">
                        <asp:Button ID="btn_Export" runat="server" Text="Export To Excel" CssClass="form-control btn btn-primary" OnClick="btn_Export_Click" />
                    </div>

                </div>
                <!-- Report Grid Area -->
                <div class="row">
                    <div class="col-md-12">

                        <asp:Panel ID="pnlDist" runat="server" ScrollBars="Auto">


                            <asp:Label ID="lblPrintDateforDistrict" runat="server" CssClass="text-success small"> </asp:Label>
                            <br />
                            <label class="form-label fw-bold">Click on the District Name to view SubDivision-Wise Application Status <span class="text-danger">*</span></label>
                            <div class="mt-2">

                                <asp:GridView ID="grd_District" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="True" ShowHeaderWhenEmpty="True"
                                    EmptyDataText="No Record(s) found" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-Font-Size="Large"
                                    CssClass="table table-bordered table-hover table-sm mb-0" GridLines="None" OnRowCommand="grd_District_RowCommand">

                                    <Columns>

                                        <asp:TemplateField HeaderText="Sl. No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>

                                            <HeaderStyle Width="5%" HorizontalAlign="Center" BackColor="#1C6794" ForeColor="White" Font-Bold="True" />

                                            <ItemStyle HorizontalAlign="Center" />

                                            <FooterStyle BackColor="#1C6794" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="District">

                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDistrict" runat="server" CommandName="DistrictClick" CommandArgument='<%# Eval("DISTRICTCODE") + "," + Eval("DISTRICTNAME") %>'
                                                    ForeColor="Blue" Font-Underline="false" ToolTip="Click here To View Sub-Division-Wise Application Status">
                                                        <%# Eval("DISTRICTNAME") %>
                                                </asp:LinkButton>
                                            </ItemTemplate>

                                            <HeaderStyle Width="30%" HorizontalAlign="Center" BackColor="#1C6794" ForeColor="White" Font-Bold="True" />

                                            <ItemStyle HorizontalAlign="Left" />

                                            <FooterStyle BackColor="#1C6794" ForeColor="White" Font-Bold="True" HorizontalAlign="Left" />

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="कुल आवेदन">

                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkTotal" runat="server" CommandArgument='<%# Eval("DISTRICTCODE") %>' OnClick="lnkTotal_Click" ForeColor="Blue" Font-Underline="false"> <%# Eval("Total") %> </asp:LinkButton>

                                                <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle Width="20%" HorizontalAlign="Center" BackColor="#1C6794" ForeColor="White" Font-Bold="True" />

                                            <ItemStyle HorizontalAlign="Center" />

                                            <FooterStyle BackColor="#1C6794" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />

                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="पूर्ण प्रविष्टि">

                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkTotalFinalize" runat="server" CommandArgument='<%# Eval("DISTRICTCODE") %>' OnClick="lnkTotalFinalize_Click" ForeColor="Blue" Font-Underline="false"> <%# Eval("Finalize") %> </asp:LinkButton>

                                                <asp:Label ID="lblTotalFinalize" runat="server" Text='<%# Eval("Finalize") %>' Visible="false"> </asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle Width="20%" HorizontalAlign="Center" BackColor="#1C6794" ForeColor="White" Font-Bold="True" />

                                            <ItemStyle HorizontalAlign="Center" />

                                            <FooterStyle BackColor="#1C6794" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="आंशिक प्रविष्टि">

                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkTotalUnfinalize" runat="server" CommandArgument='<%# Eval("DISTRICTCODE") %>' OnClick="lnkTotalUnfinalize_Click" ForeColor="Blue" Font-Underline="false"> <%# Eval("Unfinalize") %> </asp:LinkButton>

                                                <asp:Label ID="lblTotalUnfinalize" runat="server" Text='<%# Eval("Unfinalize") %>' Visible="false"> </asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle Width="20%" HorizontalAlign="Center" BackColor="#1C6794" ForeColor="White" Font-Bold="True" />

                                            <ItemStyle HorizontalAlign="Center" />

                                            <FooterStyle BackColor="#1C6794" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />

                                        </asp:TemplateField>

                                    </Columns>

                                    <EmptyDataRowStyle Font-Size="Large" ForeColor="Red" />


                                    <HeaderStyle BackColor="#1C6794" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />

                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />

                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />

                                    <FooterStyle BackColor="#1C6794" ForeColor="White" Font-Bold="True" />

                                </asp:GridView>

                            </div>

                        </asp:Panel>

                        <asp:Panel ID="pnlCircle" runat="server" ScrollBars="Auto">

                            <asp:Label ID="lblPrintDateforCircle" runat="server" CssClass="text-success small"> </asp:Label>

                            <br />


                            <label class="form-label fw-bold">Click on the Block/Circle Name to view PoliceStation-Wise Application Status<span class="text-danger">*</span></label>
                            <div class="mt-2">

                                <asp:GridView ID="grdCircle" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="True" ShowHeaderWhenEmpty="True"
                                    EmptyDataText="No Record(s) found" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-Font-Size="Large"
                                    CssClass="table table-bordered table-hover table-sm mb-0" GridLines="None" OnRowCommand="grdCircle_RowCommand">

                                    <Columns>

                                        <asp:TemplateField HeaderText="Sl. No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>

                                            <HeaderStyle Width="5%" HorizontalAlign="Center" BackColor="#1C6794" ForeColor="White" Font-Bold="True" />

                                            <ItemStyle HorizontalAlign="Center" />

                                            <FooterStyle BackColor="#1C6794" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Block">

                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkBlock" runat="server" CommandName="BlockClick" CommandArgument='<%# Eval("BlockCode") + "," + Eval("BlockName") %>' ForeColor="Blue" Font-Underline="false" ToolTip="Click here To View Thana-Wise Application Status">   <%# Eval("BlockName") %>  </asp:LinkButton>
                                            </ItemTemplate>

                                            <HeaderStyle Width="30%" HorizontalAlign="Center" BackColor="#1C6794" ForeColor="White" Font-Bold="True" />

                                            <ItemStyle HorizontalAlign="Left" />

                                            <FooterStyle BackColor="#1C6794" ForeColor="White" Font-Bold="True" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="कुल आवेदन">

                                            <ItemTemplate>

                                                <asp:LinkButton ID="lnkBlockTotal" runat="server" CommandArgument='<%# Eval("BlockCode") %>' OnClick="lnkBlockTotal_Click" ForeColor="Blue" Font-Underline="false"><%# Eval("Total") %> </asp:LinkButton>

                                                <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>' Visible="false"> </asp:Label>

                                            </ItemTemplate>

                                            <HeaderStyle Width="20%" HorizontalAlign="Center" BackColor="#1C6794" ForeColor="White" Font-Bold="True" />

                                            <ItemStyle HorizontalAlign="Center" />

                                            <FooterStyle BackColor="#1C6794" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="पूर्ण प्रविष्टि">

                                            <ItemTemplate>

                                                <asp:LinkButton ID="lnkBlockTotalFinalize" runat="server" CommandArgument='<%# Eval("BlockCode") %>' OnClick="lnkBlockTotalFinalize_Click" ForeColor="Blue" Font-Underline="false"> <%# Eval("Finalize") %> </asp:LinkButton>

                                                <asp:Label ID="lblTotalFinalize" runat="server" Text='<%# Eval("Finalize") %>' Visible="false"> </asp:Label>

                                            </ItemTemplate>

                                            <HeaderStyle Width="20%" HorizontalAlign="Center" BackColor="#1C6794" ForeColor="White" Font-Bold="True" />

                                            <ItemStyle HorizontalAlign="Center" />

                                            <FooterStyle BackColor="#1C6794" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="आंशिक प्रविष्टि">

                                            <ItemTemplate>

                                                <asp:LinkButton ID="lnkBlockTotalUnfinalize" runat="server" CommandArgument='<%# Eval("BlockCode") %>' OnClick="lnkBlockTotalUnfinalize_Click" ForeColor="Blue" Font-Underline="false"> <%# Eval("Unfinalize") %> </asp:LinkButton>

                                                <asp:Label ID="lblTotalUnfinalize" runat="server" Text='<%# Eval("Unfinalize") %>' Visible="false"> </asp:Label>

                                            </ItemTemplate>

                                            <HeaderStyle Width="20%" HorizontalAlign="Center" BackColor="#1C6794" ForeColor="White" Font-Bold="True" />

                                            <ItemStyle HorizontalAlign="Center" />

                                            <FooterStyle BackColor="#1C6794" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                    </Columns>

                                    <EmptyDataRowStyle Font-Size="Large" ForeColor="Red" />

                                    <HeaderStyle BackColor="#1C6794" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />

                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />

                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />

                                    <FooterStyle BackColor="#1C6794" ForeColor="White" Font-Bold="True" />

                                </asp:GridView>

                            </div>

                        </asp:Panel>

                        <asp:Panel ID="pnlthana" runat="server" ScrollBars="Auto">
                            <asp:Label ID="lblPrintDateforThana" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label><br />

                            <label class="form-label fw-bold">Click on the PoliceStation Name to view Panchayat-Wise Application Status <span class="text-danger">*</span></label>

                            <asp:GridView ID="grdThana" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="True" ShowHeaderWhenEmpty="True"
                                EmptyDataText="No Record(s) found" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-Font-Size="Large"
                                CssClass="table table-bordered table-hover table-sm mb-0" GridLines="None" OnRowCommand="grdThana_RowCommand">

                                <Columns>

                                    <asp:TemplateField HeaderText="Sl. No.">

                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>

                                        <HeaderStyle Width="5%" HorizontalAlign="Center" BackColor="#1C6794" ForeColor="White" Font-Bold="True" />

                                        <ItemStyle HorizontalAlign="Center" />

                                        <FooterStyle BackColor="#1C6794" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Police Station">

                                        <ItemTemplate>

                                            <asp:LinkButton ID="lnkPoliceStation" runat="server" CommandName="PoliceStationClick" CommandArgument='<%# Eval("PS_Code") + "," + Eval("Police_Station") %>' ForeColor="Blue" Font-Underline="false" ToolTip="Click here To View Panchayat-Wise Application Status"> <%# Eval("Police_Station") %> </asp:LinkButton>

                                        </ItemTemplate>

                                        <HeaderStyle Width="30%" HorizontalAlign="Center" BackColor="#1C6794" ForeColor="White" Font-Bold="True" />

                                        <ItemStyle HorizontalAlign="Left" />

                                        <FooterStyle BackColor="#1C6794" ForeColor="White" Font-Bold="True" HorizontalAlign="Left" />

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="कुल आवेदन">

                                        <ItemTemplate>

                                            <asp:LinkButton ID="lnkThanaTotal" runat="server" CommandArgument='<%# Eval("PS_Code") %>' OnClick="lnkThanaTotal_Click" ForeColor="Blue" Font-Underline="false"> <%# Eval("Total") %> </asp:LinkButton>


                                            <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>' Visible="false"> </asp:Label>

                                        </ItemTemplate>

                                        <HeaderStyle Width="20%" HorizontalAlign="Center" BackColor="#1C6794" ForeColor="White" Font-Bold="True" />

                                        <ItemStyle HorizontalAlign="Center" />

                                        <FooterStyle BackColor="#1C6794" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="पूर्ण प्रविष्टि">

                                        <ItemTemplate>

                                            <asp:LinkButton ID="lnkThanaTotalFinalize" runat="server" CommandArgument='<%# Eval("PS_Code") %>' OnClick="lnkThanaTotalFinalize_Click" ForeColor="Blue" Font-Underline="false"> <%# Eval("Finalize") %> </asp:LinkButton>

                                            <asp:Label ID="lblTotalFinalize" runat="server" Text='<%# Eval("Finalize") %>' Visible="false"> </asp:Label>

                                        </ItemTemplate>

                                        <HeaderStyle Width="20%" HorizontalAlign="Center" BackColor="#1C6794" ForeColor="White" Font-Bold="True" />

                                        <ItemStyle HorizontalAlign="Center" />

                                        <FooterStyle BackColor="#1C6794" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="आंशिक प्रविष्टि">

                                        <ItemTemplate>

                                            <asp:LinkButton ID="lnkThanaTotalUnfinalize" runat="server" CommandArgument='<%# Eval("PS_Code") %>' OnClick="lnkThanaTotalUnfinalize_Click" ForeColor="Blue" Font-Underline="false"> <%# Eval("Unfinalize") %> </asp:LinkButton>


                                            <asp:Label ID="lblTotalUnfinalize" runat="server" Text='<%# Eval("Unfinalize") %>' Visible="false"> </asp:Label>

                                        </ItemTemplate>

                                        <HeaderStyle Width="20%" HorizontalAlign="Center" BackColor="#1C6794" ForeColor="White" Font-Bold="True" />

                                        <ItemStyle HorizontalAlign="Center" />

                                        <FooterStyle BackColor="#1C6794" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />

                                    </asp:TemplateField>

                                </Columns>

                                <EmptyDataRowStyle Font-Size="Large" ForeColor="Red" />

                                <HeaderStyle BackColor="#1C6794" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />

                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />

                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />

                                <FooterStyle BackColor="#1C6794" ForeColor="White" Font-Bold="True" />

                            </asp:GridView>

                        </asp:Panel>

                        <asp:Panel ID="Panel_Panchayats" runat="server" ScrollBars="Auto">
                            <asp:Label ID="lblPrintDateforPanchayat" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label><br />

                            <label class="form-label fw-bold">Click on the Panchayat Name to view Village-Wise Application Status <span class="text-danger">*</span></label>

                            <asp:GridView ID="grdPanchayats" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="True" ShowHeaderWhenEmpty="True"
                                EmptyDataText="No Record(s) found" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-Font-Size="Large"
                                CssClass="table table-bordered table-hover table-sm mb-0" GridLines="None" OnRowCommand="grdPanchayats_RowCommand">

                                <Columns>

                                    <asp:TemplateField HeaderText="Sl. No.">

                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>

                                        <HeaderStyle Width="5%" HorizontalAlign="Center" BackColor="#1C6794" ForeColor="White" Font-Bold="True" />

                                        <ItemStyle HorizontalAlign="Center" />

                                        <FooterStyle BackColor="#1C6794" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Panchayat">

                                        <ItemTemplate>

                                            <asp:LinkButton ID="lnkPanchayat" runat="server" CommandName="PanchayatClick" CommandArgument='<%# Eval("PanchayatCode") + "," + Eval("PanchayatName") %>' ForeColor="Blue" Font-Underline="false" ToolTip="Click here To View Village-Wise Application Status">  <%# Eval("PanchayatName") %> </asp:LinkButton>

                                        </ItemTemplate>

                                        <HeaderStyle Width="30%" HorizontalAlign="Center" BackColor="#1C6794" ForeColor="White" Font-Bold="True" />

                                        <ItemStyle HorizontalAlign="Left" />

                                        <FooterStyle BackColor="#1C6794" ForeColor="White" Font-Bold="True" HorizontalAlign="Left" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="कुल आवेदन">

                                        <ItemTemplate>

                                            <asp:LinkButton ID="lnkPanchayaTotal" runat="server" CommandArgument='<%# Eval("PanchayatCode") %>' OnClick="lnkPanchayaTotal_Click" ForeColor="Blue" Font-Underline="false"> <%# Eval("Total") %> </asp:LinkButton>

                                            <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>' Visible="false">
                                            </asp:Label>

                                        </ItemTemplate>

                                        <HeaderStyle Width="20%" HorizontalAlign="Center" BackColor="#1C6794" ForeColor="White" Font-Bold="True" />

                                        <ItemStyle HorizontalAlign="Center" />

                                        <FooterStyle BackColor="#1C6794" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="पूर्ण प्रविष्टि">

                                        <ItemTemplate>

                                            <asp:LinkButton ID="lnkPanchayaTotalFinalize" runat="server" CommandArgument='<%# Eval("PanchayatCode") %>' OnClick="lnkPanchayaTotalFinalize_Click" ForeColor="Blue" Font-Underline="false"> <%# Eval("Finalize") %> </asp:LinkButton>

                                            <asp:Label ID="lblTotalFinalize" runat="server" Text='<%# Eval("Finalize") %>' Visible="false"> </asp:Label>

                                        </ItemTemplate>

                                        <HeaderStyle Width="20%" HorizontalAlign="Center" BackColor="#1C6794" ForeColor="White" Font-Bold="True" />

                                        <ItemStyle HorizontalAlign="Center" />

                                        <FooterStyle BackColor="#1C6794" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="आंशिक प्रविष्टि">

                                        <ItemTemplate>

                                            <asp:LinkButton ID="lnkPanchayaTotalUnfinalize" runat="server" CommandArgument='<%# Eval("PanchayatCode") %>' OnClick="lnkPanchayaTotalUnfinalize_Click" ForeColor="Blue" Font-Underline="false"> <%# Eval("Unfinalize") %> </asp:LinkButton>

                                            <asp:Label ID="lblTotalUnfinalize" runat="server" Text='<%# Eval("Unfinalize") %>' Visible="false"> </asp:Label>

                                        </ItemTemplate>

                                        <HeaderStyle Width="20%" HorizontalAlign="Center" BackColor="#1C6794" ForeColor="White" Font-Bold="True" />

                                        <ItemStyle HorizontalAlign="Center" />

                                        <FooterStyle BackColor="#1C6794" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />

                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Unfinalize" HeaderText="आंशिक प्रविष्टि">

                                        <HeaderStyle Width="20%" HorizontalAlign="Center" BackColor="#1C6794" ForeColor="White" Font-Bold="True" />

                                        <ItemStyle HorizontalAlign="Center" />

                                        <FooterStyle BackColor="#1C6794" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />

                                    </asp:BoundField>

                                </Columns>

                                <EmptyDataRowStyle Font-Size="Large" ForeColor="Red" />

                                <HeaderStyle BackColor="#1C6794" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />

                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />

                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />

                                <FooterStyle BackColor="#1C6794" ForeColor="White" Font-Bold="True" />

                            </asp:GridView>

                        </asp:Panel>

                        <asp:Panel ID="Pnlsearch" runat="server" Style="overflow-x: auto; overflow-y: hidden;" Visible="false">

                            <asp:GridView ID="GridView1" runat="server" DataKeyNames="a_id" AutoGenerateColumns="False" EnableTheming="False" Width="100%"
                                GridLines="None" ShowFooter="True" EmptyDataText="No Record Found" PagerStyle-CssClass="pgr" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound">

                                <Columns>


                                    <asp:TemplateField HeaderText="Sl. No.">

                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 + "." %>
                                        </ItemTemplate>

                                        <HeaderStyle Width="5%" />

                                        <ItemStyle Width="5%" HorizontalAlign="Left" VerticalAlign="Top" />

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Application No.">

                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkApplicationNo" runat="server" CommandArgument='<%# Eval("a_id") %>' Font-Underline="false" ForeColor="Blue" OnClick="lnkView_Click" OnClientClick="openwindow(this);" Text='<%# Eval("ApplicationNo") %>'> </asp:LinkButton>
                                        </ItemTemplate>

                                        <HeaderStyle Width="6%" />

                                        <ItemStyle Width="6%" HorizontalAlign="Left" VerticalAlign="Top" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="कमिश्नरी &lt;hr style='margin-bottom:0px;margin-top:0px;' /&gt; जिला &lt;hr style='margin-bottom:0px;margin-top:0px;' /&gt; सब डिवीज़न">

                                        <ItemTemplate>

                                            <%# Eval("DIVISIONAME") %>

                                            <hr style="margin: 0; border-color: #c1c1c1;" />

                                            <%# Eval("DISTRICTNAME") %>

                                            <hr style="margin: 0; border-color: #c1c1c1;" />

                                            <%# Eval("Sd_Name_En") %>
                                        </ItemTemplate>

                                        <HeaderStyle Width="10%" />

                                        <ItemStyle Width="10%" HorizontalAlign="Left" VerticalAlign="Top" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="अंचल &lt;hr style='margin-bottom:0px;margin-top:0px;' /&gt;थाना">

                                        <ItemTemplate>

                                            <%# Eval("BlockName") %>

                                            <hr style="margin: 0; border-color: #c1c1c1;" />

                                            <%# Eval("Police_Station") %>
                                        </ItemTemplate>

                                        <HeaderStyle Width="10%" />

                                        <ItemStyle Width="10%" HorizontalAlign="Left" VerticalAlign="Top" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="ग्राम पंचायत &lt;hr style='margin-bottom:0px;margin-top:0px;' /&gt;राजस्व ग्राम&lt;hr style='margin-bottom:0px;margin-top:0px;' /&gt;वार्ड">

                                        <ItemTemplate>

                                            <%# Eval("PanchayatName") %>

                                            <hr style="margin: 0; border-color: #c1c1c1;" />

                                            <%# Eval("VILLNAME") %>

                                            <hr style="margin: 0; border-color: #c1c1c1;" />

                                            <%# Eval("WARDNAME") %>
                                        </ItemTemplate>

                                        <HeaderStyle Width="15%" />

                                        <ItemStyle Width="15%" HorizontalAlign="Left" VerticalAlign="Top" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="वादी का नाम">

                                        <ItemTemplate>
                                            <%# Eval("vadi_Name") %>
                                            <br />
                                            <%# Eval("TotalVadi") %>
                                        </ItemTemplate>

                                        <HeaderStyle Width="5%" Wrap="False" />

                                        <ItemStyle Width="5%" HorizontalAlign="Left" VerticalAlign="Top" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="प्रतिवादी का नाम">

                                        <ItemTemplate>
                                            <%# Eval("pratiVadi_Name") %>
                                            <br />
                                            <%# Eval("TotalPratiVadi") %>
                                        </ItemTemplate>

                                        <HeaderStyle Width="5%" Wrap="False" />

                                        <ItemStyle Width="5%" HorizontalAlign="Left" VerticalAlign="Top" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="भूमि का प्रकार">

                                        <ItemTemplate>

                                            <%# Eval("Bhumitype") %>

                                            <hr style="margin: 0; border-color: #c1c1c1;" />

                                            <%# Eval("SarkariBhumiType") %>
                                        </ItemTemplate>

                                        <HeaderStyle Width="5%" Wrap="False" />

                                        <ItemStyle Width="5%" HorizontalAlign="Left" VerticalAlign="Top" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="भूमि विवाद का प्रकार">

                                        <ItemTemplate>
                                            <%# Eval("BhumiVivad") %>
                                        </ItemTemplate>

                                        <HeaderStyle Width="5%" Wrap="False" />

                                        <ItemStyle Width="5%" HorizontalAlign="Left" VerticalAlign="Top" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="भूमि विवाद की &lt;/br&gt; सवेदनशीलता">

                                        <ItemTemplate>
                                            <%# Eval("Bhumi_savedansheelta") %>
                                        </ItemTemplate>

                                        <HeaderStyle Width="5%" Wrap="False" />

                                        <ItemStyle Width="5%" HorizontalAlign="Left" VerticalAlign="Top" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="बैठक की तिथि">

                                        <ItemTemplate>
                                            <%# Eval("Meeting_date") %>
                                        </ItemTemplate>

                                        <HeaderStyle Width="5%" Wrap="False" />

                                        <ItemStyle Width="5%" HorizontalAlign="Left" VerticalAlign="Top" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="बैठक का निष्कर्ष">

                                        <ItemTemplate>
                                            <%# Eval("Description") %>
                                        </ItemTemplate>

                                        <HeaderStyle Width="5%" Wrap="False" />

                                        <ItemStyle Width="5%" HorizontalAlign="Left" VerticalAlign="Top" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="(Action)">

                                        <ItemTemplate>
                                            <div id="div_Action" runat="server" class="divclss"><%# Eval("disposal") %></div>
                                        </ItemTemplate>

                                        <HeaderStyle Width="5%" Wrap="False" />

                                        <ItemStyle Width="5%" HorizontalAlign="Left" VerticalAlign="Top" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="विवाद का अद्यतन कारक">

                                        <ItemTemplate>
                                            <%# Eval("bhumi_vivad_ka_adyatan_sthiti") %>
                                        </ItemTemplate>

                                        <HeaderStyle Width="5%" Wrap="False" />

                                        <ItemStyle Width="5%" HorizontalAlign="Left" VerticalAlign="Top" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="वादी द्वारा प्रस्तुत साक्ष्य">

                                        <ItemTemplate>

                                            <%# Eval("Vadi_Khatiyaan") %>
                                            <hr style="margin: 0; border-color: #c1c1c1;" />

                                            <%# Eval("Vadi_Kevaala") %>
                                            <hr style="margin: 0; border-color: #c1c1c1;" />

                                            <%# Eval("Vadi_CopyOfJamabandi") %>
                                            <hr style="margin: 0; border-color: #c1c1c1;" />

                                            <%# Eval("Vadi_LagaanRaseed") %>
                                            <hr style="margin: 0; border-color: #c1c1c1;" />

                                            <%# Eval("Vadi_Vanshaavalee") %>
                                            <hr style="margin: 0; border-color: #c1c1c1;" />

                                            <%# Eval("Vadi_Batavaara") %>
                                            <hr style="margin: 0; border-color: #c1c1c1;" />

                                            <%# Eval("Vadi_Parcha") %>
                                            <hr style="margin: 0; border-color: #c1c1c1;" />

                                            <%# Eval("vadi_nyayaalay_aadesh") %>
                                            <hr style="margin: 0; border-color: #c1c1c1;" />

                                            <%# Eval("Vadi_Anya_sakshya") %>
                                        </ItemTemplate>

                                        <HeaderStyle Width="5%" Wrap="False" />

                                        <ItemStyle Width="5%" HorizontalAlign="Left" VerticalAlign="Top" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="वादी का &lt;/br&gt;दस्तावेज">

                                        <ItemTemplate>

                                            <asp:ImageButton ID="Image6" runat="server" Height="50px" Width="50px" ImageUrl="~/images/pdf.gif" path='<%# Eval("Vadi_sakshya_File") %>' Style="cursor: pointer" Visible='<%# CheckNull(Eval("Vadi_sakshya_File")) %>' />

                                        </ItemTemplate>

                                        <HeaderStyle Width="5%" Wrap="False" />

                                        <ItemStyle Width="5%" HorizontalAlign="Left" VerticalAlign="Top" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="प्रतिवादी द्वारा प्रस्तुत साक्ष्य">

                                        <ItemTemplate>

                                            <%# Eval("prativadi_Khatiyaan") %>
                                            <hr style="margin: 0; border-color: #c1c1c1;" />

                                            <%# Eval("prativadi_Kevaala") %>
                                            <hr style="margin: 0; border-color: #c1c1c1;" />

                                            <%# Eval("prativadi_CopyOfJamabandi") %>
                                            <hr style="margin: 0; border-color: #c1c1c1;" />

                                            <%# Eval("prativadi_LagaanRaseed") %>
                                            <hr style="margin: 0; border-color: #c1c1c1;" />

                                            <%# Eval("prativadi_Vanshaavalee") %>
                                            <hr style="margin: 0; border-color: #c1c1c1;" />

                                            <%# Eval("prativadi_Batavaara") %>
                                            <hr style="margin: 0; border-color: #c1c1c1;" />

                                            <%# Eval("prativadi_Parcha") %>
                                            <hr style="margin: 0; border-color: #c1c1c1;" />

                                            <%# Eval("prativadi_nyayaalay_aadesh") %>
                                            <hr style="margin: 0; border-color: #c1c1c1;" />

                                            <%# Eval("pratiVadi_Anya_sakshya") %>
                                        </ItemTemplate>

                                        <HeaderStyle Width="10%" Wrap="False" />

                                        <ItemStyle Width="10%" HorizontalAlign="Left" VerticalAlign="Top" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="प्रतिवादी का &lt;/br&gt;दस्तावेज">

                                        <ItemTemplate>

                                            <asp:ImageButton ID="Image1" runat="server" Height="50px" Width="50px" ImageUrl="~/images/pdf.gif" path='<%# Eval("Prativadi_sakshya_File") %>' Style="cursor: pointer" Visible='<%# CheckNull(Eval("Prativadi_sakshya_File")) %>' />

                                        </ItemTemplate>

                                        <HeaderStyle Width="10%" Wrap="False" />

                                        <ItemStyle Width="10%" HorizontalAlign="Left" VerticalAlign="Top" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="पुलिस पदाधिकारी द्वारा समर्पित &lt;/br&gt;जाँच प्रतिवेदन की संक्षिप्त विवरणी">

                                        <ItemTemplate>

                                            <div id="div_pulis_padadhikari_vivarani" runat="server" class="divclss" visible='<%# CheckNull(Eval("pulis_padadhikari_vivarani")) %>'><%# Eval("pulis_padadhikari_vivarani") %> </div>

                                        </ItemTemplate>

                                        <HeaderStyle Width="20%" Wrap="False" />

                                        <ItemStyle Width="20%" HorizontalAlign="Left" VerticalAlign="Top" />

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="दस्तावेज">

                                        <ItemTemplate>

                                            <asp:ImageButton ID="Image2" runat="server" Height="50px" Width="50px" ImageUrl="~/images/pdf.gif" path='<%# Eval("pulis_padadhikar_Patr_file") %>' Style="cursor: pointer" Visible='<%# CheckNull(Eval("pulis_padadhikar_Patr_file")) %>' />

                                        </ItemTemplate>

                                        <HeaderStyle Width="5%" Wrap="False" />

                                        <ItemStyle Width="5%" HorizontalAlign="Left" VerticalAlign="Top" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="हल्का कर्मचारी / अंचल निरीक्षक द्वारा समर्पित &lt;/br&gt;जाँच प्रतिवेदन की संक्षिप्त विवरणी">

                                        <ItemTemplate>

                                            <div id="div_HalkaKarmchari_vivran" runat="server" class="divclss" visible='<%# CheckNull(Eval("HalkaKarmchari_vivran")) %>'><%# Eval("HalkaKarmchari_vivran") %> </div>

                                        </ItemTemplate>

                                        <HeaderStyle Width="20%" Wrap="False" />

                                        <ItemStyle Width="20%" HorizontalAlign="Left" VerticalAlign="Top" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="दस्तावेज">

                                        <ItemTemplate>

                                            <asp:ImageButton ID="Image3" runat="server" Height="50px" Width="50px" ImageUrl="~/images/pdf.gif" path='<%# Eval("HalkaKarmchari_Patr_file") %>' Style="cursor: pointer" Visible='<%# CheckNull(Eval("HalkaKarmchari_Patr_file")) %>' />

                                        </ItemTemplate>

                                        <HeaderStyle Width="5%" Wrap="False" />

                                        <ItemStyle Width="5%" HorizontalAlign="Left" VerticalAlign="Top" />

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="विवादित भू-खंड मापी का विवरणी">

                                        <ItemTemplate>

                                            <%# Eval("vivadit_bhukhand_Mapi_ki_avashyakta_hai") %>

                                            <hr style="margin: 0; border-color: #c1c1c1;" />

                                            <%# Eval("vivadit_bhukhand_Mapi") %>

                                            <hr style="margin: 0; border-color: #c1c1c1;" />

                                            <asp:Label ID="Label1" runat="server" Text="माप के लिए निर्धारित तिथि : " Visible='<%# CheckNull(Eval("maapee_ke_lie_nirdhaarit_tithi")) %>' />

                                            <%# Eval("maapee_ke_lie_nirdhaarit_tithi", "{0:dd/MM/yyyy}") %>

                                            <hr style="margin: 0;" />

                                            <asp:Label ID="Label2" runat="server" Text="मापी नहीं होने का कारण :" Visible='<%# CheckNull(Eval("vivaadit_bhukhand_Mapi_Reason")) %>' />

                                            <div id="div_vivaadit_bhukhand_Mapi_Reason" runat="server" class="divclss" visible='<%# CheckNull(Eval("vivaadit_bhukhand_Mapi_Reason")) %>'><%# Eval("vivaadit_bhukhand_Mapi_Reason") %> </div>

                                        </ItemTemplate>

                                        <HeaderStyle Width="5%" Wrap="False" />

                                        <ItemStyle Width="5%" HorizontalAlign="Left" VerticalAlign="Top" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="मापी का दस्तावेज">

                                        <ItemTemplate>

                                            <asp:ImageButton ID="Image4" runat="server" Height="50px" Width="50px" ImageUrl="~/images/pdf.gif" path='<%# Eval("vivaadit_bhukhand_Mapi_File") %>' Style="cursor: pointer" Visible='<%# CheckNull(Eval("vivaadit_bhukhand_Mapi_File")) %>' />

                                        </ItemTemplate>

                                        <HeaderStyle Width="5%" Wrap="False" />

                                        <ItemStyle Width="5%" HorizontalAlign="Left" VerticalAlign="Top" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="विवाद का &lt;/br&gt;प्राथमिकी/अप्राथमिकी">

                                        <ItemTemplate>
                                            <%# Eval("bhumi_vivad_Vivran_Available") %>
                                        </ItemTemplate>

                                        <HeaderStyle Width="5%" Wrap="False" />

                                        <ItemStyle Width="5%" HorizontalAlign="Left" VerticalAlign="Top" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="न्यायालय में &lt;/br&gt;प्रक्रियाधीननन वाद">

                                        <ItemTemplate>
                                            <%# Eval("dispute_in_court_available") %>
                                        </ItemTemplate>

                                        <HeaderStyle Width="5%" Wrap="False" />

                                        <ItemStyle Width="5%" HorizontalAlign="Left" VerticalAlign="Top" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="आवेदन">

                                        <ItemTemplate>

                                            <asp:ImageButton ID="Image5" runat="server" Height="50px" Width="50px" ImageUrl="~/images/pdf.gif" path='<%# Eval("ApplicationFile") %>' Style="cursor: pointer" Visible='<%# CheckNull(Eval("ApplicationFile")) %>' />

                                        </ItemTemplate>

                                        <HeaderStyle Width="5%" Wrap="False" />

                                        <ItemStyle Width="5%" HorizontalAlign="Left" VerticalAlign="Top" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="" Visible="false">

                                        <ItemTemplate>

                                            <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%# Eval("a_id") %>' CssClass="btn btn-success" Font-Underline="false" ForeColor="Blue" OnClick="lnkView_Click" Text="View" ToolTip="Click Edit">  </asp:LinkButton>

                                        </ItemTemplate>

                                    </asp:TemplateField>

                                </Columns>

                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" CssClass="alt" />

                                <HeaderStyle BackColor="Beige" ForeColor="#333333" Font-Bold="True" />

                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />

                                <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />

                                <PagerStyle CssClass="pgr" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />

                            </asp:GridView>


                        </asp:Panel>


                    </div>
                </div>

            </div>
        </div>
    </div>

</asp:Content>
