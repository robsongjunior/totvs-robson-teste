using Dapper;
using Microsoft.EntityFrameworkCore;
using Robson_Totvs_Test.Domain.Entities;
using Robson_Totvs_Test.Domain.Interfaces.Repositories;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Robson_Totvs_Test.Data.Repositories
{
    public class ProfileObjectRepository : IProfileObjectRepository
    {
        private TotvsTestDbContext _ctx;


        public ProfileObjectRepository(TotvsTestDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<bool> AddAsync(ProfileObject newObj)
        {
            this._ctx.Profiles.Add(newObj);

            var success = await this._ctx.SaveChangesAsync() > 0;

            return success;
        }

        public async Task<ProfileObject[]> FindAllAsync(Expression<Func<ProfileObject, bool>> filter)
        {
            return await _ctx.Profiles.ToArrayAsync();
        }

        public async Task<ProfileObject> FindAsync(Expression<Func<ProfileObject, bool>> filter)
        {
            return await _ctx.Profiles.FirstOrDefaultAsync(filter);
        }

        public async Task<ProfileObject[]> FindAllAsyncWithDapperAsync(string sqlCommand)
        {
            return (await _ctx.Database.GetDbConnection().QueryAsync<ProfileObject>(sqlCommand)).AsList().ToArray();
        }
    }
}