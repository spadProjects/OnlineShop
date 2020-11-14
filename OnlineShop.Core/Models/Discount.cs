using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Models
{
    public class Discount : IBaseEntity
    {
        public int Id { get; set; }
        public int DiscountType { get; set; }
        public long Amount { get; set; }
        public int OfferId { get; set; }
        public Offer Offer { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int ProductGroupId { get; set; }
        public ProductGroup ProductGroup { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
