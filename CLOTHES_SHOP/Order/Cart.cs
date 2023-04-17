using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CLOTHES_SHOP.Order
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Avatar { get; set; }
        public double Price { get; set; }
        public int Size { get; set; }
        public int Quantity { get; set; }
        public string UserName { get; set; }
    }
}
