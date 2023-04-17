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
    public class NewsCategoriesController : ControllerBase
    {
        private readonly ClothesShopDbContext _context;
        public NewsCategoriesController(ClothesShopDbContext context)
        {
            _context = context;
        }
        // GET: api/NewsCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsCategory>>> GetNewsCategories()
        {
            return await _context.NewsCategories.ToListAsync();
        }

        // GET: api/NewsCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NewsCategory>> GetNewsCategory(int id)
        {
            var newsCategory = await _context.NewsCategories.FindAsync(id);

            if (newsCategory == null)
            {
                return NotFound();
            }

            return newsCategory;
        }

        // PUT: api/NewsCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNewsCategory(int id, NewsCategory newsCategory)
        {
            if (id != newsCategory.NewCateId)
            {
                return BadRequest();
            }

            _context.Entry(newsCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsCategoryExists(id))
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

        // POST: api/NewsCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NewsCategory>> PostNewsCategory(NewsCategory newsCategory)
        {
            _context.NewsCategories.Add(newsCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNewsCategory", new { id = newsCategory.NewCateId }, newsCategory);
        }

        // DELETE: api/NewsCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNewsCategory(int id)
        {
            var newsCategory = await _context.NewsCategories.FindAsync(id);
            if (newsCategory == null)
            {
                return NotFound();
            }

            _context.NewsCategories.Remove(newsCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NewsCategoryExists(int id)
        {
            return _context.NewsCategories.Any(e => e.NewCateId == id);
        }
    }
}
