<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="ArtistWebCatalog.ProductList" MasterPageFile="~/MasterPage.Master" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server" >
</asp:content>
<asp:content id="Content2" contentplaceholderid="mainContent" runat="server">
    <table class="main">
        <tr>
            <td class="breadcrumb" colspan="2">
                <div class="breadcrumb">
                    <asp:label id="lblBreadCrumbs" runat="server" /></div>
                <div class="numberOfProducts">
                    <asp:label id="lblNumberOfProducts" runat="server"/></div></td></tr>
        <% if (bool.Parse(ConfigurationManager.AppSettings["Slideshow"])) { %>
        <tr>
            <td class="slideshow">
                <div class="slideshow">
                    <asp:label id="lblSlideshow" runat="server" /></div></td>
            <td>                
                <a id="ancSlideshow" runat="server" ><img class="graphics" src="<%= ConfigurationManager.AppSettings["imageSystemPath"] %>playbutton.jpg" /></a></td></tr><% } %>
        <tr>
            <td class="pageProductList" colspan="2">
                <asp:panel id="pnlProducts" runat="server" scrollbars="Vertical" width="100%" height="490px">
                    <asp:listview id="lvProducts" runat="server" onitemdatabound="lvProducts_ItemDataBound" >
                        <LayoutTemplate>    
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                        </LayoutTemplate>

                        <ItemTemplate>
                            <div class="pageProductList">
                                <table>
                                    <tr>
                                        <td class="pageProductListImage">
                                            <a id="ancPicture" runat="server">
                                                <asp:image cssclass="productListImage" id="imgProduct" runat="server" width="150px" /></a>
                                            <a id="ancPictureLightbox" runat="server">
                                                <asp:image id="imgProductLightbox" runat="server" visible="false" /></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="pageProductListText">
                                            <asp:label id="lblTitle" runat="server" /><br />
                                            <asp:label id="lblShortText" runat="server" /><br />
                                            <asp:label id="lblPrice" runat="server" /><br />
                                            <asp:label id="lblStock" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ItemTemplate>
                    </asp:listview>
                </asp:panel>
            </td>
        </tr>
    </table>
</asp:content>