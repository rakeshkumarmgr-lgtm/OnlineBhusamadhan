<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchAppForMetting.aspx.cs" Inherits="Bhusamadhan.LandDispute.Entry.SearchAppForMetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../assets/css/cssEntryPage.css" rel="stylesheet" />

    <style>
        .application-container {
            padding: 15px;
        }

        .application-card {
            border: 0;
            border-radius: 8px;
            overflow: hidden;
        }

        .application-card-header {
            padding: 12px 18px;
            border-bottom: 1px solid #e5e5e5;
        }

        .unfinalized-title {
            font-size: 18px;
            font-weight: 600;
            color: #343a40;
        }

        .application-count {
            font-size: 13px;
            padding: 6px 10px;
        }


        .filter-card {
            margin: 15px;
            border: 1px solid #e2e6ea;
            border-radius: 6px;
            background: #fff;
        }

        .section-header {
            padding: 10px 15px;
            font-size: 15px;
            font-weight: 600;
            color: #495057;
            background: #f8f9fa;
            border-bottom: 1px solid #e2e6ea;
        }

        .filter-card .card-body {
            padding: 18px;
        }

        .filter-item {
            margin-bottom: 14px;
        }

        .form-label {
            display: block;
            margin-bottom: 5px;
            font-size: 13px;
            font-weight: 600;
            color: #495057;
        }

        .form-control {
            height: 36px;
            font-size: 13px;
            border-radius: 4px;
        }

        select.form-control {
            cursor: pointer;
        }

        .filter-button .btn {
            height: 36px;
            font-size: 13px;
            font-weight: 600;
        }



        .search-icon {
            background: #f8f9fa;
            color: #6c757d;
            border-left: 0;
        }

        .input-group .form-control {
            border-right: 0;
        }

            .input-group .form-control:focus {
                box-shadow: none;
                border-color: #80bdff;
            }

        .grid-card-body {
            padding: 0 15px 15px 15px;
        }

        .grid-wrapper {
            width: 100%;
            overflow-x: auto;
            overflow-y: hidden;
            border: 1px solid #dee2e6;
            border-radius: 5px;
        }

        .finalized-grid {
            width: 100%;
            min-width: 1800px;
            margin-bottom: 0;
            font-size: 12px;
            color: #343a40;
        }


            /* Header */

            .finalized-grid thead th {
                padding: 9px 8px;
                background: #f1f3f5;
                color: #343a40;
                font-size: 12px;
                font-weight: 600;
                text-align: center;
                vertical-align: middle;
                white-space: normal;
                min-width: 120px;
                border-color: #dee2e6;
            }


            .finalized-grid tbody td {
                padding: 8px;
                vertical-align: top;
                line-height: 1.45;
                border-color: #dee2e6;
            }

            .finalized-grid tbody tr:hover {
                background-color: #f8f9fa;
            }


        .application-column {
            min-width: 145px;
            width: 145px;
        }

        .application-no {
            font-weight: 700;
            color: #343a40;
            margin-bottom: 6px;
            white-space: nowrap;
        }

        .action-btn {
            font-size: 11px;
            padding: 4px 8px;
            white-space: nowrap;
        }


        .location-cell {
            min-width: 150px;
        }

            .location-cell span,
            .location-cell strong {
                display: block;
                padding: 3px 0;
            }

                .location-cell span + span {
                    border-top: 1px solid #e9ecef;
                }

            .location-cell strong {
                color: #212529;
            }



        .finalized-grid td:nth-child(n+4) {
            min-width: 130px;
        }

        .finalized-grid td {
            word-break: break-word;
        }



        .pagination-outer {
            padding: 12px;
            background: #f8f9fa;
            border-top: 1px solid #dee2e6;
        }

            .pagination-outer table {
                margin: 0 auto;
            }

            .pagination-outer td {
                padding: 2px 4px;
            }

            .pagination-outer a,
            .pagination-outer span {
                display: inline-block;
                min-width: 30px;
                padding: 5px 8px;
                border: 1px solid #dee2e6;
                border-radius: 3px;
                text-align: center;
                font-size: 12px;
                text-decoration: none;
            }

            .pagination-outer a {
                color: #007bff;
                background: #fff;
            }

                .pagination-outer a:hover {
                    background: #007bff;
                    color: #fff;
                }

            .pagination-outer span {
                color: #fff;
                background: #007bff;
                border-color: #007bff;
            }

        /*Responsive*/


        @media (max-width: 767px) {
            .application-container {
                padding: 8px;
            }

            .filter-card {
                margin: 8px;
            }

            .application-card-header {
                padding: 10px 12px;
            }

            .unfinalized-title {
                font-size: 16px;
            }

            .filter-card .card-body {
                padding: 12px;
            }

            .grid-card-body {
                padding: 0 8px 8px;
            }

            .finalized-grid {
                font-size: 11px;
            }

                .finalized-grid thead th {
                    font-size: 11px;
                }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div class="container-fluid application-container">

        <!-- Page Header -->
        <div class="card application-card shadow-sm">

            <div class="card-header application-card-header">
                <div class="d-flex justify-content-between align-items-center">

                    <div class="unfinalized-title">
                        <i class="fa fa-folder-open mr-2"></i>
                        आवेदन का विवरण
                    </div>

                    <asp:Label ID="lblTotal"
                        runat="server"
                        CssClass="badge badge-primary application-count">
                    </asp:Label>

                </div>
            </div>

            <!-- Search / Filter -->
            <div class="filter-card">

                <div class="section-header">
                    <i class="fa fa-filter mr-2"></i>
                    Search &amp; Filter
                </div>

                <div class="card-body">

                    <asp:Label ID="lblMsg"
                        runat="server"
                        CssClass="text-danger d-block mb-3">
                    </asp:Label>

                    <!-- Location Filters -->
                    <div class="row">

                        <div class="col-xl-2 col-lg-3 col-md-4 col-sm-6 filter-item">
                            <label class="form-label">Commissionary</label>
                            <asp:DropDownList ID="ddlCommissionary" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCommissionary_SelectedIndexChanged"></asp:DropDownList>
                        </div>

                        <div class="col-xl-2 col-lg-3 col-md-4 col-sm-6 filter-item">
                            <label class="form-label">District</label>
                            <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                        </div>

                        <div class="col-xl-2 col-lg-3 col-md-4 col-sm-6 filter-item">
                            <label class="form-label">Sub-Division</label>
                            <asp:DropDownList ID="ddlSubDivision" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSubDivision_SelectedIndexChanged"></asp:DropDownList>
                        </div>

                        <div class="col-xl-2 col-lg-3 col-md-4 col-sm-6 filter-item">
                            <label class="form-label">Circle</label>
                            <asp:DropDownList ID="ddlBlock" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged"></asp:DropDownList>
                        </div>

                        <div class="col-xl-2 col-lg-3 col-md-4 col-sm-6 filter-item">
                            <label class="form-label">Police Station</label>
                            <asp:DropDownList ID="ddlPoliceStation" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>

                        <div class="col-xl-2 col-lg-3 col-md-4 col-sm-6 filter-item">
                            <label class="form-label">Panchayat</label>
                            <asp:DropDownList ID="ddlPanchayat" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPanchayat_SelectedIndexChanged"></asp:DropDownList>
                        </div>

                        <div class="col-xl-2 col-lg-3 col-md-4 col-sm-6 filter-item">
                            <label class="form-label">Village</label>
                            <asp:DropDownList ID="ddlVillage" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>

                        <div class="col-xl-2 col-lg-3 col-md-4 col-sm-6 filter-item">
                            <label class="form-label">Ward</label>
                            <asp:DropDownList ID="ddlWard" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>

                        <div class="col-xl-2 col-lg-3 col-md-4 col-sm-6 filter-item filter-button">
                            <label class="form-label d-block">&nbsp;</label>

                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary btn-block" OnClick="btnSearch_Click" />
                        </div>

                    </div>

                    <div class="row mt-2">

                        <div class="col-lg-5 col-md-7 col-sm-12">

                            <label class="form-label">वादी (मोबाइल संख्या) / Application No </label>

                            <div class="input-group">

                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="वादी का मोबाइल नंबर / Application No खोजें..." MaxLength="50" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged"> </asp:TextBox>

                                <div class="input-group-append">
                                    <span class="input-group-text search-icon">
                                        <i class="fa fa-search"></i>
                                    </span>
                                </div>

                            </div>

                        </div>

                    </div>

                </div>
            </div>

            <div class="card-body grid-card-body">

                <div class="grid-wrapper">

                    <asp:GridView ID="gvFinalizedForMeeting" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover table-sm finalized-grid" HeaderStyle-CssClass="thead-light" GridLines="None" AllowPaging="True" PageSize="10" EmptyDataText="कोई Finalized Application उपलब्ध नहीं है।" OnPageIndexChanging="gvFinalized_PageIndexChanging">

                        <Columns>

                            <asp:TemplateField HeaderText="Application No.">

                                <ItemTemplate>

                                    <div class="application-no">
                                        <%# Eval("ApplicationNo") %>
                                    </div>

                                    <asp:LinkButton ID="lnkApplicationNo" runat="server" CssClass="btn btn-sm btn-primary action-btn" Text="Add Meeting" CommandArgument='<%# Eval("a_id") %>' Font-Underline="false" OnClientClick="openwindow(this);" OnClick="lnkView_Click"> </asp:LinkButton>

                                </ItemTemplate>

                                <ItemStyle CssClass="application-column" />

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="जिला / अनुमंडल / अंचल">

                                <ItemTemplate>

                                    <div class="location-cell">
                                        <strong><%# Eval("DISTRICTNAME") %></strong>
                                        <span><%# Eval("Sd_Name_En") %></span>
                                        <span><%# Eval("BlockName") %></span>
                                    </div>

                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="थाना / ग्राम पंचायत / राजस्व ग्राम">

                                <ItemTemplate>

                                    <div class="location-cell">
                                        <strong><%# Eval("Police_Station") %></strong>
                                        <span><%# Eval("PanchayatName") %></span>
                                        <span><%# Eval("VILLNAME") %></span>
                                    </div>

                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="वादी का नाम एवं मोबाइल नंबर">

                                <ItemTemplate>

                                    <div class="location-cell">
                                        <strong><%# Eval("vadi_Name") %></strong>
                                        <span><%# Eval("Vadi_MobileNo") %></span>

                                    </div>

                                </ItemTemplate>

                            </asp:TemplateField>


                            <%--<asp:BoundField DataField="vadi_Name" HeaderText="वादी का नाम" />

                            <asp:BoundField DataField="Vadi_MobileNo" HeaderText="वादी का मोबाइल नंबर"/> --%>

                            <asp:BoundField DataField="pratiVadi_Name" HeaderText="प्रतिवादी का नाम" />

                            <%-- <asp:BoundField DataField="Bhumitype" HeaderText="भूमि का प्रकार" />

                            <asp:BoundField DataField="vivadtype" HeaderText="भूमि विवाद का प्रकार" />--%>

                            <asp:TemplateField HeaderText="भूमि का प्रकार / भूमि विवाद का प्रकार">

                                <ItemTemplate>

                                    <div class="location-cell">
                                        <strong><%# Eval("Bhumitype") %></strong>
                                        <span><%# Eval("vivadtype") %></span>

                                    </div>

                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:BoundField DataField="Bhumi_savedansheelta" HeaderText="भूमि विवाद की संवेदनशीलता" />

                            <asp:BoundField DataField="Meeting_date" HeaderText="बैठक की तिथि" />

                            <asp:BoundField DataField="Description" HeaderText="बैठक का निष्कर्ष" />

                            <asp:BoundField DataField="bhumi_vivad_ka_adyatan_sthiti" HeaderText="विवाद का अद्यतन कारक" />

                            <asp:BoundField DataField="pulis_padadhikari_vivarani" HeaderText="पुलिस पदाधिकारी द्वारा समर्पित जाँच प्रतिवेदन की संक्षिप्त विवरणी" />

                            <asp:BoundField DataField="HalkaKarmchari_vivran" HeaderText="हल्का कर्मचारी / अंचल निरीक्षक द्वारा समर्पित जाँच प्रतिवेदन की संक्षिप्त विवरणी" />

                            <asp:BoundField DataField="vivadit_bhukhand_Mapi_ki_avashyakta_hai" HeaderText="विवादित भू-खंड मापी का विवरणी" />

                            <asp:BoundField DataField="maapee_ke_lie_nirdhaarit_tithi" HeaderText="माप के लिए निर्धारित तिथि" />

                            <asp:BoundField DataField="vivaadit_bhukhand_Mapi_Reason" HeaderText="मापी नहीं होने का कारण" />

                            <asp:BoundField DataField="bhumi_vivad_Vivran_Available" HeaderText="विवाद का प्राथमिकी / अप्राथमिकी" />

                            <asp:BoundField DataField="dispute_in_court_available" HeaderText="न्यायालय में प्रक्रियाधीन वाद" />

                        </Columns>

                        <PagerStyle CssClass="pagination-outer" HorizontalAlign="Center" />

                    </asp:GridView>

                </div>

            </div>

        </div>

    </div>
</asp:Content>
