using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CLOTHES_SHOP.Models
{
    public class NewsCategory
    {
        [Key]
        public int NewCateId { get; set; }
        public string NewCateName { get; set; }
    }
}
