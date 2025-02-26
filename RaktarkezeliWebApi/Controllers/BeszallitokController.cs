using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using RaktarkezeliWebApi.Data;
using RaktarkezeliWebApi.Models;

namespace RaktarkezeliWebApi.Controllers
{
    [Route("beszallitok")]
    [ApiController]
    public class BeszallitokController : ControllerBase
    {
        private readonly WareHouseDbContext _context;

        public BeszallitokController(WareHouseDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetSuppliers([FromQuery] int oldal = 1, [FromQuery] int pageSize = 10)
        {
            var osszesBeszallito = await _context.Suppliers.CountAsync();
            var beszallitok = await _context.Suppliers
                .Skip((oldal - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var response = new
            {
                TotalPages = (int)System.Math.Ceiling(osszesBeszallito / (double)pageSize),
                CurrentPage = oldal,
                Suppliers = beszallitok
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplier(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null)
                return NotFound();

            return Ok(supplier);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSupplier([FromBody] Beszallitok supplier)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSupplier), new { id = supplier.Id }, supplier);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, [FromBody] Termekek updatedSupplier)
        {
            if (id != updatedSupplier.Id)
                return BadRequest();

            _context.Entry(updatedSupplier).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
                return NotFound();

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
