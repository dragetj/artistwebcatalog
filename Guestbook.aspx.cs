using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.UI.HtmlControls;
using ArtistWebCatalog.Library;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtistWebCatalog
{
    public partial class Guestbook : Page
    {
        private Library.Guestbook g;
        private int i;

        private void ValidateGuestbook()
        {
            // Validate application object
            if (Application["GuestBook"] == null)
                Application["GuestBook"] = g = Library.Guestbook.DeserializeObject(ConfigurationManager.AppSettings["GuestbookFilePath"]);
            else
            {
                g = (Library.Guestbook)Application["GuestBook"];
                if (g.Guests == null || g.Guests.Count == 0)
                    Application["GuestBook"] = g = Library.Guestbook.DeserializeObject(ConfigurationManager.AppSettings["GuestbookFilePath"]);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ValidateGuestbook();
            g = (Library.Guestbook) Application["GuestBook"];

            SetTextOnPage();

            #region Smileys
            if (bool.Parse(ConfigurationManager.AppSettings["GuestbookSmileys"]))
            {
                Session["smileys"] = Common.GetSmileys("*.gif");
                foreach (string myFile in (ArrayList)Session["smileys"])
                    litSmileys.Text += "<img src='" + ConfigurationManager.AppSettings["ImageSmileysPath"] + myFile + "' onclick=\"InsertSmiley('[" + myFile + "]')\" />\n";              
            }
            #endregion

            lvGuests.DataSource = g.Guests;
            lvGuests.DataBind();
        }

        private void SetTextOnPage()
        {
            lblBreadCrumbs.Text = Common.GetText("GuestbookBreadCrumb");
            lblEntryTitle.Text = Common.GetText("GuestbookEntryTitle");
            lblName.Text = Common.GetText("GuestbookName");
            lblEmail.Text = Common.GetText("GuestbookEmail");
            lblText.Text = Common.GetText("GuestbookText");
            lblSmileys.Text = Common.GetText("GuestbookSmileys");
            btnSave.Text = Common.GetText("GuestbookSave");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            GuestbookEntry ge = new GuestbookEntry
                                    {
                                        Id = new Guid(),
                                        Name = txtName.Text,
                                        Text = txtText.Text,
                                        Email = txtEmail.Text,
                                        Date = DateTime.Now.ToString()
                                    };

            g.InsertEntry(ge);
            Response.Redirect("Guestbook.aspx");
        }

        public void lvGuests_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                try
                {
                    ListViewDataItem currentItem = (ListViewDataItem) e.Item;

                    Label lblName = (Label) currentItem.FindControl("lblName");
                    Label lblText = (Label) currentItem.FindControl("lblText");
                    Label lblEmail = (Label) currentItem.FindControl("lblEmail");
                    HtmlAnchor ancEmail = (HtmlAnchor) currentItem.FindControl("ancEmail");
                    Label lblDate = (Label) currentItem.FindControl("lblDate");
                    lblName.Text = g.Guests[i].Name;
                    lblText.Text = g.Guests[i].Text;
                    lblEmail.Text = g.Guests[i].Email;
                    ancEmail.HRef = "mailto:" + g.Guests[i].Email;
                    lblDate.Text = g.Guests[i].Date;

                    #region Smileys
                    if (Session["smileys"] != null)
                    {
                        foreach (string myFile in (ArrayList)Session["smileys"])
                        {
                            if (lblText.Text.Contains("[" + myFile + "]"))
                                lblText.Text = lblText.Text.Replace("[" + myFile + "]", "<img src='" + ConfigurationManager.AppSettings["ImageSmileysPath"] + myFile + "' />");
                        }
                    }
                    #endregion

                    i++;
                }
                catch (Exception ex)
                {
                    Common.WriteToLog("Error on Guestbook row created! Message: " + ex.Message + ", Stack: " + ex.StackTrace);
                }
            }
        }
    }
}