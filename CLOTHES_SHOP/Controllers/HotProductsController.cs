using CLOTHES_SHOP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLOTHES_SHOP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotProductsController : ControllerBase
    {
        private readonly ClothesShopDbContext _context;

        public HotProductsController(ClothesShopDbContext context)
        {
            _context = context;
        }

        //Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotProduct>>> GetHotProducts()
        {
            return await _context.HotProducts.ToListAsync();
        }

        //Get by id
        [HttpGet("{id}")]
        public async Task<ActionResult<HotProduct>> GetHotProduct(int id)
        {
            var hotproduct = await _context.HotProducts.FindAsync(id);
            if (hotproduct == null)
            {
                return NotFound();
            }
            return hotproduct;
        }
        //Put
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotProduct(int id, HotProduct hotProduct)
        {
            if (id != hotProduct.HotProductId)
            {
                return BadRequest();
            }

            _context.Entry(hotProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();

        }

        private bool HotProductExists(int id)
        {
            return _context.HotProducts.Any(e => e.HotProductId == id);
        }

        //Post
        [HttpPost]
        public async Task<ActionResult<HotProduct>> PostHotProduct(HotProduct hotProduct)
        {
            _context.HotProducts.Add(hotProduct);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetHotProduct", new { id = hotProduct.HotProductId }, hotProduct);
        }

        //Delete
        [HttpDelete]
        public async Task<IActionResult> DeleteHotProduct(int id)
        {
            var hotProduct  = await _context.HotProducts.FindAsync(id);
            if (hotProduct == null)
            {
                return NotFound();
            }
            _context.HotProducts.Remove(hotProduct);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
