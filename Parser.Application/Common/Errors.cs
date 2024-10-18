using Parser.Application.Common.Results;

namespace Parser.Application.Common;

public class Errors
{
    public static class Template
    {
        public static Error Identifier = Error.Failure("TemplateIdentifier.NotSpecified", "Template identifier not specified");
        public static Error NotFound = Error.NotFound("Template.NotFound", "Template not found");
    }
    
    public static class DataSheet
    {
        public static Error Identifier = Error.Failure("DataSheetIdentifier.NotSpecified", "DataSheet identifier not specified");
        public static Error NotFound = Error.NotFound("DataSheet.NotFound", "DataSheet not found");
    }
        
}