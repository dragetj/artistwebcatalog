<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Exhibitions.aspx.cs" Inherits="ArtistWebCatalog.Exhibitions" MasterPageFile="~/MasterPage.Master" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server" >
</asp:content>
<asp:content id="Content2" contentplaceholderid="mainContent" runat="server">
    <table class="main">
        <tr>
            <td class="breadcrumb">
                <asp:label id="lblBreadCrumbs" runat="server" /></td>
        </tr>
        <tr>
            <td class="pageExhibitions" colspan="2">
                <asp:label id="lblFurtherPlans" runat="server" /></td>
        </tr>
        <tr>
            <td class="pageExhibitionsList">
                <a onclick="javascript:window.open('<%= ConfigurationManager.AppSettings["imagePath"] %>Utstilling_GlesværCafeSotra_11.jpg')">
                    <img src="<%= ConfigurationManager.AppSettings["imagePath"] %>Utstilling_GlesværCafeSotra_11.jpg" width="200px" /></a><br />
                    <em><asp:label id="lblExhibitions8" runat="server" /></em></td>
            <td class="pageExhibitionsList">
                <a onclick="javascript:window.open('<%= ConfigurationManager.AppSettings["imagePath"] %>Utstilling_GalleriGol_11.jpg')">
                    <img src="<%= ConfigurationManager.AppSettings["imagePath"] %>Utstilling_GalleriGol_11.jpg" width="200px" /></a><br />
                    <em><asp:label id="lblExhibitions9" runat="server" /></em></td>
            <td class="pageExhibitionsList">
                <a onclick="javascript:window.open('<%= ConfigurationManager.AppSettings["imagePath"] %>Utstilling_GalleriS9_11.jpg')">
                    <img src="<%= ConfigurationManager.AppSettings["imagePath"] %>Utstilling_GalleriS9_11.jpg" width="200px" /></a><br />
                    <em><asp:label id="lblExhibitions1" runat="server" /></em></td>
        </tr>
        <tr>
            <td class="pageExhibitionsList">
                <a onclick="javascript:window.open('<%= ConfigurationManager.AppSettings["imagePath"] %>Utstilling_Nystugu_10.jpg')">
                    <img src="<%= ConfigurationManager.AppSettings["imagePath"] %>Utstilling_Nystugu_10.jpg" width="200px" /></a><br />
                    <em><asp:label id="lblExhibitions2" runat="server" /></em></td>
            <td class="pageExhibitionsList">
                <a onclick="javascript:window.open('<%= ConfigurationManager.AppSettings["imagePath"] %>Utstilling_Ringerike_10.jpg')">
                    <img src="<%= ConfigurationManager.AppSettings["imagePath"] %>Utstilling_Ringerike_10.jpg" width="200px" /></a><br />
                    <em><asp:label id="lblExhibitions3" runat="server" /></em></td>
            <td class="pageExhibitionsList">
                <a onclick="javascript:window.open('<%= ConfigurationManager.AppSettings["imagePath"] %>Utstilling_GalleriSyningen_10.jpg')">
                    <img src="<%= ConfigurationManager.AppSettings["imagePath"] %>Utstilling_GalleriSyningen_10.jpg" width="200px" /></a><br />
                    <em><asp:label id="lblExhibitions4" runat="server" /></em></td>
        </tr>
        <tr>
            <td class="pageExhibitionsList">
                <a onclick="javascript:window.open('<%= ConfigurationManager.AppSettings["imagePath"] %>Utstilling_Vestfoldmessen_09.jpg')">
                    <img src="<%= ConfigurationManager.AppSettings["imagePath"] %>Utstilling_Vestfoldmessen_09.jpg" width="200px" /></a><br />
                    <em><asp:label id="lblExhibitions5" runat="server" /></em></td>
            <td class="pageExhibitionsList">
                <a onclick="javascript:window.open('<%= ConfigurationManager.AppSettings["imagePath"] %>Utstilling_Notteroy_09.jpg')">
                    <img src="<%= ConfigurationManager.AppSettings["imagePath"] %>Utstilling_Notteroy_09.jpg" width="200px" /></a><br />
                    <em><asp:label id="lblExhibitions6" runat="server" /></em></td>
            <td class="pageExhibitionsList">
                <a onclick="javascript:window.open('<%= ConfigurationManager.AppSettings["imagePath"] %>Utstilling_Kjerringtorget_08.jpg')">
                    <img src="<%= ConfigurationManager.AppSettings["imagePath"] %>Utstilling_Kjerringtorget_08.jpg" width="200px" /></a><br />
                    <em><asp:label id="lblExhibitions7" runat="server" /></em></td>
        </tr>
    </table>
</asp:content>