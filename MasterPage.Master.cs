using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ArtistWebCatalog.Library;

namespace ArtistWebCatalog
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        private ProductCatalog pc;
        
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
            if (!Page.IsPostBack)
            {
                ValidateProductCatalog();
                pc = (ProductCatalog) Application["ProductCatalog"];

                #region Set artist CSS
                HtmlLink cssControl = FindControl("css") as HtmlLink;
                cssControl.Href = ConfigurationManager.AppSettings["CssPath"] + ConfigurationManager.AppSettings["Artist"] + ".css";
                #endregion

                SetTextOnPage();

                #region Build tree node categories

                TreeNodeCollection tnc = new TreeNodeCollection();
                ArrayList addedCategories = new ArrayList();
                int counter = 0;
                foreach (Product item in pc.Products)
                {
                    TreeNode tn = new TreeNode();
                    tn.Text = item.Category;
                    tn.SelectAction = TreeNodeSelectAction.SelectExpand;
                    tn.Value = "ProductList.aspx?category=" + item.Category;

                    if (!addedCategories.Contains(item.Category))
                    {
                        addedCategories.Add(item.Category);

                        bool categoryChild = false;
                        bool categoryAdult = false;
                        foreach (Product subItem in pc.Products)
                        {
                            if (subItem.Category.Equals(item.Category))
                            {
                                if (subItem.CategoryChild)
                                    categoryChild = true;

                                if (subItem.CategoryAdult)
                                    categoryAdult = true;
                            }
                        }

                        tnc.Add(tn);

                        if (categoryChild)
                        {
                            TreeNode categoryChildNode = new TreeNode();
                            categoryChildNode.Text = Common.GetText("CommonCategoryChild");
                            categoryChildNode.Value = "ProductList.aspx?category=" + item.Category + "&size=child";

                            tnc[counter].ChildNodes.Add(categoryChildNode);
                        }

                        if (categoryAdult)
                        {
                            TreeNode categoryAdultNode = new TreeNode();
                            categoryAdultNode.Text = Common.GetText("CommonCategoryAdult");
                            categoryAdultNode.Value = "ProductList.aspx?category=" + item.Category + "&size=adult";

                            tnc[counter].ChildNodes.Add(categoryAdultNode);
                        }

                        counter++;
                    }
                }

                foreach (TreeNode node in tnc)
                {
                    tvProducts.Nodes.Add(node);
                }

                if (Session["treeState"] != null)
                    RestoreTreeState(tvProducts, (Dictionary<string, bool?>) Session["treeState"]);

                tvProducts.DataBind();

                #endregion
            }
        }

        private void SetTextOnPage()
        {
            litDate.Text = DateTime.Now.Year.ToString();
            litLogoText.Text = Common.GetText("MasterPageLogoText");
            lblTreeViewTitle.Text = Common.GetText("MasterPageProductLink");
            hlColorMap.Text = Common.GetText("MasterPageColorMapLink");
            hlExhibitions.Text = Common.GetText("MasterPageExhibitionsLink");
            hlGuestbook.Text = Common.GetText("MasterPageGuestbookLink");
            hlContact.Text = Common.GetText("MasterPageContactLink");
            hlAboutMe.Text = Common.GetText("MasterPageAboutMeLink");
        }

        protected void tvProducts_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (tvProducts.SelectedNode.Value != string.Empty)
                Response.Redirect(tvProducts.SelectedNode.Value);
        }

        protected void tvProducts_Unload(object sender, EventArgs e)
        {
            // save the state of all nodes.
            Session["treeState"] = SaveTreeState(tvProducts);
        }

        private static Dictionary<string, bool?> SaveTreeState(TreeView tree)
        {
            Dictionary<string, bool?> nodeStates = new Dictionary<string, bool?>();
            for (int i = 0; i < tree.Nodes.Count; i++)
            {
                nodeStates.Add(tree.Nodes[i].Text, tree.Nodes[i].Expanded);
            }

            return nodeStates;
        }

        private static void RestoreTreeState(TreeView tree, Dictionary<string, bool?> treeState)
        {
            for (int i = 0; i < tree.Nodes.Count; i++)
            {
                if (treeState.ContainsKey(tree.Nodes[i].Text))
                {
                    if(treeState[tree.Nodes[i].Text].HasValue)
                    {
                        bool? test = treeState[tree.Nodes[i].Text];
                        if ((bool) test)
                            tree.Nodes[i].Expand();
                        else
                            tree.Nodes[i].Collapse();
                    }
                }
            }
        }
    }
}