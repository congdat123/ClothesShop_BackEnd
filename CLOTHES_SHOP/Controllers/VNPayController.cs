using CLOTHES_SHOP.IService.VNPay;
using CLOTHES_SHOP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLOTHES_SHOP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VNPayController : ControllerBase
    {
        private readonly IVNPayService _vnpayService;
        public VNPayController(IVNPayService vNPayService)
        {
            this._vnpayService = vNPayService;
        }

        [HttpPost]
        public string Post([FromBody] OrderInfo order)
        {
            return this._vnpayService.CreateOrder(order);
        }
    }
}
