<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationPrint.aspx.cs" Inherits="Bhusamadhan.LandDispute.Entry.ApplicationPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Application Print</title>
    <link href="../assets/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        /* =========================================================
       PRINT PAGE - BASE
       Keep the same layout as ApplicationPreview.aspx
       ========================================================= */

        html,
        body {
            margin: 0;
            padding: 0;
            background: #fff;
            color: #000;
            font-family: Arial, "Noto Sans Devanagari", sans-serif;
            font-size: 13px;
        }

        .application-header {
            padding: 10px 0;
            text-align: center;
        }

            .application-header h3 {
                margin: 0 0 5px;
                font-weight: 700;
            }

            .application-header h5 {
                margin: 0;
                font-weight: 600;
            }


        /* =========================================================
       APPLICATION NUMBER
       ========================================================= */

        .application-number {
            padding: 6px 10px;
            margin-top: 5px;
            text-align: right;
            font-weight: 600;
        }


        /* =========================================================
       SECTION
       ========================================================= */

        .preview-section {
            margin-bottom: 15px;
        }

        .section-title {
            background-color: #d8d8d8;
            padding: 7px 10px;
            margin: 0;
            min-height: 34px;
            font-size: 17px;
            font-weight: 600;
            line-height: 20px;
            border: 1px solid #c8c8c8;
            /* Important for printing */
            page-break-after: avoid;
        }



        /* =========================================================
       NORMAL LABEL / VALUE
       ========================================================= */

        .preview-field {
            padding: 5px 10px;
            line-height: 1.4;
        }

        .preview-label {
            display: inline !important;
            font-weight: 600;
            color: #222;
        }

        .preview-value {
            display: inline !important;
            margin-left: 4px;
            color: #333;
            word-break: break-word;
        }


        /* =========================================================
       BLOCK LABEL / VALUE
       ========================================================= */

        .preview-label-block {
            font-weight: 600;
            color: #222;
        }

        .preview-value-block {
            color: #333;
            word-break: break-word;
        }


        /* =========================================================
       VADI / PRATIVADI CARD
       ========================================================= */

        .vadi-card {
            margin: 10px 0;
            border: 1px solid #ccc;
            border-radius: 3px;
            overflow: visible;
            background-color: #fff;
            /* Keep one person's details together where possible */
            page-break-inside: avoid;
        }

        .vadi-header {
            padding: 6px 10px;
            background-color: #f1f1f1;
            border-bottom: 1px solid #ccc;
            font-size: 15px;
            font-weight: 600;
        }

        .vadi-body {
            padding: 2px 0;
        }


        /* =========================================================
       TABLE
       ========================================================= */

        .preview-table {
            width: 100%;
            margin-bottom: 0;
            border-collapse: collapse;
        }

            .preview-table th {
                padding: 6px;
                text-align: center;
                vertical-align: middle;
                font-weight: 600;
                white-space: normal;
            }

            .preview-table td {
                padding: 6px;
                vertical-align: top;
                word-break: break-word;
            }


        /* =========================================================
       GRIDVIEW
       ========================================================= */

        .CSSTableGeneratorGrid {
            width: 100%;
            margin-bottom: 0;
            border-collapse: collapse;
        }

            .CSSTableGeneratorGrid th {
                text-align: center;
                vertical-align: middle;
                font-weight: 600;
                white-space: normal;
                padding: 6px;
            }

            .CSSTableGeneratorGrid td {
                vertical-align: top;
                word-break: break-word;
                padding: 6px;
            }


        /* =========================================================
       LONG TEXT
       IMPORTANT:
       Do NOT use fixed height in print.
       ========================================================= */

        .preview-grid-text {
            display: block;
            max-height: none;
            height: auto;
            overflow: visible;
            line-height: 1.4;
            word-break: break-word;
            white-space: normal;
        }


        /* =========================================================
       PDF ICONS
       ========================================================= */

        .getpdfdoc,
        .evidence-pdf,
        .preview-pdf {
            cursor: pointer;
            border: 0;
        }

        .pdf-link {
            display: inline-block;
            cursor: pointer;
        }

        .pdf-icon {
            width: 35px;
            height: 35px;
        }

        .document-field {
            padding: 6px 10px;
        }

            .document-field img {
                vertical-align: middle;
            }


        /* =========================================================
       ACTION BUTTONS
       Hide Print / Edit / Final Submit buttons
       ========================================================= */

        .preview-actions {
            display: none;
        }


        .preview-value-highlight {
            font-weight: 600;
        }




        /* =========================================================
   PRINT - PRESERVE BOOTSTRAP COLUMN LAYOUT
   ========================================================= */

        @media print {

            .section-title {
                background-color: #d8d8d8 !important;
                -webkit-print-color-adjust: exact !important;
                print-color-adjust: exact !important;
            }

            .row {
                display: flex !important;
                flex-wrap: wrap !important;
                margin-left: -15px !important;
                margin-right: -15px !important;
            }

            /* col-md-12 */
            .col-md-12 {
                flex: 0 0 100% !important;
                max-width: 100% !important;
            }

            /* col-md-9 */
            .col-md-9 {
                flex: 0 0 75% !important;
                max-width: 75% !important;
            }

            /* col-md-6 */
            .col-md-6 {
                flex: 0 0 50% !important;
                max-width: 50% !important;
            }

            /* col-md-4 */
            .col-md-4 {
                flex: 0 0 33.333333% !important;
                max-width: 33.333333% !important;
            }

            /* col-md-3 */
            .col-md-3 {
                flex: 0 0 25% !important;
                max-width: 25% !important;
            }

            [class*="col-md-"] {
                position: relative;
                min-height: 1px;
                padding-left: 15px !important;
                padding-right: 15px !important;
                box-sizing: border-box;
            }

            .vadi-card {
                page-break-inside: avoid;
            }

            .preview-section {
                page-break-inside: auto;
            }

            .preview-field {
                padding-top: 5px;
                padding-bottom: 5px;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <div class="print-container">

            <!-- Header -->
            <div class="application-header">

                <h3>भूमि विवाद समाधान  </h3>

                <h5>आवेदन पत्र </h5>

            </div>

            <!-- Application Number -->
            <div class="application-number">
                आवेदन संख्या :
                <asp:Label ID="lblApplicationNo" runat="server" />
            </div>

            <div class="application-number">
                आवेदन तिथि :<asp:Label ID="lblAppDate" runat="server" CssClass="preview-label"> </asp:Label>

            </div>

            <div class="preview-section">
                <!-- Vadi Details -->
                <div class="section-title">
                    वादी का विवरण
                </div>

                <asp:Repeater ID="rptVadi" runat="server">

                    <HeaderTemplate>
                        <div class="vadi-list">
                    </HeaderTemplate>

                    <ItemTemplate>

                        <div class="vadi-card">

                            <div class="vadi-header">
                                वादी <%# Container.ItemIndex + 1 %>
                            </div>

                            <div class="row">


                                <div class="col-md-4 preview-field">
                                    <span class="preview-label">वादी का नाम :</span>
                                    <span class="preview-value">
                                        <%# Eval("NameAsPerAadhaar") %>
                                    </span>
                                </div>


                                <div class="col-md-4 preview-field">
                                    <span class="preview-label">पिता/पति का नाम :</span>
                                    <span class="preview-value">
                                        <%# Eval("Vadi_Father_Husband_Name") %>
                                    </span>
                                </div>


                                <div class="col-md-4 preview-field">
                                    <span class="preview-label">लिंग :</span>
                                    <span class="preview-value">
                                        <%# Convert.ToString(Eval("SexAsPerAadhaar")) == "F" ? "महिला" : "पुरुष" %>
                                    </span>
                                </div>

                            </div>

                            <div class="row">


                                <div class="col-md-4 preview-field">
                                    <span class="preview-label">जन्म वर्ष :</span>
                                    <span class="preview-value">
                                        <%# Eval("YearOfBirthAsPerAadhaar") %>
                                    </span>
                                </div>


                                <div class="col-md-4 preview-field">
                                    <span class="preview-label">मोबाइल संख्या :</span>
                                    <span class="preview-value">
                                        <%# Eval("Vadi_MobileNo") %>
                                    </span>
                                </div>


                                <div class="col-md-4 preview-field">
                                    <span class="preview-label">क्षेत्र का प्रकार :</span>
                                    <span class="preview-value">
                                        <%# Eval("area_type") %>
                                    </span>
                                </div>

                            </div>

                            <div class="row">


                                <div class="col-md-4 preview-field">
                                    <span class="preview-label">जिला :</span>
                                    <span class="preview-value">
                                        <%# Eval("dist") %>
                                    </span>
                                </div>


                                <div class="col-md-4 preview-field">
                                    <span class="preview-label">अनुमंडल :</span>
                                    <span class="preview-value">
                                        <%# Eval("sub_division") %>
                                    </span>
                                </div>


                                <div class="col-md-4 preview-field">
                                    <span class="preview-label">अंचल :</span>
                                    <span class="preview-value">
                                        <%# Eval("block") %>
                                    </span>
                                </div>

                            </div>

                            <div class="row">


                                <div class="col-md-4 preview-field">
                                    <span class="preview-label">थाना :</span>
                                    <span class="preview-value">
                                        <%# Eval("thana") %>
                                    </span>
                                </div>


                                <div class="col-md-4 preview-field">
                                    <span class="preview-label">ग्राम पंचायत :</span>
                                    <span class="preview-value">
                                        <%# Eval("panchayt") %>
                                    </span>
                                </div>


                                <div class="col-md-4 preview-field">
                                    <span class="preview-label">राजस्व ग्राम :</span>
                                    <span class="preview-value">
                                        <%# Eval("village") %>
                                    </span>
                                </div>

                            </div>

                            <div class="row">

                                <!-- Ward -->
                                <div class="col-md-4 preview-field">
                                    <span class="preview-label">वार्ड :</span>
                                    <span class="preview-value">
                                        <%# Eval("WardNo") %>
                                    </span>
                                </div>


                                <div class="col-md-4 preview-field">
                                    <span class="preview-label">विभाग का प्रतिनिधि :</span>
                                    <span class="preview-value">
                                        <%-- <%# Convert.ToString(Eval("is_vadi_from_an_dept")) == "Y"  ? "हां" : "नहीं" %>--%>
                                        <%# Convert.ToString(Eval("IsDepartmentRepresentative")) == "Y"  ? "हां" : "नहीं" %>
                                    </span>
                                </div>

                                <div class="col-md-4 preview-field">
                                    <span class="preview-label">संस्था का प्रतिनिधि :</span>
                                    <span class="preview-value">
                                        <%-- <%# Convert.ToString(Eval("is_vadi_from_an_org")) == "Y"  ? "हां" : "नहीं" %>--%>
                                        <%# Convert.ToString(Eval("IsOrganizationRepresentative")) == "Y"  ? "हां" : "नहीं" %>
                                    </span>
                                </div>

                            </div>

                            <div class="row">


                                <div class="col-md-6 preview-field">
                                    <span class="preview-label">विभाग/संस्था का नाम :</span>
                                    <span class="preview-value">
                                        <%--  <%# Convert.ToString(Eval("is_vadi_from_an_org")) == "Y" ? Eval("vadi_org_name") : Eval("org_type") %>--%>
                                        <%# Convert.ToString(Eval("IsOrganizationRepresentative")) == "Y" ? Eval("DepartmentOrganizationName") : Eval("org_type") %>
                                    </span>
                                </div>


                                <div class="col-md-6 preview-field">
                                    <span class="preview-label">विभाग/संस्था में पदनाम :</span>
                                    <span class="preview-value">
                                        <%-- <%# Convert.ToString(Eval("is_vadi_from_an_org")) == "Y"  ? Eval("vadi_org_pad_name")  : Eval("vadi_dept_pad_name") %>--%>
                                        <%# Convert.ToString(Eval("DepartmentOrganizationName")) == "Y"  ? Eval("DepartmentOrganizationPost")  : Eval("DepartmentOrganizationPost") %>
                                    </span>
                                </div>

                            </div>

                        </div>

                    </ItemTemplate>

                    <FooterTemplate>
                        </div>
                    </FooterTemplate>

                </asp:Repeater>

            </div>

            <div class="preview-section">

                <div class="section-title">
                    भूमि विवाद का विवरण
                </div>

                <div class="preview-section">


                    <div class="row">

                        <div class="col-md-3 preview-field">
                            <span class="preview-label">जिला :</span>
                            <asp:Label ID="lblDistrict" runat="server" CssClass="preview-value" />
                        </div>

                        <div class="col-md-3 preview-field">
                            <span class="preview-label">अनुमंडल :</span>
                            <asp:Label ID="lblSubdivision" runat="server" CssClass="preview-value" />
                        </div>

                        <div class="col-md-3 preview-field">
                            <span class="preview-label">अंचल :</span>
                            <asp:Label ID="lblBlock" runat="server" CssClass="preview-value" />
                        </div>

                        <div class="col-md-3 preview-field">
                            <span class="preview-label">थाना :</span>
                            <asp:Label ID="lblPolice_Station" runat="server" CssClass="preview-value" />
                        </div>

                    </div>



                    <div class="row">

                        <div class="col-md-3 preview-field">
                            <span class="preview-label">क्षेत्र का प्रकार :</span>
                            <asp:Label ID="lblAreaType" runat="server" CssClass="preview-value" />
                        </div>

                        <div class="col-md-3 preview-field">
                            <span class="preview-label">ग्राम पंचायत :</span>
                            <asp:Label ID="lblPanchayatName" runat="server" CssClass="preview-value" />
                        </div>

                        <div class="col-md-3 preview-field">
                            <span class="preview-label">राजस्व ग्राम :</span>
                        </div>

                        <div class="col-md-3 preview-field">
                            <asp:Label ID="lblVILLNAME" runat="server" CssClass="preview-value" />
                        </div>

                    </div>


                    <div class="row">

                        <div class="col-md-3 preview-field">
                            <span class="preview-label">वार्ड :</span>
                            <asp:Label ID="lblWARDNAME" runat="server" CssClass="preview-value" />
                        </div>

                        <div class="col-md-3 preview-field">
                            <span class="preview-label">विवाद का अद्यतन कारक :</span>
                            <asp:Label ID="lblvadi_Vivad_Ka_AadyatanKaran" runat="server" CssClass="preview-value" />
                        </div>

                        <div class="col-md-3 preview-field">
                            <span class="preview-label">राजस्व थाना संख्या :</span>
                            <asp:Label ID="lblvadi_rajashv_sankhaya" runat="server" CssClass="preview-value" />
                        </div>

                        <div class="col-md-3 preview-field">
                            <span class="preview-label">भूमि का प्रकार :</span>
                            <asp:Label ID="lblVadi_BhumiKaPrakar" runat="server" CssClass="preview-value" />
                        </div>

                    </div>



                    <div class="row">

                        <div class="col-md-3 preview-field">
                            <span class="preview-label">सरकारी भूमि का प्रकार :</span>
                        </div>

                        <div class="col-md-3 preview-field">
                            <asp:Label ID="lblvadi_sarkari_bhumi_ka_prakar" runat="server" CssClass="preview-value" />
                        </div>

                        <div class="col-md-3 preview-field">
                            <span class="preview-label">सरकारी भूमि का प्रकार (अगर अन्य है) :  </span>
                        </div>

                        <div class="col-md-3 preview-field">
                            <asp:Label ID="lblvadi_Sarkari_bhumi_ka_Prakar_ager_anya" runat="server" CssClass="preview-value" />
                        </div>

                    </div>


                    <div class="row">

                        <div class="col-md-3 preview-field">
                            <span class="preview-label">भूमि विवाद का प्रकार :</span>

                            <asp:Label ID="lblBhumiKa_VivadPrakar" runat="server" CssClass="preview-value" />
                        </div>

                        <div class="col-md-3 preview-field">

                            <span class="preview-label">भूमि विवाद का प्रकार (अगर अन्य है) : </span>

                        </div>

                        <div class="col-md-3 preview-field" id="div_Preview_vadi_Bhumivivad_Prakar_Anaya" runat="server">

                            <asp:Label ID="lblvadi_Bhumivivad_Prakar_Anaya" runat="server" CssClass="preview-value" />

                        </div>

                    </div>


                    <!-- Vadi's Description -->
                    <div class="row">

                        <div class="col-md-3 preview-field">
                            <span class="preview-label">वादी द्वारा भूमि विवाद का संक्षिप्त विवरण : </span>
                        </div>

                        <div class="col-md-9 preview-field">

                            <asp:Label ID="lblVadiKabhumiVivaran" runat="server" CssClass="preview-value" />

                        </div>

                    </div>


                    <!-- Prativadi's Description -->
                    <div class="row">

                        <div class="col-md-3 preview-field">
                            <span class="preview-label">प्रतिवादी द्वारा भूमि विवाद का संक्षिप्त विवरण : </span>
                        </div>

                        <div class="col-md-9 preview-field">

                            <asp:Label ID="lblPrativadiKabhumiVivaran" runat="server" CssClass="preview-value" />

                        </div>

                    </div>


                </div>

            </div>

            <div class="preview-section">

                <div class="section-title">
                    प्रतिवादी का विवरण
                </div>

                <asp:Repeater ID="rptPratiwadi" runat="server">

                    <HeaderTemplate>
                        <div class="pratiwadi-list">
                    </HeaderTemplate>

                    <ItemTemplate>

                        <div class="pratiwadi-card">

                            <!-- Pratiwadi Header -->
                            <div class="pratiwadi-header">
                                प्रतिवादी <%# Container.ItemIndex + 1 %>
                            </div>


                            <div class="row">

                                <div class="col-md-4 preview-field">
                                    <span class="preview-label">प्रतिवादी का नाम :</span>
                                    <span class="preview-value">
                                        <%# Eval("pratiVadi_Name") %>
                                    </span>
                                </div>

                                <div class="col-md-4 preview-field">
                                    <span class="preview-label">पिता/पति का नाम :</span>
                                    <span class="preview-value">
                                        <%# Eval("pratiVadi_Father_Husband_Name") %>
                                    </span>
                                </div>

                                <div class="col-md-4 preview-field">
                                    <span class="preview-label">मोबाइल संख्या :</span>
                                    <span class="preview-value">
                                        <%# Eval("pratiVadi_MobileNo") %>
                                    </span>
                                </div>

                            </div>


                            <div class="row">

                                <div class="col-md-4 preview-field">
                                    <span class="preview-label">जिला :</span>
                                    <span class="preview-value">
                                        <%# Eval("dist") %>
                                    </span>
                                </div>

                                <div class="col-md-4 preview-field">
                                    <span class="preview-label">अनुमंडल :</span>
                                    <span class="preview-value">
                                        <%# Eval("sub_division") %>
                                    </span>
                                </div>

                                <div class="col-md-4 preview-field">
                                    <span class="preview-label">अंचल :</span>
                                    <span class="preview-value">
                                        <%# Eval("block") %>
                                    </span>
                                </div>

                            </div>

                            <div class="row">

                                <div class="col-md-4 preview-field">
                                    <span class="preview-label">थाना :</span>
                                    <span class="preview-value">
                                        <%# Eval("thana") %>
                                    </span>
                                </div>

                                <div class="col-md-4 preview-field">
                                    <span class="preview-label">क्षेत्र का प्रकार :</span>
                                    <span class="preview-value">
                                        <%# Eval("area_type") %>
                                    </span>
                                </div>

                                <div class="col-md-4 preview-field">
                                    <span class="preview-label">ग्राम पंचायत :</span>
                                    <span class="preview-value">
                                        <%# Eval("panchayt") %>
                                    </span>
                                </div>

                            </div>

                            <div class="row">

                                <div class="col-md-4 preview-field">
                                    <span class="preview-label">राजस्व ग्राम :</span>
                                    <span class="preview-value">
                                        <%# Eval("village") %>
                                    </span>
                                </div>

                                <div class="col-md-4 preview-field">
                                    <span class="preview-label">वार्ड :</span>
                                    <span class="preview-value">
                                        <%# Eval("WardNo") %>
                                    </span>
                                </div>

                            </div>

                            <div class="row">

                                <div class="col-md-4 preview-field">
                                    <span class="preview-label">संस्था का प्रतिनिधि :</span>
                                    <span class="preview-value">
                                        <%-- <%# Convert.ToString(Eval("is_pratiVadi_from_an_org")) == "Y" ? "हां" : "नहीं" %>--%>
                                        <%# Convert.ToString(Eval("IsOrganizationRepresentative")) == "Y" ? "हां" : "नहीं" %>
                                    </span>
                                </div>

                                <div class="col-md-4 preview-field">
                                    <span class="preview-label">विभाग का प्रतिनिधि :</span>
                                    <span class="preview-value">
                                        <%--  <%# Convert.ToString(Eval("is_pratiVadi_from_an_dept")) == "Y"  ? "हां"  : "नहीं" %>--%>
                                        <%# Convert.ToString(Eval("IsDepartmentRepresentative")) == "Y"  ? "हां"  : "नहीं" %>
                                    </span>
                                </div>

                            </div>

                            <!-- Department / Organization Details -->
                            <div class="row">

                                <div class="col-md-6 preview-field">
                                    <span class="preview-label">विभाग/संस्था का नाम :</span>
                                    <span class="preview-value">
                                        <%-- <%# Convert.ToString(Eval("is_pratiVadi_from_an_org")) == "Y"  ? Eval("pratiVadi_org_name") : Eval("org_type") %>--%>
                                        <%# Convert.ToString(Eval("IsOrganizationRepresentative")) == "Y"  ? Eval("DepartmentOrganizationName") : Eval("DepartmentOrganizationName") %>
                                    </span>
                                </div>

                                <div class="col-md-6 preview-field">
                                    <span class="preview-label">विभाग/संस्था में पदनाम :</span>
                                    <span class="preview-value">
                                        <%--<%# Convert.ToString(Eval("is_pratiVadi_from_an_org")) == "Y" ? Eval("pratiVadi_org_pad_name"): Eval("pratiVadi_dept_pad_name") %>--%>
                                        <%# Convert.ToString(Eval("IsOrganizationRepresentative")) == "Y" ? Eval("DepartmentOrganizationPost"): Eval("DepartmentOrganizationPost") %>
                                    </span>
                                </div>

                            </div>

                        </div>

                    </ItemTemplate>

                    <FooterTemplate>
                        </div>
                    </FooterTemplate>

                </asp:Repeater>

            </div>

            <div class="preview-section">
                <div class="section-title">
                    अन्य विवरण
                </div>

                <div class="preview-section">

                    <div class="row">

                        <div class="col-md-6 preview-field">
                            <span class="preview-label">प्रतिवादी को सूचित किया गया है या नहीं ? </span>

                            <asp:Label ID="lblprativadi_ka_suchit" runat="server" CssClass="preview-value" />
                        </div>

                        <!-- Reason -->
                        <div class="col-md-6 preview-field">
                            <span class="preview-label">कारण स्पष्ट करें : </span>

                            <asp:Label ID="lblprativadi_ka_Karan" runat="server" CssClass="preview-value" />
                        </div>

                    </div>


                    <div class="row">


                        <div class="col-md-6 preview-field">
                            <span class="preview-label">माध्यम :  </span>

                            <asp:Label ID="lblprativadi_ka_madham" runat="server" CssClass="preview-value" />
                        </div>

                        <!-- Notice Received -->
                        <div class="col-md-6 preview-field">
                            <span class="preview-label">प्रतिवादी को सूचना तामिला प्राप्त है या नहीं ?  </span>

                            <asp:Label ID="lblprativadi_ka_SuchnaTamil" runat="server" CssClass="preview-value" />
                        </div>

                    </div>


                    <div class="row">

                        <!-- Pratiwadi Present -->
                        <div class="col-md-6 preview-field">
                            <span class="preview-label">प्रतिवादी उपस्थित हुआ है या नहीं ?  </span>

                            <asp:Label ID="lblprativadi_ka_Upashtith" runat="server" CssClass="preview-value" />
                        </div>

                    </div>

                </div>
            </div>

            <div class="preview-section">
                <div class="section-title">
                    भूमि का खाता-खेसरा का विवरण
                </div>

                <asp:Repeater ID="rptBhumiKhataKhesra" runat="server">

                    <HeaderTemplate>
                        <div class="khata-khesra-list">
                    </HeaderTemplate>

                    <ItemTemplate>

                        <div class="person-card">

                            <!-- Record Header -->
                            <div class="person-header">
                                भूमि विवरण <%# Container.ItemIndex + 1 %>
                            </div>


                            <div class="row">

                                <div class="col-md-4 preview-field">
                                    <span class="preview-label">खाता संख्या :
                                    </span>

                                    <span class="preview-value">
                                        <%# Eval("khataNo") %>
                                    </span>
                                </div>

                                <div class="col-md-4 preview-field">
                                    <span class="preview-label">खेसरा संख्या :
                                    </span>

                                    <span class="preview-value">
                                        <%# Eval("khesraNo") %>
                                    </span>
                                </div>

                                <div class="col-md-4 preview-field">
                                    <span class="preview-label">रकबा :
                                    </span>

                                    <span class="preview-value">
                                        <%# Eval("Rakba") %>
                                    </span>
                                </div>

                            </div>


                            <div class="row">

                                <div class="col-md-6 preview-field">
                                    <span class="preview-label">जमीन की किस्म :
                                    </span>

                                    <span class="preview-value">
                                        <%# Eval("LandTypesInKhatianDesc") %>
                                    </span>
                                </div>

                            </div>

                            <!-- Khatian Land Details -->
                            <div class="row">

                                <div class="col-md-12 preview-field">
                                    <span class="preview-label">खतियान में जमीन का विवरण :
                                    </span>

                                    <div class="preview-long-text">
                                        <%# Eval("LandDetailsInKhatian") %>
                                    </div>
                                </div>

                            </div>


                            <div class="row">

                                <div class="col-md-3 preview-field">
                                    <span class="preview-label">उत्तर :
                                    </span>

                                    <span class="preview-value">
                                        <%# Eval("North_chauhaddee") %>
                                    </span>
                                </div>

                                <div class="col-md-3 preview-field">
                                    <span class="preview-label">दक्षिण :
                                    </span>

                                    <span class="preview-value">
                                        <%# Eval("South_chauhaddee") %>
                                    </span>
                                </div>

                                <div class="col-md-3 preview-field">
                                    <span class="preview-label">पूर्व :
                                    </span>

                                    <span class="preview-value">
                                        <%# Eval("East_chauhaddee") %>
                                    </span>
                                </div>

                                <div class="col-md-3 preview-field">
                                    <span class="preview-label">पश्चिम :
                                    </span>

                                    <span class="preview-value">
                                        <%# Eval("West_chauhaddee") %>
                                    </span>
                                </div>

                            </div>

                        </div>

                    </ItemTemplate>

                    <FooterTemplate>
                        </div>
                    </FooterTemplate>

                </asp:Repeater>
            </div>

            <div class="preview-section">
                <div class="section-title">
                    वादी द्वारा प्रस्तुत साक्ष्य का विवरण
                </div>

                <asp:Repeater ID="rptVadiEvidence" runat="server">

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


                            </div>

                        </div>

                    </ItemTemplate>

                    <FooterTemplate>
                        </div>
                    </FooterTemplate>

                </asp:Repeater>

            </div>

            <div class="preview-section">
                <div class="section-title">
                    प्रतिवादी द्वारा प्रस्तुत साक्ष्य का विवरण
                </div>

                <asp:Repeater ID="rptPratiwadiEvidence" runat="server">

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
                                        <%# Convert.ToString(Eval("evidence_id")) != "9" ? Eval("evidence_name") : Eval("evidence_any_name") %>
                                    </span>

                                </div>

                            </div>

                        </div>

                    </ItemTemplate>

                    <FooterTemplate>
                        </div>
                    </FooterTemplate>

                </asp:Repeater>
            </div>

            <div class="preview-section">
                <div class="section-title">
                    राजस्व अधिकारी / पुलिस पदाधिकारी / हल्का कर्मचारी द्वारा प्रस्तुत साक्ष्य का विवरण
                </div>

                <div class="preview-section">

                    <div class="row">

                        <div class="col-md-4 preview-field">
                            <span class="preview-label">पुलिस पदाधिकारी द्वारा समर्पित जाँच प्रतिवेदन की संक्षिप्त विवरणी :  </span>
                        </div>

                        <div class="col-md-8 preview-field">
                            <asp:Label ID="lblPoliceAdhikariVivarni" runat="server" CssClass="preview-value" />
                        </div>

                    </div>


                    <!-- Halka Karmchari / Revenue Officer Report -->
                    <div class="row">

                        <div class="col-md-4 preview-field">
                            <span class="preview-label">हल्का कर्मचारी / राजस्व अधिकारी द्वारा समर्पित जाँच प्रतिवेदन की संक्षिप्त विवरणी : </span>
                        </div>

                        <div class="col-md-8 preview-field">
                            <asp:Label ID="lblHalkaKarmchariVivarni" runat="server" CssClass="preview-value" />
                        </div>

                    </div>


                    <!-- Disputed Land Measurement -->
                    <div class="row">

                        <div class="col-md-4 preview-field">
                            <span class="preview-label">विवादित भू-खंड की मापी : </span>
                        </div>

                        <div class="col-md-8 preview-field">
                            <asp:Label ID="lblVivaditBhukandKiMapiKaReasonHai" runat="server" CssClass="preview-value" />
                        </div>

                    </div>


                    <!-- Measurement Status -->
                    <div class="row">

                        <div class="col-md-4 preview-field">
                            <span class="preview-label">मापी : </span>
                        </div>

                        <div class="col-md-8 preview-field">
                            <asp:Label ID="lblMapiValue" runat="server" CssClass="preview-value" />
                        </div>

                    </div>


                    <!-- Reason for No Measurement -->
                    <div class="row">

                        <div class="col-md-4 preview-field">
                            <span class="preview-label">विवादित भू-खंड की मापी नहीं होने का कारण :  </span>
                        </div>

                        <div class="col-md-8 preview-field">
                            <asp:Label ID="lblVivaditBhumiKaMapiNahiHoneKaKaran" runat="server" CssClass="preview-value" />
                        </div>

                    </div>


                    <!-- Scheduled Measurement Date -->
                    <div class="row">

                        <div class="col-md-4 preview-field">
                            <span class="preview-label">मापी के लिए निर्धारित तिथि :  </span>
                        </div>

                        <div class="col-md-8 preview-field">
                            <asp:Label ID="lblMapiKeNirdharnKiThithiValue" runat="server" CssClass="preview-value" />
                        </div>

                    </div>


                </div>
            </div>

            <div class="preview-section">
                <div class="section-title">
                    भूमि विवाद से संबंधित घटना / वारदात का विवरण
                </div>

                <div class="preview-section">


                    <div class="row">

                        <div class="col-md-6 preview-field">

                            <span class="preview-label">प्राथमिकी / अप्राथमिकी / सनहा दर्ज है ? </span>

                            <asp:Label ID="lblPrathamikHai" runat="server" CssClass="preview-value" />

                        </div>

                    </div>


                    <!-- Incident Records -->
                    <asp:Repeater ID="rptBhumiVivAdIncident" runat="server">

                        <HeaderTemplate>
                            <div class="incident-list">
                        </HeaderTemplate>

                        <ItemTemplate>

                            <div class="incident-card">

                                <!-- Incident Header -->
                                <div class="incident-header">
                                    घटना / वारदात <%# Container.ItemIndex + 1 %>
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


                                <!-- Sanha Details -->
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

            <div class="preview-section">
                <div class="section-title">
                    न्यायालय में प्रक्रियाधीन वाद का विवरण
                </div>

                <div class="preview-section">


                    <div class="row">

                        <div class="col-md-6 preview-field">

                            <span class="preview-label">प्रक्रियाधीन वाद का विवरण उपलब्ध है ?
                            </span>

                            <asp:Label ID="lblPrakiriyadhinVadAvailable" runat="server" CssClass="preview-value" />

                        </div>

                    </div>

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


                                <!-- Location -->
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


                                <!-- Case Number -->
                                <div class="row">

                                    <div class="col-md-6 preview-field">

                                        <span class="preview-label">वाद संख्या / वर्ष :   </span>

                                        <span class="preview-value">
                                            <%# Eval("vaadi_ki_vaad_sankhya_varsh") %>
                                        </span>

                                    </div>

                                </div>


                                <!-- Parties -->
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

            <div class="preview-section">
                <div class="section-title">
                    अंचलाधिकारी एवं थानाध्यक्ष द्वारा भूमि विवाद के निराकरण हेतु कृत कारवाई का विवरण
                </div>

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



                </div>
            </div>
        </div>

    </form>
    <script type="text/javascript">

        window.onload = function () {

            window.print();

        };

    </script>
</body>
</html>
