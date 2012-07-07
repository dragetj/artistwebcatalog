<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ArtistWebCatalog.Default" MasterPageFile="~/MasterPage.Master" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server" >
</asp:content>
<asp:content id="Content2" contentplaceholderid="mainContent" runat="server">
    <table class="pageDefault">
        <tr>
            <td class="pageDefaultText">
                <asp:label id="lblInfoText" runat="server" /></td></tr>
        <% if (bool.Parse(ConfigurationManager.AppSettings["UseAds"])) { %>
        <tr>
            <td class="pageDefaultAd">
                <asp:adrotator id="arFrontImage" runat="server" width="700px" Target="_self" AdvertisementFile="ad.xml"/></td></tr><% } %>
    </table>
</asp:content>