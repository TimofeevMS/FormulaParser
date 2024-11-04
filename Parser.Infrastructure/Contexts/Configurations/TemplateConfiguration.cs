using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parser.Domain.Entities;

namespace Parser.Infrastructure.Contexts.Configurations;

public class TemplateConfiguration : IEntityTypeConfiguration<Template>
{
    public void Configure(EntityTypeBuilder<Template> builder)
    {
        builder.HasQueryFilter(e => !e.DeletedAt.HasValue);
    }
}