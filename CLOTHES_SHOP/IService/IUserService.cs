using CLOTHES_SHOP.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLOTHES_SHOP.IService
{
    public interface IUserService
    {
        Task<ActionResult<User>> Signup(User user);
        UserLoginModel Login(UserLoginModel model);
        List<User> GetAllUsers();
    }
}
