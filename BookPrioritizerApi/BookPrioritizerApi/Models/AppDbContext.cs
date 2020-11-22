using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPrioritizerApi.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BookPrioritizerApi.Models
{
    public class AppDbContext: DbContext
    {
        public DbSet<Book> Books { get; set; } 
        public DbSet<Author> Authors { get; set; }
        public DbSet<TagBook> TagBooks { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Relevance> Relevances { get; set; }
        public DbSet<ReferenceLink> ReferenceLinks { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"server=(localdb)\MSSQLLocalDB;database=BookPrioritizer;Trusted_Connection=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
    }
}
