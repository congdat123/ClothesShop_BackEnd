using CLOTHES_SHOP.Chart;
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
    public class UserChartsController : ControllerBase
    {
        private readonly ClothesShopDbContext _context;
        public UserChartsController(ClothesShopDbContext context)
        {
            _context = context;
        }
        // GET: api/UserCharts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserChart>>> GetUserCharts()
        {
            return await _context.UserCharts.ToListAsync();
        }

        // GET: api/UserCharts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserChart>> GetUserChart(int id)
        {
            var userChart = await _context.UserCharts.FindAsync(id);

            if (userChart == null)
            {
                return NotFound();
            }

            return userChart;
        }

        // PUT: api/UserCharts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserChart(int id, UserChart userChart)
        {
            if (id != userChart.UserChartId)
            {
                return BadRequest();
            }

            _context.Entry(userChart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserChartExists(id))
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

        // POST: api/UserCharts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserChart>> PostUserChart(UserChart userChart)
        {
            _context.UserCharts.Add(userChart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserChart", new { id = userChart.UserChartId }, userChart);
        }

        // DELETE: api/UserCharts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserChart(int id)
        {
            var userChart = await _context.UserCharts.FindAsync(id);
            if (userChart == null)
            {
                return NotFound();
            }

            _context.UserCharts.Remove(userChart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserChartExists(int id)
        {
            return _context.UserCharts.Any(e => e.UserChartId == id);
        }
    }
}
