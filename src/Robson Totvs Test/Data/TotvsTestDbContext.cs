using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Robson_Totvs_Test.Domain.Entities;
using Robson_Totvs_Test.Mapping;

namespace Robson_Totvs_Test.Data
{
    public class TotvsTestDbContext : IdentityDbContext<Account>
    {
        public TotvsTestDbContext(DbContextOptions<TotvsTestDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new AccountMapping());
            builder.ApplyConfiguration(new ProfileObjectMapping());
        }
    }
}
