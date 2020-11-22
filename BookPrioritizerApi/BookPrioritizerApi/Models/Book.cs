using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPrioritizerApi.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public string UniqueName { get; set; }
        public decimal? GoodReadsRating { get; set; }
        public int? GoodReadsVotes { get; set; }
        public decimal? AmazonRating { get; set; }
        public int? AmazonVotes { get; set; }
        public int StatusId { get; set; }
        public int? Year { get; set; }
        public DateTime DateAdded { get; set; }
        public int AuthorId { get; set; }
        public ICollection<TagBook> TagBook { get; set; }
        public Author Author { get; set; }
        public Status Status { get; set; }
    }
}
