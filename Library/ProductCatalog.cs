using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;

namespace ArtistWebCatalog.Library
{
    [Serializable]
    public class ProductCatalog 
    {
        public Product this[int index]
        {
            get
            {
                return Products[index];
            }
            set
            {
                Products[index] = value;
            }
        }
        public List<Product> Products { get; set; }

        public void EditProduct(int index, Product p)
        {
            Products.RemoveAt(index);
            Products.Insert(index, p);
            SerializeObject(ConfigurationManager.AppSettings["ProductFilePath"], this);
        }

        public void RemoveProduct(int index)
        {
            Products.RemoveAt(index);
            SerializeObject(ConfigurationManager.AppSettings["ProductFilePath"], this);
        }

        public void AddProduct(Product p)
        {
            Products.Insert(0, p);
            SerializeObject(ConfigurationManager.AppSettings["ProductFilePath"], this);
        }

        public static void SerializeObject(string filename, ProductCatalog objectToSerialize)
        {
            using (Stream stream = File.Open(filename, FileMode.Create))
            {
                try
                {
                    XmlSerializer bFormatter = new XmlSerializer(typeof(ProductCatalog));
                    bFormatter.Serialize(stream, objectToSerialize);
                }
                catch (Exception e)
                {
                    Common.WriteToLog("ProductCatalog could not be serialized! Message: " + e.Message);
                }
            }
        }

        public static ProductCatalog DeserializeObject(string filename)
        {
            using (Stream stream = File.Open(filename, FileMode.Open))
            {
                try
                {
                    XmlSerializer bFormatter = new XmlSerializer(typeof(ProductCatalog));
                    ProductCatalog objectToSerialize = (ProductCatalog)bFormatter.Deserialize(stream);
                    return objectToSerialize;
                }
                catch (Exception e)
                {
                    Common.WriteToLog("ProductCatalog could not be deserialized! Message: " + e.Message);

                    // File could not be deserialized - return new product catalog
                    return new ProductCatalog { Products = new List<Product>() };
                }
            }
        }
    }
}