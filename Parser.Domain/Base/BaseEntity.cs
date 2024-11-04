using Parser.Domain.Interfaces;

namespace Parser.Domain.Base;

/// <summary>
/// Базовая сущность домена.
/// </summary>
public abstract class BaseEntity : IEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
}