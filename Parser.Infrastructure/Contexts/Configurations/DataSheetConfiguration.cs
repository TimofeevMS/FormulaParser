using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parser.Domain.Entities;

namespace Parser.Infrastructure.Contexts.Configurations;

public class DataSheetConfiguration : IEntityTypeConfiguration<DataSheet>
{
    public void Configure(EntityTypeBuilder<DataSheet> builder)
    {
        builder.HasQueryFilter(e => !e.DeletedAt.HasValue);
    }
}