using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ArtistWebCatalog.Library;

namespace ArtistWebCatalog
{
    public partial class Admin : Page
    {
        private ProductCatalog pc;
        private Library.Guestbook g;
        public string artistCss;
        private int i;
        private const int colEdit = 0;
        private const int colImage = 1;
        private const int colTitle = 2;
        private const int colShortText = 3;
        private const int colLongText = 4;
        private const int colPrice = 5;
        private const int colStock = 6;
        private const int colCategory = 7;
        private const int colSize = 8;
        private const int colFabric = 9;
        private const int colDelete = 10;

        private void ValidateApplicationObjects()
        {
            // Validate productCatalog application object
            if (Application["ProductCatalog"] == null)
                Application["ProductCatalog"] = pc = ProductCatalog.DeserializeObject(ConfigurationManager.AppSettings["ProductFilePath"]);
            else
            {
                pc = (ProductCatalog)Application["ProductCatalog"];
                if (pc.Products == null || pc.Products.Count == 0)
                    Application["ProductCatalog"] = pc = ProductCatalog.DeserializeObject(ConfigurationManager.AppSettings["ProductFilePath"]);
            }

            // Validate guestbook application object
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
            ValidateApplicationObjects();
            //pc = (ProductCatalog)Application["ProductCatalog"];
            //g = (Library.Guestbook) Application["Guestbook"];

            if(!Page.IsPostBack)
            {
                SetTextOnPage();

                if (Session["productResult"] != null)
                {
                    lblProductInputResult.Text = Session["productResult"].ToString();

                    if (lblProductInputResult.Text.Equals(Common.GetText("AdminProductInputDeleted")))
                        lblProductInputResult.CssClass = "failure";

                    Session["productResult"] = null;
                }

                if (Session["guestbookResult"] != null)
                {
                    lblGuestbookInputResult.Text = Session["guestbookResult"].ToString();

                    if (lblGuestbookInputResult.Text.Equals(Common.GetText("AdminGuestbookDeleted")))
                        lblGuestbookInputResult.CssClass = "failure";

                    Session["guestbookResult"] = null;
                }

                #region Populate drop down lists
                ListItemCollection licCategories = new ListItemCollection();
                ListItemCollection licFabrics = new ListItemCollection();
                ArrayList alCategories = new ArrayList();
                ArrayList alFabrics = new ArrayList();
                foreach (Product item in pc.Products)
                {
                    ListItem liCategory = new ListItem {Text = item.Category, Value = item.Category};
                    ListItem liFabric = new ListItem { Text = item.Fabric, Value = item.Fabric };

                    if (!alCategories.Contains(item.Category))
                    {
                        alCategories.Add(item.Category);
                        licCategories.Add(liCategory);
                    }

                    if (!alFabrics.Contains(item.Fabric))
                    {
                        alFabrics.Add(item.Fabric);
                        licFabrics.Add(liFabric);
                    }
                }
                ddlProductInputCategory.DataSource = licCategories;
                ddlProductInputCategory.DataBind();

                ddlProductInputFabric.DataSource = licFabrics;
                ddlProductInputFabric.DataBind();
                #endregion

                #region Smileys
                if (bool.Parse(ConfigurationManager.AppSettings["GuestbookSmileys"]))
                    Session["smileys"] = Common.GetSmileys("*.gif");
                #endregion

                gvProducts.DataSource = pc.Products;
                gvProducts.DataBind();

                lvGuests.DataSource = g.Guests;
                lvGuests.DataBind();

                if(Session["activeView"] != null)
                {
                    switch (int.Parse(Session["activeView"].ToString()))
                    {
                        case 0: //Products
                            {
                                btnViewProducts.Enabled = false;
                                btnViewGuestbook.Enabled = true;
                                btnViewAdmin.Enabled = true;
                                mvAdmin.SetActiveView(viewProducts);
                                return;
                            }
                        case 1: //Guestbook
                            {
                                btnViewProducts.Enabled = true;
                                btnViewGuestbook.Enabled = false;
                                btnViewAdmin.Enabled = true;
                                mvAdmin.SetActiveView(viewGuestbook);
                                return;
                            }
                        case 2: //Admin
                            {
                                btnViewProducts.Enabled = true;
                                btnViewGuestbook.Enabled = true;
                                btnViewAdmin.Enabled = false;
                                mvAdmin.SetActiveView(viewForceObjectReload);
                                return;
                            }
                    }
                }
            }
        }

