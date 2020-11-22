using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPrioritizerApi.ViewModels
{
    public class UpdateBookStatusVM
    {
        public int BookId { get; set; }
        public int StatusId { get; set; }
    }
}
