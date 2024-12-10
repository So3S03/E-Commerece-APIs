using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karim.ECommerce.Shared.Dtos.Products
{
    public class BrandInCategoryDto
    {
        public int Id { get; set; }
        public required string BrandName { get; set; }
    }
}
