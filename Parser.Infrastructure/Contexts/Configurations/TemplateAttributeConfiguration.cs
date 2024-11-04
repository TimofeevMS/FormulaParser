using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parser.Domain.Entities;

namespace Parser.Infrastructure.Contexts.Configurations;

public class TemplateAttributeConfiguration : IEntityTypeConfiguration<TemplateAttribute>
{
    public void Configure(EntityTypeBuilder<TemplateAttribute> builder)
    {
        builder.HasOne(a => a.Template)
               .WithMany(t => t.Attributes)
               .HasForeignKey(a => a.DataSheetTemplateId);
        
        builder.HasQueryFilter(e => !e.DeletedAt.HasValue);
    }
}