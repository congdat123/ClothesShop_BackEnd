using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CLOTHES_SHOP.Chart
{
    public class UserChart
    {
        [Key]
        public int UserChartId { get; set; }
        public string UserChartMonth { get; set; }
        public int ChartSumOfUser { get; set; }
    }
}
