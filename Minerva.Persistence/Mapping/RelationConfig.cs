using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Minerva.Persistence.Entities.Relations;

namespace Minerva.Persistence.Mapping
{
    public class RelationConfig : IEntityTypeConfiguration<Relation>
    {
        public void Configure(EntityTypeBuilder<Relation> builder)
        {
            builder.ToTable("Relations");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.RelationType)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(r => r.MetaDataJson)
                .HasColumnType("nvarchar(max)");

            builder.Property(r => r.MetaDataBinary)
                .HasColumnType("varbinary(max)");

            builder.HasIndex(r => new { r.SourceEntityId, r.RelationType });
            builder.HasIndex(r => new { r.TargetEntityId });
        }
    }
}
