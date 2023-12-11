using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebShopAppp.Infrastructure.Data.Domain;

namespace WebShopAppp.Core.Contracts
{
    public interface ICategoryService
    {
        List<Category> GetCategories();
        Category GetCategoryById(int categoryid);
        List<Product> GetProductByCategory(int categoryId);
    }
}
