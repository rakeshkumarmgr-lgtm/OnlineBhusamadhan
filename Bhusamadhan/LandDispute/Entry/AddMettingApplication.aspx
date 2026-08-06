<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddMettingApplication.aspx.cs" Inherits="Bhusamadhan.LandDispute.Entry.AddMettingApplication" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div class="container-fluid">
        <div class="card shadow-sm mb-3">

            <div class="card-header bg-primary text-white">
                <h5 class="mb-0">भूमि विवाद का विवरण</h5>
            </div>

            <div class="card-body">


                <div class="row mb-4">

                    <div class="col-md-6">
                        <span class="fw-bold fs-5">आवेदन संख्या :</span>
                        <asp:Label ID="lblApplicationNo" runat="server" CssClass="fw-bold text-primary ms-2"></asp:Label>
                    </div>

                    <div class="col-md-6 text-md-end">
                        <span class="fw-bold fs-5">आवेदन की तिथि :</span>
                        <asp:Label ID="lblAppDate" runat="server" CssClass="fw-bold text-primary ms-2"></asp:Label>
                    </div>

                </div>


                <div class="row border-bottom py-2">

                    <div class="col-md-3 fw-bold">जिला  </div>

                    <div class="col-md-3">
                        <asp:Label ID="lblDistrict" runat="server"></asp:Label>
                    </div>

                    <div class="col-md-3 fw-bold">अनुमंडल  </div>

                    <div class="col-md-3">
                        <asp:Label ID="lblSubdivision" runat="server"></asp:Label>
                    </div>

                </div>


                <div class="row border-bottom py-2">

                    <div class="col-md-3 fw-bold">अंचल </div>

                    <div class="col-md-3">
                        <asp:Label ID="lblBlock" runat="server"></asp:Label>
                    </div>

                    <div class="col-md-3 fw-bold">थाना </div>

                    <div class="col-md-3">
                        <asp:Label ID="lblPolice_Station" runat="server"></asp:Label>
                    </div>

                </div>


                <div class="row border-bottom py-2">

                    <div class="col-md-3 fw-bold">क्षेत्र का प्रकार </div>

                    <div class="col-md-3">
                        <asp:Label ID="lblAreaType" runat="server"></asp:Label>
                    </div>

                    <div class="col-md-3 fw-bold">
                        <asp:Label ID="lblVillage" runat="server"></asp:Label>
                    </div>

                    <div class="col-md-3">
                        <asp:Label ID="lblPanchayatName" runat="server"></asp:Label>
                    </div>

                </div>


                <div class="row border-bottom py-2">

                    <div class="col-md-3 fw-bold" id="div_Vadi_Svarajaya_Label" runat="server">राजस्व ग्राम </div>

                    <div class="col-md-3" id="div_Vadi_Svarajaya" runat="server">
                        <asp:Label ID="lblVILLNAME" runat="server"></asp:Label>
                    </div>

                    <div class="col-md-3 fw-bold">वार्ड  </div>

                    <div class="col-md-3" id="div_Vadi_Ward" runat="server">
                        <asp:Label ID="lblWARDNAME" runat="server"></asp:Label>
                    </div>
                </div>


                <div class="row border-bottom py-2">

                    <div class="col-md-3 fw-bold">
                        विवाद का अद्यतन कारक
                    </div>

                    <div class="col-md-3" id="div_vadi_Vivad_Ka_Vighatan" runat="server">
                        <asp:Label ID="lblvadi_Vivad_Ka_Vighatan" runat="server"></asp:Label>
                    </div>

                    <div class="col-md-3 fw-bold">
                        राजस्व थाना संख्या
                    </div>

                    <div class="col-md-3" id="div_vadi_rajashv_sankhaya" runat="server">
                        <asp:Label ID="lblvadi_rajashv_sankhaya" runat="server"></asp:Label>
                    </div>

                </div>


                <div class="row border-bottom py-2">

                    <div class="col-md-3 fw-bold">
                        भूमि का प्रकार
                    </div>

                    <div class="col-md-3" id="div_Vadi_BhumiKaPrakar" runat="server">
                        <asp:Label ID="lblVadi_BhumiKaPrakar" runat="server"></asp:Label>
                    </div>

                    <div class="col-md-3 fw-bold" id="div_vadi_sarkari_bhumi_ka_prakar_Label" runat="server">
                        सरकारी भूमि का प्रकार
                    </div>

                    <div class="col-md-3" id="div_Preview_vadi_sarkari_bhumi_ka_prakar" runat="server">
                        <asp:Label ID="lblvadi_sarkari_bhumi_ka_prakar_Label" runat="server"></asp:Label>
                    </div>

                </div>


                <div class="row border-bottom py-2">

                    <div class="col-md-3 fw-bold"
                        id="div_vadi_Sarkari_bhumi_ka_Prakar_ager_anya_Label"
                        runat="server">
                        सरकारी भूमि का प्रकार (अगर अन्य है)

                    </div>

                    <div class="col-md-3"
                        id="div_vadi_Sarkari_bhumi_ka_Prakar_ager_anya"
                        runat="server">

                        <asp:Label ID="lblvadi_Sarkari_bhumi_ka_Prakar_ager_anya" runat="server"></asp:Label>

                    </div>

                    <div class="col-md-3 fw-bold">
                        भूमि विवाद का प्रकार
                    </div>

                    <div class="col-md-3" id="div_BhumiKa_VivadPrakar" runat="server">

                        <asp:Label ID="lblBhumiKa_VivadPrakar" runat="server"></asp:Label>

                    </div>

                </div>


                <div class="row border-bottom py-2">

                    <div class="col-md-3 fw-bold" id="div_Preview_vadi_Bhumivivad_Prakar_Anaya_Label" runat="server">
                        भूमि विवाद का प्रकार (अगर अन्य है)

                    </div>

                    <div class="col-md-9" id="div_Preview_vadi_Bhumivivad_Prakar_Anaya" runat="server">

                        <asp:Label ID="lblvadi_Bhumivivad_Prakar_Anaya" runat="server"></asp:Label>

                    </div>

                </div>

                <!-- Applicant Description -->
                <div class="row border-bottom py-2">

                    <div class="col-md-3 fw-bold">
                        वादी द्वारा भूमि विवाद का संक्षिप्त विवरणी
                    </div>

                    <div class="col-md-9" id="divVadiKabhumiVivaran" runat="server">

                        <asp:Label ID="lblVadiKabhumiVivaran" runat="server"></asp:Label>

                    </div>

                </div>


                <div class="row border-bottom py-2">

                    <div class="col-md-3 fw-bold">
                        प्रतिवादी द्वारा भूमि विवाद का संक्षिप्त विवरणी
                    </div>

                    <div class="col-md-9" id="divPrativadiKabhumiVivaran" runat="server">

                        <asp:Label ID="lblPrativadiKabhumiVivaran" runat="server"></asp:Label>

                    </div>

                </div>
                <div class="row pt-3">

                    <div class="col-md-3 fw-bold">
                        वादी द्वारा प्रस्तुत आवेदन
                    </div>

                    <div class="col-md-3">

                        <asp:ImageButton ID="lnkAppDoc" runat="server" ImageUrl="~/images/pdf.gif" CssClass="img-fluid" Width="50" Height="50" path="display" />

                    </div>

                    <div class="col-md-3 fw-bold">
                        प्रतिवादी द्वारा प्रस्तुत आवेदन
                    </div>

                    <div class="col-md-3">

                        <asp:ImageButton ID="lnkPrativadiDoc" runat="server" ImageUrl="~/images/pdf.gif" CssClass="img-fluid" Width="50" Height="50" path="display" />

                    </div>

                </div>

            </div>

        </div>

        <div class="card shadow-sm mb-3">

            <div class="card-header bg-primary text-white">
                <h5 class="mb-0">वादी का विवरण</h5>
            </div>

            <div class="card-body p-0">

                <div class="table-responsive">

                    <asp:GridView ID="gvWadi" runat="server" Width="100%" AutoGenerateColumns="False" EmptyDataText="No Record Found!" CssClass="table table-bordered table-hover table-striped mb-0">

                        <HeaderStyle CssClass="table-primary text-center align-middle" />
                        <RowStyle CssClass="align-middle" />
                        <EmptyDataRowStyle CssClass="text-center text-danger fw-bold p-3" />

                        <Columns>


                            <asp:TemplateField HeaderText="Sl. No.">
                                <HeaderStyle CssClass="text-center" Width="5%" />
                                <ItemStyle CssClass="text-center" />
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:BoundField DataField="NameAsPerAadhaar" HeaderText="वादी का नाम" />


                            <asp:BoundField DataField="Vadi_Father_Husband_Name" HeaderText="पिता / पति का नाम" />

                           
                            <asp:TemplateField HeaderText="लिंग">
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle CssClass="text-center" />
                                <ItemTemplate>

                                    <asp:Label ID="lblGender" runat="server" Text='<%# Convert.ToString(Eval("SexAsPerAadhaar")) == "F" ? "Female" : "Male" %>'>  </asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:BoundField DataField="YearOfBirthAsPerAadhaar" HeaderText="उम्र (वर्ष)" />


                            <asp:BoundField DataField="dist" HeaderText="जिला" />


                            <asp:BoundField DataField="sub_division" HeaderText="अनुमंडल" />


                            <asp:BoundField DataField="block" HeaderText="अंचल" />


                            <asp:BoundField DataField="thana" HeaderText="थाना" />


                            <asp:BoundField DataField="area_type" HeaderText="क्षेत्र का प्रकार" />


                            <asp:BoundField DataField="panchayt" HeaderText="ग्राम पंचायत" />


                            <asp:BoundField DataField="village" HeaderText="राजस्व ग्राम" />


                            <asp:BoundField DataField="WardNo" HeaderText="वार्ड" />


                            <asp:BoundField DataField="Vadi_MobileNo" HeaderText="मोबाइल संख्या" />


                            <asp:TemplateField HeaderText="विभाग का प्रतिनिधि">

                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle CssClass="text-center" />

                                <ItemTemplate>

                                    <asp:Label ID="lblDeppratinidhi" runat="server" Text='<%# Convert.ToString(Eval("is_vadi_from_an_dept")) == "Y" ? "हाँ" : "नहीं" %>'>
                                    </asp:Label>

                                </ItemTemplate>

                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="संस्था का प्रतिनिधि">

                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle CssClass="text-center" />

                                <ItemTemplate>

                                    <asp:Label ID="lblOrgpratinidhi" runat="server" Text='<%# Convert.ToString(Eval("is_vadi_from_an_org")) == "Y" ? "हाँ" : "नहीं" %>'> </asp:Label>

                                </ItemTemplate>

                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="विभाग / संस्था का नाम">

                                <ItemTemplate>

                                    <asp:Label ID="lblOrgName" runat="server" Text='<%# Convert.ToString(Eval("is_vadi_from_an_org")) == "Y" ? Eval("vadi_org_name") : Eval("org_type") %>'> </asp:Label>

                                </ItemTemplate>

                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="विभाग / संस्था में पदनाम">

                                <ItemTemplate>

                                    <asp:Label ID="lblPadName" runat="server" Text='<%# Convert.ToString(Eval("is_vadi_from_an_org")) == "Y" ? Eval("vadi_org_pad_name") : Eval("vadi_dept_pad_name") %>'> </asp:Label>

                                </ItemTemplate>

                            </asp:TemplateField>

                        </Columns>

                    </asp:GridView>

                </div>

            </div>

        </div>

        <div class="card shadow-sm mb-3">

            <div class="card-header bg-primary text-white">
                <h5 class="mb-0">प्रतिवादी का विवरण</h5>
            </div>

            <div class="card-body p-0">

                <div class="table-responsive" id="divPratiwadi" runat="server">

                    <asp:GridView ID="pratiWadi_grid" runat="server" Width="100%" AutoGenerateColumns="False" EmptyDataText="No Record Found!" CssClass="table table-bordered table-striped table-hover mb-0">

                        <HeaderStyle CssClass="table-primary text-center align-middle" />
                        <RowStyle CssClass="align-middle" />
                        <EmptyDataRowStyle CssClass="text-center text-danger fw-bold p-3" />

                        <Columns>


                            <asp:TemplateField HeaderText="Sl. No.">
                                <HeaderStyle CssClass="text-center" Width="5%" />
                                <ItemStyle CssClass="text-center" />
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:BoundField DataField="pratiVadi_Name" HeaderText="प्रतिवादी का नाम" />


                            <asp:BoundField DataField="pratiVadi_Father_Husband_Name" HeaderText="पिता / पति का नाम" />


                            <asp:BoundField DataField="dist" HeaderText="जिला" />


                            <asp:BoundField DataField="sub_division" HeaderText="अनुमंडल" />


                            <asp:BoundField DataField="block" HeaderText="अंचल" />


                            <asp:BoundField DataField="thana" HeaderText="थाना" />


                            <asp:BoundField DataField="area_type" HeaderText="क्षेत्र का प्रकार" />


                            <asp:BoundField DataField="panchayt" HeaderText="ग्राम पंचायत" />


                            <asp:BoundField DataField="village" HeaderText="राजस्व ग्राम" />


                            <asp:BoundField DataField="WardNo" HeaderText="वार्ड" />


                            <asp:BoundField DataField="pratiVadi_MobileNo" HeaderText="मोबाइल संख्या" />


                            <asp:TemplateField HeaderText="संस्था का प्रतिनिधि">

                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle CssClass="text-center" />

                                <ItemTemplate>

                                    <asp:Label ID="lblis_pratiVadi_from_an_org" runat="server" Text='<%# Convert.ToString(Eval("is_pratiVadi_from_an_org")) == "Y" ? "हाँ" : "नहीं" %>'>
                                    </asp:Label>

                                </ItemTemplate>

                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="विभाग का प्रतिनिधि">

                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle CssClass="text-center" />

                                <ItemTemplate>

                                    <asp:Label ID="lblis_pratiVadi_from_an_dept" runat="server" Text='<%# Convert.ToString(Eval("is_pratiVadi_from_an_dept")) == "Y" ? "हाँ" : "नहीं" %>'> </asp:Label>

                                </ItemTemplate>

                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="विभाग / संस्था का नाम">

                                <ItemTemplate>

                                    <asp:Label ID="lblOrgName" runat="server" Text='<%# Convert.ToString(Eval("is_pratiVadi_from_an_org")) == "Y"  ? Eval("pratiVadi_org_name") : Eval("org_type") %>'>  </asp:Label>

                                </ItemTemplate>

                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="विभाग / संस्था में पदनाम">

                                <ItemTemplate>

                                    <asp:Label ID="lblPadName" runat="server" Text='<%# Convert.ToString(Eval("is_pratiVadi_from_an_org")) == "Y" ? Eval("pratiVadi_org_pad_name") : Eval("pratiVadi_dept_pad_name") %>'> </asp:Label>

                                </ItemTemplate>

                            </asp:TemplateField>

                        </Columns>

                    </asp:GridView>

                </div>

            </div>

        </div>


        <div class="card shadow-sm mb-3">

            <div class="card-header bg-primary text-white fw-bold">
                अन्य विवरण
            </div>

            <div class="card-body">

                <div class="row">

                    <!-- प्रतिवादी को सूचित -->
                    <div id="divprativadi_ka_suchit_Label" runat="server" class="col-md-3 fw-bold mb-3 d-flex align-items-center">
                        प्रतिवादी को सूचित किया गया है या नहीं?
                    </div>

                    <div id="divprativadi_ka_suchit" runat="server" class="col-md-3 mb-3 d-flex align-items-center">
                        <asp:Label ID="lblprativadi_ka_suchit" runat="server" />
                    </div>

                    <!-- कारण -->
                    <div id="divprativadi_ka_Karan_Label" runat="server" class="col-md-3 fw-bold mb-3 d-flex align-items-center">
                        कारण स्पष्ट करें
                    </div>

                    <div id="divprativadi_ka_Karan" runat="server" class="col-md-3 mb-3 d-flex align-items-center">
                        <asp:Label ID="lblprativadi_ka_Karan" runat="server" />
                    </div>

                    <!-- माध्यम -->
                    <div id="divprativadi_ka_madham_Label" runat="server" class="col-md-3 fw-bold mb-3 d-flex align-items-center">
                        माध्यम
                    </div>

                    <div id="divprativadi_ka_madham" runat="server" class="col-md-3 mb-3 d-flex align-items-center">
                        <asp:Label ID="lblprativadi_ka_madham" runat="server" />
                    </div>

                    <!-- सूचना तामिला -->
                    <div id="divprativadi_ka_Suchna_Label" runat="server" class="col-md-3 fw-bold mb-3 d-flex align-items-center">
                        प्रतिवादी को सूचना तामिला प्राप्त है या नहीं?
                    </div>

                    <div id="divprativadi_ka_Suchna" runat="server" class="col-md-3 mb-3 d-flex align-items-center">
                        <asp:Label ID="lblprativadi_ka_Suchna" runat="server" />
                    </div>

                    <!-- उपस्थिति -->
                    <div id="divprativadi_ka_Upashtith_Label" runat="server" class="col-md-3 fw-bold mb-3 d-flex align-items-center">
                        प्रतिवादी उपस्थित हुआ है या नहीं?
                    </div>

                    <div id="divprativadi_ka_Upashtith" runat="server" class="col-md-3 mb-3 d-flex align-items-center">
                        <asp:Label ID="lblprativadi_ka_Upashtith" runat="server" />
                    </div>

                </div>

            </div>

        </div>

        <div class="card mb-3 shadow-sm">
            <div class="card-header bg-primary text-white font-weight-bold">
                भूमि का खाता-खेसरा का विवरण
            </div>

            <div class="card-body">

                <div class="table-responsive" id="divbhumikhata_shekher_ka_vivaran" runat="server">

                    <asp:GridView ID="grd_bhumivivad" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found!" CssClass="table table-bordered table-hover table-striped mb-0">

                        <Columns>


                            <asp:TemplateField HeaderText="Sl. No.">
                                <HeaderStyle CssClass="text-center" Width="5%" />
                                <ItemStyle CssClass="text-center align-middle" />
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:BoundField DataField="khataNo" HeaderText="खाता संख्या">
                                <ItemStyle Width="10%" />
                            </asp:BoundField>


                            <asp:BoundField DataField="khesraNo" HeaderText="खेसरा संख्या">
                                <ItemStyle Width="10%" />
                            </asp:BoundField>


                            <asp:BoundField DataField="Rakba" HeaderText="रकबा">
                                <ItemStyle Width="10%" />
                            </asp:BoundField>


                            <asp:BoundField DataField="LandTypesInKhatianDesc" HeaderText="जमीन की किस्म">
                                <ItemStyle Width="12%" />
                            </asp:BoundField>


                            <asp:TemplateField HeaderText="ख़तियन में जमीन का विवरण">
                                <ItemStyle Width="18%" />
                                <ItemTemplate>
                                    <div class="small" style="max-height: 70px; overflow-y: auto; white-space: normal;">

                                        <%# Eval("LandDetailsInKhatian") %>
                                    </div>

                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:BoundField DataField="North_chauhaddee" HeaderText="उत्तर">
                                <ItemStyle Width="10%" />
                            </asp:BoundField>


                            <asp:BoundField DataField="South_chauhaddee" HeaderText="दक्षिण">
                                <ItemStyle Width="10%" />
                            </asp:BoundField>


                            <asp:BoundField DataField="East_chauhaddee" HeaderText="पूर्व">
                                <ItemStyle Width="10%" />
                            </asp:BoundField>


                            <asp:BoundField DataField="West_chauhaddee" HeaderText="पश्चिम">
                                <ItemStyle Width="10%" />
                            </asp:BoundField>

                        </Columns>

                    </asp:GridView>

                </div>

            </div>
        </div>

        <div class="card mb-3 shadow-sm">

            <div class="card-header bg-primary text-white font-weight-bold">
                वादी द्वारा प्रस्तुत साक्ष्य का विवरण
            </div>

            <div class="card-body">

                <div class="table-responsive">

                    <asp:GridView ID="gdVadiEvidence" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found!"  CssClass="table table-bordered table-hover table-striped mb-0">

                        <Columns>


                            <asp:TemplateField HeaderText="Sl. No.">
                                <HeaderStyle CssClass="text-center" Width="5%" />
                                <ItemStyle CssClass="text-center align-middle" />

                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="साक्ष्य का प्रकार">
                                <ItemStyle Width="75%" CssClass="align-middle" />

                                <ItemTemplate>

                                    <asp:Label ID="lblEvidenceType" runat="server" Text='<%# Convert.ToString(Eval("evidence_id")) == "9"  ? Eval("evidence_any_name") : Eval("evidence_name") %>'> </asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="साक्ष्य का दस्तावेज">
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle Width="20%" CssClass="text-center align-middle" />

                                <ItemTemplate>

                                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/images/pdf.gif" Width="45" Height="45" CssClass="img-fluid" Style="cursor: pointer;" path='<%# Eval("FullfileName") %>' CommandName="View" CommandArgument='<%# Container.DataItemIndex %>' />

                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>

                    </asp:GridView>

                </div>

            </div>

        </div>

        <div class="card mb-3 shadow-sm">

            <div class="card-header bg-primary text-white font-weight-bold">
                प्रतिवादी द्वारा प्रस्तुत साक्ष्य का विवरण
            </div>

            <div class="card-body">

                <div class="table-responsive">

                    <asp:GridView ID="gdPrativadiEvidence" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found!"  CssClass="table table-bordered table-hover table-striped mb-0">

                        <Columns>


                            <asp:TemplateField HeaderText="Sl. No.">
                                <HeaderStyle CssClass="text-center" Width="5%" />
                                <ItemStyle CssClass="text-center align-middle" />

                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="साक्ष्य का प्रकार">
                                <ItemStyle Width="75%" CssClass="align-middle" />

                                <ItemTemplate>

                                    <asp:Label ID="lblEvidenceType" runat="server" Text='<%# Convert.ToString(Eval("evidence_id")) == "9" ? Eval("evidence_any_name") : Eval("evidence_name") %>'>  </asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="साक्ष्य का दस्तावेज">
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle Width="20%" CssClass="text-center align-middle" />

                                <ItemTemplate>

                                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/images/pdf.gif" Width="45" Height="45" CssClass="img-fluid" Style="cursor: pointer;" path='<%# Eval("FullfileName") %>' />

                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>

                    </asp:GridView>

                </div>

            </div>

        </div>

        <div class="card mb-3 shadow-sm">

            <div class="card-header bg-primary text-white font-weight-bold">
                राजस्व अधिकारी / पुलिस पदाधिकारी / हल्का कर्मचारी द्वारा प्रस्तुत साक्ष्य का विवरण
            </div>

            <div class="card-body">


                <div class="row mb-3">

                    <div class="col-md-4 font-weight-bold" id="div2" runat="server">
                        पुलिस पदाधिकारी द्वारा समर्पित जाँच प्रतिवेदन की संक्षिप्त विवरणी
                    </div>

                    <div class="col-md-8" id="divPoliceAdhikari" runat="server">
                        <asp:Label ID="lblPoliceAdhikari" runat="server"></asp:Label>
                    </div>

                </div>


                <div class="row mb-3" id="divHalkaKarmchari" runat="server">

                    <div class="col-md-4 font-weight-bold" id="div4" runat="server">
                        हल्का कर्मचारी / राजस्व अधिकारी द्वारा समर्पित जाँच प्रतिवेदन की संक्षिप्त विवरणी
                    </div>

                    <div class="col-md-8" id="divHalkaKarmchariValue" runat="server">
                        <asp:Label ID="lblHalkaKarmchariValue" runat="server"></asp:Label>
                    </div>

                </div>

                <!-- Report Documents -->
                <div class="row mb-4" id="div1" runat="server">

                    <div class="col-md-6">

                        <div class="d-flex justify-content-between align-items-center border rounded p-2">

                            <span class="font-weight-bold">पुलिस पदाधिकारी द्वारा समर्पित जाँच प्रतिवेदन का दस्तावेज </span>

                            <asp:ImageButton ID="lnkpulis_padadhikari_Patr_file" runat="server" ImageUrl="~/images/pdf.gif" Width="45" Height="45" CssClass="img-fluid getpdfdoc" path="display" Style="cursor: pointer;" />

                        </div>

                    </div>

                    <div class="col-md-6 mt-3 mt-md-0">

                        <div class="d-flex justify-content-between align-items-center border rounded p-2">

                            <span class="font-weight-bold">हल्का कर्मचारी / राजस्व अधिकारी द्वारा समर्पित जाँच प्रतिवेदन का दस्तावेज
                            </span>

                            <asp:ImageButton ID="lnkfile_halkakarmchari_praptr" runat="server" ImageUrl="~/images/pdf.gif" Width="45" Height="45" CssClass="img-fluid getpdfdoc" path="display" Style="cursor: pointer;" />

                        </div>

                    </div>

                </div>


                <div class="row mb-3" id="divVivaditBhukand" runat="server">

                    <div class="col-md-4 font-weight-bold" id="div6" runat="server">
                        विवादित भू-खंड की मापी
                    </div>

                    <div class="col-md-8" id="divVivaditBhukandValue" runat="server">
                        <asp:Label ID="lblVivaditBhukandValue" runat="server"></asp:Label>
                    </div>

                </div>


                <div class="row mb-3" id="divMapi" runat="server">

                    <div class="col-md-4 font-weight-bold" id="div8" runat="server">
                        मापी
                    </div>

                    <div class="col-md-8" id="divMapiValue" runat="server">
                        <asp:Label ID="lblMapiValue" runat="server"></asp:Label>
                    </div>

                </div>


                <div class="row mb-4" id="divVivaditBhukandKaMapi" runat="server">

                    <div class="col-md-4 font-weight-bold" id="div10" runat="server">
                        विवादित भू-खंड की मापी नहीं होने का कारण
                    </div>

                    <div class="col-md-8 mb-3" id="divVivaditBhukandKaMapiValue" runat="server">
                        <asp:Label ID="lblVivaditBhukandKaMapiValue" runat="server"></asp:Label>
                    </div>

                    <div class="col-md-4 font-weight-bold" id="div7" runat="server">
                        विवादित भू-खंड की मापी का प्रतिवेदन
                    </div>

                    <div class="col-md-8" id="div9" runat="server">

                        <asp:ImageButton ID="lnkfile_bhukand_prativedan" runat="server" ImageUrl="~/images/pdf.gif" Width="45" Height="45" CssClass="img-fluid getpdfdoc" path="display" Style="cursor: pointer;" />

                    </div>

                </div>

                <!-- Scheduled Date -->
                <div class="row" id="MapiKeNirdharnKiThithi" runat="server">

                    <div class="col-md-4 font-weight-bold" id="div5" runat="server">
                        मापी के लिए निर्धारित तिथि
                    </div>

                    <div class="col-md-8" id="divMapiKeNirdharnKiThithiValue" runat="server">
                        <asp:Label ID="lblMapiKeNirdharnKiThithiValue" runat="server"></asp:Label>
                    </div>

                </div>

            </div>

        </div>

        <div class="card shadow-sm mb-3">
            <div class="card-header bg-primary text-white font-weight-bold">
                भूमि विवाद सें संबंधित घटना / वारदात का विवरण
            </div>

            <div class="card-body">

                <!-- FIR Status -->
                <div class="row align-items-center mb-3">
                    <div class="col-md-3 font-weight-bold" id="div3" runat="server">
                        प्राथमिकी / अप्राथमिकी / सनहा दर्ज है ?
                    </div>

                    <div class="col-md-3" id="div_Prathamik" runat="server">
                        <asp:Label ID="lblPrathamik" runat="server"></asp:Label>
                    </div>
                </div>

                <!-- Grid -->
                <div class="table-responsive">

                    <asp:GridView ID="grdbhumivivad" runat="server" Width="100%" AutoGenerateColumns="False" EmptyDataText="No Record Found!" CssClass="table table-bordered table-striped table-hover">

                        <Columns>

                           
                            <asp:TemplateField HeaderText="Sl. No.">
                                <HeaderStyle CssClass="text-center" Width="5%" />
                                <ItemStyle CssClass="text-center align-middle" />
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>

                          
                            <asp:BoundField DataField="Ghatna_Vardat_date" HeaderText="घटना की तिथि">
                                <ItemStyle Width="8%" />
                            </asp:BoundField>

                          
                            <asp:TemplateField HeaderText="घटना की संक्षिप्त विवरण">
                                <ItemStyle Width="18%" />
                                <ItemTemplate>
                                    <div style="max-height: 60px; overflow-y: auto;">
                                        <%# Eval("Ghatna_Short_vivran") %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:BoundField DataField="is_FIR_registered" HeaderText="प्राथमिकी">
                                <ItemStyle Width="6%" />
                            </asp:BoundField>


                            <asp:BoundField DataField="praathamiki_sankhya" HeaderText="प्राथमिकी संख्या">
                                <ItemStyle Width="8%" />
                            </asp:BoundField>


                            <asp:TemplateField HeaderText="प्राथमिकी का विवरण">
                                <ItemStyle Width="18%" />
                                <ItemTemplate>
                                    <div style="max-height: 60px; overflow-y: auto;">
                                        <%# Eval("praathamiki_ka_vivaran") %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:BoundField DataField="is_complaint_filed" HeaderText="अप्राथमिकी">
                                <ItemStyle Width="8%" />
                            </asp:BoundField>


                            <asp:BoundField DataField="dhaara" HeaderText="धारा">
                                <ItemStyle Width="6%" />
                            </asp:BoundField>


                            <asp:BoundField DataField="apraathamiki_sankhya" HeaderText="अप्राथमिकी संख्या">
                                <ItemStyle Width="8%" />
                            </asp:BoundField>


                            <asp:TemplateField HeaderText="अप्राथमिकी का विवरण">
                                <ItemStyle Width="18%" />
                                <ItemTemplate>
                                    <div style="max-height: 60px; overflow-y: auto;">
                                        <%# Eval("apraathamiki_ka_vivaran") %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:BoundField DataField="is_Sanha_recorded" HeaderText="सनहा">
                                <ItemStyle Width="6%" />
                            </asp:BoundField>


                            <asp:BoundField DataField="sanha_sankhya" HeaderText="सनहा संख्या">
                                <ItemStyle Width="8%" />
                            </asp:BoundField>


                            <asp:TemplateField HeaderText="अभियुक्ति">
                                <ItemStyle Width="18%" />
                                <ItemTemplate>
                                    <div style="max-height: 60px; overflow-y: auto;">
                                        <%# Eval("Abhiyukt") %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>

                    </asp:GridView>

                </div>

            </div>
        </div>

        <div class="container-fluid">


            <div class="row mb-3">
                <div class="col-12">
                    <h4 class="text-center font-weight-bold text-dark">न्यायालय में प्रक्रियाधीन वाद का विवरण
                    </h4>
                </div>
            </div>


            <div class="card shadow-sm border-0 mb-3">

                <div class="card-header bg-primary text-white font-weight-bold">
                    न्यायालय में प्रक्रियाधीन वाद का विवरण
                </div>

                <div class="card-body">


                    <div class="row mb-4 align-items-center">

                        <div class="col-md-4 font-weight-bold" id="div11" runat="server">
                            प्रक्रियाधीन वाद का विवरण उपलब्ध है ?
                        </div>

                        <div class="col-md-8" id="divPrakiriyaVad" runat="server">
                            <asp:Label ID="lblPrakiriyaVad" runat="server" CssClass="font-weight-bold text-primary"> </asp:Label>
                        </div>

                    </div>


                    <div class="table-responsive">

                        <asp:GridView ID="grdnyayalay_vivran" runat="server" Width="100%" AutoGenerateColumns="False" EmptyDataText="No Record Found!" CssClass="table table-bordered table-striped table-hover">

                            <Columns>


                                <asp:TemplateField HeaderText="Sl. No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="text-center" Width="5%" />
                                    <ItemStyle CssClass="text-center" />
                                </asp:TemplateField>


                                <asp:BoundField DataField="court" HeaderText="न्यायालय" />


                                <asp:BoundField DataField="courtType" HeaderText="न्यायालय का प्रकार" />


                                <asp:BoundField DataField="Dst" HeaderText="जिला" />


                                <asp:BoundField DataField="SubDiv" HeaderText="अनुमंडल" />


                                <asp:BoundField DataField="Vibhag" HeaderText="विभाग" />


                                <asp:BoundField DataField="vaadi_ki_vaad_sankhya_varsh" HeaderText="वाद संख्या / वर्ष" />


                                <asp:BoundField DataField="vadi_name" HeaderText="वादी का नाम" />


                                <asp:BoundField DataField="prativadi_name" HeaderText="प्रतिवादी का नाम" />


                                <asp:BoundField DataField="vaad_ki_addhatan_sthiti_vivaran" HeaderText="अद्यतन स्थिति का विवरण" />

                            </Columns>

                        </asp:GridView>

                    </div>

                </div>

            </div>

        </div>

        <div class="card shadow-sm border-0 mb-3">

            <div class="card-header bg-primary text-white font-weight-bold">
                अंचलाधिकारी एवं थाना अध्यक्ष द्वारा भूमि विवाद के निराकरण हेतु कृत कार्रवाई की विवरणी
            </div>

            <div class="card-body">

                <div class="table-responsive">

                    <asp:GridView ID="GVAnchalaDhakari" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="a_id" EmptyDataText="No Record Found" ShowHeaderWhenEmpty="True" AllowPaging="False" PageSize="25" ShowFooter="True" CssClass="table table-bordered table-striped table-hover">

                        <Columns>

                          
                            <asp:TemplateField HeaderText="Sl. No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="text-center" Width="5%" />
                                <ItemStyle CssClass="text-center align-middle" />
                            </asp:TemplateField>

                          
                            <asp:TemplateField HeaderText="भूमि विवाद की संवेदनशीलता">
                                <ItemTemplate>
                                    <%# Eval("SensitivityType") %>
                                </ItemTemplate>
                                <ItemStyle CssClass="align-middle" Width="13%" />
                            </asp:TemplateField>

                           
                            <asp:TemplateField HeaderText="बैठक की तिथि">
                                <ItemTemplate>
                                    <%# Eval("Meeting_date", "{0:dd MMM yyyy}") %>
                                </ItemTemplate>
                                <ItemStyle CssClass="align-middle" Width="8%" />
                            </asp:TemplateField>

                         
                            <asp:TemplateField HeaderText="क्या वादी उपस्थित है ?">
                                <ItemTemplate>
                                    <%# Eval("Is_Vadi_Present") %>
                                </ItemTemplate>
                                <ItemStyle CssClass="align-middle" Width="10%" />
                            </asp:TemplateField>

                           
                            <asp:TemplateField HeaderText="क्या प्रतिवादी उपस्थित है ?">
                                <ItemTemplate>
                                    <%# Eval("Is_PratiVadi_Present") %>
                                </ItemTemplate>
                                <ItemStyle CssClass="align-middle" Width="12%" />
                            </asp:TemplateField>

                         
                            <asp:TemplateField HeaderText="बैठक का निष्कर्ष">
                                <ItemTemplate>
                                    <%# Eval("Action") %>
                                </ItemTemplate>
                                <ItemStyle CssClass="align-middle" Width="8%" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="अंचलाधिकारी का मंतव्य">
                                <ItemTemplate>
                                    <%# Eval("anchala_dhikari_mantavy") %>
                                </ItemTemplate>
                                <ItemStyle CssClass="align-middle" Width="12%" />
                            </asp:TemplateField>

                       
                            <asp:TemplateField HeaderText="थानाध्यक्ष का मंतव्य">
                                <ItemTemplate>
                                    <%# Eval("thana_prabhari_mantavy") %>
                                </ItemTemplate>
                                <ItemStyle CssClass="align-middle" Width="12%" />
                            </asp:TemplateField>

                        
                            <asp:TemplateField HeaderText="थानाध्यक्ष एवं अंचलाधिकारी का संयुक्त प्रतिवेदन">
                                <ItemTemplate>

                                    <asp:ImageButton ID="Image1"
                                        runat="server" Visible='<%# CheckImage(Eval("Joint_report_SHO_Circle_Officer_file")) %>' path='<%# Eval("Joint_report_SHO_Circle_Officer_file") %>' CssClass="getpdfdoc" ImageUrl="~/images/pdf.gif" Width="45px" Height="45px" Style="cursor: pointer;" />

                                </ItemTemplate>

                                <ItemStyle CssClass="text-center align-middle" Width="15%" />

                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="बैठक में लिया गया निर्णय">
                                <ItemTemplate>
                                    <%# Eval("conclusion_of_the_meeting") %>
                                </ItemTemplate>
                                <ItemStyle CssClass="align-middle" Width="15%" />
                            </asp:TemplateField>

                        </Columns>

                    </asp:GridView>

                </div>

            </div>

        </div>

        <div class="card shadow-sm border-0 mb-3">

            <!-- Card Header -->
            <div class="card-header bg-primary text-white font-weight-bold">
                मंतव्य की विवरणी
            </div>

            <div class="card-body">

                <div class="table-responsive">

                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-hover" EmptyDataText="No Record Found" ShowHeaderWhenEmpty="True" ShowFooter="True" AllowPaging="False" PageSize="25" >

                        <Columns>


                            <asp:TemplateField HeaderText="Sl. No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="text-center" Width="5%" />
                                <ItemStyle CssClass="text-center align-middle" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="आवेदन संख्या">
                                <ItemTemplate>
                                    <%# Eval("ApplicationNo") %>
                                </ItemTemplate>
                                <ItemStyle CssClass="align-middle" Width="15%" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="मंतव्य">
                                <ItemTemplate>
                                    <%# Eval("Remarks") %>
                                </ItemTemplate>
                                <ItemStyle CssClass="align-middle" Width="50%" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="मंतव्य विवरण द्वारा">
                                <ItemTemplate>
                                    <%# Eval("usernamee") %>
                                </ItemTemplate>
                                <ItemStyle CssClass="align-middle" Width="20%" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="दस्तावेज देखें">
                                <ItemTemplate>

                                    <asp:ImageButton ID="Image1" runat="server" Visible='<%# CheckImage(Eval("Remarks_file")) %>' path='<%# Eval("Remarks_file") %>' CssClass="getpdfdoc" ImageUrl="~/images/pdf.gif" Width="45px" Height="45px" Style="cursor: pointer;"
                                        CommandArgument='<%# Container.DataItemIndex %>' CommandName="View" />

                                </ItemTemplate>

                                <HeaderStyle CssClass="text-center" Width="10%" />
                                <ItemStyle CssClass="text-center align-middle" />

                            </asp:TemplateField>

                        </Columns>

                    </asp:GridView>

                </div>

            </div>

        </div>

        <div class="card shadow-sm mb-3">

            <div class="card-header bg-primary text-white fw-bold">
                >>> नई बैठक के अनुसार अंचलाधिकरी एवम्‌ थाना अध्यक्ष द्वारा भूमि विवाद के निराकरण हेतु कृत करवाई की विवरणी जोड़ें >>>
            </div>

            <asp:HiddenField ID="lastAction" runat="server" Value="0" />

            <div class="card-body">


                <div class="row g-3 align-items-center mb-3">

                    <div class="col-lg-3 col-md-6">
                        <label class="form-label fw-bold">
                            भूमि विवाद की सवेदनशीलता<span class="text-danger">*</span>
                        </label>

                        <asp:DropDownList ID="ddlbhumivivadki_sanvedanshilta" runat="server" CssClass="form-select" AutoPostBack="true"></asp:DropDownList>

                    </div>

                    <div class="col-lg-3 col-md-6 text-center">

                        <asp:Image ID="onestar" runat="server" ImageUrl="images/1.png" Width="100" Visible="true" />

                        <asp:Image ID="twostar" runat="server" ImageUrl="images/2.png" Width="100" Visible="false" />

                        <asp:Image ID="threestar" runat="server" ImageUrl="images/3.png" Width="100" Visible="false" />

                        <asp:Image ID="fourstar" runat="server" ImageUrl="images/4.png" Width="100" Visible="false" />

                    </div>

                </div>

                <hr />

                <!-- Meeting -->
                <div class="row g-3 mb-3">

                    <div class="col-lg-3 col-md-6">

                        <label class="form-label fw-bold">
                            बैठक की तिथि <span class="text-danger">*</span>
                        </label>

                        <asp:TextBox ID="txtbaithakDate" runat="server" CssClass="form-control" AutoComplete="off" onkeypress="return dateValidate(event)">  </asp:TextBox>

                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtbaithakDate" Format="dd-MM-yyyy" CssClass="zindex" />

                    </div>

                    <div class="col-lg-3 col-md-6">

                        <label class="form-label fw-bold">
                            क्या वादी उपस्थित है ? <span class="text-danger">*</span>
                        </label>

                        <asp:DropDownList ID="ddlIsVadiAvailable" runat="server" CssClass="form-select">

                            <asp:ListItem Value="0">--चुने--</asp:ListItem>
                            <asp:ListItem Value="Y">हां</asp:ListItem>
                            <asp:ListItem Value="N">नहीं</asp:ListItem>

                        </asp:DropDownList>

                    </div>

                    <div class="col-lg-3 col-md-6">

                        <label class="form-label fw-bold">
                            क्या प्रतिवादी उपस्थित है ? <span class="text-danger">*</span>
                        </label>

                        <asp:DropDownList ID="ddl_IsprativadiAvailable" runat="server" CssClass="form-select">

                            <asp:ListItem Value="0">--चुने--</asp:ListItem>
                            <asp:ListItem Value="Y">हां</asp:ListItem>
                            <asp:ListItem Value="N">नहीं</asp:ListItem>

                        </asp:DropDownList>

                    </div>

                    <div class="col-lg-3 col-md-6">

                        <label class="form-label fw-bold">
                            बैठक का निष्कर्ष <span class="text-danger">*</span>
                        </label>

                        <asp:DropDownList ID="ddlaction" runat="server" CssClass="form-select" AutoPostBack="true">

                            <asp:ListItem Value="0">--चुने--</asp:ListItem>
                            <asp:ListItem Value="1">प्रारंभिक निष्पादन</asp:ListItem>
                            <asp:ListItem Value="4">अस्वीकृत</asp:ListItem>
                            <asp:ListItem Value="2">मापी के लिए निर्धारित</asp:ListItem>
                            <asp:ListItem Value="3">प्रक्रियाधीन</asp:ListItem>
                            <asp:ListItem Value="5">अंतिम निष्पादन</asp:ListItem>
                            <asp:ListItem Value="6">न्यायालय में लंबित</asp:ListItem>

                        </asp:DropDownList>

                    </div>

                </div>

                <!-- Remarks -->

                <div class="row g-3 mb-3">

                    <div class="col-lg-3">
                        <label class="form-label fw-bold">अंचलाधिकारी का मंतव्य</label>

                        <asp:TextBox ID="txtabhiyukt_anchaladhikari" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" MaxLength="500"> </asp:TextBox>

                        <div class="text-end small text-danger">
                            अधिकतम 500 वर्ण
                        </div>

                    </div>

                    <div class="col-lg-3">

                        <label class="form-label fw-bold">थानाध्यक्ष का मंतव्य</label>

                        <asp:TextBox ID="txtabhiyukt_thaanprabhaaree" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" MaxLength="500"> </asp:TextBox>

                        <div class="text-end small text-danger">
                            अधिकतम 500 वर्ण
                        </div>

                    </div>

                    <div class="col-lg-3">

                        <label class="form-label fw-bold">बैठक में लिया गया निर्णय</label>

                        <asp:TextBox ID="txtfalafal" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" MaxLength="500"></asp:TextBox>

                        <div class="text-end small text-danger">
                            अधिकतम 500 वर्ण
                        </div>

                    </div>

                </div>


                <div class="row g-3 mb-3">

                    <div id="divlabNextDate" runat="server" visible="false" class="col-lg-3">
                        <label class="form-label fw-bold">
                            <asp:Label ID="labNextDate" runat="server" Text="अगला/मापी की तिथि"></asp:Label>
                            <span class="text-danger">*</span>
                        </label>
                    </div>

                    <div id="divNextDate" runat="server" visible="false" class="col-lg-3">

                        <asp:TextBox ID="txtAgalaDate" runat="server" CssClass="form-control" AutoComplete="off" onkeypress="return dateValidate(event)">  </asp:TextBox>

                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtAgalaDate" Format="dd-MM-yyyy" CssClass="zindex" />

                    </div>

                    <div id="divvadkavars" runat="server" visible="false" class="col-lg-3">

                        <asp:TextBox ID="txtvadkavars" runat="server" CssClass="form-control"> </asp:TextBox>

                    </div>

                    <div id="divCancelReason" runat="server" visible="false" class="col-lg-3">

                        <asp:TextBox ID="txtCancelReason" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" MaxLength="500">  </asp:TextBox>

                    </div>

                </div>

                <!-- File Upload -->

                <div class="row g-3">

                    <div class="col-lg-4">

                        <label class="form-label fw-bold">
                            संयुक्त प्रतिवेदन
                        </label>

                        <asp:FileUpload ID="LandDoc" runat="server" CssClass="form-control" accept=".pdf" />

                        <asp:HiddenField ID="hdLandDoc" runat="server" />

                    </div>

                    <div class="col-lg-4">

                        <label class="form-label fw-bold">
                            अंचलाधिकारी का मंतव्य पत्र
                        </label>

                        <asp:FileUpload ID="CircleOfficer_letterOfIntent" runat="server" CssClass="form-control" accept=".pdf" />

                        <asp:HiddenField ID="hdCircleOfficer_letterofintent" runat="server" />

                    </div>

                    <div class="col-lg-4">

                        <label class="form-label fw-bold">
                            थानाध्यक्ष का मंतव्य पत्र
                        </label>

                        <asp:FileUpload ID="PoliceOfficer_letterOfIntent" runat="server" CssClass="form-control" accept=".pdf" />

                        <asp:HiddenField ID="hdPoliceOfficer_letterOfIntent" runat="server" />

                    </div>

                </div>

            </div>

            <div class="card-footer text-center">

                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success me-2" OnClientClick="return SaveAnotherMetting();" />

                <asp:Button ID="btnCancel" runat="server" Text="Go Back" CssClass="btn btn-secondary me-2" OnClientClick="JavaScript:window.history.back(1); return true;" />

                <asp:Button ID="btnDraft" runat="server" CssClass="btn btn-info" Text="Send To Draft" Visible="false" />

                <div class="mt-3">
                    <asp:Label ID="lblMsg" runat="server" CssClass="fw-bold text-danger"> </asp:Label>
                </div>

            </div>

        </div>
    </div>
</asp:Content>
