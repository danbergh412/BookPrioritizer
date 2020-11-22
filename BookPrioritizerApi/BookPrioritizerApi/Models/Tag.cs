using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPrioritizerApi.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public ICollection<TagBook> TagBook { get; set; }
    }
}
