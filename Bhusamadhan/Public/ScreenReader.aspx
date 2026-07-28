<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScreenReader.aspx.cs" Inherits="Bhusamadhan.ScreenReader" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <title>भू-समाधान | बिहार सरकार</title>

    <link rel="icon" href="images/bihar_homedept_logo.png" />

    <!-- Bootstrap 4.6.2 CDN -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css"/>

    <!-- Font Awesome CDN -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.2/css/all.min.css"/>

    <!-- Google Fonts -->
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@400;500;600&display=swap" rel="stylesheet"/>


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


            <div class="page-title text-center">
                Screen Reader Access

            </div>



            <p class="mt-3 text-justify">
                The Department of Information Technology website complies with
                World Wide Web Consortium (W3C) Web Content Accessibility Guidelines
                (WCAG) 2.0 level AA. This will enable people with visual impairments
                to access the website using assistive technologies such as screen readers.
                The information of the website is accessible with different screen readers,
                such as JAWS.

            </p>



            <p class="font-weight-bold">
                Following table lists the information about different screen readers:

            </p>



            <div class="table-responsive">


                <table class="table table-reader">


                    <thead>

                        <tr>

                            <th>Screen Reader
                            </th>

                            <th>Website
                            </th>

                            <th>Free / Commercial
                            </th>

                        </tr>

                    </thead>



                    <tbody>


                        <tr>

                            <td>Non Visual Desktop Access (NVDA)
                            </td>

                            <td>
                                <a href="https://www.nvaccess.org/" target="_blank">https://www.nvaccess.org/

                                </a>
                            </td>

                            <td>Free
                            </td>


                        </tr>



                        <tr>

                            <td>Screen Access For All (SAFA)
                            </td>

                            <td>
                                <a href="http://www.nabdelhi.org/NAB_SAFA.htm" target="_blank">NAB SAFA

                                </a>
                            </td>

                            <td>Free
                            </td>

                        </tr>

                        <tr>

                            <td>JAWS
                            </td>

                            <td>

                                <a href="https://www.freedomscientific.com/products/software/jaws/" target="_blank">Freedom Scientific JAWS

                                </a>

                            </td>


                            <td>Commercial
                            </td>


                        </tr>

                        <tr>

                            <td>Supernova
                            </td>


                            <td>

                                <a href="https://yourdolphin.com/supernova" target="_blank">Supernova

                                </a>

                            </td>


                            <td>Commercial
                            </td>


                        </tr>


                    </tbody>


                </table>


            </div>


        </div>


        <!-- Footer -->

        <div class="footer text-center">
            © Copyright 2022 |Home Department, Govt. of Bihar |All Rights Reserved | Software solution provided and designed by

            <a href="https://www.nic.in/" target="_blank">

                <b>NIC-Bihar</b>

            </a>


        </div>

    </form>


    <!-- jQuery CDN -->

    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>

    <!-- Bootstrap 4 JS CDN -->

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/js/bootstrap.bundle.min.js"></script>

</body>
</html>
