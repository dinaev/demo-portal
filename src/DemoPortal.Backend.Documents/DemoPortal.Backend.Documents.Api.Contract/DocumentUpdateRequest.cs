namespace DemoPortal.Backend.Documents.Api.Contract;

public class DocumentUpdateRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
}