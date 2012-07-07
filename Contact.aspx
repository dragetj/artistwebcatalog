<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="ArtistWebCatalog.Contact" MasterPageFile="~/MasterPage.Master" %>
<%@ Import Namespace="ArtistWebCatalog.Library"%>

<asp:content id="Content1" contentplaceholderid="head" runat="server" >
</asp:content>
<asp:content id="Content2" contentplaceholderid="mainContent" runat="server">
    <table class="main">
    <tr>
        <td class="breadcrumb">
            <asp:label id="lblBreadCrumbs" runat="server" /></td>
    </tr>
    <tr>
        <td class="pageContact">
            <asp:label id="lblInfo" runat="server" /></td>
    </tr>
    <tr>
        <td class="pageContactDetails">
            <p><asp:label id="lblContact" runat="server" /></p>
            <p><asp:label id="lblAddress" runat="server" /></p>
            <p>
                <a class="pageContact" href="<%= Common.GetText("ContactEmail") %>"><%= Common.GetText("ContactEmail") %></a> / 
                <asp:label id="lblPhone" runat="server" /></p></td></tr></table>
</asp:content>