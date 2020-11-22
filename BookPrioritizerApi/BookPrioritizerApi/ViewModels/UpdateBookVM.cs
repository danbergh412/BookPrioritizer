using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPrioritizerApi.ViewModels
{
    public class UpdateBookVM
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public decimal? GoodReadsRating { get; set; }
        public int? GoodReadsVotes { get; set; }
        public decimal? AmazonRating { get; set; }
        public int? AmazonVotes { get; set; }
        public int StatusId { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public int? Year { get; set; }
    }
}
