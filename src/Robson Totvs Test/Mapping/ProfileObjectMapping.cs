using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Robson_Totvs_Test.Domain.Entities;

namespace Robson_Totvs_Test.Mapping
{
    public class ProfileObjectMapping : IEntityTypeConfiguration<ProfileObject>
    {
        public void Configure(EntityTypeBuilder<ProfileObject> builder)
        {
            builder.Property(x => x.AccountId).IsRequired();
            builder.Property(x => x.Type).IsRequired();
        }
    }
}
