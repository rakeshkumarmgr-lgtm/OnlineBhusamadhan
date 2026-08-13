<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EntryPage.aspx.cs" Inherits="Bhusamadhan.LandDispute.Entry.EntryPage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../../assets/css/cssSteps.css" rel="stylesheet" />
    <link href="../../assets/css/cssEntryPage.css" rel="stylesheet" />

    <script type="text/javascript">
        function checkDate(sender, args) {
            //if (sender._selectedDate > new Date()) {
            //    alert("You cannot select a day latter than today!");
            //    sender._selectedDate = new Date();
            //    // set the date back to the current date
            //    sender._textbox.set_Value("")
            //}
        }
        function dateValidate(evt) {
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
    <script>
        function saveIPCSelection() {
            var list = document.getElementById('<%= ddldhara1.ClientID %>');
            var selected = [];

            for (var i = 0; i < list.options.length; i++) {
                if (list.options[i].selected) {
                    selected.push(list.options[i].value);
                }
            }

            document.getElementById('<%= hdnSelectedIPC.ClientID %>').value = selected.join(',');
        }

        $(document).ready(function () {
            $('#<%= ddlbsn_dhara_hai.ClientID %>').on('change', function () {
                saveIPCSelection();
            });
        });
    </script>

    <style>
        .selected-dhara {
            display: inline-block;
            margin: 5px;
            padding: 5px 10px;
            background-color: #e0f7fa;
            border-radius: 4px;
        }

        .remove-cross {
            color: red;
            margin-left: 8px;
            text-decoration: none;
            font-weight: bold;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div class="container-fluid">

        <div class="card shadow-sm mb-3">

            <div class="card-header bg-primary text-white">
                <h5 class="mb-0">Application Entry
                </h5>
            </div>

            <div class="card-body">

                <ul class="wizard-steps">

                    <li>
                        <a id="hstep1" runat="server" class="step current">
                            <span class="step-no">1</span>
                            <span class="step-text">वादी और भूमि विवाद</span>
                        </a>
                    </li>

                    <li>
                        <a id="hstep2" runat="server" class="step disabled">
                            <span class="step-no">2</span>
                            <span class="step-text">प्रतिवादी और अन्य</span>
                        </a>
                    </li>

                    <li>
                        <a id="hstep3" runat="server" class="step disabled">
                            <span class="step-no">3</span>
                            <span class="step-text">खाता-खेसरा</span>
                        </a>
                    </li>

                    <li>
                        <a id="hstep4" runat="server" class="step disabled">
                            <span class="step-no">4</span>
                            <span class="step-text">वादी/प्रतिवादी का साक्ष्य</span>
                        </a>
                    </li>

                    <li>
                        <a id="hstep5" runat="server" class="step disabled">
                            <span class="step-no">5</span>
                            <span class="step-text">प्रस्तुत साक्ष्य</span>
                        </a>
                    </li>

                    <li>
                        <a id="hstep6" runat="server" class="step disabled">
                            <span class="step-no">6</span>
                            <span class="step-text">घटना एवं न्यायालय</span>
                        </a>
                    </li>

                    <li>
                        <a id="hstep7" runat="server" class="step disabled">
                            <span class="step-no">7</span>
                            <span class="step-text">अंचलाधिकारी एवं थानाध्यक्ष बैठक</span>
                        </a>
                    </li>

                </ul>

            </div>

        </div>

        <div class="row">
            <center>
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
            </center>
        </div>
        <div class="alert alert-info mb-3" runat="server" id="divDraftInfo" visible="false">
            <strong>Draft Application ID :</strong>
            <asp:Label ID="lblApplicationId" runat="server"></asp:Label>
        </div>

        <!-- Step-1  -->
        <asp:Panel ID="pnlStep1" runat="server">

            <div class="card mt-3">

                <div class="card-header bg-light">

                    <h5 class="mb-0">Step-1 : वादी एवं भूमि विवाद
                    </h5>

                </div>

                <div class="card-body">

                    <!-- Step-1 Controls -->

                    <asp:UpdatePanel runat="server" ID="pnlupdate1" UpdateMode="Conditional">
                        <ContentTemplate>

                            <div class="card section-card">

                                <%--<div class="section-header"><i class="fa fa-user"></i>व्यक्तिगत जानकारी </div>--%>

                                <div class="card-body section-body">

                                    <div class="form-row">

                                        <!-- वादी का नाम -->
                                        <div class="form-group col-md-3 mb-2">
                                            <label class="form-label">वादी का नाम <span class="required">*</span> </label>
                                            <asp:TextBox ID="txtNamePerAadhaar" runat="server" CssClass="form-control"
                                                placeholder="वादी का नाम" AutoComplete="off" oninput="this.value=this.value.toUpperCase();"></asp:TextBox>
                                            <%-- <asp:Label ID="DtxtNamePerAadhaar" runat="server" CssClass="form-control" Visible="false"> </asp:Label>--%>

                                            <asp:RequiredFieldValidator ID="rfv1" runat="server" CssClass="validator" ControlToValidate="txtNamePerAadhaar" ErrorMessage="वादी का नाम दर्ज करें।" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true" />

                                        </div>

                                        <!-- पिता/पति -->
                                        <div class="form-group col-md-3 mb-2">

                                            <label class="form-label">पिता / पति का नाम <span class="required">*</span> </label>

                                            <asp:TextBox ID="txtFName" runat="server" CssClass="form-control" placeholder="पिता / पति का नाम" AutoComplete="off" oninput="this.value=this.value.toUpperCase();"> </asp:TextBox>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" CssClass="validator" ControlToValidate="txtFName" ErrorMessage="पिता / पति का नाम दर्ज करें।" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true" />

                                        </div>

                                        <!-- Gender -->
                                        <div class="form-group col-md-3 mb-2">

                                            <label class="form-label">लिंग <span class="required">*</span> </label>

                                            <asp:DropDownList ID="ddlgender" runat="server" CssClass="form-control">

                                                <asp:ListItem Value="0">--चुनें--</asp:ListItem>
                                                <asp:ListItem Value="M">Male</asp:ListItem>
                                                <asp:ListItem Value="F">Female</asp:ListItem>
                                                <asp:ListItem Value="O">Other</asp:ListItem>

                                            </asp:DropDownList>
                                            <%--<asp:Label ID="Dddlgender" runat="server" CssClass="form-control" Visible="false"></asp:Label>--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" CssClass="validator" ControlToValidate="ddlgender" InitialValue="0" ErrorMessage="लिंग चुनें।" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true" />

                                        </div>

                                        <!-- Birth Year -->
                                        <div class="form-group col-md-3 mb-2">

                                            <label class="form-label">जन्म वर्ष </label>

                                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control"></asp:DropDownList>

                                            <%-- <asp:Label ID="Dtxtdatebirth" runat="server" CssClass="form-control" Visible="false"></asp:Label>--%>
                                        </div>

                                    </div>

                                    <div class="form-row">

                                        <!-- वादी का नाम -->
                                        <div class="form-group col-md-3 mb-2">
                                            <label class="font-weight-bold">मोबाइल नंबर<span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtvadimobile" runat="server" CssClass="form-control" MaxLength="10" placeholder="मोबाइल नंबर"></asp:TextBox>
                                            <asp:RegularExpressionValidator Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtvadimobile" ID="RegularExpressionValidator3"
                                                ValidationExpression="^[\s\S]{10,10}$" runat="server" ValidationGroup="1" ErrorMessage="10 numbers required."></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Enter मोबाइल नंबर..."
                                                ControlToValidate="txtvadimobile" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                </div>

                            </div>


                            <!-- ====================== Address Information ====================== -->
                            <div class="card section-card">

                                <%--<div class="section-header"><i class="fa fa-map-marker-alt"></i>पता विवरण </div>--%>

                                <div class="card-body section-body">

                                    <div class="form-row">

                                        <!-- District -->
                                        <div class="form-group col-md-3 mb-2">

                                            <label class="form-label">जिला <span class="required">*</span> </label>

                                            <asp:DropDownList ID="ddlUserDist" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlUserDist_SelectedIndexChanged"></asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="validator" ControlToValidate="ddlUserDist" InitialValue="0" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true" ErrorMessage="जिला चुनें।">
                                            </asp:RequiredFieldValidator>

                                        </div>

                                        <!-- Sub Division -->
                                        <div class="form-group col-md-3 mb-2">

                                            <label class="form-label">अनुमंडल <span class="required">*</span> </label>

                                            <asp:DropDownList ID="ddlUserSubdivision" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlUserSubdivision_SelectedIndexChanged">
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="validator" ControlToValidate="ddlUserSubdivision" InitialValue="0" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true" ErrorMessage="अनुमंडल चुनें।">
                                            </asp:RequiredFieldValidator>

                                        </div>

                                        <!-- Circle / Block -->
                                        <div class="form-group col-md-3 mb-2">

                                            <label class="form-label">अंचल <span class="required">*</span> </label>

                                            <asp:DropDownList ID="ddlUserBlock" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlUserBlock_SelectedIndexChanged">
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="validator" ControlToValidate="ddlUserBlock" InitialValue="0" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true" ErrorMessage="अंचल चुनें।">
                                            </asp:RequiredFieldValidator>

                                        </div>

                                        <!-- Police Station -->
                                        <div class="form-group col-md-3 mb-2">

                                            <label class="form-label">थाना <span class="required">*</span> </label>

                                            <asp:DropDownList ID="ddlUserThana" runat="server" CssClass="form-control">
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="validator" ControlToValidate="ddlUserThana" InitialValue="0" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true" ErrorMessage="थाना चुनें।">
                                            </asp:RequiredFieldValidator>

                                        </div>

                                    </div>

                                </div>

                            </div>


                            <!-- ====================== Area Information ====================== -->
                            <div class="card section-card">

                                <%--<div class="section-header"><i class="fa fa-map"></i>स्थानीय पता विवरण </div>--%>

                                <div class="card-body section-body">

                                    <!-- Row-1 -->
                                    <div class="form-row">

                                        <!-- Area Type -->
                                        <div class="form-group col-md-3 mb-2">

                                            <label class="form-label">क्षेत्र का प्रकार <span class="required">*</span> </label>

                                            <asp:DropDownList ID="ddlUserAreatype" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlUserAreatype_SelectedIndexChanged">

                                                <asp:ListItem Value="0" Text="--Select--" Enabled="true"></asp:ListItem>
                                                <asp:ListItem Value="R" Text="Rural" Enabled="true"></asp:ListItem>
                                                <asp:ListItem Value="U" Text="Urban" Enabled="true"></asp:ListItem>

                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="validator" ControlToValidate="ddlUserAreatype" InitialValue="0" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true" ErrorMessage="क्षेत्र का प्रकार चुनें।">
                                            </asp:RequiredFieldValidator>

                                        </div>

                                        <!-- Panchayat / Nagar Nikay -->
                                        <div class="form-group col-md-3 mb-2" id="divUserPanchyat" runat="server">

                                            <label class="form-label">

                                                <asp:Label ID="labUVillage" runat="server" Text="ग्राम पंचायत"> </asp:Label>
                                                <span class="required">*</span>

                                            </label>

                                            <asp:DropDownList ID="ddlUserPanchyat" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlUserPanchyat_SelectedIndexChanged">
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="validator" ControlToValidate="ddlUserPanchyat" InitialValue="0" ValidationGroup="1" Display="Dynamic" ErrorMessage="ग्राम पंचायत चुनें।"> </asp:RequiredFieldValidator>

                                        </div>

                                        <!-- Other Panchayat -->
                                        <div class="form-group col-md-3 mb-2" id="divUserPanchyat_Anya" runat="server" visible="false">

                                            <label class="form-label">अन्य पंचायत </label>

                                            <asp:TextBox ID="txtUserPanchyat_Anya" runat="server" CssClass="form-control" MaxLength="100" placeholder="यदि अन्य हो"></asp:TextBox>

                                        </div>

                                        <!-- Revenue Village -->
                                        <div class="form-group col-md-3 mb-2" id="divUserVillageCol" runat="server">

                                            <label class="form-label">राजस्व ग्राम <span class="required">*</span>  </label>

                                            <asp:DropDownList ID="ddlUserVillage" runat="server" CssClass="form-control"></asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" CssClass="validator" ControlToValidate="ddlUserVillage" InitialValue="0" ValidationGroup="1" Display="Dynamic" ErrorMessage="राजस्व ग्राम चुनें।">
                                            </asp:RequiredFieldValidator>

                                        </div>

                                    </div>

                                    <!-- Row-2 -->
                                    <div class="form-row">

                                        <!-- Other Village -->
                                        <div class="form-group col-md-3 mb-2" id="divUserVillage_Anya" runat="server" visible="false">

                                            <label class="form-label">अन्य ग्राम </label>

                                            <asp:TextBox ID="txtUserVillage_Anya" runat="server" CssClass="form-control" MaxLength="100"> </asp:TextBox>

                                        </div>

                                        <!-- Ward -->
                                        <div class="form-group col-md-3 mb-2" id="divUserWard" runat="server">

                                            <label class="form-label">वार्ड<span class="required">*</span> <span id="UWard" runat="server" visible="true"></span></label>

                                            <asp:DropDownList ID="ddlUserWard" runat="server" CssClass="form-control"></asp:DropDownList>

                                        </div>

                                        <!-- Other Ward -->
                                        <div class="form-group col-md-3 mb-2" id="divUserWard_Anya" runat="server" visible="false">

                                            <label class="form-label">अन्य वार्ड </label>

                                            <asp:TextBox ID="txtUserWard_Anya" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>

                                        </div>

                                        <!-- Mohalla -->
                                        <div class="form-group col-md-3 mb-2" id="divUserMohalla" runat="server" visible="false">

                                            <label class="form-label">मोहल्ला  <span class="required">*</span>   </label>

                                            <asp:TextBox ID="txtUserMohalla" runat="server" CssClass="form-control" MaxLength="30" placeholder="मोहल्ला का नाम" AutoComplete="off">
                                            </asp:TextBox>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" CssClass="validator" ControlToValidate="txtUserMohalla" ValidationGroup="1" Display="Dynamic" ErrorMessage="मोहल्ला का नाम दर्ज करें।">
                                            </asp:RequiredFieldValidator>

                                        </div>

                                    </div>

                                </div>

                            </div>
                            <!-- ============================================================= -->


                            <div class="card section-card">

                                <%--<div class="section-header"><i class="fa fa-building"></i>विभाग सम्बन्धी जानकारी </div>--%>

                                <div class="card-body section-body">

                                    <div class="form-row">

                                        <div class="form-group col-md-4">

                                            <label class="form-label">क्या वादी किसी विभाग का प्रतिनिधि है?<span class="required">*</span> </label>

                                            <asp:DropDownList ID="ddl_is_vadi_from_an_dept" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddl_is_vadi_from_an_dept_SelectedIndexChanged">

                                                <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                                <asp:ListItem Value="Y">हाँ</asp:ListItem>
                                                <asp:ListItem Value="N">नहीं</asp:ListItem>

                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" CssClass="validator" ControlToValidate="ddl_is_vadi_from_an_dept" InitialValue="0" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true" ErrorMessage="विभाग प्रतिनिधि चुनें।" />

                                        </div>


                                        <div class="form-group col-md-4" id="divWVibhag_details" runat="server" visible="false">

                                            <label class="form-label">विभाग का नाम <span class="required">*</span> </label>

                                            <asp:DropDownList ID="ddlWvibhaag_naam" runat="server" CssClass="form-control"></asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" CssClass="validator" ControlToValidate="ddlWvibhaag_naam" InitialValue="0" ValidationGroup="1" Display="Dynamic" ErrorMessage="विभाग का नाम चुनें।" />

                                        </div>


                                        <div class="form-group col-md-4" id="divWvibhaag_padanaam" runat="server" visible="false">

                                            <label class="form-label">विभाग में पदनाम </label>

                                            <asp:TextBox ID="txtWvibhaag_padanaam" runat="server" CssClass="form-control" placeholder="विभाग में पदनाम">
                                            </asp:TextBox>

                                        </div>

                                    </div>

                                    <div class="note-box">

                                        <strong>नोट :</strong> यदि विभाग की कोई जमीन है तो उस स्थिति में वादी विभाग के प्रतिनिधि होंगे।

                                    </div>

                                </div>

                            </div>

                            <div class="card section-card">

                                <%--  <div class="section-header bg-success"><i class="fa fa-university"></i>संस्था सम्बन्धी जानकारी </div>--%>

                                <div class="card-body section-body">

                                    <div class="form-row">

                                        <div class="form-group col-md-4">

                                            <label class="form-label">क्या वादी किसी संस्था का प्रतिनिधि है? <span class="required">*</span>  </label>

                                            <asp:DropDownList ID="ddl_is_vadi_from_an_org" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddl_is_vadi_from_an_org_SelectedIndexChanged">

                                                <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                                <asp:ListItem Value="Y">हाँ</asp:ListItem>
                                                <asp:ListItem Value="N">नहीं</asp:ListItem>

                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" CssClass="validator" ControlToValidate="ddl_is_vadi_from_an_org" InitialValue="0" ValidationGroup="1" Display="Dynamic" ErrorMessage="संस्था प्रतिनिधि चुनें।" />

                                        </div>

                                    </div>

                                    <div id="divWSanstha_details" runat="server" visible="false">

                                        <div class="form-row">

                                            <div class="form-group col-md-3">

                                                <label class="form-label">संस्था का प्रकार <span class="required">*</span></label>

                                                <asp:DropDownList ID="ddlWsanstha_naam" runat="server" CssClass="form-control"></asp:DropDownList>

                                            </div>


                                            <div class="form-group col-md-3">

                                                <label class="form-label">संस्था का सम्बन्ध<span class="required">*</span></label>

                                                <asp:DropDownList ID="ddlWsanshaanya_naam" runat="server" CssClass="form-control"></asp:DropDownList>

                                            </div>


                                            <div class="form-group col-md-3">

                                                <label class="form-label">संस्था का नाम <span class="required">*</span> </label>

                                                <asp:TextBox ID="txtWsanstha_naam" runat="server" CssClass="form-control" placeholder="संस्था का नाम" AutoComplete="off">
                                                </asp:TextBox>

                                            </div>


                                            <div class="form-group col-md-3">

                                                <label class="form-label">संस्था में पदनाम </label>

                                                <asp:TextBox ID="txtWsanstha_padanaam" runat="server" CssClass="form-control" placeholder="संस्था में पदनाम" AutoComplete="off">
                                                </asp:TextBox>

                                            </div>

                                        </div>

                                    </div>

                                    <!-- Save Button -->

                                    <div class="row mb-2">
                                        <div class="col-md-12 text-center">
                                            <asp:HiddenField ID="hfwadiprint" runat="server" />

                                            <asp:Button ID="btnAddVadiDetail" runat="server" Text="Save" CssClass="btn btn-primary" ValidationGroup="1" OnClick="btnAddVadiDetail_Click" />
                                        </div>
                                    </div>

                                    <!-- Repeater -->

                                    <div class="row mt-3">
                                        <div class="col-md-12">
                                            <div class="table-responsive">

                                                <asp:Repeater ID="rptWadi" runat="server" OnItemCommand="rptWadi_ItemCommand">

                                                    <HeaderTemplate>

                                                        <table class="table table-bordered table-striped table-hover table-sm mb-0">
                                                            <thead class="thead-dark text-center">
                                                                <tr>
                                                                    <th style="width: 70px;">Action</th>
                                                                    <th style="width: 50px;">#</th>
                                                                    <th>वादी का नाम</th>
                                                                    <th>पिता / पति का नाम</th>
                                                                    <th>लिंग</th>
                                                                    <th>मोबाइल</th>
                                                                    <th>जन्म वर्ष</th>
                                                                    <th>जिला</th>
                                                                    <th>अनुमंडल</th>
                                                                    <th>अंचल</th>
                                                                    <%--  <th>थाना</th>--%>
                                                                    <th>क्षेत्र</th>
                                                                    <th>ग्राम पंचायत</th>
                                                                    <th>राजस्व ग्राम</th>
                                                                    <th>वार्ड</th>

                                                                    <th>विभाग प्रतिनिधि</th>
                                                                    <th>संस्था प्रतिनिधि</th>
                                                                    <%-- <th>विभाग / संस्था</th>
                                                                    <th>पदनाम</th>--%>
                                                                </tr>
                                                            </thead>

                                                            <tbody>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>

                                                        <tr>

                                                            <td class="text-center">

                                                                <asp:LinkButton ID="btnDelete" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Remove" CommandArgument='<%# Container.ItemIndex %>' ToolTip="Delete Record" OnClientClick="return confirm('Are you sure you want to delete this record?');"> <i class="fa fa-trash"></i> </asp:LinkButton>

                                                            </td>

                                                            <td class="text-center">
                                                                <%# Container.ItemIndex + 1 %>
                                                            </td>

                                                            <td><%# Eval("vadi_Name") %></td>

                                                            <td><%# Eval("Vadi_Father_Husband_Name") %></td>

                                                            <td class="text-center">
                                                                <%# Eval("SexAsPerAadhaar").ToString() == "M" ? "पुरुष" : Eval("SexAsPerAadhaar").ToString() == "F" ? "महिला" : "अन्य" %>
                                                            </td>
                                                            <td class="text-center">
                                                                <%# Eval("Vadi_MobileNo") %>
                                                            </td>
                                                            <td class="text-center">
                                                                <%# Eval("YearOfBirthAsPerAadhaar") %> 

                                                            </td>

                                                            <td>
                                                                <%# Eval("DistrictName") %>

                                                            </td>

                                                            <td>
                                                                <%# Eval("SubDivisionName") %>

                                                            </td>

                                                            <td>
                                                                <%# Eval("BlockName") %>

                                                            </td>
                                                            <td>
                                                                <%# Eval("AreaTypeName") %>

                                                            </td>

                                                            <td>
                                                                <%# Eval("PanchayatName") %>

                                                            </td>

                                                            <td>
                                                                <%# Eval("VillageName") %>

                                                            </td>

                                                            <td>
                                                                <%# Eval("WardName") %>

                                                            </td>
                                                            <td>

                                                                <%# Eval("is_vadi_from_an_dept") %> 

                                                            </td>
                                                            <td>

                                                                <%# Eval("is_vadi_from_an_org") %>

                                                            </td>
                                                        </tr>

                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        </tbody>

                                                     </table>

                                                    </FooterTemplate>

                                                </asp:Repeater>

                                            </div>
                                        </div>
                                    </div>

                                    <!-- Repeater End -->

                                </div>

                            </div>

                            <%---------------------------------------------------------------%>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />

                </div>

            </div>
            <!-- ====================== भूमि विवाद का विवरण Section ====================== -->
            <div class="card section-card mt-3">

                <div class="section-header"><i class="fa fa-map-marker-alt mr-2"></i>भूमि विवाद का विवरण </div>

                <div class="card-body section-body">

                    <asp:UpdatePanel ID="UPBhumivivad_ka_vivarn" runat="server">
                        <ContentTemplate>
                            <!-- ==================== Location Information ==================== -->

                            <div class="row mb-3">
                                <div class="col-12">
                                    <h6 class="border-bottom pb-2 text-primary font-weight-bold"><i class="fa fa-map mr-1"></i>स्थान संबंधी जानकारी </h6>
                                </div>
                            </div>

                            <div class="form-row">

                                <!-- District -->

                                <div class="form-group col-md-3 mb-2">

                                    <label class="form-label">जिला <span class="required">*</span> </label>

                                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control"></asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" CssClass="validator" ControlToValidate="ddlDistrict" ValidationGroup="2" InitialValue="0" ErrorMessage="जिला चुनें।">  </asp:RequiredFieldValidator>

                                </div>


                                <!-- Subdivision -->

                                <div class="form-group col-md-3 mb-2">

                                    <label class="form-label">अनुमंडल <span class="required">*</span></label>

                                    <asp:DropDownList ID="ddlSubdivision" runat="server" CssClass="form-control"></asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" CssClass="validator" ValidationGroup="2" InitialValue="0" ControlToValidate="ddlSubdivision" ErrorMessage="अनुमंडल चुनें।"> </asp:RequiredFieldValidator>

                                </div>


                                <!-- Block -->

                                <div class="form-group col-md-3 mb-2">

                                    <label class="form-label">अंचल <span class="required">*</span> </label>

                                    <asp:DropDownList ID="ddlBlock" runat="server" CssClass="form-control"></asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" CssClass="validator" ValidationGroup="2" InitialValue="0" ControlToValidate="ddlBlock" ErrorMessage="अंचल चुनें।"> </asp:RequiredFieldValidator>

                                </div>


                                <!-- Police -->

                                <div class="form-group col-md-3 mb-2">

                                    <label class="form-label">थाना <span class="required">*</span> </label>

                                    <asp:DropDownList ID="ddlPolice" runat="server" CssClass="form-control"></asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" CssClass="validator" ValidationGroup="2" InitialValue="0" ControlToValidate="ddlPolice" ErrorMessage="थाना चुनें।"></asp:RequiredFieldValidator>

                                </div>

                            </div>



                            <div class="form-row">

                                <!-- Area Type -->

                                <div class="form-group col-md-3 mb-2">

                                    <label class="form-label">क्षेत्र का प्रकार <span class="required">*</span>  </label>

                                    <asp:DropDownList ID="ddlareatype" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlareatype_SelectedIndexChanged">

                                        <asp:ListItem Value="0">--चुनें--</asp:ListItem>
                                        <asp:ListItem Value="R">Rural</asp:ListItem>
                                        <asp:ListItem Value="U">Urban</asp:ListItem>

                                    </asp:DropDownList>

                                </div>


                                <!-- Panchayat -->

                                <div class="form-group col-md-3 mb-2" id="divPanchyat" runat="server">

                                    <label class="form-label">ग्राम पंचायत <span class="required">*</span> </label>

                                    <asp:DropDownList ID="ddlPanchyat" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPanchyat_SelectedIndexChanged">
                                    </asp:DropDownList>

                                </div>


                                <!-- Other Panchayat -->

                                <div class="form-group col-md-3 mb-2" id="divPanchyat_Anya" runat="server" visible="false">

                                    <label class="form-label">अन्य ग्राम पंचायत </label>

                                    <asp:TextBox ID="txtPanchyat_Anya" runat="server" CssClass="form-control"> </asp:TextBox>

                                </div>


                                <!-- Village -->

                                <div class="form-group col-md-3 mb-2" id="divVillage" runat="server">

                                    <label class="form-label">राजस्व ग्राम<span class="required">*</span> </label>

                                    <asp:DropDownList ID="ddlVillage" runat="server" CssClass="form-control"></asp:DropDownList>

                                </div>


                                <!-- Other Village -->

                                <div class="form-group col-md-3 mb-2" id="divVillage_Anya" runat="server" visible="false">

                                    <label class="form-label">अन्य राजस्व ग्राम</label>

                                    <asp:TextBox ID="txtVillage_Anya" runat="server" CssClass="form-control"> </asp:TextBox>

                                </div>


                                <!-- Ward -->

                                <div class="form-group col-md-3 mb-2" id="divWard" runat="server">

                                    <label class="form-label">वार्ड <span class="required">*</span> </label>

                                    <asp:DropDownList ID="ddlWard" runat="server" CssClass="form-control"></asp:DropDownList>

                                </div>


                                <!-- Other Ward -->

                                <div class="form-group col-md-3 mb-2" id="divWard_Anya" runat="server" visible="false">
                                    <label class="form-label">अन्य वार्ड </label>

                                    <asp:TextBox ID="txtWard_Anya" runat="server" CssClass="form-control"> </asp:TextBox>

                                </div>

                            </div>


                            <!-- ===================== भूमि संबंधी जानकारी ===================== -->

                            <div class="row mt-4">

                                <div class="col-12">
                                    <h6 class="border-bottom pb-2 mb-3 text-primary font-weight-bold"><i class="fa fa-globe mr-2"></i>भूमि संबंधी जानकारी </h6>

                                </div>

                                <!-- विवाद का अद्यतन कारक -->
                                <div class="col-md-3 mb-3">
                                    <label class="form-label">विवाद का अद्यतन कारक <span class="required">*</span> </label>

                                    <asp:DropDownList ID="ddl_vivad_adyatan_sthiti" runat="server" CssClass="form-control"></asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="ddl_vivad_adyatan_sthiti" InitialValue="0" ValidationGroup="2" CssClass="validator" Display="Dynamic" SetFocusOnError="true" ErrorMessage="विवाद का अद्यतन कारक चुनें।" />
                                </div>

                                <!-- राजस्व थाना संख्या -->
                                <div class="col-md-3 mb-3">

                                    <label class="form-label">राजस्व थाना संख्या </label>

                                    <asp:TextBox ID="txtrajaswa_sankhya" runat="server" CssClass="form-control" placeholder="राजस्व थाना संख्या"> </asp:TextBox>

                                </div>

                                <!-- भूमि का प्रकार -->
                                <div class="col-md-3 mb-3">

                                    <label class="form-label">भूमि का प्रकार <span class="required">*</span> </label>

                                    <asp:DropDownList ID="ddlbhumitype" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlbhumitype_SelectedIndexChanged"></asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="ddlbhumitype" InitialValue="0" ValidationGroup="2" CssClass="validator" Display="Dynamic" SetFocusOnError="true" ErrorMessage="भूमि का प्रकार चुनें।"> </asp:RequiredFieldValidator>

                                </div>

                                <!-- सरकारी भूमि का प्रकार -->
                                <div class="col-md-3 mb-3" id="divSarkaribhumitype" runat="server" visible="false">

                                    <label class="form-label">सरकारी भूमि का प्रकार <span class="required">*</span> </label>

                                    <asp:DropDownList ID="ddlsarkaribhumitype" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlsarkaribhumitype_SelectedIndexChanged"></asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlsarkaribhumitype" InitialValue="0" ValidationGroup="2" CssClass="validator" Display="Dynamic" SetFocusOnError="true" ErrorMessage="सरकारी भूमि का प्रकार चुनें।"> </asp:RequiredFieldValidator>

                                </div>

                                <!-- सरकारी भूमि प्रकार अन्य -->
                                <div class="col-md-3 mb-3" id="divsarkaribhumitype_Anya" runat="server" visible="false">

                                    <label class="form-label">सरकारी भूमि का प्रकार (यदि अन्य)<span class="required">*</span> </label>

                                    <asp:TextBox ID="txtsarkaribhumitype_Anya" runat="server" CssClass="form-control" MaxLength="100" placeholder="सरकारी भूमि का प्रकार"> </asp:TextBox>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtsarkaribhumitype_Anya" ValidationGroup="2" CssClass="validator" Display="Dynamic" SetFocusOnError="true" ErrorMessage="सरकारी भूमि का प्रकार दर्ज करें।"> </asp:RequiredFieldValidator>

                                </div>

                                <!-- भूमि विवाद का प्रकार -->
                                <div class="col-md-3 mb-3">

                                    <label class="form-label">भूमि विवाद का प्रकार <span class="required">*</span> </label>

                                    <asp:DropDownList ID="ddlbhumivivadtype" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlbhumivivadtype_SelectedIndexChanged"></asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="ddlbhumivivadtype" InitialValue="0" ValidationGroup="2" CssClass="validator" Display="Dynamic" SetFocusOnError="true" ErrorMessage="भूमि विवाद का प्रकार चुनें।"> </asp:RequiredFieldValidator>

                                </div>

                                <!-- भूमि विवाद अन्य -->
                                <div class="col-md-3 mb-3" id="divBhumivivad_Anya" runat="server" visible="false">

                                    <label class="form-label">भूमि विवाद का प्रकार (यदि अन्य)<span class="required">*</span></label>

                                    <asp:TextBox ID="txtbhumivivad_Anya" runat="server" CssClass="form-control" MaxLength="100" placeholder="भूमि विवाद का प्रकार"> </asp:TextBox>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txtbhumivivad_Anya" ValidationGroup="2" CssClass="validator" Display="Dynamic" SetFocusOnError="true" ErrorMessage="भूमि विवाद का प्रकार दर्ज करें।"> </asp:RequiredFieldValidator>

                                </div>

                            </div>

                            <!-- ===================== विवाद विवरण एवं दस्तावेज़ ===================== -->

                            <div class="row mt-4">

                                <div class="col-12">

                                    <h6 class="border-bottom pb-2 mb-3 text-primary font-weight-bold"><i class="fa fa-file-alt mr-2"></i>विवाद विवरण एवं दस्तावेज़ </h6>

                                </div>

                                <!-- आवेदन की तिथि -->

                                <div class="col-md-3 mb-3">

                                    <label class="form-label">आवेदन की तिथि <span class="required">*</span> </label>

                                    <asp:TextBox ID="txtAwadenKiTithi" runat="server" CssClass="form-control" placeholder="dd-MMM-yyyy">  </asp:TextBox>

                                    <cc1:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtAwadenKiTithi" Format="dd-MM-yyyy"></cc1:CalendarExtender>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="txtAwadenKiTithi" ValidationGroup="2" CssClass="validator" Display="Dynamic" SetFocusOnError="true" ErrorMessage="आवेदन की तिथि दर्ज करें।"> </asp:RequiredFieldValidator>

                                </div>

                            </div>

                            <!-- ===================== विवरण ===================== -->

                            <div class="row">

                                <!-- वादी विवरण -->

                                <div class="col-lg-6 mb-4">

                                    <label class="form-label">वादी द्वारा भूमि विवाद का संक्षिप्त विवरण <span class="required">*</span>  </label>

                                    <asp:TextBox ID="txtVadiVivarani" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="6" MaxLength="500" placeholder="अधिकतम 500 शब्द"> </asp:TextBox>

                                    <small class="text-muted">अधिकतम 500 शब्द </small>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ControlToValidate="txtVadiVivarani" ValidationGroup="2" CssClass="validator" Display="Dynamic" SetFocusOnError="true" ErrorMessage="वादी द्वारा विवाद का विवरण दर्ज करें।"> </asp:RequiredFieldValidator>

                                </div>

                                <!-- प्रतिवादी विवरण -->

                                <div class="col-lg-6 mb-4">

                                    <label class="form-label">प्रतिवादी द्वारा भूमि विवाद का संक्षिप्त विवरण </label>

                                    <asp:TextBox ID="txtPrativadiVivarani" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="6" MaxLength="500" placeholder="अधिकतम 500 शब्द"> </asp:TextBox>

                                    <small class="text-muted">अधिकतम 500 शब्द</small>

                                </div>

                            </div>

                            <!-- ===================== दस्तावेज़ ===================== -->

                            <div class="row">

                                <!-- वादी दस्तावेज़ -->

                                <div class="col-lg-6 mb-4">

                                    <label class="form-label">वादी द्वारा प्रस्तुत आवेदन </label>

                                    <asp:FileUpload ID="AppDoc" runat="server" CssClass="form-control-file border rounded p-2" />

                                    <small class="text-danger">केवल PDF (अधिकतम 3 MB) </small>

                                    <br />

                                    <a id="lnkAppDoc" runat="server" visible="false" class="btn btn-link p-0 mt-1"><i class="fa fa-file-pdf text-danger"></i>दस्तावेज़ देखें </a>

                                </div>

                                <!-- प्रतिवादी दस्तावेज़ -->

                                <div class="col-lg-6 mb-4">

                                    <label class="form-label">प्रतिवादी द्वारा प्रस्तुत आवेदन </label>

                                    <asp:FileUpload ID="PrativadiDoc" runat="server" accept=".pdf" CssClass="form-control-file border rounded p-2" />

                                    <small class="text-danger">केवल PDF (अधिकतम 3 MB) </small>

                                    <br />

                                    <a id="lnkPrativadiDoc" runat="server" visible="false" class="btn btn-link p-0 mt-1"><i class="fa fa-file-pdf text-danger"></i>दस्तावेज़ देखें </a>

                                </div>

                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>

            </div>


        </asp:Panel>


        <!-- Step-2  -->
        <asp:Panel ID="pnlStep2" runat="server" Visible="false">

            <div class="section-card">

                <div class="card-header bg-light">

                    <h5 class="mb-0">Step-2 : प्रतिवादी और अन्य </h5>

                </div>

                <div class="section-body">

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                        <ContentTemplate>

                            <div class="section-card">

                                <div class="section-header">प्रतिवादी का विवरण </div>

                                <div class="section-body">

                                    <!-- Row 1 -->
                                    <div class="row">

                                        <div class="col-lg-3 col-md-6 mb-3">
                                            <label class="form-label">प्रतिवादी का नाम <span class="required">*</span> </label>

                                            <asp:TextBox ID="txtPName" runat="server" CssClass="form-control" placeholder="प्रतिवादी का नाम" AutoComplete="off" oninput="this.value=this.value.toUpperCase();">
                                            </asp:TextBox>

                                            <asp:RequiredFieldValidator runat="server" CssClass="validator" ControlToValidate="txtPName" ValidationGroup="PratiVadi" Display="Dynamic" ErrorMessage="प्रतिवादी का नाम आवश्यक है">  </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="col-lg-3 col-md-6 mb-3">
                                            <label class="form-label">पिता / पति का नाम <span class="required">*</span> </label>

                                            <asp:TextBox ID="txtPFName" runat="server" CssClass="form-control" placeholder="पिता / पति का नाम" AutoComplete="off" oninput="this.value=this.value.toUpperCase();">
                                            </asp:TextBox>
                                        </div>

                                        <div class="col-lg-3 col-md-6 mb-3">
                                            <label class="form-label">जिला  <span class="required">*</span>  </label>

                                            <asp:DropDownList ID="ddlPDistrict" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPDistrict_SelectedIndexChanged"></asp:DropDownList>
                                        </div>

                                        <div class="col-lg-3 col-md-6 mb-3">
                                            <label class="form-label">अनुमंडल <span class="required">*</span> </label>

                                            <asp:DropDownList ID="ddlPSubdivision" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPSubdivision_SelectedIndexChanged"></asp:DropDownList>
                                        </div>

                                    </div>

                                    <!-- Row 2 -->
                                    <div class="row">

                                        <div class="col-lg-3 col-md-6 mb-3">
                                            <label class="form-label">अंचल  <span class="required">*</span> </label>

                                            <asp:DropDownList ID="ddlPBlock" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPBlock_SelectedIndexChanged"></asp:DropDownList>
                                        </div>

                                        <div class="col-lg-3 col-md-6 mb-3">
                                            <label class="form-label">थाना<span class="required">*</span> </label>

                                            <asp:DropDownList ID="ddlPThana" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                        </div>

                                        <div class="col-lg-3 col-md-6 mb-3">
                                            <label class="form-label">क्षेत्र का प्रकार<span class="required">*</span> </label>

                                            <asp:DropDownList ID="ddlPAreatype" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPAreatype_SelectedIndexChanged">

                                                <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                                <asp:ListItem Value="R">ग्रामीण</asp:ListItem>
                                                <asp:ListItem Value="U">शहरी</asp:ListItem>

                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-lg-3 col-md-6 mb-3" id="divPPanchyat" runat="server">

                                            <label class="form-label">ग्राम पंचायत <span class="required">*</span> </label>

                                            <asp:DropDownList ID="ddlPPanchyat" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPPanchyat_SelectedIndexChanged"></asp:DropDownList>

                                        </div>

                                    </div>

                                    <!-- Other Option Fields -->
                                    <div class="row">

                                        <div class="col-lg-3 col-md-6 mb-3" id="divPPanchyat_Anya" runat="server" visible="false">

                                            <label class="form-label">पंचायत (अगर अन्य है) <span class="required">*</span> </label>

                                            <asp:TextBox ID="txtPPanchyat_Anya" runat="server" CssClass="form-control" AutoComplete="off"> </asp:TextBox>

                                        </div>

                                        <div class="col-lg-3 col-md-6 mb-3" id="divPVillage_Anya" runat="server" visible="false">

                                            <label class="form-label">ग्राम (अगर अन्य है)<span class="required">*</span> </label>

                                            <asp:TextBox ID="txtPVillage_Anya" runat="server" CssClass="form-control" AutoComplete="off">  </asp:TextBox>

                                        </div>

                                        <div class="col-lg-3 col-md-6 mb-3" id="divPWard_Anya" runat="server" visible="false">

                                            <label class="form-label">वार्ड (अगर अन्य है) <span class="required">*</span> </label>

                                            <asp:TextBox ID="txtPWard_Anya" runat="server" CssClass="form-control" AutoComplete="off"> </asp:TextBox>

                                        </div>

                                    </div>

                                    <!-- Row 4 -->
                                    <div class="row">

                                        <div class="col-lg-3 col-md-6 mb-3" id="divPVillageCol" runat="server">

                                            <label class="form-label">राजस्व ग्राम </label>

                                            <asp:DropDownList ID="ddlPVillage" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPVillage_SelectedIndexChanged"></asp:DropDownList>

                                        </div>

                                        <div class="col-lg-3 col-md-6 mb-3" id="divPWard" runat="server">

                                            <label class="form-label">वार्ड <span class="required">*</span> </label>

                                            <asp:DropDownList ID="ddlPWard" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPWard_SelectedIndexChanged"></asp:DropDownList>

                                        </div>

                                        <div class="col-lg-3 col-md-6 mb-3" id="divPMohalla" runat="server" visible="false">

                                            <label class="form-label">मोहल्ला </label>

                                            <asp:TextBox ID="txtPMohalla" runat="server" CssClass="form-control" MaxLength="100" AutoComplete="off" placeholder="मोहल्ला">
                                            </asp:TextBox>

                                        </div>

                                        <div class="col-lg-3 col-md-6 mb-3">

                                            <label class="form-label">मोबाइल नंबर </label>

                                            <asp:TextBox ID="txtprativadi_Mobile" runat="server" CssClass="form-control" MaxLength="10" placeholder="मोबाइल नंबर"> </asp:TextBox>

                                        </div>

                                    </div>

                                </div>

                            </div>

                            <!-- Department Details -->
                            <div class="section-card">

                                <div class="section-header">विभाग का विवरण  </div>

                                <div class="section-body">

                                    <div class="row">

                                        <div class="col-md-4 mb-3">
                                            <label class="form-label">क्या प्रतिवादी किसी विभाग का प्रतिनिधि है?  <span class="required">*</span> </label>

                                            <asp:DropDownList ID="ddl_is_pratiVadi_from_an_dept" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_is_pratiVadi_from_an_dept_SelectedIndexChanged">

                                                <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                                <asp:ListItem Value="Y">हाँ</asp:ListItem>
                                                <asp:ListItem Value="N">नहीं</asp:ListItem>

                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator46" runat="server" CssClass="validator" ControlToValidate="ddl_is_pratiVadi_from_an_dept" InitialValue="0" ValidationGroup="PratiVadi" Display="Dynamic" ErrorMessage="कृपया विभाग प्रतिनिधि चुनें।"> </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="col-md-4 mb-3" id="divPVibhag_details" runat="server" visible="false">

                                            <label class="form-label">
                                                विभाग का नाम  <span class="required">*</span>
                                            </label>

                                            <asp:DropDownList ID="ddlPvibhaag_naam" runat="server" CssClass="form-control"></asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator44" runat="server" CssClass="validator" ControlToValidate="ddlPvibhaag_naam" InitialValue="0" ValidationGroup="PratiVadi" Display="Dynamic" ErrorMessage="कृपया विभाग का नाम चुनें।">  </asp:RequiredFieldValidator>

                                        </div>

                                        <div class="col-md-4 mb-3" id="divPVibhag_details2" runat="server" visible="false">

                                            <label class="form-label">विभाग में पदनाम </label>

                                            <asp:TextBox ID="txtPvibhaag_padanaam" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="100" placeholder="विभाग में पदनाम">
                                            </asp:TextBox>

                                        </div>

                                    </div>

                                    <div class="note-box mt-2">
                                        <strong>नोट :</strong> यदि विभाग की कोई जमीन है, तो उस स्थिति में वादी विभाग के प्रतिनिधि होंगे।
                                    </div>

                                </div>

                            </div>

                            <!-- Organization Details -->
                            <div class="section-card mt-4">

                                <div class="section-header">संस्था का विवरण  </div>

                                <div class="section-body">

                                    <div class="row">

                                        <div class="col-md-6 mb-3">

                                            <label class="form-label">क्या प्रतिवादी किसी संस्था का प्रतिनिधि है? <span class="required">*</span>  </label>

                                            <asp:DropDownList ID="ddl_is_pratiVadi_from_an_org" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_is_pratiVadi_from_an_org_SelectedIndexChanged">

                                                <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                                <asp:ListItem Value="Y">हाँ</asp:ListItem>
                                                <asp:ListItem Value="N">नहीं</asp:ListItem>

                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator47" runat="server" CssClass="validator" ControlToValidate="ddl_is_pratiVadi_from_an_org" InitialValue="0" ValidationGroup="PratiVadi" Display="Dynamic" ErrorMessage="कृपया संस्था प्रतिनिधि चुनें।"> </asp:RequiredFieldValidator>

                                        </div>

                                    </div>

                                </div>

                            </div>


                            <div id="divPSanstha_details" runat="server" visible="false" class="row mb-2 text-white" style="background-color: lightseagreen">
                                <div class="col-md-3 mb-2 p-1">

                                    <label class="form-label">संस्था का प्रकार<span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="ddlPsanstha_naam" runat="server" CssClass="form-control" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server" ErrorMessage="select संस्था का प्रकार..."
                                        ControlToValidate="ddlPsanstha_naam" SetFocusOnError="true" Display="Dynamic" ValidationGroup="PratiVadi" InitialValue="0">संस्था का प्रकार</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3 mb-2 p-1">

                                    <label class="form-label">संस्था का सम्बन्ध<span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="ddlPsanshaanya_naam" runat="server" CssClass="form-control" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator45" runat="server" ErrorMessage="select संस्था का प्रकार..."
                                        ControlToValidate="ddlPsanshaanya_naam" SetFocusOnError="true" Display="Dynamic" ValidationGroup="PratiVadi" InitialValue="0">संस्था का प्रकार</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3 mb-2 p-1">

                                    <label class="form-label">संस्था का नाम<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtPsanstha_naam" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="100" placeholder="संस्था का नाम"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator49" runat="server" ErrorMessage="select संस्था का नाम..."
                                        ControlToValidate="txtPsanstha_naam" SetFocusOnError="true" Display="Dynamic" ValidationGroup="PratiVadi"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3 mb-2 p-1">

                                    <label class="form-label">संस्था में पदनाम<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtPsanstha_padanaam" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="100" placeholder="संस्था में पदनाम"></asp:TextBox>
                                </div>
                            </div>

                            <!-- Save Button -->

                            <div class="row mb-2">
                                <div class="col-md-12 text-center">

                                    <asp:Button ID="btnAddPratiVadiDetail" runat="server" Text="Save" CssClass="btn btn-primary" ValidationGroup="PratiVadi" OnClick="btnAddPratiVadiDetail_Click" />
                                </div>
                            </div>

                            <!-- Repeater -->

                            <div class="row mt-3">
                                <div class="col-md-12">
                                    <div class="table-responsive">

                                        <asp:Repeater ID="Pratiwadi_repeater" runat="server" OnItemCommand="Pratiwadi_repeater_ItemCommand">

                                            <HeaderTemplate>

                                                <table class="table table-bordered table-striped table-hover table-sm mb-0">
                                                    <thead class="thead-dark text-center">
                                                        <tr>
                                                            <th style="width: 70px;">Action</th>
                                                            <th style="width: 50px;">#</th>
                                                            <th>प्रतिवादी का नाम</th>
                                                            <th>पिता / पति का नाम</th>

                                                            <th>मोबाइल</th>

                                                            <th>जिला</th>
                                                            <th>अनुमंडल</th>
                                                            <th>अंचल</th>

                                                            <th>क्षेत्र</th>
                                                            <th>ग्राम पंचायत</th>
                                                            <th>राजस्व ग्राम</th>
                                                            <th>वार्ड</th>

                                                            <th>विभाग प्रतिनिधि</th>
                                                            <th>संस्था प्रतिनिधि</th>
                                                        </tr>
                                                    </thead>

                                                    <tbody>
                                            </HeaderTemplate>

                                            <ItemTemplate>

                                                <tr>

                                                    <td class="text-center">

                                                        <asp:LinkButton ID="btnDelete" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Remove" CommandArgument='<%# Container.ItemIndex %>' ToolTip="Delete Record" OnClientClick="return confirm('Are you sure you want to delete this record?');"> <i class="fa fa-trash"></i> </asp:LinkButton>

                                                    </td>

                                                    <td class="text-center">
                                                        <%# Container.ItemIndex + 1 %>
                                                    </td>

                                                    <td><%# Eval("pratiVadi_Name") %></td>

                                                    <td><%# Eval("pratiVadi_Father_Husband_Name") %></td>


                                                    <td class="text-center">
                                                        <%# Eval("pratiVadi_MobileNo") %>
                                                    </td>


                                                    <td>
                                                        <%# Eval("DistrictName") %>

                                                    </td>

                                                    <td>
                                                        <%# Eval("SubDivisionName") %>

                                                    </td>

                                                    <td>
                                                        <%# Eval("BlockName") %>

                                                    </td>
                                                    <td>
                                                        <%# Eval("AreaTypeName") %>

                                                    </td>

                                                    <td>
                                                        <%# Eval("PanchayatName") %>

                                                    </td>

                                                    <td>
                                                        <%# Eval("VillageName") %>

                                                    </td>

                                                    <td>
                                                        <%# Eval("WardName") %>

                                                    </td>
                                                    <td>

                                                        <%# Eval("is_pratiVadi_from_an_dept") %> 

                                                    </td>
                                                    <td>

                                                        <%# Eval("is_pratiVadi_from_an_org") %>

                                                    </td>
                                                </tr>

                                            </ItemTemplate>

                                            <FooterTemplate>
                                                </tbody>

                  </table>

                                            </FooterTemplate>

                                        </asp:Repeater>

                                    </div>
                                </div>
                            </div>

                            <!-- Repeater End -->

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>

            <!-- Other Details -->
            <div class="section-card mt-4">
                <div class="section-header">अन्य विवरण</div>

                <div class="section-body">

                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>

                            <div class="row">

                                <!-- Notice Sent -->
                                <div class="col-md-4 mb-4">

                                    <label class="form-label">प्रतिवादी को सूचित किया गया है या नहीं? <span class="required">*</span> </label>

                                    <asp:DropDownList ID="ddlwadi_pratiwadi_sunwai" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlwadi_pratiwadi_sunwai_SelectedIndexChanged">

                                        <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                        <asp:ListItem Value="Y">हाँ</asp:ListItem>
                                        <asp:ListItem Value="N">नहीं</asp:ListItem>

                                    </asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator48" runat="server" CssClass="validator" ControlToValidate="ddlwadi_pratiwadi_sunwai" InitialValue="0" ValidationGroup="3" Display="Dynamic" SetFocusOnError="true" ErrorMessage="कृपया विकल्प चुनें।">
                                    </asp:RequiredFieldValidator>

                                </div>

                                <!-- Mode -->
                                <div class="col-md-4 mb-4">

                                    <asp:Label ID="labNotice" runat="server" Text="सूचना का माध्यम" CssClass="form-label">  </asp:Label>
                                    <asp:DropDownList ID="ddlKiskeduwara_bhejagaya" runat="server" CssClass="form-control" Visible="false">

                                        <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                        <asp:ListItem Value="Telephone">दूरभाष के माध्यम से</asp:ListItem>
                                        <asp:ListItem Value="Watchman">चौकीदार के माध्यम से</asp:ListItem>
                                        <asp:ListItem Value="Letter">पत्र के माध्यम से</asp:ListItem>
                                        <asp:ListItem Value="Other">अन्य</asp:ListItem>

                                    </asp:DropDownList>

                                    <asp:TextBox ID="txtsunwaiHetuNoticKaKaran" runat="server" CssClass="form-control" TextMode="MultiLine" MaxLength="500" Visible="false"> </asp:TextBox>

                                    <div id="div_sunwaiHetuNoticKaKaran" runat="server" visible="false" class="text-end mt-1">

                                        <small class="text-muted">अधिकतम 500 वर्ण </small>

                                    </div>

                                </div>

                            </div>
                            <div class="row">
                                <!-- Notice Served -->
                                <div id="divSuchana_ka_tamila" runat="server" visible="false" class="col-md-4 mb-4">

                                    <label class="form-label">प्रतिवादी को सूचना तामिला प्राप्त है या नहीं? <span class="required">*</span> </label>

                                    <asp:DropDownList ID="ddlSuchana_ka_tamila" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSuchana_ka_tamila_SelectedIndexChanged">

                                        <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                        <asp:ListItem Value="Y">हाँ</asp:ListItem>
                                        <asp:ListItem Value="N">नहीं</asp:ListItem>

                                    </asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator50" runat="server" CssClass="validator" ControlToValidate="ddlSuchana_ka_tamila" InitialValue="0" Display="Dynamic" SetFocusOnError="true" ValidationGroup="3" ErrorMessage="कृपया विकल्प चुनें।"> </asp:RequiredFieldValidator>

                                </div>

                                <!-- Presence -->
                                <div id="divSuchana_ka_upasthiti" runat="server" visible="false" class="col-md-4 mb-4">

                                    <label class="form-label">प्रतिवादी उपस्थित हुआ है या नहीं? <span class="required">*</span> </label>

                                    <asp:DropDownList ID="ddlSuchana_ka_upasthiti" runat="server" CssClass="form-control">

                                        <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                        <asp:ListItem Value="Y">हाँ</asp:ListItem>
                                        <asp:ListItem Value="N">नहीं</asp:ListItem>

                                    </asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator51" runat="server" CssClass="validator" ControlToValidate="ddlSuchana_ka_upasthiti" InitialValue="0" Display="Dynamic" SetFocusOnError="true" ValidationGroup="3" ErrorMessage="कृपया विकल्प चुनें।"> </asp:RequiredFieldValidator>

                                </div>

                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>

            </div>
        </asp:Panel>

        <!-- Step-3  -->

        <asp:Panel ID="pnlStep3" runat="server" Visible="false">

            <div class="card mt-3">

                <div class="card-header bg-light">
                    <h5>Step-3 : खाता-खेसरा</h5>
                </div>
                <div class="section-card">

                    <div class="section-header">खाता / खेसरा विवरण </div>

                    <div class="section-body">

                        <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="row">

                                    <!-- Khata Number -->
                                    <div class="col-lg-3 col-md-6 mb-3">

                                        <label class="form-label">खाता संख्या <span class="required">*</span>  </label>

                                        <asp:TextBox ID="txtkhatasankhya" runat="server" CssClass="form-control" AutoComplete="off"> </asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator52" runat="server" CssClass="validator" ControlToValidate="txtkhatasankhya" ValidationGroup="4" Display="Dynamic" SetFocusOnError="true" ErrorMessage="कृपया खाता संख्या दर्ज करें।"> </asp:RequiredFieldValidator>

                                    </div>

                                    <!-- Khesra Number -->
                                    <div class="col-lg-3 col-md-6 mb-3">

                                        <label class="form-label">खेसरा संख्या<span class="required">*</span> </label>

                                        <asp:TextBox ID="txtkhesarasankhya" runat="server" CssClass="form-control" AutoComplete="off"> </asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator53" runat="server" CssClass="validator" ControlToValidate="txtkhesarasankhya" ValidationGroup="4" Display="Dynamic" SetFocusOnError="true" ErrorMessage="कृपया खेसरा संख्या दर्ज करें।"> </asp:RequiredFieldValidator>

                                    </div>

                                </div>

                                <!-- Verification Link -->
                                <div class="mt-2">

                                    <a href="http://land.bihar.gov.in/Ror/RoR.aspx" target="_blank" class="text-primary font-weight-bold"><i class="fa fa-external-link-alt"></i>खाता-खेसरा सत्यापित करने के लिए यहाँ क्लिक करें </a>

                                </div>

                                <!-- Note -->
                                <div class="note-box mt-3"><strong>नोट :</strong>   यदि एक से अधिक खेसरा संख्या हो, तो उन्हें कॉमा (<strong>,</strong>) से अलग-अलग दर्ज करें। </div>

                                <%-- -------------------------------------------------%>
                                <!-- =================== Area Details =================== -->
                                <div class="section-card mb-4">

                                    <div class="section-header">रकबा का विवरण </div>

                                    <div class="section-body">

                                        <!-- Area 1 -->
                                        <div class="row align-items-end mb-3">

                                            <div class="col-lg-4 col-md-6">

                                                <label class="form-label">क्षेत्रफल (बड़ी इकाई) <span class="required">*</span> </label>

                                                <asp:TextBox ID="txtrakabasankhya1" runat="server" CssClass="form-control" MaxLength="15"> </asp:TextBox>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator54" runat="server" CssClass="validator" ValidationGroup="4" Display="Dynamic" ControlToValidate="txtrakabasankhya1" ErrorMessage="क्षेत्रफल दर्ज करें">  </asp:RequiredFieldValidator>

                                            </div>

                                            <div class="col-lg-4 col-md-6">

                                                <label class="form-label">यूनिट <span class="required">*</span> </label>

                                                <asp:DropDownList ID="ddlrakabaunit1" runat="server" CssClass="form-control"></asp:DropDownList>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator57" runat="server" CssClass="validator" ValidationGroup="4" InitialValue="0" Display="Dynamic" ControlToValidate="ddlrakabaunit1" ErrorMessage="यूनिट चुनें"> </asp:RequiredFieldValidator>

                                            </div>

                                        </div>

                                        <!-- Area 2 -->
                                        <div class="row align-items-end mb-3">

                                            <div class="col-lg-4 col-md-6">

                                                <label class="form-label">क्षेत्रफल (मध्यम इकाई)</label>

                                                <asp:TextBox ID="txtrakabasankhya2" runat="server" CssClass="form-control" MaxLength="15"> </asp:TextBox>

                                            </div>

                                            <div class="col-lg-4 col-md-6">

                                                <label class="form-label">यूनिट </label>

                                                <asp:DropDownList ID="ddlrakabaunit2" runat="server" CssClass="form-control"></asp:DropDownList>

                                            </div>

                                        </div>

                                        <!-- Area 3 -->
                                        <div class="row align-items-end mb-3">

                                            <div class="col-lg-4 col-md-6">

                                                <label class="form-label">क्षेत्रफल (सबसे छोटी इकाई) </label>

                                                <asp:TextBox ID="txtrakabasankhya3" runat="server" CssClass="form-control" MaxLength="15">  </asp:TextBox>

                                            </div>

                                            <div class="col-lg-4 col-md-6">

                                                <label class="form-label">यूनिट  </label>

                                                <asp:DropDownList ID="ddlrakabaunit3" runat="server" CssClass="form-control"></asp:DropDownList>

                                            </div>

                                        </div>

                                        <div class="note-box">

                                            <strong>नोट :</strong> सबसे बड़ी इकाई पहले दर्ज करें, फिर छोटी तथा अंत में सबसे छोटी इकाई दर्ज करें।
                                            
                                            <br />
                                            <strong>उदाहरण :</strong> 0 हेक्टेयर → 0 एकड़ → 1.5 डेसिमल
               
                                        </div>

                                        <hr class="my-4" />

                                        <div class="row">

                                            <div class="col-lg-4 col-md-6 mb-3">

                                                <label class="form-label">खतियान में जमीन की किस्म <span class="required">*</span>  </label>

                                                <asp:DropDownList ID="ddlkhatiyan_me_jaminvivran" runat="server" CssClass="form-control"></asp:DropDownList>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator60" runat="server" CssClass="validator" ValidationGroup="4" InitialValue="0" Display="Dynamic" ControlToValidate="ddlkhatiyan_me_jaminvivran" ErrorMessage="जमीन की किस्म चुनें"> </asp:RequiredFieldValidator>

                                            </div>

                                            <div class="col-lg-8 col-md-6 mb-3">

                                                <label class="form-label">खतियान में जमीन का विवरण </label>

                                                <asp:TextBox ID="txtkhatiyan_me_jaminvivran_text" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" MaxLength="500" placeholder="अधिकतम 500 शब्द"> </asp:TextBox>

                                            </div>

                                        </div>

                                    </div>

                                </div>

                                <!-- =================== Boundary Details =================== -->

                                <div class="section-card">

                                    <div class="section-header">चौहद्दी का विवरण </div>

                                    <div class="section-body">

                                        <div class="row">

                                            <div class="col-lg-3 col-md-6 mb-3">

                                                <label class="form-label">उत्तर</label>

                                                <asp:TextBox ID="txtuttari_chohaddi" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" MaxLength="100"> </asp:TextBox>

                                            </div>

                                            <div class="col-lg-3 col-md-6 mb-3">

                                                <label class="form-label">दक्षिण</label>

                                                <asp:TextBox ID="txtdakshini_chohaddi" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" MaxLength="100"> </asp:TextBox>

                                            </div>

                                            <div class="col-lg-3 col-md-6 mb-3">

                                                <label class="form-label">पूर्व</label>

                                                <asp:TextBox ID="txtpurvi_chohaddi" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" MaxLength="100"> </asp:TextBox>

                                            </div>

                                            <div class="col-lg-3 col-md-6 mb-3">

                                                <label class="form-label">पश्चिम</label>

                                                <asp:TextBox ID="txtpashchimi_chohaddi" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" MaxLength="100">  </asp:TextBox>

                                            </div>

                                        </div>

                                    </div>

                                </div>

                                <!-- Save Button -->

                                <div class="row mb-2">
                                    <div class="col-md-12 text-center">

                                        <asp:Button ID="btnsaveBhumiKaVivaran" runat="server" Text="Save" CssClass="btn btn-primary" ValidationGroup="4" OnClick="btnsaveBhumiKaVivaran_Click" />
                                    </div>
                                </div>

                                <!-- Repeater -->

                                <div class="row mt-3">
                                    <div class="col-md-12">
                                        <div class="table-responsive">

                                            <asp:Repeater ID="rptKhataKhesraVivarni" runat="server" OnItemCommand="rptKhataKhesraVivarni_ItemCommand">

                                                <HeaderTemplate>

                                                    <table class="table table-bordered table-striped table-hover table-sm mb-0">
                                                        <thead class="thead-dark text-center">
                                                            <tr>
                                                                <th style="width: 70px;">Action</th>
                                                                <th style="width: 50px;">#</th>
                                                                <th>खाता संख्या</th>
                                                                <th>खेसरा संख्या</th>

                                                                <th>रकबा</th>

                                                                <th>जमीन की किस्म</th>
                                                                <th>ख़तियन में जमीन का विवरण</th>
                                                                <th>उत्तर</th>

                                                                <th>दक्षिण</th>
                                                                <th>पूर्व</th>
                                                                <th>पश्चिम</th>

                                                            </tr>
                                                        </thead>

                                                        <tbody>
                                                </HeaderTemplate>

                                                <ItemTemplate>

                                                    <tr>

                                                        <td class="text-center">

                                                            <asp:LinkButton ID="btnDelete" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Remove" CommandArgument='<%# Container.ItemIndex %>' ToolTip="Delete Record" OnClientClick="return confirm('Are you sure you want to delete this record?');"> <i class="fa fa-trash"></i> </asp:LinkButton>

                                                        </td>

                                                        <td class="text-center">
                                                            <%# Container.ItemIndex + 1 %>
                                                        </td>

                                                        <td><%# Eval("khataNo") %></td>

                                                        <td><%# Eval("khesraNo") %></td>


                                                        <td class="text-center">
                                                            <%# Eval("Rakba") %>
                                                        </td>


                                                        <td>
                                                            <%# Eval("Landdesciption") %>

                                                        </td>

                                                        <td>
                                                            <%# Eval("LandDetailsInKhatian") %>

                                                        </td>

                                                        <td>
                                                            <%# Eval("North_chauhaddee") %>

                                                        </td>
                                                        <td>
                                                            <%# Eval("South_chauhaddee") %>

                                                        </td>

                                                        <td>
                                                            <%# Eval("East_chauhaddee") %>

                                                        </td>

                                                        <td>
                                                            <%# Eval("West_chauhaddee") %>

                                                        </td>

                                                    </tr>

                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    </tbody>

                                                    </table>

                                                </FooterTemplate>

                                            </asp:Repeater>

                                        </div>
                                    </div>
                                </div>

                                <!-- Repeater End -->
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

            </div>
        </asp:Panel>

        <!-- Step-4  -->

        <asp:Panel ID="pnlStep4" runat="server" Visible="false">

            <div class="card mt-3">

                <div class="card-header bg-light">
                    <h5>Step-4 : वादी/प्रतिवादी का साक्ष्य</h5>
                </div>

                <div class="section-card">

                    <div class="section-header">
                        वादी द्वारा प्रस्तुत साक्ष्य का विवरण
                    </div>

                    <div class="section-body">

                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">

                            <ContentTemplate>

                                <div class="row">

                                    <!-- Evidence Available -->
                                    <div class="col-lg-4 col-md-6 mb-3">

                                        <label class="form-label">वादी द्वारा साक्ष्य का दस्तावेज उपलब्ध है? <span class="required">*</span> </label>

                                        <asp:DropDownList ID="ddlIsVadiEvi" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlIsVadiEvi_SelectedIndexChanged">

                                            <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                            <asp:ListItem Value="Y">हाँ</asp:ListItem>
                                            <asp:ListItem Value="N">नहीं</asp:ListItem>

                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator62" runat="server" CssClass="validator" ControlToValidate="ddlIsVadiEvi" InitialValue="0" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true" ErrorMessage="कृपया विकल्प चुनें।"> </asp:RequiredFieldValidator>

                                    </div>

                                    <!-- Evidence Type -->
                                    <div class="col-lg-3 col-md-6 mb-3" id="divVadiEvidenceType" runat="server" visible="false">

                                        <label class="form-label">साक्ष्य का प्रकार  <span class="required">*</span> </label>

                                        <asp:DropDownList ID="ddlVadiEvidenceType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlVadiEvidenceType_SelectedIndexChanged"></asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator63" runat="server" CssClass="validator" ControlToValidate="ddlVadiEvidenceType" InitialValue="0" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true" ErrorMessage="साक्ष्य का प्रकार चुनें।">   </asp:RequiredFieldValidator>

                                    </div>

                                    <!-- Other Evidence -->
                                    <div class="col-lg-5 col-md-12 mb-3" id="divtxtVadiEvidenceType" runat="server" visible="false">

                                        <label class="form-label">अन्य होने पर दस्तावेज का नाम <span class="required">*</span> </label>

                                        <asp:TextBox ID="txtVadiEvidenceType" runat="server" CssClass="form-control" MaxLength="100" AutoComplete="off" placeholder="दस्तावेज का नाम"> </asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator64" runat="server" CssClass="validator" ControlToValidate="txtVadiEvidenceType" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true" ErrorMessage="दस्तावेज का नाम दर्ज करें।">  </asp:RequiredFieldValidator>

                                    </div>

                                </div>

                                <!-- File Upload -->

                                <div class="row" id="divvadi_dastavej" runat="server" visible="false">

                                    <div class="col-lg-6 col-md-12 mb-3">

                                        <label class="form-label">वादी द्वारा प्रस्तुत साक्ष्य का दस्तावेज</label>

                                        <asp:FileUpload ID="file_vadi_dastavej_new" runat="server" CssClass="form-control" accept=".pdf" />

                                        <small class="text-danger">केवल PDF (.pdf) फ़ाइल अपलोड करें (अधिकतम 3 MB)  </small>

                                        <br />

                                        <a id="lnkvadikashachhDoc" runat="server" class="btn btn-link p-0 mt-2 getpdfdoc" path="display" visible="false"><i class="fa fa-file-pdf text-danger"></i>अपलोड किया गया दस्तावेज़ देखें </a>

                                    </div>

                                </div>

                                <!-- Save Button -->

                                <div class="row mb-2">
                                    <div class="col-md-12 text-center">

                                        <asp:Button ID="btnAddVadiEvidenceDetail" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnAddVadiEvidenceDetail_Click" />
                                    </div>
                                </div>

                                <!-- Repeater -->

                                <div class="row mt-3">
                                    <div class="col-md-12">
                                        <div class="table-responsive">

                                            <asp:Repeater ID="rptVadiEvidence" runat="server" OnItemCommand="rptVadiEvidence_ItemCommand">

                                                <HeaderTemplate>

                                                    <table class="table table-bordered table-striped table-hover table-sm mb-0">
                                                        <thead class="thead-dark text-center">
                                                            <tr>
                                                                <th style="width: 70px;">Action</th>
                                                                <th style="width: 50px;">#</th>
                                                                <th>साक्ष्य का प्रकार</th>
                                                                <th>साक्ष्य का दस्तावेज</th>


                                                            </tr>
                                                        </thead>

                                                        <tbody>
                                                </HeaderTemplate>

                                                <ItemTemplate>

                                                    <tr>

                                                        <td class="text-center">

                                                            <asp:LinkButton ID="btnDelete" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Remove" CommandArgument='<%# Container.ItemIndex %>' ToolTip="Delete Record" OnClientClick="return confirm('Are you sure you want to delete this record?');"> <i class="fa fa-trash"></i> </asp:LinkButton>

                                                        </td>

                                                        <td class="text-center">
                                                            <%# Container.ItemIndex + 1 %>
                                                        </td>

                                                        <td>
                                                            <asp:Label ID="lblEvidenceType" runat="server" Text='<%# (Convert.ToString(Eval("evidence_id")) != "9")  ? Eval("evidence_name")  : Eval("evidence_any_name") %>'>  </asp:Label></td>

                                                        <td>
                                                            <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer;" CommandArgument='<%# Container.ItemIndex %>' CommandName="View" ToolTip="View Document" /></td>

                                                    </tr>

                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    </tbody>

                                                    </table>

                                                </FooterTemplate>

                                            </asp:Repeater>



                                        </div>
                                    </div>
                                </div>

                            </ContentTemplate>
                            <Triggers>

                                <asp:PostBackTrigger ControlID="btnAddVadiEvidenceDetail" />
                            </Triggers>

                        </asp:UpdatePanel>



                    </div>

                </div>

                <div class="section-card">

                    <div class="section-header">
                        प्रतिवादी द्वारा प्रस्तुत साक्ष्य का विवरण
                    </div>

                    <div class="section-body">

                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">

                            <ContentTemplate>

                                <div class="row">

                                    <!-- Evidence Available -->
                                    <div class="col-lg-4 col-md-6 mb-3">

                                        <label class="form-label">प्रतिवादी द्वारा साक्ष्य का दस्तावेज उपलब्ध है?  <span class="required">*</span>  </label>

                                        <asp:DropDownList ID="ddlIsPvadiEvi" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlIsPvadiEvi_SelectedIndexChanged">

                                            <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                            <asp:ListItem Value="Y">हाँ</asp:ListItem>
                                            <asp:ListItem Value="N">नहीं</asp:ListItem>

                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator65" runat="server" CssClass="validator" ControlToValidate="ddlIsPvadiEvi" InitialValue="0" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true" ErrorMessage="कृपया विकल्प चुनें।"> </asp:RequiredFieldValidator>

                                    </div>

                                    <!-- Evidence Type -->
                                    <div class="col-lg-3 col-md-6 mb-3" id="divPrativadiEvidence" runat="server" visible="false">

                                        <label class="form-label">साक्ष्य का प्रकार <span class="required">*</span> </label>

                                        <asp:DropDownList ID="ddlPrativadiEvidenceType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPrativadiEvidenceType_SelectedIndexChanged"></asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator66" runat="server" CssClass="validator" ControlToValidate="ddlPrativadiEvidenceType" InitialValue="0" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true" ErrorMessage="साक्ष्य का प्रकार चुनें।"> </asp:RequiredFieldValidator>

                                    </div>

                                    <!-- Other Evidence Name -->
                                    <div class="col-lg-5 col-md-12 mb-3" id="divtxtPrativadiEvidenceType" runat="server" visible="false">

                                        <label class="form-label">अन्य होने पर दस्तावेज का नाम <span class="required">*</span>  </label>

                                        <asp:TextBox ID="txtPrativadiEvidenceType" runat="server" CssClass="form-control" MaxLength="100" AutoComplete="off" placeholder="दस्तावेज का नाम">  </asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator67" runat="server" CssClass="validator" ControlToValidate="txtPrativadiEvidenceType" Display="Dynamic" ValidationGroup="5" SetFocusOnError="true" ErrorMessage="दस्तावेज का नाम दर्ज करें।"> </asp:RequiredFieldValidator>

                                    </div>

                                </div>

                                <!-- File Upload -->
                                <div class="row" id="divPrativadi_dastavej_new" runat="server" visible="false">

                                    <div class="col-lg-6 col-md-12 mb-3">

                                        <label class="form-label">प्रतिवादी द्वारा प्रस्तुत साक्ष्य का दस्तावेज  </label>

                                        <asp:FileUpload ID="file_Prativadi_dastavej_new" runat="server" CssClass="form-control" accept=".pdf" />

                                        <small class="text-danger">केवल PDF (.pdf) फ़ाइल अपलोड करें (अधिकतम 3 MB) </small>

                                        <br />

                                        <a id="lnkPrativadiKashachhDoc" runat="server" class="btn btn-link p-0 mt-2 getpdfdoc" path="display" visible="false"><i class="fa fa-file-pdf text-danger"></i>अपलोड किया गया दस्तावेज़ देखें </a>

                                    </div>

                                </div>

                                <!-- Save Button -->

                                <div class="row mb-2">
                                    <div class="col-md-12 text-center">

                                        <asp:Button ID="btnAddPrativadiEvidenceDetail" runat="server" Text="Save" CssClass="btn btn-primary" OnClientClick="return ValidatePrativadiEvidenceDetail();" OnClick="btnAddPrativadiEvidenceDetail_Click" />
                                    </div>
                                </div>

                                <!-- Repeater -->

                                <div class="row mt-3">
                                    <div class="col-md-12">
                                        <div class="table-responsive">


                                            <asp:Repeater ID="rptPrativadiEvidence" runat="server" OnItemCommand="rptPrativadiEvidence_ItemCommand">

                                                <HeaderTemplate>

                                                    <table class="table table-bordered table-striped table-hover table-sm mb-0">
                                                        <thead class="thead-dark text-center">
                                                            <tr>
                                                                <th style="width: 70px;">Action</th>
                                                                <th style="width: 50px;">#</th>
                                                                <th>साक्ष्य का प्रकार</th>
                                                                <th>साक्ष्य का दस्तावेज</th>


                                                            </tr>
                                                        </thead>

                                                        <tbody>
                                                </HeaderTemplate>

                                                <ItemTemplate>

                                                    <tr>

                                                        <td class="text-center">

                                                            <asp:LinkButton ID="btnDelete" runat="server" CssClass="btn btn-danger btn-sm" CommandName="Remove" CommandArgument='<%# Container.ItemIndex %>' ToolTip="Delete Record" OnClientClick="return confirm('Are you sure you want to delete this record?');"> <i class="fa fa-trash"></i> </asp:LinkButton>

                                                        </td>

                                                        <td class="text-center">
                                                            <%# Container.ItemIndex + 1 %>
                                                        </td>

                                                        <td>
                                                            <asp:Label ID="lblEvidenceType" runat="server" Text='<%# (Convert.ToString(Eval("evidence_id")) != "9")  ? Eval("evidence_name")  : Eval("evidence_any_name") %>'>  </asp:Label></td>

                                                        <td>
                                                            <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer;" CommandArgument='<%# Container.ItemIndex %>' CommandName="View" ToolTip="View Document" /></td>

                                                    </tr>

                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    </tbody>

                                                     </table>

                                                </FooterTemplate>

                                            </asp:Repeater>

                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>

                                <asp:PostBackTrigger ControlID="btnAddPrativadiEvidenceDetail" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>

                </div>

            </div>


        </asp:Panel>

        <!-- Step-5  -->

        <asp:Panel ID="pnlStep5" runat="server" Visible="false">

            <div class="card mt-3">

                <div class="card-header bg-light">
                    <h5>Step-5 : प्रस्तुत साक्ष्य</h5>
                </div>
                <div class="section-header">राजस्व अधिकारी / पुलिस पदाधिकारी / हल्का कर्मचारी द्वारा प्रस्तुत साक्ष्य का विवरण </div>

                <div class="section-body">

                    <asp:UpdatePanel ID="UPanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

                            <!-- Police Report -->
                            <div class="row mb-4">

                                <div class="col-md-6">
                                    <label class="form-label">पुलिस पदाधिकारी द्वारा समर्पित जाँच प्रतिवेदन की संक्षिप्त विवरणी  </label>

                                    <asp:TextBox ID="txtpulis_padadhikari_vivarani" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" MaxLength="500" placeholder="अधिकतम 500 शब्द"> </asp:TextBox>
                                </div>

                                <div class="col-md-6">
                                    <label class="form-label">पुलिस पदाधिकारी द्वारा समर्पित जाँच प्रतिवेदन का दस्तावेज </label>

                                    <asp:FileUpload ID="pulis_padadhikari_Patr_file" runat="server" CssClass="form-control" accept=".pdf" />

                                    <small class="text-danger">केवल PDF (अधिकतम 3 MB)</small>

                                    <br />

                                    <a id="lnkpulis_padadhikari_Patr_file" runat="server" visible="false" class="getpdfdoc" path="display" href="#">View Document </a>
                                </div>

                            </div>

                            <!-- Revenue Officer Report -->
                            <div class="row mb-4">

                                <div class="col-md-6">
                                    <label class="form-label">हल्का कर्मचारी / राजस्व अधिकारी द्वारा समर्पित जाँच प्रतिवेदन की संक्षिप्त विवरणी </label>

                                    <asp:TextBox ID="txthalkakarmchari_prativedan" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" MaxLength="500" placeholder="अधिकतम 500 शब्द"> </asp:TextBox>
                                </div>

                                <div class="col-md-6">
                                    <label class="form-label">
                                        हल्का कर्मचारी / राजस्व अधिकारी द्वारा समर्पित जाँच प्रतिवेदन का दस्तावेज<br />
                                        <br />
                                    </label>

                                    <asp:FileUpload ID="file_halkakarmchari_praptr" runat="server" CssClass="form-control" accept=".pdf" />

                                    <small class="text-danger">केवल PDF (अधिकतम 3 MB) </small>

                                    <br />

                                    <a id="lnkfile_halkakarmchari_praptr" runat="server" visible="false" class="getpdfdoc" path="display" href="#">View Document </a>
                                </div>

                            </div>

                            <!-- Land Measurement -->
                            <div class="row mb-3">

                                <div class="col-md-3" id="divbhukhand_mapi" runat="server">

                                    <label class="form-label">विवादित भू-खंड की मापी <span class="required">*</span> </label>

                                    <asp:DropDownList ID="ddlbhukhand_mapi" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlbhukhand_mapi_SelectedIndexChanged">

                                        <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                        <asp:ListItem Value="Y">मापी आवश्यक है</asp:ListItem>
                                        <asp:ListItem Value="N">मापी आवश्यक नहीं है</asp:ListItem>

                                    </asp:DropDownList>


                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator68" runat="server" CssClass="validator" ControlToValidate="ddlbhukhand_mapi" ValidationGroup="4" InitialValue="0" Display="Dynamic"> विवादित भू-खंड की मापी </asp:RequiredFieldValidator>

                                </div>

                                <div class="col-md-3" id="divbhukhand_Copy" runat="server" visible="false">

                                    <label class="form-label">मापी <span class="required">*</span> </label>

                                    <asp:DropDownList ID="ddlbhukhand_Copy" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlbhukhand_Copy_SelectedIndexChanged">

                                        <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                        <asp:ListItem Value="Y">मापी हुई है</asp:ListItem>
                                        <asp:ListItem Value="N">मापी नहीं हुई है</asp:ListItem>

                                    </asp:DropDownList>


                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator69" runat="server" CssClass="validator" ControlToValidate="ddlbhukhand_Copy" ValidationGroup="6" InitialValue="0" Display="Dynamic"> मापी  </asp:RequiredFieldValidator>

                                </div>

                                <div class="col-md-3" id="divMapiKeNirdharit_tithi" runat="server" visible="false">

                                    <label class="form-label">मापी के लिए निर्धारित तिथि <span class="required">*</span></label>

                                    <asp:TextBox ID="txtMapiKeNirdharit_tithi" runat="server" CssClass="form-control" MaxLength="10" placeholder="dd-MM-yyyy" AutoComplete="off" oncopy="return false" onpaste="return false" oncut="return false" onkeypress="return dateValidate(event)"></asp:TextBox>

                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtMapiKeNirdharit_tithi" Format="dd-MM-yyyy" CssClass="zindex"></cc1:CalendarExtender>

                                </div>

                                <div class="col-md-3" id="divBhukhandReport" runat="server">

                                    <label class="form-label">विवादित भू-खंड की मापी का प्रतिवेदन</label>

                                    <asp:FileUpload ID="file_bhukand_prativedan" runat="server" CssClass="form-control" accept=".pdf" />


                                    <small class="text-danger">केवल PDF (अधिकतम 3 MB) </small>

                                    <br />

                                    <a id="lnkfile_bhukand_prativedan" runat="server" visible="false" class="getpdfdoc" path="display" href="#">View Document  </a>

                                </div>

                            </div>

                            <!-- Reason -->
                            <div class="row">

                                <div class="col-md-6" id="divBhukhandReason" runat="server" visible="false">

                                    <label class="form-label">विवादित भू-खंड की मापी नहीं होने का कारण  </label>

                                    <asp:TextBox ID="txtbhukhand_reason" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" MaxLength="500">  </asp:TextBox>

                                </div>

                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>

            </div>

        </asp:Panel>

        <!-- Step-6  -->

        <asp:Panel ID="pnlStep6" runat="server" Visible="false">
            <asp:UpdatePanel ID="upStep6" runat="server" UpdateMode="Conditional">

                <ContentTemplate>


                    <div class="card mt-3">

                        <div class="card-header bg-light">
                            <h5>Step-6 : घटना एवं न्यायालय</h5>
                        </div>
                        <div class="section-card">

                            <div class="section-header">भूमि विवाद से संबंधित घटना / वारदात का विवरण </div>

                            <div class="section-body">

                                <div class="row">

                                    <!-- FIR / Sanha -->
                                    <div class="col-md-4 mb-3">
                                        <label class="form-label">प्राथमिकी / अप्राथमिकी / सनहा दर्ज है?<span class="required">*</span> </label>

                                        <asp:DropDownList ID="dd_IsBhumiVivad" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="dd_IsBhumiVivad_SelectedIndexChanged">
                                            <asp:ListItem Value="0" >--चुने--</asp:ListItem>
                                            <asp:ListItem Value="Y">हाँ</asp:ListItem>
                                            <asp:ListItem Value="N">नहीं</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator71" runat="server" CssClass="validator" ControlToValidate="dd_IsBhumiVivad" InitialValue="0" Display="Dynamic" ValidationGroup="7" SetFocusOnError="true" ErrorMessage="प्राथमिकी / अप्राथमिकी / सनहा दर्ज है?"> </asp:RequiredFieldValidator>
                                    </div>

                                    <!-- Incident Date -->
                                    <div class="col-md-3 mb-3" id="btnBhumiVivadVivran1" runat="server" visible="false">

                                        <label class="form-label">घटना / वारदात की तिथि <span class="required">*</span></label>

                                        <asp:TextBox ID="txtghatanaDate" runat="server" CssClass="form-control" placeholder="dd-MM-yyyy" AutoComplete="off"> </asp:TextBox>

                                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtghatanaDate" Format="dd-MM-yyyy" CssClass="zindex"></cc1:CalendarExtender>

                                    </div>

                                    <!-- Incident Details -->
                                    <div class="col-md-5 mb-3" id="btnBhumiVivadVivran2" runat="server" visible="false">

                                        <label class="form-label">घटना / वारदात का संक्षिप्त विवरण </label>

                                        <asp:TextBox ID="txtghatanavivran" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" MaxLength="500" placeholder="अधिकतम 500 शब्द">  </asp:TextBox>

                                    </div>

                                </div>

                            </div>

                        </div>


                        <div class="section-card" id="btnBhumiVivadVivran3" runat="server" visible="false">

                            <div class="section-header">प्राथमिकी का विवरण</div>

                            <div class="section-body">

                                <div class="row">

                                    <!-- FIR Available -->
                                    <div class="col-md-3 mb-3">

                                        <label class="form-label">प्राथमिकी दर्ज है?<span class="required">*</span>  </label>

                                        <asp:DropDownList ID="ddlPrathmiki_huyee_hai" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPrathmiki_huyee_hai_SelectedIndexChanged">

                                            <asp:ListItem Value="0" >--चुने--</asp:ListItem>
                                            <asp:ListItem Value="Y">हाँ</asp:ListItem>
                                            <asp:ListItem Value="N">नहीं</asp:ListItem>

                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator73" runat="server" CssClass="validator" ControlToValidate="ddlPrathmiki_huyee_hai" InitialValue="0" ValidationGroup="7" Display="Dynamic" SetFocusOnError="true" ErrorMessage="प्राथमिकी दर्ज है?"> </asp:RequiredFieldValidator>

                                    </div>

                                    <!-- FIR Number -->
                                    <div class="col-md-3 mb-3" id="divPrathmiki_sankhiyan" runat="server" visible="false">

                                        <label class="form-label">प्राथमिकी संख्या <span class="required">*</span>  </label>

                                        <asp:TextBox ID="txtFIR_sankhya" runat="server" CssClass="form-control" placeholder="प्राथमिकी संख्या" AutoComplete="off">  </asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator74" runat="server" CssClass="validator" ControlToValidate="txtFIR_sankhya" ValidationGroup="7" Display="Dynamic" SetFocusOnError="true" ErrorMessage="प्राथमिकी संख्या दर्ज करें"> </asp:RequiredFieldValidator>

                                    </div>

                                    <!-- FIR Details -->
                                    <div class="col-md-6 mb-3" id="divPrathmiki_vivaran" runat="server" visible="false">

                                        <label class="form-label">प्राथमिकी का संक्षिप्त विवरण</label>

                                        <asp:TextBox ID="txtPrathmik_vivran" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" MaxLength="500" placeholder="अधिकतम 500 शब्द"> </asp:TextBox>

                                    </div>

                                </div>

                            </div>

                        </div>


                        <div class="section-card" id="btnBhumiVivadVivran4" runat="server" visible="false">

                            <div class="section-header">अप्राथमिकी का विवरण </div>

                            <div class="section-body">

                                <div class="row">

                                    <!-- Aprathmiki Available -->
                                    <div class="col-md-3 mb-3">

                                        <label class="form-label">अप्राथमिकी दर्ज है?<span class="required">*</span> </label>

                                        <asp:DropDownList ID="ddlAprathmiki_huyee_hai" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAprathmiki_huyee_hai_SelectedIndexChanged">

                                            <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                            <asp:ListItem Value="Y">हाँ</asp:ListItem>
                                            <asp:ListItem Value="N">नहीं</asp:ListItem>

                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator75" runat="server" CssClass="validator" ControlToValidate="ddlAprathmiki_huyee_hai" InitialValue="0" ValidationGroup="7" Display="Dynamic" ErrorMessage="अप्राथमिकी दर्ज है?">  </asp:RequiredFieldValidator>

                                    </div>

                                    <!-- IPC / BNS Type -->
                                    <div class="col-md-3 mb-3" id="divdharabsn" runat="server" visible="false">

                                        <label class="form-label">कानून का प्रकार<span class="required">*</span></label>

                                        <div class="pt-2">

                                            <asp:RadioButton ID="rdoOld" runat="server" Text="IPC" GroupName="dhara" AutoPostBack="true" OnCheckedChanged="DharaChanged" />

                                            &nbsp;&nbsp;

                                            <asp:RadioButton ID="rdoNew" runat="server" Text="BNS" GroupName="dhara" AutoPostBack="true" OnCheckedChanged="DharaChanged" />

                                        </div>

                                    </div>

                                    <!-- Aprathmiki Number -->
                                    <div class="col-md-3 mb-3" id="divAPrathmiki_sankhiyan" runat="server" visible="false">

                                        <label class="form-label">अप्राथमिकी संख्या  <span class="required">*</span>  </label>

                                        <asp:TextBox ID="txtAFIR_sankhya" runat="server" CssClass="form-control" AutoComplete="off" placeholder="अप्राथमिकी संख्या">  </asp:TextBox>

                                    </div>

                                    <!-- Description -->
                                    <div class="col-md-3 mb-3" id="divAPrathmiki_vivaran" runat="server" visible="false">

                                        <label class="form-label">अप्राथमिकी का विवरण </label>

                                        <asp:TextBox ID="txtAprathmik_vivran" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" MaxLength="500" placeholder="अधिकतम 500 शब्द"> </asp:TextBox>

                                    </div>

                                </div>
                                <!-- IPC Checkboxes -->
                                <div class="row">

                                    <div class="col-md-6 mb-3" id="divDhara" runat="server" visible="false">

                                        <label class="form-label">IPC धाराएँ </label>

                                        <div class="border rounded p-2">

                                            <asp:CheckBox ID="chk107" runat="server" Text="107" />
                                            <asp:CheckBox ID="chk109" runat="server" Text="109" />
                                            <asp:CheckBox ID="chk110" runat="server" Text="110" />
                                            <asp:CheckBox ID="chk113" runat="server" Text="113" />
                                            <asp:CheckBox ID="chk116" runat="server" Text="116" />
                                            <asp:CheckBox ID="chk133" runat="server" Text="133" />
                                            <asp:CheckBox ID="chk144" runat="server" Text="144" />
                                            <asp:CheckBox ID="chk145" runat="server" Text="145" />
                                            <asp:CheckBox ID="chk147" runat="server" Text="147" />

                                        </div>

                                    </div>

                                    <!-- BNS -->
                                    <div class="col-md-3 mb-3" id="divbsn" runat="server" visible="false">

                                        <label class="form-label">BNS धाराएँ  </label>

                                        <asp:ListBox ID="ddlbsn_dhara_hai" runat="server" CssClass="form-control select2" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="ddlbsn_dhara_hai_SelectedIndexChanged"></asp:ListBox>

                                    </div>

                                    <!-- IPC -->
                                    <div class="col-md-3 mb-3" id="divdhara1" runat="server" visible="false">

                                        <label class="form-label">IPC धाराएँ </label>

                                        <asp:ListBox ID="ddldhara1" runat="server" CssClass="form-control select2" SelectionMode="Multiple"></asp:ListBox>

                                        <asp:HiddenField ID="hdnSelectedIPC" runat="server" />

                                       

                                    </div>

                                    <row>
                                        <div class="col-md-3" id="div_tbnm" runat="server" visible="false">
                                            <asp:Label ID="lblnm" runat="server" Text="Add BNS"></asp:Label>
                                            <asp:TextBox ID="txtbnm" runat="server" CssClass="form-control" Style="height: 43px;"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3" id="div_tdhara" runat="server" visible="false">
                                            <asp:Label ID="lbldhara" runat="server" Text="Add IPC "></asp:Label>
                                            <asp:TextBox ID="txtdhara" runat="server" CssClass="form-control" Style="height: 43px;"></asp:TextBox>
                                        </div>
                                    </row>

                                </div>

                            </div>

                        </div>


                        <div class="section-card" id="btnBhumiVivadVivran5" runat="server" visible="false">

                            <div class="section-header">सनहा</div>

                            <div class="section-body">

                                <!-- Sanha Details -->
                                <div class="row">

                                    <!-- Sanha Registered -->
                                    <div class="col-md-3 mb-3">
                                        <label class="form-label">सनहा दर्ज है ? <span class="required">*</span> </label>

                                        <asp:DropDownList ID="ddlSanhaStatus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSanhaStatus_SelectedIndexChanged">

                                            <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                            <asp:ListItem Value="Y">हाँ</asp:ListItem>
                                            <asp:ListItem Value="N">नहीं</asp:ListItem>

                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator77" runat="server" ControlToValidate="ddlSanhaStatus" InitialValue="0" ValidationGroup="7" Display="Dynamic" SetFocusOnError="true" ErrorMessage="सनहा दर्ज है ?" CssClass="validator" />
                                    </div>


                                    <!-- Sanha Number -->
                                    <div class="col-md-3 mb-3" id="divSanahaSankhiyan1" runat="server" visible="false">

                                        <label class="form-label">सनहा संख्या  <span class="required">*</span> </label>

                                        <asp:TextBox ID="txtSanahaSankhiyan" runat="server" CssClass="form-control" placeholder="सनहा संख्या"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator78" runat="server" ControlToValidate="txtSanahaSankhiyan" ValidationGroup="7" Display="Dynamic" SetFocusOnError="true" ErrorMessage="सनहा संख्या" CssClass="validator" />
                                    </div>

                                    <!-- Sanha Description -->
                                    <div class="col-md-6 mb-3" id="divSanahaSankhiyan2" runat="server" visible="false">

                                        <label class="form-label">सनहा का विवरण </label>

                                        <asp:TextBox ID="txtSanhaDesc" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" MaxLength="500" placeholder="अधिकतम 500 शब्द"> </asp:TextBox>

                                    </div>

                                </div>

                                <!-- Allegation -->
                                <div class="row">

                                    <div class="col-md-6 mb-3">

                                        <label class="form-label">अभियुक्ति </label>

                                        <asp:TextBox ID="txtabhiyukt_vaad" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" MaxLength="500" placeholder="अधिकतम 500 शब्द"> </asp:TextBox>

                                    </div>

                                </div>

                            </div>

                        </div>

                        <%--button + grid display section--%>
                        <div id="btnBhumiVivadVivran6" runat="server" visible="false">

                            <div class="row mb-3">
                                <div class="col-md-12 text-center">
                                    <asp:Button ID="btnbhumivivad" runat="server" Text="Save" CssClass="btn btn-primary" Visible="false" OnClick="btnbhumivivad_Click" />
                                </div>
                            </div>

                            <!-- Incident Details Grid -->
                            <div class="row">
                                <div class="col-md-12">

                                    <asp:Panel ID="Panelgrdbhumivivad" runat="server" ScrollBars="Auto">

                                        <asp:GridView ID="grdbhumivivad" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped table-hover" OnRowCommand="grdbhumivivad_RowCommand">

                                            <Columns>

                                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">

                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnRowDel" runat="server" CssClass="btn btn-danger btn-sm" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Remove"
                                                            OnClientClick="return confirm('Are you sure you want to delete this data?');"> <i class="fa fa-trash" aria-hidden="true"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Sl. No." ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">

                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:BoundField DataField="Ghatna_Vardat_date" HeaderText="घटना की तिथि" ItemStyle-Width="100px" />


                                                <asp:TemplateField HeaderText="घटना की संक्षिप्त विवरण" ItemStyle-Width="220px">

                                                    <ItemTemplate>
                                                        <div style="max-height: 80px; overflow: auto;">
                                                            <%# Eval("Ghatna_Short_vivran") %>
                                                        </div>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:BoundField DataField="is_FIR_registered" HeaderText="प्राथमिकी" ItemStyle-Width="80px" />


                                                <asp:BoundField DataField="praathamiki_sankhya" HeaderText="प्राथमिकी संख्या" ItemStyle-Width="110px" />


                                                <asp:TemplateField HeaderText="प्राथमिकी का विवरण" ItemStyle-Width="220px">

                                                    <ItemTemplate>
                                                        <div style="max-height: 80px; overflow: auto;">
                                                            <%# Eval("praathamiki_ka_vivaran") %>
                                                        </div>
                                                    </ItemTemplate>

                                                </asp:TemplateField>


                                                <asp:BoundField DataField="is_complaint_filed" HeaderText="अप्राथमिकी" ItemStyle-Width="100px" />


                                                <asp:BoundField DataField="dhaara" HeaderText="धारा (Old)" ItemStyle-Width="100px" />


                                                <asp:TemplateField HeaderText="BNS (New)" ItemStyle-Width="120px">

                                                    <ItemTemplate>
                                                        <%# string.IsNullOrEmpty(Eval("bnm").ToString()) ? "--" : Eval("bnm") %>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="New IPC" ItemStyle-Width="120px">

                                                    <ItemTemplate>
                                                        <%# string.IsNullOrEmpty(Eval("newdhara").ToString()) ? "--" : Eval("newdhara") %>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="BNS Other" ItemStyle-Width="120px">

                                                    <ItemTemplate>
                                                        <%# string.IsNullOrEmpty(Eval("bnm1").ToString()) ? "--" : Eval("bnm1") %>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="IPC Other" ItemStyle-Width="120px">

                                                    <ItemTemplate>
                                                        <%# string.IsNullOrEmpty(Eval("newdhara1").ToString())  ? "--" : Eval("newdhara1") %>
                                                    </ItemTemplate>

                                                </asp:TemplateField>


                                                <asp:BoundField DataField="apraathamiki_sankhya" HeaderText="अप्राथमिकी संख्या" ItemStyle-Width="110px" />

                                                <asp:TemplateField HeaderText="अप्राथमिकी का विवरण" ItemStyle-Width="220px">

                                                    <ItemTemplate>
                                                        <div style="max-height: 80px; overflow: auto;">
                                                            <%# Eval("apraathamiki_ka_vivaran") %>
                                                        </div>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:BoundField DataField="is_Sanha_recorded" HeaderText="सनहा" ItemStyle-Width="80px" />

                                                <asp:BoundField DataField="sanha_sankhya" HeaderText="सनहा संख्या" ItemStyle-Width="110px" />

                                                <asp:TemplateField HeaderText="अभियुक्ति" ItemStyle-Width="220px">

                                                    <ItemTemplate>
                                                        <div style="max-height: 80px; overflow: auto;">
                                                            <%# Eval("Abhiyukt") %>
                                                        </div>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                            </Columns>

                                        </asp:GridView>

                                    </asp:Panel>

                                </div>
                            </div>

                        </div>

                        <div class="section-card">

                            <div class="section-header text-center">
                                न्यायालय में प्रक्रियाधीन वाद का विवरण
                            </div>

                            <div class="section-body">

                                <!-- Case Availability -->
                                <div class="row">

                                    <div class="col-md-3 mb-3">

                                        <label class="form-label">प्रक्रियाधीन वाद का विवरण उपलब्ध है ? <span class="required">*</span> </label>

                                        <asp:DropDownList ID="ddl_Isbhumi_Viviad_available" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_Isbhumi_Viviad_available_SelectedIndexChanged">

                                            <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                            <asp:ListItem Value="Y">उपलब्ध है</asp:ListItem>
                                            <asp:ListItem Value="N">उपलब्ध नहीं है</asp:ListItem>

                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator79" runat="server" ControlToValidate="ddl_Isbhumi_Viviad_available" InitialValue="0" ValidationGroup="4" Display="Dynamic" SetFocusOnError="true" CssClass="validator" ErrorMessage="प्रक्रियाधीन वाद का विवरण उपलब्ध है" />

                                    </div>

                                </div>

                                <!-- Court Details -->

                                <div class="row">

                                    <!-- Court -->
                                    <div class="col-md-3 mb-3" id="btnnyayalay1" runat="server" visible="false">

                                        <label class="form-label">न्यायालय <span class="required">*</span>  </label>

                                        <asp:DropDownList ID="ddlnyayalaya" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlnyayalaya_SelectedIndexChanged"></asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator80" runat="server" ControlToValidate="ddlnyayalaya" InitialValue="0" ValidationGroup="4" Display="Dynamic" CssClass="validator" ErrorMessage="न्यायालय चुनें" />

                                    </div>

                                    <!-- Court Type -->
                                    <div class="col-md-3 mb-3" id="div_rajasw_vevhar_nyalay" runat="server" visible="false">

                                        <%-- <label class="form-label">न्यायालय का प्रकार <span class="required">*</span> </label>--%>
                                        <asp:Label ID="labNyayalaya_type" runat="server" Text="न्यायालय का प्रकार"></asp:Label>

                                        <asp:DropDownList ID="ddlnyayalaya_type" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlnyayalaya_type_SelectedIndexChanged"></asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator81" runat="server" ControlToValidate="ddlnyayalaya_type" InitialValue="0" ValidationGroup="4" Display="Dynamic" CssClass="validator" ErrorMessage="न्यायालय का प्रकार चुनें" />

                                    </div>

                                    <!-- District -->
                                    <div class="col-md-3 mb-3" id="divDist_nyayalaya_type" runat="server" visible="false">

                                        <label class="form-label">जिला <span class="required">*</span>  </label>

                                        <asp:DropDownList ID="ddlDist_nyayalaya_type" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDist_nyayalaya_type_SelectedIndexChanged"></asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator82" runat="server" ControlToValidate="ddlDist_nyayalaya_type" InitialValue="0" ValidationGroup="4" Display="Dynamic" CssClass="validator" ErrorMessage="जिला चुनें" />

                                    </div>

                                    <!-- Sub Division -->
                                    <div class="col-md-3 mb-3" id="divSubdivision_nyayalaya_type" runat="server" visible="false">

                                        <label class="form-label">अनुमंडल <span class="required">*</span> </label>

                                        <asp:DropDownList ID="ddlSubdivision_nyayalaya_type" runat="server" CssClass="form-control"></asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator83" runat="server" ControlToValidate="ddlSubdivision_nyayalaya_type" InitialValue="0" ValidationGroup="4" Display="Dynamic" CssClass="validator" ErrorMessage="अनुमंडल चुनें" />

                                    </div>

                                </div>


                                <div class="row">

                                    <!-- विभाग -->
                                    <div class="col-md-3 mb-3" id="divVibhag_nyayalay_type" runat="server" visible="false">

                                        <label class="form-label">विभाग <span class="required">*</span> </label>

                                        <asp:DropDownList ID="ddlVibhag_nyayalay_type" runat="server" CssClass="form-control"></asp:DropDownList>

                                    </div>


                                    <!-- वादी की वाद संख्या / वर्ष -->
                                    <div class="col-md-3 mb-3" id="btnnyayalay3" runat="server" visible="false">

                                        <label class="form-label">वादी की वाद संख्या / वर्ष <span class="required">*</span>  </label>

                                        <asp:TextBox ID="txtdayarvaadsankhya_nayalay" runat="server" CssClass="form-control" placeholder="वादी की वाद संख्या / वर्ष"> </asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator84" runat="server" ControlToValidate="txtdayarvaadsankhya_nayalay" ValidationGroup="4" CssClass="validator" Display="Dynamic" SetFocusOnError="true" ErrorMessage="वादी की वाद संख्या / वर्ष दर्ज करें।"> </asp:RequiredFieldValidator>

                                    </div>


                                    <!-- वादी की वाद का वर्ष -->
                                    <div class="col-md-3 mb-3">

                                        <label class="form-label">वादी की वाद का वर्ष <span class="required">*</span> </label>

                                        <asp:TextBox ID="txtdayaryear_nayayaly" runat="server" CssClass="form-control">  </asp:TextBox>

                                    </div>


                                    <!-- वादी का नाम -->
                                    <div class="col-md-3 mb-3" id="btnnyayalay4" runat="server" visible="false">

                                        <label class="form-label">वादी का नाम <span class="required">*</span>  </label>

                                        <asp:TextBox ID="txtvaadiname_nayaylay" runat="server" CssClass="form-control" placeholder="वादी का नाम">
                                        </asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator85" runat="server" ControlToValidate="txtvaadiname_nayaylay" ValidationGroup="4" CssClass="validator" Display="Dynamic" SetFocusOnError="true" ErrorMessage="वादी का नाम दर्ज करें।">  </asp:RequiredFieldValidator>

                                    </div>


                                    <!-- प्रतिवादी का नाम -->
                                    <div class="col-md-3 mb-3" id="btnnyayalay5" runat="server" visible="false">

                                        <label class="form-label">प्रतिवादी का नाम <span class="required">*</span> </label>

                                        <asp:TextBox ID="txtprativadi_nayaylay" runat="server" CssClass="form-control" placeholder="प्रतिवादी का नाम"> </asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator86" runat="server" ControlToValidate="txtprativadi_nayaylay" ValidationGroup="4" CssClass="validator" Display="Dynamic" SetFocusOnError="true" ErrorMessage="प्रतिवादी का नाम दर्ज करें।"> </asp:RequiredFieldValidator>

                                    </div>

                                </div>

                                <!-- अद्यतन स्थिति -->
                                <div class="row" id="btnnyayalay6" runat="server" visible="false">

                                    <div class="col-md-6 mb-3">

                                        <label class="form-label">वाद की अद्यतन स्थिति का विवरण </label>

                                        <asp:TextBox ID="txtwadKiAddhatan_Sthiti_nayayaly" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" MaxLength="500" placeholder="अधिकतम 500 शब्द"> </asp:TextBox>

                                        <small class="text-muted">अधिकतम 500 शब्द </small>

                                    </div>

                                </div>

                                <%--Grid+Button--%>

                                <div class="row mb-2">
                                    <div class="col-md-12" id="btnnyayalay7" runat="server" visible="false">
                                        <center>
                                            <asp:Button ID="btnnayaylaysave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnnayaylaysave_Click" />
                                        </center>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" style="text-align: center">
                                        <asp:Panel ID="Panelgrdnyayalay_vivran" runat="server" ScrollBars="Auto">
                                            <asp:GridView ID="grdnyayalay_vivran" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped table-hover" OnRowCommand="grdnyayalay_vivran_RowCommand">

                                                <Columns>

                                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">

                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnRowDel" runat="server" CssClass="btn btn-danger btn-sm" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Remove"
                                                                OnClientClick="return confirm('Are you sure you want to delete this data?');"> <i class="fa fa-trash" aria-hidden="true"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">

                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>


                                                    <asp:BoundField DataField="court" HeaderText="न्यायालय" ItemStyle-Width="100px" />


                                                    <asp:BoundField DataField="courtType" HeaderText="न्यायालय का प्रकार" ItemStyle-Width="120px" />


                                                    <asp:BoundField DataField="Dst" HeaderText="जिला" ItemStyle-Width="90px" />

                                                    <asp:BoundField DataField="SubDiv" HeaderText="अनुमंडल" ItemStyle-Width="90px" />

                                                    <asp:BoundField DataField="Vibhag" HeaderText="विभाग" ItemStyle-Width="90px" />

                                                    <asp:BoundField DataField="vaadi_ki_vaad_sankhya_varsh" HeaderText="वाद संख्या / वर्ष" ItemStyle-Width="120px" />

                                                    <asp:BoundField DataField="vadi_name" HeaderText="वादी का नाम" ItemStyle-Width="120px" />

                                                    <asp:BoundField DataField="prativadi_name" HeaderText="प्रतिवादी का नाम" ItemStyle-Width="120px" />

                                                    <asp:TemplateField HeaderText="अद्धतन स्थिति का विवरण" ItemStyle-Width="220px">

                                                        <ItemTemplate>
                                                            <div style="max-height: 80px; overflow: auto;">
                                                                <%# Eval("vaad_ki_addhatan_sthiti_vivaran") %>
                                                            </div>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                </Columns>

                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
                                </div>

                            </div>

                        </div>

                    </div>

                </ContentTemplate>

            </asp:UpdatePanel>

        </asp:Panel>

        <!-- Step-7  -->

        <asp:Panel ID="pnlStep7" runat="server" Visible="false">

            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">

                <ContentTemplate>

                    <div class="card mt-3">

                        <div class="card-header bg-light">
                            <h5>Step-7 : अंचलाधिकारी एवं थानाध्यक्ष बैठक</h5>
                        </div>

                        <div class="section-card">

                            <div class="section-header">
                                अंचलाधिकारी एवं थानाध्यक्ष द्वारा भूमि विवाद के निराकरण हेतु कृत कारवाई का विवरण
                            </div>

                            <div class="section-body">

                                <!-- =========================
                                     Meeting Information
                                ==========================-->

                                <div class="row g-3 mb-4">

                                    <div class="col-lg-3 col-md-6">

                                        <label class="form-label">विवाद की संवेदनशीलता<span class="required">*</span> </label>

                                        <asp:DropDownList ID="ddlbhumivivadki_sanvedanshilta" runat="server" CssClass="form-control"></asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator87" runat="server" ControlToValidate="ddlbhumivivadki_sanvedanshilta" Display="Dynamic" ForeColor="Red"> विवाद की संवेदनशीलता </asp:RequiredFieldValidator>
                                    </div>


                                    <%--  <div class="col-lg-2 col-md-6 text-center">

                                <label class="form-label d-block">
                                    संवेदनशीलता स्तर
                                </label>

                                <asp:Image ID="onestar" runat="server" ImageUrl="images/1.png" Width="90" />
                                <asp:Image ID="twostar" runat="server" ImageUrl="images/2.png" Width="90" Visible="false" />
                                <asp:Image ID="threestar" runat="server" ImageUrl="images/3.png" Width="90" Visible="false" />
                                <asp:Image ID="fourstar" runat="server" ImageUrl="images/4.png" Width="90" Visible="false" />

                            </div>--%>


                                    <div class="col-lg-3 col-md-6">

                                        <label class="form-label">बैठक की तिथि <span class="required">*</span> </label>

                                        <asp:TextBox ID="txtbaithakDate" runat="server" CssClass="form-control" MaxLength="10" placeholder="dd-MM-yyyy"></asp:TextBox>

                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtbaithakDate" Format="dd-MM-yyyy" ></cc1:CalendarExtender>

                                    </div>


                                    <div class="col-lg-2 col-md-6">

                                        <label class="form-label">क्या वादी उपस्थित है? <span class="required">*</span> </label>

                                        <asp:DropDownList ID="ddlIsVadiAvailable" runat="server" CssClass="form-control">

                                            <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                            <asp:ListItem Value="Y">हाँ</asp:ListItem>
                                            <asp:ListItem Value="N">नहीं</asp:ListItem>

                                        </asp:DropDownList>

                                    </div>


                                    <div class="col-lg-3 col-md-6">

                                        <label class="form-label">क्या प्रतिवादी उपस्थित है? <span class="required">*</span>  </label>

                                        <asp:DropDownList ID="ddl_IsprativadiAvailable" runat="server" CssClass="form-control">

                                            <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                            <asp:ListItem Value="Y">हाँ</asp:ListItem>
                                            <asp:ListItem Value="N">नहीं</asp:ListItem>

                                        </asp:DropDownList>

                                    </div>

                                </div>


                                <hr />


                                <!-- =========================
                                          Meeting Result
                                    ==========================-->

                                <div class="row g-3 mb-4">

                                    <div class="col-lg-3">

                                        <label class="form-label">बैठक का निष्कर्ष <span class="required">*</span> </label>

                                        <asp:DropDownList ID="ddlaction" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlaction_SelectedIndexChanged">

                                            <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                            <asp:ListItem Value="1">प्रारंभिक निष्पादन</asp:ListItem>
                                            <asp:ListItem Value="2">मापी के लिए निर्धारित</asp:ListItem>
                                            <asp:ListItem Value="3">प्रक्रियाधीन</asp:ListItem>
                                            <asp:ListItem Value="4">अस्वीकृत</asp:ListItem>
                                            <asp:ListItem Value="5">अंतिम निष्पादन</asp:ListItem>
                                            <asp:ListItem Value="6">न्यायालय में लंबित</asp:ListItem>

                                        </asp:DropDownList>

                                    </div>


                                    <div class="col-lg-3" id="divNextDate" runat="server" visible="false">

                                        <%--<label class="form-label">अगली सुनवाई की तिथि </label>--%>
                                        <asp:Label ID="labNextDate" runat="server" Text="अगली सुनवाई की तिथि"></asp:Label>
                                        <asp:TextBox ID="txtAgalaDate" runat="server" CssClass="form-control" placeholder="dd-MM-yyyy"> </asp:TextBox>

                                        <cc1:CalendarExtender ID="PopCalendar2" runat="server" TargetControlID="txtAgalaDate" Format="dd-MM-yyyy"></cc1:CalendarExtender>

                                    </div>


                                    <div class="col-lg-3" id="divvadkavars" runat="server" visible="false">

                                        <label class="form-label">वादी की वाद संख्या / वर्ष </label>

                                        <asp:TextBox ID="txtvadkavars" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>


                                    <div class="col-lg-6" id="divCancelReason" runat="server" visible="false">

                                        <label class="form-label">अस्वीकृति का कारण </label>

                                        <asp:TextBox ID="txtCancelReason" runat="server" CssClass="form-control" TextMode="MultiLine" MaxLength="500" Rows="3" placeholder="अधिकतम 500 शब्द"> </asp:TextBox>

                                    </div>

                                </div>


                                <!-- =========================
                                     Decision
                                ==========================-->

                                <div class="row g-3 mb-4">

                                    <div class="col-lg-6">

                                        <label class="form-label">बैठक में लिया गया निर्णय </label>

                                        <asp:TextBox ID="txtfalafal" runat="server" CssClass="form-control" Rows="4" TextMode="MultiLine" MaxLength="500" placeholder="अधिकतम 500 शब्द"> </asp:TextBox>

                                    </div>

                                    <div class="col-lg-6">

                                        <label class="form-label">थानाध्यक्ष एवं अंचलाधिकारी का संयुक्त प्रतिवेदन </label>

                                        <asp:HiddenField ID="hdLandDoc" runat="server" />

                                        <asp:FileUpload ID="LandDoc" runat="server" CssClass="form-control" accept=".pdf" />

                                        <small class="text-danger">केवल PDF (3 MB) </small>

                                        <a id="lnkLandDoc" runat="server" class="getpdfdoc" path="display" visible="false">View Document  </a>

                                    </div>

                                </div>


                                <!-- =========================
                                    Circle Officer
                                ==========================-->

                                <div class="row g-3 mb-4">

                                    <div class="col-lg-6">

                                        <label class="form-label">अंचलाधिकारी का मंतव्य </label>

                                        <asp:TextBox ID="txtabhiyukt_anchaladhikari" runat="server" CssClass="form-control" Rows="4" MaxLength="500" TextMode="MultiLine"> </asp:TextBox>

                                    </div>

                                    <div class="col-lg-6">

                                        <label class="form-label">अंचलाधिकारी का मंतव्य पत्र </label>

                                        <asp:HiddenField ID="hdCircleOfficer_letterofintent" runat="server" />

                                        <asp:FileUpload ID="CircleOfficer_letterOfIntent" runat="server" CssClass="form-control" accept=".pdf" />

                                        <small class="text-danger">केवल PDF (3 MB) </small>

                                        <a id="lnkCircleOfficer_letterOfIntent" runat="server" class="getpdfdoc" path="display" visible="false">View Document </a>

                                    </div>

                                </div>



                                <%-----SHO--%>


                                <div class="row g-3">

                                    <div class="col-lg-6">

                                        <label class="form-label">थानाध्यक्ष का मंतव्य </label>

                                        <asp:TextBox ID="txtabhiyukt_thaanprabhaaree" runat="server" CssClass="form-control" Rows="4" MaxLength="500" TextMode="MultiLine"> </asp:TextBox>

                                    </div>

                                    <div class="col-lg-6">

                                        <label class="form-label">थानाध्यक्ष का मंतव्य पत्र </label>

                                        <asp:HiddenField ID="hdPoliceOfficer_letterOfIntent" runat="server" />

                                        <asp:FileUpload ID="PoliceOfficer_letterOfIntent" runat="server" CssClass="form-control" accept=".pdf" />

                                        <small class="text-danger">केवल PDF (3 MB)</small>

                                        <a id="lnkPoliceOfficer_letterOfIntent" runat="server" class="getpdfdoc" path="display" visible="false">View Document </a>

                                    </div>

                                </div>

                            </div>

                        </div>

                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>

        </asp:Panel>

        <%-- ButtonSection--%>
        <div class="text-center mt-3 mb-4">

            <asp:Button ID="btnPrevious" runat="server" Text="Previous" CssClass="btn btn-secondary" OnClick="btnPrevious_Click" />
            &nbsp;
           <asp:Button ID="btnNext" runat="server" Text="Save & Next" CssClass="btn btn-success" OnClick="btnNext_Click" CausesValidation="false" />

        </div>

    </div>
</asp:Content>
