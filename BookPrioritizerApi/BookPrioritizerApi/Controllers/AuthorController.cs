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
    public class AuthorController : ControllerBase
    {

        public AuthorController()
        {
        }

        [HttpGet]
        [Route("[action]")]
        public List<Author> List()
        {
            return AuthorRepository.GetAll();
        }

        [HttpDelete]
        [Route("")]
        public void Delete()
        {
            AuthorRepository.DeleteUnused();
        }
    }
}
