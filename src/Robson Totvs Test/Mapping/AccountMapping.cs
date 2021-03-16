using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Robson_Totvs_Test.Domain.Entities;

namespace Robson_Totvs_Test.Mapping
{
    public class AccountMapping : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(x => x.LastLogin).IsRequired(false);
            builder.Property(x => x.Created).IsRequired();
            builder.Property(x => x.Modified).IsRequired(false);
            //_ = builder.Property(x => x.LastLogin).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(128).IsRequired(true);

            builder.HasMany(x => x.Profiles).WithOne().HasForeignKey(x => x.AccountId);
        }
    }
}