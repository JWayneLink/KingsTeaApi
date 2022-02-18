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
    public class ProductRepository : IProductRepository
    {
        private readonly IDbContextFactory<KTADbContext> _ctx;

        public ProductRepository(IDbContextFactory<KTADbContext> ctx)
        {
            _ctx = ctx;
        }

        public async Task<int> AddAsync(ProductEntity item)
        {
            using (var context = _ctx.CreateDbContext())
            {
                context.Entry(item).State = EntityState.Added;

                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteAsync(ProductEntity item)
        {
            using (var context = _ctx.CreateDbContext())
            {
                context.Entry(item).State = EntityState.Deleted;

                return await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ProductEntity>> GetAllItemsAsync()
        {
            using (var context = _ctx.CreateDbContext())
            {
                return await context.Product.ToListAsync();
            }
        }

        public async Task<ProductEntity> GetSingleItemAsync(ProductEntity item)
        {
            using (var context = _ctx.CreateDbContext())
            {
                return await context.Product.Where(x => x.Pn.Equals(item.Pn)).FirstOrDefaultAsync();
            }
        }

        public async Task<ProductEntity> GetSingleItemAsync(string pn)
        {
            using (var context = _ctx.CreateDbContext())
            {
                return await context.Product.Where(x => x.Pn.Equals(pn)).FirstOrDefaultAsync();
            }
        }

        public async Task<int> UpdateAsync(ProductEntity item)
        {
            using (var context = _ctx.CreateDbContext())
            {
                context.Entry(item).State = EntityState.Modified;

                return await context.SaveChangesAsync();
            }
        }
    }
}
