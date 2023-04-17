using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CLOTHES_SHOP.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public string BankCode { get; set; }
        public string CardCode { get; set; }
        public string CardHolder { get; set; }
        public string CardDate { get; set; }
        public double Amount { get; set; }
        public Transaction()
        {
            PayDate = DateTime.Now;
        }
        public DateTime? PayDate { get; set; }
        public string Description { get; set; }
        public int BillId { get; set; }
    }
}