        private void SetTextOnPage()
        {
            lblProductInput.Text = Common.GetText("AdminProductInputAdd");
            lblProductInputTitle.Text = Common.GetText("AdminProductInputTitle");
            txtProductInputTitle.Text = Common.GetText("AdminProductInputTitleExText");
            lblProductInputShortText.Text = Common.GetText("AdminProductInputShortText");
            txtProductInputShortText.Text = Common.GetText("AdminProductInputShortTextExText");
            lblProductInputLongText.Text = Common.GetText("AdminProductInputLongText");
            txtProductInputLongText.Text = Common.GetText("AdminProductInputLongTextExText");
            lblProductInputImage.Text = Common.GetText("AdminProductInputImage");
            lblProductInputImageInfo.Text = Common.GetText("AdminProductInputImageInfo");
            lblProductInputPrice.Text = Common.GetText("AdminProductInputPrice");
            txtProductInputPrice.Text = Common.GetText("AdminProductInputPriceExText");
            lblProductInputStock.Text = Common.GetText("AdminProductInputStock");
            lblProductInputCategory.Text = Common.GetText("AdminProductInputCategory");
            txtProductInputCategory.Text = Common.GetText("AdminProductInputCategoryExText");
            lblProductInputSize.Text = Common.GetText("AdminProductInputSize");
            lblProductInputCategoryChild.Text = Common.GetText("AdminProductInputCategoryChild");
            lblProductInputCategoryAdult.Text = Common.GetText("AdminProductInputCategoryAdult");
            lblProductInputFabric.Text = Common.GetText("AdminProductInputFabric");
            txtProductInputFabric.Text = Common.GetText("AdminProductInputFabricExText");
            btnProductInputSave.Text = Common.GetText("AdminProductInputSave");
            btnProductInputCancel.Text = Common.GetText("AdminProductInputCancel");
        }

        #region Products
        protected void bntViewProducts_Click(object sender, EventArgs e)
        {
            lblProductInputResult.Text = string.Empty;

            btnViewProducts.Enabled = false;
            btnViewGuestbook.Enabled = true;
            btnViewAdmin.Enabled = true;

            // Reload products to trigger gvProducts_RowCreated()
            gvProducts.DataSource = pc.Products;
            gvProducts.DataBind();

            mvAdmin.SetActiveView(viewProducts);
            Session["activeView"] = mvAdmin.ActiveViewIndex;
        }
        protected void btnProductInputSave_Click(object sender, EventArgs e)
        {
            // Validate price input
            int price;
            if (!int.TryParse(txtProductInputPrice.Text, out price))
            {
                lblProductInputResult.Text = Common.GetText("AdminProductInputErrorPrice");
                lblProductInputResult.CssClass = "failure";
                return;
            }

            Product p = new Product
                            {
                                Id = (litId.Text.Equals(string.Empty) ? Guid.NewGuid() : new Guid(litId.Text)), 
                                Title = txtProductInputTitle.Text, 
                                ShortText = txtProductInputShortText.Text, 
                                LongText = txtProductInputLongText.Text, 
                                Image = fuProductInputImage.FileName != string.Empty ? fuProductInputImage.FileName : litImage.Text, 
                                Price = txtProductInputPrice.Text, 
                                Stock = cbProductInputStock.Checked,
                                Category = (txtProductInputCategory.Text != string.Empty && txtProductInputCategory.Text != Common.GetText("AdminProductInputCategoryExText")) ? txtProductInputCategory.Text : ddlProductInputCategory.SelectedValue,
                                CategoryChild = cbProductInputCategoryChild.Checked, 
                                CategoryAdult = cbProductInputCategoryAdult.Checked,
                                Fabric = (txtProductInputFabric.Text != string.Empty && txtProductInputFabric.Text != Common.GetText("AdminProductInputFabricExText")) ? txtProductInputFabric.Text : ddlProductInputFabric.SelectedValue
                            };

            // Save file to disk
            if (fuProductInputImage.HasFile)
                p.Image = SaveFile();
            if (string.IsNullOrEmpty(p.Image))
                p.Image = "noimage.jpg";

            if(litEdit.Text.Equals(string.Empty))
                pc.AddProduct(p);
            else
                pc.EditProduct(Int16.Parse(litEdit.Text), p);

            Application["ProductCatalog"] = pc;

            Session["productResult"] = Common.GetText("AdminProductInputAdded");
            Response.Redirect("admin.aspx");
        }
        protected void btnProductInputCancel_Click(object sender, EventArgs e)
        {
            // Cancel index helper
            litEdit.Text = string.Empty;
            // Cancel id helper
            litId.Text = string.Empty;

            lblProductInput.Text = Common.GetText("AdminProductInputAdd");
            txtProductInputTitle.Text = Common.GetText("AdminProductInputTitle");
            txtProductInputShortText.Text = Common.GetText("AdminProductInputShortText");
            txtProductInputLongText.Text = Common.GetText("AdminProductInputLongText");
            lblProductInputImageInfo.Visible = false;
            txtProductInputPrice.Text = Common.GetText("AdminProductInputPrice");
            cbProductInputStock.Checked = false;
            ddlProductInputCategory.SelectedIndex = 0;
            cbProductInputCategoryChild.Checked = false;
            cbProductInputCategoryAdult.Checked = false;
            ddlProductInputFabric.SelectedIndex = 0;
            lblProductInputResult.Text = "";

            // Turn on onfocus and onblur javascript function
            JSattributes(true);

            Response.Redirect("admin.aspx");
        }

