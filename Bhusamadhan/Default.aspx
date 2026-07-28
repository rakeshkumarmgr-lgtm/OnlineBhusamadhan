<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Bhusamadhan.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <style>
      .dashboard-section {
          background: #6C9BCF;
          color: #fff;
          text-align: center;
          padding: 8px;
          margin: 25px 0 15px;
          border-radius: 4px;
          box-shadow: 0 1px 3px rgba(0,0,0,.2);
      }

      .dashboard-card {
          box-shadow: 0 3px 10px rgba(0,0,0,.18);
          transition: .3s;
          border: 0;
      }

          .dashboard-card:hover {
              transform: translateY(-3px);
          }

      .card-title {
          text-align: center;
          font-weight: 600;
          font-size: 15px;
          color: #000;
          margin: 12px 0;
      }

      .card-value {
          font-size: 35px;
          font-weight: 700;
          color: darkblue;
          text-decoration: none;
      }

      .card-body {
          text-align: center;
      }
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
 <%--   <div class="container text-center mt-1 pt-5">

        <a href="DefaultHome.aspx" class="text-decoration-none fw-bold" style="font-size: 32px; color: #0d6efd;">
            <i class="fa fa-chart-line"></i>
            Click Here to View Summary Report

        </a>

    </div>--%>

     <div class="container my-4">

     <h3 class="text-center mb-4">Dashboard</h3>

     <!-- आवेदन -->
     <h4 class="dashboard-section">आवेदन</h4>

     <div class="row">

         <div class="col-md-4 mb-3">
             <div class="card dashboard-card">
                 <div class="card-title">कुल आवेदन</div>

                 <div class="card-body">

                     <a href="~/LandDispute/Reports/Consolidate/SearchApplicationwise.aspx" id="TotalApplication1" runat="server">
                         <asp:Label ID="lbltotalapplication1" runat="server" CssClass="card-value"></asp:Label>
                     </a>

                     <br />

                     <a href="LandDispute/Reports/Consolidate/ApplicationDistConsolidateDashboard.aspx" id="TotalApplication2" runat="server">
                         <asp:Label ID="lbltotalapplication2" runat="server" CssClass="card-value"></asp:Label>
                     </a>

                 </div>
             </div>
         </div>

         <div class="col-md-4 mb-3">
             <div class="card dashboard-card">

                 <div class="card-title">पूर्ण प्रविष्टि</div>

                 <div class="card-body">

                     <a href="~/LandDispute/Entry/Finalize.aspx" id="Finalize1" runat="server">
                         <asp:Label ID="lblFinalize1" runat="server" CssClass="card-value"></asp:Label>
                     </a>

                     <br />

                     <a href="LandDispute/Reports/Consolidate/ApplicationDistConsolidateDashboard.aspx" id="Finalize2" runat="server">
                         <asp:Label ID="lblFinalize2" runat="server" CssClass="card-value"></asp:Label>
                     </a>

                 </div>
             </div>
         </div>

         <div class="col-md-4 mb-3">
             <div class="card dashboard-card">

                 <div class="card-title">आंशिक प्रविष्टि</div>

                 <div class="card-body">

                     <a href="~/LandDispute/Entry/Unfinalize.aspx" id="UnFinalize1" runat="server">
                         <asp:Label ID="lblUnFinalize1" runat="server" CssClass="card-value"></asp:Label>
                     </a>

                     <br />

                     <a href="LandDispute/Reports/Consolidate/ApplicationDistConsolidateDashboard.aspx" id="UnFinalize2" runat="server">
                         <asp:Label ID="lblUnFinalize2" runat="server" CssClass="card-value"></asp:Label>
                     </a>

                 </div>

             </div>
         </div>

     </div>

     <!-- संवेदनशीलता -->
     <h4 class="dashboard-section">संवेदनशीलता</h4>

     <div class="row">

         <div class="col-md-4 mb-3">
             <div class="card dashboard-card">
                 <div class="card-title">अति संवेदनशील</div>
                 <div class="card-body">
                     <a href="LandDispute/Reports/Consolidate/DistrictSensitivityType.aspx">
                         <asp:Label ID="lblatiSavedansheel" runat="server" CssClass="card-value"></asp:Label>
                     </a>
                 </div>
             </div>
         </div>


         <div class="col-md-4 mb-3">
             <div class="card dashboard-card">
                 <div class="card-title">संवेदनशील</div>
                 <div class="card-body">
                     <a href="LandDispute/Reports/Consolidate/DistrictSensitivityType.aspx">
                         <asp:Label ID="lblsavedansheel" runat="server" CssClass="card-value"></asp:Label>

                     </a>
                 </div>
             </div>
         </div>

         <div class="col-md-4 mb-3">
             <div class="card dashboard-card">
                 <div class="card-title">सामान्य</div>
                 <div class="card-body">
                     <a href="LandDispute/Reports/Consolidate/DistrictSensitivityType.aspx">
                         <asp:Label ID="lblsamanya" runat="server" CssClass="card-value"></asp:Label></a>
                 </div>
             </div>
         </div>

         <!-- Repeat same structure for remaining two cards -->

     </div>

     <!-- स्थिति -->
     <h4 class="dashboard-section">स्थिति</h4>

     <div class="row">

         <div class="col-lg-2 col-md-4 col-sm-6 mb-3">
             <div class="card dashboard-card">

                 <div class="card-title">प्रारंभिक निष्पादन</div>

                 <div class="card-body">
                     <a href="LandDispute/Reports/Consolidate/ApplicationDistConsolidateRpt.aspx">
                         <asp:Label ID="lblnispadan" runat="server" CssClass="card-value"></asp:Label>
                     </a>
                 </div>

             </div>
         </div>

         <!-- Repeat same card for remaining five status cards -->

         <div class="col-lg-2 col-md-4 col-sm-6 mb-3">
             <div class="card dashboard-card">

                 <div class="card-title">अंतिम निष्पादन</div>

                 <div class="card-body">
                     <a href="LandDispute/Reports/Consolidate/ApplicationDistConsolidateRpt.aspx">
                         <asp:Label ID="lblFinaldisposal" runat="server" CssClass="card-value"></asp:Label></a>
                 </div>

             </div>
         </div>

         <div class="col-lg-2 col-md-4 col-sm-6 mb-3">
             <div class="card dashboard-card">

                 <div class="card-title">प्रक्रियाधीन</div>

                 <div class="card-body">
                     <a href="LandDispute/Reports/Consolidate/ApplicationDistConsolidateRpt.aspx">
                         <asp:Label ID="lblprakreeyadheen" runat="server" CssClass="card-value"></asp:Label></a>
                 </div>

             </div>
         </div>

         <div class="col-lg-2 col-md-4 col-sm-6 mb-3">
             <div class="card dashboard-card">

                 <div class="card-title">मापी के लिए निर्धारित</div>

                 <div class="card-body">
                     <a href="LandDispute/Reports/Consolidate/ApplicationDistConsolidateRpt.aspx">
                         <asp:Label ID="lblmapikenirdharit" runat="server" CssClass="card-value"></asp:Label></a>
                 </div>

             </div>
         </div>

         <div class="col-lg-2 col-md-4 col-sm-6 mb-3">
             <div class="card dashboard-card">

                 <div class="card-title">अस्वीकृत</div>

                 <div class="card-body">
                     <a href="LandDispute/Reports/Consolidate/ApplicationDistConsolidateRpt.aspx">
                         <asp:Label ID="lblashvikrit" runat="server" CssClass="card-value"></asp:Label></a>
                 </div>

             </div>
         </div>

         <div class="col-lg-2 col-md-4 col-sm-6 mb-3">
             <div class="card dashboard-card">

                 <div class="card-title">न्यायालय में लंबित</div>

                 <div class="card-body">
                     <a href="LandDispute/Reports/Consolidate/ApplicationDistConsolidateRpt.aspx">
                         <asp:Label ID="lblNaylayNilambit" runat="server" CssClass="card-value"></asp:Label></a>
                 </div>

             </div>
         </div>

     </div>

 </div>
</asp:Content>
