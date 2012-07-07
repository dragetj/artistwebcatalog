<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Admin.aspx.cs" Inherits="ArtistWebCatalog.Admin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Admin</title>
    <link href="App_Themes\css\admin.css" rel="stylesheet" type="text/css" />
    
    <script src="App_Themes/js/prototype.js" type="text/javascript"></script>
	<script src="App_Themes/js/scriptaculous.js?load=effects,builder" type="text/javascript"></script>
	<script src="App_Themes/js/lightbox.js" type="text/javascript"></script>
    <script type="text/javascript">
        function blank(a) { if(a.value == a.defaultValue) a.value = ""; }
        function unblank(a) { if(a.value == "") a.value = a.defaultValue; }
    </script> 
</head>
<body>
    <form id="form1" runat="server">
        <asp:button id="btnViewProducts" runat="server" text="Vis produkter" onclick="bntViewProducts_Click" enabled="false" />
        <asp:button id="btnViewGuestbook" runat="server" text="Vis gjestebok" onclick="btnViewGuestbook_Click" />
        <asp:button id="btnViewAdmin" runat="server" text="Administrator" forecolor="Red" onclick="btnViewAdmin_Click" />
        <asp:multiview id="mvAdmin" runat="server" ActiveViewIndex="0">
            <asp:View id="viewProducts" runat="server">
                <table class="main">
                    <tr>
                        <td class="input">
                            <fieldset>
                                <legend><asp:label id="lblProductInput" runat="server" /></legend>
                                <table >
                                    <tr>
                                        <td class="cola"><asp:label id="lblProductInputTitle" runat="server" /></td>
                                        <td class="colb"><asp:textbox width="97%" id="txtProductInputTitle" runat="server" onfocus="blank(this)" onblur="unblank(this)"/></td></tr>
                                    <tr>
                                        <td class="cola"><asp:label id="lblProductInputShortText" runat="server" /></td>
                                        <td class="colb"><asp:textbox width="97%" id="txtProductInputShortText" runat="server" onfocus="blank(this)" onblur="unblank(this)"/></td></tr>
                                    <% if (ConfigurationManager.AppSettings["Artist"] == "tskdesign") { %>
                                    <tr>
                                        <td class="cola"><asp:label id="lblProductInputLongText" runat="server" /></td>
                                        <td class="colb"><asp:textbox width="97%" id="txtProductInputLongText" runat="server" textmode="MultiLine" height="200px" onfocus="blank(this)" onblur="unblank(this)"/></td></tr><% } %>
                                    <tr>
                                        <td class="cola"><asp:label id="lblProductInputImage" runat="server" /></td>
                                        <td class="colb"><asp:fileupload id="fuProductInputImage" runat="server" /><br />
                                        <em><asp:label id="lblProductInputImageInfo" runat="server" Visible="false" /></em></td></tr>
                                    <tr>
                                        <td class="cola"><asp:label id="lblProductInputPrice" runat="server" /></td>
                                        <td class="colb"><asp:textbox width="30%" id="txtProductInputPrice" runat="server" onfocus="blank(this)" onblur="unblank(this)"/></td></tr>
                                    <tr>
                                        <td class="cola"><asp:label id="lblProductInputStock" runat="server" /></td>
                                        <td class="colb"><asp:checkbox id="cbProductInputStock" runat="server" /></td></tr>
                                    <tr>
                                        <td class="cola"><asp:label id="lblProductInputCategory" runat="server" /></td>
                                        <td class="colb"><asp:dropdownlist width="50%" id="ddlProductInputCategory" runat="server" />&nbsp;<asp:textbox width="45%" id="txtProductInputCategory" runat="server" onfocus="blank(this)" onblur="unblank(this)"/></td></tr>
                                    <% if (ConfigurationManager.AppSettings["Artist"] == "tskdesign") { %>
                                    <tr>
                                        <td class="cola"><asp:label id="lblProductInputSize" runat="server" /></td>
                                        <td class="colb">
                                            <table>
                                                <tr>
                                                    <td><asp:label id="lblProductInputCategoryChild" runat="server" /></td>
                                                    <td><asp:checkbox id="cbProductInputCategoryChild" runat="server" /></td></tr>
                                                <tr>
                                                    <td><asp:label id="lblProductInputCategoryAdult" runat="server" /></td>
                                                    <td><asp:checkbox id="cbProductInputCategoryAdult" runat="server" /></td></tr></table></td></tr><% } %>
                                    <% if (ConfigurationManager.AppSettings["Artist"] == "tskdesign") { %>
                                    <tr>
                                        <td class="cola"><asp:label id="lblProductInputFabric" runat="server" /></td>
                                        <td class="colb"><asp:dropdownlist width="50%" id="ddlProductInputFabric" runat="server" />&nbsp;<asp:textbox width="45%" id="txtProductInputFabric" runat="server" onfocus="blank(this)" onblur="unblank(this)"/></td></tr><% } %>
                                    <tr>
                                        <td class="cola">&nbsp;</td>
                                        <td class="colbbutton"><asp:button id="btnProductInputSave" runat="server" onclick="btnProductInputSave_Click" />&nbsp;
                                        <asp:button id="btnProductInputCancel" runat="server" onclick="btnProductInputCancel_Click" /></td></tr>
                                    <tr>
                                        <td class="cola" colspan="2"><asp:label id="lblProductInputResult" runat="server" cssclass="success" /></td></tr>
                                </table></fieldset>
                            <asp:literal id="litEdit" runat="server" visible="false" />
                            <asp:literal id="litId" runat="server" visible="false" />
                            <asp:literal id="litImage" runat="server" visible="false" />
                        </td>
                        <td class="output">
                            <asp:GridView ID="gvProducts" runat="server" AutoGenerateDeleteButton="false" AutoGenerateEditButton="false" autogeneratecolumns="false"
                            OnSelectedIndexChanging="gvProducts_RowSelecting" onrowdeleting="gvProducts_RowDeleting" OnRowCreated="gvProducts_RowCreated">
                                <headerstyle cssclass="gridHeader" />
                                <rowstyle cssclass="gridRow"  />
                                <AlternatingRowStyle CssClass="gridAltRow" />   
                                <SelectedRowStyle CssClass="gridSelRow" />  
                                <columns>
                                    <asp:templatefield itemstyle-width="5%">
                                        <itemtemplate>
                                            <asp:linkbutton id="lbtnEdit" runat="server" commandname="select" text="<%$ Resources:admin, AdminProductOutputEdit %>" />
                                        </itemtemplate>
                                    </asp:templatefield>
                                    <asp:templatefield itemstyle-width="75px" HeaderText="<%$ Resources:admin, AdminProductInputImage %>">
                                        <itemtemplate>
                                            <a id="ancImage" runat="server" title='<%#DataBinder.Eval(Container.DataItem,"Image")%>'>
                                                <img src='<%= ConfigurationManager.AppSettings["imagePath"] %><%#DataBinder.Eval(Container.DataItem,"Image")%>' Width="75px" height="100px" /></a>
                                        </itemtemplate>
                                    </asp:templatefield>
                                    <asp:templatefield HeaderText="<%$ Resources:admin, AdminProductInputTitle %>">
                                        <itemtemplate>
                                            <a id="ancTitle" runat="server" title='<%#DataBinder.Eval(Container.DataItem,"Title")%>'><%#DataBinder.Eval(Container.DataItem,"Title")%></a>
                                        </itemtemplate>
                                    </asp:templatefield>
                                    <asp:templatefield HeaderText="<%$ Resources:admin, AdminProductInputShortText %>">
                                        <itemtemplate>
                                            <asp:label id="lblShortText" runat="server" title='<%#DataBinder.Eval(Container.DataItem,"ShortText")%>' />
                                        </itemtemplate>
                                    </asp:templatefield>
                                    <asp:templatefield headertext="<%$ Resources:admin, AdminProductInputLongText %>">
                                        <itemtemplate>
                                            <asp:label id="lblLongText" runat="server" title='<%#DataBinder.Eval(Container.DataItem,"LongText")%>' />
                                        </itemtemplate>
                                    </asp:templatefield>
                                    <asp:boundfield itemstyle-width="5%" DataField="Price" headertext="<%$ Resources:admin, AdminProductInputPrice %>" />
                                    <asp:templatefield itemstyle-width="5%" headertext="<%$ Resources:admin, AdminProductOutputStock %>">
                                        <itemtemplate>
                                            <asp:checkbox id="cbStock" runat="server" enabled="false" />
                                        </itemtemplate>
                                    </asp:templatefield>
                                    <asp:boundfield DataField="Category" headertext="<%$ Resources:admin, AdminProductInputCategory %>" />
                                    <asp:templatefield headertext="<%$ Resources:admin, AdminProductInputSize %>">
                                        <itemtemplate>
                                            <asp:label id="lblSize" runat="server" />
                                        </itemtemplate>
                                    </asp:templatefield>
                                    <asp:templatefield headertext="<%$ Resources:admin, AdminProductInputFabric %>">
                                        <itemtemplate>
                                            <asp:label id="lblFabric" runat="server" />
                                        </itemtemplate>
                                    </asp:templatefield>
                                    <asp:templatefield itemstyle-width="5%">
                                        <itemtemplate>
                                            <asp:linkbutton id="lbtnDelete" runat="server" commandname="delete" text="<%$ Resources:admin, AdminProductOutputDelete %>" />
                                        </itemtemplate>
                                    </asp:templatefield>
                                </columns>              
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View id="viewGuestbook" runat="server">
                <table class="main">
                    <strong><asp:label id="lblGuestbookInputResult" runat="server" /></strong><br />
                    <asp:listview id="lvGuests" runat="server" onitemdatabound="lvGuests_ItemDataBound" OnItemDeleting="lvGuests_ItemDeleting">
                        <LayoutTemplate>    
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                        </LayoutTemplate>

                        <ItemTemplate>
                        <tr>
                            <td class="guestbook">
                                <asp:label id="lblDate" runat="server" /><br />
                                <asp:linkbutton id="lbtnDelete" runat="server" commandname="delete" text="<%$ Resources:admin, AdminGuestbookOutputDelete %>" /></td></tr>
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
            </asp:View>
            <asp:View id="viewForceObjectReload" runat="server">
                <table class="main">
                    <tr>
                        <td>
                            <asp:button id="btnForceObjectReload" runat="server" text="-- Reload application objects --" forecolor="Red" onclick="btnForceObjectReload_Click" />
                            <asp:label id="lblForceObjectReload" runat="server" /></td></tr>
                
            </asp:View>
        </asp:multiview>
    </form>
</body>
</html>