        protected void gvProducts_RowSelecting(object sender, GridViewSelectEventArgs e)
        {
            // Set index helper
            litEdit.Text = e.NewSelectedIndex.ToString();
            // Set id helper
            litId.Text = pc.Products[e.NewSelectedIndex].Id.ToString();

            lblProductInput.Text = Common.GetText("AdminProductInputEdit");
            txtProductInputTitle.Text = pc.Products[e.NewSelectedIndex].Title;
            txtProductInputShortText.Text = pc.Products[e.NewSelectedIndex].ShortText;
            txtProductInputLongText.Text = pc.Products[e.NewSelectedIndex].LongText;
            litImage.Text = pc.Products[e.NewSelectedIndex].Image;
            lblProductInputImageInfo.Visible = true;
            txtProductInputPrice.Text = pc.Products[e.NewSelectedIndex].Price;
            cbProductInputStock.Checked = pc.Products[e.NewSelectedIndex].Stock;
            ddlProductInputCategory.SelectedValue = pc.Products[e.NewSelectedIndex].Category;
            txtProductInputCategory.Text = string.Empty;
            cbProductInputCategoryChild.Checked = pc.Products[e.NewSelectedIndex].CategoryChild;
            cbProductInputCategoryAdult.Checked = pc.Products[e.NewSelectedIndex].CategoryAdult;
            ddlProductInputFabric.SelectedValue = pc.Products[e.NewSelectedIndex].Fabric;
            txtProductInputFabric.Text = string.Empty;
            lblProductInputResult.Text = "";

            // Turn off onfocus and onblur javascript function
            JSattributes(false);
        }
        protected void gvProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string filePath = ConfigurationManager.AppSettings["ApplicationPath"] + ConfigurationManager.AppSettings["ImagePath"] + pc[e.RowIndex].Image;
            
            pc.RemoveProduct(e.RowIndex);
            Application["ProductCatalog"] = pc;

            try
            {
                FileInfo theFile = new FileInfo(filePath);
                if(theFile.Exists)
                    File.Delete(filePath);
                else
                    throw new FileNotFoundException();
            }
            catch (FileNotFoundException ex)
            {
                Common.WriteToLog("Error on Admin (product) row deleting! Message: " + ex.Message + ", Stack: " + ex.StackTrace);
            }
            catch (Exception ex)
            {
                Common.WriteToLog("Error on Admin (product) row deleting! Message: " + ex.Message + ", Stack: " + ex.StackTrace);
            }

