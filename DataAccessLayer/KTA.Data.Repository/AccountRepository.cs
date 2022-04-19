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
    public class AccountRepository: IAccountRepository
    {
        private readonly IDbContextFactory<KTADbContext> _ctx;

        public AccountRepository(IDbContextFactory<KTADbContext> ctx)
        {
            _ctx = ctx;
        }

        public async Task<int> AddAsync(AccountEntity item)
        {
            using (var context = _ctx.CreateDbContext())
            {
                context.Entry(item).State = EntityState.Added;

                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteAsync(AccountEntity item)
        {
            using (var context = _ctx.CreateDbContext())
            {
                context.Entry(item).State = EntityState.Deleted;

                return await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<AccountEntity>> GetAllItemsAsync()
        {
            using (var context = _ctx.CreateDbContext())
            {
                return await context.Account.ToListAsync();
            }
        }

        public async Task<AccountEntity> GetSingleItemAsync(AccountEntity item)
        {
            using (var context = _ctx.CreateDbContext())
            {
                return await context.Account.Where(x=> x.Account.Equals(item.Account)).FirstOrDefaultAsync();
            }
        }

        public async Task<AccountEntity> GetSingleItemAsync(string account)
        {
            using (var context = _ctx.CreateDbContext())
            {                
                return await context.Account.Where(x => x.Account.Equals(account)).FirstOrDefaultAsync();
            }
        }

        public async Task<AccountEntity> GetAccountByEmail(string account, string email)
        {
            using (var context = _ctx.CreateDbContext())
            {
                return await context.Account.Where(x => x.Account.Equals(account) && x.Email.Equals(email)).FirstOrDefaultAsync();
            }
        }

        public async Task<int> UpdateAsync(AccountEntity item)
        {
            using (var context = _ctx.CreateDbContext())
            {
                context.Entry(item).State = EntityState.Modified;

                return await context.SaveChangesAsync();
            }
        }
    }
}
