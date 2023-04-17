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
    public class CarouselsController : ControllerBase
    {
        private readonly ClothesShopDbContext _context;

        public CarouselsController(ClothesShopDbContext context)
        {
            _context = context;
        }

        //Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carousel>>> GetCarousels()
        {
            return await _context.Carousels.ToListAsync();
        }

        //Get by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Carousel>> GetCarousel(int id)
        {
            var carousel = await _context.Carousels.FindAsync(id);
            if (carousel == null)
            {
                return NotFound();
            }

            return carousel;
        }

        //Put
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarousel(int id, Carousel carousel)
        {
            if (id != carousel.CarouselId)
            {
                return BadRequest();
            }

            _context.Entry(carousel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarouselExists(id))
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

        private bool CarouselExists(int id)
        {
            return _context.Carousels.Any(e => e.CarouselId == id);
        }
    }
}
