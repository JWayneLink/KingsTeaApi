using KTA.Data.Entity;
using KTA.Data.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTA.Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbContextFactory<KTADbContext> _ctx;

        public CustomerRepository(IDbContextFactory<KTADbContext> ctx)
        {
            _ctx = ctx;
        }

        public async Task<int> AddAsync(CustomerEntity item)
        {
            using (var context = _ctx.CreateDbContext())
            {
                context.Entry(item).State = EntityState.Added;

                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteAsync(CustomerEntity item)
        {
            using (var context = _ctx.CreateDbContext())
            {
                context.Entry(item).State = EntityState.Deleted;

                return await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CustomerEntity>> GetAllItemsAsync()
        {
            using (var context = _ctx.CreateDbContext())
            {
                return await context.Customer.ToListAsync();
            }
        }

        public async Task<CustomerEntity> GetSingleItemAsync(CustomerEntity item)
        {
            using (var context = _ctx.CreateDbContext())
            {
                return await context.Customer.Where(x => x.CustId.Equals(item.CustId)).FirstOrDefaultAsync();
            }
        }

        public async Task<CustomerEntity> GetSingleItemAsync(string custId)
        {
            using (var context = _ctx.CreateDbContext())
            {
                return await context.Customer.Where(x => x.CustId.Equals(custId)).FirstOrDefaultAsync();
            }
        }

        public async Task<int> UpdateAsync(CustomerEntity item)
        {
            using (var context = _ctx.CreateDbContext())
            {
                context.Entry(item).State = EntityState.Modified;

                return await context.SaveChangesAsync();
            }
        }
    }
}
