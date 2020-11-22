using BookPrioritizerApi.Mappers;
using BookPrioritizerApi.Models;
using BookPrioritizerApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPrioritizerApi.Repositories
{
    public static class AuthorRepository
    {
        public static List<Author> GetAll()
        {
            using (var db = new AppDbContext())
            {
                var authors = db.Authors.ToList();

                return authors;
            }
        }
        public static Author GetByName(string first, string last)
        {
            using (var db = new AppDbContext())
            {
                var author = db.Authors
                    .Where(a => a.FirstName.ToLower() == first.ToLower()
                                && a.LastName.ToLower() == last.ToLower()
                                )
                    .FirstOrDefault();

                return author; 
            }
        }
        public static Author AddAuthor(string first, string last)
        {
            using (var db = new AppDbContext())
            {
                var author = new Author
                {
                    FirstName = first,
                    LastName = last
                };
                db.Add(author);
                db.SaveChanges();

                return author;
            }
        }

        public static Author GetAddAuthor(string first, string last)
        {
            var author = GetByName(first, last);

            if (author == null)
            {
                author = AddAuthor(first, last);
            }

            return author;
        }

        public static void DeleteUnused()
        {
            using (var db = new AppDbContext())
            {
                var authors = db.Authors.Where(a => a.Book.Count == 0).ToList();

                foreach (var author in authors)
                {
                    db.Authors.Remove(author);
                    db.SaveChanges();
                }
            }
        }
        public static void DeleteAuthor(int authorId)
        {
            using (var db = new AppDbContext())
            {
                var author = new Author
                {
                    AuthorId = authorId
                };

                db.Authors.Remove(author);
                db.SaveChanges();
            }
        }
    }
}
