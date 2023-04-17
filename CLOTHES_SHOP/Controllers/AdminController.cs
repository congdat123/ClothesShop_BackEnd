using CLOTHES_SHOP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class AdminController : ControllerBase
    {
        [Authorize(AuthenticationSchemes = "AuthKey")]
        [Route("index")]
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(new { User.Identity.Name });
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] AdminLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.UserName == "Administrator" && model.Password == "nhom1ltw")
            {
                string secret = "g2sL6RLQh6dVRSSnCRjvEqP971-V2DGpFoUleIfKrIc";
                string iss = "dcaea177e8d14f1fb863059dde75ca3b";
                string audience = "Sneaker Shop";

                var singinKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, model.UserName),
                    new Claim(ClaimTypes.NameIdentifier, model.UserName),
                    new Claim("Title", "This is Admin of Sneaker Shop"),
                    new Claim("Role",model.UserName),
                };

                var jwt = new JwtSecurityToken(
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
                    role = model.UserName
                });
            }

            return BadRequest(new { state = "invalid username or password" });
        }
    }
}
