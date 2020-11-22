using BookPrioritizerApi.Extensions;
using BookPrioritizerApi.Models;
using BookPrioritizerApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPrioritizerApi.Mappers
{
    public static class BookMappers
    {
        public static Book UpdateFromVM(Book book, UpdateBookVM bookVM, Author author)
        {
            book.AmazonRating = bookVM.AmazonRating;
            book.AmazonVotes = bookVM.AmazonVotes;
            book.AuthorId = author.AuthorId;
            book.GoodReadsRating = bookVM.GoodReadsRating;
            book.GoodReadsVotes = bookVM.GoodReadsVotes;
            book.Name = bookVM.Name;
            book.StatusId = bookVM.StatusId;
            book.UniqueName = GetUniqueName(bookVM);
            book.Year = bookVM.Year;

            return book;
        }
        public static Book UpdateFromStatusVM(Book book, UpdateBookStatusVM statusUpdate)
        {
            book.StatusId = statusUpdate.StatusId;

            return book;
        }
        public static string GetUniqueName(UpdateBookVM bookVM)
        {
            var combinedInfo = (bookVM.Name + bookVM.AuthorFirstName + bookVM.AuthorLastName);
            var uniqueName = combinedInfo.LettersOnly().ToUpper();
            return uniqueName;
        }
        public static UpdateBookVM UpdateFromDuplicate(UpdateBookVM update, Book duplicate)
        {
            update.BookId = duplicate.BookId;
            update.StatusId = duplicate.StatusId;
            return update;
        }
        public static Book FromAddVM(UpdateBookVM bookVM, Author authorVM)
        {
            var uniqueName = GetUniqueName(bookVM);

            var book = new Book
            {
                DateAdded = DateTime.Now,
                AmazonRating = bookVM.AmazonRating,
                AmazonVotes = bookVM.AmazonVotes,
                AuthorId = authorVM.AuthorId,
                GoodReadsRating = bookVM.GoodReadsRating,
                GoodReadsVotes = bookVM.GoodReadsVotes,
                Name = bookVM.Name,
                StatusId = bookVM.StatusId,
                UniqueName = uniqueName,
                Year = bookVM.Year
            };

            return book;
        }
    }
}
