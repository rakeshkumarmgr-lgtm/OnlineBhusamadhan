<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddMettingApplication.aspx.cs" Inherits="Bhusamadhan.LandDispute.Entry.AddMettingApplication" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function SaveAnotherMetting() {
            debugger;

            var ddlbhumivivadki_sanvedanshilta = document.getElementById('<%=ddlbhumivivadki_sanvedanshilta.ClientID%>')
            if (ddlbhumivivadki_sanvedanshilta.selectedIndex == 0) {
                alert("कृपया भूमि की संवेदनशीलता चुनें...!");
                ddlbhumivivadki_sanvedanshilta.focus();
                return false;
            }

            var txtbaithakDate = document.getElementById('<%=txtbaithakDate.ClientID%>');
            if (txtbaithakDate.value.trim() == "") {
                alert("कृपया बैठक की तिथि अंकित करें...!");
                txtbaithakDate.focus();
                return false;
            }

            var ddlIsVadiAvailable = document.getElementById('<%=ddlIsVadiAvailable.ClientID%>');
            if (ddlIsVadiAvailable.selectedIndex == 0) {
                alert("क्या वादी उपस्थित है ? हां/नहीं चुनें...!");
                ddlIsVadiAvailable.focus();
                return false;
            }

            var ddl_IsprativadiAvailable = document.getElementById('<%=ddl_IsprativadiAvailable.ClientID%>');
            if (ddl_IsprativadiAvailable.selectedIndex == 0) {
                alert("क्या प्रतिवादी उपस्थित है ? हां/नहीं चुनें...!");
                ddl_IsprativadiAvailable.focus();
                return false;
            }

            var ddlaction = document.getElementById('<%=ddlaction.ClientID%>');
            if (ddlaction.selectedIndex == 0) {
                alert("कृपया बैठक का निष्कर्ष चुनें...!");
                ddlaction.focus();
                return false;
            }

            var txtAgalaDate = document.getElementById('<%=txtAgalaDate.ClientID%>');
            var txtvadkavars = document.getElementById('<%=txtvadkavars.ClientID%>');
            if (ddlaction.selectedIndex == 1) {
                if (txtAgalaDate.value.trim() == "") {
                    alert("कृपया निस्तारण की तिथि अंकित करें...!");
                    txtAgalaDate.focus();
                    return false;
                }
            }

            var txtCancelReason = document.getElementById('<%=txtCancelReason.ClientID%>')
            if (ddlaction.selectedIndex == 2) {
                if (txtCancelReason.value.trim() == "") {
                    alert("कृपया अस्वीकृति का कारण अंकित करें...!");
                    txtCancelReason.focus();
                    return false;
                }
            }

            if (ddlaction.selectedIndex == 3) {
                if (txtAgalaDate.value.trim() == "") {
                    alert("कृपया मापी की तिथि अंकित करें...!");
                    txtAgalaDate.focus();
                    return false;
                }
            }

            if (ddlaction.selectedIndex == 4) {
                if (txtAgalaDate.value.trim() == "") {
                    alert("कृपया अगली सुनवाई की तिथि अंकित करें...!");
                    txtAgalaDate.focus();
                    return false;
                }
            }
            if (ddlaction.selectedIndex == 5) {
                if (txtvadkavars.value.trim() == "") {
                    alert("वादी की वाद संख्या / वर्ष अंकित करें...!");
                    txtvadkavars.focus();
                    return false;
                }
            }
            var LandDoc = document.getElementById("<%=LandDoc.ClientID %>").value;
            if (LandDoc != '') {
                var valid_extensions = /(.pdf)$/i;
                if (!valid_extensions.test(LandDoc)) {
                    alert('Please Select only PDF File?');
                    return false;
                }
            }


            if (!confirm('Are you sure to save data?')) {
                return false;
            }

            return true;
        }


        function checkDate(sender, args) {

            var ddl = document.getElementById('<%=ddlaction.ClientID%>');
            if (ddl.selectedIndex == 3) {

            }
            else {
                if (sender._selectedDate > new Date()) {
                    alert("You cannot select a day latter than today!");
                    sender._selectedDate = new Date();
                    // set the date back to the current date
                    sender._textbox.set_Value("")
                }
                else if (sender._selectedDate.getDay() != 6) {
                    alert("You can only select Saturday!");
                    sender._selectedDate = new Date(); //set back to current date
                    sender._textbox.set_Value("");
                }


            }

        }
        function dateValidate(evt) {
            alert(ddl);
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode == 45) {
                return true;
            }
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }

            return true;
        }
    </script>
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

                    <div class="col-md-3 fw-bold">ग्राम पंचायत </div>

                    <div class="col-md-3 fw-bold">

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
                        <asp:Label ID="lblvadi_Vivad_Ki_Adyatan_Sthithi" runat="server"></asp:Label>
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
                        <asp:Label ID="lblvadi_sarkari_bhumi_ka_prakar" runat="server"></asp:Label>
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
                    <table class="table table-bordered table-hover table-striped mb-0 align-middle">
                        <thead class="table-primary text-center">
                            <tr>
                                <th style="width: 5%;">क्र. सं.</th>
                                <th>वादी का नाम</th>
                                <th>पिता / पति का नाम</th>
                                <th>लिंग</th>
                                <th>उम्र (वर्ष)</th>
                                <th>जिला</th>
                                <th>अनुमंडल</th>
                                <th>अंचल</th>
                                <th>थाना</th>
                                <th>क्षेत्र का प्रकार</th>
                                <th>ग्राम पंचायत</th>
                                <th>राजस्व ग्राम</th>
                                <th>वार्ड</th>
                                <th>मोबाइल संख्या</th>
                                <th>विभाग का प्रतिनिधि</th>
                                <th>संस्था का प्रतिनिधि</th>
                                <th>विभाग / संस्था का नाम</th>
                                <th>विभाग / संस्था में पदनाम</th>
                            </tr>
                        </thead>

                        <tbody>
                            <asp:Repeater ID="rptWadi" runat="server">
                                <ItemTemplate>
                                    <tr>

                                        <td class="text-center">
                                            <%# Container.ItemIndex + 1 %>
                                        </td>

                                        <td>
                                            <%# Eval("NameAsPerAadhaar") %>
                                        </td>

                                        <td>
                                            <%# Eval("Vadi_Father_Husband_Name") %>
                                        </td>

                                        <td class="text-center">
                                            <%# Convert.ToString(Eval("SexAsPerAadhaar")) == "F" ? "Female"  : "Male" %>
                                        </td>

                                        <td class="text-center">
                                            <%# Eval("YearOfBirthAsPerAadhaar") %>
                                        </td>

                                        <td>
                                            <%# Eval("dist") %>
                                        </td>


                                        <td>
                                            <%# Eval("sub_division") %>
                                        </td>

                                        <td>
                                            <%# Eval("block") %>
                                        </td>

                                        <td>
                                            <%# Eval("thana") %>
                                        </td>

                                        <td>
                                            <%# Eval("area_type") %>
                                        </td>


                                        <td>
                                            <%# Eval("panchayt") %>
                                        </td>

                                        <td>
                                            <%# Eval("village") %>
                                        </td>

                                        <td class="text-center">
                                            <%# Eval("WardNo") %>
                                        </td>


                                        <td class="text-center">
                                            <%# Eval("Vadi_MobileNo") %>
                                        </td>

                                        <td class="text-center">
                                            <%# Convert.ToString(Eval("IsDepartmentRepresentative")) == "Y" ? "हाँ" : "नहीं" %>
                                        </td>

                                        <td class="text-center">
                                            <%# Convert.ToString(Eval("IsOrganizationRepresentative")) == "Y" ? "हाँ" : "नहीं" %>
                                        </td>


                                        <td>
                                            <%# Convert.ToString(Eval("IsOrganizationRepresentative")) == "Y" ? Convert.ToString(Eval("DepartmentOrganizationName")): Convert.ToString(Eval("org_type")) %>
                                        </td>

                                        <td>
                                            <%# Convert.ToString(Eval("IsOrganizationRepresentative")) == "Y"  ? Convert.ToString(Eval("DepartmentOrganizationPost")) : Convert.ToString(Eval("DepartmentOrganizationPost")) %>
                                        </td>
                                    </tr>
                                </ItemTemplate>

                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>

            </div>

        </div>

        <div class="card shadow-sm mb-3">

            <div class="card-header bg-primary text-white">
                <h5 class="mb-0">प्रतिवादी का विवरण</h5>
            </div>

            <div class="card-body p-0">

                <div class="table-responsive">
                    <table class="table table-bordered table-striped table-hover mb-0 preview-table">
                        <thead class="table-primary text-center align-middle">
                            <tr>
                                <th style="width: 5%;">क्र. सं.</th>
                                <th>प्रतिवादी का नाम</th>
                                <th>पिता / पति का नाम</th>
                                <th>जिला</th>
                                <th>अनुमंडल</th>
                                <th>अंचल</th>
                                <th>थाना</th>
                                <th>क्षेत्र का प्रकार</th>
                                <th>ग्राम पंचायत</th>
                                <th>राजस्व ग्राम</th>
                                <th>वार्ड</th>
                                <th>मोबाइल संख्या</th>
                                <th>संस्था का प्रतिनिधि</th>
                                <th>विभाग का प्रतिनिधि</th>
                                <th>विभाग / संस्था का नाम</th>
                                <th>विभाग / संस्था में पदनाम</th>
                            </tr>
                        </thead>

                        <tbody>
                            <asp:Repeater ID="rptPratiWadi" runat="server">

                                <ItemTemplate>
                                    <tr>

                                        <td class="text-center">
                                            <%# Container.ItemIndex + 1 %>
                                        </td>

                                        <td>
                                            <%# Eval("pratiVadi_Name") %>
                                        </td>

                                        <td>
                                            <%# Eval("pratiVadi_Father_Husband_Name") %>
                                        </td>

                                        <td>
                                            <%# Eval("dist") %>
                                        </td>

                                        <td>
                                            <%# Eval("sub_division") %>
                                        </td>

                                        <td>
                                            <%# Eval("block") %>
                                        </td>

                                        <td>
                                            <%# Eval("thana") %>
                                        </td>

                                        <td>
                                            <%# Eval("area_type") %>
                                        </td>

                                        <td>
                                            <%# Eval("panchayt") %>
                                        </td>

                                        <td>
                                            <%# Eval("village") %>
                                        </td>

                                        <td class="text-center">
                                            <%# Eval("WardNo") %>
                                        </td>

                                        <td class="text-center">
                                            <%# Eval("pratiVadi_MobileNo") %>
                                        </td>

                                        <td class="text-center">
                                            <%# Convert.ToString(Eval("IsOrganizationRepresentative")) == "Y"  ? "हाँ"  : "नहीं" %>
                                        </td>


                                        <td class="text-center">
                                            <%# Convert.ToString(Eval("IsDepartmentRepresentative")) == "Y"  ? "हाँ" : "नहीं" %>
                                        </td>

                                        <td>
                                            <%# Convert.ToString(Eval("IsOrganizationRepresentative")) == "Y" ? Convert.ToString(Eval("DepartmentOrganizationName"))  : Convert.ToString(Eval("associationName")) %>
                                        </td>

                                        <td>
                                            <%# Convert.ToString(Eval("IsOrganizationRepresentative")) == "Y" ? Convert.ToString(Eval("DepartmentOrganizationPost"))  : Convert.ToString(Eval("DepartmentOrganizationPost")) %>
                                        </td>
                                    </tr>
                                </ItemTemplate>


                            </asp:Repeater>
                        </tbody>
                    </table>
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
                    <div runat="server" class="col-md-3 fw-bold mb-3 d-flex align-items-center">
                        प्रतिवादी को सूचित किया गया है या नहीं?
                    </div>

                    <div class="col-md-3 mb-3 d-flex align-items-center">
                        <asp:Label ID="lblprativadi_ka_suchit" runat="server" />
                    </div>

                    <!-- कारण -->
                    <div class="col-md-3 fw-bold mb-3 d-flex align-items-center">
                        कारण स्पष्ट करें
                    </div>

                    <div class="col-md-3 mb-3 d-flex align-items-center">
                        <asp:Label ID="lblprativadi_ka_Karan" runat="server" />
                    </div>

                    <!-- माध्यम -->
                    <div class="col-md-3 fw-bold mb-3 d-flex align-items-center">
                        माध्यम
                    </div>

                    <div class="col-md-3 mb-3 d-flex align-items-center">
                        <asp:Label ID="lblprativadi_ka_madhayam" runat="server" />
                    </div>

                    <!-- सूचना तामिला -->
                    <div class="col-md-3 fw-bold mb-3 d-flex align-items-center">
                        प्रतिवादी को सूचना तामिला प्राप्त है या नहीं?
                    </div>

                    <div class="col-md-3 mb-3 d-flex align-items-center">
                        <asp:Label ID="lblprativadi_ka_SuchnaTamil" runat="server" />
                    </div>

                    <!-- उपस्थिति -->
                    <div class="col-md-3 fw-bold mb-3 d-flex align-items-center">
                        प्रतिवादी उपस्थित हुआ है या नहीं?
                    </div>

                    <div class="col-md-3 mb-3 d-flex align-items-center">
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

                <div class="table-responsive">
                    <table class="table table-bordered table-hover table-striped mb-0 preview-table">

                        <thead class="table-primary text-center align-middle">
                            <tr>
                                <th style="width: 5%;">क्र. सं.</th>
                                <th style="width: 10%;">खाता संख्या</th>
                                <th style="width: 10%;">खेसरा संख्या</th>
                                <th style="width: 10%;">रकबा</th>
                                <th style="width: 12%;">जमीन की किस्म</th>
                                <th style="width: 18%;">ख़तियन में जमीन का विवरण</th>
                                <th style="width: 10%;">उत्तर</th>
                                <th style="width: 10%;">दक्षिण</th>
                                <th style="width: 10%;">पूर्व</th>
                                <th style="width: 10%;">पश्चिम</th>
                            </tr>
                        </thead>

                        <tbody>

                            <asp:Repeater ID="rptBhumiKhataKhesra" runat="server">

                                <ItemTemplate>
                                    <tr>

                                        <!-- Sl. No. -->
                                        <td class="text-center align-middle">
                                            <%# Container.ItemIndex + 1 %>
                                        </td>


                                        <td class="text-center align-middle">
                                            <%# Eval("khataNo") %>
                                        </td>


                                        <td class="text-center align-middle">
                                            <%# Eval("khesraNo") %>
                                        </td>


                                        <td class="text-center align-middle">
                                            <%# Eval("Rakba") %>
                                        </td>


                                        <td class="align-middle">
                                            <%# Eval("LandTypesInKhatianDesc") %>
                                        </td>


                                        <td class="align-middle">
                                            <div class="small land-details">
                                                <%# Eval("LandDetailsInKhatian") %>
                                            </div>
                                        </td>


                                        <td class="align-middle">
                                            <%# Eval("North_chauhaddee") %>
                                        </td>


                                        <td class="align-middle">
                                            <%# Eval("South_chauhaddee") %>
                                        </td>

                                        <td class="align-middle">
                                            <%# Eval("East_chauhaddee") %>
                                        </td>

                                        <td class="align-middle">
                                            <%# Eval("West_chauhaddee") %>
                                        </td>

                                    </tr>
                                </ItemTemplate>

                            </asp:Repeater>

                        </tbody>
                    </table>
                </div>

            </div>
        </div>

        <div class="card mb-3 shadow-sm">

            <div class="card-header bg-primary text-white font-weight-bold">
                वादी द्वारा प्रस्तुत साक्ष्य का विवरण
            </div>

            <div class="card-body">

                <div class="table-responsive">

                    <asp:Repeater ID="rptVadiEvidence" runat="server" OnItemCommand="rptVadiEvidence_ItemCommand">

                        <HeaderTemplate>
                            <div class="evidence-list">
                        </HeaderTemplate>

                        <ItemTemplate>

                            <div class="evidence-card">


                                <div class="evidence-number">
                                    साक्ष्य <%# Container.ItemIndex + 1 %>
                                </div>

                                <div class="row align-items-center">

                                    <div class="col-md-8 preview-field">

                                        <span class="preview-label">साक्ष्य का प्रकार : </span>

                                        <span class="preview-value">
                                            <%# Convert.ToString(Eval("evidence_id")) != "9" ? Eval("evidence_name")  : Eval("evidence_any_name") %>
                                        </span>

                                    </div>


                                    <div class="col-md-4 preview-field">

                                        <span class="preview-label">साक्ष्य का दस्तावेज : </span>

                                        <asp:ImageButton ID="imgVadiEvidence" runat="server" ImageUrl="~/images/pdf.gif" Width="40px" Height="40px" CssClass="evidence-pdf" CommandArgument='<%# Eval("FullfileName") %>' CommandName="View" Visible='<%# !string.IsNullOrWhiteSpace(Convert.ToString(Eval("FullfileName"))) %>' />

                                    </div>

                                </div>

                            </div>

                        </ItemTemplate>

                        <FooterTemplate>
                            </div>
                        </FooterTemplate>

                    </asp:Repeater>

                </div>

            </div>

        </div>

        <div class="card mb-3 shadow-sm">

            <div class="card-header bg-primary text-white font-weight-bold">
                प्रतिवादी द्वारा प्रस्तुत साक्ष्य का विवरण
            </div>

            <div class="card-body">

                <div class="table-responsive">

                    <asp:Repeater ID="rptPratiwadiEvidence" runat="server" OnItemCommand="rptPratiwadiEvidence_ItemCommand">

                        <HeaderTemplate>
                            <div class="evidence-list">
                        </HeaderTemplate>

                        <ItemTemplate>

                            <div class="evidence-card">

                                <!-- Evidence Number -->
                                <div class="evidence-number">
                                    साक्ष्य <%# Container.ItemIndex + 1 %>
                                </div>

                                <div class="row align-items-center">


                                    <div class="col-md-8 preview-field">

                                        <span class="preview-label">साक्ष्य का प्रकार : </span>

                                        <span class="preview-value">
                                            <%# Convert.ToString(Eval("evidence_id")) != "9" ? Eval("evidence_name") : Eval("evidence_any_name") %>
                                        </span>

                                    </div>


                                    <div class="col-md-4 preview-field">

                                        <span class="preview-label">साक्ष्य का दस्तावेज :  </span>

                                        <asp:ImageButton ID="imgPratiwadiEvidence" runat="server" ImageUrl="~/images/pdf.gif" Width="40px" Height="40px" CssClass="evidence-pdf" CommandArgument='<%# Eval("FullfileName") %>' CommandName="View" Visible='<%# !string.IsNullOrWhiteSpace(Convert.ToString(Eval("FullfileName"))) %>' />

                                    </div>

                                </div>

                            </div>

                        </ItemTemplate>

                        <FooterTemplate>
                            </div>
                        </FooterTemplate>

                    </asp:Repeater>

                </div>

            </div>

        </div>

        <div class="card mb-3 shadow-sm">

            <div class="card-header bg-primary text-white font-weight-bold">
                राजस्व अधिकारी / पुलिस पदाधिकारी / हल्का कर्मचारी द्वारा प्रस्तुत साक्ष्य का विवरण
            </div>

            <div class="card-body">


                <div class="row mb-3">

                    <div class="col-md-4">
                        पुलिस पदाधिकारी द्वारा समर्पित जाँच प्रतिवेदन की संक्षिप्त विवरणी
                    </div>

                    <div class="col-md-8" id="divPoliceAdhikari" runat="server">
                        <asp:Label ID="lblPoliceAdhikariVivarni" runat="server"></asp:Label>
                    </div>

                </div>


                <div class="row mb-3">

                    <div class="col-md-4 ">
                        हल्का कर्मचारी / राजस्व अधिकारी द्वारा समर्पित जाँच प्रतिवेदन की संक्षिप्त विवरणी
                    </div>

                    <div class="col-md-8" id="divHalkaKarmchariValue" runat="server">
                        <asp:Label ID="lblHalkaKarmchariVivarni" runat="server"></asp:Label>
                    </div>

                </div>

                <!-- Report Documents -->
                <div class="row mb-4">

                    <div class="col-md-6">

                        <div class="d-flex justify-content-between align-items-center border rounded p-2">

                            <span>पुलिस पदाधिकारी द्वारा समर्पित जाँच प्रतिवेदन का दस्तावेज </span>

                            <asp:ImageButton ID="lnkpulis_padadhikari_Patr_file" runat="server" ImageUrl="~/images/pdf.gif" Width="45" Height="45" CssClass="img-fluid getpdfdoc" path="display" Style="cursor: pointer;" />

                        </div>

                    </div>

                    <div class="col-md-6 mt-3 mt-md-0">

                        <div class="d-flex justify-content-between align-items-center border rounded p-2">

                            <span>हल्का कर्मचारी / राजस्व अधिकारी द्वारा समर्पित जाँच प्रतिवेदन का दस्तावेज
                            </span>

                            <asp:ImageButton ID="lnkfile_halkakarmchari_praptr" runat="server" ImageUrl="~/images/pdf.gif" Width="45" Height="45" CssClass="img-fluid getpdfdoc" path="display" Style="cursor: pointer;" />

                        </div>

                    </div>

                </div>


                <div class="row mb-3">

                    <div class="col-md-4 " id="div6" runat="server">
                        विवादित भू-खंड की मापी
                    </div>

                    <div class="col-md-8">
                        <asp:Label ID="lblVivaditBhukandKiMapiKaReasonHai" runat="server"></asp:Label>
                    </div>

                </div>


                <div class="row mb-3">

                    <div class="col-md-4 ">
                        मापी
                    </div>

                    <div class="col-md-8">
                        <asp:Label ID="lblMapiValue" runat="server"></asp:Label>
                    </div>

                </div>


                <div class="row mb-4">

                    <div class="col-md-4 ">
                        विवादित भू-खंड की मापी नहीं होने का कारण
                    </div>

                    <div class="col-md-8 mb-3" id="divVivaditBhukandKaMapiValue" runat="server">
                        <asp:Label ID="lblVivaditBhumiKaMapiNahiHoneKaKaran" runat="server"></asp:Label>
                    </div>

                    <div class="col-md-4 " id="div7" runat="server">
                        विवादित भू-खंड की मापी का प्रतिवेदन
                    </div>

                    <div class="col-md-8" id="div9" runat="server">

                        <asp:ImageButton ID="lnkfile_bhukand_prativedan" runat="server" ImageUrl="~/images/pdf.gif" Width="45" Height="45" CssClass="img-fluid getpdfdoc" path="display" Style="cursor: pointer;" />

                    </div>

                </div>

                <!-- Scheduled Date -->
                <div class="row">

                    <div class="col-md-4 ">
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

                <div class="row align-items-center mb-3">
                    <div class="col-md-3 ">
                        प्राथमिकी / अप्राथमिकी / सनहा दर्ज है ?
                    </div>

                    <div class="col-md-3">
                        <asp:Label ID="lblPrathamikHai" runat="server"></asp:Label>
                    </div>
                </div>

                <!-- Grid -->
                <asp:Repeater ID="rptBhumiVivAdIncident" runat="server">

                    <HeaderTemplate>
                        <div class="incident-list">
                    </HeaderTemplate>

                    <ItemTemplate>

                        <div class="incident-card">

                            <!-- Incident Header -->
                            <div class="incident-header">
                                घटना / वारदात- <%# Container.ItemIndex + 1 %>
                            </div>


                            <!-- Date and Short Description -->
                            <div class="row">

                                <div class="col-md-4 preview-field">

                                    <span class="preview-label">घटना की तिथि : </span>

                                    <span class="preview-value">
                                        <%# Eval("Ghatna_Vardat_date") %>
                                    </span>

                                </div>

                                <div class="col-md-8 preview-field">

                                    <span class="preview-label">घटना की संक्षिप्त विवरण :
                                    </span>

                                    <div class="preview-long-text">
                                        <%# Eval("Ghatna_Short_vivran") %>
                                    </div>

                                </div>

                            </div>


                            <!-- FIR Details -->
                            <div class="sub-section-title">
                                प्राथमिकी का विवरण
                            </div>

                            <div class="row">

                                <div class="col-md-4 preview-field">

                                    <span class="preview-label">प्राथमिकी दर्ज : </span>

                                    <span class="preview-value">
                                        <%# Eval("is_FIR_registered") %>
                                    </span>

                                </div>

                                <div class="col-md-4 preview-field">

                                    <span class="preview-label">प्राथमिकी संख्या : </span>

                                    <span class="preview-value">
                                        <%# Eval("praathamiki_sankhya") %>
                                    </span>

                                </div>

                                <div class="col-md-12 preview-field">

                                    <span class="preview-label">प्राथमिकी का विवरण : </span>

                                    <div class="preview-long-text">
                                        <%# Eval("praathamiki_ka_vivaran") %>
                                    </div>

                                </div>

                            </div>


                            <!-- Applicable Sections -->
                            <div class="sub-section-title">
                                धाराओं का विवरण
                            </div>

                            <div class="row">

                                <div class="col-md-3 preview-field">

                                    <span class="preview-label">धारा : </span>

                                    <span class="preview-value">
                                        <%# Eval("dhaara") %>
                                    </span>

                                </div>

                                <div class="col-md-3 preview-field">

                                    <span class="preview-label">BNS :  </span>

                                    <span class="preview-value">
                                        <%# Eval("bns") %>
                                    </span>

                                </div>

                                <div class="col-md-3 preview-field">

                                    <span class="preview-label">IPC धारा : </span>

                                    <span class="preview-value">
                                        <%# Eval("dhaaranew") %>
                                    </span>

                                </div>

                                <div class="col-md-3 preview-field">

                                    <span class="preview-label">BNS अन्य : </span>

                                    <span class="preview-value">
                                        <%# Eval("bns_oth") %>
                                    </span>

                                </div>

                            </div>

                            <div class="row">

                                <div class="col-md-6 preview-field">

                                    <span class="preview-label">IPC अन्य :
                                    </span>

                                    <span class="preview-value">
                                        <%# Eval("dhaara_oth") %>
                                    </span>

                                </div>

                            </div>


                            <!-- Aprathmiki Details -->
                            <div class="sub-section-title">
                                अप्राथमिकी का विवरण
                            </div>

                            <div class="row">

                                <div class="col-md-4 preview-field">

                                    <span class="preview-label">अप्राथमिकी दर्ज :   </span>

                                    <span class="preview-value">
                                        <%# Eval("is_complaint_filed") %>
                                    </span>

                                </div>

                                <div class="col-md-4 preview-field">

                                    <span class="preview-label">अप्राथमिकी संख्या :  </span>

                                    <span class="preview-value">
                                        <%# Eval("apraathamiki_sankhya") %>
                                    </span>

                                </div>

                                <div class="col-md-12 preview-field">

                                    <span class="preview-label">अप्राथमिकी का विवरण :   </span>

                                    <div class="preview-long-text">
                                        <%# Eval("apraathamiki_ka_vivaran") %>
                                    </div>

                                </div>

                            </div>

                            <div class="sub-section-title">
                                सनहा का विवरण
                            </div>

                            <div class="row">

                                <div class="col-md-4 preview-field">

                                    <span class="preview-label">सनहा दर्ज :   </span>

                                    <span class="preview-value">
                                        <%# Eval("is_Sanha_recorded") %>
                                    </span>

                                </div>

                                <div class="col-md-4 preview-field">

                                    <span class="preview-label">सनहा संख्या :  </span>

                                    <span class="preview-value">
                                        <%# Eval("sanha_sankhya") %>
                                    </span>

                                </div>

                                <div class="col-md-12 preview-field">

                                    <span class="preview-label">अभियुक्ति :  </span>

                                    <div class="preview-long-text">
                                        <%# Eval("Abhiyukt") %>
                                    </div>

                                </div>

                            </div>

                        </div>

                    </ItemTemplate>

                    <FooterTemplate>
                        </div>
                    </FooterTemplate>

                </asp:Repeater>

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
                            <asp:Label ID="lblPrakiriyadhinVadAvailable" runat="server" CssClass="font-weight-bold text-primary"> </asp:Label>
                        </div>

                    </div>


                    <div class="table-responsive">

                        <asp:Repeater ID="rptNyayalayVivran" runat="server">

                            <HeaderTemplate>
                                <div class="court-case-list">
                            </HeaderTemplate>

                            <ItemTemplate>

                                <div class="court-case-card">


                                    <div class="court-case-header">प्रक्रियाधीन वाद <%# Container.ItemIndex + 1 %>  </div>

                                    <div class="sub-section-title">न्यायालय का विवरण </div>

                                    <div class="row">

                                        <div class="col-md-4 preview-field">

                                            <span class="preview-label">न्यायालय : </span>

                                            <span class="preview-value">
                                                <%# Eval("court") %>
                                            </span>

                                        </div>

                                        <div class="col-md-4 preview-field">

                                            <span class="preview-label">न्यायालय का प्रकार :  </span>

                                            <span class="preview-value">
                                                <%# Eval("courtType") %>
                                            </span>

                                        </div>

                                        <div class="col-md-4 preview-field">

                                            <span class="preview-label">विभाग :  </span>

                                            <span class="preview-value">
                                                <%# Eval("Vibhag") %>
                                            </span>

                                        </div>

                                    </div>


                                    <div class="row">

                                        <div class="col-md-6 preview-field">

                                            <span class="preview-label">जिला :   </span>

                                            <span class="preview-value">
                                                <%# Eval("Dst") %>
                                            </span>

                                        </div>

                                        <div class="col-md-6 preview-field">

                                            <span class="preview-label">अनुमंडल :  </span>

                                            <span class="preview-value">
                                                <%# Eval("SubDiv") %>
                                            </span>

                                        </div>

                                    </div>

                                    <div class="row">

                                        <div class="col-md-6 preview-field">

                                            <span class="preview-label">वाद संख्या / वर्ष :   </span>

                                            <span class="preview-value">
                                                <%# Eval("vaadi_ki_vaad_sankhya_varsh") %>
                                            </span>

                                        </div>

                                    </div>


                                    <div class="sub-section-title">
                                        पक्षकारों का विवरण
                                    </div>

                                    <div class="row">

                                        <div class="col-md-6 preview-field">

                                            <span class="preview-label">वादी का नाम :  </span>

                                            <span class="preview-value">
                                                <%# Eval("vadi_name") %>
                                            </span>

                                        </div>

                                        <div class="col-md-6 preview-field">

                                            <span class="preview-label">प्रतिवादी का नाम :  </span>

                                            <span class="preview-value">
                                                <%# Eval("prativadi_name") %>
                                            </span>

                                        </div>

                                    </div>


                                    <div class="row">

                                        <div class="col-md-12 preview-field">

                                            <span class="preview-label">अद्यतन स्थिति का विवरण :  </span>

                                            <div class="preview-long-text">
                                                <%# Eval("vaad_ki_addhatan_sthiti_vivaran") %>
                                            </div>

                                        </div>

                                    </div>

                                </div>

                            </ItemTemplate>

                            <FooterTemplate>
                                </div>
                            </FooterTemplate>

                        </asp:Repeater>

                    </div>

                </div>

            </div>

        </div>

        <div class="card shadow-sm border-0 mb-3">

            <div class="card-header bg-primary text-white font-weight-bold">
                अंचलाधिकारी एवं थाना अध्यक्ष द्वारा भूमि विवाद के निराकरण हेतु कृत कार्रवाई की विवरणी
            </div>

            <div class="card-body">

                <div class="preview-section">


                    <div class="row">

                        <div class="col-md-6 preview-field">
                            <span class="preview-label">विवाद की संवेदनशीलता : </span>

                            <asp:Label ID="lblVivaadKiSanvedanasheelata" runat="server" CssClass="preview-value" />
                        </div>

                        <div class="col-md-6 preview-field">
                            <span class="preview-label">बैठक की तिथि :  </span>

                            <asp:Label ID="lblBaithakKiTithi" runat="server" CssClass="preview-value" />
                        </div>

                    </div>



                    <div class="row">

                        <div class="col-md-6 preview-field">
                            <span class="preview-label">क्या वादी उपस्थित है ? </span>

                            <asp:Label ID="lblkyaVaadeeUpasthitHai" runat="server" CssClass="preview-value" />
                        </div>

                        <div class="col-md-6 preview-field">
                            <span class="preview-label">क्या प्रतिवादी उपस्थित है ? </span>

                            <asp:Label ID="lblKyaPrativaadeeUpasthitHai" runat="server" CssClass="preview-value" />
                        </div>

                    </div>



                    <div class="row">

                        <div class="col-md-3 preview-field">
                            <span class="preview-label">बैठक का निष्कर्ष : </span>
                        </div>

                        <div class="col-md-9 preview-field">
                            <asp:Label ID="lblBaithakKaNishkarsh" runat="server" CssClass="preview-value preview-long-text" />
                        </div>

                    </div>


                    <div class="row">

                        <div class="row" id="divtithi" runat="server">

                            <asp:Label ID="lbltithi" runat="server" CssClass="preview-label" />

                            <asp:Label ID="lbltithivalue" runat="server" CssClass="preview-value" />

                        </div>

                    </div>


                    <div class="row" id="divAsveekrtiKaKaaranLabel" runat="server">

                        <div class="col-md-3 preview-field">
                            <span class="preview-label">अस्वीकृति का कारण :  </span>
                        </div>

                        <div class="col-md-9 preview-field">

                            <asp:Label ID="lblAsveekrtiKaKaaran" runat="server" CssClass="preview-value preview-long-text" />

                        </div>

                    </div>


                    <div class="row" id="divvadikavarsh" runat="server">

                        <div class="col-md-3 preview-field">
                            <span class="preview-label">वादी की वाद संख्या / वर्ष : </span>
                        </div>

                        <div class="col-md-9 preview-field">

                            <asp:Label ID="lblvadikaVadSankhyaVarsh" runat="server" CssClass="preview-value" />

                        </div>

                    </div>



                    <div class="row">

                        <div class="col-md-3 preview-field">
                            <span class="preview-label">बैठक में लिया गया निर्णय :  </span>
                        </div>

                        <div class="col-md-9 preview-field">

                            <asp:Label ID="lblBaithakMeinLiyaGayaNirnay" runat="server" CssClass="preview-value preview-long-text" />

                        </div>

                    </div>

                    <div class="row">

                        <div class="col-md-3 preview-field">
                            <span class="preview-label">अंचलाधिकारी का मंतव्य :  </span>
                        </div>

                        <div class="col-md-9 preview-field">

                            <asp:Label ID="lblAnchalaadhikaareeKaMantavy" runat="server" CssClass="preview-value preview-long-text" />

                        </div>

                    </div>

                    <div class="row">

                        <div class="col-md-3 preview-field">
                            <span class="preview-label">थानाध्यक्ष का मंतव्य :  </span>
                        </div>

                        <div class="col-md-9 preview-field">

                            <asp:Label ID="lblThaanaadhyakshKaMantavy" runat="server" CssClass="preview-value preview-long-text" />

                        </div>

                    </div>


                    <!-- Documents -->
                    <div class="preview-documents">

                        <!-- संयुक्त प्रतिवेदन -->
                        <div class="row">

                            <div class="col-md-4 preview-field">
                                <span class="preview-label">थानाध्यक्ष एवं अंचलाधिकारी का संयुक्त प्रतिवेदन : </span>
                            </div>

                            <div class="col-md-8 preview-field">

                                <asp:ImageButton ID="lnkJointDoc__letterOfIntent" runat="server" ImageUrl="~/images/pdf.gif" Width="45px" Height="45px" CssClass="preview-pdf" CommandArgument='<%# Eval("FullfileName") %>' ToolTip="संयुक्त प्रतिवेदन देखें" />

                            </div>

                        </div>


                        <div class="row">

                            <div class="col-md-4 preview-field">
                                <span class="preview-label">अंचलाधिकारी का मंतव्य पत्र :   </span>
                            </div>

                            <div class="col-md-8 preview-field">

                                <asp:ImageButton ID="lnkCircleOfficer_letterOfIntent" runat="server" ImageUrl="~/images/pdf.gif" Width="45px" Height="45px" CssClass="preview-pdf" CommandArgument='<%# Eval("FullfileName") %>' ToolTip="अंचलाधिकारी का मंतव्य पत्र देखें" />

                            </div>

                        </div>


                        <div class="row">

                            <div class="col-md-4 preview-field">
                                <span class="preview-label">थानाध्यक्ष का मंतव्य : </span>
                            </div>

                            <div class="col-md-8 preview-field">

                                <asp:ImageButton ID="lnkPoliceOfficer_letterOfIntent" runat="server" ImageUrl="~/images/pdf.gif" Width="45px" Height="45px" CssClass="preview-pdf" CommandArgument='<%# Eval("FullfileName") %>' ToolTip="थानाध्यक्ष का मंतव्य देखें" />

                            </div>

                        </div>

                    </div>

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

                    <div class="table-responsive">
                        <table class="table table-bordered table-striped table-hover mb-0 preview-table">

                            <thead class="table-primary text-center align-middle">
                                <tr>
                                    <th style="width: 5%;">क्र. सं.</th>
                                    <th style="width: 50%;">मंतव्य</th>
                                    <th style="width: 20%;">मंतव्य विवरण द्वारा</th>
                                    <th style="width: 10%;">दस्तावेज देखें</th>
                                </tr>
                            </thead>

                            <tbody>

                                <asp:Repeater ID="rptRemarks" runat="server" OnItemCommand="rptRemarks_ItemCommand">

                                    <ItemTemplate>
                                        <tr>

                                            <td class="text-center align-middle">
                                                <%# Container.ItemIndex + 1 %>
                                            </td>

                                            <td class="align-middle">
                                                <%# Eval("Remarks") %>
                                            </td>

                                            <td class="align-middle">
                                                <%# Eval("usernamee") %>
                                            </td>


                                            <td class="text-center align-middle">

                                                <asp:ImageButton ID="imgViewDocument" runat="server" ImageUrl="~/images/pdf.gif" Width="45px" Height="45px" CssClass="getpdfdoc" Style="cursor: pointer;" Visible='<%# CheckImage(Eval("Remarks_file")) %>' CommandArgument='<%# Eval("Remarks_file") %>' CommandName="View" AlternateText="दस्तावेज देखें" ToolTip="दस्तावेज देखें" />

                                            </td>

                                        </tr>
                                    </ItemTemplate>

                                </asp:Repeater>

                            </tbody>
                        </table>
                    </div>

                </div>

            </div>

        </div>
        <asp:UpdatePanel runat="server" ID="pnlupdate1" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="card shadow-sm mb-3">

                    <div class="card-header bg-primary text-white fw-bold">
                        >>> नई बैठक के अनुसार अंचलाधिकरी एवम्‌ थाना अध्यक्ष द्वारा भूमि विवाद के निराकरण हेतु कृत करवाई की विवरणी जोड़ें >>>
                    </div>
                    <div class="mt-3">
                        <asp:Label ID="lblMsg" runat="server" CssClass="fw-bold text-danger"> </asp:Label>
                    </div>
                    <asp:HiddenField ID="lastAction" runat="server" Value="0" />

                    <div class="card-body">


                        <div class="row g-3 align-items-center mb-3">

                            <div class="col-lg-3 col-md-6">
                                <label class="form-label fw-bold">
                                    भूमि विवाद की सवेदनशीलता<span class="text-danger">*</span>
                                </label>

                                <asp:DropDownList ID="ddlbhumivivadki_sanvedanshilta" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlbhumivivadki_sanvedanshilta_SelectedIndexChanged"></asp:DropDownList>

                            </div>

                            <div class="col-lg-3 col-md-6 text-center">

                                <asp:Image ID="onestar" runat="server" ImageUrl="~/images/1.png" Width="100" Visible="true" />

                                <asp:Image ID="twostar" runat="server" ImageUrl="~/images/2.png" Width="100" Visible="false" />

                                <asp:Image ID="threestar" runat="server" ImageUrl="~/images/3.png" Width="100" Visible="false" />

                                <asp:Image ID="fourstar" runat="server" ImageUrl="~/images/4.png" Width="100" Visible="false" />

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

                                <asp:DropDownList ID="ddlIsVadiAvailable" runat="server" CssClass="form-control">

                                    <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                    <asp:ListItem Value="Y">हां</asp:ListItem>
                                    <asp:ListItem Value="N">नहीं</asp:ListItem>

                                </asp:DropDownList>

                            </div>

                            <div class="col-lg-3 col-md-6">

                                <label class="form-label fw-bold">
                                    क्या प्रतिवादी उपस्थित है ? <span class="text-danger">*</span>
                                </label>

                                <asp:DropDownList ID="ddl_IsprativadiAvailable" runat="server" CssClass="form-control">

                                    <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                    <asp:ListItem Value="Y">हां</asp:ListItem>
                                    <asp:ListItem Value="N">नहीं</asp:ListItem>

                                </asp:DropDownList>

                            </div>

                            <div class="col-lg-3 col-md-6">

                                <label class="form-label fw-bold">
                                    बैठक का निष्कर्ष <span class="text-danger">*</span>
                                </label>

                                <asp:DropDownList ID="ddlaction" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlaction_SelectedIndexChanged">

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

                        <div class="row g-3">

                            <!-- संयुक्त प्रतिवेदन -->
                            <div class="col-lg-4 col-md-6">
                                <div class="upload-group">
                                    <label for="<%= LandDoc.ClientID %>" class="form-label fw-bold mb-2">
                                        संयुक्त प्रतिवेदन
                                    </label>

                                    <asp:FileUpload ID="LandDoc" runat="server" CssClass="form-control" accept=".pdf" />

                                    <asp:HiddenField ID="hdLandDoc" runat="server" />

                                    <small class="text-danger d-block text-end mt-1">.pdf प्रारूप में 2 MB तक में अपलोड करें
                                    </small>
                                </div>
                            </div>


                            <!-- अंचलाधिकारी का मंतव्य पत्र -->
                            <div class="col-lg-4 col-md-6">
                                <div class="upload-group">
                                    <label for="<%= CircleOfficer_letterOfIntent.ClientID %>" class="form-label fw-bold mb-2">
                                        अंचलाधिकारी का मंतव्य पत्र
                                    </label>

                                    <asp:FileUpload ID="CircleOfficer_letterOfIntent" runat="server" CssClass="form-control" accept=".pdf" />

                                    <asp:HiddenField ID="hdCircleOfficer_letterofintent" runat="server" />

                                    <small class="text-danger d-block text-end mt-1">.pdf प्रारूप में 2 MB तक में अपलोड करें
                                    </small>
                                </div>
                            </div>


                            <!-- थानाध्यक्ष का मंतव्य पत्र -->
                            <div class="col-lg-4 col-md-6">
                                <div class="upload-group">
                                    <label for="<%= PoliceOfficer_letterOfIntent.ClientID %>" class="form-label fw-bold mb-2">
                                        थानाध्यक्ष का मंतव्य पत्र
                                    </label>

                                    <asp:FileUpload ID="PoliceOfficer_letterOfIntent" runat="server" CssClass="form-control" accept=".pdf" />

                                    <asp:HiddenField ID="hdPoliceOfficer_letterOfIntent" runat="server" />

                                    <small class="text-danger d-block text-end mt-1">.pdf प्रारूप में 2 MB तक में अपलोड करें
                                    </small>
                                </div>
                            </div>

                            <!-- मापी का प्रतिवेदन -->

                            <div id="lastActionMapi" runat="server" visible="false" class="row align-items-start mb-3">

                                <div class="col-md-3 mb-2">
                                    <label for="<%= lastActionMapiKaPrativadan.ClientID %>" class="form-label fw-bold mb-1">मापी का प्रतिवेदन <span class="text-danger">*</span> </label>
                                </div>

                                <div class="col-md-3 mb-3">
                                    <asp:FileUpload ID="lastActionMapiKaPrativadan" runat="server" CssClass="form-control" accept=".pdf" />

                                    <asp:HiddenField ID="HiddenField1" runat="server" />

                                    <small class="text-danger d-block mt-1 upload-hint">Document केवल .pdf प्रारूप में 2 MB तक में अपलोड करें
                                    </small>
                                </div>


                                <!-- मापी के लिए निर्धारित तिथि -->
                                <div class="col-md-3 mb-2">
                                    <label for="<%= txtMapikiNirdharitThiti.ClientID %>" class="form-label fw-bold mb-1">मापी के लिए निर्धारित तिथि <span class="text-danger">*</span> </label>
                                </div>

                                <div class="col-md-3 mb-3">
                                    <asp:TextBox ID="txtMapikiNirdharitThiti" runat="server" CssClass="form-control"> </asp:TextBox>

                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtMapikiNirdharitThiti" Format="dd-MM-yyyy" OnClientDateSelectionChanged="checkDate" CssClass="zindex" />
                                </div>

                            </div>

                        </div>

                    </div>

                    <div class="card-footer text-center">

                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success me-2" OnClientClick="return SaveAnotherMetting();" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;

                        <asp:Button ID="btnCancel" runat="server" Text="Go Back" CssClass="btn btn-secondary me-2" OnClientClick="JavaScript:window.history.back(1); return true;" />

                        <%-- <asp:Button ID="btnDraft" runat="server" CssClass="btn btn-info" Text="Send To Draft" Visible="false" />--%>
                    </div>

                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
