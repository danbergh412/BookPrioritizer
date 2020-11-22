using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPrioritizerApi.Models
{
    public class Status
    {
        public int StatusId { get; set; }
        public string Name { get; set; }

        public ICollection<Book> Book { get; set; }
    }
}
