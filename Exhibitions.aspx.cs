using System;
using System.Web.UI;
using ArtistWebCatalog.Library;

namespace ArtistWebCatalog
{
    public partial class Exhibitions : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetTextOnPage();
        }

        private void SetTextOnPage()
        {
            lblBreadCrumbs.Text = Common.GetText("ExhibitionsBreadCrumb");
            lblFurtherPlans.Text = Common.GetText("ExhibitionsFurtherPlans");
            lblExhibitions1.Text = Common.GetText("Exhibitions1");
            lblExhibitions2.Text = Common.GetText("Exhibitions2");
            lblExhibitions3.Text = Common.GetText("Exhibitions3");
            lblExhibitions4.Text = Common.GetText("Exhibitions4");
            lblExhibitions5.Text = Common.GetText("Exhibitions5");
            lblExhibitions6.Text = Common.GetText("Exhibitions6");
            lblExhibitions7.Text = Common.GetText("Exhibitions7");
            lblExhibitions8.Text = Common.GetText("Exhibitions8");
            lblExhibitions9.Text = Common.GetText("Exhibitions9");
        }
    }
}