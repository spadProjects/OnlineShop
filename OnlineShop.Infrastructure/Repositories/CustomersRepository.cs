﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Core.Models;

namespace OnlineShop.Infrastructure.Repositories
{
    public class CustomersRepository : BaseRepository<Customer, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public CustomersRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<Customer> GetCustomerTable()
        {
            return _context.Customers.Where(c => c.IsDeleted == false).Include(c => c.User).ToList();
        }

        public Customer GetCustomer(int id)
        {
            return _context.Customers.Include(c=>c.User).FirstOrDefault(c => c.Id == id);
        }
    }
}
