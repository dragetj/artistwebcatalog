using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ArtistWebCatalog.Library;

namespace ArtistWebCatalog
{
    public partial class ProductList : Page
    {
        private ProductCatalog pc;
        private List<Product> itemsToDisplay;
        private int i;

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

            itemsToDisplay = new List<Product>();
            string category = Request.QueryString["category"];
            string size = Request.QueryString["size"];

            if (string.IsNullOrEmpty(category))
                itemsToDisplay = pc.Products;
            else
            {
                foreach (Product item in pc.Products)
                {
                    if (item.Category.Equals(category))
                    {
                        if (!string.IsNullOrEmpty(size))
                        {
                            if (size.Equals("child") && item.CategoryChild)
                                itemsToDisplay.Add(item);

                            if (size.Equals("adult") && item.CategoryAdult)
                                itemsToDisplay.Add(item);
                        }
                        else
                            itemsToDisplay.Add(item);
                    }
                }
            }

            lblNumberOfProducts.Text = Common.GetText("ProductListNumberOfProducts") + itemsToDisplay.Count;

            lvProducts.DataSource = itemsToDisplay;
            lvProducts.DataBind();
        }

        private void SetTextOnPage()
        {
            lblBreadCrumbs.Text = Common.GetText("ProductListBreadCrumb");
            lblSlideshow.Text = Common.GetText("ProductListSlideshow");
        }

        public void lvProducts_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                try
                {
                    ListViewDataItem currentItem = (ListViewDataItem) e.Item;

                    Image imgProduct = (Image) currentItem.FindControl("imgProduct");
                    imgProduct.ImageUrl = ConfigurationManager.AppSettings["ImagePath"] + itemsToDisplay[i].Image;
                    imgProduct.AlternateText = itemsToDisplay[i].Image;

                    HtmlAnchor ancPicture = (HtmlAnchor) currentItem.FindControl("ancPicture");

                    #region ProductDetail page on/off

                    if (bool.Parse(ConfigurationManager.AppSettings["ProductDetailPage"]))
                        ancPicture.HRef = "ProductDetail.aspx?id=" + itemsToDisplay[i].Id;
                    else
                    {
                        #region Lightbox functionality on/off

                        if (bool.Parse(ConfigurationManager.AppSettings["Lightbox"]))
                        {
                            ancPicture.Attributes.Add("class", "lightview");
                            ancPicture.HRef = imgProduct.ImageUrl;
                            ancPicture.Title = Common.GetTitleTag(itemsToDisplay[i]);
                        }
                        else
                            ancPicture.Attributes.Add("onclick", "javascript:window.open('" + imgProduct.ImageUrl + "')");

                        #endregion
                    }

                    #endregion

                    Label lblTitle = (Label) currentItem.FindControl("lblTitle");
                    Label lblShortText = (Label) currentItem.FindControl("lblShortText");
                    Label lblPrice = (Label)currentItem.FindControl("lblPrice");
                    Label lblStock = (Label) currentItem.FindControl("lblStock");
                    lblTitle.Text = itemsToDisplay[i].Title;
                    lblShortText.Text = itemsToDisplay[i].ShortText;
                    lblStock.Text = itemsToDisplay[i].Stock ? Common.GetText("CommonStockYes") : Common.GetText("CommonStockNo");

                    if (!int.Parse(itemsToDisplay[i].Price).Equals(0))
                        lblPrice.Text = Common.GetText("CommonPrice") + itemsToDisplay[i].Price + " " + Common.GetText("CommonPriceSuffix");
                    else
                        lblPrice.Text = "&nbsp;";
                    
                    #region Slideshow functionality on/off)

                    if (bool.Parse(ConfigurationManager.AppSettings["Slideshow"]))
                    {
                        Image imgProductLightbox = (Image) currentItem.FindControl("imgProductLightbox");
                        imgProductLightbox.ImageUrl = imgProduct.ImageUrl;

                        if (i > 0)
                        {
                            HtmlAnchor ancPictureLightbox = (HtmlAnchor) currentItem.FindControl("ancPictureLightbox");
                            ancPictureLightbox.HRef = imgProduct.ImageUrl;
                            ancPictureLightbox.Title = Common.GetTitleTag(itemsToDisplay[i]);
                            ancPictureLightbox.Attributes.Add("class", "lightview");
                            ancPictureLightbox.Attributes.Add("rel", "gallery[mygallery]");
                        }
                        else
                        {
                            ancSlideshow.HRef = imgProduct.ImageUrl;
                            ancSlideshow.Title = Common.GetTitleTag(itemsToDisplay[i]);
                            ancSlideshow.Attributes.Add("class", "lightview");
                            ancSlideshow.Attributes.Add("rel", "gallery[mygallery]");
                        }
                    }

                    #endregion

                    i++;
                }
                catch (Exception ex)
                {
                    Common.WriteToLog("Error on ProductList row created! Message: " + ex.Message + ", Stack: " + ex.StackTrace);
                }
            }
        }
    }
}