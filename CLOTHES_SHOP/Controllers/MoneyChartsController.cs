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
    public class MoneyChartsController : ControllerBase
    {
        private readonly ClothesShopDbContext _context;
        public MoneyChartsController(ClothesShopDbContext context)
        {
            _context = context;
        }
        // GET: api/MoneyCharts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MoneyChart>>> GetMoneyCharts()
        {
            return await _context.MoneyCharts.ToListAsync();
        }

        // GET: api/MoneyCharts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MoneyChart>> GetMoneyChart(int id)
        {
            var moneyChart = await _context.MoneyCharts.FindAsync(id);

            if (moneyChart == null)
            {
                return NotFound();
            }

            return moneyChart;
        }

        // PUT: api/MoneyCharts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMoneyChart(int id, MoneyChart moneyChart)
        {
            if (id != moneyChart.MoneyChartId)
            {
                return BadRequest();
            }

            _context.Entry(moneyChart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoneyChartExists(id))
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

        // POST: api/MoneyCharts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MoneyChart>> PostMoneyChart(MoneyChart moneyChart)
        {
            _context.MoneyCharts.Add(moneyChart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMoneyChart", new { id = moneyChart.MoneyChartId }, moneyChart);
        }

        // DELETE: api/MoneyCharts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoneyChart(int id)
        {
            var moneyChart = await _context.MoneyCharts.FindAsync(id);
            if (moneyChart == null)
            {
                return NotFound();
            }

            _context.MoneyCharts.Remove(moneyChart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MoneyChartExists(int id)
        {
            return _context.MoneyCharts.Any(e => e.MoneyChartId == id);
        }
    }
}
