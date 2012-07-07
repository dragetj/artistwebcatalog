<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductDetail.aspx.cs" Inherits="ArtistWebCatalog.ProductDetail" MasterPageFile="~/MasterPage.Master" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server" >
</asp:content>
<asp:content id="Content2" contentplaceholderid="mainContent" runat="server">
    <table class="main">
        <tr>
            <td class="breadcrumb" colspan="2">
                <asp:label id="lblBreadCrumbs" runat="server" /></td>
        </tr>
        <tr>
            <td class="pageProductDetail">
                <table class="pageProductDetailText">
                    <tr>
                        <td class="pageProductDetailTextHeader" colspan="2">
                            <asp:label id="lblTitle" runat="server" /></td></tr>
                    <tr>
                        <td class="pageProductDetailTextInfo" colspan="2">
                            <asp:label id="lblLongText" runat="server" /></td></tr>
                    <tr>
                        <td class="pageProductDetailTextInfo" colspan="2">
                            <asp:label id="lblSize" runat="server" /></td></tr>
                    <tr>
                        <td colspan="2">
                            <asp:label id="lblFabric" runat="server" /></td></tr>
                    <tr>
                        <td class="pageProductDetailTextPrice">&nbsp;</td>
                        <td>
                            <asp:label id="lblPrice" runat="server" /><br />
                            <em><asp:label id="lblStock" runat="server" /></em></td></tr>
                    <tr>
                        <td colspan="2">
                            <div id="fb-root"></div>
                            <span id="fb"></span>
                        </td>
                    </tr>
                </table></td>
            <td class="pageProductDetail">
                <a id="ancPicture" runat="server"><asp:image id="imgPicture" runat="server" width="380px"/><br /></a>
                <em><asp:label id="lblPicture" runat="server" /></em></td>
        </tr>
    </table>
</asp:content>