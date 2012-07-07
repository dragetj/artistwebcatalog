using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using ArtistWebCatalog.Library;

namespace ArtistWebCatalog
{
    public partial class ProductDetail : Page
    {
        private ProductCatalog pc;
        private Product p;

        private void ValidateProductCatalog()
        {
            // Validate application object
            if (Application["ProductCatalog"] == null)
                Application["ProductCatalog"] = pc = ProductCatalog.DeserializeObject(ConfigurationManager.AppSettings["ProductFilePath"]);
            else
            {
                pc = (ProductCatalog)Application["ProductCatalog"];
                if (pc.Products == null || pc.Products.Count == 0)
                    Application["ProductCatalog"] = pc = ProductCatalog.DeserializeObject(ConfigurationManager.AppSettings["ProductFilePath"]);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ValidateProductCatalog();
            pc = (ProductCatalog)Application["ProductCatalog"];

            SetTextOnPage();

            string id = Request.QueryString["id"];

            foreach (Product item in pc.Products)
            {
                if (item.Id.ToString().Equals(id))
                {
                    p = item;
                    break;
                }
            }

            string imageUrl = ConfigurationManager.AppSettings["ImagePath"] + p.Image;
            imgPicture.ImageUrl = imageUrl;

            #region Lightbox functionality on/off
            if (bool.Parse(ConfigurationManager.AppSettings["Lightbox"]))
            {
                ancPicture.Attributes.Add("class", "lightview");
                ancPicture.HRef = imageUrl;
                ancPicture.Title = Common.GetTitleTag(p);
            }
            else
                ancPicture.Attributes.Add("onclick", "javascript:window.open('" + imageUrl + "')");
            #endregion

            lblTitle.Text = p.Title;
            lblLongText.Text = p.LongText;
            lblPrice.Text = Common.GetText("CommonPrice") + p.Price + " " + Common.GetText("CommonPriceSuffix");
            lblStock.Text = p.Stock ? Common.GetText("CommonStockYes") : Common.GetText("CommonStockNo");

            lblSize.Text = Common.GetText("ProductDetailSize");
            if (p.CategoryChild && !p.CategoryAdult)
                lblSize.Text += Common.GetText("CommonCategoryChild");
            if (p.CategoryAdult && !p.CategoryChild)
                lblSize.Text += Common.GetText("CommonCategoryAdult");
            if (p.CategoryChild && p.CategoryAdult)
                lblSize.Text += Common.GetText("CommonCategoryChildAdult");

            lblFabric.Text = Common.GetText("ProductDetailWash") + p.Fabric + ". " + Common.GetText("ProductDetailWash_" + p.Fabric);
        }

        private void SetTextOnPage()
        {
            lblBreadCrumbs.Text = Common.GetText("ProductDetailBreadCrumb");
            lblPicture.Text = Common.GetText("ProductDetailPicture");
        }
    }
}