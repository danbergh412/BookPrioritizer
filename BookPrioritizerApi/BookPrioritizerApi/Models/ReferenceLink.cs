using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPrioritizerApi.Models
{
    public class ReferenceLink
    {
        public int ReferenceLinkId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
    }
}
