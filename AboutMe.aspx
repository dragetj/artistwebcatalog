<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AboutMe.aspx.cs" Inherits="ArtistWebCatalog.AboutMe" MasterPageFile="~/MasterPage.Master" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server" >
</asp:content>
<asp:content id="Content2" contentplaceholderid="mainContent" runat="server">
    <table class="main">
        <tr>
            <td class="breadcrumb" colspan="2">
                <asp:label id="lblBreadCrumbs" runat="server" /></td>
        </tr>
        <tr>
            <td class="pageAboutMeText">
                <asp:label id="lblInfo" runat="server" /></td>
            <td class="pageAboutMeImage">
                <img src="<%= ConfigurationManager.AppSettings["imagePath"] %>roselind.jpg" height="300px" /></td>
        </tr>
    </table>
</asp:content>