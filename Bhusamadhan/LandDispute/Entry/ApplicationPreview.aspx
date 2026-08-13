<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApplicationPreview.aspx.cs" Inherits="Bhusamadhan.LandDispute.Entry.ApplicationPreview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

    <div class="container-fluid">

    <div class="card">

        <div class="card-header bg-primary text-white">

            <h5>Application Preview</h5>

        </div>

        <div class="card-body">

            <asp:Label ID="lblApplicationNo" runat="server" Font-Bold="true"  Font-Size="Large" ForeColor="Green"> </asp:Label>

            <hr />

        </div>

        <div class="card-footer text-center">

            <asp:Button ID="btnEdit"  runat="server" Text="Edit Application"  CssClass="btn btn-warning" OnClick="btnEdit_Click" />

            &nbsp;

            <asp:Button ID="btnFinalSubmit" runat="server" Text="Final Submit" CssClass="btn btn-success"   OnClientClick="return confirm('After Final Submit, application cannot be edited.\nDo you want to continue?');" OnClick="btnFinalSubmit_Click"/>

        </div>

    </div>

</div>
</asp:Content>
