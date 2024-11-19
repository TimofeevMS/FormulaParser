using Parser.Application.Common.Results;

namespace Parser.Application.Common;

public static class Errors
{
    public static class Template
    {
        public static Error Identifier = Error.Failure("Template.IdentifierNotSpecified", "Идентификатор шаблона не указан");
        public static Error NotFound = Error.NotFound("Template.NotFound", "Шаблон не найден");
    }
    
    public static class DataSheet
    {
        public static Error Identifier = Error.Failure("DataSheet.IdentifierNotSpecified", "Идентификатор листа данных не указан");
        public static Error NotFound = Error.NotFound("DataSheet.NotFound", "Лист данных не найден");
    }

    public static class Attribute
    {
        public static Error NotFound = Error.NotFound("Attribute.NotFound", "Атрибут не найден");
    }
}