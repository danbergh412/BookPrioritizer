using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPrioritizerApi.Models
{
    public class Relevance
    {
        public int RelevanceId { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public ICollection<TagBook> TagBook { get; set; }
    }
}
