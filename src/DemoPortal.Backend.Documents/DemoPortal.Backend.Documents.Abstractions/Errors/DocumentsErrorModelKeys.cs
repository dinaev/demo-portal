namespace DemoPortal.Backend.Documents.Abstractions.Errors;

public class DocumentsErrorModelKeys
{
    public const string DocumentNotFound = "documents/not-found";
    public const string DocumentDeletionError = "documents/deletion-error";
    public const string UserNotProvided = "documents/user-not-provided";
    public const string TitleIsEmpty = "documents/empty-title";
    public const string TitleIsTooLong = "documents/title-too-long";
    public const string TextIsEmpty = "documents/empty-text";
    public const string TextIsTooLong = "documents/text-too-long";
}