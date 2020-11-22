using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPrioritizerApi.Models
{
    public class TagBook
    {
        public int TagBookId { get; set; }
        public int BookId { get; set; }
        public int TagId { get; set; }
        public int? Shelved { get; set; }
        public int RelevanceId { get; set; }
        public int? GoodReadsRank { get; set; }
        public Book Book { get; set; }
        public Tag Tag { get; set; }
        public Relevance Relevance { get; set; }
    }
}
