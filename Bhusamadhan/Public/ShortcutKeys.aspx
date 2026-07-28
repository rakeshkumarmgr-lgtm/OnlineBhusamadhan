<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShortcutKeys.aspx.cs" Inherits="Bhusamadhan.ShortcutKeys" %>

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
    </style>


</head>
<body>
    <form id="form1" runat="server">
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

        <div class="container-fluid mt-3 mb-3">


            <div class="page-title text-center">
                Shortcut Keys

            </div>

            <p class="mt-3 text-justify">
                The Department of Information Technology website complies with World Wide Web Consortium (W3C) Web Content Accessibility Guidelines (WCAG) 2.0 level AA. 
 This will view and explore Web pages using shortcut keys.

            </p>

            <p class="font-weight-bold">
                Following table lists the information about different screen readers:

            </p>

            <div class="table-responsive">


                <table class="table table-reader">


                    <thead>

                        <tr>

                            <th>Shortcut Keys
                            </th>

                            <th>Web Page Name
                            </th>

                        </tr>

                    </thead>



                    <tbody>


                        <tr>

                            <td>Alt+h </td>

                            <td>
                                <a href="~/Login.aspx" style="text-decoration: none;">Home
                                </a>
                            </td>

                        </tr>



                        <tr>

                            <td>Alt+d
                            </td>


                            <td>

                                <a href="~/Login.aspx" style="text-decoration: none;">Official Login</a>

                            </td>



                        </tr>


                    </tbody>


                </table>


            </div>


        </div>


        <!-- Footer -->

        <div class="footer text-center">
            © Copyright 2022 |Home Department, Govt. of Bihar |All Rights Reserved | Software solution provided and designed by

            <a href="https://www.nic.in/" target="_blank"><b>NIC-Bihar</b></a>


        </div>
    </form>

    <script type="text/javascript">
        (function ($) {
            $.fn.countTo = function (options) {
                options = options || {};

                return $(this).each(function () {
                    // set options for current element
                    var settings = $.extend({}, $.fn.countTo.defaults, {
                        from: $(this).data('from'),
                        to: $(this).data('to'),
                        speed: $(this).data('speed'),
                        refreshInterval: $(this).data('refresh-interval'),
                        decimals: $(this).data('decimals')
                    }, options);

                    // how many times to update the value, and how much to increment the value on each update
                    var loops = Math.ceil(settings.speed / settings.refreshInterval),
                        increment = (settings.to - settings.from) / loops;

                    // references & variables that will change with each update
                    var self = this,
                        $self = $(this),
                        loopCount = 0,
                        value = settings.from,
                        data = $self.data('countTo') || {};

                    $self.data('countTo', data);

                    // if an existing interval can be found, clear it first
                    if (data.interval) {
                        clearInterval(data.interval);
                    }
                    data.interval = setInterval(updateTimer, settings.refreshInterval);

                    // initialize the element with the starting value
                    render(value);

                    function updateTimer() {
                        value += increment;
                        loopCount++;

                        render(value);

                        if (typeof (settings.onUpdate) == 'function') {
                            settings.onUpdate.call(self, value);
                        }

                        if (loopCount >= loops) {
                            // remove the interval
                            $self.removeData('countTo');
                            clearInterval(data.interval);
                            value = settings.to;

                            if (typeof (settings.onComplete) == 'function') {
                                settings.onComplete.call(self, value);
                            }
                        }
                    }

                    function render(value) {
                        var formattedValue = settings.formatter.call(self, value, settings);
                        $self.html(formattedValue);
                    }
                });
            };

            $.fn.countTo.defaults = {
                from: 0,               // the number the element should start at
                to: 0,                 // the number the element should end at
                speed: 1000,           // how long it should take to count between the target numbers
                refreshInterval: 100,  // how often the element should be updated
                decimals: 0,           // the number of decimal places to show
                formatter: formatter,  // handler for formatting the value before rendering
                onUpdate: null,        // callback method for every time the element is updated
                onComplete: null       // callback method for when the element finishes updating
            };

            function formatter(value, settings) {
                return value.toFixed(settings.decimals);
            }
        }(jQuery));

        jQuery(function ($) {
            // custom formatting example
            $('.count-number').data('countToOptions', {
                formatter: function (value, options) {
                    return value.toFixed(options.decimals).replace(/\B(?=(?:\d{3})+(?!\d))/g, ',');
                }
            });

            // start all the timers
            $('.timer').each(count);

            function count(options) {
                var $this = $(this);
                options = $.extend({}, options || {}, $this.data('countToOptions') || {});
                $this.countTo(options);
            }
        });

    </script>
</body>
</html>
