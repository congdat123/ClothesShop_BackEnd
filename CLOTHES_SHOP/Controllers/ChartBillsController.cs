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
    public class ChartBillsController : ControllerBase
    {

        private readonly ClothesShopDbContext _context;
        public ChartBillsController(ClothesShopDbContext context)
        {
            _context = context;
        }
        // GET: api/ChartBills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChartBill>>> GetCharts()
        {
            return await _context.Charts.ToListAsync();
        }

        // GET: api/ChartBills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChartBill>> GetChartBill(int id)
        {
            var chartBill = await _context.Charts.FindAsync(id);

            if (chartBill == null)
            {
                return NotFound();
            }

            return chartBill;
        }

        // PUT: api/ChartBills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChartBill(int id, ChartBill chartBill)
        {
            if (id != chartBill.ChartId)
            {
                return BadRequest();
            }

            _context.Entry(chartBill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChartBillExists(id))
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

        // POST: api/ChartBills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ChartBill>> PostChartBill(ChartBill chartBill)
        {
            _context.Charts.Add(chartBill);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChartBill", new { id = chartBill.ChartId }, chartBill);
        }

        // DELETE: api/ChartBills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChartBill(int id)
        {
            var chartBill = await _context.Charts.FindAsync(id);
            if (chartBill == null)
            {
                return NotFound();
            }

            _context.Charts.Remove(chartBill);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChartBillExists(int id)
        {
            return _context.Charts.Any(e => e.ChartId == id);
        }
    }
}
