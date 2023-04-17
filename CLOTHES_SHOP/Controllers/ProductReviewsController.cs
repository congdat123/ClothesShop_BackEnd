using CLOTHES_SHOP.Models;
using CLOTHES_SHOP.Service;
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
    public class ProductReviewsController : ControllerBase
    {
        private readonly ClothesShopDbContext _context;
        public ProductReviewsController(ClothesShopDbContext context)
        {
            _context = context;
        }
        // GET: api/ProductReviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductReview>>> GetProductReviews()
        {
            return await _context.ProductReviews.ToListAsync();
        }

        // GET: api/ProductReviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReview>> GetProductReview(int id)
        {
            var productReview = await _context.ProductReviews.FindAsync(id);

            if (productReview == null)
            {
                return NotFound();
            }

            return productReview;
        }

        // GET CustomerReview vid ProductId
        [HttpGet("viaProductId")]
        public async Task<ActionResult<IEnumerable<ProductReview>>> GetCustomerReviewviaProductId([FromQuery] string ProductId)
        {
            int tmp;
            tmp = int.Parse(ProductId);
            var data = from p in _context.ProductReviews
                       where (p.ProductId == tmp)
                       select (p);
            return await data.ToListAsync();
        }

        // GET CustomerReview vid Star
        [HttpGet("viaStar")]
        public async Task<ActionResult<IEnumerable<ProductReview>>> GetCustomerReviewviaStar([FromQuery] string Star, string ProductId)
        {
            int tmp;
            tmp = int.Parse(Star);
            int tmp2;
            tmp2 = int.Parse(ProductId);
            var data = from p in _context.ProductReviews
                       where (p.Star == tmp && p.ProductId==tmp2)
                       select (p);
            return await data.ToListAsync();
        }


        // PUT: api/ProductReviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductReview(int id, ProductReview productReview)
        {
            if (id != productReview.ProductReviewId)
            {
                return BadRequest();
            }

            _context.Entry(productReview).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductReviewExists(id))
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

        // POST: api/ProductReviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductReview>> PostProductReview(ProductReview productReview)
        {
            _context.ProductReviews.Add(productReview);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductReview", new { id = productReview.ProductReviewId }, productReview);
        }

        // DELETE: api/ProductReviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductReview(int id)
        {
            var productReview = await _context.ProductReviews.FindAsync(id);
            if (productReview == null)
            {
                return NotFound();
            }

            _context.ProductReviews.Remove(productReview);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool ProductReviewExists(int id)
        {
            return _context.ProductReviews.Any(e => e.ProductReviewId == id);
        }
    }
}
