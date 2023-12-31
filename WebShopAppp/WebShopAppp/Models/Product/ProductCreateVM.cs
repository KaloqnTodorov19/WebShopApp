﻿using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

using WebShopAppp.Infrastructure.Data.Domain;
using WebShopAppp.Models.Brand;
using WebShopAppp.Models.Category;

namespace WebShopAppp.Models.Product
{
    public class ProductCreateVM
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        [Display(Name = "Product Name")]

        public string ProductName { get; set; } = null!;
        [Required]
        [Display(Name = "Brand")]
        public int BrandId { get; set; }
        public virtual List<BrandPairVM> Brands { get; set; } = new List<BrandPairVM>();


        [Required]
        [Display(Name = "Category")]
        public virtual List<CategoryPairVM> Categories { get; set; } = new List<CategoryPairVM>();
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Picture")]
        public string Picture { get; set; } = null!;

        [Range(0, 5000)]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Price")]

        public decimal Price { get; set; }

        [Display(Name = "Discount")]

        public decimal Discount { get; set; }
    }
}
