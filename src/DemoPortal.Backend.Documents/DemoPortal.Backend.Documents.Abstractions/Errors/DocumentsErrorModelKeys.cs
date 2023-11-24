namespace DemoPortal.Backend.Documents.Abstractions.Errors;

/// <summary>
/// Document API error codes
/// </summary>
public class DocumentsErrorModelKeys
{
    /// <summary>
    /// Exception
    /// </summary>
    public const string Exception = "documents/exception";
    
    /// <summary>
    /// Documents common error
    /// </summary>
    public const string CommonError = "documents/common-error";
    
    /// <summary>
    /// Document not found
    /// </summary>
    public const string DocumentNotFound = "documents/not-found";
    
    /// <summary>
    /// Document ID not provided
    /// </summary>
    public const string DocumentIdNotProvided = "documents/id-not-provided";
    
    /// <summary>
    /// Error occured during document updating
    /// </summary>
    public const string DocumentUpdatingError = "documents/updating-error";
    
    /// <summary>
    /// Error occured during document deletion
    /// </summary>
    public const string DocumentDeletionError = "documents/deletion-error";
    
    /// <summary>
    /// User (ID) not provided
    /// </summary>
    public const string UserNotProvided = "documents/user-not-provided";
    
    /// <summary>
    /// Document title is empty
    /// </summary>
    public const string TitleIsEmpty = "documents/empty-title";
    
    /// <summary>
    /// Document title is too long
    /// </summary>
    public const string TitleIsTooLong = "documents/title-too-long";
    
    /// <summary>
    /// Document text is empty
    /// </summary>
    public const string TextIsEmpty = "documents/empty-text";
    
    /// <summary>
    /// Document text is too long
    /// </summary>
    public const string TextIsTooLong = "documents/text-too-long";
}