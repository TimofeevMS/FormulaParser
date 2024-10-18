using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parser.Domain.Entities;

namespace Parser.Infrastructure.Contexts.Configurations;

public class DataSheetValueConfiguration : IEntityTypeConfiguration<DataSheetValue>
{
    public void Configure(EntityTypeBuilder<DataSheetValue> builder)
    {
        builder.HasOne(v => v.DataSheet)
               .WithMany(e => e.Values)
               .HasForeignKey(v => v.DataSheetId);
        
        builder.HasOne(v => v.TemplateAttribute)
               .WithMany()
               .HasForeignKey(v => v.TemplateAttributeId);
    }
}