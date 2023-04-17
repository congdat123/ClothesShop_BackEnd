using CLOTHES_SHOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLOTHES_SHOP.IService.VNPay
{
    public interface IVNPayService
    {
        string CreateOrder(OrderInfo orderInfo);
    }
}
