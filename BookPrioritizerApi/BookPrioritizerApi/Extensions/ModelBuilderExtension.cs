using BookPrioritizerApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPrioritizerApi.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>().HasData(
                new Status { StatusId = 1, Name = "Read" },
                new Status { StatusId = 2, Name = "Downloaded" },
                new Status { StatusId = 3, Name = "Not Downloaded" },
                new Status { StatusId = 4, Name = "Not Found" },
                new Status { StatusId = 5, Name = "Hide" }
            );
            modelBuilder.Entity<Relevance>().HasData(
                new Relevance { RelevanceId = 1, Level = 0, Name = "Not Relevant" },
                new Relevance { RelevanceId = 2, Level = 1, Name = "Little Relevant" },
                new Relevance { RelevanceId = 3, Level = 2, Name = "Somewhat Relevant" },
                new Relevance { RelevanceId = 4, Level = 3, Name = "Mostly Relevant" },
                new Relevance { RelevanceId = 5, Level = 4, Name = "Completely Relevant" }
            );
        }
    }
}
