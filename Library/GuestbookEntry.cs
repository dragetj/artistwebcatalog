using System;

namespace ArtistWebCatalog.Library
{
    public class GuestbookEntry
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Email { get; set; }
        public string Date { get; set; }
    }
}