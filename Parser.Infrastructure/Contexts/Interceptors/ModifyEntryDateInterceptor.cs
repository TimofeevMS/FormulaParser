using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Parser.Domain.Base;

namespace Parser.Infrastructure.Contexts.Interceptors;

/// <summary>
/// Перехватчик для обновления свойств <see cref="AuditableEntity.CreatedAt" />, <see cref="AuditableEntity.UpdatedAt" /> и <see cref="AuditableEntity.DeletedAt" /> при сохранении.
/// </summary>
public class ModifyEntryDateInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
                                                                          InterceptionResult<int> result,
                                                                          CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            UpdateId(eventData.Context);
            UpdateTimestamps(eventData.Context);
        }

        return new ValueTask<InterceptionResult<int>>(result);
    }

    /// <summary>
    /// Обновление свойств <see cref="AuditableEntity.CreatedAt" />, <see cref="AuditableEntity.UpdatedAt" /> и <see cref="AuditableEntity.DeletedAt" />
    /// </summary>
    /// <param name="context">Контекст данных</param>
    private void UpdateTimestamps(DbContext context)
    {
        var entries = context.ChangeTracker.Entries<AuditableEntity>();
        
        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = GetCorrectDateTime(entry.Entity.CreatedAt);
                    entry.Entity.UpdatedAt = GetCorrectDateTime(entry.Entity.CreatedAt);
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = GetCorrectDateTime(entry.Entity.CreatedAt);
                    break;
                case EntityState.Deleted:
                    entry.Entity.DeletedAt = GetCorrectDateTime(entry.Entity.DeletedAt);
                    entry.State = EntityState.Modified;
                    break;
            }
        }
    }
    
    /// <summary>
    /// Обновление свойства <see cref="BaseEntity.Id" />
    /// </summary>
    /// <param name="context">Контекст данных</param>
    private void UpdateId(DbContext context)
    {
        var entries = context.ChangeTracker.Entries<BaseEntity>();
        
        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Id = Guid.NewGuid();
                    break;
            }
        }
    }

    /// <summary>
    /// Получение корректного значения поля <see cref="AuditableEntity.CreatedAt" />, <see cref="AuditableEntity.UpdatedAt" /> и <see cref="AuditableEntity.DeletedAt" />.
    /// </summary>
    /// <param name="timestamp">Значение поля <see cref="AuditableEntity.CreatedAt" />, <see cref="AuditableEntity.UpdatedAt" /> или <see cref="AuditableEntity.DeletedAt" /></param>
    private DateTime GetCorrectDateTime(DateTime? timestamp)
    {
        if (timestamp is null)
            return DateTime.UtcNow;
        
        return !timestamp.Value.Equals(new DateTime()) ? timestamp.Value : DateTime.UtcNow;
    }
}