using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineShop.Core.Models;

namespace OnlineShop.Web.ViewModels
{
    public class CreateDiscountViewModel
    {
        public List<Offer> Offers { get; set; }
        public List<Brand> Brands { get; set; }
        public List<ProductGroup> ProductGroups { get; set; }
        public List<Product> Products { get; set; }
    }
}