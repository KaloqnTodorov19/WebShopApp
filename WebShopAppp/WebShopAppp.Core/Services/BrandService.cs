using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebShopAppp.Core.Contracts;
using WebShopAppp.Infrastructure.Data;
using WebShopAppp.Infrastructure.Data.Domain;

namespace WebShopAppp.Core.Services
{
    public class BrandService : IBrandService
    {
        private readonly ApplicationDbContext _context;

        public BrandService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Brand GetBrandById(int brandid)
        {
            return _context.Brands.Find(brandid);
        }

        public List<Brand> GetBrands()
        {
            List<Brand> brands = _context.Brands.ToList();
            return brands;
        }

        public List<Product> GetProductByBrand(int brandId)
        {
            return _context.Products.Where(x => x.BrandId == brandId).ToList();
        }
    }
}
