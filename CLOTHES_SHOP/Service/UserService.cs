using CLOTHES_SHOP.IService;
using CLOTHES_SHOP.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLOTHES_SHOP.Service
{
    public class UserService : IUserService
    {
        private readonly ClothesShopDbContext _context;
        public UserService(ClothesShopDbContext context)
        {
            _context = context;
        }
        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public UserLoginModel Login(UserLoginModel model)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserName == model.UserName);
            if (user != null)
            {
                bool isValidPassword = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);
                if (isValidPassword)
                {
                    return model;
                }
            }
            return null;
        }

        public async Task<ActionResult<User>> Signup(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user; ;
        }
    }
}
