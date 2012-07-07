using System;
using System.Configuration;
using System.Web.UI;
using ArtistWebCatalog.Library;

namespace ArtistWebCatalog
{
    public partial class Default : Page
    {
        private ProductCatalog pc;

        private void ValidateProductCatalog()
        {
            // Validate application object
            if (Application["ProductCatalog"] == null)
                Application["ProductCatalog"] = pc = ProductCatalog.DeserializeObject(ConfigurationManager.AppSettings["ProductFilePath"]);
            else
            {
                pc = (ProductCatalog) Application["ProductCatalog"];
                if(pc.Products == null || pc.Products.Count == 0)
                    Application["ProductCatalog"] = pc = ProductCatalog.DeserializeObject(ConfigurationManager.AppSettings["ProductFilePath"]);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ValidateProductCatalog();

            SetTextOnPage();
        }

        private void SetTextOnPage()
        {
            lblInfoText.Text = Common.GetText("DefaultInfoText");
        }
    }
}