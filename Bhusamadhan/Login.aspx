<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Bhusamadhan.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>भू-समाधान | बिहार सरकार</title>
    <link href="images/bihar_homedept_logo.png" rel="icon" />

    <link href="assets/vendor/fontawesome-free-6.1.1/css/all.min.css" rel="stylesheet" />
    <%--<link href="assets/css/ruang-admin.min.css" rel="stylesheet" />--%>
    <link href="assets/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        @import url('https://fonts.googleapis.com/css2?family=Poppins:wght@500&display=swap');
    </style>

    <%--<style>
        #ll1 {
            list-style: none;
        }
    </style>--%>
    <%--  <style>
        .carousel .carousel-indicators li {
            background-color: #fff;
            background-color: rgba(70,70,70,.25);
        }

        .carousel .carousel-indicators .active {
            background-color: #444;
        }


        h1 {
            margin: 60px auto;
            text-align: center;
        }

            h1 > small {
                color: #999;
            }

        img {
            width: 100%;
        }

        .carousel .carousel-caption {
            color: #999;
        }
    </style>--%>
    <link href="assets/css/customized.css" rel="stylesheet" />

</head>

<body style="font-family: 'Poppins', sans-serif;">

    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div class="top-header">

            <div class="container-fluid">

                <div class="d-flex justify-content-between align-items-center flex-wrap">

                    <div>

                        <a href="http://homeonline.bih.nic.in/" target="_blank"><i class="fa fa-university"></i>Home Department </a>

                        <span class="divider">|</span>

                        <i class="fa fa-envelope"></i>homeonlinebihar@gmail.com

                    </div>

                    <div class="text-tools">

                        <i class="fa fa-book"></i>

                        <asp:LinkButton ID="lnkScreenReader" runat="server" PostBackUrl="~/Public/ScreenReader.aspx">
                            <asp:Literal ID="LtMenuScreenReader" runat="server" Text="<%$Resources:Resource,MenuScreenReader%>" />

                        </asp:LinkButton>

                        <span class="divider">|</span>

                        <i class="fa fa-key"></i>

                        <asp:LinkButton ID="lnkShortcutkey" runat="server" PostBackUrl="~/Public/ShortcutKeys.aspx">
                            <asp:Literal ID="LtMenuSkey" runat="server" Text="<%$Resources:Resource,MenuSkey%>" />

                        </asp:LinkButton>

                        <span class="divider">|</span>

                        <asp:Literal ID="LtMenuTextSize" runat="server" Text="<%$Resources:Resource,MenuTextSize%>" />
                        :

                    <a href="JavaScript:" class="Aplus">
                        <asp:Literal ID="LtTextMax" runat="server" Text="<%$Resources:Resource,TextMax%>" />
                    </a>
                        <a href="JavaScript:" class="reset">

                            <asp:Literal ID="LtTextMin" runat="server" Text="<%$Resources:Resource,TextMin%>" />

                        </a>

                    </div>

                </div>

            </div>

        </div>

        <div class="header-banner">

            <img src="images/bhu_samadhan_header.jpg" alt="Bhu Samadhan Portal" class="img-fluid" />

        </div>
        <%--  <div class="container-fluid">
            <div class="row">
                <div class="col-md-12" style="height: 1px; background-color: #22577E"></div>
            </div>
        </div>--%>


        <nav class="navbar navbar-expand-lg gov-navbar">

            <div class="container-fluid">

                <a class="navbar-brand" href="../Login.aspx"><i class="fa fa-home"></i>

                </a>

                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent">
                    <span class="navbar-toggler-icon"></span>

                </button>

                <div class="collapse navbar-collapse" id="navbarSupportedContent">

                    <ul class="navbar-nav ml-auto">

                        <li class="nav-item active">

                            <a class="nav-link" href="../Login.aspx">Home </a>

                        </li>

                        <li class="nav-item">

                            <a class="nav-link" href="Public/Bhu_Samadhan_format.pdf" target="_blank"><i class="fa fa-download"></i>Entry Format </a>

                        </li>

                        <li class="nav-item">

                            <a class="nav-link" href="Public/BHU_SAMADAHAN.pdf" target="_blank"><i class="fa fa-book"></i>User Manual  </a>

                        </li>

                        <li class="nav-item">

                            <a class="nav-link" href="Public/Helpdesk.aspx"><i class="fa fa-phone"></i>Help Desk </a>

                        </li>

                    </ul>

                </div>

            </div>

        </nav>

        <%-- Login Page Section--%>

        <div class="container-fluid login-section">

            <div class="row">
                <div class="col-md-8">
                    <div id="carouselExampleIndicators" class="carousel slide portal-slider" data-ride="carousel">
                        <ol class="carousel-indicators">
                            <li data-target="#carouselExampleIndicators" data-slide-to="0" class="active"></li>
                            <li data-target="#carouselExampleIndicators" data-slide-to="1"></li>
                            <li data-target="#carouselExampleIndicators" data-slide-to="2"></li>
                        </ol>
                        <div class="carousel-inner">
                            <div class="carousel-item active">
                                <img class="d-block w-100" src="images/Desktop1.jpg" alt="First slide" />
                                <div class="carousel-caption d-none d-md-block">
                                    <h5>भू-समाधान</h5>
                                    <p>गृह-विभाग | बिहार सरकार</p>
                                </div>
                            </div>
                            <div class="carousel-item">
                                <img class="d-block w-100" src="images/Desktop1.jpg" alt="Second slide" />
                                <div class="carousel-caption d-none d-md-block">
                                    <h5>भू-समाधान</h5>
                                    <p>गृह-विभाग | बिहार सरकार</p>
                                </div>
                            </div>
                            <div class="carousel-item">
                                <img class="d-block w-100" src="images/Desktop1.jpg" alt="Third slide" />
                                <div class="carousel-caption d-none d-md-block">
                                    <h5>भू-समाधान</h5>
                                    <p>गृह-विभाग | बिहार सरकार</p>
                                </div>
                            </div>
                        </div>

                        <a class="carousel-control-prev" href="#carouselExampleIndicators" role="button" data-slide="prev">
                            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                            <span class="sr-only">Previous</span>
                        </a>
                        <a class="carousel-control-next" href="#carouselExampleIndicators" role="button" data-slide="next">
                            <span class="carousel-control-next-icon" aria-hidden="true"></span>
                            <span class="sr-only">Next</span>
                        </a>
                    </div>
                </div>

                <div class="col-lg-4">

                    <div class="card login-card">

                        <div class="login-header">
                            <i class="fa fa-user-circle"></i>Official Login
                        </div>

                        <div class="login-body">

                            <!-- Username -->
                            <div class="form-group">

                                <label class="font-weight-bold text-dark">
                                    Username
               
                                </label>

                                <div class="input-group">

                                    <asp:TextBox ID="txtUserid" runat="server" CssClass="form-control" placeholder="Enter Username" autocomplete="off" MaxLength="50"> </asp:TextBox>

                                    <div class="input-group-append">
                                        <span class="input-group-text bg-light">
                                            <i class="fa fa-user text-primary"></i>
                                        </span>
                                    </div>

                                </div>

                                <asp:RequiredFieldValidator ID="rfvUserid" runat="server" ControlToValidate="txtUserid" ErrorMessage="Username is required." Display="Dynamic" CssClass="text-danger small" ValidationGroup="L">* </asp:RequiredFieldValidator>

                            </div>

                            <!-- Password -->
                            <div class="form-group">

                                <label class="font-weight-bold text-dark">
                                    Password
               
                                </label>

                                <div class="input-group">

                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Enter Password"
                                        TextMode="Password" autocomplete="off" ToolTip="Password is required."></asp:TextBox>

                                    <div class="input-group-append">

                                        <button type="button" id="btnTogglePassword" class="btn btn-light">

                                            <i id="togglePassword" class="fa fa-eye"></i>

                                        </button>
                                    </div>

                                </div>

                                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required." Display="Dynamic" CssClass="text-danger small" ValidationGroup="L"> *</asp:RequiredFieldValidator>

                            </div>

                            <%--<label>Captcha</label>--%>

                            <asp:UpdatePanel ID="upCaptcha" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <div class="form-row align-items-center mb-3">

                                        <!-- Captcha TextBox -->
                                        <div class="col-5">

                                            <asp:TextBox ID="txtCaptha" runat="server" CssClass="form-control" placeholder="Enter Code"></asp:TextBox>

                                        </div>

                                        <!-- Captcha Image -->
                                        <div class="col-5 text-center">

                                            <asp:Image ID="imgCaptcha" runat="server" ImageUrl="~/Public/CreateCaptcha.aspx" CssClass="img-fluid border rounded shadow-sm" Style="height: 46px;" />

                                        </div>

                                        <!-- Refresh Button -->
                                        <div class="col-2 text-center">

                                            <asp:ImageButton ID="btnRefreshCaptcha" runat="server" OnClick="btnRefreshCaptcha_Click" CausesValidation="false" ImageUrl="~/images/refresh.png" Width="36" Height="36" />

                                        </div>

                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <asp:Button ID="btnLogin" runat="server" Text="LOGIN" CssClass="btn login-btn" ValidationGroup="L" OnClick="btnLogin_Click" />
                            <div class="text-right mb-3">

                                <a href="Public/ForgotPassword.aspx" class="text-primary font-weight-bold">Forgot Password?</a>

                            </div>

                            <asp:Label ID="lblErrorMsg" runat="server" CssClass="error-msg"></asp:Label>

                        </div>
                    </div>
                </div>
            </div>
        </div>


        <%-- Login Page Section ends here--%>

        <%-- Footer Section--%>
        <footer class="gov-footer">

            <div class="container-fluid">

                <div class="row">

                    <div class="col-12 text-center">

                        <p>

                            <i class="fa fa-university mr-1"></i><strong>Home Department, Government of Bihar</strong> <span class="footer-divider">|</span>

                                <i class="fa fa-copyright mr-1"></i>All Rights Reserved <span class="footer-divider">|</span>

                                <i class="fa fa-desktop mr-1"></i>Designed &amp; Developed by

                             <a href="https://www.nic.in/" target="_blank">National Informatics Centre (NIC), Bihar </a>
                        </p>

                    </div>

                </div>

            </div>

        </footer>


    </form>
    <script src="https://code.jquery.com/jquery-3.5.1.js" type="text/javascript"></script>
    <%--<script src="assets/vendor/jquery/jquery.min.js"></script>--%>
    <script src="assets/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="assets/vendor/jquery-easing/jquery.easing.min.js"></script>
    <%--<script src="assets/vendor/chart.js/Chart.min.js"></script>--%>
    <%-- <script src="assets/js/demo/chart-area-demo.js"></script>--%>
    <script src="assets/vendor/fontawesome-free-6.1.1/js/all.min.js"></script>
    <%--<script src="assets/js/ruang-admin.min.js"></script>--%>
    <%-- <script src="assets/skey1.js"></script>
    <script src="assets/SKey.js"></script>--%>
    <script>
        $('.carousel').carousel()
    </script>

    <%-- <script type="text/javascript">
        var togglePassword = document.querySelector('#togglePassword');
        var password = document.querySelector('#txtPassword');
        togglePassword.addEventListener('click', function (e) {
            const type = password.getAttribute('type') === 'password' ? 'text' : 'password';
            password.setAttribute('type', type);
            this.classList.toggle('fa-eye-slash');
        });
    </script>--%>
    <script>
        $(function () {

            $("#btnTogglePassword").click(function () {

                var input = $("#<%= txtPassword.ClientID %>");
                var icon = $("#togglePassword");

                if (input[0].type === "password") {

                    input[0].type = "text";

                    icon.removeClass("fa-eye").addClass("fa-eye-slash");

                }
                else {

                    input[0].type = "password";

                    icon.removeClass("fa-eye-slash").addClass("fa-eye");

                }

            });

        });

    </script>
</body>
</html>
