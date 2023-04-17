using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLOTHES_SHOP.Models
{
    public class ProductPaginator
    {
        public int Page_size { get; set; }
        public int Current_page { get; set; }
        public string Sort { get; set; }
        public ProductPaginator()
        {
            this.Page_size = 9;
            this.Current_page = 1;
            this.Sort = "productId";
        }

        public ProductPaginator(int page_size, int current_page, string sort)
        {
            this.Page_size = page_size > 15 ? 15 : page_size;
            this.Current_page = current_page < 1 ? 1 : current_page;
            this.Sort = sort;
        }
    }
}
