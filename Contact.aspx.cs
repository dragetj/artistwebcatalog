using System;
using System.Web.UI;
using ArtistWebCatalog.Library;

namespace ArtistWebCatalog
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetTextOnPage();
        }

        private void SetTextOnPage()
        {
            lblBreadCrumbs.Text = Common.GetText("ContactBreadCrumb");
            lblInfo.Text = Common.GetText("ContactInfo");
            lblContact.Text = Common.GetText("ContactName");
            lblAddress.Text = Common.GetText("ContactAddress");
            lblPhone.Text = Common.GetText("ContactPhone");
        }
    }
}