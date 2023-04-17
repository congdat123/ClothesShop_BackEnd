using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CLOTHES_SHOP.Chart
{
    public class ChartBill
    {
        [Key]
        public int ChartId { get; set; }
        public string ChartMonth { get; set; }
        public string ChartSumOfBill { get; set; }
    }
}
