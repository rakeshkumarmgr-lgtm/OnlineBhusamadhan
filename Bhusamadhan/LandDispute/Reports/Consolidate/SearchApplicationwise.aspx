<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchApplicationwise.aspx.cs" Inherits="Bhusamadhan.LandDispute.Reports.Consolidate.SearchApplicationwise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .pager-link {
            display: inline-block;
            padding: 5px;
            margin: 2px;
            border-radius: 5px;
            color: black;
            text-decoration: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

    <div class="container-fluid">
        <div class="card">
            <div class="card-body">

                <div class="row mb-3">
                    <div class="col-12 text-center">
                        <h4 class="text-dark font-weight-bold mb-0">Application Wise Consolidated Report
                        </h4>
                    </div>
                </div>

                <div class="row mb-2">
                    <div class="col-md-8 offset-md-2 text-center">
                        <asp:Label ID="lbltext" runat="server" Visible="false" CssClass="text-dark font-weight-bold"> </asp:Label>

                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="True"> </asp:Label>
                    </div>
                </div>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">

                    <ContentTemplate>

                        <div class="form-row">

                            <!-- Range -->
                            <div class="form-group col-md-2" runat="server" id="divddlRange">

                                <label runat="server" id="divLabRange" class="font-weight-bold">Range </label>

                                <asp:DropDownList ID="ddlRange" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>

                            </div>

                            <div class="form-group col-md-2" runat="server" id="divddlCommissionary">

                                <label runat="server" id="divLabCommissionary" class="font-weight-bold">Commissionary </label>

                                <asp:DropDownList ID="ddlCommissionary" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>

                            </div>

                            <div class="form-group col-md-2">

                                <label class="font-weight-bold">District</label>

                                <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>

                            </div>


                            <div class="form-group col-md-2">

                                <label class="font-weight-bold">Sub-Division </label>

                                <asp:DropDownList ID="ddlSubDivision" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>

                            </div>


                            <div class="form-group col-md-2">

                                <label class="font-weight-bold">Circle </label>

                                <asp:DropDownList ID="ddlBlock" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>

                            </div>


                            <div class="form-group col-md-2">

                                <label class="font-weight-bold">Thana </label>

                                <asp:DropDownList ID="ddlThana" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>

                            </div>

                        </div>

                        <div class="form-row">
                            <div class="form-group col-md-2">
                                <label class="font-weight-bold">Page Size </label>

                                <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="form-control mb-2"
                                    Enabled="true">
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="70">70</asp:ListItem>
                                    <asp:ListItem Value="130">130</asp:ListItem>
                                </asp:DropDownList>


                            </div>



                            <div class="form-group col-md-2">
                                <label class="font-weight-bold">Search By </label>
                                <asp:DropDownList ID="ddlSearchby" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchby_SelectedIndexChanged">

                                    <asp:ListItem Value="0">-- All --</asp:ListItem>
                                    <asp:ListItem Value="1">Application No.</asp:ListItem>
                                    <asp:ListItem Value="2">Vadi Name</asp:ListItem>
                                    <asp:ListItem Value="3">Prativadi Name</asp:ListItem>
                                    <asp:ListItem Value="4">Vadi Mobile Number</asp:ListItem>
                                    <asp:ListItem Value="5">Prativadi Mobile Number</asp:ListItem>

                                </asp:DropDownList>
                            </div>


                            <div id="divlblsearchType" runat="server" style="display: none;"></div>

                            <div class="form-group col-md-3" id="divtxtsearchType" runat="server" visible="false">

                                <label class="font-weight-bold">
                                    <asp:Label ID="lblsearchType" runat="server" CssClass="text-primary font-weight-bold"> </asp:Label>
                                </label>

                                <asp:TextBox ID="txtsearch" runat="server" CssClass="form-control" placeholder="Enter search value"> </asp:TextBox>

                            </div>

                            <div class="form-group col-md-2">
                                <label class="font-weight-bold">From Date </label>

                                <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control" placeholder="dd-mm-yyyy" ReadOnly="true"> </asp:TextBox>

                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfrmdate" Format="dd-MM-yyyy"></cc1:CalendarExtender>

                                <asp:RequiredFieldValidator ID="RFVFromDate" runat="server" ControlToValidate="txtfrmdate" ValidationGroup="a" ForeColor="Red" Display="Dynamic" ErrorMessage="From Date is required"> </asp:RequiredFieldValidator>
                            </div>


                            <div class="form-group col-md-2">
                                <label class="font-weight-bold">To Date </label>

                                <asp:TextBox ID="txtTodate" runat="server" CssClass="form-control" placeholder="dd-mm-yyyy" ReadOnly="true"> </asp:TextBox>

                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTodate" Format="dd-MM-yyyy"></cc1:CalendarExtender>

                                <asp:RequiredFieldValidator ID="RFVToDate" runat="server" ControlToValidate="txtTodate" ValidationGroup="a" ForeColor="Red" Display="Dynamic" ErrorMessage="To Date is required"> </asp:RequiredFieldValidator>
                            </div>


                        </div>

                    </ContentTemplate>

                </asp:UpdatePanel>

                <div class="form-row mt-2">

                    <div class="form-group col-md-2">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary btn-block" ValidationGroup="a" OnClick="btnSearch_Click" />
                    </div>

                    <div class="form-group col-md-2">
                        <asp:Button ID="btnExport" runat="server" Text="Export To Excel" CssClass="btn btn-success btn-block" ValidationGroup="a" OnClick="btnExport_Click" />
                    </div>

                </div>

                <div class="row mt-2">

                    <asp:GridView ID="GridView1" runat="server" DataKeyNames="a_id" AutoGenerateColumns="False" EnableTheming="False" Width="100%" CssClass="mGrid"
                        GridLines="None" ShowFooter="True" EmptyDataText="No Record Found" CellPadding="4" ForeColor="#333333">

                        <AlternatingRowStyle CssClass="alt" BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%#Eval("slno")%>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Application No." ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="6%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkApplicationNo" runat="server" CommandArgument='<%#Eval("a_id")%>' Font-Underline="false" ForeColor="Blue" OnClick="lnkView_Click" OnClientClick="openwindow(this);" Text='<%#Eval("ApplicationNo")%>'></asp:LinkButton>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="कमिश्नरी &lt;hr style='margin-bottom: 0px; margin-top: 0px;' /&gt; जिला &lt;hr style='margin-bottom: 0px; margin-top: 0px;' /&gt; सब डिवीज़न" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <%#Eval("DIVISIONAME")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                    <%#Eval("DISTRICTNAME")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                    <%#Eval("Sd_Name_En")%>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="अंचल &lt;hr style='margin-bottom: 0px; margin-top: 0px;' /&gt;थाना " ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <%#Eval("BlockName")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                    <%#Eval("Police_Station")%>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ग्राम पंचायत &lt;hr style='margin-bottom: 0px; margin-top: 0px;' /&gt;राजस्व ग्राम&lt;hr style='margin-bottom: 0px; margin-top: 0px;' /&gt;वार्ड" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <%#Eval("PanchayatName")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                    <%#Eval("VILLNAME")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                    <%#Eval("WARDNAME")%>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="वादी का नाम " ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%#Eval("vadi_Name")%>
                                    <br />
                                    <%#Eval("TotalVadi")%>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="प्रतिवादी का नाम" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%#Eval("pratiVadi_Name")%>
                                    <br />
                                    <%#Eval("TotalPratiVadi")%>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="भूमि का प्रकार" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%#Eval("Bhumitype")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                    <%#Eval("SarkariBhumiType")%>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="भूमि विवाद का प्रकार" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%#Eval("BhumiVivad")%>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="भूमि विवाद की &lt;/br&gt; सवेदनशीलता" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%#Eval("Bhumi_savedansheelta")%>
                                </ItemTemplate>


                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="बैठक की तिथि" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%#Eval("Meeting_date")%>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="बैठक का निष्कर्ष" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%#Eval("Description")%>
                                </ItemTemplate>


                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="(Action)" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <div id="div_Action" runat="server" class="divclss">
                                        <%#Eval("disposal")%>
                                    </div>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="विवाद का अद्यतन कारक" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%#Eval("bhumi_vivad_ka_adyatan_sthiti")%>
                                </ItemTemplate>


                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="वादी द्वारा प्रस्तुत साक्ष्य" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%#Eval("Vadi_Khatiyaan")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                    <%#Eval("Vadi_Kevaala")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                    <%#Eval("Vadi_CopyOfJamabandi")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                    <%#Eval("Vadi_LagaanRaseed")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                    <%#Eval("Vadi_Vanshaavalee")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                    <%#Eval("Vadi_Batavaara")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                    <%#Eval("Vadi_Parcha")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                    <%#Eval("vadi_nyayaalay_aadesh")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                    <%#Eval("Vadi_Anya_sakshya")%>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="वादी का &lt;/br&gt;दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:ImageButton ID="Image6" runat="server" Height="50px" ImageUrl="~/images/pdf.gif" path='<%#Eval("Vadi_sakshya_File")%>' Style="cursor: pointer" Visible='<%# CheckNull(Eval("Vadi_sakshya_File"))%>' Width="50px" />
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="प्रतिवादी द्वारा प्रस्तुत साक्ष्य" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <%#Eval("prativadi_Khatiyaan")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                    <%#Eval("prativadi_Kevaala")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                    <%#Eval("prativadi_CopyOfJamabandi")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                    <%#Eval("prativadi_LagaanRaseed")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                    <%#Eval("prativadi_Vanshaavalee")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                    <%#Eval("prativadi_Batavaara")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                    <%#Eval("prativadi_Parcha")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                    <%#Eval("prativadi_nyayaalay_aadesh")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                    <%#Eval("pratiVadi_Anya_sakshya")%>
                                </ItemTemplate>
                                <HeaderStyle Width="10%" Wrap="False" />

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="प्रतिवादी का &lt;/br&gt;दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:ImageButton ID="Image1" runat="server" Height="50px" ImageUrl="~/images/pdf.gif" path='<%# Eval("Prativadi_sakshya_File")%>' Style="cursor: pointer" Visible='<%# CheckNull(Eval("Prativadi_sakshya_File"))%>' Width="50px" />
                                </ItemTemplate>
                                <HeaderStyle Width="10%" Wrap="False" />

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="पुलिस पदाधिकारी द्वारा समर्पित &lt;/br&gt;जाँच प्रतिवेदन की संक्षिप्त विवरणी" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <div id="div_pulis_padadhikari_vivarani" runat="server" class="divclss" visible='<%# CheckNull(Eval("pulis_padadhikari_vivarani"))%>'>
                                        <%#Eval("pulis_padadhikari_vivarani")%>
                                    </div>
                                </ItemTemplate>


                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:ImageButton ID="Image2" runat="server" Height="50px" ImageUrl="~/images/pdf.gif" path='<%# Eval("pulis_padadhikar_Patr_file")%>' Style="cursor: pointer" Visible='<%# CheckNull(Eval("pulis_padadhikar_Patr_file"))%>' Width="50px" />
                                </ItemTemplate>


                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="हल्का कर्मचारी / अंचल निरीक्षक द्वारा समर्पित &lt;/br&gt;जाँच प्रतिवेदन की संक्षिप्त विवरणी" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <div id="div_HalkaKarmchari_vivran" runat="server" class="divclss" visible='<%# CheckNull(Eval("HalkaKarmchari_vivran"))%>'>
                                        <%#Eval("HalkaKarmchari_vivran")%>
                                    </div>
                                </ItemTemplate>


                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:ImageButton ID="Image3" runat="server" Height="50px" ImageUrl="~/images/pdf.gif" path='<%#Eval("HalkaKarmchari_Patr_file")%>' Style="cursor: pointer" Visible='<%# CheckNull(Eval("HalkaKarmchari_Patr_file"))%>' Width="50px" />
                                </ItemTemplate>


                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="विवादित भू-खंड मापी का विवरणी" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%#Eval("vivadit_bhukhand_Mapi_ki_avashyakta_hai")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                    <%#Eval("vivadit_bhukhand_Mapi")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                    <asp:Label ID="Label1" runat="server" Text="माप के लिए निर्धारित तिथि : " Visible='<%# CheckNull(Eval("maapee_ke_lie_nirdhaarit_tithi"))%>'></asp:Label>
                                    <%#Eval("maapee_ke_lie_nirdhaarit_tithi","{0:dd/MM/yyyy}")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px;' />
                                    <asp:Label ID="Label2" runat="server" Text="मापी नहीं होने का कारण :" Visible='<%# CheckNull(Eval("vivaadit_bhukhand_Mapi_Reason"))%>'></asp:Label>
                                    <div id="div_vivaadit_bhukhand_Mapi_Reason" runat="server" class="divclss" visible='<%# CheckNull(Eval("vivaadit_bhukhand_Mapi_Reason"))%>'>
                                        <%#Eval("vivaadit_bhukhand_Mapi_Reason")%>
                                    </div>
                                </ItemTemplate>


                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="मापी का दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:ImageButton ID="Image4" runat="server" Height="50px" ImageUrl="~/images/pdf.gif" path='<%#Eval("vivaadit_bhukhand_Mapi_File")%>' Style="cursor: pointer" Visible='<%# CheckNull(Eval("vivaadit_bhukhand_Mapi_File"))%>' Width="50px" />
                                </ItemTemplate>


                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="विवाद का &lt;/br&gt;प्राथमिकी/अप्राथमिकी" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%#Eval("bhumi_vivad_Vivran_Available")%>
                                </ItemTemplate>


                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="न्यायालय में &lt;/br&gt;प्रक्रियाधीननन वाद" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%#Eval("dispute_in_court_available")%>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="आवेदन" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:ImageButton ID="Image5" runat="server" Height="50px" ImageUrl="~/images/pdf.gif" path='<%#Eval("ApplicationFile")%>' Style="cursor: pointer" Visible='<%# CheckNull(Eval("ApplicationFile"))%>' Width="50px" />
                                </ItemTemplate>


                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" Visible="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%#Eval("a_id")%>' CssClass="btn btn-success" Font-Underline="false" ForeColor="Blue" Text="View" ToolTip="Click Edit"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle BackColor="#5bc0de" ForeColor="Black" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" CssClass="pgr" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <EditRowStyle BackColor="#999999" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    </asp:GridView>
                </div>

                <div class="row mb-2">
                    <div class="col-md-12 text-center py-2">
                        <asp:Repeater ID="rptPager" runat="server">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkPage" runat="server" Text='<%# Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>' OnClick="Page_Changed" CssClass="pager-link">  </asp:LinkButton>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>

            </div>
        </div>
    </div>


</asp:Content>
