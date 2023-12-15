using System.ComponentModel.DataAnnotations;

namespace WebShopAppp.Models.Category
{
    public class CategoryPairVM
    {
        public int Id { get; set; }

        [Display(Name = "Category")]
        public string Name { get; set; } = null!;
    }
}
