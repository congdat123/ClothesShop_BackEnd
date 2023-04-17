using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CLOTHES_SHOP.Service
{
    public class ProductReview
    {
        [Key]
        public int ProductReviewId { get; set; }
        public string CustomerName { get; set; }
        public string UserName { get; set; }
        public string Avatar { get;set; }
        public int Star { get; set; }
        public string Content { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public ProductReview()
        {
            DateRate = DateTime.Now;
        }
        public DateTime? DateRate { get; set; }
    }
}
