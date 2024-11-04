namespace Parser.Domain.Base;

/// <summary>
/// Аудитируемая сущность
/// </summary>
public class AuditableEntity : BaseEntity
{
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Дата обновления
    /// </summary>
    public DateTime UpdatedAt { get; set; }
    
    /// <summary>
    /// Дата удаления
    /// </summary>
    public DateTime? DeletedAt { get; set; }
}