using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebShopAppp.Infrastructure.Data.Domain;

namespace WebShopAppp.Core.Contracts
{
    public interface IBrandService
    {
        List<Brand> GetBrands();
        Brand GetBrandById(int brandid);
        List<Product> GetProductByBrand(int brandId);
    }
}
