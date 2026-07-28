<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Entry_Page.aspx.cs" Inherits="Bhusamadhan.LandDispute.Entry.Entry_Page" %>
<%@ Register Src="~/LandDispute/Entry/UserControls/UC_Step1.ascx" TagPrefix="uc" TagName="Step1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<style>
        .wizard-steps {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin: 0;
            padding: 0;
            list-style: none;
        }

            .wizard-steps li {
                position: relative;
                flex: 1;
                text-align: center;
            }

                .wizard-steps li:not(:last-child)::after {
                    content: "";
                    position: absolute;
                    top: 22px;
                    left: 50%;
                    width: 100%;
                    height: 4px;
                    background: #d9d9d9;
                    z-index: 0;
                }

        .step {
            display: inline-block;
            position: relative;
            z-index: 2;
            text-decoration: none;
            color: #666;
        }

        .step-no {
            width: 45px;
            height: 45px;
            border-radius: 50%;
            background: #d9d9d9;
            color: #fff;
            line-height: 45px;
            margin: auto;
            font-weight: bold;
            font-size: 18px;
        }

        .step-text {
            display: block;
            margin-top: 8px;
            font-size: 13px;
            font-weight: 600;
        }

        .completed .step-no {
            background: #28a745;
        }

        .current .step-no {
            background: #0d6efd;
        }

        .disabled .step-no {
            background: #c7c7c7;
        }

        .completed {
            color: #28a745;
        }

        .current {
            color: #0d6efd;
        }

        .disabled {
            color: #999;
            pointer-events: none;
        }
    </style>--%>

    <style>
        .wizard-steps {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin: 0;
            padding: 0;
            list-style: none;
        }

            .wizard-steps li {
                position: relative;
                flex: 1;
                text-align: center;
            }

                .wizard-steps li:not(:last-child)::after {
                    content: "";
                    position: absolute;
                    top: 22px;
                    left: 50%;
                    width: 100%;
                    height: 4px;
                    background: #d9d9d9;
                    z-index: 0;
                }

        .step {
            display: inline-block;
            position: relative;
            z-index: 2;
            text-decoration: none;
            color: #666;
        }

        .step-no {
            width: 45px;
            height: 45px;
            border-radius: 50%;
            background: #d9d9d9;
            color: #fff;
            line-height: 45px;
            margin: auto;
            font-weight: bold;
            font-size: 18px;
        }

        .step-text {
            display: block;
            margin-top: 8px;
            font-size: 13px;
            font-weight: 600;
        }

        .completed .step-no {
            background: #28a745;
        }

        .current .step-no {
            background: #0d6efd;
        }

        .disabled .step-no {
            background: #c7c7c7;
        }

        .completed {
            color: #28a745;
        }

        .current {
            color: #0d6efd;
        }

        .disabled {
            color: #999;
            pointer-events: none;
        }
    </style>


    <%--  <style>
        .wizard-steps {
            display: flex;
            justify-content: space-between;
            align-items: flex-start;
            list-style: none;
            padding: 0;
            margin: 0;
            flex-wrap: wrap;
        }

            .wizard-steps li {
                flex: 1;
                text-align: center;
                position: relative;
                min-width: 160px;
            }

                .wizard-steps li:not(:last-child)::after {
                    content: '';
                    position: absolute;
                    top: 18px;
                    right: -50%;
                    width: 100%;
                    height: 2px;
                    background: #d6d6d6;
                    z-index: 0;
                }

        .step {
            display: inline-block;
            text-decoration: none !important;
            position: relative;
            z-index: 2;
            color: #999;
        }

        .step-no {
            width: 38px;
            height: 38px;
            line-height: 38px;
            border-radius: 50%;
            display: block;
            margin: auto;
            background: #d9d9d9;
            color: #fff;
            font-weight: bold;
            font-size: 17px;
        }

        .step-text {
            display: block;
            margin-top: 8px;
            font-size: 14px;
            font-weight: 600;
        }

        .step.active .step-no {
            background: #007bff;
        }

        .step.active {
            color: #007bff;
        }

        .step.completed .step-no {
            background: #28a745;
        }

        .step.completed {
            color: #28a745;
        }

        .step.disabled {
            pointer-events: none;
            cursor: not-allowed;
            color: #bfbfbf;
        }

            .step.disabled .step-no {
                background: #d3d3d3;
            }
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div class="container-fluid">

        <%--   <div class="card shadow-sm border-0 mb-3">

            <div class="card-header bg-white">

                <h6 class="mb-0 font-weight-bold text-primary">
                    <i class="fas fa-edit"></i>
                    आवेदन प्रविष्टि (Application Entry)
                </h6>

            </div>

            <div class="card-body p-2">

                <ul class="wizard-steps">

                    <li>
                        <a id="hstep1" runat="server" class="step active">
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

        </div>--%>

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

        <asp:Panel ID="pnlStep1" runat="server">
            <uc:Step1 ID="Step1" runat="server" />
        </asp:Panel>

        <asp:Panel ID="pnlStep2" runat="server" Visible="false">

            <uc:Step2 ID="Step2" runat="server" />

        </asp:Panel>

        <asp:Panel ID="pnlStep3" runat="server" Visible="false">

            <div class="card mt-3">

                <div class="card-header bg-light">
                    <h5>Step-3 : खाता-खेसरा</h5>
                </div>

                <div class="card-body">

                    <!-- Step-2 Controls -->

                </div>

            </div>

        </asp:Panel>

        <asp:Panel ID="pnlStep4" runat="server" Visible="false">

            <div class="card mt-3">

                <div class="card-header bg-light">
                    <h5>Step-4 : वादी/प्रतिवादी का साक्ष्य</h5>
                </div>

                <div class="card-body">

                    <!-- Step-2 Controls -->

                </div>

            </div>

        </asp:Panel>

        <asp:Panel ID="pnlStep5" runat="server" Visible="false">

            <div class="card mt-3">

                <div class="card-header bg-light">
                    <h5>Step-5 : प्रस्तुत साक्ष्य</h5>
                </div>

                <div class="card-body">

                    <!-- Step-2 Controls -->

                </div>

            </div>

        </asp:Panel>

        <asp:Panel ID="pnlStep6" runat="server" Visible="false">

            <div class="card mt-3">

                <div class="card-header bg-light">
                    <h5>Step-6 : घटना एवं न्यायालय</h5>
                </div>

                <div class="card-body">

                    <!-- Step-2 Controls -->

                </div>

            </div>

        </asp:Panel>

        <asp:Panel ID="pnlStep7" runat="server" Visible="false">

            <div class="card mt-3">

                <div class="card-header bg-light">
                    <h5>Step-7 : अंचलाधिकारी एवं थानाध्यक्ष बैठक</h5>
                </div>

                <div class="card-body">

                    <!-- Step-2 Controls -->

                </div>

            </div>

        </asp:Panel>

        <%-- ButtonSection--%>
        <div class="text-center mt-3 mb-4">

            <asp:Button ID="btnPrevious" runat="server" Text="Previous" CssClass="btn btn-secondary" OnClick="btnPrevious_Click" />
            &nbsp;

              <asp:LinkButton ID="LinkBtnPreview" runat="server" CssClass="btn btn-info">&nbsp;Preview<i class="fa fa-eye"></i></asp:LinkButton>&nbsp;
              <asp:Button ID="btnNext" runat="server" Text="Save & Next" CssClass="btn btn-success" OnClick="btnNext_Click" />

        </div>

    </div>

</asp:Content>
