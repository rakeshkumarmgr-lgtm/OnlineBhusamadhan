<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="Bhusamadhan.Public.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <title>भू-समाधान | बिहार सरकार</title>

    <link rel="icon" href="images/bihar_homedept_logo.png" />

    <!-- Bootstrap 4.6.2 CDN -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css" />

    <!-- Font Awesome CDN -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.2/css/all.min.css" />

    <!-- Google Fonts -->
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@400;500;600&display=swap" rel="stylesheet" />


    <style>
        body {
            font-family: 'Poppins',sans-serif;
            background: #ffffff;
        }


        /* Header Image */

        .header-image {
            width: 100%;
            height: auto;
        }


        /* Navbar */

        .custom-navbar {
            background: linear-gradient( 90deg, #1eb089, #7c49ab );
            min-height: 45px;
        }


            .custom-navbar .navbar-brand {
                color: white;
                font-size: 22px;
            }


            .custom-navbar .nav-link {
                color: white !important;
                font-size: 15px;
                padding: 12px 18px;
            }


                .custom-navbar .nav-link:hover {
                    background: rgba(255,255,255,.25);
                    border-radius: 5px;
                }


        /* Page Heading */

        .page-title {
            background: #f4f6fb;
            border: 1px solid #e5e7eb;
            color: #052033;
            font-size: 20px;
            font-weight: 600;
            padding: 8px;
        }


        /* Screen Reader Table */

        .table-reader th {
            background: #f4f6fb;
            text-align: center;
        }


        .table-reader td,
        .table-reader th {
            border: 1px solid #333;
            padding: 8px;
        }


        .table-reader a {
            color: #0056b3;
        }


            .table-reader a:hover {
                text-decoration: underline;
            }



        /* Footer */

        .footer {
            background: linear-gradient( 90deg, #1eb089, #7c49ab );
            color: white;
            padding: 10px;
            font-size: 14px;
        }


            .footer a {
                color: white;
            }

        /*Forgot Password Section*/

        .card {
            border-radius: 10px;
        }

        .card-header {
            border-radius: 10px 10px 0 0 !important;
        }

        .input-group-text {
            background: #f8f9fa;
        }

        .form-control {
            height: 45px;
        }

        .btn {
            min-width: 150px;
        }

        .togglePassword {
            cursor: pointer;
        }

        .alert {
            font-size: 14px;
        }
    </style>


</head>
<body>
    <form id="form1" runat="server">
        <!-- Header -->

        <img src="../images/bhu_samadhan_header.jpg" class="header-image" alt="Bhu_Samadhan_Header" />



        <!-- Navigation -->

        <nav class="navbar navbar-expand-lg navbar-dark custom-navbar">


            <a class="navbar-brand" href="../Login.aspx"><i class="fa fa-home"></i></a>


            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#mainMenu" aria-label="Toggle Navigation">
                <span class="navbar-toggler-icon"></span>

            </button>



            <div class="collapse navbar-collapse" id="mainMenu">


                <ul class="navbar-nav mr-auto">


                    <li class="nav-item active">

                        <a class="nav-link" href="../Login.aspx">Home
                        </a>

                    </li>



                    <li class="nav-item">

                        <a class="nav-link" href="Bhu_Samadhan_format.pdf" target="_blank">Entry Format </a>

                    </li>



                    <li class="nav-item">

                        <a class="nav-link" href="BHU_SAMADAHAN.pdf" target="_blank">User Manual </a>

                    </li>



                    <li class="nav-item">

                        <a class="nav-link" href="Public/Helpdesk.aspx">Helpdesk

                        </a>

                    </li>


                </ul>


            </div>


        </nav>


        <!-- Main Content -->
        <div class="container-fluid mt-3 mb-3">
            <asp:Panel ID="pnlForgotPassword" runat="server" Visible="true">
                <div class="row">
                    <div class="col-lg-4 offset-lg-4">
                        <div class="card-body pb-0 ">
                            <div class="row">
                                <div class=" d-flex align-items-stretch flex-column">
                                    <div class="card card-outline bg-light d-flex flex-fill">
                                        <div class="card text-center" style="width: 600px;">
                                            <div class="card-header h6 text-white bg-primary">
                                                Find Your Account
                                            </div>
                                            <div class="card-body px-5">
                                                <div class="form-group row">
                                                    <label for="inputUid" class="col-sm-3 col-form-label">UserId</label>
                                                    <div class="col-sm-7">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtUserID" runat="server" CssClass="form-control" placeholder="Userid" autocomplete="off"></asp:TextBox>
                                                            <div class="input-group-append">
                                                                <span class="input-group-text"><i class="fa fa-user"></i></span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                            ControlToValidate="txtUserID" Display="Dynamic"
                                                            ErrorMessage="User ID Required" ForeColor="Red"
                                                            SetFocusOnError="True">required !</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label for="inputAadhar" class="col-sm-3 col-form-label">Contact No</label>
                                                    <div class="col-sm-7">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control" placeholder="Enter Contact No" autocomplete="off" TextMode="Password"></asp:TextBox>
                                                            <div class="input-group-append">
                                                                <span class="input-group-text"><i id="togglePassword" class="fa fa-eye"></i></span>
                                                            </div>
                                                        </div>
                                                        <%--<small class="form-text text-muted text-right">Last 8 digit Aadhar Number
                                            </small>--%>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                            ControlToValidate="txtContactNo" Display="Dynamic"
                                                            ErrorMessage="required" ForeColor="Red"
                                                            SetFocusOnError="True">required !</asp:RequiredFieldValidator>

                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                            ControlToValidate="txtContactNo" Display="Dynamic"
                                                            ErrorMessage="Only numeric allowed." ForeColor="#CC0000"
                                                            ValidationExpression="^([0-9]{10})$" SetFocusOnError="True">Invalid Mobile Number
                                                        </asp:RegularExpressionValidator>

                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-sm-3 col-form-label">
                                                        <%--<asp:Image ID="imgCaptcha" runat="server" />--%>
                                                        <asp:Label ID="lblCaptchaImage" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Maroon"></asp:Label>
                                                    </label>
                                                    <div class="col-sm-7">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtCaptchaImage" runat="server" CssClass="form-control" placeholder="Enter Text Shown"
                                                                AutoComplete="off" ToolTip="Enter Text Shown"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <center>
                                                    <div class="col text-center">
                                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-info" OnClick="btnSubmit_Click" />
                                                    </div>
                                                </center>

                                                <div>
                                                    <asp:Label ID="lblErrMsg" runat="server" Font-Bold="True" ForeColor="#cc0000"></asp:Label>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </asp:Panel>

            <br />
            <asp:Panel ID="pnlResetPwd" runat="server" Visible="false">

                <div class="row justify-content-center mt-5">

                    <div class="col-lg-5 col-md-7 col-sm-10">

                        <div class="card shadow-lg border-0">

                            <div class="card-header bg-primary text-white text-center">

                                <h4 class="mb-0"> <i class="fa fa-lock"></i> Reset Password </h4>

                            </div>

                            <div class="card-body">

                                <asp:HiddenField ID="hdUid" runat="server" />
                                <asp:HiddenField ID="hdUserID" runat="server" />

                                <!-- New Password -->

                                <div class="form-group">

                                    <label class="font-weight-bold"> New Password </label>

                                    <div class="input-group">

                                        <div class="input-group-prepend"> <span class="input-group-text"> <i class="fa fa-lock"></i> </span> </div>

                                        <asp:TextBox ID="txtNewPwd" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter New Password">
                                        </asp:TextBox>

                                        <div class="input-group-append">

                                            <span class="input-group-text">

                                                <i class="fa fa-eye togglePassword" toggle="#<%=txtNewPwd.ClientID%>"></i>

                                            </span>

                                        </div>

                                    </div>

                                    <asp:RequiredFieldValidator ID="rfvNewPwd"  runat="server"  ControlToValidate="txtNewPwd"  CssClass="text-danger small" ValidationGroup="a" ErrorMessage="New Password is required" />

                                </div>

                                <!-- Confirm Password -->

                                <div class="form-group">

                                    <label class="font-weight-bold">
                                        Confirm Password

                                    </label>

                                    <div class="input-group">

                                        <div class="input-group-prepend">

                                            <span class="input-group-text"> <i class="fa fa-lock"></i>  </span>

                                        </div>

                                        <asp:TextBox ID="txtConfirmPwd" runat="server"  CssClass="form-control" TextMode="Password" placeholder="Confirm Password">  </asp:TextBox>

                                        <div class="input-group-append">

                                            <span class="input-group-text">

                                                <i class="fa fa-eye togglePassword" toggle="#<%=txtConfirmPwd.ClientID%>"></i>

                                            </span>

                                        </div>

                                    </div>

                                    <asp:CompareValidator
                                        ID="cvResetMatch"
                                        runat="server"
                                        ControlToCompare="txtNewPwd"
                                        ControlToValidate="txtConfirmPwd"
                                        CssClass="text-danger small"
                                        ValidationGroup="a"
                                        ErrorMessage="Passwords do not match." />

                                </div>

                                <div class="alert alert-light border">

                                    <b>Password Requirements</b>

                                    <ul class="mb-0 pl-3">

                                        <li>Minimum 8 characters</li>

                                        <li>One uppercase letter</li>

                                        <li>One lowercase letter</li>

                                        <li>One number</li>

                                        <li>One special character</li>

                                    </ul>

                                </div>

                                <div class="text-center">

                                    <asp:Button ID="btnChangePwd" runat="server" Text="Change Password" CssClass="btn btn-primary px-4" ValidationGroup="a" OnClick="btnChangePwd_Click" /> &nbsp;

                                    <asp:Button ID="btnHome" runat="server" Text="Home" CssClass="btn btn-outline-secondary px-4" OnClick="btnHome_Click" />

                                </div>

                                <div class="mt-3 text-center">

                                    <asp:Label ID="lblChangePwdMsg" runat="server" CssClass="font-weight-bold"> </asp:Label>

                                </div>

                            </div>

                        </div>

                    </div>

                </div>

            </asp:Panel>

        </div>

        <div class="footer text-center">
            Home Department, Govt. of Bihar |All Rights Reserved | Software solution provided and designed by

             <a href="https://www.nic.in/" target="_blank"><b>NIC-Bihar</b></a>


        </div>
    </form>

    <script>

        $(function () {

            $(".togglePassword").click(function () {

                var input = $($(this).attr("toggle"));

                if (input.attr("type") == "password") {

                    input.attr("type", "text");

                    $(this).removeClass("fa-eye");

                    $(this).addClass("fa-eye-slash");

                }
                else {

                    input.attr("type", "password");

                    $(this).removeClass("fa-eye-slash");

                    $(this).addClass("fa-eye");

                }

            });

        });

    </script>
</body>
</html>
