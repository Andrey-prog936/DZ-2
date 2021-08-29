using Librarry.Data.Services;
using Librarry.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private  readonly AuthorsService _authorService;

        public AuthorsController(AuthorsService authorsService)
        {
            _authorService = authorsService;
        }

        [HttpPost("add-author")]
        public IActionResult AddAuthor([FromBody] AuthorVM author)
        {
            var newAuthor = _authorService.AddAuthor(author);
            return Created(nameof(AddAuthor), newAuthor);
        }

        [HttpGet("get-author-with-books/{id}")]
        public IActionResult GetAuthorsWithBooks(int id)
        {
            var _author = _authorService.GetAuthorWithBooks(id);

            if(_author != null)
            {
                return Ok(_author);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
