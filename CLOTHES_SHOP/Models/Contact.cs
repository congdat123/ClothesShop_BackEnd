using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CLOTHES_SHOP.Models
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }

    }
}
