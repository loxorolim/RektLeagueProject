using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RektLeague.Models
{
    public class FetchUserViewModel
    {
        public int CurrentPage { get; set; }
        public int NumberToFetch { get; set; }
        public string Order { get; set; }
        public string Orientation { get; set; }
        public string SearchParameter { get; set; }
        public List<string> AdminFilter { get; set; }
}
}