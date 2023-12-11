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
    public class CategoryService:ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Category GetCategoryById(int categoryid)
        {
            return _context.Categories.Find(categoryid);
        }

        public List<Category> GetCategories()
        {
            List<Category> categories = _context.Categories.ToList();
            return categories;
        }

        public List<Product> GetProductByCategory(int categoryId)
        {
            return _context.Products.Where(x => x.CategoryId == categoryId).ToList();
        }
    }
}