            Session["productResult"] = Common.GetText("AdminProductInputDeleted");
            Response.Redirect("admin.aspx");
        }
        protected void gvProducts_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (pc != null)
            {
                if (e.Row.RowType != DataControlRowType.Header && e.Row.RowType != DataControlRowType.Footer)
                {
                    try
                    {
                        Product p = (Product) e.Row.DataItem;

                        Label lblShortText = (Label) e.Row.FindControl("lblShortText");
                        lblShortText.Text = p.ShortText;

                        Label lblLongText = (Label) e.Row.FindControl("lblLongText");
                        lblLongText.Text = p.LongText.Length <= 60 ? p.LongText : p.LongText.Substring(0, 60) + "...";

                        CheckBox cbStock = (CheckBox) e.Row.FindControl("cbStock");
                        cbStock.Checked = p.Stock;

                        Label lblSize = (Label) e.Row.FindControl("lblSize");
                        if (p.CategoryChild && !p.CategoryAdult)
                            lblSize.Text = Common.GetText("AdminProductOutputSizeChild");
                        if (p.CategoryAdult && !p.CategoryChild)
                            lblSize.Text += Common.GetText("AdminProductOutputSizeAdult");
                        if (p.CategoryChild && p.CategoryAdult)
                            lblSize.Text = Common.GetText("AdminProductOutputSizeChildAdult");

                        Label lblFabric = (Label) e.Row.FindControl("lblFabric");
                        lblFabric.Text = p.Fabric;

                        #region ProductDetail page on/off

                        HtmlAnchor ancImage = (HtmlAnchor) e.Row.FindControl("ancImage");
                        HtmlAnchor ancTitle = (HtmlAnchor) e.Row.FindControl("ancTitle");
                        if (bool.Parse(ConfigurationManager.AppSettings["ProductDetailPage"]))
                        {
                            ancImage.HRef = "ProductDetail.aspx?id=" + p.Id;
                            ancTitle.HRef = "ProductDetail.aspx?id=" + p.Id;
                        }
                        else
                        {
                            #region Lightbox functionality on/off

                            string imagePath = ConfigurationManager.AppSettings["ImagePath"] + p.Image;
                            if (bool.Parse(ConfigurationManager.AppSettings["Lightbox"]))
                            {
                                ancImage.Attributes.Add("rel", "lightbox");
                                ancImage.HRef = imagePath;
                                ancImage.Title = Common.GetTitleTag(p);

                                ancTitle.Attributes.Add("rel", "lightbox");
                                ancTitle.HRef = imagePath;
                                ancTitle.Title = Common.GetTitleTag(p);
                            }
                            else
                            {
                                ancImage.Attributes.Add("onclick", "javascript:window.open('" + imagePath + "')");
                                ancTitle.Attributes.Add("onclick", "javascript:window.open('" + imagePath + "')");
                            }

                            #endregion
                        }

                        #endregion
                    }
                    catch (Exception ex)
                    {
                        Common.WriteToLog("Error on Admin (product) row created! Message: " + ex.Message + ", Stack: " + ex.StackTrace);
                    }
                }

                if (ConfigurationManager.AppSettings["Artist"] == "rrart")
                {
                    e.Row.Cells[colLongText].Visible = false;
                    e.Row.Cells[colCategory].Visible = false;
                    e.Row.Cells[colSize].Visible = false;
                    e.Row.Cells[colFabric].Visible = false;
                }
            }
        }

        /// <summary>
        /// Turn on/off onfocus and onblur javascript function
        ///     -True=ON
        ///     -False=OFF
        /// </summary>
        /// <param name="state"></param>
        private void JSattributes(bool state)
        {
            if(state)
            {
                txtProductInputTitle.Attributes.Add("onfocus", "blank(this)");
                txtProductInputTitle.Attributes.Add("onblur", "unblank(this)");
                txtProductInputShortText.Attributes.Add("onfocus", "blank(this)");
                txtProductInputShortText.Attributes.Add("onblur", "unblank(this)");
                txtProductInputLongText.Attributes.Add("onfocus", "blank(this)");
                txtProductInputLongText.Attributes.Add("onblur", "unblank(this)");
                txtProductInputPrice.Attributes.Add("onfocus", "blank(this)");
                txtProductInputPrice.Attributes.Add("onblur", "unblank(this)");
                txtProductInputCategory.Attributes.Add("onfocus", "blank(this)");
                txtProductInputCategory.Attributes.Add("onblur", "unblank(this)");
                txtProductInputFabric.Attributes.Add("onfocus", "blank(this)");
                txtProductInputFabric.Attributes.Add("onblur", "unblank(this)");
            }
            else
            {
                txtProductInputTitle.Attributes.Remove("onfocus");
                txtProductInputTitle.Attributes.Remove("onblur");
                txtProductInputShortText.Attributes.Remove("onfocus");
                txtProductInputShortText.Attributes.Remove("onblur");
                txtProductInputLongText.Attributes.Remove("onfocus");
                txtProductInputLongText.Attributes.Remove("onblur");
                txtProductInputPrice.Attributes.Remove("onfocus");
                txtProductInputPrice.Attributes.Remove("onblur");
                txtProductInputCategory.Attributes.Remove("onfocus");
                txtProductInputCategory.Attributes.Remove("onblur");
                txtProductInputFabric.Attributes.Remove("onfocus");
                txtProductInputFabric.Attributes.Remove("onblur");
            }
        }
        private string SaveFile()
        {
            string fileName = string.Empty;
            string savePath = string.Empty;
            try
            {
                // Specify the path to save the uploaded file to.
                savePath = ConfigurationManager.AppSettings["ApplicationPath"] + ConfigurationManager.AppSettings["ImagePath"];

                // Get the name of the file to upload.
                fileName = fuProductInputImage.FileName;

                // Create the path and file name to check for duplicates.
                string pathToCheck = savePath + fileName;

                // Create a temporary file name to use for checking duplicates.
                string tempfileName = "";

                // Check to see if a file already exists with the
                // same name as the file to upload.        
                if (System.IO.File.Exists(pathToCheck))
                {
                    int counter = 2;
                    while (System.IO.File.Exists(pathToCheck))
                    {
                        // if a file with this name already exists,
                        // prefix the filename with a number.
                        tempfileName = fileName.Insert(fileName.Length - 4, counter.ToString());
                        pathToCheck = savePath + tempfileName;
                        counter++;
                    }

                    fileName = tempfileName;

                    // Notify the user that the file name was changed.
                    lblProductInputImageInfo.Text = Common.GetText("AdminProductUploadFileExist") + fileName;
                    lblProductInputImageInfo.Visible = true;
                }
                else
                {
                    // Notify the user that the file was saved successfully.
                    lblProductInputImageInfo.Text = Common.GetText("AdminProductUploadFileOk");
                    lblProductInputImageInfo.Visible = true;
                }

                // Append the name of the file to upload to the path.
                savePath += fileName;

                // Call the SaveAs method to save the uploaded
                // file to the specified directory.
                fuProductInputImage.SaveAs(savePath);
            }
            catch (Exception e)
            {
                Common.WriteToLog("SaveFile method failed during file save to path: " + savePath + ", Message: " + e.Message + ", Stack: " + e.StackTrace);
            }
            return fileName;
        }
        #endregion

        #region Guestbook
        protected void btnViewGuestbook_Click(object sender, EventArgs e)
        {
            lblGuestbookInputResult.Text = string.Empty;

            btnViewProducts.Enabled = true;
            btnViewGuestbook.Enabled = false;
            btnViewAdmin.Enabled = true;

            mvAdmin.SetActiveView(viewGuestbook);
            Session["activeView"] = mvAdmin.ActiveViewIndex;
        }
        protected void lvGuests_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (g != null)
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
                            foreach (string myFile in (ArrayList) Session["smileys"])
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
                        Common.WriteToLog("Error on Admin (guestbook) row created! Message: " + ex.Message + ", Stack: " + ex.StackTrace);
                    }
                }
            }
        }
        protected void lvGuests_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            g.RemoveEntry(e.ItemIndex);
            Application["Guestbook"] = g;

            Session["guestbookResult"] = Common.GetText("AdminGuestbookDeleted");
            Response.Redirect("admin.aspx");
        }
        #endregion

        #region Admin
        protected void btnViewAdmin_Click(object sender, EventArgs e)
        {
            lblForceObjectReload.Text = string.Empty;

            btnViewProducts.Enabled = true;
            btnViewGuestbook.Enabled = true;
            btnViewAdmin.Enabled = false;

            mvAdmin.SetActiveView(viewForceObjectReload);
            Session["activeView"] = mvAdmin.ActiveViewIndex;
        }

        protected void btnForceObjectReload_Click(object sender, EventArgs e)
        {
            // Validate productCatalog application object
            Application["ProductCatalog"] = ProductCatalog.DeserializeObject(ConfigurationManager.AppSettings["ProductFilePath"]);
            Application["GuestBook"] = Library.Guestbook.DeserializeObject(ConfigurationManager.AppSettings["GuestbookFilePath"]);

            lblForceObjectReload.Text = "OK";
        }
        #endregion
    }
}