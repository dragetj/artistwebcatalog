using System;

namespace ArtistWebCatalog.Library
{
    public class Product
    {
        //[XmlAttribute]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ShortText { get; set; }
        public string LongText { get; set; }
        public string Image { get; set; }
        public bool Stock { get; set; }
        public string Price { get; set; }
        public string Category { get; set; }
        public bool CategoryChild { get; set; }
        public bool CategoryAdult { get; set; }
        public string Fabric { get; set; }
    }
}