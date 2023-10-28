using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Repositories.EFCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly RepositoryContext _context;

        public BooksController(RepositoryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _context.Books.ToList();
                return Ok(books);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                var entity = _context.Books.SingleOrDefault(x => x.Id.Equals(id));
                if (entity is null)
                    return NotFound();

                return Ok(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody]Book book)
        {
            try
            {
                if(book is null)
                    return BadRequest();

                _context.Books.Add(book);
                _context.SaveChanges();
                return StatusCode(201, book);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")]int id, [FromBody]Book book)
        {
            try
            {
                var entity = _context.Books.Where(x => x.Id.Equals(id)).SingleOrDefault();

                //Book check
                if (entity is null)
                    return NotFound();

                //id check
                if (id != book.Id)
                    return BadRequest();

                entity.Title = book.Title;
                entity.Price = book.Price;

                _context.SaveChanges();

                return Ok(book);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBooks([FromRoute(Name = "id")]int id)
        {
            try
            {
                var entity = _context.Books.Where(x => x.Id.Equals(id)).SingleOrDefault();

                if(entity is null)
                    return NotFound();

                _context.Books.Remove(entity);
                _context.SaveChanges();

                return NoContent();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute]int id, [FromBody]JsonPatchDocument<Book> bookPatch)
        {
            try
            {
                var entity = _context.Books.Where(x => x.Id.Equals(id)).SingleOrDefault();

                if(entity is null)
                    return NotFound();

                bookPatch.ApplyTo(entity);
                _context.SaveChanges();

                return NoContent(); 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
