using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLOTHES_SHOP.Models;
using CLOTHES_SHOP.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace SNEAKER_SHOP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly ClothesShopDbContext _context;

        public BillsController(ClothesShopDbContext context)
        {
            _context = context;
        }

        // GET: api/Bills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bill>>> GetBills()
        {
            return await _context.Bills.ToListAsync();
        }

        // GET: api/Bills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bill>> GetBill(int id)
        {
            var bill = await _context.Bills.FindAsync(id);

            if (bill == null)
            {
                return NotFound();
            }

            return bill;
        }

        // GET Bill vid Status
        [HttpGet("viaStatus")]
        public async Task<ActionResult<IEnumerable<Bill>>> GetBillViaStatus([FromQuery] string Status)
        {
            var data = from p in _context.Bills
                       where (p.Status == Status)
                       select (p);
            return await data.ToListAsync();
        }

        //Get Bill via UserName
        [HttpGet("viaUserName")]
        public async Task<ActionResult<IEnumerable<Bill>>> GetBillViaUserName([FromQuery] string UserName)
        {
            var data = from p in _context.Bills
                       where (p.UserName == UserName)
                       select (p);
            return await data.ToListAsync();
        }

        //Get Bill via MonthOrder
        [HttpGet("viaMonthOrder")]
        public async Task<ActionResult<IEnumerable<Bill>>> GetBillViaMonthOrder([FromQuery] string Month)
        {
            int tmp;
            tmp = int.Parse(Month);
            var data = from p in _context.Bills
                       where p.DayOrder.Value.Month == tmp
                       select (p);
            return await data.ToListAsync();
        }

        // GET Bill vid sort and pagination
        [HttpGet("viaSortingAndPagination")]
        public async Task<ActionResult<IEnumerable<Bill>>> GetBillviaSortingandPagination([FromQuery] Paginator filter)
        {
            var paginator = new Paginator(filter.Page_size, filter.Current_page, filter.Sort, filter.Status);
            if (filter.Sort == "billId")
            {
                var data = from p in _context.Bills
                           where (p.Status == filter.Status)
                           orderby p.BillId ascending
                           select (p);
                return await data
                    .Skip((paginator.Current_page - 1) * paginator.Page_size)
                    .Take(paginator.Page_size).ToListAsync();
            }
            if (filter.Sort == "-billId")
            {
                var data = from p in _context.Bills
                           where (p.Status == filter.Status)
                           orderby p.BillId descending
                           select (p);
                return await data
                    .Skip((paginator.Current_page - 1) * paginator.Page_size)
                    .Take(paginator.Page_size).ToListAsync();
            }
            if (filter.Sort == "customerName")
            {
                var data = from p in _context.Bills
                           where (p.Status == filter.Status)
                           orderby p.CustomerName ascending
                           select (p);
                return await data
                    .Skip((paginator.Current_page - 1) * paginator.Page_size)
                    .Take(paginator.Page_size).ToListAsync();
            }
            if (filter.Sort == "-customerName")
            {
                var data = from p in _context.Bills
                           where (p.Status == filter.Status)
                           orderby p.CustomerName descending
                           select (p);
                return await data
                    .Skip((paginator.Current_page - 1) * paginator.Page_size)
                    .Take(paginator.Page_size).ToListAsync();
            }

            return BadRequest();
        }

        //SEARCH DATA 
        [HttpGet("SearchBill")]
        public async Task<ActionResult<IEnumerable<Bill>>> SearchBill([FromQuery] string search)
        {
            int tmp;
            try
            {
                tmp = int.Parse(search);
            }
            catch (Exception ex)
            {
                var data = from p in _context.Bills
                           where (p.CustomerName.Contains(search) || p.Address.Contains(search) || p.UserName.Contains(search))
                           select (p);
                return await data.ToListAsync();
            }
            return BadRequest();
        }

        // PUT: api/Bills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBill(int id, Bill bill)
        {
            if (id != bill.BillId)
            {
                return BadRequest();
            }

            _context.Entry(bill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillExists(id))
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

        // POST: api/Bills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bill>> PostBill(Bill bill)
        {
            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBill", new { id = bill.BillId }, bill);
        }

        // DELETE: api/Bills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBill(int id)
        {
            var bill = await _context.Bills.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }

            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BillExists(int id)
        {
            return _context.Bills.Any(e => e.BillId == id);
        }
    }
}
