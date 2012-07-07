using System;
using System.Web.UI;
using ArtistWebCatalog.Library;

namespace ArtistWebCatalog
{
    public partial class AboutMe : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetTextOnPage();
        }

        private void SetTextOnPage()
        {
            lblBreadCrumbs.Text = Common.GetText("AboutMeBreadCrumb");
            lblInfo.Text = Common.GetText("AboutMeInfo");
        }
    }
}