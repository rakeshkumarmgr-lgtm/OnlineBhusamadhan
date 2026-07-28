<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_Step1.ascx.cs" Inherits="Bhusamadhan.LandDispute.Entry.UserControls.UC_Step1" %>

 <style>
     .section-card {
         border: 1px solid #cfd8dc;
         border-radius: 6px;
         margin-bottom: 20px;
     }

     .section-header {
         background: #0d6efd;
         color: #fff;
         padding: 10px 15px;
         font-size: 16px;
         font-weight: 600;
     }

     .section-body {
         background: #ffffff;
         padding: 15px;
     }

     .form-label {
         display: block;
         margin-bottom: 4px;
         font-weight: 600;
     }

     .note-box {
         background: #fff8e1;
         border-left: 5px solid #ffc107;
         padding: 10px;
         margin-top: 15px;
         border-radius: 4px;
     }

     .required {
         color: red;
     }

     .validator {
         color: red;
         font-size: 13px;
     }
 </style>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<div class="row">
    <center>
        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
    </center>
</div>
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

                    <div class="section-header"><i class="fa fa-user"></i>व्यक्तिगत जानकारी </div>

                    <div class="card-body section-body">

                        <div class="form-row">

                            <!-- वादी का नाम -->
                            <div class="form-group col-md-3 mb-2">
                                <label class="form-label">वादी का नाम <span class="required">*</span> </label>
                                <asp:TextBox ID="txtNamePerAadhaar" runat="server" CssClass="form-control"
                                    placeholder="वादी का नाम" AutoComplete="off" oninput="this.value=this.value.toUpperCase();" onkeypress="return ValidateAlpha(event)"></asp:TextBox>
                                <asp:Label ID="DtxtNamePerAadhaar" runat="server" CssClass="form-control" Visible="false"> </asp:Label>

                                <asp:RequiredFieldValidator ID="rfv1" runat="server" CssClass="validator" ControlToValidate="txtNamePerAadhaar" ErrorMessage="वादी का नाम दर्ज करें।" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true" />

                            </div>

                            <!-- पिता/पति -->
                            <div class="form-group col-md-3 mb-2">

                                <label class="form-label">पिता / पति का नाम <span class="required">*</span> </label>

                                <asp:TextBox ID="txtFName" runat="server" CssClass="form-control" placeholder="पिता / पति का नाम" AutoComplete="off" oninput="this.value=this.value.toUpperCase();" onkeypress="return ValidateAlpha(event)"> </asp:TextBox>

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
                                <asp:Label ID="Dddlgender" runat="server" CssClass="form-control" Visible="false"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" CssClass="validator" ControlToValidate="ddlgender" InitialValue="0" ErrorMessage="लिंग चुनें।" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true" />

                            </div>

                            <!-- Birth Year -->
                            <div class="form-group col-md-3 mb-2">

                                <label class="form-label">जन्म वर्ष </label>

                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control"></asp:DropDownList>

                                <asp:Label ID="Dtxtdatebirth" runat="server" CssClass="form-control" Visible="false"></asp:Label>

                            </div>

                        </div>

                        <div class="form-row">

                            <!-- वादी का नाम -->
                            <div class="form-group col-md-3 mb-2">
                                <label class="font-weight-bold">मोबाइल नंबर<span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtvadimobile" runat="server" CssClass="form-control" MaxLength="10" onkeypress="return ValidateMobile(event)" placeholder="मोबाइल नंबर"></asp:TextBox>
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

                    <div class="section-header"><i class="fa fa-map-marker-alt"></i>पता विवरण </div>

                    <div class="card-body section-body">

                        <div class="form-row">

                            <!-- District -->
                            <div class="form-group col-md-3 mb-2">

                                <label class="form-label">जिला <span class="required">*</span> </label>

                                <asp:DropDownList ID="ddlUserDist" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlUserDist_SelectedIndexChanged" ></asp:DropDownList>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="validator" ControlToValidate="ddlUserDist" InitialValue="0" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true" ErrorMessage="जिला चुनें।">
                                </asp:RequiredFieldValidator>

                            </div>

                            <!-- Sub Division -->
                            <div class="form-group col-md-3 mb-2">

                                <label class="form-label">अनुमंडल <span class="required">*</span> </label>

                                <asp:DropDownList ID="ddlUserSubdivision" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlUserSubdivision_SelectedIndexChanged" >
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="validator" ControlToValidate="ddlUserSubdivision" InitialValue="0" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true" ErrorMessage="अनुमंडल चुनें।">
                                </asp:RequiredFieldValidator>

                            </div>

                            <!-- Circle / Block -->
                            <div class="form-group col-md-3 mb-2">

                                <label class="form-label">अंचल <span class="required">*</span> </label>

                                <asp:DropDownList ID="ddlUserBlock" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlUserBlock_SelectedIndexChanged" >
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="validator" ControlToValidate="ddlUserBlock" InitialValue="0" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true" ErrorMessage="अंचल चुनें।">
                                </asp:RequiredFieldValidator>

                            </div>

                            <!-- Police Station -->
                            <div class="form-group col-md-3 mb-2">

                                <label class="form-label">थाना <span class="required">*</span> </label>

                                <asp:DropDownList ID="ddlUserThana" runat="server" CssClass="form-control" AutoPostBack="True">
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="validator" ControlToValidate="ddlUserThana" InitialValue="0" ValidationGroup="1" Display="Dynamic" SetFocusOnError="true" ErrorMessage="थाना चुनें।">
                                </asp:RequiredFieldValidator>

                            </div>

                        </div>

                    </div>

                </div>
           

                <!-- ====================== Area Information ====================== -->
                <div class="card section-card">

                    <div class="section-header"><i class="fa fa-map"></i>स्थानीय पता विवरण </div>

                    <div class="card-body section-body">

                        <!-- Row-1 -->
                        <div class="form-row">

                            <!-- Area Type -->
                            <div class="form-group col-md-3 mb-2">

                                <label class="form-label">क्षेत्र का प्रकार <span class="required">*</span> </label>

                                <asp:DropDownList ID="ddlUserAreatype" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlUserAreatype_SelectedIndexChanged" >

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

                                <asp:DropDownList ID="ddlUserPanchyat" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlUserPanchyat_SelectedIndexChanged" >
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

                                <label class="form-label">
                                    वार्ड<span class="required">*</span> <span id="UWard" runat="server" visible="true"></span>

                                </label>

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

                                <asp:TextBox ID="txtUserMohalla" runat="server" CssClass="form-control" MaxLength="30" placeholder="मोहल्ला का नाम" AutoComplete="off" oncopy="return false" onpaste="return false" oncut="return false" onkeyup="Upper(this)">
                                </asp:TextBox>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" CssClass="validator" ControlToValidate="txtUserMohalla" ValidationGroup="1" Display="Dynamic" ErrorMessage="मोहल्ला का नाम दर्ज करें।">
                                </asp:RequiredFieldValidator>

                            </div>

                        </div>

                    </div>

                </div>
              
                <br />
                <%----------------------------------------------------------------------%>
     

                <div class="card section-card">

                    <div class="section-header"><i class="fa fa-building"></i>विभाग सम्बन्धी जानकारी </div>

                    <div class="card-body section-body">

                        <div class="form-row">

                            <div class="form-group col-md-4">

                                <label class="form-label">क्या वादी किसी विभाग का प्रतिनिधि है?<span class="required">*</span> </label>

                                <asp:DropDownList ID="ddl_is_vadi_from_an_dept" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddl_is_vadi_from_an_dept_SelectedIndexChanged" >

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

                                <asp:TextBox ID="txtWvibhaag_padanaam" runat="server" CssClass="form-control" placeholder="विभाग में पदनाम" onkeyup="Upper(this)" onkeypress="return ValidateAlpha(event)">
                                </asp:TextBox>

                            </div>

                        </div>

                        <div class="note-box">

                            <strong>नोट :</strong> यदि विभाग की कोई जमीन है तो उस स्थिति में वादी विभाग के प्रतिनिधि होंगे।

                        </div>

                    </div>

                </div>

                <div class="card section-card">

                    <div class="section-header bg-success"><i class="fa fa-university"></i>संस्था सम्बन्धी जानकारी </div>

                    <div class="card-body section-body">

                        <div class="form-row">

                            <div class="form-group col-md-4">

                                <label class="form-label">क्या वादी किसी संस्था का प्रतिनिधि है? <span class="required">*</span>  </label>

                                <asp:DropDownList ID="ddl_is_vadi_from_an_org" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddl_is_vadi_from_an_org_SelectedIndexChanged" >

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

                                    <label class="form-label">
                                        संस्था का नाम <span class="required">*</span>
                                    </label>

                                    <asp:TextBox ID="txtWsanstha_naam" runat="server" CssClass="form-control" placeholder="संस्था का नाम" AutoComplete="off" oncopy="return false" onpaste="return false" oncut="return false" onkeyup="Upper(this)" onkeypress="return ValidateAlpha(event)">
                                    </asp:TextBox>

                                </div>


                                <div class="form-group col-md-3">

                                    <label class="form-label">संस्था में पदनाम </label>

                                    <asp:TextBox ID="txtWsanstha_padanaam" runat="server" CssClass="form-control" placeholder="संस्था में पदनाम" AutoComplete="off" oncopy="return false" onpaste="return false" oncut="return false" onkeyup="Upper(this)" onkeypress="return ValidateAlpha(event)">
                                    </asp:TextBox>

                                </div>

                            </div>

                        </div>

                        <!-- Save Button -->

                        <div class="row mb-2">
                            <div class="col-md-12 text-center">
                                <asp:HiddenField ID="hfwadiprint" runat="server" />

                                <asp:Button ID="btnAddVadiDetail" runat="server" Text="Save" CssClass="btn btn-primary" ValidationGroup="1" OnClick="btnAddVadiDetail_Click"  />
                            </div>
                        </div>

                        <!-- Repeater -->

                        <div class="row mt-3">
                            <div class="col-md-12">
                                <div class="table-responsive">

                                    <asp:Repeater ID="rptWadi" runat="server" OnItemCommand="rptWadi_ItemCommand" >

                                        <HeaderTemplate>

                                            <table class="table table-bordered table-striped table-hover table-sm mb-0">
                                                <thead class="thead-dark text-center">
                                                    <tr>
                                                        <th style="width: 70px;">Action</th>
                                                        <th style="width: 50px;">#</th>
                                                        <th>वादी का नाम</th>
                                                        <th>पिता / पति का नाम</th>
                                                        <th>लिंग</th>
                                                        <th>जन्म वर्ष</th>
                                                        <th>जिला</th>
                                                        <th>अनुमंडल</th>
                                                        <th>अंचल</th>
                                                        <th>थाना</th>
                                                        <th>क्षेत्र</th>
                                                        <th>ग्राम पंचायत</th>
                                                        <th>राजस्व ग्राम</th>
                                                        <th>वार्ड</th>
                                                        <th>मोबाइल</th>
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

                                                <td><%# Eval("NameAsPerAadhaar") %></td>

                                                <td><%# Eval("Vadi_Father_Husband_Name") %></td>

                                                <td class="text-center">
                                                    <%# Eval("SexAsPerAadhaar").ToString() == "M" ? "पुरुष" : Eval("SexAsPerAadhaar").ToString() == "F" ? "महिला" : "अन्य" %>
                                                </td>

                                                <td class="text-center">
                                                    <%# Eval("YearOfBirthAsPerAadhaar") %>
                                                </td>

                                                <td><%# Eval("DistrictName") %></td>

                                                <td><%# Eval("SubdivisionName") %></td>

                                                <td><%# Eval("BlockName") %></td>

                                                <td><%# Eval("ThanaName") %></td>

                                                <td><%# Eval("AreaTypeName") %></td>

                                                <td><%# Eval("PanchayatName") %></td>

                                                <td><%# Eval("VillageName") %></td>

                                                <td class="text-center">
                                                    <%# Eval("WardName") %>
                                                </td>

                                                <td class="text-center">
                                                    <%# Eval("Vadi_MobileNo") %>
                                                </td>

                                                <td class="text-center">
                                                    <%# Eval("is_vadi_from_an_dept").ToString() == "Y" ? "हाँ" : "नहीं" %>
                                                </td>

                                                <td class="text-center">
                                                    <%# Eval("is_vadi_from_an_org").ToString() == "Y" ? "हाँ" : "नहीं" %>
                                                </td>

                                                <%-- <td>

                                                     <%# Eval("is_vadi_from_an_dept").ToString() == "Y" ? Eval("vadi_dept_name") : Eval("is_vadi_from_an_org").ToString() == "Y"  ? Eval("vadi_org_name") : "" %>

                                                 </td>

                                                 <td>

                                                     <%# Eval("is_vadi_from_an_dept").ToString() == "Y" ? Eval("vadi_dept_pad_name") : Eval("is_vadi_from_an_org").ToString() == "Y" ? Eval("vadi_org_pad_name") : "" %>

                                                 </td>--%>
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

                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" CssClass="validator" ControlToValidate="ddlDistrict" ValidationGroup="2" InitialValue="0" ErrorMessage="जिला चुनें।">  </asp:RequiredFieldValidator>

                    </div>


                    <!-- Subdivision -->

                    <div class="form-group col-md-3 mb-2">

                        <label class="form-label">अनुमंडल <span class="required">*</span></label>

                        <asp:DropDownList ID="ddlSubdivision" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" CssClass="validator" ValidationGroup="2" InitialValue="0" ControlToValidate="ddlSubdivision" ErrorMessage="अनुमंडल चुनें।"> </asp:RequiredFieldValidator>

                    </div>


                    <!-- Block -->

                    <div class="form-group col-md-3 mb-2">

                        <label class="form-label">अंचल <span class="required">*</span> </label>

                        <asp:DropDownList ID="ddlBlock" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" CssClass="validator" ValidationGroup="2" InitialValue="0" ControlToValidate="ddlBlock" ErrorMessage="अंचल चुनें।"> </asp:RequiredFieldValidator>

                    </div>


                    <!-- Police -->

                    <div class="form-group col-md-3 mb-2">

                        <label class="form-label">थाना <span class="required">*</span> </label>

                        <asp:DropDownList ID="ddlPolice" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" CssClass="validator" ValidationGroup="2" InitialValue="0" ControlToValidate="ddlPolice" ErrorMessage="थाना चुनें।"></asp:RequiredFieldValidator>

                    </div>

                </div>



                <div class="form-row">

                    <!-- Area Type -->

                    <div class="form-group col-md-3 mb-2">

                        <label class="form-label">क्षेत्र का प्रकार <span class="required">*</span>  </label>

                        <asp:DropDownList ID="ddlareatype" runat="server" CssClass="form-control" AutoPostBack="true">

                            <asp:ListItem Value="0">--चुनें--</asp:ListItem>
                            <asp:ListItem Value="R">ग्रामीण</asp:ListItem>
                            <asp:ListItem Value="U">शहरी</asp:ListItem>

                        </asp:DropDownList>

                    </div>


                    <!-- Panchayat -->

                    <div class="form-group col-md-3 mb-2" id="divPanchyat" runat="server">

                        <label class="form-label">ग्राम पंचायत <span class="required">*</span> </label>

                        <asp:DropDownList ID="ddlPanchyat" runat="server" CssClass="form-control" AutoPostBack="true">
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

                        <asp:DropDownList ID="ddlVillage" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>

                    </div>


                    <!-- Other Village -->

                    <div class="form-group col-md-3 mb-2" id="divVillage_Anya" runat="server" visible="false">

                        <label class="form-label">अन्य राजस्व ग्राम</label>

                        <asp:TextBox ID="txtVillage_Anya" runat="server" CssClass="form-control"> </asp:TextBox>

                    </div>


                    <!-- Ward -->

                    <div class="form-group col-md-3 mb-2" id="divWard" runat="server">

                        <label class="form-label">वार्ड <span class="required">*</span> </label>

                        <asp:DropDownList ID="ddlWard" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>

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

                        <asp:DropDownList ID="ddl_vivad_adyatan_sthiti" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="ddl_vivad_adyatan_sthiti" InitialValue="0" ValidationGroup="2" CssClass="validator" Display="Dynamic" SetFocusOnError="true" ErrorMessage="विवाद का अद्यतन कारक चुनें।" />
                    </div>

                    <!-- राजस्व थाना संख्या -->
                    <div class="col-md-3 mb-3">

                        <label class="form-label">राजस्व थाना संख्या </label>

                        <asp:TextBox ID="txtrajaswa_sankhya" runat="server" CssClass="form-control" placeholder="राजस्व थाना संख्या" onkeypress="return ValidateNum(event)"> </asp:TextBox>

                    </div>

                    <!-- भूमि का प्रकार -->
                    <div class="col-md-3 mb-3">

                        <label class="form-label">भूमि का प्रकार <span class="required">*</span> </label>

                        <asp:DropDownList ID="ddlbhumitype" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="ddlbhumitype" InitialValue="0" ValidationGroup="2" CssClass="validator" Display="Dynamic" SetFocusOnError="true" ErrorMessage="भूमि का प्रकार चुनें।"> </asp:RequiredFieldValidator>

                    </div>

                    <!-- सरकारी भूमि का प्रकार -->
                    <div class="col-md-3 mb-3" id="divSarkaribhumitype" runat="server" visible="false">

                        <label class="form-label">सरकारी भूमि का प्रकार <span class="required">*</span> </label>

                        <asp:DropDownList ID="ddlsarkaribhumitype" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>

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

                        <asp:DropDownList ID="ddlbhumivivadtype" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>

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

                        <asp:TextBox ID="txtAwadenKiTithi" runat="server" CssClass="form-control" placeholder="dd-MM-yyyy" onkeypress="return dateValidate(event)">  </asp:TextBox>

                        <cc1:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtAwadenKiTithi" Format="dd-MM-yyyy" CssClass="zindex" OnClientDateSelectionChanged="checkDate"></cc1:CalendarExtender>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="txtAwadenKiTithi" ValidationGroup="2" CssClass="validator" Display="Dynamic" SetFocusOnError="true" ErrorMessage="आवेदन की तिथि दर्ज करें।"> </asp:RequiredFieldValidator>

                    </div>

                </div>

                <!-- ===================== विवरण ===================== -->

                <div class="row">

                    <!-- वादी विवरण -->

                    <div class="col-lg-6 mb-4">

                        <label class="form-label">वादी द्वारा भूमि विवाद का संक्षिप्त विवरण <span class="required">*</span>  </label>

                        <asp:TextBox ID="txtVadiVivarani" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="6" MaxLength="500" placeholder="अधिकतम 500 शब्द" onkeyup="Upper(this)">
                        </asp:TextBox>

                        <small class="text-muted">अधिकतम 500 शब्द </small>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ControlToValidate="txtVadiVivarani" ValidationGroup="2" CssClass="validator" Display="Dynamic" SetFocusOnError="true" ErrorMessage="वादी द्वारा विवाद का विवरण दर्ज करें।"> </asp:RequiredFieldValidator>

                    </div>

                    <!-- प्रतिवादी विवरण -->

                    <div class="col-lg-6 mb-4">

                        <label class="form-label">प्रतिवादी द्वारा भूमि विवाद का संक्षिप्त विवरण </label>

                        <asp:TextBox ID="txtPrativadiVivarani" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="6" MaxLength="500" placeholder="अधिकतम 500 शब्द" onkeyup="Upper(this)"> </asp:TextBox>

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
