<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="Bhusamadhan.UC.Menu" %>

<ul class="nav nav-pills nav-sidebar flex-column" data-widget="treeview" data-accordion="false" role="menu">

    <asp:Literal ID="ltMenu" runat="server"></asp:Literal>

    <li class="nav-item">
        <a href="/Logout.aspx" class="nav-link text-danger">
            <i class="nav-icon fas fa-sign-out-alt"></i>
            <p>Logout</p>
        </a>
    </li>

</ul>