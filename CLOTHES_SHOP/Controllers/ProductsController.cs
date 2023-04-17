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
    public class ProductsController : ControllerBase
    {
        private readonly ClothesShopDbContext _context;

        public ProductsController(ClothesShopDbContext context)
        {
            _context = context;
        }

        //Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }
        //Get by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        //Put
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }

        //Post
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }
        
        //Delete
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        //Get Products Paginator and sort

        [HttpGet("viaSortingAndPagination")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductviaSortingAndPagination([FromQuery] ProductPaginator filter)
        {
            var paginator = new ProductPaginator(filter.Page_size, filter.Current_page, filter.Sort);
            if (filter.Sort == "productId")
            {
                var data = from p in _context.Products
                           orderby p.ProductId descending
                           select (p);
                return await data
                    .Skip((paginator.Current_page - 1) * paginator.Page_size)
                    .Take(paginator.Page_size).ToListAsync();
            }
            if (filter.Sort == "-productId")
            {
                var data = from p in _context.Products
                           orderby p.ProductId ascending
                           select (p);
                return await data
                    .Skip((paginator.Current_page - 1) * paginator.Page_size)
                    .Take(paginator.Page_size).ToListAsync();
            }
            if (filter.Sort == "productName")
            {
                var data = from p in _context.Products
                           orderby p.ProductName descending
                           select (p);
                return await data
                    .Skip((paginator.Current_page - 1) * paginator.Page_size)
                    .Take(paginator.Page_size).ToListAsync();
            }
            if (filter.Sort == "-productName")
            {
                var data = from p in _context.Products
                           orderby p.ProductName ascending
                           select (p);
                return await data
                    .Skip((paginator.Current_page - 1) * paginator.Page_size)
                    .Take(paginator.Page_size).ToListAsync();
            }
            return BadRequest();
        }

        // GET Products vid CategoryId
        [HttpGet("viaCategoryId")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductviaCategoryId([FromQuery] string CategoryId)
        {
            int tmp;
            tmp = int.Parse(CategoryId);
            var data = from p in _context.Products
                       where (p.CategoryId == tmp)
                       select (p);
            return await data.ToListAsync();
        }

        //SEARCH DATA 
        [HttpGet("SearchProduct")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProduct([FromQuery] string text)
        {
            int tmp;
            try
            {
                tmp = int.Parse(text);
            }
            catch (Exception ex)
            {
                var data = from p in _context.Products
                           where (p.ProductName.Contains(text))
                           orderby p.ProductName ascending
                           select (p);
                return await data
                    .Take(5)
                    .ToListAsync();
            }

            var data2 = from p in _context.Products
                        where ((p.ProductId == tmp) || (p.Price == tmp) || (p.ProductName.Contains(text)))
                        orderby p.ProductName ascending
                        select (p);
            return await data2
                .Take(5)
                .ToListAsync();
        }

    }
}
