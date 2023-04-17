using CLOTHES_SHOP.IService;
using CLOTHES_SHOP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CLOTHES_SHOP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ClothesShopDbContext _context;
        IUserService _userService = null;
        public AccountController(IUserService userService, ClothesShopDbContext context)
        {
            _context = context;
            _userService = userService;
        }
        //GET
        [HttpGet]
        public List<User> GetAllUsers()
        {
            return _userService.GetAllUsers();
        }

        //Get Info via UserName
        [HttpGet("viaUserName")]
        public async Task<ActionResult<IEnumerable<User>>> GetInfoViaUserName([FromQuery] string UserName)
        {
            var data = from p in _context.Users
                       where (p.UserName == UserName)
                       select (p);
            return await data.ToListAsync();
        }

        //Get User via MonthCreate
        [HttpGet("viaMonthCreate")]
        public async Task<ActionResult<IEnumerable<User>>> GetUserViaMonthCreate([FromQuery] string Month)
        {
            int tmp;
            tmp = int.Parse(Month);
            var data = from p in _context.Users
                       where p.DayCreated.Value.Month == tmp
                       select (p);
            return await data.ToListAsync();
        }

        // GET Account vid sort and pagination
        [HttpGet("viaSortingAndPagination")]
        public async Task<ActionResult<IEnumerable<User>>> GetAccountviaSortingandPagination([FromQuery] AccountPaginator filter)
        {
            var paginator = new AccountPaginator(filter.Page_size, filter.Current_page, filter.Sort);
            if (filter.Sort == "userId")
            {
                var data = from p in _context.Users
                           orderby p.UserId ascending
                           select (p);
                return await data
                    .Skip((paginator.Current_page - 1) * paginator.Page_size)
                    .Take(paginator.Page_size).ToListAsync();
            }
            if (filter.Sort == "-userId")
            {
                var data = from p in _context.Users
                           orderby p.UserId descending
                           select (p);
                return await data
                    .Skip((paginator.Current_page - 1) * paginator.Page_size)
                    .Take(paginator.Page_size).ToListAsync();
            }
            if (filter.Sort == "userName")
            {
                var data = from p in _context.Users
                           orderby p.UserName ascending
                           select (p);
                return await data
                    .Skip((paginator.Current_page - 1) * paginator.Page_size)
                    .Take(paginator.Page_size).ToListAsync();
            }
            if (filter.Sort == "-userName")
            {
                var data = from p in _context.Users
                           orderby p.UserName descending
                           select (p);
                return await data
                    .Skip((paginator.Current_page - 1) * paginator.Page_size)
                    .Take(paginator.Page_size).ToListAsync();
            }

            return BadRequest();
        }
        // POST api/<AccountController>
        [HttpPost]
        public async Task<ActionResult<User>> Signup(User user)
        {
            var tmp = _context.Users.FirstOrDefault(x => x.UserName == user.UserName);
            if (tmp == null)
            {
                return await _userService.Signup(user);
            }
            return BadRequest(new { state = "This account is already in use, please try another one" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginModel model)
        {
            var user = _userService.Login(model);
            if (user != null)
            {
                string secret = "g2sL6RLQh6dVRSSnCRjvEqP971-V2DGpFoUleIfKrIc";
                string iss = "dcaea177e8d14f1fb863059dde75ca3b";
                string audience = "Sneaker Shop";

                var singinKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, model.UserName),
                    new Claim(ClaimTypes.NameIdentifier, model.UserName),
                    new Claim("Title", "This is User of Sneaker Shop"),
                };

                var jwt = new JwtSecurityToken  (
                    issuer: iss,
                    audience: audience,
                    claims: claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddSeconds(60 * 60),
                    signingCredentials: new SigningCredentials(singinKey, SecurityAlgorithms.HmacSha256)
                    );
                var jwtToken = new JwtSecurityTokenHandler().WriteToken(jwt);

                return Ok(new
                {
                    access_token = jwtToken,
                    expires = DateTime.UtcNow.AddSeconds(60 * 60),
                    name = model.UserName
                });
            }
            return BadRequest(new { state = "invalid username or password" });
        }
    }
}
