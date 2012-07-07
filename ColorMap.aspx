<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ColorMap.aspx.cs" Inherits="ArtistWebCatalog.ColorMap" MasterPageFile="~/MasterPage.Master" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server" >
</asp:content>
<asp:content id="Content2" contentplaceholderid="mainContent" runat="server">
    <table class="main">
        <tr>
            <td class="breadcrumb" colspan="2">
                <asp:label id="lblBreadCrumbs" runat="server" /></td>
        </tr>
        <tr>
            <td class="colorMapList">
                <a onclick="javascript:window.open('<%= ConfigurationManager.AppSettings["imagePath"] %>Bomull.jpg')">
                    <img src="<%= ConfigurationManager.AppSettings["imagePath"] %>Bomull.jpg" width="100px" height="150px" /></a><br />
                    <em><asp:label id="lblColorMap1" runat="server" /></em></td>
            <td class="colorMapList">
                <a onclick="javascript:window.open('<%= ConfigurationManager.AppSettings["imagePath"] %>Bomull_lin.jpg')">
                    <img src="<%= ConfigurationManager.AppSettings["imagePath"] %>Bomull_lin.jpg" width="100px" height="150px" /></a><br />
                    <em><asp:label id="lblColorMap2" runat="server" /></em></td>
            <td class="colorMapList">
                <a onclick="javascript:window.open('<%= ConfigurationManager.AppSettings["imagePath"] %>Bomull_perletvinn.jpg')">
                    <img src="<%= ConfigurationManager.AppSettings["imagePath"] %>Bomull_perletvinn.jpg" width="100px" height="150px" /></a><br />
                    <em><asp:label id="lblColorMap3" runat="server" /></em></td>
        </tr>
        <tr>
            <td class="colorMapList">
                <a onclick="javascript:window.open('<%= ConfigurationManager.AppSettings["imagePath"] %>Ull.jpg')">
                    <img src="<%= ConfigurationManager.AppSettings["imagePath"] %>Ull.jpg" width="100px" height="150px" /></a><br />
                    <em><asp:label id="lblColorMap4" runat="server" /></em></td>
            <td class="colorMapList">
                <a onclick="javascript:window.open('<%= ConfigurationManager.AppSettings["imagePath"] %>Ull_superwash.jpg')">
                    <img src="<%= ConfigurationManager.AppSettings["imagePath"] %>Ull_superwash.jpg" width="100px" height="150px" /></a><br />
                    <em><asp:label id="lblColorMap5" runat="server" /></em></td>
            <td class="colorMapList">
                <a onclick="javascript:window.open('<%= ConfigurationManager.AppSettings["imagePath"] %>Kamgarn.jpg')">
                    <img src="<%= ConfigurationManager.AppSettings["imagePath"] %>Kamgarn.jpg" width="100px" height="150px" /></a><br />
                    <em><asp:label id="lblColorMap6" runat="server" /></em></td>
        </tr>
    </table>
</asp:content>