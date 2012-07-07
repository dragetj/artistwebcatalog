using System;
using System.Web.UI;
using ArtistWebCatalog.Library;

namespace ArtistWebCatalog
{
    public partial class ColorMap : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetTextOnPage();
        }

        private void SetTextOnPage()
        {
            lblBreadCrumbs.Text = Common.GetText("ColorMapBreadCrumb");
            lblColorMap1.Text = Common.GetText("ColorMap1");
            lblColorMap2.Text = Common.GetText("ColorMap2");
            lblColorMap3.Text = Common.GetText("ColorMap3");
            lblColorMap4.Text = Common.GetText("ColorMap4");
            lblColorMap5.Text = Common.GetText("ColorMap5");
            lblColorMap6.Text = Common.GetText("ColorMap6");
        }
    }
}