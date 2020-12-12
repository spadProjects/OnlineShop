using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Core.Models;

namespace OnlineShop.Infrastructure.Repositories
{
    public class InvoicesRepository : BaseRepository<Invoice, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public InvoicesRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<Invoice> GetInvoices()
        {
            return _context.Invoices.Include(i => i.Customer.User).ToList();
        }
    }
}
