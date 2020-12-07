using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Core.Models;
using OnlineShop.Infrastructure.Extensions;

namespace OnlineShop.Infrastructure.Repositories
{
    public class DiscountsRepository : BaseRepository<Discount, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public DiscountsRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<Discount> GetDiscounts()
        {
            return _context.Discounts.Where(d => d.IsDeleted == false).DistinctBy(d=>d.GroupIdentifier).ToList();
        }
    }
}
