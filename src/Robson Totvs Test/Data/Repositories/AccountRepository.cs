using Dapper;
using Microsoft.EntityFrameworkCore;
using Robson_Totvs_Test.Application.DTO.Models.Request;
using Robson_Totvs_Test.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Robson_Totvs_Test.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private TotvsTestDbContext _ctx;

        public AccountRepository(TotvsTestDbContext ctx)
        {
            this._ctx = ctx;
        }

        public async Task<bool> AddAsync(Account newObj)
        {
            _ctx.Users.Add(newObj);

            var success = await this._ctx.SaveChangesAsync() > 0;

            return success;
        }

        public async Task<Account[]> FindAllAsync(Expression<Func<Account, bool>> filter)
        {
            return await this._ctx.Users.Where(filter)
                .Include(x => x.Profiles)
                .ToArrayAsync();
        }

        public async Task<Account> FindAsync(Expression<Func<Account, bool>> filter)
        {
            return await this._ctx.Users.Where(filter)
                .Include(x => x.Profiles)
                .FirstOrDefaultAsync();
        }

        public async Task<Account[]> FindAllAsyncWithDapperAsync(string sqlCommand)
        {
            

            return (await _ctx.Database.GetDbConnection().QueryAsync<Account>(sqlCommand)).AsList().ToArray();
        }
    }
}