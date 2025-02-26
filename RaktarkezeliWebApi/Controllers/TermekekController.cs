using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaktarkezeliWebApi.Data;
using RaktarkezeliWebApi.Models;

namespace RaktarkezeliWebApi.Controllers
{
    [Route("termekek")]
    [ApiController]
    public class TermekekController : ControllerBase
    {
        private readonly WareHouseDbContext _context;

        public TermekekController(WareHouseDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var products = await _context.Products
                .Include(p => p.BeszallitokId).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _context.Products.Include(p => p.Beszallitoks).FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Termekek product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Termekek updatedProduct)
        {
            if (id != updatedProduct.Id)
                return BadRequest();

            _context.Entry(updatedProduct).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
