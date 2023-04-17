using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CLOTHES_SHOP.Models
{
    public class OrderInfo
    {
        [Key]
        public long OrderId { get; set; }
        public long Amount { get; set; }
        public string OrderDesc { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
        public long PaymentTranId { get; set; }
        public string BankCode { get; set; }
        public string PayStatus { get; set; }
        public string Locale { get; set; }
        public string Vnp_BankCode { get; set; }
        public string OrderCategory { get; set; }
        public long ReferenceCode { get; set; }
    }
}
