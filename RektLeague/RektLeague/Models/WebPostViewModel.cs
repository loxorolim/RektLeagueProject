using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RektLeague.Models
{
    public class WebPostViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; } = DateTime.UtcNow;
        // [StringLength(255, MinimumLength = 5)]
        //public string MainText { get; set; }
        // public string SecondaryText { get; set; }
        public List<string> Texts { get; set; }
        public List<HttpPostedFileBase> Images { get; set; }
        public List<string> YoutubeURLs { get; set; }
        public List<string> DisplayOrder { get; set; }

        //public string VideoURL { get; set; }
        public string Author { get; set; }
        public int Category { get; set; }
    }
}