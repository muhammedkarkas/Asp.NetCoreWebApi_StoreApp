using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Repositories.EFCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IRepositoryManager _manager;

        public BooksController(IRepositoryManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _manager.Book.GetAllBooks(false);
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
                var entity = _manager.Book.GetOneBookById(id, false);
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

                _manager.Book.CreateOneBook(book);
                _manager.Save();
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
                var entity = _manager.Book.GetOneBookById(id, true);

                //Book check
                if (entity is null)
                    return NotFound();

                //id check
                if (id != book.Id)
                    return BadRequest();

                entity.Title = book.Title;
                entity.Price = book.Price;

                _manager.Save();

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
                var entity = _manager.Book.GetOneBookById(id, false);

                if(entity is null)
                    return NotFound();

                _manager.Book.DeleteOneBook(entity);
                _manager.Save();

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
                var entity = _manager.Book.GetOneBookById(id, true);

                if(entity is null)
                    return NotFound();

                bookPatch.ApplyTo(entity);
                _manager.Book.Update(entity);

                return NoContent(); 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
