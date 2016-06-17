using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RektLeague.Models
{
    public class WebPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Author { get; set; }
        public int Category { get; set; }
        public virtual List<Element> Elements { get; set; }
    }

    public class Element
    {
        public enum Type
        {
            Text,
            Image,
            YoutubeURL,
        }

        public int Id { get; set; }
        public int WebPostId { get; set; }
        public virtual WebPost WebPost { get; set; }
        public Type ElementType { get; set; }
        //   public int DisplayOrder { get; set; }
        public byte[] PostBytes { get; set; }
        public string PostString { get; set; }

    }
}