<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApplicationStepManagement.aspx.cs" Inherits="Bhusamadhan.LandDispute.Entry.ApplicationStepManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .step-list {
            display: flex;
            justify-content: center;
            flex-wrap: wrap;
            gap: 10px;
        }

            .step-list input[type="radio"] {
                margin-right: 5px;
            }


            .step-list label {
                margin-right: 20px;
                font-weight: bold;
                cursor: pointer;
            }

            .step-list input[type="radio"]:checked + label {
                background-color: #007bff;
                color: white;
            }
    </style>


    <script type="text/javascript">

        function confirmStepUpdate() {

            var selected = document.querySelector(
                'input[name$="rblSteps"]:checked'
            );

            if (!selected) {
                alert("कृपया नया Step चुनें।");
                return false;
            }

            return confirm(
                "क्या आप Application का CurrentStep बदलना चाहते हैं?"
            );
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div class="container-fluid">

        <!-- Page Header -->
        <div class="card">
            <div class="card-header bg-primary text-white">
                <h5 class="mb-0">Application Step Management
                </h5>
            </div>

            <div class="card-body">

                <!-- Search -->
                <div class="row">

                    <div class="col-md-5">
                        <label class="font-weight-bold">
                            Application ID / Application No
                       
                        </label>

                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="a_id या Application No दर्ज करें"></asp:TextBox>
                    </div>

                    <div class="col-md-2 d-flex align-items-end">

                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />

                    </div>

                </div>

                <asp:Label ID="lblMessage" runat="server" CssClass="d-block mt-3">  </asp:Label>


                <!-- Application Details -->
                <asp:Panel ID="pnlApplication" runat="server" Visible="false" CssClass="mt-4">

                    <div class="card">

                        <div class="card-header bg-light">
                            <strong>Application Details</strong>
                        </div>

                        <div class="card-body">

                            <div class="row">

                                <div class="col-md-3 mb-3">
                                    <label>Application ID</label>
                                    <asp:Label ID="lblAId" runat="server" CssClass="form-control bg-light"> </asp:Label>
                                </div>

                                <div class="col-md-3 mb-3">
                                    <label>Application No</label>
                                    <asp:Label ID="lblApplicationNo" runat="server" CssClass="form-control bg-light"> </asp:Label>
                                </div>

                                <div class="col-md-3 mb-3">
                                    <label>User</label>
                                    <asp:Label ID="lblUser" runat="server" CssClass="form-control bg-light"> </asp:Label>
                                </div>

                                <div class="col-md-3 mb-3">
                                    <label>आवेदन की तिथि</label>
                                    <asp:Label ID="lblAavedanKiTithi" runat="server" CssClass="form-control bg-light"> </asp:Label>
                                </div>

                            </div>


                            <div class="row">

                                <div class="col-md-3 mb-3">
                                    <label>District</label>
                                    <asp:Label ID="lblDistrict" runat="server" CssClass="form-control bg-light"> </asp:Label>
                                </div>

                                <div class="col-md-3 mb-3">
                                    <label>Block</label>
                                    <asp:Label ID="lblBlock" runat="server" CssClass="form-control bg-light"> </asp:Label>
                                </div>

                                <div class="col-md-3 mb-3">
                                    <label>Police Station</label>
                                    <asp:Label ID="lblPoliceStation" runat="server" CssClass="form-control bg-light">  </asp:Label>
                                </div>

                                <div class="col-md-3 mb-3">
                                    <label>Area Type</label>
                                    <asp:Label ID="lblAreaType" runat="server" CssClass="form-control bg-light">  </asp:Label>
                                </div>

                            </div>


                            <div class="row">

                                <div class="col-md-3 mb-3">
                                    <label>Village</label>
                                    <asp:Label ID="lblVillage" runat="server" CssClass="form-control bg-light">  </asp:Label>
                                </div>

                                <div class="col-md-3 mb-3">
                                    <label>राजस्व थाना संख्या</label>
                                    <asp:Label ID="lblRajasvThana" runat="server" CssClass="form-control bg-light">  </asp:Label>
                                </div>

                                <div class="col-md-3 mb-3">
                                    <label>भूमि का प्रकार</label>
                                    <asp:Label ID="lblBhumitype" runat="server" CssClass="form-control bg-light"> </asp:Label>
                                </div>

                                <div class="col-md-3 mb-3">
                                    <label>भूमि विवाद प्रकार</label>
                                    <asp:Label ID="lblBhumiVivadType" runat="server" CssClass="form-control bg-light"> </asp:Label>
                                </div>

                            </div>

                        </div>
                    </div>


                    <!-- STEP MANAGEMENT -->
                    <div class="card mt-4">

                        <div class="card-header bg-light">
                            <strong>Application Step Management</strong>
                        </div>

                        <div class="card-body text-center">

                            <div class="mb-3">

                                <span class="font-weight-bold">Current Step:
                                </span>

                                <asp:Label ID="lblCurrentStep" runat="server" CssClass="badge badge-primary" Style="font-size: 18px;">  </asp:Label>

                            </div>


                            <div class="step-container">

                                <asp:RadioButtonList ID="rblSteps" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="step-list">

                                    <asp:ListItem Value="1">Step 1</asp:ListItem>
                                    <asp:ListItem Value="2">Step 2</asp:ListItem>
                                    <asp:ListItem Value="3">Step 3</asp:ListItem>
                                    <asp:ListItem Value="4">Step 4</asp:ListItem>
                                    <asp:ListItem Value="5">Step 5</asp:ListItem>
                                    <asp:ListItem Value="6">Step 6</asp:ListItem>
                                    <asp:ListItem Value="7">Step 7</asp:ListItem>

                                </asp:RadioButtonList>

                            </div>


                            <div class="mt-4">

                                <asp:Button ID="btnUpdateStep" runat="server" Text="Update Current Step" CssClass="btn btn-success px-4" OnClick="btnUpdateStep_Click" OnClientClick="return confirmStepUpdate();" />

                            </div>

                        </div>

                    </div>

                </asp:Panel>

            </div>
        </div>
    </div>
</asp:Content>
