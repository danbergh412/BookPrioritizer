using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPrioritizerApi.Models;
using BookPrioritizerApi.Repositories;
using BookPrioritizerApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookPrioritizerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {

        public BookController()
        {
        }

        [HttpGet]
        [Route("[action]")]
        public List<Book> List()
        {
            return BookRepository.GetBooks();
        }
        [HttpPost]
        [Route("")]
        public Book Post([FromBody] UpdateBookVM model)
        {
            return BookRepository.AddUpdateBook(model);
        }

        [HttpPost]
        [Route("[action]")]
        public Book PostStatus([FromBody] UpdateBookStatusVM model)
        {
            return BookRepository.UpdateBookStatus(model);
        }

        [HttpPost]
        [Route("[action]")]
        public List<Book> PostList([FromBody] List<UpdateBookVM> model)
        {
            return BookRepository.AddUpdateBookList(model);
        }

        [HttpDelete("{bookId}")]
        [Route("")]
        public void Delete(int bookId)
        {
            BookRepository.DeleteBook(bookId);
        }
        [HttpDelete]
        [Route("[action]")]
        public void DeleteAll()
        {
            BookRepository.DeleteAllBooksAuthors();
        }
    }
}
