using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Web;

namespace ArtistWebCatalog.Library
{
    public class Common
    {
        public static string GetText(string id)
        {
            string returnText;
            try
            {
                returnText = HttpContext.GetGlobalResourceObject(ConfigurationManager.AppSettings["Artist"], id).ToString();
            }
            catch (Exception e)
            {
                returnText = id;

                WriteToLog("Resource object [" + id == null ? "null" : id  + "] is missing! Message: " + e.Message);
            }

            return returnText;
        }

        public static string GetTitleTag(Product p)
        {
            string titleText = string.Empty;

            try
            {
                String[] values = ConfigurationManager.AppSettings["LightboxTitleTag"].ToLower().Split(',');
                
                if (values.Length > 0)
                {
                    bool title = false;
                    foreach (string value in values)
                    {
                        switch (value)
                        {
                            case "title":
                                title = true;
                                break;
                            case "shorttext":
                                titleText += p.ShortText + ", ";
                                break;
                            case "longtext":
                                titleText += p.LongText + ", ";
                                break;
                            case "stock":
                                titleText += (p.Stock ? GetText("CommonStockYes") : GetText("CommonStockNo")) + ", ";
                                break;
                            case "price":
                                titleText += GetText("CommonPrice") + p.Price + " " + GetText("CommonPriceSuffix") + ", ";
                                break;
                            case "size":
                                {
                                    titleText += GetText("ProductDetailSize");
                                    if (p.CategoryChild && !p.CategoryAdult)
                                        titleText += GetText("CommonCategoryChild");
                                    if (p.CategoryAdult && !p.CategoryChild)
                                        titleText += GetText("CommonCategoryAdult");
                                    if (p.CategoryChild && p.CategoryAdult)
                                        titleText += GetText("CommonCategoryChildAdult");
                                    titleText += ", ";
                                }
                                break;
                            case "fabric":
                                titleText += p.Fabric + ", ";
                                break;
                        }
                    }

                    if(title)
                        titleText = p.Title + " :: " + titleText;

                    titleText = titleText.Substring(0, titleText.Length - 2);
                }
            }
            catch(Exception e)
            {
                WriteToLog("GetTitleTag method failed! LightBoxTitleTag setting: " + ConfigurationManager.AppSettings["LightboxTitleTag"] + ", Message: " + e.Message);
            }

            return titleText;
        }

        public static ArrayList GetSmileys(string searchPattern)
        {
            string path = ConfigurationManager.AppSettings["ApplicationPath"] + ConfigurationManager.AppSettings["ImageSmileysPath"];
            ArrayList myFiles = new ArrayList();
            DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo fi in di.GetFiles(searchPattern))
                myFiles.Add(fi.Name);

            return myFiles;
        }

        public static void WriteToLog(string message)
        {
            // Specify file, instructions, and privelegdes
            FileStream file = new FileStream(ConfigurationManager.AppSettings["LogFilePath"], FileMode.Append, FileAccess.Write);

            // Create a new stream to write to the file
            StreamWriter sw = new StreamWriter(file);

            // Write a string to the file
            sw.WriteLine(message);

            // Close StreamWriter
            sw.Close();

            // Close file
            file.Close();
        }
    }
}