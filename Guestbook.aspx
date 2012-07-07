<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Guestbook.aspx.cs" Inherits="ArtistWebCatalog.Guestbook" MasterPageFile="~/MasterPage.Master" %>
<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>
<asp:content id="Content1" contentplaceholderid="head" runat="server" >
<script language="javascript" type="text/javascript">
function InsertSmiley(text) {
    document.getElementById('<%=txtText.ClientID%>').value += text;
}
</script>
</asp:content>
<asp:content id="Content2" contentplaceholderid="mainContent" runat="server">
    <table class="main">
    <tr>
        <td class="breadcrumb" colspan="3">
            <asp:label id="lblBreadCrumbs" runat="server" /></td>
    </tr>
    <tr>
        <td class="pageGuestbook">
            <fieldset><legend><asp:label id="lblEntryTitle" runat="server" /></legend>
                <table>
                    <tr>
                        <td class="pageGuestbookEntryLeft"><asp:label id="lblName" runat="server" /></td>
                        <td class="pageGuestbookEntryRight"><asp:textbox id="txtName" runat="server" width="100%" /></td></tr>
                    <tr>
                        <td class="pageGuestbookEntry"><asp:label id="lblEmail" runat="server" /></td>
                        <td class="pageGuestbookEntryRight"><asp:textbox id="txtEmail" runat="server" width="100%" /></td></tr>
                    <tr>
                        <td class="pageGuestbookEntry"><asp:label id="lblText" runat="server" /></td>
                        <td class="pageGuestbookEntryRight"><asp:textbox id="txtText" runat="server" textmode="MultiLine" maxlength="1000" width="100%" height="130px" /></td></tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <recaptcha:RecaptchaControl
                            ID="recaptcha"
                            runat="server"
                            PublicKey="6LdiG8ISAAAAABjy_GlUUmQV2aoj8TD_-srO_WmK"
                            PrivateKey="6LdiG8ISAAAAAHN8X5KR3qLkRW14uaNFDTk53yAf"
                            />
                        </td>
                    </tr>
                    <% if (bool.Parse(ConfigurationManager.AppSettings["GuestbookSmileys"])) { %>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <em><asp:label id="lblSmileys" runat="server" /></em><br />
                            <asp:literal id="litSmileys" runat="server" /></td></tr><% } %>
                    <tr>
                        <td>&nbsp;</td>
                        <td class="pageGuestbookEntryRightButton">
                            <asp:button id="btnSave" runat="server" onclick="btnSave_Click" /></td></tr></table></fieldset></td>
        <td class="pageGuestbookSpace">&nbsp;</td>
        <td class="pageGuestbookList">
            <asp:panel id="pnlGuestbook" runat="server" scrollbars="Vertical" width="100%" height="100%">
                <table class="pageGuestbookList">
                    <asp:listview id="lvGuests" runat="server" onitemdatabound="lvGuests_ItemDataBound" >
                        <LayoutTemplate>    
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                        </LayoutTemplate>

                        <ItemTemplate>
                        <tr>
                            <td class="pageGuestbookListEntry">
                                <asp:label id="lblDate" runat="server" /></td></tr>
                        <tr>
                            <td>
                                <asp:label id="lblText" runat="server" /><br />
                                <br />
                                <em><asp:label id="lblName" runat="server" /></em><br />
                                <a id="ancEmail" runat="server">
                                    <asp:label id="lblEmail" runat="server" /></a>
                                <hr /></td></tr>
                        </ItemTemplate>
                    </asp:listview>
                </table>
            </asp:panel></td></tr></table>
</asp:content>