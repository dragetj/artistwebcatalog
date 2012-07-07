using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;

namespace ArtistWebCatalog.Library
{
    [Serializable]
    public class Guestbook
    {
        public GuestbookEntry this[int index]
        {
            get
            {
                return Guests[index];
            }
            set
            {
                Guests[index] = value;
            }
        }
        public List<GuestbookEntry> Guests { get; set; }

        public void EditEntry(int index, GuestbookEntry ge)
        {
            Guests.RemoveAt(index);
            Guests.Insert(index, ge);
            SerializeObject(ConfigurationManager.AppSettings["GuestbookFilePath"], this);
        }

        public void RemoveEntry(int index)
        {
            Guests.RemoveAt(index);
            SerializeObject(ConfigurationManager.AppSettings["GuestbookFilePath"], this);
        }

        public void InsertEntry(GuestbookEntry ge)
        {
            Guests.Insert(0, ge);
            SerializeObject(ConfigurationManager.AppSettings["GuestbookFilePath"], this);
        }

        public static void SerializeObject(string filename, Guestbook objectToSerialize)
        {
            using (Stream stream = File.Open(filename, FileMode.Create))
            {
                try
                {
                    XmlSerializer bFormatter = new XmlSerializer(typeof(Guestbook));
                    bFormatter.Serialize(stream, objectToSerialize);
                }
                catch (Exception e)
                {
                    Common.WriteToLog("Guestbook could not be serialized! Message: " + e.Message);
                }
            }
        }

        public static Guestbook DeserializeObject(string filename)
        {
            using (Stream stream = File.Open(filename, FileMode.Open))
            {
                try
                {
                    XmlSerializer bFormatter = new XmlSerializer(typeof(Guestbook));
                    Guestbook objectToSerialize = (Guestbook)bFormatter.Deserialize(stream);
                    return objectToSerialize;
                }
                catch (Exception e)
                {
                    Common.WriteToLog("Guestbook could not be deserialized! Message: " + e.Message);

                    // File could not be deserialized - return new guestbook
                    return new Guestbook { Guests = new List<GuestbookEntry>() };
                }
            }
        }
    }
}