using BookPrioritizerApi.Extensions;
using BookPrioritizerApi.Mappers;
using BookPrioritizerApi.Models;
using BookPrioritizerApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BookPrioritizerApi.Repositories
{
    public static class BookRepository
    {

        public static Book GetByUniqueName(string uniqueName)
        {
            using (var db = new AppDbContext())
            {
                var book = db.Books
                    .Where(b => b.UniqueName == uniqueName.ToUpper()
                                )
                    .FirstOrDefault();

                return book;
            }
        }
        public static List<Book> GetBooks()
        {
            using (var db = new AppDbContext())
            {
                var books = db.Books.AsNoTracking()
                    .Include(b => b.Author)
                    .Include(b => b.Status)
                    .Select(
                        b => new Book
                        {
                            BookId = b.BookId,
                            AmazonRating = b.AmazonRating,
                            AmazonVotes = b.AmazonVotes,
                            AuthorId = b.AuthorId,
                            DateAdded = b.DateAdded,
                            GoodReadsRating = b.GoodReadsRating,
                            GoodReadsVotes = b.GoodReadsVotes,
                            Name = b.Name,
                            StatusId = b.StatusId,
                            UniqueName = b.UniqueName,
                            Year = b.Year,
                            Author = new Author
                            {
                                AuthorId = b.AuthorId,
                                FirstName = b.Author.FirstName,
                                LastName = b.Author.LastName
                            },
                            Status = new Status
                            {
                                Name = b.Status.Name,
                                StatusId = b.StatusId
                            }
                        }
                    )
                    .ToList();


                return books;
            }
        }

        public static Book AddUpdateBook(UpdateBookVM book)
        {
            if (book.BookId > 0)
            {
                return UpdateBook(book);
            }

            var duplicate = GetByUniqueName(BookMappers.GetUniqueName(book));

            if (duplicate != null)
            {
                book = BookMappers.UpdateFromDuplicate(book, duplicate);
                return UpdateBook(book);
            }
            else
            {
                return AddBook(book);
            }
        }

        public static List<Book> AddUpdateBookList(List<UpdateBookVM> updateBooks)
        {
            var returnBooks = new List<Book>();

            foreach(var book in updateBooks)
            {
                returnBooks.Add(AddUpdateBook(book));
            }

            return returnBooks;
        }

        public static Book UpdateBook(UpdateBookVM bookVM)
        {
            using (var db = new AppDbContext())
            {
                var book = db.Books.Find(bookVM.BookId);

                var authorVM = AuthorRepository.GetAddAuthor(bookVM.AuthorFirstName, bookVM.AuthorLastName);

                book = BookMappers.UpdateFromVM(book, bookVM, authorVM);

                db.Books.Update(book);
                db.SaveChanges();

                return book;
            }
        }
        public static Book UpdateBookStatus(UpdateBookStatusVM bookVM)
        {
            using (var db = new AppDbContext())
            {
                var book = db.Books.Find(bookVM.BookId);

                book = BookMappers.UpdateFromStatusVM(book, bookVM);

                db.Books.Update(book);
                db.SaveChanges();

                return book;
            }
        }

        public static Book AddBook(UpdateBookVM bookVM)
        {
            using (var db = new AppDbContext())
            {
                var author = AuthorRepository.GetAddAuthor(bookVM.AuthorFirstName, bookVM.AuthorLastName);

                var book = BookMappers.FromAddVM(bookVM, author);

                db.Add(book);
                db.SaveChanges();

                return book;
            }
        }

        public static void DeleteBook(int bookId)
        {
            using (var db = new AppDbContext())
            {
                var book = new Book
                {
                    BookId = bookId
                };

                db.Books.Remove(book);
                db.SaveChanges();
            }
        }

        public static void DeleteAllBooks()
        {
            using (var db = new AppDbContext())
            {
                var books = db.Books.ToList();

                foreach(var book in books)
                {
                    db.Books.Remove(book);
                }

                db.SaveChanges();
            }
        }

        public static void DeleteAllBooksAuthors()
        {
            DeleteAllBooks();
            AuthorRepository.DeleteUnused();
        }
    }
}
