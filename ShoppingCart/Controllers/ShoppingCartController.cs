using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Database;
using ShoppingCart.Model;
using ShoppingCart.Repo.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepo _repo;
        public ShoppingCartController(IShoppingCartRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ShoppingVM>> Get() => Ok(_repo.GetAllItems());

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var item = _repo.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ShoppingVM value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = _repo.Add(value);
            return CreatedAtAction("Get", new { id = item.Id }, item);
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(Guid id)
        {
            var existingItem = _repo.GetById(id);
            if (existingItem == null)
            {
                return NotFound();
            }
            _repo.Remove(existingItem);
            return NoContent();
        }
    }
}
