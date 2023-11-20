using DemoPortal.Backend.Shared.BusinessLogic;

namespace DemoPortal.Backend.Documents.Abstractions.Errors;

public class DocumentsErrorModels
{
    public static ErrorModel DocumentNotFound
        => new ErrorModel(DocumentsErrorModelKeys.DocumentNotFound, "Document not found");
    
    public static ErrorModel DocumentDeletionError
        => new ErrorModel(DocumentsErrorModelKeys.DocumentDeletionError, "Error occured during document deletion");
    
    public static ErrorModel UserNotProvided
        => new ErrorModel(DocumentsErrorModelKeys.UserNotProvided, "User not provided");
    
    public static ErrorModel TitleIsEmpty
        => new ErrorModel(DocumentsErrorModelKeys.TitleIsEmpty, "Document title is empty");
    
    public static ErrorModel TitleIsTooLong
        => new ErrorModel(DocumentsErrorModelKeys.TitleIsTooLong, "Document title is too long");
    
    public static ErrorModel TextIsEmpty
        => new ErrorModel(DocumentsErrorModelKeys.TextIsEmpty, "Document text is empty");
    
    public static ErrorModel TextIsTooLong
        => new ErrorModel(DocumentsErrorModelKeys.TextIsTooLong, "Document text is too long");
}